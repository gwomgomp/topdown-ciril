using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLaunchController : MonoBehaviour
{
  public GameObject dropdownObject;
  
  public void LaunchLevel()
  {
    Dropdown levelDropdown = dropdownObject.GetComponent<Dropdown>();
    MapLoader.mapName = levelDropdown.options[levelDropdown.value].text;
    
    SceneManager.LoadScene(1);
  }
}
