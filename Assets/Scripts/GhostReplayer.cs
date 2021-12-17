using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GhostReplayer : MonoBehaviour
{
  private List<GhostRecorder.GhostPosition> ghostData;
  
  private bool raceStarted = false;
  private float timer = 0.0f;
  private int targetGhostPositionIndex = 1;
  private GhostRecorder.GhostPosition lastGhostPosition;
  private GhostRecorder.GhostPosition targetGhostPosition;

  void Start()
  {
    string filePath = Application.persistentDataPath + "\\ghosts\\" + MapLoader.mapName + ".txt";
    
    if (File.Exists(filePath))
    {
      StreamReader reader = new StreamReader(filePath, true);
      string line;
      string[] values;
    
      line = reader.ReadLine();
      if(!"+gigadriftghost".Equals(line))
      {
        reader.Close();
        return;
      }
      
      ghostData = new List<GhostRecorder.GhostPosition>();
      
      while ((line = reader.ReadLine()) != null)
      {
        values = line.Split(' ');
        ghostData.Add(new GhostRecorder.GhostPosition(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2])));
      }
      
      reader.Close();
    }
    else
    {
      return;
    }
    
    lastGhostPosition = ghostData[0];
    targetGhostPosition = ghostData[targetGhostPositionIndex];
    
    SetPosition(lastGhostPosition);
    
    RaceEventController.OnRaceStart += StartRace;
  }

  void FixedUpdate()
  {
    if (raceStarted && targetGhostPositionIndex < ghostData.Count)
    {
      timer += Time.deltaTime;
      
      if (timer >= GhostRecorder.recordFrequency)
      {
        timer -= GhostRecorder.recordFrequency;
        targetGhostPositionIndex += 1;
        
        if (targetGhostPositionIndex >= ghostData.Count)
        {
          SetPosition(targetGhostPosition);
          return;
        }
        
        lastGhostPosition = targetGhostPosition;
        targetGhostPosition = ghostData[targetGhostPositionIndex];
      }
      
      SetPosition(TweenGhostPosition(lastGhostPosition, targetGhostPosition, timer / GhostRecorder.recordFrequency));
    }
  }
  
  private GhostRecorder.GhostPosition TweenGhostPosition(GhostRecorder.GhostPosition lastGhostPosition, GhostRecorder.GhostPosition targetGhostPosition, float step)
  {
    float lastRotation = lastGhostPosition.rotation;
    float targetRotation = targetGhostPosition.rotation;
    if (Math.Abs(lastRotation - targetRotation) > 180)
    {
      if (lastRotation > targetRotation)
      {
        targetRotation += 360;
      }
      else
      {
        lastRotation += 360;
      }
    }
    
    float tweenedX = ((targetGhostPosition.x - lastGhostPosition.x) * step) + lastGhostPosition.x;
    float tweenedY = ((targetGhostPosition.y - lastGhostPosition.y) * step) + lastGhostPosition.y;
    float tweenedRotation = ((targetRotation - lastRotation) * step) + lastRotation;
    
    return new GhostRecorder.GhostPosition(tweenedX, tweenedY, tweenedRotation);
  }
  
  private void SetPosition(GhostRecorder.GhostPosition ghostPosition)
  {
    gameObject.transform.localPosition = new Vector3(ghostPosition.x, ghostPosition.y, 0);
    gameObject.transform.localEulerAngles = Vector3.forward * ghostPosition.rotation;
  }
  
  private void StartRace()
  {
    raceStarted = true;
  }
}
