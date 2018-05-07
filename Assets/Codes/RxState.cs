using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RxState : MonoBehaviour
{
  public int coin;
  public static RxState single;
  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    coin = 0;
    single = this;
  }
}