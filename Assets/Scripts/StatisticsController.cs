using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsController : MonoBehaviour
{
  public Text currentLapTimeText;
  public Text bestLapTimeText;
  public Text lapsCompletedText;
  
  private bool countStarted = false;
  
  private float totalTrackTime = 0.0f;
  private float currentLapTime = 0.0f;
  private float bestLapTime = -1.0f;
  private int lapsCompleted = 0;
  
  void Start()
  {
    lapsCompletedText.text = lapsCompleted + " / " + LapController.totalLaps;
    
    RaceEventController.OnLap += CompleteLap;
    RaceEventController.OnRaceStart += StartCount;
    RaceEventController.OnRaceEnd += StopCount;
  }

  void Update()
  {
    if (countStarted)
    {
      totalTrackTime += Time.deltaTime;
      currentLapTime += Time.deltaTime;
      currentLapTimeText.text = string.Format("{0:N2}", currentLapTime);
    }
  }
  
  private void StartCount()
  {
    countStarted = true;
  }
  
  private void StopCount()
  {
    countStarted = false;
  }
  
  private void CompleteLap()
  {
    if (bestLapTime < 0.0f || currentLapTime < bestLapTime)
    {
      bestLapTime = currentLapTime;
    }
    
    bestLapTimeText.text = string.Format("{0:N2}", bestLapTime);
    
    lapsCompleted += 1;
    lapsCompletedText.text = lapsCompleted + " / " + LapController.totalLaps;
    
    currentLapTime = 0.0f;
  }
  
  public float getTotalTrackTime()
  {
    return totalTrackTime;
  }
  
  public float getBestLapTime()
  {
    return bestLapTime;
  }
}
