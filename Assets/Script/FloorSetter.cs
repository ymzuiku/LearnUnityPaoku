using UnityEngine;
using System.Collections;

/// <summary>
/// 道路设置器
/// </summary>
public class FloorSetter : MonoBehaviour {

    /// <summary>
    /// 当前主角正在上面奔跑的道路
    /// </summary>
    public GameObject floorOnRunning;

    /// <summary>
    /// 主角奔跑前面的道路
    /// </summary>
    public GameObject floorForward;

    /// <summary>
    /// 全局实例
    /// </summary>
    public static FloorSetter instance;

	// Use this for initialization
	void Start () {
        //设置全局实例
        instance = this;
	}
	
    /// <summary>
    /// 移除所有道具与障碍物
    /// </summary>
    /// <param name="floor">道路</param>
    void RemoveItem(GameObject floor)
    {
        //查找Item GameObject
        var item = floor.transform.Find("Item");
        if (item != null)
        {
            foreach (var child in item) //遍历子物体
            {
                Transform childTranform = child as Transform;
                if (childTranform != null)
                {
                    //销毁
                    Destroy(childTranform.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// 添加道具与障碍物
    /// </summary>
    /// <param name="floor">道路</param>
    void AddItem(GameObject floor)
    {
        //查找Item GameObject
        var item = floor.transform.Find("Item");
        if (item != null)
        {
            //获取场景管理实例
            var patternManager = PatternManager.instance;
            if (patternManager != null &&
                patternManager.Patterns != null &&
                patternManager.Patterns.Count > 0)  //场景库不为空
            {
                //随机获取场景库中的一种场景
                var pattern = patternManager.Patterns[Random.Range(0, patternManager.Patterns.Count)];
                if (pattern != null &&
                    pattern.PatternItems != null &&
                    pattern.PatternItems.Count > 0) //该场景不为空
                {
                    foreach (var patternItem in pattern.PatternItems)   //遍历该场景中的所有项
                    {
                        var newObj = Instantiate(patternItem.gameobject);   //生成新的gameobject
                        newObj.transform.parent = item;     //设置parent
                        newObj.transform.localPosition = patternItem.position;  //设置局部坐标
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (transform.position.z > floorOnRunning.transform.position.z + 32)    //主角已经跑到下一个floor上
        {
            //移除所有道具与障碍物
            RemoveItem(floorOnRunning);

            //添加道具与障碍物
            AddItem(floorOnRunning);

            //将当前正在奔跑的道路往后移动32
            floorOnRunning.transform.position = new Vector3(0, 0, floorForward.transform.position.z + 32);

            //以下交换正在奔跑的道路与将要奔跑的道路
            GameObject temp = floorOnRunning;
            floorOnRunning = floorForward;
            floorForward = temp;
        }
	}
}