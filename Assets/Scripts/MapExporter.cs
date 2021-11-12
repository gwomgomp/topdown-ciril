using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapExporter : MonoBehaviour
{
  public GameObject tilemapObject;
  
  void Start()
  {
    Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
    
    string filePath = "G:\\racer_levels\\export_" + System.DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
    StreamWriter writer = new StreamWriter(filePath, true);
    
    writer.WriteLine("Tiles:");
    
    foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
    {
      if(tilemap.HasTile(position))
      {
        writer.WriteLine(tilemap.GetSprite(position).name + "," + position.x + "," + position.y);
      }
    }
    
    writer.WriteLine("");
    writer.WriteLine("Checkpoints:");
    
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
