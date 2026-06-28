using UnityEngine;
using System.Collections.Generic;

public class CharacterBrick : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public void OnCollect(ENUM_COLOR color, Character character )
    {
        meshRenderer.material = GameManager.Instance.GetMaterial(color);
    }

}