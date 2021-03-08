using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCameraController : MonoBehaviour
{

    public Camera m_OrthographicCamera;
    private float timer;
    private float time;
    public GameObject title;
    public GameObject directionalLight;
    public GameObject boys;
    public GameObject boysShadow;

    // Start is called before the first frame update
    void Start()
    {
        boys.SetActive(false);
        boysShadow.SetActive(false);
        timer = 0.0f; 
        time = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < time) {
            m_OrthographicCamera.transform.position += Vector3.up * 0.007f;
            m_OrthographicCamera.orthographicSize -= .008f;
            timer += Time.deltaTime;
            if (timer > 5f) {
                boys.SetActive(true);
                boysShadow.SetActive(true);
            }
        } else if (timer >= time) {
            StartCoroutine(FadeTitleAndLight(title.GetComponent<TextMeshPro>(), 2f));
            // StartCoroutine(RotateOverTime(pivotPoint.transform, pivotPointNewTransform.transform.rotation, 10f));
        }
    }

    IEnumerator FadeTitleAndLight(TextMeshPro renderer, float duration) {
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float oldIntensity = directionalLight.GetComponent<Light>().intensity;
        float newIntensity = .4f;
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, time/duration);
            directionalLight.GetComponent<Light>().intensity = Mathf.Lerp(oldIntensity, newIntensity, time/duration);
            yield return null;
        }
    }
}
