using System.Xml.Serialization;
using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false ;
    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>(); 
        float ratio = (float)Screen.width / (float)Screen.height;
        if(ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin; 
            Vector2 rightBottom = rect.offsetMax;

            leftBottom.y = 0f; 
            rightBottom.y= -100f; 

            rect.offsetMin = leftBottom; 
            rect.offsetMax = rightBottom;
        }
    }
    // Goi truoc khi canvas active 
    public virtual void Setup()
    {
        
    }

    public void ButtonSfx()
    {
        SoundManager.Instance.PlaySfx(ENUM_SOUND.ButtonClick);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true); 
    }
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time) ; 
    }
    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject); 
        }
        else
        {
            gameObject.SetActive(false); 
        }
    }
    public virtual void PlayButton()
    {
        Close(0); 

        UIManager.Instance.OpenUI<CanvasGamePlay>();
        SoundManager.Instance.PlayBgm(ENUM_SOUND.GameplayBgm);

        int index = LevelManager.Instance.CurrentLevelIndex; 
        LevelManager.Instance.ChangeLevel(index);
        GameManager.Instance.OnPlayGame();
    }
    public virtual void MainMenuButton()
    {
        LevelManager.Instance.OnEnd(); 

        UIManager.Instance.CloseAllUI(); 
        UIManager.Instance.OpenUI<CanvasMainMenu>(); 
    }
    public virtual void PlayNextLevel()
    {
        LevelManager.Instance.PlayNextLevel();
        Close(0); 
    }
    public virtual void SetBestScore(int coin , TextMeshProUGUI coinText)
    {
        coinText.text = coin.ToString(); 
    }
}
