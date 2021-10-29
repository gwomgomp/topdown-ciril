using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
  private PowerUpController.HitPowerUp OnBoost;
  
  public float boostPower = 1.5f;
  public float boostDuration = 0.5f;
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    OnBoost(other, boostPower, boostDuration);
  }
  
  public void SetHandler(PowerUpController.HitPowerUp OnBoost)
  {
    this.OnBoost = OnBoost;
  }
}
