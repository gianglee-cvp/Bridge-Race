using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] public ENUM_COLOR colorBrick;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] public Collider colliderBrick;
    [SerializeField] public Stage stage;
    public void SetColor(ENUM_COLOR newColor, Material material)
    {
        colorBrick = newColor;
        meshRenderer.material = material;
    }
    public void OnCollect()
    {
        gameObject.SetActive(false);
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
                stage.RemoveActiveBrick(this);
            }
        }
    }
}
