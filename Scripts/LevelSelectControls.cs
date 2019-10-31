using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectControls : MonoBehaviour
{
    Transform transform;
    [SerializeField] float speed = 10;

    Vector2 startingPos;
    Vector2 direction;

    [SerializeField] GameObject levelSelectMenu;
    
    void Awake()
    {
        Application.targetFrameRate = 60;
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.right * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position -= transform.right * speed;
        }
        
    }
}
