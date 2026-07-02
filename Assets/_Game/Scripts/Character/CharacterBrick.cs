using UnityEngine;
using System.Collections.Generic;

public class CharacterBrick : GameUnit
{
    [SerializeField] private MeshRenderer meshRenderer;
    public void OnCollect(ENUM_COLOR color, Character character )
    {
        meshRenderer.material = GameManager.Instance.GetMaterial(color);
        gameObject.layer = LayerMask.NameToLayer(
                GameManager.Instance.listColorLayerName[(int)color]
        );
    }

}