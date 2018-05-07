using UnityEngine;
using System.Collections;

/// <summary>
/// 动画管理
/// </summary>
public class AnimationManager : MonoBehaviour {

    /// <summary>
    /// 动画播放委托
    /// </summary>
    public delegate void AnimationHandler();

    /// <summary>
    /// 当前挂载的动画组件
    /// </summary>
    Animation animation;

    /// <summary>
    /// 全局实例
    /// </summary>
    public static AnimationManager instance;

    //以下为主角动画
    public AnimationClip Dead;
    public AnimationClip JumpDown;
    public AnimationClip JumpLoop;
    public AnimationClip JumpUp;
    public AnimationClip Roll;
    public AnimationClip Run;
    public AnimationClip TurnLeft;
    public AnimationClip TurnRight;

    /// <summary>
    /// 动画播放委托实例
    /// </summary>
    public AnimationHandler animationHandler;

	// Use this for initialization
	void Start () {

        //设置全局实例
        instance = this;

        //默认播放Run动画
        animationHandler = PlayRun;

        //获取挂载的动画组件
        animation = GetComponent<Animation>();
	}
 
    /// <summary>
    /// 播放Dead动画
    /// </summary>
    public void PlayDead()
    {
        animation.Play(Dead.name);
    }

    /// <summary>
    /// 播放JumpDown动画
    /// </summary>
    public void PlayJumpDown()
    {
        animation.Play(JumpDown.name);
    }

    /// <summary>
    /// 播放JumpLoop动画
    /// </summary>
    public void PlayJumpLoop()
    {
        animation.Play(JumpLoop.name);
    }

    /// <summary>
    /// 播放JumpUp动画
    /// </summary>
    public void PlayJumpUp()
    {
        animation.Play(JumpUp.name);

        //如果动画播放到尾声，则自动切换到Run动画
        if (animation[JumpUp.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;
        }
    }

    /// <summary>
    /// 播放Roll动画
    /// </summary>
    public void PlayRoll()
    {
        animation.Play(Roll.name);

        //如果动画播放到尾声，则自动切换到Run动画
        if (animation[Roll.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;

            //当前不在roll状态
            PlayerController.instance.isRoll = false;
        }
        else
        {
            //当前正在roll状态
            PlayerController.instance.isRoll = true;
        }
    }

    /// <summary>
    /// 播放DoubleJump动画
    /// </summary>
    public void PlayDoubleJump()
    {
        animation.Play(Roll.name);

        //如果动画播放到尾声，则自动切换到JumpLoop动画，双连跳之后紧跟的动作是JumpLoop，就是类似白鹤亮翅那个动画
        if (animation[Roll.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayJumpLoop;
        }
    }

    /// <summary>
    /// 播放Run动画
    /// </summary>
    public void PlayRun()
    {
        animation.Play(Run.name);
    }

    /// <summary>
    /// 播放TurnLeft动画
    /// </summary>
    public void PlayTurnLeft()
    {
        animation.Play(TurnLeft.name);

        //如果动画播放到尾声，则自动切换到Run动画
        if(animation[TurnLeft.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;
        }
    }

    /// <summary>
    /// 播放TurnRight动画
    /// </summary>
    public void PlayTurnRight()
    {
        animation.Play(TurnRight.name);

        //如果动画播放到尾声，则自动切换到Run动画
        if (animation[TurnRight.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;
        }
    }
	
	// Update is called once per frame
	void Update () {

        //每一帧里面调用动画播放委托
        if(animationHandler !=null)
        {
            animationHandler();
        }
	}
}