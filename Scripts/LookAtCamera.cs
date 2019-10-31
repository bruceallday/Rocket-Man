using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform transform;

    Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
        mainCamera = Camera.main;
        // Update is called once per frame
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
