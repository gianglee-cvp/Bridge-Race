using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    [SerializeField] public ColorDataSO colorDataSO;
    public MapManager mapManager;
    public Player player;
    public Level currentLevel;
    private void Awake()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        // stageList = currentLevel.stageList;
        AddListLevel();
        foreach (var character in listCharacters)
        {
            characterDictionary.Add(character.characterCollider, character);
            character.OnInit();
        }
    }
    public void OnChangeLevel(int levelIndex)
    {
        if(currentLevel != null)
        {
            currentLevel.gameObject.SetActive(false);
        }
        if (levelIndex >= 0 && levelIndex < listLevels.Count)
        {
            currentLevel = listLevels[levelIndex];
            currentLevel.gameObject.SetActive(true);

            stageList = currentLevel.stageList;
            mapManager.LoadMap(currentLevel);
        }
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
    
}
