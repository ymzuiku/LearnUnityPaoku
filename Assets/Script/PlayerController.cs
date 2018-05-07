using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 主角控制器
/// </summary>
public class PlayerController : MonoBehaviour {

    /// <summary>
    /// 速度
    /// </summary>
    public float speed = 1;

    /// <summary>
    /// 出事速度
    /// </summary>
    public float init_speed=5;

    /// <summary>
    /// 最大速度
    /// </summary>
    private float maxSpeed = 20;

    /// <summary>
    /// 手势滑动方向
    /// </summary>
    InputDirection inputDirection;

    /// <summary>
    /// 输入位置
    /// </summary>
    Vector3 mousePos;

    /// <summary>
    /// 是否已经输入
    /// </summary>
    bool activeInput;

    /// <summary>
    /// 站立位置
    /// </summary>
    Position standPosition;

    /// <summary>
    /// 从什么位置跳过来
    /// </summary>
    Position fromPosition;

    /// <summary>
    /// x方向移动
    /// </summary>
    Vector3 xDirection;

    /// <summary>
    /// y和z移动
    /// </summary>
    Vector3 moveDirection;

    /// <summary>
    /// 任务控制器
    /// </summary>
    CharacterController characterController;

    /// <summary>
    /// 是否在roll
    /// </summary>
    public bool isRoll = false;

    /// <summary>
    /// 向上跳动距离
    /// </summary>
    float jumpValue = 7;

    /// <summary>
    /// 重力
    /// </summary>
    float gravity = 20;

    /// <summary>
    /// 是否能双连跳
    /// </summary>
    public bool canDoubleJump = true;

    /// <summary>
    /// 是否在双连跳
    /// </summary>
    bool doubleJump = false;

    /// <summary>
    /// 是否在暴走
    /// </summary>
    bool isQuickMoving = false;

    /// <summary>
    /// 保存暴走前的速度
    /// </summary>
    float saveSpeed;

    /// <summary>
    /// 暴走持续时间
    /// </summary>
    float quickMoveDuration = 3;

    /// <summary>
    /// 暴走剩余时间
    /// </summary>
    public float quickMoveTimeLeft;

    /// <summary>
    /// 暴走协程
    /// </summary>
    IEnumerator quickMoveCor;

    /// <summary>
    /// 磁铁持续时间
    /// </summary>
    float magnetDuration = 15;

    /// <summary>
    /// 磁铁剩余时间
    /// </summary>
    public float magnetTimeLeft;

    /// <summary>
    /// 磁铁协程
    /// </summary>
    IEnumerator magnetCor;

    /// <summary>
    /// 磁铁碰撞体
    /// </summary>
    public GameObject MagnetCollider;

    /// <summary>
    /// 跑鞋持续时间
    /// </summary>
    float shoeDuration = 10;

    /// <summary>
    /// 跑鞋剩余时间
    /// </summary>
    public float shoeTimeLeft;

    /// <summary>
    /// 跑鞋协程
    /// </summary>
    IEnumerator shoeCor;

    /// <summary>
    /// 积分加成剩余时间
    /// </summary>
    float multiplyDuration = 10;

    /// <summary>
    /// 积分加成剩余时间
    /// </summary>
    public float multiplyTimeLeft;

    /// <summary>
    /// 积分加成协程
    /// </summary>
    IEnumerator multiplyCor;

    /// <summary>
    /// 磁铁道具状态
    /// </summary>
    public Text Text_Magnet;

    /// <summary>
    /// 跑鞋道具状态
    /// </summary>
    public Text Text_Shoe;

    /// <summary>
    /// 暴走道具状态
    /// </summary>
    public Text Text_Star;

    /// <summary>
    /// 积分加成道具状态
    /// </summary>
    public Text Text_Multiply;

    //以下为正在奔跑和即将奔跑到的道路
    public GameObject road1;
    public GameObject road2;

    //以下为游戏开始内置的第一个和第二个道路
    public GameObject start1;
    public GameObject start2;

    /// <summary>
    /// 每过多远距离速度递增
    /// </summary>
    private float speedAddDistance = 300;

    /// <summary>
    /// 速度每次递增多少
    /// </summary>
    private float speedAddRate = 0.5f;

    /// <summary>
    /// 距离计数
    /// </summary>
    private float speedAddCount = 0;

    /// <summary>
    /// 动画组件
    /// </summary>
    private Animation animation;

    /// <summary>
    /// 全局实例
    /// </summary>
    public static PlayerController instance;

