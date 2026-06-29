using UnityEngine;
using System.Collections.Generic;
public class Stair : MonoBehaviour
{
    public List<Step> steps = new List<Step>(); 
    [SerializeField] public List<GameObject> stopPoints = new List<GameObject>(); // so in dex trung voi index cua ENUM_COLOR 
    [SerializeField] public Stage stage;
    public Transform stopPoint;
    public void SetStopTransform(Vector3 position, Quaternion rotation , ENUM_COLOR color)
    {
        int index = (int)color;
        stopPoints[index].transform.position = position;
        stopPoints[index].transform.rotation = rotation;
    }
    public int GetOpponentCount(ENUM_COLOR color)
    {
        Dictionary<ENUM_COLOR, bool> colorCount = new Dictionary<ENUM_COLOR, bool>();
        foreach(Step st in steps)
        {
            if(colorCount.ContainsKey(st.colorStep) || st.colorStep == color)
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
    public Vector3 GetLastStepPosition()
    {
        if (steps.Count > 0)
        {
            Step lastStep = steps[steps.Count - 1];
            return lastStep.transform.position;
        }
        else
        {
            return transform.position;
        }
    }
    public void CloseDoor(ENUM_COLOR color)
    {
        if (steps.Count > 0)
        {
            Step lastStep = steps[steps.Count - 1];
            Transform lastStepTF = lastStep.transform; 
            SetStopTransform(
                lastStepTF.TransformPoint(lastStep.backPointOffset),
                lastStepTF.rotation,
                color
            );
        }
        else
        {
            Debug.LogWarning("Stair: No steps found in the stair.");
        }
    }
}