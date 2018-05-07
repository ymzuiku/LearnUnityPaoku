using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
  public Material material;
  public float speed = 1.0f;
  public float longTime = 0f;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    longTime += Time.deltaTime;
  }

  /// <summary>
  /// OnTriggerEnter is called when the Collider other enters the trigger.
  /// </summary>
  /// <param name="other">The other Collider involved in this collision.</param>
  void OnTriggerEnter(Collider other)
  {
    speed = 0f;
    Debug.Log(longTime);
  }

  public void BeginUpdateColor()
  {
		StartCoroutine(UpdateColor());
  }
	
  IEnumerator UpdateColor()
  {
    for (byte i = 0; i < 255; i++)
    {
      material.color = new Color32(i, i, i, 255);
			yield return 0;
    }
  }


}
