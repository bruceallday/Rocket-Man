using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{

    Transform transform;

    [SerializeField] float speed = 100;
    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z > 300)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z - 1006));
        }
    }
}
