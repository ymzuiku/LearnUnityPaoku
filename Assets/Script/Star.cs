using UnityEngine;
using System.Collections;

/// <summary>
/// 暴走
/// </summary>
public class Star : Item {
    public override void OnTriggerEnter(Collider other)
    {
        //基类处理
        base.OnTriggerEnter(other);
        if (other.tag == "Player")  //撞击到主角
        {
            PlayerController.instance.QuickMove();  //进入暴走
        }
    }
}