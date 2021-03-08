using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotPointController : MonoBehaviour
{
    public List<AnimationCurve> animationCurves;
    
    private bool spinning;    
    private float anglePerItem;    
    private int randomTime;
    private int itemNumber;
    public GameObject boys;
    public GameObject boysShadow;
    public GameObject world;
    public GameObject worldShadow;
    public GameObject worldNoTent;
    public GameObject worldNoTentShadow;
    public GameObject ball;
    public GameObject sphere;
    public GameObject dog;
    
    void Start(){
        anglePerItem = 90;
        // 1.97 represents the item number
        float maxAngle = 360 * 4 + (1.97f * anglePerItem);
        Coroutine introSpin = StartCoroutine(SpinTheWheel (20f, maxAngle));
    }
    
    void  Update ()
    {

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
        StartCoroutine(QuarterSpin (10f));
    } 

    IEnumerator QuarterSpin (float time)
    {
        float maxAngle = 150;
        float timer = 0.0f;        
        float startAngle = transform.eulerAngles.z;        
        maxAngle = maxAngle - startAngle;
        
        int animationCurveNumber = 0;
        
        while (timer < time / 2) {
            float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
            transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            if (angle > maxAngle / 2 + 40) {
                boys.SetActive(false);
                boysShadow.SetActive(false);
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
