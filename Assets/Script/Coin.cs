using UnityEngine;
using System.Collections;

/// <summary>
/// 金币
/// </summary>
public class Coin : Item {

    /// <summary>
    /// 撞击到物体
    /// </summary>
    public override void HitItem()
    {
        //基类处理
        base.HitItem();

        //金币+1
        GameAttribute.instance.AddCoin(1);
    }

    /// <summary>
    /// 播放撞击音效
    /// </summary>
    public override void PlayHitAudio()
    {
        AudioManager.instance.PlayCoinAudio();
    }
}