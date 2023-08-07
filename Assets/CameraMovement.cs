using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public GameObject player;
  Vector3 cameraPosition;

  private void Start() {
    cameraPosition = new Vector3(0, 8, -10);
  }
  // Update is called once per frame
  void Update() {
      transform.position = player.transform.position + cameraPosition;
    }
}
