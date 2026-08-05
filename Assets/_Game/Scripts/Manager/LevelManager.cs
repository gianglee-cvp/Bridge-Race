using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] public ColorDataSO colorDataSO;
    [SerializeField] public Player player;
    [SerializeField] public List<Character> listCharacters = new List<Character>();
    [SerializeField] private Transform levelParent;

    private  Dictionary<Collider, Character> characterDictionary = new Dictionary<Collider, Character>();
    private  List<Level> levelPrefabs = new List<Level>();
    private List<Stage> stageList = new List<Stage>();
    private Level currentLevel;
    private int currentLevelIndex = 0;

    public List<Character> Characters => listCharacters;
    public Player Player => player;
    public int CurrentLevelIndex
    {
       get => currentLevelIndex;
       set => currentLevelIndex = value;  
    } 

    public void OnInit()
    {
        characterDictionary.Clear();
        levelPrefabs.Clear();
        stageList.Clear();

        LoadLevelPrefabs();
        currentLevel = null;
        currentLevelIndex = Mathf.Clamp(currentLevelIndex, 0, Mathf.Max(0, levelPrefabs.Count - 1));

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

    public Material GetMaterial(ColorType color)
    {
        return colorDataSO.GetMaterial(color);
    }

    public Stage GetStage(int index)
    {
        return stageList[index];
    }

    public void ChangeLevel(int levelIndex)
    {
        if (levelPrefabs.Count == 0)
        {
            Debug.LogError("LevelManager: no level prefabs found in Resources/Levels.");
            return;
        }

        OnEnd();

        GameManager.Instance.InitCamera(); 

        levelIndex = Mathf.Clamp(levelIndex, 0, levelPrefabs.Count - 1);
        GameManager.Instance.SaveChangeLevel(levelIndex);

        currentLevelIndex = levelIndex;
        currentLevel = SpawnLevel(levelPrefabs[currentLevelIndex]);
        currentLevel.Load();

        stageList = currentLevel.stageList;
        InitCharacters(currentLevel.levelDataSO);
    }

    public void NextLevel()
    {
        if (levelPrefabs.Count == 0)
        {
            return;
        }

        int nextLevel = currentLevelIndex + 1;
        if (nextLevel >= levelPrefabs.Count)
        {
            nextLevel = 0;
        }

        currentLevelIndex = nextLevel;
        UIManager.Instance.GetUI<CanvasMainMenu>().UpdateLevelText(currentLevelIndex);
    }

    public void PrevLevel()
    {
        if (levelPrefabs.Count == 0)
        {
            return;
        }

        int prevLevel = currentLevelIndex - 1;
        if (prevLevel < 0)
        {
            prevLevel = levelPrefabs.Count - 1;
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
        if (currentLevel != null)
        {
            if (currentLevel.gameObject.activeSelf)
            {
                CollectBrickPool();
                currentLevel.Unload();
            }
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }

        foreach (Character character in listCharacters)
        {
            character.OnExitGame();
        }
    }

    private void LoadLevelPrefabs()
    {
        levelPrefabs = new List<Level>(Resources.LoadAll<Level>("Levels"));

        if (levelPrefabs.Count == 0)
        {
            Debug.LogError("Load Level Error");
        }
    }

    private void InitCharacters(LevelDataSO levelData)
    {
        //TODO đổi màu character
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
        if (levelPrefabs.Count == 0)
        {
            return;
        }

        int nextLevel = currentLevelIndex + 1;
        if (nextLevel >= levelPrefabs.Count)
        {
            nextLevel = 0;
        }

        ChangeLevel(nextLevel);
        GameManager.Instance.StartGame(); 
    }
    public Level GetCurrentLevel()
    {
        return currentLevel; 
    }
    public void PlaceAtWinPosition(Character character, int seed)
    {
        if (currentLevel == null)
        {
            return;
        }

        WinPos win = currentLevel.levelDataSO.GetPosAndRot(seed);

        Transform root = currentLevel.transform;
        character.transform.SetPositionAndRotation(
            root.TransformPoint(win.position),
            root.rotation * Quaternion.Euler(win.rotation));
    }

    private Level SpawnLevel(Level levelPrefab)
    {
        Transform parent = levelParent != null ? levelParent : transform;
        return Instantiate(
            levelPrefab,
            levelPrefab.transform.position,
            levelPrefab.transform.rotation,
            parent);
    }

    private void CollectBrickPool()
    {
        if (SimplePool.poolInstance.ContainsKey(PoolType.Brick))
        {
            SimplePool.Collect(PoolType.Brick);
        }
    }
}
