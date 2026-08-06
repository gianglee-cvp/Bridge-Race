using UnityEngine;

public class Brick : GameUnit, IColor
{
    [SerializeField] public ColorType colorBrick;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] public Collider colliderBrick;
    [SerializeField] public Stage stage;
    public void SetColor(ColorType newColor)
    {
        ((IColor)this).ISetColor(meshRenderer, ref colorBrick, newColor);
    }
    public void OnCollect()
    {
        colliderBrick.enabled = false; 
        SetColor(ColorType.None); 
    }
    public void OnRemain(ColorType color)
    {
        colliderBrick.enabled = true;
        SetColor(color); 
    }
    public void OnTriggerEnter(Collider other)
    {
        Character ch = CacheComponent<Collider, Character>.Get(other);
        if(ch == null) return;
        if (ch.colorCharacter == colorBrick)
        {
            ch.AddBrick(this);
            stage.AddBrickToRemain(this);
        }
    }
}
