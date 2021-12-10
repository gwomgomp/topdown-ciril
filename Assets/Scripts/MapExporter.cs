using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapExporter : MonoBehaviour
{
  public GameObject barrierTilemapObject;
  public bool exportMap = false;
  
  void Update()
  {
    if(exportMap)
    {
      exportMap = false;
      
      Tilemap barrierTilemap = barrierTilemapObject.GetComponent<Tilemap>();
      
      string filePath = "G:\\racer_levels\\export_" + System.DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
      StreamWriter writer = new StreamWriter(filePath, true);
      
      writer.WriteLine("+gigadriftlevel");
      
      writer.WriteLine("+barriers");
      
      foreach (Vector3Int position in barrierTilemap.cellBounds.allPositionsWithin)
      {
        if(barrierTilemap.HasTile(position))
        {
          writer.WriteLine(barrierTilemap.GetSprite(position).name + "," + position.x + "," + position.y);
        }
      }
      
      writer.WriteLine("+checkpoints");
      
      GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
      
      foreach (GameObject checkpointObject in checkpoints)
      {
        Transform transform = checkpointObject.transform;
        
        Checkpoint checkpoint = checkpointObject.GetComponent<Checkpoint>();
        writer.WriteLine(checkpoint.checkpointNumber + "," + transform.position.x + "," + transform.position.y + "," + transform.localScale.x + "," + transform.localScale.y);
      }
      
      writer.Close();
    }
  }
}
