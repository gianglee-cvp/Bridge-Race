using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] public ENUM_COLOR colorBrick;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] public Collider colliderBrick;
    public void SetColor(ENUM_COLOR newColor, Material material)
    {
        colorBrick = newColor;
        meshRenderer.material = material;
    }
}
