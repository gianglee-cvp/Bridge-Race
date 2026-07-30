using UnityEngine;
public enum ColorType
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
    public Material GetMaterial(ColorType color)
    {
        return material[(int)color ];
    }
}