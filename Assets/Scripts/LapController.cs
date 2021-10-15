using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LapController : MonoBehaviour
{
  public short lapNumber = 3;
  
  private short finalCheckpointNumber = 0;
  private short nextCheckpointNumber = 1;
  private short lapsCompleted = 0;
  
  public delegate void HitCheckpoint(short checkpointNumber);
  public static event HitCheckpoint OnCheckpoint;
  
  // Start is called before the first frame update
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
  
  void OnGUI()
  {
    GUI.Label(new Rect(10,30,100,100), "nextCheckpoint: " + nextCheckpointNumber);
    GUI.Label(new Rect(10,60,100,100), "laps: " + lapsCompleted);
    //GUI.Label(new Rect(10,90,100,100), "angle: " + Math.Tan(body.velocity.x, body.velocity.y));
  }
  
  public void UpdateCheckpoint(short checkpointNumber)
  {
    Debug.Log("Trigger recieved: " + checkpointNumber);
    Debug.Log("Current nextCheckpoint: " + nextCheckpointNumber);
    
    if (checkpointNumber == nextCheckpointNumber)
    {
      Debug.Log("Checkpoint hit");
      nextCheckpointNumber++;
      
      if (nextCheckpointNumber > finalCheckpointNumber)
      {
        Debug.Log("Lap completed");
        nextCheckpointNumber = 1;
        lapsCompleted++;
      }
    }
  }
}
