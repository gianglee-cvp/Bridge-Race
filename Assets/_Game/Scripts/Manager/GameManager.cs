using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
public enum GameStateType 
{
    Lose = 0,
    MainMenu = 1,
    Pause = 2,
    Playing = 3, 
    Win = 4 
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollow camMain;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] public DataManager dataManager;
    private IGameState currentState;
    protected static IGameState Lose = new LoseState();
    protected static IGameState MainMenu = new MainMenuState();
    protected static IGameState Pause = new PausedState();
    protected static IGameState Playing = new PlayingState();
    protected static IGameState Win = new WinState();
    protected static List<IGameState> states = new List<IGameState> { Lose, MainMenu, Pause, Playing, Win };
    
    public IGameState CurrentState => currentState;

    private void Awake()
    {
        poolControl.OnInit(); 
        dataManager.OnInit();
        SoundManager.Instance.OnInit();
        UIManager.Instance.OnInit(); 
        LevelManager.Instance.OnInit();
        InputManager.Instance.OnInit();
        EnterMainMenu();
    }
    public void OnEnable()
    {
        CanvasSettings.OnSoundButton += SaveSoundOn;
    }
    public void OnDisable()
    {
        CanvasSettings.OnSoundButton -= SaveSoundOn;
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
    public void ChangeState(int index)
    {
        IGameState newStage = states[index];
        ChangeState(newStage);
    }

    public void EnterMainMenu()
    {
        ChangeState(MainMenu);
    }

    public void StartGame()
    {
        ChangeState(Playing);
        LevelManager.Instance.OnPlay();
    }

    public void PauseGame()
    {
        if (!IsInState<PlayingState>())
        {
            return;
        }

        ChangeState(Pause);
    }

    public void ResumeGame()
    {
        if (!IsInState<PausedState>())
        {
            return;
        }

        ChangeState(Playing);
    }
    public void SetLose()
    {
        ChangeState(Lose);
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
    public void SaveChangeLevel(int index)
    {
        dataManager.ChangeLevel(index);
    }
    public void SaveSoundOn()
    {
        bool sound = SoundManager.Instance.SoundButton();
        dataManager.SaveSoundOn(sound);
    }
}
