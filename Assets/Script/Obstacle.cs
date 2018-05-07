using UnityEngine;
using System.Collections;

/// <summary>
/// 障碍物基类
/// </summary>
public class Obstacle : MonoBehaviour {

    /// <summary>
    /// 伤害值
    /// </summary>
    public int hurtValue = 1;

    /// <summary>
    /// 移动速度
    /// </summary>
    public int moveSpeed = 0;

    /// <summary>
    /// 移动方向
    /// </summary>
    public Vector3 moveDirection = Vector3.back;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //移动
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
	}

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")  //撞击到主角
        {
            AudioManager.instance.PlayHitAudio();   //播放音效
            CameraManager.instance.CameraShake();   //相机抖动
            GameAttribute.instance.life -= hurtValue;   //生命值更新
        }
        if (other.tag != "Road" &&  //撞击到的不是道路
            other.tag != "MagnetCollider")  //不是磁铁碰撞器
        {
            moveSpeed = 0;      //停下来
        }
    }
}