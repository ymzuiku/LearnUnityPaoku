using UnityEngine;
using System.Collections;

/// <summary>
/// 积分加成
/// </summary>
public class Multiply : Item
{
    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerEnter(Collider other)
    {
        //基类处理
        base.OnTriggerEnter(other);
        if (other.tag == "Player")      //碰撞到主角
        {
            PlayerController.instance.Multiply();   //进入积分加成
        }
    }
}