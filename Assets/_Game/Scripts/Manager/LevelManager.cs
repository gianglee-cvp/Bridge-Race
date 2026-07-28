using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] public ColorDataSO colorDataSO;
    [SerializeField] public Player player;
    [SerializeField] public List<Character> listCharacters = new List<Character>();

    private  Dictionary<Collider, Character> characterDictionary = new Dictionary<Collider, Character>();
    private  List<Level> listLevels = new List<Level>();
    private List<Stage> stageList = new List<Stage>();
    private int currentLevelIndex = 0;

    public List<Character> Characters => listCharacters;
    public Player Player => player;
    public int CurrentLevelIndex => currentLevelIndex;

    public void OnInit()
    {
        characterDictionary.Clear();
        listLevels.Clear();
        stageList.Clear();
        currentLevelIndex = 0;

        AddListLevel();

        foreach (Character character in listCharacters)
        {
            if (character != null && character.characterCollider != null)
            {
                characterDictionary[character.characterCollider] = character;
            }
        }
    }

    public Character GetCharacter(Collider collider)
    {
        return characterDictionary[collider];
    }

    public Material GetMaterial(ENUM_COLOR color)
    {
        return colorDataSO.GetMaterial(color);
    }

    public Stage GetStage(int index)
    {
        return stageList[index];
    }

    public void ChangeLevel(int levelIndex)
    {
        OnEnd();
        GameManager.Instance.InitCamera(); 
        // if (currentLevelIndex >= 0 && currentLevelIndex < listLevels.Count)
        // {
        //     listLevels[currentLevelIndex].Unload();
        // }

        GameManager.Instance.SetTimeScale(1);

        currentLevelIndex = levelIndex;
        Level level = listLevels[currentLevelIndex];
        level.Load();

        stageList = level.stageList;
        InitCharacters(level.levelDataSO);
    }

    public void NextLevel()
    {
        if (listLevels.Count == 0)
        {
            return;
        }

        int nextLevel = currentLevelIndex + 1;
        if (nextLevel >= listLevels.Count)
        {
            nextLevel = 0;
        }

        currentLevelIndex = nextLevel;
        UIManager.Instance.GetUI<CanvasMainMenu>().UpdateLevelText(currentLevelIndex);
    }

    public void PrevLevel()
    {
        if (listLevels.Count == 0)
        {
            return;
        }

        int prevLevel = currentLevelIndex - 1;
        if (prevLevel < 0)
        {
            prevLevel = listLevels.Count - 1;
        }

        currentLevelIndex = prevLevel;
        UIManager.Instance.GetUI<CanvasMainMenu>().UpdateLevelText(currentLevelIndex);
    }

    public void OnPlay()
    {
        foreach (Character character in listCharacters)
        {
            character.OnPlay();
        }
    }

    public void OnEnd()
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < listLevels.Count)
        {
            if (!listLevels[currentLevelIndex].gameObject.activeSelf)
            {
                return;
            }
            listLevels[currentLevelIndex].Unload();
        }

        foreach (Character character in listCharacters)
        {
            character.OnExitGame();
        }
    }

    private void AddListLevel()
    {
        Queue<GameUnit> levelQueue = SimplePool.poolInstance[PoolType.Level].GetInactive();
        foreach (GameUnit level in levelQueue)
        {
            listLevels.Add((Level)level);
        }
    }

    private void InitCharacters(LevelDataSO levelData)
    {
        Vector3 pos = levelData.player.position;
        player.OnInit(pos);

        for (int i = 1; i < listCharacters.Count; i++)
        {
            pos = levelData.listEnemy[i - 1].position;
            listCharacters[i].OnInit(pos);
        }
    }
    public void PlayNextLevel()
    {
        int nextLevel = currentLevelIndex + 1;
        if (nextLevel >= listLevels.Count)
        {
            nextLevel = 0;
        }

        ChangeLevel(nextLevel);
        GameManager.Instance.OnPlayGame(); 
    }
}
