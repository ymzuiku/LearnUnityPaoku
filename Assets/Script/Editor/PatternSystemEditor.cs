using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// 场景系统编辑器
/// </summary>
public class PatternSystemEditor : EditorWindow {

    [MenuItem("Window/AddPatternToSystem")]
    static void AddPatternToSystem()
    {
        //查找GameManager
        var gameManager = GameObject.Find("GameManager");
        if(gameManager !=null)
        {
            //获取其挂载的PatternManager
            var patternManager = gameManager.GetComponent<PatternManager>();

            if (Selection.gameObjects.Length == 1)  //当前选中了gameobject
            {
                //查找选中物体下的Item
                var item =  Selection.gameObjects[0].transform.Find("Item");
                if (item != null)
                {
                    //生成一种新的场景
                    Pattern pattern = new Pattern();
                    foreach (var child in item)     //遍历该Item下所有的物体
                    {
                        Transform childTransform = child as Transform;
                        if (childTransform != null)
                        {
                            //根据gameobject获取对应的prefeb
                            var prefeb = UnityEditor.PrefabUtility.GetPrefabParent(childTransform.gameObject);
                            if (prefeb != null)
                            {
                                //生成新的场景物体
                                PatternItem patternItem = new PatternItem
                                {
                                    //设置gameobject
                                    gameobject = prefeb as GameObject,

                                    //设置位置
                                    position = childTransform.transform.localPosition
                                };

                                //物体添加到场景中
                                pattern.PatternItems.Add(patternItem);
                            }
                        }
                    }

                    //场景添加到场景库中
                    patternManager.Patterns.Add(pattern);
                }
            }
        }
    }
}