using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        //can always press space, but only a or d
        
        if (Input.GetKey(KeyCode.Space))
        {
            print("Space pressed.");
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            print("A Pressed, Rotate left.");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("D Pressed, Rotate right.");
        }
    }

}
