using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 150f;

    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

    // set physics
    Rigidbody rigidBody;

    // set sound
    AudioSource rocketThrust;
    bool playThurst;

    // Start is called before the first frame update
    void Start()
    {
        // give us access to rigid body of Rocket Ship
        rigidBody = GetComponent<Rigidbody>();

        // get access to sound
        rocketThrust = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
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
                print("FINISH.");
                state = State.Transcending;
                Invoke("LoadLevel", 1f);
                break;
            default:
                print("DEAD");
                state = State.Dying;
                Invoke("LoadLevel", 1f);
                break;
        }
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

    private void Rotate()
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

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!rocketThrust.isPlaying)  // bang signifys is NOT, stops from trying to play repeatedly
            {
                rocketThrust.Play();
            }
        }
        else
        {
            rocketThrust.Stop();
        }
    }
}
