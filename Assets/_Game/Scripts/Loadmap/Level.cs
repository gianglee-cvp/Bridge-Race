using UnityEngine;
using System.Collections.Generic;
public class Level : MonoBehaviour
{
    [SerializeField] public List<Stage> stageList = new List<Stage>();
    [SerializeField] public LevelDataSO levelDataSO;
}