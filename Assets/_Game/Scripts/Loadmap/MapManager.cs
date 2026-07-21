using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
public class MapManager : MonoBehaviour
{
    // private string path = Application.dataPath + "/_Game/Resources/Level_1/map.json";
    private Brick brickPrefab;
    private ColorDataSO colorDataSO;
    public void LoadMap(Level currentLevel)
    {
        // string json = System.IO.File.ReadAllText(path);
        // mapData = JsonUtility.FromJson<MapData>(json);
        // mapData = currentLevel.levelDataSO.mapData;
        List<StageData> stageDataList = currentLevel.levelDataSO.stages;

        brickPrefab = Resources.Load<Brick>("BrickPrefab"); 
        colorDataSO = GameManager.Instance.colorDataSO;

        for(int i = 0 ; i < stageDataList.Count; i++)
        {
            StageData stageData = stageDataList[i];
            Stage stageRoot = currentLevel.stageList[i];
            foreach (var brickData in stageData.bricks)
            {
                Brick unit = SimplePool.Spawn<Brick>(
                    brickPrefab.poolType, 
                    brickData.position, 
                    stageRoot.transform.rotation,
                    stageRoot.transform);
                
                stageRoot.AddRemainBrick(unit , brickData.color);


                //unit.SetColor(brickData.color);
                unit.stage = stageRoot;
                //unit.gameObject.SetActive(false);
                
                GameManager.Instance.RegisterBrick(unit.colliderBrick, unit);
            }
        }
    }
    // public void SpawnBrickNewStage(Stage stageRoot,ENUM_COLOR color)
    // {
    //     StageData stageData = mapData.stages[stageRoot.stageIndex];
        
    //     foreach (var brickData in stageData.bricks)
    //     {
    //         if(brickData.color != color) continue;
            
    //         Brick unit = Instantiate(
    //             brickPrefab, 
    //             brickData.position, 
    //             stageRoot.transform.rotation,
    //             stageRoot.transform);

    //         stageRoot.AddActiveBrick(unit , brickData.color);
    //     }
    // }

}