using UnityEngine;
using System.Collections;

/// <summary>
/// 跑鞋
/// </summary>
public class Shoe : Item {
    public override void OnTriggerEnter(Collider other)
    {
        //基类处理
        base.OnTriggerEnter(other);
        if (other.tag == "Player")  //撞击到主角
        {
            PlayerController.instance.UseShoe();        //使用跑鞋
        }
    }
}