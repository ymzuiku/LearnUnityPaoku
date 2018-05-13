using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
  public GameObject roadRuning;
  public GameObject roadForward;

  public float forward = 40f;

  // Use this for initialization
  private void Start()
  {
  }

  // Update is called once per frame
  private void Update()
  {
    if (transform.position.z > roadRuning.transform.position.z + forward / 2)
    {
      roadForward.transform.position = new Vector3(0, 0, roadRuning.transform.position.z + forward);
      GameObject temp = roadRuning;
      roadRuning = roadForward;
      roadForward = temp;
    }
  }
}