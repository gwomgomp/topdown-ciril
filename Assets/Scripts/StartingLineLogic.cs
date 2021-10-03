using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartingLineLogic : MonoBehaviour
{
  Rigidbody2D startingLineBody;
  public float startingFrom = 0;
  public float carOffset = 90;
  
  
  private float radiansToDegrees(float angle)
  {
    return angle * (float) (180.0f / Math.PI);
  }
  
  private float degreesToRadians(float angle)
  {
    return angle * (float) (Math.PI / 180.0f);
  }
  
  void Start()
  {
    startingLineBody = GetComponent<Rigidbody2D>();
    
    float nextCarAngle = startingFrom + 45.0f;
    Vector3 nextCarPosition = transform.position + new Vector3(((float) Math.Sin(degreesToRadians(nextCarAngle)) * (carOffset / 2.0f)), (float) Math.Cos(degreesToRadians(nextCarAngle)) * (carOffset / 2.0f), 0.0f);
    bool alternator = true;
    
    GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
    
    foreach (GameObject car in cars)
    {
      car.transform.position = nextCarPosition;
      car.transform.eulerAngles = new Vector3(
        car.transform.eulerAngles.x,
        car.transform.eulerAngles.y,
        startingFrom
      );
      
      CameraFollow cameraFollow = car.GetComponent<CameraFollow>();
      if (cameraFollow != null)
      {
        cameraFollow.centerCamera();
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
      
      nextCarPosition = nextCarPosition + new Vector3((float) Math.Sin(degreesToRadians(nextCarAngle)) * (carOffset), (float) Math.Cos(degreesToRadians(nextCarAngle)) * (carOffset), 0.0f);
    }
  }
}
