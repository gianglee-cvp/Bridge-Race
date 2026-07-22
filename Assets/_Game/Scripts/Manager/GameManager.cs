using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    [SerializeField] public ColorDataSO colorDataSO;
    public MapManager mapManager;
    public Player player;
    public int currentLevelIndex = 0;
    private void Awake()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();

        AddListLevel();
        foreach (var character in listCharacters)
        {
            characterDictionary.Add(character.characterCollider, character);
        }

    }
    public void OnChangeLevel(int levelIndex)
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < listLevels.Count)
        {
            listLevels[currentLevelIndex].gameObject.SetActive(false);
        }

        if (levelIndex >= 0 && levelIndex < listLevels.Count)
        {
            SetTimeScale(1); 

            currentLevelIndex = levelIndex;
            Level level = listLevels[currentLevelIndex];
            level.OnInit();
            level.gameObject.SetActive(true);

            stageList = level.stageList;
            Debug.Log("level Load");
            mapManager.LoadMap(level);
        }
        // foreach (var character in listCharacters)
        // {
        //     character.OnInit();
        // }
        LevelDataSO levelData = listLevels[currentLevelIndex].levelDataSO ;
        Vector3 pos = levelData.player.position;
        player.OnInit(pos) ;
        for(int i = 1 ; i < listCharacters.Count; i++)
        {
            Debug.Log("1");
            pos = levelData.listEnemy[i-1].position;
            listCharacters[i].OnInit(pos); 
        }
    }

    public void NextLevel()
    {
        if (listLevels.Count == 0) return;

        int nextLevel = currentLevelIndex + 1;
        if (nextLevel >= listLevels.Count)
        {
            nextLevel = 0; 
        }

        currentLevelIndex = nextLevel;
        UIManager.Instance.GetUI<CanvasMainMenu>().UpdateLevelText(currentLevelIndex);
        // OnChangeLevel(nextLevel);
    }

    public void PrevLevel()
    {
        if (listLevels.Count == 0) return;

        int prevLevel = currentLevelIndex - 1;
        if (prevLevel < 0)
        {
            prevLevel = listLevels.Count - 1; 
        }
        
        currentLevelIndex = prevLevel;
        UIManager.Instance.GetUI<CanvasMainMenu>().UpdateLevelText(currentLevelIndex);
        // OnChangeLevel(prevLevel);
    }

    public Vector2 GetVector2XZ(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
    public void OnCharacterWin(Character character, Transform firstSeed, Transform secondSeed, Transform thirdSeed)
    {
        character.OnWin(firstSeed);

        Character secondPlace = null;
        Character thirdPlace = null;

        for (int i = 0; i < listCharacters.Count; i++)
        {
            Character c = listCharacters[i];
            if (c == character) continue;

            if (secondPlace == null || c.Point > secondPlace.Point)
            {
                thirdPlace = secondPlace;
                secondPlace = c;
            }
            else if (thirdPlace == null || c.Point > thirdPlace.Point)
            {
                thirdPlace = c;
            }
        }

        if (secondPlace != null)
        {
            secondPlace.OnWin(secondSeed);
        }

        if (thirdPlace != null)
        {
            thirdPlace.OnWin(thirdSeed);
        }
    }   
    public void OnPlayGame()
    {
        foreach (var character in listCharacters)
        {
            character.OnPlay();
        }
    }
    public void SetTimeScale(int t)
    {
        Time.timeScale = t ; 
    }
    public void OnEnd()
    {
        Level level = listLevels[currentLevelIndex]; 
        level.OnEnd();

        listLevels[currentLevelIndex].gameObject.SetActive(false);
        Debug.Log("1++");
        foreach(var ch in listCharacters)
        {
            ch.OnExitGame();
        }
    }
}
