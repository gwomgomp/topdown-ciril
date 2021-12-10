using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LapController : MonoBehaviour
{
  public static short totalLaps = 1;
  public GameObject statisticsControllerObject;
  
  private short finalCheckpointNumber = 0;
  private short nextCheckpointNumber = 1;
  private short lapsCompleted = 0;
  
  void Start()
  {
    RaceEventController.OnCheckpoint += UpdateCheckpoint;
    
    GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    
    foreach (GameObject checkpointObject in checkpoints)
    {
      Checkpoint checkpoint = checkpointObject.GetComponent<Checkpoint>();
      finalCheckpointNumber = Math.Max(checkpoint.checkpointNumber, finalCheckpointNumber);
    }
  }
  
  private void UpdateCheckpoint(short checkpointNumber)
  {
    if (checkpointNumber == nextCheckpointNumber)
    {
      nextCheckpointNumber++;
      
      if (nextCheckpointNumber > finalCheckpointNumber)
      {
        nextCheckpointNumber = 1;
        lapsCompleted += 1;
        RaceEventController.TriggerOnLap();
        
        if (lapsCompleted >= totalLaps)
        {
          RaceEventController.TriggerOnRaceEnd();
        }
      }
    }
  }
}
