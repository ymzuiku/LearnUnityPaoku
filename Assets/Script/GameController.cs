using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏控制器
/// </summary>
public class GameController : MonoBehaviour {

    /// <summary>
    /// 是否暂停
    /// </summary>
    public bool isPause;

    /// <summary>
    /// 是否在游戏中
    /// </summary>
    public bool isPlay;

    /// <summary>
    /// 全局实例
    /// </summary>
    public static GameController instance;

	// Use this for initialization
	void Start () {

        //设置全局实例
        instance = this;

        //暂停
        isPause = true;

        //在游戏中
        isPlay = true;
	}

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void Play()
    {
        //没有暂停
        isPause = false;
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void Pause()
    {
        isPause = true;
    }

    /// <summary>
    /// 恢复游戏
    /// </summary>
    public void Resume()
    {
        isPause = false;
    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    public void Restart()
    {
        //重置游戏配置
        GameAttribute.instance.Reset();

        //重置主角控制器
        PlayerController.instance.Reset();

        //重新开始游戏
        PlayerController.instance.Play();
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR    //在unity editor中
        UnityEditor.EditorApplication.isPlaying = false;    //退出播放
#else
        Application.Quit();     //退出游戏
#endif
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}