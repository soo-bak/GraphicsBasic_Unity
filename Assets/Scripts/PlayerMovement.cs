using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 5f;
  public float turnSpeed = 500f;

  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
    transform.Translate(movement * speed * Time.deltaTime, Space.World);

    RotatePlayerToMouse();
  }

  void RotatePlayerToMouse() {
    Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    float rayLength;

    if (groundPlane.Raycast(cameraRay, out rayLength)) {
      Vector3 pointToLook = cameraRay.GetPoint(rayLength);
      Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
      transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
    }
  }
}
