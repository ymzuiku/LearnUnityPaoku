using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmerController : MonoBehaviour
{

  public GameObject target;
  public float positionX = 0f;
  public float positionY = 0f;
  public float positionZ = 0f;
  public float lerpSpeedX = 2f;
  public float lerpSpeedY = 2f;
  public float lerpSpeedZ = 2f;

  Vector3 pos;

  // Use this for initialization
  void Start()
  {
    pos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {

  }
  /// <summary>
  /// LateUpdate is called every frame, if the Behaviour is enabled.
  /// It is called after all Update functions have been called.
  /// </summary>
  void LateUpdate()
  {
    Vector3 p = target.transform.position;
    pos.x = Mathf.Lerp(pos.x, p.x + positionX, Time.deltaTime * lerpSpeedX);
    pos.y = Mathf.Lerp(pos.y, p.y + positionY, Time.deltaTime * lerpSpeedY);
    pos.z = Mathf.Lerp(pos.z, p.z + positionZ, Time.deltaTime * lerpSpeedZ);
    transform.position = pos;
  }
}
