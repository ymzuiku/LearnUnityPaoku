using UnityEngine;
using System.Collections;

/// <summary>
/// 障碍物与道具基类
/// </summary>
public class Item : MonoBehaviour {

    /// <summary>
    /// 转动速度
    /// </summary>
    public float rotateSpeed = 1;

    /// <summary>
    /// 撞击粒子特效
    /// </summary>
    public GameObject hitEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //转动
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// 撞击
    /// </summary>
    public virtual void HitItem()
    {
        //播放撞击音效
        PlayHitAudio();

        //生成撞击粒子特效
        GameObject effect = Instantiate(hitEffect);

        //设置parent为主角
        effect.transform.parent = PlayerController.instance.gameObject.transform;

        //设置局部位置
        effect.transform.localPosition = new Vector3(0, 0.5f, 0);

        //销毁所挂载的物体
        Destroy(gameObject);
    }

    /// <summary>
    /// 播放撞击音效
    /// </summary>
    public virtual void PlayHitAudio()
    {
        AudioManager.instance.PlayGetItemAudio();
    }

    /// <summary>
    /// 撞击
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnTriggerEnter(Collider other)
    {
        //撞击到主角
        if (other.tag == "Player")
        {
            HitItem();
        }
    }
}