using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{

    bool initialSave;

    ScoreScreen scoreScreen;

    void Start()
    {
        Application.targetFrameRate = 60;
        scoreScreen = GameObject.Find("Score Screen").GetComponent<ScoreScreen>();
        SavingSystem.InitialSave(scoreScreen);
    }

}
