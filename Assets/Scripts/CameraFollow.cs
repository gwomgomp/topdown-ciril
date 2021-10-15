using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
  Rigidbody2D body;
  Camera m_MainCamera;
  Vector3 cameraPosition;
  
  float staticCameraZ;
  
  public float speedZoomStart = 10.0f;
  public float speedZoomStop = 20.0f;
  
  public float minZoom = 12.0f;
  public float maxZoom = 24.0f;
  
  public float smoothingRate = 0.5f;
  public float zoomSmoothingRate = 0.5f;
  
  public float lookAheadMultiplier = 2.0f;
  
  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    m_MainCamera = Camera.main;
    
    staticCameraZ = m_MainCamera.transform.position.z;
  }
  
  public void CenterCamera()
  {
    m_MainCamera.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, staticCameraZ);
  }

  void FixedUpdate()
  {
    Vector3 targetCameraPos = body.transform.position;
    Vector3 velocityMultiplied = new Vector3(body.velocity.x * lookAheadMultiplier, body.velocity.y * lookAheadMultiplier, staticCameraZ);
    targetCameraPos = targetCameraPos + velocityMultiplied;
    
    float speedZoomMagnitude = Math.Max(speedZoomStart, Math.Min(speedZoomStop, body.velocity.magnitude)) - speedZoomStart;
    float speedZoomScale = speedZoomStop - speedZoomStart;
    
    float cameraSize = minZoom + ((maxZoom - minZoom) * (speedZoomMagnitude / speedZoomScale));
    
    float cameraSizeDiff = cameraSize - m_MainCamera.orthographicSize;
    if(cameraSizeDiff > 0)
    {
      m_MainCamera.orthographicSize += Math.Min(cameraSizeDiff, zoomSmoothingRate);
    }
    else if(cameraSizeDiff < 0)
    {
      m_MainCamera.orthographicSize += Math.Max(cameraSizeDiff, -zoomSmoothingRate);
    }
    
    m_MainCamera.transform.position = Vector3.MoveTowards(m_MainCamera.transform.position, targetCameraPos, smoothingRate);
  }
}
