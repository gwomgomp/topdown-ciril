using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LapController : MonoBehaviour
{
  public short lapNumber = 3;
  
  private short finalCheckpointNumber = 0;
  private short nextCheckpointNumber = 1;
  
  public delegate void HitCheckpoint(short checkpointNumber);
  public static event HitCheckpoint OnCheckpoint;
  
  public delegate void FinishLap();
  public static event FinishLap OnLap;
  
  void Start()
  {
    OnCheckpoint += UpdateCheckpoint;
    
    GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    
    foreach (GameObject checkpointObject in checkpoints)
    {
      Checkpoint checkpoint = checkpointObject.GetComponent<Checkpoint>();
      finalCheckpointNumber = Math.Max(checkpoint.checkpointNumber, finalCheckpointNumber);
      checkpoint.SetHandler(OnCheckpoint);
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
        OnLap();
      }
    }
  }
}
