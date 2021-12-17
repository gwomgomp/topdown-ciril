using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class RaceEndController : MonoBehaviour
{
  private enum GameState
  {
    Running,
    Stats,
    NameEntry,
    Scores,
  }
  
  private class Score
  {
    public float time;
    public string name;
    
    public Score(float time, string name)
    {
      this.time = time;
      this.name = name;
    }
  }
  
  public GameObject statsCanvas;
  public GameObject nameEntryCanvas;
  public GameObject highscoreCanvas;
  public GameObject statisticsControllerObject;
  
  public GameObject highscoreEntriesParentObject;
  
  public GameObject playerCar;
  
  public Text totalTrackTimeText;
  public Text bestLapText;
  
  public InputField nameEntryField;
  
  private GameState gameState = GameState.Running;
  
  private List<Score> scoreList;
  
  private float totalTrackTime;
  private Score newScore;
  
  void Start()
  {
    statsCanvas.SetActive(false);
    nameEntryCanvas.SetActive(false);
    highscoreCanvas.SetActive(false);
    
    RaceEventController.OnRaceEnd += ShowStatsScreen;
  }
  
  void Update()
  {
    if (Input.GetKeyDown("return"))
    {
      if (gameState == GameState.Stats)
      {
        ShowNameEntry();
      }
      else if (gameState == GameState.NameEntry)
      {
        UpdateHighscores();
        ShowHighscores();
      }
      else if (gameState == GameState.Scores)
      {
        SceneManager.LoadScene(0);
      }
    }
  }
  
  private void ShowStatsScreen()
  {
    StatisticsController statisticsController = statisticsControllerObject.GetComponent<StatisticsController>();
    
    totalTrackTime = statisticsController.getTotalTrackTime();
    float bestLapTime = statisticsController.getBestLapTime();
    
    totalTrackTimeText.text = string.Format("{0:N2}", totalTrackTime);
    bestLapText.text = string.Format("Best Lap: {0:N2}", bestLapTime);
    
    gameState = GameState.Stats;
    statsCanvas.SetActive(true);
  }
  
  private void ShowNameEntry()
  {
    scoreList = ReadScoreList();
    
    statsCanvas.SetActive(false);
    
    if (NewHighscoreSet(scoreList, totalTrackTime))
    {
      gameState = GameState.NameEntry;
      nameEntryCanvas.SetActive(true);
    }
    else
    {
      ShowHighscores();
    }
  }
  
  private void UpdateHighscores()
  {
    newScore = new Score(totalTrackTime, nameEntryField.text);
    
    int rank = InsertScore(scoreList, newScore);
    
    if (rank == 0)
    {
      WriteGhostData();
    }
    
    WriteScoreList(scoreList);
  }
  
  private void ShowHighscores()
  {
    DisplayScores(scoreList);
    
    nameEntryCanvas.SetActive(false);
    
    gameState = GameState.Scores;
    highscoreCanvas.SetActive(true);
  }
  
  private List<Score> ReadScoreList()
  {
    List<Score> scoreList = new List<Score>();
    
    string filePath = Application.persistentDataPath + "\\scores\\" + MapLoader.mapName + ".txt";
    
    if (File.Exists(filePath))
    {
      StreamReader reader = new StreamReader(filePath, true);
      string line;
      string[] values;
    
      line = reader.ReadLine();
      if(!"+gigadriftscore".Equals(line))
      {
        reader.Close();
        return scoreList;
      }
      
      while ((line = reader.ReadLine()) != null)
      {
        values = line.Split(';');
        scoreList.Add(new Score(float.Parse(values[0]), values[1]));
      }
      
      reader.Close();
    }
    
    return scoreList;
  }
  
  private bool NewHighscoreSet(List<Score> scoreList, float newScore)
  {
    if (scoreList.Count < 10)
    {
      return true;
    }
    
    foreach (Score score in scoreList)
    {
      if (score.time > newScore)
      {
        return true;
      }
    }
    
    return false;
  }
  
  private int InsertScore(List<Score> scoreList, Score newScore)
  {
    int i = 0;
    
    while (i < 10)
    {
      if (i == scoreList.Count || scoreList[i].time > newScore.time)
      {
        scoreList.Insert(i, newScore);
        
        while (scoreList.Count > 10)
        {
          scoreList.RemoveAt(10);
        }
        
        return i;
      }
      
      i += 1;
    }
    
    return 10;
  }
  
  private void WriteScoreList(List<Score> scoreList)
  {
    string levelFolderPath = Application.persistentDataPath + "\\scores\\";
    
    if(!Directory.Exists(levelFolderPath))
    {    
      Directory.CreateDirectory(levelFolderPath);
    }
    
    if(MapLoader.mapName.Equals(""))
    {
      return;
    }
    
    string filePath = levelFolderPath + MapLoader.mapName + ".txt";
    
    StreamWriter writer = new StreamWriter(filePath, false);
    
    writer.WriteLine("+gigadriftscore");
    
    foreach (Score score in scoreList)
    {
      writer.WriteLine(score.time + ";" + score.name);
    }
    
    writer.Close();
  }
  
  private void WriteGhostData()
  {
    string levelFolderPath = Application.persistentDataPath + "\\ghosts\\";
    
    if(!Directory.Exists(levelFolderPath))
    {    
      Directory.CreateDirectory(levelFolderPath);
    }
    
    if(MapLoader.mapName.Equals(""))
    {
      return;
    }
    
    GhostRecorder ghostRecorder = playerCar.GetComponent<GhostRecorder>();
    
    if (ghostRecorder == null)
    {
      return;
    }
    
    List<GhostRecorder.GhostPosition> ghostData = ghostRecorder.GetGhostData();
    
    string filePath = levelFolderPath + MapLoader.mapName + ".txt";
    
    StreamWriter writer = new StreamWriter(filePath, false);
    
    writer.WriteLine("+gigadriftghost");
    
    foreach (GhostRecorder.GhostPosition ghostPosition in ghostData)
    {
      writer.WriteLine(ghostPosition.x + " " + ghostPosition.y + " " + ghostPosition.rotation);
    }
    
    writer.Close();
  }
  
  private void DisplayScores(List<Score> scoreList)
  {
    Transform highscoreEntriesParentTransform = highscoreEntriesParentObject.transform;
    
    int i = 0;
    
    while (i < scoreList.Count)
    {
      Text highscoreEntryText = highscoreEntriesParentTransform.Find("HighscoreEntryText_" + i).GetComponent<Text>();
      highscoreEntryText.text = string.Format("{0:N2}", scoreList[i].time) + " - " + scoreList[i].name;
      
      i += 1;
    }
  }
}
