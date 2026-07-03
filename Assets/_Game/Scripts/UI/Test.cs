using UnityEngine;

public class Test : MonoBehaviour
{
    int score = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && UIManager.Instance.IsOpened<CanvasGamePlay>())
        {
            UIManager.Instance.GetUI<CanvasGamePlay>().UpdateCoin(++ score); 
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UIManager.Instance.CloseAllUI(); 
            UIManager.Instance.OpenUI<CanvasVictory>().SetBestScore(score); 
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.CloseAllUI(); 
            UIManager.Instance.OpenUI<CanvasFail>().SetBestScore(score); 
        }
    }
}
