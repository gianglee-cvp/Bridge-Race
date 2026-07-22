using UnityEngine; 

public interface IColor
{
    public void ISetColor(Renderer mesh, ref ENUM_COLOR selfcolor , ENUM_COLOR color)
    {
        selfcolor = color;
        Material mat = GameManager.Instance.colorDataSO.GetMaterial(color);
        mesh.material = mat; 
    }
}