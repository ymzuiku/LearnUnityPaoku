using UnityEngine;
using System.Collections;

/// <summary>
/// 摄像机管理
/// </summary>
public class CameraManager : MonoBehaviour {

    /// <summary>
    /// 跟随目标
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 摄像机与主角的高度差
    /// </summary>
    public float height;

    /// <summary>
    /// 摄像机与主角的距离
    /// </summary>
    public float distance;

    /// <summary>
    /// 摄像机位置临时变量
    /// </summary>
    Vector3 pos;

    /// <summary>
    /// 是否在抖动
    /// </summary>
    bool isShaking = false;

    /// <summary>
    /// 全局实例
    /// </summary>
    public static CameraManager instance;

	// Use this for initialization
	void Start () {
        //设置全局实例
        instance = this;

        //设置pos初始值
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// 相机抖动
    /// </summary>
    public void CameraShake()
    {
        if (!isShaking)     //如果当前不在抖动
            StartCoroutine(ShakeCoroutine());   //启动抖动协程
    }

    /// <summary>
    /// 抖动协程
    /// </summary>
    /// <returns></returns>
    IEnumerator ShakeCoroutine()
    {
        isShaking = true;   //设置为正在抖动
        float time = 0.5f;  //抖动总时间
        while (time > 0)    //进入倒计时
        {
            //设置摄像机的位置
            transform.position = new Vector3(
                target.transform.position.x + Random.Range(-0.1f, 0.1f),    //X值设置为当前的X值加上一个随机的值
                target.transform.position.y + height, 
                target.transform.position.z - distance);
            time -= Time.deltaTime;     //倒计时
            yield return null;  //返回
        }   
        isShaking = false;      //抖动已经完成
    }

    void LateUpdate()
    {
        if (!isShaking  //当前不在抖动
            && GameController.instance.isPlay   //正在播放
            && !GameController.instance.isPause)    //没有暂停
        {
            pos.x = target.transform.position.x;    //x与主角的x相同

            //y=主角的y+height，使用Lerp实现缓冲效果
            pos.y = Mathf.Lerp(pos.y, target.transform.position.y + height, Time.deltaTime * 5);

            //z=主角的z-distance
            pos.z = target.transform.position.z - distance;

            transform.position = pos;   //设置摄像机位置，实现跟随效果
        }
    }
}