using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int currentLevelIndex = 0;
    public int bestScore = 0 ;
    public int coinsTotal = 0;
    public bool musicOn = true;
    public bool soundOn = true;
}

public class DataManager : MonoBehaviour
{
    public GameData gameData { get; private set; }

    public void OnInit()
    {
        Load();
        LevelManager.Instance.CurrentLevelIndex = gameData.currentLevelIndex;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(Constants.GAMEDATA, json);
        PlayerPrefs.Save();
    } 

    public void Load()
    {
        if (PlayerPrefs.HasKey(Constants.GAMEDATA))
        {
            gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(Constants.GAMEDATA));
        }
        else
        {
            gameData = new GameData();
            Save();
        }
    }
    public void ChangeLevel(int index)
    {
        gameData.currentLevelIndex = index; 
        Save();
    }
    public void SaveSoundOn(bool sound)
    {
        gameData.musicOn = sound;
        Save();
    }
    public bool GetMusic()
    {
        return gameData.musicOn;
    }
}

