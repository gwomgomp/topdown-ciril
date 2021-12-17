using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
  public class GhostPosition
  {
    public float x;
    public float y;
    
    public float rotation;
    
    public GhostPosition(float x, float y, float rotation)
    {
      this.x = x;
      this.y = y;
      this.rotation = rotation;
    }
  }
  
  public static float recordFrequency = 0.25f;
  
  private List<GhostPosition> ghostData;
  
  private bool raceStarted = false;
  private bool recording = false;
  private float timer = 0.0f;
  
  void Start()
  {
    ghostData = new List<GhostPosition>();
    
    RecordGhostPosition(gameObject.transform);
    
    RaceEventController.OnRaceStart += StartRecording;
    RaceEventController.OnRaceEnd += StopRecording;
  }
  
  void Update()
  {
    if (recording)
    {
      timer += Time.deltaTime;
      
      if (timer >= recordFrequency)
      {
        recording = raceStarted;
        timer -= recordFrequency;
        RecordGhostPosition(gameObject.transform);
      }
    }
  }

  private void RecordGhostPosition(Transform carTransform)
  {
    ghostData.Add(new GhostPosition(carTransform.localPosition.x, carTransform.localPosition.y, carTransform.localEulerAngles.z));
  }
  
  private void StartRecording()
  {
    raceStarted = true;
    recording = true;
  }
  
  private void StopRecording()
  {
    raceStarted = false;
  }
  
  public List<GhostPosition> GetGhostData()
  {
    return ghostData;
  }
}
