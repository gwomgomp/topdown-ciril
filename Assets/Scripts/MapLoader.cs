using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapLoader : MonoBehaviour
{
  public GameObject barrierTilemapObject;
  public GameObject checkpointParentObject;
  public GameObject checkpointPrefab;
  public static string mapName = "";
  public bool loadMap = false;
  
  void Start()
  {
    loadMap = false;
    
    Tilemap barrierTilemap = barrierTilemapObject.GetComponent<Tilemap>();
    
    foreach (Vector3Int position in barrierTilemap.cellBounds.allPositionsWithin)
    {
      if(barrierTilemap.HasTile(position))
      {
        barrierTilemap.SetTile(position, null);
      }
    }
    
    GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
  
    foreach (GameObject checkpointObject in checkpoints)
    {
      Destroy(checkpointObject);
    }
  
    string filePath = "G:\\racer_levels\\" + mapName + ".txt";
    StreamReader reader = new StreamReader(filePath, true);
    string line;
    string[] values;
    
    line = reader.ReadLine();
    if(!"+gigadriftlevel".Equals(line))
    {
      return;
    }
    
    string mode = "+barriers";
    bool unknownMode = false;
    
    while ((line = reader.ReadLine()) != null)
    {
      if(line.Equals(""))
      {
        continue;
      }
      else if(line[0] == '+')
      {
        mode = line;
        unknownMode = false;
        continue;
      }
      else if(unknownMode)
      {
        continue;
      }
      
      switch(mode)
      {
        case "+barriers":
          values = line.Split(',');
      
          Vector3Int position = new Vector3Int(int.Parse(values[1]), int.Parse(values[2]), 0);
          TileBase tile = Resources.Load<TileBase>("Tiles/" + values[0]);
          
          barrierTilemap.SetTile(position, tile);
          
          break;
        case "+checkpoints":
          values = line.Split(',');
          
          GameObject newCheckpointObject = Instantiate(checkpointPrefab, new Vector3(float.Parse(values[1]), float.Parse(values[2]), 0.0f), Quaternion.identity);
          newCheckpointObject.transform.parent = checkpointParentObject.transform;
          
          newCheckpointObject.transform.localScale = new Vector3(float.Parse(values[3]), float.Parse(values[4]), 1);
          
          short newCheckpointNumber = short.Parse(values[0]);
          
          Checkpoint newCheckpointController = newCheckpointObject.GetComponent<Checkpoint>();
          newCheckpointController.checkpointNumber = newCheckpointNumber;
          
          break;
        default:
          unknownMode =  true;
          
          break;
      }
    }
  }
}
