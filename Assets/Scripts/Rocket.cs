using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 150f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip trancendingSound;

    [SerializeField] ParticleSystem mainEnginePart;
    [SerializeField] ParticleSystem deathPart;
    [SerializeField] ParticleSystem successPart;


    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

    // set physics
    Rigidbody rigidBody;

    // set sound
    AudioSource audioSource;
    bool playThurst;

    // Start is called before the first frame update
    void Start()
    {
        // give us access to rigid body of Rocket Ship
        rigidBody = GetComponent<Rigidbody>();

        // get access to sound
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly collision");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathPart.Play();
        Invoke("LoadLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(trancendingSound);
        successPart.Play();
        Invoke("LoadLevel", levelLoadDelay);
    }

    private void LoadLevel()
    {
        if (state == State.Transcending)
        {
            SceneManager.LoadScene(1);
        }
        else if (state == State.Dying)
        {
            SceneManager.LoadScene(0);
        }
            
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true; // take control of rotation, fix rotation bug
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // resume 
    }

    private void RespondToThrustInput()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(thrustThisFrame);
        }
        else
        {
            audioSource.Stop();
            mainEnginePart.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!audioSource.isPlaying)  // bang signifys is NOT, stops from trying to play repeatedly
        {
            audioSource.PlayOneShot(mainEngineSound);
        }
        mainEnginePart.Play();
    }
}
