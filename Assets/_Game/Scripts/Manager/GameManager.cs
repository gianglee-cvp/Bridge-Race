using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollow camMain;
    [SerializeField] private PoolControl poolControl;
    private void Awake()
    {
        poolControl.OnInit(); 
        UIManager.Instance.OnInit(); 
        
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        LevelManager.Instance.OnInit();
    }


    public void OnCharacterWin(Character character)
    {
        List<Character> rank = new List<Character>(LevelManager.Instance.Characters);
        rank.Sort((a,b) =>  
        {
            if(a == character) return -1; 
            if(b == character) return 1; 
            return b.Point.CompareTo(a.Point); 
        });
        int i = 0; 
        int cnt = rank.Count;
        while(i < cnt)
        {
            if( i < 3)
            {
                rank[i].OnWin(i+1); 
            }
            else rank[i].OnLose(); 
            i++; 
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
