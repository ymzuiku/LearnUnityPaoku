using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2 : Food
{
  public float moveSpeed = 10f;
  GameObject moveTheTarget;
  public void MoveToTarget(GameObject target)
  {
    moveTheTarget = target;
  }
  public override void OnTriggerEnter(Collider other)
  {
    base.OnTriggerEnter(other);
    if (other.tag == "Player")
    {
      RxState.single.coin += 1;
    }
  }
  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    if (moveTheTarget)
    {
      transform.position = Vector3.Lerp(transform.position, moveTheTarget.transform.position, Time.deltaTime * moveSpeed);
    }
  }
}
