using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D body;

  public float driveSpeed = 20.0f;
  public float turnSpeed = 2.5f;
  
  private bool controlEnabled = false;

  private float horizontal;
  private float vertical;
  
  private float boostTimer = 0.0f;
  private float effectiveDriveSpeed = 0.0f;
  
  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    effectiveDriveSpeed = driveSpeed;
    
    RaceEventController.OnBoost += BoostHandler;
    RaceEventController.OnRaceStart += EnableControl;
    RaceEventController.OnRaceEnd += DisableControl;
  }

  void Update()
  {
    if (Input.GetKeyDown("escape"))
    {
      SceneManager.LoadScene(0);
    }
    
    if (controlEnabled)
    {
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");
      
      if (boostTimer > 0.0f)
      {
        boostTimer -= Time.deltaTime;
        
        if (boostTimer <= 0.0f)
        {
          effectiveDriveSpeed = driveSpeed;
        }
      }
    }
  }

  private void FixedUpdate()
  {
    body.AddRelativeForce(new Vector2(0, effectiveDriveSpeed * vertical));
    
    body.rotation += -horizontal * turnSpeed;
    body.rotation %= 360;
    if(body.rotation < 0.0f) {body.rotation += 360.0f;}
  }
  
  private void EnableControl()
  {
    controlEnabled = true;
  }
  
  private void DisableControl()
  {
    horizontal = 0.0f;
    vertical = 0.0f;
    controlEnabled = false;
  }
  
  private void BoostHandler(Collider2D other, float boostPower, float boostDuration)
  {
    if(other.gameObject == gameObject)
    {
      ApplyBoost(boostPower, boostDuration);
    }
  }
  
  private void ApplyBoost(float boostPower, float boostDuration)
  {
    boostTimer = boostDuration;
    effectiveDriveSpeed *= boostPower;
  }
}
