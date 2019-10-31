using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMissileBehaviour : MonoBehaviour
{
    Transform transform;

    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 400, transform.position.z);
        }
    }
}
