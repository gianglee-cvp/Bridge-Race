using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSettings : UICanvas
{
    [SerializeField] GameObject[] buttons; 
    public override void Setup()
    {
        GameManager.Instance.SetTimeScale(0); 
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
    public void MainMenuButton()
    {
        GameManager.Instance.OnEnd(); 

        UIManager.Instance.CloseAllUI(); 
        UIManager.Instance.OpenUI<CanvasMainMenu>(); 

    }
    public void ContinueButton()
    {
        GameManager.Instance.SetTimeScale(1); 
        Close(0);
    }

}
