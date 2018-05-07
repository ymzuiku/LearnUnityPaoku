using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 声效管理
/// </summary>
public class AudioManager : MonoBehaviour {

    //以下为引用的音频
    public AudioClip button;
    public AudioClip coin;
    public AudioClip getitem;
    public AudioClip hit;
    public AudioClip slide;

    /// <summary>
    /// 全局实例
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// 音效打开Sprite
    /// </summary>
    public Sprite soundOnSprite;

    /// <summary>
    /// 音效关闭Sprite
    /// </summary>
    public Sprite soundOffSprite;

    /// <summary>
    /// 用于显示音频图标的Image
    /// </summary>
    public Image soundImage;

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">音频</param>
    private void PlayAudio(AudioClip clip)
    {
        if (GameAttribute.instance.soundOn)      //如果声效打开
            AudioSource.PlayClipAtPoint(clip, PlayerController.instance.transform.position);    //在主角所在的位置，播放对应的音频
    }

    /// <summary>
    /// 切换音效开关
    /// </summary>
    public void SwichSound()
    {
        //取反
        GameAttribute.instance.soundOn = !GameAttribute.instance.soundOn;

        //设置Image对应的Sprite
        soundImage.sprite = GameAttribute.instance.soundOn ? soundOnSprite : soundOffSprite;
    }

    /// <summary>
    /// 播放Button音效
    /// </summary>
    public void PlayButtonAudio()
    {
        PlayAudio(button);
    }

    /// <summary>
    /// 播放Coin音效
    /// </summary>
    public void PlayCoinAudio()
    {
        PlayAudio(coin);
    }

    /// <summary>
    /// 播放GetItem音效
    /// </summary>
    public void PlayGetItemAudio()
    {
        PlayAudio(getitem);
    }

    /// <summary>
    /// 播放Hit音效
    /// </summary>
    public void PlayHitAudio()
    {
        PlayAudio(hit);
    }

    /// <summary>
    /// 播放Slide音效
    /// </summary>
    public void PlaySlideAudio()
    {
        PlayAudio(slide);
    }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}