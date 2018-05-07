using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : Food
{
  public override void OnTriggerEnter(Collider other)
  {
    base.OnTriggerEnter(other);
    if (other.tag == "Player")
    {
      other.transform.Find("GetCoinController").GetComponent<MagnetCollider2>().SetCanGetCoin(true);
    }
  }
}