using System;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollow camMain;
    private void Awake()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        LevelManager.Instance.OnInit();
    }


    public void OnCharacterWin(Character character, Transform firstSeed, Transform secondSeed, Transform thirdSeed)
    {
        character.OnWin(firstSeed);

        List<Character> rank = new List<Character>(LevelManager.Instance.Characters);

        rank.Remove(character); 
        rank.Sort((a,b) => b.Point.CompareTo(a.Point));

        rank[0].OnWin(secondSeed); 
        rank.RemoveAt(0); 

        rank[0].OnWin(thirdSeed);
        rank.RemoveAt(0); 

        rank[0].OnLose(); 
        
        if(rank[0] is Player)
        {
            UIManager.Instance.OpenUI<CanvasFail>(); 
        }
        else
        {
            UIManager.Instance.OpenUI<CanvasVictory>();           
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
    public void SetCameraWin()
    {
        camMain.OnWin(); 
    }
    public void InitCamera()
    {
        camMain.OnInit(); 
    }
}
