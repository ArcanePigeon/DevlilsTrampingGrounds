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
    public GameObject directionalLight;
    public GameObject devil;

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
        // Key Key
        // Rotate to Empty Scene : 1
        // Devil Appears : 2
        // Devil Disappears : 3
        // Throw Tennis Ball : 4
        // Dog Rotation : 5
        // Rotate to Boys : 6
        // Lightning Effect : 7

        // Mike Talking : M
        // Dustin Talking : D
        // Jimmy Talking : J

        if (Input.GetKeyDown(KeyCode.D)) {
            UpdateSprite(boys, dustinTalking);
        } else if (Input.GetKeyDown(KeyCode.M)) {
            UpdateSprite(boys, mikeTalking);
        } else if (Input.GetKeyDown(KeyCode.J)) {
            UpdateSprite(boys, jimmyTalking);
        } else if (Input.GetKeyUp(KeyCode.D)) {
            UpdateSprite(dustinTalking, boys);
        } else if (Input.GetKeyUp(KeyCode.M)) {
            UpdateSprite(mikeTalking, boys);
        } else if (Input.GetKeyUp(KeyCode.J)) {
            UpdateSprite(jimmyTalking, boys);
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            StopAllCoroutines();
            StartCoroutine(QuarterSpin (10f));
        } else if (Input.GetKeyDown(KeyCode.W)) {
            StopAllCoroutines();
            StartCoroutine(ThrowBall());
        } else if (Input.GetKeyDown(KeyCode.E)) {
            StopAllCoroutines();
            StartCoroutine(RotateDog(8f));
        } else if (Input.GetKeyDown(KeyCode.R)) {
            StopAllCoroutines();
            StartCoroutine(ShowDevil(.25f));
        }
    }

    void UpdateSprite(GameObject oldSprite, GameObject newSprite) {
        oldSprite.SetActive(false);
        newSprite.SetActive(true);
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
                devil.SetActive(false);
                ball.SetActive(false);
                dog.SetActive(false);
            }
            yield return 0;
        }
    }

    IEnumerator ShowDevil(float duration) {
        devil.SetActive(true);
        Light backgroundLight = directionalLight.GetComponent<Light>();
        SpriteRenderer devilSprite = devil.GetComponent<SpriteRenderer>();
        Color startColorDevil = devilSprite.color;
        Color endColorDevil = new Color(startColorDevil.r, startColorDevil.g, startColorDevil.b, 1);
        Color startColorLight = backgroundLight.color;
        Color endColorLight = new Color(108/255f, 5/255f, 0);
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            backgroundLight.color = Color.Lerp(startColorLight, endColorLight, time/duration);
            devilSprite.color = Color.Lerp(startColorDevil, endColorDevil, time/duration);
            yield return null;
        }
    }

    IEnumerator ThrowBall ()
    {
        ball.SetActive(true);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(-2.2f, -0.30f, 0.0f);
        rb.AddForce(movement * 30f);
        yield return null;
    }

    IEnumerator RotateDog (float time)
    {
        dog.SetActive(true);
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
