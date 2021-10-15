using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  public short checkpointNumber = 1;
  
  private LapController.HitCheckpoint OnCheckpoint;
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Trigger hit: " + checkpointNumber);
    
    if (OnCheckpoint != null && other.CompareTag("Player")) {
      OnCheckpoint(checkpointNumber);
    }
  }
  
  public void SetHandler(LapController.HitCheckpoint OnCheckpoint)
  {
    this.OnCheckpoint = OnCheckpoint;
  }
}
