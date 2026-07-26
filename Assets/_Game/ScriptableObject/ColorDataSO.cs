using UnityEngine;
public enum ENUM_COLOR
{
    None = 0 ,
    Blue = 1,
    Red = 2,
    Yellow = 3,
    Green = 4, 
    Black = 5,
    Stair = 6
}

[CreateAssetMenu(menuName = "Game/ColorDataSO")]
public class ColorDataSO : ScriptableObject
{
    [SerializeField] private Material[] material;
    public Material GetMaterial(ENUM_COLOR color)
    {
        return material[(int)color ];
    }
}