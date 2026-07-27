using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        LevelManager.Instance.OnInit();
    }


    public void OnCharacterWin(Character character, Transform firstSeed, Transform secondSeed, Transform thirdSeed)
    {
        character.OnWin(firstSeed);
        SoundManager.Instance.PlaySfx(ENUM_SOUND.Win);

        Character secondPlace = null;
        Character thirdPlace = null;

        for (int i = 0; i < LevelManager.Instance.Characters.Count; i++)
        {
            Character c = LevelManager.Instance.Characters[i];
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
        LevelManager.Instance.OnPlay();
    }
    public void OnChangeLevel(int levelIndex)
    {
        LevelManager.Instance.ChangeLevel(levelIndex);
    }
    public void SetTimeScale(int t)
    {
        Time.timeScale = t ; 
    }
    public void OnEnd()
    {
        LevelManager.Instance.OnEnd();
    }
}
