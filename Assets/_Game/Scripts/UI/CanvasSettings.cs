using System;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    [SerializeField] GameObject[] buttons; 
    public static event Action OnSoundButton;
    public override void Setup()
    {
        if (GameManager.Instance.IsInState<PlayingState>())
        {
            GameManager.Instance.PauseGame();
        }
    }
    public void SetState(UICanvas canvas)
    {
     for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }   
        if(canvas is CanvasMainMenu)
        {
            buttons[2].gameObject.SetActive(true); 
        }
        else if(canvas is CanvasGamePlay)
        {
            buttons[0].gameObject.SetActive(true); 
            buttons[1].gameObject.SetActive(true); 
        }
    }

    public void ContinueButton()
    {
        GameManager.Instance.ResumeGame();
        Close(0);
    }
    public void SoundButton()
    {
        OnSoundButton?.Invoke();   
    }
}
