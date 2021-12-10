using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  public enum PowerUpType
  {
    Boost,
    Turn
  }
  
  public PowerUpType powerUpType;
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    RaceEventController.TriggerOnPowerUp(other, powerUpType);
  }
}
