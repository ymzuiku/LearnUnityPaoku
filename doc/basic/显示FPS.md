# 显示FPS

```cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
  public bool isShowFPS = true;
  private float updateInterval = 1f; //帧数刷新间隔
  private double lastInterval;
  private int frames = 0;
  private float currFPS;
  void Awake()
  {
    Application.targetFrameRate = 60;
  }

  void Start()
  {
    if (!isShowFPS)
    {
      return;
    }
    lastInterval = Time.realtimeSinceStartup;
    frames = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if (!isShowFPS)
    {
      return;
    }
    ++frames;
    float timeNow = Time.realtimeSinceStartup;
    if (timeNow > lastInterval + updateInterval)
    {
      currFPS = (float)(frames / (timeNow - lastInterval));
      frames = 0;
      lastInterval = timeNow;
    }
  }

  void OnGUI()
  {
    if (!isShowFPS)
    {
      return;
    }
    string str = "FPS:" + currFPS.ToString("f2");
    var style = new GUIStyle();
    style.fontSize = 24;
    style.normal.textColor = new Color(1, 1, 1);
    GUI.Label(new Rect(Screen.width - 160, Screen.height - 80, 100, 100), str, style);
  }
}

```