using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take control of rotation, fix rotation bug

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
        rigidBody.freezeRotation = false; // resume 
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
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
