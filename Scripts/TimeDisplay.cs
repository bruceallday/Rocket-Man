using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    TextMeshProUGUI timerText;
    public float startTime;
    public bool finished;
    public string minutes;
    public string secdonds;
    public float timeTotalScore;

    int totalMinutes;
    int totalSecdonds;




    void Awake()
    {
        finished = true;
        timerText = GetComponent<TextMeshProUGUI>();
        
    }

    void Update()
    {
        StartTimer();
        if (finished) return;
        UpdateTime();
        UpdateTimeTotalScore();
        
    }

    private void StartTimer()
    {
        if (!finished) return;
        startTime = Time.time;
    }

    public void UpdateTime()
    { 
        if (finished) return;
        float t = Time.time - startTime;
        minutes = ((int)t / 60).ToString();
        secdonds = (t % 60).ToString("f1");
        timerText.text = minutes + ":" + secdonds;
        
    }

    public void UpdateTimeTotalScore()
    {

        timeTotalScore += Time.deltaTime;
        
    }

   

   
        

  
}
