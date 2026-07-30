using UnityEngine; 

public interface IColor
{
    public void ISetColor(Renderer mesh, ref ColorType selfcolor , ColorType color)
    {
        selfcolor = color;
        Material mat = LevelManager.Instance.GetMaterial(color);
        mesh.material = mat; 
    }
}
