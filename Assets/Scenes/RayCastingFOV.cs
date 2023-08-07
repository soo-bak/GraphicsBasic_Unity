using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayCastingFOV : MonoBehaviour {

  public int numberOfRays = 1080;

  public float fov = 60f;

  public float rayDistance = 1f;

  public LayerMask wallMask;

  public Color visibleColor = Color.yellow;

  public Color nonVisibleColor = Color.black;

  private void Update() {
    foreach (Transform child in transform) {
      Destroy(child.gameObject);
    }

    for (int i = 0; i < numberOfRays; i++) {
      float angle = (i / (float)numberOfRays) * 360 - 180;
      Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

      Ray ray = new Ray(transform.position, rayDirection);
      RaycastHit hit;

      Color rayColor = nonVisibleColor;

      if (angle >= -fov / 2 && angle <= fov / 2)
        rayColor = visibleColor;


      if (Physics.Raycast(ray, out hit, rayDistance, wallMask)) {
        LineRenderer line = CreateLineRenderer(rayColor);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);
      } else {
        LineRenderer line = CreateLineRenderer(rayColor);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + rayDirection * rayDistance);
      }
    }
  }
  private LineRenderer CreateLineRenderer(Color color) {
    GameObject lineObject = new GameObject("Line");
    lineObject.transform.SetParent(transform);
    LineRenderer line = lineObject.AddComponent<LineRenderer>();
    line.material = new Material(Shader.Find("UI/Default"));
    line.startColor = color;
    line.endColor = color;
    line.startWidth = 0.01f;
    line.endWidth = 0.1f;
    line.positionCount = 2;
    return line;
  }
}

