using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControll : MonoBehaviour
{
    GameObject player;
    GameObject landingPad;

    //Delaying the start time
    TimeDisplay timer;

    private void Awake()
    {
        GetComponent<PlayableDirector>().stopped += EnableControl;
        timer = GameObject.Find("Time Display").GetComponent<TimeDisplay>();
        player = GameObject.FindWithTag("Player");
        landingPad = GameObject.FindWithTag("Friendly");
        player.GetComponent<Astronaut>().isTransitioning = true;
    }

    void EnableControl(PlayableDirector pd)
    {
        timer.finished = false;
        player.GetComponent<Astronaut>().isTransitioning = false;
        landingPad.GetComponent<LandingPadParticleEffect>().PlayParticleEffect();
        landingPad.GetComponent<LandingPadParticleEffect>().StartSpawnInRouteine();
    }
      
}
