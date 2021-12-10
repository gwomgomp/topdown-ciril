using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  public short checkpointNumber = 1;
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player")) {
      RaceEventController.TriggerOnCheckpoint(checkpointNumber);
    }
  }
}
