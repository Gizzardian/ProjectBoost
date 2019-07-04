using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 8f;

// [] are attributes
    float movementFactor; // 0 for not moved

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // set movement factor automatically
        if (period <= Mathf.Epsilon) { return; } // leave if 0, floats use Epilon to compare
        float cycles = Time.time / period; // grows from 0

        // https://en.wikipedia.org/wiki/Sine 
        // https://en.wikipedia.org/wiki/Turn_(geometry)#Tau_proposal
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset =  movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
