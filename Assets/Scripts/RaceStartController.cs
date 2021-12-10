using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceStartController : MonoBehaviour
{
  public Text countdownText;
  private float timer = 0;
  
  void Start()
  {
    countdownText.text = "3";
  }

  void Update()
  {
    if (timer < 5.0f)
    {
      timer += Time.deltaTime;
      
      if (timer >= 4.0f)
      {
        countdownText.text = "";
      }
      else if (timer >= 3.0f)
      {
        countdownText.text = "Go!";
        RaceEventController.TriggerOnRaceStart();
      }
      else if (timer >= 2.0f)
      {
        countdownText.text = "1";
      }
      else if (timer >= 1.0f)
      {
        countdownText.text = "2";
      }
    }
  }
}
