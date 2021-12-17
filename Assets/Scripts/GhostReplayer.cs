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
    Vector3 tweenedPosition = Vector3.Lerp(lastGhostPosition.ToVector3(), targetGhostPosition.ToVector3(), step);
    
    float tweenedRotation = Mathf.LerpAngle(lastGhostPosition.rotation, targetGhostPosition.rotation, step);
    
    return new GhostRecorder.GhostPosition(tweenedPosition.x, tweenedPosition.y, tweenedRotation);
  }
  
  private void SetPosition(GhostRecorder.GhostPosition ghostPosition)
  {
    gameObject.transform.localPosition = ghostPosition.ToVector3();
    gameObject.transform.localEulerAngles = Vector3.forward * ghostPosition.rotation;
  }
  
  private void StartRace()
  {
    raceStarted = true;
  }
}
