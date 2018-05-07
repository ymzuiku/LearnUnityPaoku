using UnityEngine;
using System.Collections;

/// <summary>
/// 磁铁
/// </summary>
public class Magnet : Item {
    public override void OnTriggerEnter(Collider other)
    {
        //基类处理
        base.OnTriggerEnter(other);
        if (other.tag == "Player")  //撞击到主角
        {
            //使用磁铁
            PlayerController.instance.UseMagnet();
        }
    }
}