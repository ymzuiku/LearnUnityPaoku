using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 场景管理
/// </summary>
public class PatternManager : MonoBehaviour {

    /// <summary>
    /// 全局实例
    /// </summary>
    public static PatternManager instance;

    /// <summary>
    /// 场景集合
    /// </summary>
    public List<Pattern> Patterns = new List<Pattern>();

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

/// <summary>
/// 场景
/// </summary>
[System.Serializable]
public class Pattern
{
    /// <summary>
    /// 场景物体集合
    /// </summary>
    public List<PatternItem> PatternItems = new List<PatternItem>();
}

/// <summary>
/// 场景物体
/// </summary>
[System.Serializable]
public class PatternItem
{
    /// <summary>
    /// 物体
    /// </summary>
    public GameObject gameobject;

    /// <summary>
    /// 位置
    /// </summary>
    public Vector3 position;
}