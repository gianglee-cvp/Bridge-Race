using System.Collections.Generic;
using UnityEngine;
public class MapManager : MonoBehaviour
{
    public MapData mapData = new MapData();
    private string path = Application.dataPath + "/_Game/Resources/Level_1/map.json";
    private Brick brickPrefab;
    private ColorDataSO colorDataSO;
    public void LoadMap(List<Stage> stageList)
    {
        string json = System.IO.File.ReadAllText(path);
        mapData = JsonUtility.FromJson<MapData>(json);

        brickPrefab = Resources.Load<Brick>("BrickPrefab"); 
        colorDataSO = GameManager.Instance.colorDataSO;

        for(int i = 0 ; i < mapData.stages.Count; i++)
        {
            StageData stageData = mapData.stages[i];
            Stage stageRoot = stageList[i];
            foreach (var brickData in stageData.bricks)
            {
                Brick unit = Instantiate(
                    brickPrefab, 
                    brickData.position, 
                    stageRoot.transform.rotation,
                    stageRoot.transform);

                unit.SetColor(brickData.color, GameManager.Instance.colorDataSO.GetMaterial(brickData.color));
                unit.stage = stageRoot;
                
                GameManager.Instance.RegisterBrick(unit.colliderBrick, unit);
            }
        }
    }
    public void SaveMap()
    {
        string json = JsonUtility.ToJson(mapData, true);
        System.IO.File.WriteAllText(path, json);
    }
}