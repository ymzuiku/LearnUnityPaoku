using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory2 : MonoBehaviour
{
  public float destoryTime = 0.7f;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    Destroy(gameObject, destoryTime);
  }
}