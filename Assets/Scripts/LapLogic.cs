using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapLogic : MonoBehaviour
{
  public short lapNumber = 3;
  
  private short numberOfCheckpoints = 0;
  private short nextCheckpointNumber = 1;
  private short lapsCompleted = 0;
  
  // Start is called before the first frame update
  void Start()
  {
    numberOfCheckpoints = (short) GameObject.FindGameObjectsWithTag("Checkpoint").Length;
  }

  // Update is called once per frame
  void Update()
  {
    
  }
  
  void OnGUI()
  {
    GUI.Label(new Rect(10,30,100,100), "nextCheckpoint: " + nextCheckpointNumber);
    GUI.Label(new Rect(10,60,100,100), "laps: " + lapsCompleted);
    //GUI.Label(new Rect(10,90,100,100), "angle: " + Math.Tan(body.velocity.x, body.velocity.y));
  }
  
  public void hitCheckpoint(short checkpointNumber)
  {
    Debug.Log("Trigger recieved: " + checkpointNumber);
    Debug.Log("Current nextCheckpoint: " + nextCheckpointNumber);
    
    if (checkpointNumber == nextCheckpointNumber)
    {
      Debug.Log("Checkpoint hit");
      nextCheckpointNumber++;
      
      if (nextCheckpointNumber > numberOfCheckpoints)
      {
        Debug.Log("Lap completed");
        nextCheckpointNumber = 1;
        lapsCompleted++;
      }
    }
  }
}
