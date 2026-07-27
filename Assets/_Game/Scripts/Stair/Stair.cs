using UnityEngine;
using System.Collections.Generic;
public class Stair : MonoBehaviour
{
    public List<Step> listStep = new List<Step>(); 
    [SerializeField] public List<GameObject> stopPoints = new List<GameObject>(); // so in dex trung voi index cua ENUM_COLOR 
    [SerializeField] public Stage stage;
    public Transform stopPoint;
    public int GetOpponentCount(ENUM_COLOR color)
    {
        HashSet<ENUM_COLOR> colorSet = new HashSet<ENUM_COLOR>();

        foreach (Step st in listStep)
        {
            if (st.colorStep == color)
                continue;

            colorSet.Add(st.colorStep);
        }

        return colorSet.Count;
    }
    public int GetMaxPointCount(ENUM_COLOR color)
    {
        int maxCount = 0;
        foreach(Step st in listStep)
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
        if (listStep.Count > 0)
        {
            Step lastStep = listStep[listStep.Count - 1];
            return lastStep.transform.position;
        }
        else
        {
            return transform.position;
        }
    }
    public void OnEnd()
    {
        foreach(var step in listStep)
        {
            step.SetColor(ENUM_COLOR.Stair);
        }
    }
}