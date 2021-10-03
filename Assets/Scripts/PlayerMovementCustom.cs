using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovementCustom : MonoBehaviour
{
  Rigidbody2D body;

  float horizontal;
  float vertical;
  bool brake;

  public float acceleration = 0.01f;
  public float topSpeed = 10.0f;
  public float turnSpeed = 5.0f;
  public float driftAngle = 0.785398f;
  public float speedDecay = 3.0f;
  public float brakeForce = 30.0f;
  
  float currentSpeed;
  float currentAngle;
  float angleDiff;
  
  private void cycleAngles()
  {
    body.rotation = body.rotation % 360;
    if(body.rotation < 0.0f) {body.rotation += 360.0f;}
    currentAngle = currentAngle % 360;
    if(currentAngle < 0.0f) {currentAngle += 360.0f;}
  }
  
  // Start is called before the first frame update
  void Start()
  {
      body = GetComponent<Rigidbody2D>();
      
      currentAngle = body.rotation;
      cycleAngles();
  }

  // Update is called once per frame
  void Update()
  {
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");
      brake = Input.GetKey("space");
  }    
  
  void OnGUI()
  {
    GUI.Label(new Rect(10,30,100,100), "bodyrotation: "+body.rotation);
    GUI.Label(new Rect(10,60,100,100), "currentangle: "+currentAngle);
    GUI.Label(new Rect(10,90,100,100), "angleDiff: "+angleDiff);
  }

  private void FixedUpdate()
  {
      body.rotation += -horizontal * turnSpeed;
      
      currentSpeed += vertical * acceleration;
      currentSpeed = Math.Min(currentSpeed, topSpeed);
      
      angleDiff = body.rotation - currentAngle;
      if(angleDiff < -180.0f) { angleDiff += 360.0f; }
      if(angleDiff > 180.0f) { angleDiff -= 360.0f; }
      
      if(angleDiff < 0.0f)
      {
        //currentAngle += Math.Max(angleDiff, -(0.4f + (0.8f * (1.0f - (topSpeed / currentSpeed)))));
        currentAngle += Math.Max(angleDiff, -5.0f);
      }
      else
      {
        //currentAngle += Math.Min(angleDiff, 0.4f + (0.8f * (1.0f - (topSpeed / currentSpeed))));
        currentAngle += Math.Min(angleDiff, 5.0f);
      }
      
      cycleAngles();
      
      float radAngle = currentAngle * ((float) Math.PI / 180);
      
      body.velocity = new Vector2((float) -Math.Sin(radAngle) * currentSpeed, (float) Math.Cos(radAngle) * currentSpeed);
  }
}
