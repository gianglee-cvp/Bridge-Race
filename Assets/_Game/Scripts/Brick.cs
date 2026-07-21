using Unity.VisualScripting;
using UnityEngine;

public class Brick : GameUnit
{
    [SerializeField] public ENUM_COLOR colorBrick;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] public Collider colliderBrick;
    [SerializeField] public Stage stage;
    public void SetColor(ENUM_COLOR newColor)
    {
        Material mat = GameManager.Instance.colorDataSO.GetMaterial(newColor); 
        meshRenderer.material = mat;
        
        colorBrick = newColor;
    }
    public void OnCollect()
    {
        // gameObject.SetActive(false); 
        // SetColor(ENUM_COLOR.None);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character ch = GameManager.Instance.GetCharacter(other);
            if (ch.colorCharacter == colorBrick)
            {
                ch.AddBrick(this);
                stage.AddBrickToRemain(this);
            }
        }
    }
}
