using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    Astronaut player;

    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Astronaut>();
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    void OnParticleCollision(GameObject other)
    {
        //if (isTransitioning || !collisionEnabled) return;

        if (other.tag == "Player")
        {
            var collision = particle.collision;

            collision.enabled = false;
            player.StartDeathSequence();
        }

    }
}
