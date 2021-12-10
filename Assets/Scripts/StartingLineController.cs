using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartingLineController : MonoBehaviour
{
  public float startingFrom = 0;
  public float carOffset = 5;
  
  public GameObject carsParentObject;
  private GameObject startingLineObject;
  
  private float RadiansToDegrees(float angle)
  {
    return angle * (float) (180.0f / Math.PI);
  }
  
  private float DegreesToRadians(float angle)
  {
    return angle * (float) (Math.PI / 180.0f);
  }
  
  void Start()
  {
    startingLineObject = FindLastCheckpoint();
    
    float nextCarAngle = startingFrom + 45.0f;
    Vector3 nextCarPosition = startingLineObject.transform.position + new Vector3(((float) Math.Sin(DegreesToRadians(nextCarAngle)) * (carOffset / 2.0f)), (float) Math.Cos(DegreesToRadians(nextCarAngle)) * (carOffset / 2.0f), 0.0f);
    bool alternator = true;
    
    foreach (Transform carTransform in carsParentObject.transform)
    {
      carTransform.position = nextCarPosition;
      carTransform.eulerAngles = new Vector3(
        carTransform.eulerAngles.x,
        carTransform.eulerAngles.y,
        startingFrom
      );
      
      CameraFollow cameraFollow = carTransform.gameObject.GetComponent<CameraFollow>();
      if (cameraFollow != null)
      {
        cameraFollow.CenterCamera();
      }
      
      if (alternator)
      {
        nextCarAngle -= 90.0f;
        alternator = false;
      }
      else
      {
        nextCarAngle += 90.0f;
        alternator = true;
      }
      
      nextCarPosition = nextCarPosition + new Vector3((float) Math.Sin(DegreesToRadians(nextCarAngle)) * (carOffset), (float) Math.Cos(DegreesToRadians(nextCarAngle)) * (carOffset), 0.0f);
    }
  }
  
  private GameObject FindLastCheckpoint()
  {
    GameObject lastCheckpoint = null;
    GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    int highestCheckpointNumber = -1;
    
    foreach (GameObject checkpointObject in checkpoints)
    {
      Checkpoint checkpoint = checkpointObject.GetComponent<Checkpoint>();
      
      if(checkpoint.checkpointNumber > highestCheckpointNumber)
      {
        highestCheckpointNumber = checkpoint.checkpointNumber;
        lastCheckpoint = checkpointObject;
      }
    }
    
    return lastCheckpoint;
  }
}
