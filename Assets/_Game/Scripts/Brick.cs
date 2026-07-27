using UnityEngine;

public class Brick : GameUnit, IColor
{
    [SerializeField] public ENUM_COLOR colorBrick;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] public Collider colliderBrick;
    [SerializeField] public Stage stage;
    public void SetColor(ENUM_COLOR newColor)
    {
        ((IColor)this).ISetColor(meshRenderer, ref colorBrick, newColor);
    }
    public void OnCollect()
    {
        colliderBrick.enabled = false; 
        SetColor(ENUM_COLOR.None); 
    }
    public void OnRemain(ENUM_COLOR color)
    {
        colliderBrick.enabled = true;
        SetColor(color); 
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CharacterTag))
        {
            Character ch = LevelManager.Instance.GetCharacter(other);
            if (ch.colorCharacter == colorBrick)
            {
                ch.AddBrick(this);
                stage.AddBrickToRemain(this);
            }
        }
    }
}
