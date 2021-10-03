using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementBackup : MonoBehaviour
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
  
  void OnGUI()
  {
    GUI.Label(new Rect(10,30,100,100), "speed: " + (Math.Sqrt((body.velocity.x * body.velocity.x) + (body.velocity.x * body.velocity.x))));
    GUI.Label(new Rect(10,60,100,100), "rotation: " + body.rotation);
    //GUI.Label(new Rect(10,90,100,100), "angle: " + Math.Tan(body.velocity.x, body.velocity.y));
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
