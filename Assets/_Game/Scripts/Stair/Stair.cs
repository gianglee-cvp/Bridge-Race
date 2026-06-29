using UnityEngine;
using System.Collections.Generic;
public class Stair : MonoBehaviour
{
    public List<Step> steps = new List<Step>(); 
    //Dictionary luu stopPoint theo tung mau 
    //public Dictionary<int , GameObject> stopPoints = new Dictionary<int, GameObject>();
    [SerializeField] public List<GameObject> stopPoints = new List<GameObject>(); // so in dex trung voi index cua ENUM_COLOR 
    [SerializeField] public Stage stage;
    public Transform stopPoint;
    public void SetStopTransform(Vector3 position, Quaternion rotation)
    {
        stopPoint.position = position;
        stopPoint.rotation = rotation;
        
    }
    public int GetOpponentCount()
    {
        Dictionary<ENUM_COLOR, bool> colorCount = new Dictionary<ENUM_COLOR, bool>();
        foreach(Step st in steps)
        {
            if(colorCount.ContainsKey(st.colorStep))
            {
                continue;
            }
            else
            {
                colorCount.Add(st.colorStep, true);
            }
        }
        return colorCount.Count;
    }
    public int GetMaxPointCount(ENUM_COLOR color)
    {
        int maxCount = 0;
        foreach(Step st in steps)
        {
            if(st.colorStep == color)
            {
                maxCount++;
            }
        }
        return maxCount;
    }
}