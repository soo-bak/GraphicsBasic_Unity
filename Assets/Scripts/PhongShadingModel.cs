using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhongShadingModel : MonoBehaviour
{
  public Light lightSource;

  public Color objectColor = Color.white;

  [Range(0, 1)]
  public float ambientIntensity = 1f;

  [Range(0, 1)]
  public float diffuseIntensity = 0f;

  [Range(0, 1)]
  public float specularIntensity = 0f;

  public float shininess = 10f;

  public Slider redSlider;

  public Slider greenSlider;

  public Slider blueSlider;

  public Slider ambientSlider;

  public Slider diffuseSlider;

  public Slider specularSlider;

  private Mesh mesh;

  private Vector3[] normals;

  private Color[] colors;

  private void Start() {
    mesh = GetComponent<MeshFilter>().mesh;
    normals = mesh.normals;
    colors = new Color[normals.Length];

    ambientSlider.onValueChanged.AddListener(UpdateAmbientIntensity);
    diffuseSlider.onValueChanged.AddListener(UpdateDiffuseIntensity);
    specularSlider.onValueChanged.AddListener(UpdateSpecularIntensity);
    redSlider.onValueChanged.AddListener(UpdateRedColor);
    greenSlider.onValueChanged.AddListener(UpdateGreenColor);
    blueSlider.onValueChanged.AddListener(UpdateBlueColor);

    ambientSlider.value = ambientIntensity;
    diffuseSlider.value = diffuseIntensity;
    specularSlider.value = specularIntensity;
    redSlider.value = objectColor.r;
    greenSlider.value = objectColor.g;
    blueSlider.value = objectColor.b;
  }

  private void Update() {
    CalculatePhongShading();
    mesh.colors = colors;
  }

  private void CalculatePhongShading() {
    for (int i = 0; i < normals.Length; i++) {
      Vector3 worldNormal = transform.TransformDirection(normals[i]);
      Vector3 toLight = (lightSource.transform.position - transform.position).normalized;
      Vector3 toCamera = (Camera.main.transform.position - transform.position).normalized;
      Vector3 reflected = Vector3.Reflect(-toLight, worldNormal);

      // Ambient
      Color ambient = objectColor * ambientIntensity;

      // Diffuse
      float diffuseFactor = Mathf.Max(Vector3.Dot(worldNormal, toLight), 0);
      Color diffuse = objectColor * lightSource.color * diffuseFactor * diffuseIntensity;

      // Specular
      float specFactor = Mathf.Pow(Mathf.Max(Vector3.Dot(reflected, toCamera), 0), shininess);
      Color specular = lightSource.color * specFactor * specularIntensity;

      colors[i] = new Color(
          Mathf.Clamp01(ambient.r + diffuse.r + specular.r),
          Mathf.Clamp01(ambient.g + diffuse.g + specular.g),
          Mathf.Clamp01(ambient.b + diffuse.b + specular.b)
      );
    }
  }

  private void UpdateAmbientIntensity(float value) {
    ambientIntensity = value;
  }

  private void UpdateDiffuseIntensity(float value) {
    diffuseIntensity = value;
  }

  private void UpdateSpecularIntensity(float value) {
    specularIntensity = value;
  }

  private void UpdateRedColor(float value) {
    objectColor.r = value;
  }

  private void UpdateGreenColor(float value) {
    objectColor.g = value;
  }

  private void UpdateBlueColor(float value) {
    objectColor.b = value;
  }
}
