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
    // TODO tat colider de stay bi check point 1 nhieu lan 
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character ch = GameManager.Instance.GetCharacter(other);
            //TODO fix stay
            //Debug.Log("check point 1"); 
            if (ch.colorCharacter == colorBrick)
            {
                Debug.Log("Checkpoint 2"); 
                ch.AddBrick(this);
                stage.AddBrickToRemain(this);
            }
        }
    }
}
