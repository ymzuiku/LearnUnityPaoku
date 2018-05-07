using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagnetCollider2 : MonoBehaviour
{
  public float getCoinTime = 3f;
  float nowGetCoinTime = 0f;
  bool canGetCoin = false;
  void Start()
  {

  }
  public void SetCanGetCoin(bool can){
    canGetCoin = can;
  }
  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Coin" && canGetCoin == true)
    {
      other.gameObject.GetComponent<Coin2>().MoveToTarget(gameObject);
    }
  }
  void Update()
  {
      if(canGetCoin == true){
        nowGetCoinTime += Time.deltaTime;
        if(nowGetCoinTime >= getCoinTime) {
          nowGetCoinTime = 0f;
          canGetCoin = false;
        }
      }
  }
}