using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class RaceEventController : MonoBehaviour
{
  public delegate void RaceStart();
  public static event RaceStart OnRaceStart;
  
  public delegate void RaceEnd();
  public static event RaceEnd OnRaceEnd;
  
  public delegate void HitCheckpoint(short checkpointNumber);
  public static event HitCheckpoint OnCheckpoint;
  
  public delegate void FinishLap();
  public static event FinishLap OnLap;
  
  public delegate void HitPowerUp(Collider2D other, PowerUp.PowerUpType powerUpType);
  public static event HitPowerUp OnPowerUp;
  
  public delegate void HitBoost(Collider2D other, float boostPower, float boostDuration);
  public static event HitBoost OnBoost;
  
  void Start()
  {
    RemoveEventListeners();
  }
  
  public static void TriggerOnRaceStart()
  {
    if (OnRaceStart != null)
    {
      OnRaceStart();
    }
  }
  
  public static void TriggerOnRaceEnd()
  {
    if (OnRaceEnd != null)
    {
      OnRaceEnd();
    }
  }
  
  public static void TriggerOnCheckpoint(short checkpointNumber)
  {
    if (OnCheckpoint != null)
    {
      OnCheckpoint(checkpointNumber);
    }
  }
  
  public static void TriggerOnLap()
  {
    if (OnLap != null)
    {
      OnLap();
    }
  }
  
  public static void TriggerOnPowerUp(Collider2D other, PowerUp.PowerUpType powerUpType)
  {
    if (OnPowerUp != null)
    {
      OnPowerUp(other, powerUpType);
    }
  }
  
  public static void TriggerOnBoost(Collider2D other, float boostPower, float boostDuration)
  {
    if (OnBoost != null)
    {
      OnBoost(other, boostPower, boostDuration);
    }
  }
  
  private static void RemoveEventListeners()
  {
    OnRaceStart = null;
    OnRaceEnd = null;
    OnLap = null;
    OnCheckpoint = null;
    OnPowerUp = null;
    OnBoost = null;
  }
}