	// Use this for initialization
	void Start () {
        //设置全局实例
        instance = this;

        //初始速度
        speed = init_speed;

        //获取动画组件
        animation = GetComponent<Animation>();

        //获取人物控制器
        characterController = GetComponent<CharacterController>();

        //默认站在中间
        standPosition = Position.Middle;

        //开始游戏协程
        StartCoroutine(UpdateAction());
	}

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void Play()
    {   
        //没有暂停
        GameController.instance.isPause = false;

        //在游戏中
        GameController.instance.isPlay = true;

        //开始游戏协程
        StartCoroutine(UpdateAction());
    }

    /// <summary>
    /// 设置主角速度
    /// </summary>
    /// <param name="newSpeed"></param>
    private void SetSpeed(float newSpeed)
    {
        //速度不能大于最大速度
        if (newSpeed <= maxSpeed)
            speed = newSpeed;
        else
            speed = maxSpeed;
    }

    /// <summary>
    /// 更新主角位置协程
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateAction()
    {
        while (GameAttribute.instance.life > 0)     //生命值大于0
        {
            if (GameController.instance.isPlay &&   //在游戏中
                !GameController.instance.isPause)   //没有暂停
            {
                GetInputDirection();    //获取输入方向
                MoveLeftRight();    //左右移动
                MoveForward();      //往前以及上下移动
                UpdateSpeed();      //更新速度
            }
            else
            {
                animation.Stop();   //否则停止动画
            }
            yield return 0;
        }
        speed = 0;  //停下来
        GameController.instance.isPlay = false; //不在游戏中
        xDirection = Vector3.zero;  //重置x方向移动
        AnimationManager.instance.animationHandler = AnimationManager.instance.PlayDead;    //播放dead动画
        iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(0, 0, 2), 2.0f);  //撞击后往后倒退2米
        yield return new WaitForSeconds(3);     //等待3秒
        Debug.Log("restart");

