using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceEndController : MonoBehaviour
{
  public GameObject statsCanvas;
  public GameObject statisticsControllerObject;
  
  public Text totalTrackTimeText;
  public Text bestLapText;
  
  void Start()
  {
    statsCanvas.SetActive(false);
    
    RaceEventController.OnRaceEnd += EndRace;
  }
  
  private void EndRace()
  {
    StatisticsController statisticsController = statisticsControllerObject.GetComponent<StatisticsController>();
    
    float totalTrackTime = statisticsController.getTotalTrackTime();
    float bestLapTime = statisticsController.getBestLapTime();
    
    totalTrackTimeText.text = string.Format("{0:N2}", totalTrackTime);
    bestLapText.text = string.Format("Best Lap: {0:N2}", bestLapTime);
    
    statsCanvas.SetActive(true);
  }
}
