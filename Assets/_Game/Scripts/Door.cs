using UnityEngine;

public class Door : MonoBehaviour, IColor
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Renderer meshRenderer; 
    protected ColorType color;
    public void SetColor(ColorType newColor)
    {
        ((IColor)this).ISetColor(meshRenderer, ref color, newColor);
    }
    public void OnTriggerEnter(Collider other)
    {
        Character ch = CacheComponent<Collider,Character>.Get(other);
        if(ch == null) return;

        SetColor(ch.colorCharacter);
    }

}
