using UnityEngine;
using System.Collections.Generic;
public class Bridge : MonoBehaviour
{
    public List<Step> listStep = new List<Step>(); 
    [SerializeField] public Stage stage;
    public int GetOpponentCount(ColorType color)
    {
        HashSet<ColorType> colorSet = new HashSet<ColorType>();

        foreach (Step st in listStep)
        {
            if (st.colorStep == color || st.colorStep == ColorType.None)
                continue;

            colorSet.Add(st.colorStep);
        }

        return colorSet.Count;
    }
    public int GetMaxPointCount(ColorType color)
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
            step.SetColor(ColorType.Stair);
        }
    }
}