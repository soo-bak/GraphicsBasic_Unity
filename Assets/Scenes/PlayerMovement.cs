using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 5f;
  public float turnSpeed = 500f;  // 추가된 변수: 회전 속도

  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
    transform.Translate(movement * speed * Time.deltaTime, Space.World);

    RotatePlayerToMouse();  // 플레이어를 마우스 방향으로 회전
  }

  // 플레이어가 마우스 커서를 향하도록 회전하는 함수
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
