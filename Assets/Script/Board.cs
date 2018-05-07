using UnityEngine;
using System.Collections;

/// <summary>
/// 栅栏
/// </summary>
public class Board : Obstacle {
    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="other">碰撞对方的collider</param>
    public override void OnTriggerEnter(Collider other)
    {
        //如果不在roll状态
        if(!PlayerController.instance.isRoll)
            base.OnTriggerEnter(other);     //让基类处理
    }
}