using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
  public delegate void HitPowerUp(Collider2D other, float boostPower, float boostDuration);
  public static event HitPowerUp OnBoost;
  
  void Start()
  {
    GameObject[] boosts = GameObject.FindGameObjectsWithTag("Boost");
    
    foreach (GameObject boostObject in boosts)
    {
      Boost boost = boostObject.GetComponent<Boost>();
      boost.SetHandler(OnBoost);
    }
  }
}
