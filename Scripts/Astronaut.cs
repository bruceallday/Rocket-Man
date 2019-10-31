using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.iOS;
using System.Collections;
using System.Numerics;

public class Astronaut : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    Animator anim;


    [SerializeField] float levelLoadTime = 3f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 50f;

    [SerializeField] AudioClip mainBoosterSound;
    [SerializeField] AudioClip deathSound;

    [SerializeField] ParticleSystem jetPackParticleEffect;
    [SerializeField] ParticleSystem deathImpactParticleEffect;
    [SerializeField] ParticleSystem deadParticleEffect;

    [SerializeField] GameObject scoreScreen;

    GameObject landingPad;

    UnityEngine.Vector2 startingPos;
    UnityEngine.Vector2 startingPos2;
    UnityEngine.Vector2 direction;

    bool touchingRight;
    bool touchingLeft;

    bool rotateLeft;
    bool rotateRight;

    [SerializeField] bool controlsReversed;

    [SerializeField] ParticleSystem successParticleEffect;

    private TimeDisplay timeDisplay;
    bool collisionEnabled = true;
    public bool isTransitioning;

    void Awake()
    {
        Application.targetFrameRate = 60;
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        landingPad = GameObject.FindWithTag("Finish");
        timeDisplay = GameObject.Find("Time Display").GetComponent<TimeDisplay>();
    }

    void Update()
    {
        if (!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
            RespondToTouchInput();
        }

        if (Debug.isDebugBuild)
        {
            //DEBUG METHOD
            RunDebug();
        }
    }

    private void RespondToTouchInput()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && touch.position.x > (Screen.width / 2))
            {
                touchingRight = true;
                
            }

            if (touch.position.x > (Screen.width / 2) && touch.phase == TouchPhase.Ended)
            {
                touchingRight = false;
            }

        }

        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began && touch.position.x > (Screen.width / 2))
            {
                touchingRight = true; 
            }
            
            if (touch.position.x > (Screen.width / 2) && touch.phase == TouchPhase.Ended)
            {
                touchingRight = false;
            }
            
        }

    }

    private void RunDebug()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !collisionEnabled) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Particle Collider":
                StartDeathSequence();
                rigidBody.constraints = RigidbodyConstraints.None;
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                rigidBody.constraints = RigidbodyConstraints.None;
                break;
        }
    }


    private void StartSuccessSequence()
    {
        timeDisplay.finished = true;
        PlaySuccessAnimations();
        isTransitioning = true;
        landingPad.GetComponent<LandingPadParticleEffect>().PlayLandingEffects();
        landingPad.GetComponent<LandingPadParticleEffect>().BeginScoreReveal();
    }

    private void PlaySuccessAnimations()
    {
        anim.SetBool("isFloating", false);
        anim.SetTrigger("hasLanded");
        anim.SetBool("idle", true);
    }

    public void StartDeathSequence()
    {
       
        anim.SetTrigger("isDead");
        isTransitioning = true;
        PlayDeathAudio();
        PlayDeathParticleEffects();
        Invoke("LoadFirstLevel", levelLoadTime);
    }

    private void PlayDeathParticleEffects()
    {
        deathImpactParticleEffect.Play();
        deadParticleEffect.Play();
    }

    private void PlayDeathAudio()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
    }

    private void LoadFirstLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void RespondToRotateInput()
    {

        float rotaionThisFrame = rcsThrust * Time.deltaTime;



        // Keyboard controlls

        if (!controlsReversed)
        {
            if (Input.GetKey(KeyCode.A) || rotateLeft)
            {
                RotateManually(rotaionThisFrame);
            }
            else if (Input.GetKey(KeyCode.D) || rotateRight)
            {
                RotateManually(-rotaionThisFrame);
            }
        }

        else if (controlsReversed)
        {
            if (Input.GetKey(KeyCode.A) || rotateLeft)
            {
                RotateManually(-rotaionThisFrame);
            }
            else if (Input.GetKey(KeyCode.D) || rotateRight)
            {
                RotateManually(rotaionThisFrame);
            }
        }

    }

    private void RotateManually(float rotaionThisFrame)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(UnityEngine.Vector3.forward * rotaionThisFrame);
        rigidBody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {

        if (Input.GetKey(KeyCode.Space) || touchingRight)
        {
            anim.SetBool("idle", false);
            anim.SetTrigger("jump");
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        jetPackParticleEffect.Stop();
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(UnityEngine.Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainBoosterSound);
            jetPackParticleEffect.Play();
            anim.SetBool("isFloating", true);
        }
    }

    public void TurnLeft()
    {
        rotateLeft = true;
    }

    public void TurnRight()
    {
        rotateRight = true;
    }

    public void StopRotating()
    {
        rotateRight = false;
        rotateLeft = false;
        
    }

}
