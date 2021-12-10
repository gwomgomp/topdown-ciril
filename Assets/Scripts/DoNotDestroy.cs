using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
  public static GameObject instance = null; 
  
  void Awake() 
  { 
    if (instance == null)
    {
      instance = transform.gameObject;
    }
    else
    {
      Destroy (transform.gameObject);
    }
    
    DontDestroyOnLoad (transform.gameObject); 
  }
}
