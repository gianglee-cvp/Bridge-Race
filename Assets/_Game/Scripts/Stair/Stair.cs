using UnityEngine;
using System.Collections.Generic;
public class Stair : MonoBehaviour
{
    public List<Step> steps = new List<Step>(); 
    //Dictionary luu stopPoint theo tung mau 
    //public Dictionary<int , GameObject> stopPoints = new Dictionary<int, GameObject>();
    [SerializeField] public List<GameObject> stopPoints = new List<GameObject>(); // so in dex trung voi index cua ENUM_COLOR 
    public Transform stopPoint;
    public void SetStopTransform(Vector3 position, Quaternion rotation)
    {
        stopPoint.position = position;
        stopPoint.rotation = rotation;
        
    }
}