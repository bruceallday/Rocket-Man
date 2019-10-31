using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);

    float movementFactor;

    [SerializeField] float period = 2f;

    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSine = Mathf.Sin(cycles * tau);

        movementFactor = rawSine / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
