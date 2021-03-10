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
    public GameObject background;

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
            timer += Time.deltaTime;
            var newPos = Mathf.Lerp(-5.0f, -.5f, timer/time);
            var newScale = Mathf.Lerp(1.0f, .5f, timer/time);
            background.transform.localScale = new Vector3(newScale, newScale, 1.0f);
            background.transform.position = new Vector3(0f, newPos, 0f);
            if (timer > 5.5f) {
                boys.SetActive(true);
            }
        } else if (timer >= time) {
            StartCoroutine(FadeTitleAndLight(title.GetComponent<TextMeshPro>(), 2f));
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