        UIController.instance.ShowRestartUI();  //显示重启菜单
        UIController.instance.HidePauseUI();    //隐藏暂停菜单
    }

    /// <summary>
    /// 更新用户速度
    /// </summary>
    private void UpdateSpeed()
    {
        //累加距离
        speedAddCount += speed * Time.deltaTime;

        //距离达到了需要更新的值
        if (speedAddCount >= speedAddDistance)
        {
            //设置新的速度
            SetSpeed(speed + speedAddRate);

            //重置距离计数器
            speedAddCount = 0;
        }
    }

    /// <summary>
    /// 往前以及上下移动
    /// </summary>
    void MoveForward()
    {
        if(inputDirection == InputDirection.Down)   //向下滑动
        {
            //播放roll动画
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRoll;
        }
        if(characterController.isGrounded)  //主角在地面上
        {
            moveDirection = Vector3.zero;   //重置x方向输入

            //如果当前不在播放Roll、TurnLeft、TurnRight动画
            if(AnimationManager.instance.animationHandler != AnimationManager.instance.PlayRoll &&
                AnimationManager.instance.animationHandler != AnimationManager.instance.PlayTurnLeft &&
                AnimationManager.instance.animationHandler != AnimationManager.instance.PlayTurnRight)
            {
                //那么就要播放run动画
                AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRun;
            }
            if(inputDirection == InputDirection.Up) //向上滑动
            {
                JumpUp();   //跳跃
                if (canDoubleJump)      //能双连跳
                    doubleJump = true;  //进入双连跳状态
            }
        }
        else
        {
            if(inputDirection == InputDirection.Down)   //向下滑动
            {
                QuickGround();  //快速落地
            }
            if(inputDirection == InputDirection.Up) //向上滑动
            {
                if(doubleJump)  //能双连跳
                {
                    JumpDouble();   //双连跳
                    doubleJump = false;     //退出双连跳状态
                }
            }

            //如果当前不在播放JumpUp、Roll、DoubleJump动画
            if(AnimationManager.instance.animationHandler != AnimationManager.instance.PlayJumpUp
                && AnimationManager.instance.animationHandler != AnimationManager.instance.PlayRoll
                && AnimationManager.instance.animationHandler != AnimationManager.instance.PlayDoubleJump)
            {
                //则播放JumpLoop动画
                AnimationManager.instance.animationHandler = AnimationManager.instance.PlayJumpLoop;
            }
        }

        //z方向为当前速度
        moveDirection.z = speed;

        //y方向需要计算重力
        moveDirection.y -= gravity * Time.deltaTime;

        //移动主角
        characterController.Move((xDirection * 10 + moveDirection) * Time.deltaTime);
    }

    /// <summary>
    /// 重置主角控制器
    /// </summary>
    public void Reset()
    {
        //设置初始速度
        speed = init_speed;

        //重置手势滑动方向
        inputDirection = InputDirection.NULL;

        //不在输入
        activeInput = false;

        //默认站在中间
        standPosition = Position.Middle;

        //x方向输入重置
        xDirection = Vector3.zero;

        //重置
        moveDirection = Vector3.zero;

        //不在滚动
        isRoll = false;

        //不能双连跳
        canDoubleJump = false;

        //不在暴走
        isQuickMoving = false;

        //道具剩余时间都为0
        quickMoveTimeLeft = 0;
        magnetTimeLeft = 0;
        shoeTimeLeft = 0;
        multiplyTimeLeft = 0;

        //重置距离计数
        speedAddCount = 0;

        //重置主角位置
        gameObject.transform.position = new Vector3(0, 0, -64);

        //重置摄像机位置
        Camera.main.transform.position = new Vector3(0, 5, -67);

        //播放run动画
        AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRun;

        //重新生成道路
        var newRoad1 = Respawn("road1", road1, new Vector3(0, 0, 0));
        var newRoad2 = Respawn("road2", road2, new Vector3(0, 0, 32));

        //重新生成起始道路
        Respawn("start1", start1, new Vector3(0, 0, -32));
        Respawn("start2", start2, new Vector3(0, 0, -64));

        //重新设置道具设置器中的对应道路
        FloorSetter.instance.floorOnRunning = newRoad1;
        FloorSetter.instance.floorForward = newRoad2;
    }

    /// <summary>
    /// 重新生成对象
    /// </summary>
    /// <param name="name">对象名称</param>
    /// <param name="prefab">预设</param>
    /// <param name="location">生成位置</param>
    /// <returns>新对象</returns>
    private GameObject Respawn(string name,GameObject prefab,Vector3 location)
    {
        //根据名称查找
        var old = GameObject.Find(name);
        if (old != null)
        {
            //销毁原有对象
            Destroy(old);

            //生成新对象
            var newObj = Instantiate(prefab);

            //设置新对象名称
            newObj.name = name;

            //设置新对象位置
            newObj.transform.localPosition = location;

            //返回新对象
            return newObj;
        }
        return null;
    }

    /// <summary>
    /// 快速落地
    /// </summary>
    void QuickGround()
    {
        //将y值减去一个较大值
        moveDirection.y -= jumpValue * 3;
    }

    /// <summary>
    /// 双连跳
    /// </summary>
    void JumpDouble()
    {
        //播放DoubleJump动画
        AnimationManager.instance.animationHandler = AnimationManager.instance.PlayDoubleJump;

        //y值增加，比普通的跳跃稍微高点
        moveDirection.y += jumpValue * 1.3f;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    void JumpUp()
    {
        //播放JumpUp动画
        AnimationManager.instance.animationHandler = AnimationManager.instance.PlayJumpUp;

        //y值增加
        moveDirection.y += jumpValue;
    }

    /// <summary>
    /// 往左移动
    /// </summary>
    void MoveLeft()
    {
        if (standPosition != Position.Left) //当前位置不在最左侧
        {
            GetComponent<Animation>().Stop();   //停止动画
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnLeft;    //播放TurnLeft动画

            xDirection = Vector3.left;  //设置x方向移动为左侧

            if(standPosition ==  Position.Middle)   //从中间移动过来
            {
                standPosition = Position.Left;  //新的站立位置为最左侧
                fromPosition = Position.Middle; //从中间移动过来
            }
            else if(standPosition == Position.Right)    //从右侧移动过来
            {
                standPosition = Position.Middle;    //新的站立位置为中间
                fromPosition = Position.Right;  //从右侧移动过来
            }
        }
    }

    /// <summary>
    /// 往右移动
    /// </summary>
    void MoveRight()
    {
        if (standPosition != Position.Right)    //当前位置不在最右侧
        {
            GetComponent<Animation>().Stop();   //停止动画
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnRight;   //播放TurnRight动画

            xDirection = Vector3.right; //设置x方向移动为右侧

            if (standPosition == Position.Middle)   //从中间移动过来
            {
                standPosition = Position.Right;     //新的站立位置为左右侧
                fromPosition = Position.Middle;     //从中间移动过来
            }
            else if (standPosition == Position.Left)    //从左侧移动过来
            {
                standPosition = Position.Middle;    //新的站立位置为中间
                fromPosition = Position.Left;       //从左侧移动过来
            }
        }
    }

    /// <summary>
    /// 左右移动
    /// </summary>
    void MoveLeftRight()
    {
        if (inputDirection == InputDirection.Left)  //左侧移动
        {
            MoveLeft();
        }
        else if(inputDirection == InputDirection.Right) //右侧移动
        {
            MoveRight();
        }

        if(standPosition ==  Position.Left) //移到左侧
        {
            if (transform.position.x <= -1.7f)  //主角已经到达最左边的位置
            {
                xDirection = Vector3.zero;  //不能再往左移动了
                transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);    //更新主角位置
            }
        }
        if(standPosition == Position.Middle)    //主角站在中间
        {
            if (fromPosition == Position.Left)  //从左侧移动过来
            {
                if (transform.position.x > 0)   //主角的x值已经开始大于0
                {
                    xDirection = Vector3.zero;      //不能再往右移动了
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);    //更新主角位置
                }
            }
            else if(fromPosition == Position.Right) //从右侧移动过来
            {
                if (transform.position.x < 0)   //主角的x值已经开始小于0了
                {
                    xDirection = Vector3.zero;      //不能再往左移动了
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);    //更新主角位置
                }
            }
        }

        if(standPosition == Position.Right)     //移到右侧
        {
            if (transform.position.x >= 1.7f)   //主角已经到达最右边的位置
            {
                xDirection = Vector3.zero;      //不能再往右移动了
                transform.position = new Vector3(1.7f, transform.position.y, transform.position.z);     //更新主角位置
            }
        }
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    void PlayAnimation()
    {
        if (inputDirection == InputDirection.Left)  //向左滑动
        {
            //播放TurnLeft
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnLeft;
        }
        else if (inputDirection == InputDirection.Right)    //向右滑动
        {
            //播放TurnRight
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnRight;
        }
        else if (inputDirection == InputDirection.Up)   //向上滑动
        {
            //播放JumpUp
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayJumpUp;
        }
        else if (inputDirection == InputDirection.Down) //向下滑动
        {
            //播放Roll
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRoll;
        }
    }

    /// <summary>
    /// 暴走
    /// </summary>
    public void QuickMove()
    {
        //如果协程已经创建，则停止
        if (quickMoveCor != null)
            StopCoroutine(quickMoveCor);

        //初始化协程
        quickMoveCor = QuickMoveCoroutine();

        //启动协程
        StartCoroutine(quickMoveCor);
    }

    /// <summary>
    /// 使用磁铁
    /// </summary>
    public void UseMagnet()
    {
        //如果协程已经创建，则停止
        if (magnetCor != null)
            StopCoroutine(magnetCor);

        //初始化协程
        magnetCor = MagnetCoroutine();

        //启动协程
        StartCoroutine(magnetCor);
    }

    /// <summary>
    /// 使用跑鞋
    /// </summary>
    public void UseShoe()
    {
        //如果协程已经创建，则停止
        if (shoeCor != null)
            StopCoroutine(shoeCor);

        //初始化协程
        shoeCor = ShoeCoroutine();

        //启动协程
        StartCoroutine(shoeCor);
    }

    /// <summary>
    /// 双倍积分
    /// </summary>
    public void Multiply()
    {
        //如果协程已经创建，则停止
        if (multiplyCor != null)
            StopCoroutine(multiplyCor);

        //初始化协程
        multiplyCor = MultiplyCoroutine();

        //启动协程
        StartCoroutine(multiplyCor);
    }

    /// <summary>
    /// 是否可以更新道具状态
    /// </summary>
    /// <returns></returns>
    private bool CanPlay()
    {
        return !GameController.instance.isPause &&  //没有暂停
            GameController.instance.isPlay;     //在游戏中
    }

    /// <summary>
    /// 积分加成协程
    /// </summary>
    /// <returns></returns>
    IEnumerator MultiplyCoroutine()
    {
        //初始剩余时间
        multiplyTimeLeft = multiplyDuration;

        //更新游戏配置中的积分基数
        GameAttribute.instance.multiply = 2;

        //倒计时
        while(multiplyTimeLeft>=0)
        {
            if (CanPlay())
                multiplyTimeLeft -= Time.deltaTime; //时间递减
            yield return null;
        }

        //恢复游戏配置中的积分基数
        GameAttribute.instance.multiply = 1;
    }

    /// <summary>
    /// 跑鞋协程
    /// </summary>
    /// <returns></returns>
    IEnumerator ShoeCoroutine()
    {
        //初始剩余时间
        shoeTimeLeft = shoeDuration;

        //设置能双连跳
        PlayerController.instance.canDoubleJump = true;

        while (shoeTimeLeft >= 0)   //倒计时
        {
            if (CanPlay())
                shoeTimeLeft -= Time.deltaTime; //时间递减
            yield return null;
        }

        //取消双连跳
        PlayerController.instance.canDoubleJump = false;
    }

    /// <summary>
    /// 磁铁协程
    /// </summary>
    /// <returns></returns>
    IEnumerator MagnetCoroutine()
    {
        //初始剩余时间
        magnetTimeLeft = magnetDuration;

        //开启磁铁碰撞器
        MagnetCollider.SetActive(true);

        while (magnetTimeLeft >= 0) //倒计时
        {
            if (CanPlay())
                magnetTimeLeft -= Time.deltaTime;   //时间递减
            yield return null;
        }

        //关闭磁铁碰撞器
        MagnetCollider.SetActive(false);
    }

    /// <summary>
    /// 暴走协程
    /// </summary>
    /// <returns></returns>
    IEnumerator QuickMoveCoroutine()
    {
        //初始剩余时间
        quickMoveTimeLeft = quickMoveDuration;

        //如果不在暴走
        if(!isQuickMoving)
            saveSpeed = speed;  //保存暴走之前的速度
        speed = 20;     //设置暴走速度
        isQuickMoving = true;   //正在暴走
        while (quickMoveTimeLeft>=0)    //倒计时
        {
            if (CanPlay())
                quickMoveTimeLeft -= Time.deltaTime;    //时间递减
            yield return null;
        }
        speed = saveSpeed;  //恢复暴走前的速度
        isQuickMoving = false;  //不在暴走
    }

    /// <summary>
    /// 获取输入方向
    /// </summary>
    void GetInputDirection()
    {
        //默认为空
        inputDirection = InputDirection.NULL;
        if(Input.GetMouseButtonDown(0)) //鼠标按下
        {
            activeInput = true; //开始输入
            mousePos = Input.mousePosition;     //记录按下的位置
        }
        if(Input.GetMouseButton(0) && activeInput)  //鼠标没有松开，并且曾经按下过
        {
            Vector3 vec = Input.mousePosition - mousePos;   //滑动方向
            if (vec.magnitude > 20)     //距离大于20才处理
            {
                //滑动方向与+y轴夹角
                var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.up)) * Mathf.Rad2Deg;

                //滑动方向与+x轴夹角
                var anglex = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.right)) * Mathf.Rad2Deg;

                if (angleY <= 45)   //y夹角小于45度，为向上滑动
                {
                    inputDirection = InputDirection.Up;
                    AudioManager.instance.PlaySlideAudio(); //播放滑动音效
                }
                else if(angleY >=135)   //y夹角大于135度，为向下滑动
                {
                    inputDirection = InputDirection.Down;
                    AudioManager.instance.PlaySlideAudio(); //播放滑动音效
                }
                else if (anglex <= 45)  //x夹角小于45度，为向右移动
                {
                    inputDirection = InputDirection.Right;
                    AudioManager.instance.PlaySlideAudio(); //播放滑动音效
                }
                else if(anglex>=135)    //x夹角大于135度，为向左移动
                {
                    inputDirection = InputDirection.Left;
                    AudioManager.instance.PlaySlideAudio(); //播放滑动音效
                }

                //输入结束
                activeInput = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        //更新技能剩余时间
        UpdateItemTime();

	}

    /// <summary>
    /// 更新技能剩余时间
    /// </summary>
    private void UpdateItemTime()
    {
        //以下设置各个技能的剩余时间
        Text_Magnet.text = GetTime(magnetTimeLeft);
        Text_Multiply.text = GetTime(multiplyTimeLeft);
        Text_Shoe.text = GetTime(shoeTimeLeft);
        Text_Star.text = GetTime(quickMoveTimeLeft);
    }

    /// <summary>
    /// 获取剩余时间的文本显示
    /// </summary>
    /// <param name="time">时间</param>
    /// <returns></returns>
    private string GetTime(float time)
    {
        //小于等于0，返回空
        if (time <= 0)
            return "";

        //获取文本显示
        return ((int)time + 1).ToString()+"s";
    }
}

/// <summary>
/// 输入方向
/// </summary>
public enum InputDirection
{
    /// <summary>
    /// 空
    /// </summary>
    NULL,

    /// <summary>
    /// 向左
    /// </summary>
    Left,

    /// <summary>
    /// 向右
    /// </summary>
    Right,

    /// <summary>
    /// 向上
    /// </summary>
    Up,

    /// <summary>
    /// 向下
    /// </summary>
    Down
}

/// <summary>
/// 位置
/// </summary>
public enum Position
{
    /// <summary>
    /// 左侧
    /// </summary>
    Left,

    /// <summary>
    /// 中间
    /// </summary>
    Middle,

    /// <summary>
    /// 右侧
    /// </summary>
    Right
}