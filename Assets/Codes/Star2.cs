using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Star2 : Food
{
  public override void OnTriggerEnter(Collider other)
  {
    base.OnTriggerEnter(other);
    if (other.tag == "Player")
    {
      PlayerController2.single.QuickMove();
    }
  }
}