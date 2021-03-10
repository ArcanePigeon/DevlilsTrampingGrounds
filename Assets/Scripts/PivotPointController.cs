using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PivotPointController : MonoBehaviour
{
    public List<AnimationCurve> animationCurves;
    
    private bool spinning;    
    private float anglePerItem;    
    private int randomTime;
    private int itemNumber;
    public GameObject boys;
    public GameObject dustinTalking;
    public GameObject mikeTalking;
    public GameObject jimmyTalking;

    public GameObject world;
    public GameObject worldShadow;
    public GameObject worldNoTent;
    public GameObject worldNoTentShadow;
    public GameObject ball;
    public GameObject sphere;
    public GameObject dog;
    public Camera mainCamera;

    public CinemachineVirtualCamera boyCam;
    public CinemachineVirtualCamera overviewCam;
    public CinemachineVirtualCamera closerCam;

    public GameObject background;
    
    void Start(){
        anglePerItem = 90;
        // 1.97 represents the item number
        float maxAngle = 360 * 4 + (1.97f * anglePerItem);
        Coroutine introSpin = StartCoroutine(SpinTheWheel (20f, maxAngle));
        // switch to closer view
        closerCam.Priority = 4;
    }
    
    void  Update ()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            boys.SetActive(false);
            dustinTalking.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.M)) {
            boys.SetActive(false);
            mikeTalking.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.J)) {
            boys.SetActive(false);
            jimmyTalking.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            boys.SetActive(true);
            dustinTalking.SetActive(false);
        } else if (Input.GetKeyUp(KeyCode.M)) {
            boys.SetActive(true);
            mikeTalking.SetActive(false);
        } else if (Input.GetKeyUp(KeyCode.J)) {
            boys.SetActive(true);
            jimmyTalking.SetActive(false);
        }
    }
    
    IEnumerator SpinTheWheel (float time, float maxAngle)
    {
        float timer = 0.0f;        
        float startAngle = transform.eulerAngles.z;        
        maxAngle = maxAngle - startAngle;
        
        int animationCurveNumber = 0;
        
        while (timer < time / 2) {
            float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
            transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
        StartCoroutine(SwitchToBoyView(10f));
    } 

    IEnumerator SwitchToBoyView (float time)
    {
        boyCam.Priority = 5;
        // float timer = 0.0f;
        // while (timer < time) {
        //     timer += Time.deltaTime;
        //     var newScale = Mathf.Lerp(.5f, .25f, timer/time);
        //     background.transform.localScale = new Vector3(newScale, newScale, 1.0f);
        // }

        yield return new WaitForSeconds(10f);

        closerCam.Priority = 6;
        StartCoroutine(QuarterSpin (10f));
    }


    IEnumerator QuarterSpin (float time)
    {
        float maxAngle = 150;
        float timer = 0.0f;        
        float startAngle = transform.eulerAngles.z;        
        maxAngle = maxAngle - startAngle;
        
        int animationCurveNumber = 0;
        
        while (timer < time / 3) {
            float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
            transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            if (angle > maxAngle / 2 + 40) {
                boys.SetActive(false);
                world.SetActive(false);
                worldShadow.SetActive(false);
                worldNoTent.SetActive(true);
                worldNoTentShadow.SetActive(true);
            }
            yield return 0;
        }
        dog.SetActive(true);
        StartCoroutine(ThrowBall());
    }

    IEnumerator ThrowBall ()
    {
        ball.SetActive(true);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(-2.2f, -0.30f, 0.0f);
        rb.AddForce(movement * 30f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(RotateDog(8f));
    }

    IEnumerator RotateDog (float time)
    {
        float maxAngle = 10f;
        float timer = 0.0f;        
        float startAngle = -100;        
        maxAngle = maxAngle - startAngle;
        
        int animationCurveNumber = 1;
        
        while (timer < time) {
            float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
            sphere.transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
    }  
}
