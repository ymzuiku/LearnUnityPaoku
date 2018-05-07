using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  public float rotateSpeed = 1;
  public GameObject hitEffect;

  void Start()
  {

  }

  void Update()
  {
    transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
  }

  /// <summary>
  /// OnTriggerEnter is called when the Collider other enters the trigger.
  /// </summary>
  /// <param name="other">The other Collider involved in this collision.</param>
  public virtual void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      if (hitEffect != null)
      {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.parent = PlayerController2.single.gameObject.transform;
        effect.transform.localPosition = new Vector3(0, 0.5f, 0);
      }

      Destroy(gameObject);
    }
  }
}