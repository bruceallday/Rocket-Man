using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    Camera target;

    void Start()
    {
        target = Camera.main;
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
    }
}
