using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollow camMain;
    [SerializeField] private PoolControl poolControl;
    private IGameState currentState;

    public IGameState CurrentState => currentState;

    private void Awake()
    {
        poolControl.OnInit(); 
        UIManager.Instance.OnInit(); 
        LevelManager.Instance.OnInit();
        InputManager.Instance.OnInit();
        EnterMainMenu();
    }

    public void ChangeState(IGameState newState)
    {
        if (currentState != null && currentState.GetType() == newState.GetType())
        {
            return;
        }

        currentState = newState;
        currentState.OnChange(this);
    }

    public void EnterMainMenu()
    {
        ChangeState(new MainMenuState());
    }

    public void StartGame()
    {
        ChangeState(new PlayingState());
        LevelManager.Instance.OnPlay();
    }

    public void PauseGame()
    {
        if (!IsInState<PlayingState>())
        {
            return;
        }

        ChangeState(new PausedState());
    }

    public void ResumeGame()
    {
        if (!IsInState<PausedState>())
        {
            return;
        }

        ChangeState(new PlayingState());
    }
    public void SetLose()
    {
        ChangeState(new LoseState());
    }

    public bool IsInState<T>() where T : class, IGameState
    {
        return currentState != null && currentState.GetType() == typeof(T);
    }

    public void HandleCharacterRank(Character character)
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
                if(rank[i] is Player)
                {
                    UIManager.Instance.GetUI<CanvasVictory>().InitSeed(i+1);
                }
                rank[i].OnWin(i+1); 
            }
            else rank[i].OnLose(); 
            i++; 
        }
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
