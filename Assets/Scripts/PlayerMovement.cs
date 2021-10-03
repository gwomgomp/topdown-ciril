using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D body;

  float horizontal;
  float vertical;

  public float driveSpeed = 10.0f;
  public float turnSpeed = 1.0f;
  
  float radiansToDegrees(float angle)
  {
    return angle * (180 / (float) Math.PI);
  }
  
  float degreesToRadians(float angle)
  {
    return angle * ((float) Math.PI / 180);
  }
  
  // Start is called before the first frame update
  void Start()
  {
      body = GetComponent<Rigidbody2D>();        
  }

  // Update is called once per frame
  void Update()
  {
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");
  }

  private void FixedUpdate()
  {
      body.AddRelativeForce(new Vector2(0, driveSpeed * vertical));
      
      body.rotation += -horizontal * turnSpeed;
      body.rotation %= 360;
      if(body.rotation < 0.0f) {body.rotation += 360.0f;}
  }
}
