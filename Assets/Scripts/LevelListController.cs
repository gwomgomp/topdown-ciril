using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelListController : MonoBehaviour
{
  public GameObject dropdownObject;
  private Dropdown levelSelectDropdown;
  
  void Start()
  {
    string levelDirectory = Application.persistentDataPath + "\\levels\\";
    levelSelectDropdown = dropdownObject.GetComponent<Dropdown>();
    List<string> levelNames = new List<string>();
    
    DirectoryInfo dir = new DirectoryInfo(levelDirectory);
    FileInfo[] info = dir.GetFiles("*.txt");
    
    StreamReader reader;
    string line;
    
    foreach (FileInfo f in info) 
    {
      reader = new StreamReader(f.FullName, true);
      line = reader.ReadLine();
      
      if("+gigadriftlevel".Equals(line))
      {
        levelNames.Add(Path.GetFileNameWithoutExtension(f.Name));
      }
    }
    
    levelSelectDropdown.AddOptions(levelNames);
  }
}
