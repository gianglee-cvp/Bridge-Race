using System;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollow camMain;
    private void Awake()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        LevelManager.Instance.OnInit();
    }


    public void OnCharacterWin(Character character)
    {
        List<Character> rank = new List<Character>(LevelManager.Instance.Characters);
        rank.Sort((a,b) =>  
        {
            if(a == character) return -1; 
            return b.Point.CompareTo(a.Point); 
        });
        
        for( int i = 0 ; i < 3 ; i++)
        {

            rank[i].OnWin(i + 1); 
        }
        rank[3].OnLose(); 
        
        if(rank[3] is Player)
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
