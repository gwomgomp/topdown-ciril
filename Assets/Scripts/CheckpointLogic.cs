using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{
  public short checkpointNumber = 1;
  
  void Start()
  {
      
  }
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Trigger hit: " + checkpointNumber);
    
    LapLogic lapScript = other.gameObject.GetComponent<LapLogic>();
    if (lapScript != null)
    {
      lapScript.hitCheckpoint(checkpointNumber);
    }
  }
}
