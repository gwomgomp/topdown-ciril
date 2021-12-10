using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticsController : MonoBehaviour
{
  public Text currentLapTimeText;
  public Text bestLapTimeText;
  public Text lapsCompletedText;
  
  private float currentLapTime = 0.0f;
  private float bestLapTime = -1.0f;
  private int lapsCompleted = 0;
  
  void Start()
  {
    LapController.OnLap += CompleteLap;
  }

  void Update()
  {
    currentLapTime += Time.deltaTime;
    currentLapTimeText.text = string.Format("{0:N2}", currentLapTime);
  }
  
  private void CompleteLap()
  {
    if (bestLapTime < 0.0f || currentLapTime < bestLapTime)
    {
      bestLapTime = currentLapTime;
    }
    
    bestLapTimeText.text = string.Format("{0:N2}", bestLapTime);
    
    lapsCompleted += 1;
    lapsCompletedText.text = lapsCompleted + "";
    
    currentLapTime = 0.0f;
  }
}
