using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
  public float boostPower = 1.5f;
  public float boostDuration = 0.5f;
  
  void Start()
  {
    RaceEventController.OnPowerUp += HandlePowerUp;
  }
  
  private void HandlePowerUp(Collider2D other, PowerUp.PowerUpType powerUpType)
  {
    switch(powerUpType)
    {
      case PowerUp.PowerUpType.Boost:
      {
        RaceEventController.TriggerOnBoost(other, boostPower, boostDuration);
        
        break;
      }
      default: break;
    }
  }
}
