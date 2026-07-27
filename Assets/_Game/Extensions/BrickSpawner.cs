using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private List<Stage> stageList = new List<Stage>();
    [SerializeField] private Brick prefabBase;
    [SerializeField] private int length; 
    [SerializeField] private int width; 
    [SerializeField] public float disX = 0.6f; 
    [SerializeField] public float disZ = 1f;

    [SerializeField] private LevelDataSO levelData;

    private StageData stageData;
    private bool isSpawningAll = false;

    [System.Serializable]
    public struct ColorRatio
    {
        public ENUM_COLOR color;
        [Tooltip("Trọng số tỉ lệ xuất hiện")]
        public int ratio; 
        [Tooltip("Prefab")]
        public Material material;
    }

    [Header("Color Ratio Settings")]
    [SerializeField] private List<ColorRatio> colorRatios = new List<ColorRatio>();

    private NavMeshModifier modifier;
    private NavMeshSurface surface;

    public void SpawnBrick()
    {
        AddNavMeshSurface();
        stageData = new StageData();

        int i = -width/2; 
        int j = -length/2; 
        
        while(i < width / 2)
        {
            j = -length/2;
            while (j < length / 2)
            {
                // Sử dụng TransformPoint để tính toán tọa độ local theo rotation của Stage
                Vector3 localSpawnPos = new Vector3(disX * i, 0.625f, disZ * j);
                Vector3 spawnPoint = root.TransformPoint(localSpawnPos);

                NavMeshHit hit;
                if(NavMesh.SamplePosition(spawnPoint, out hit, 0.5f, NavMesh.AllAreas))
                {
                    ColorRatio selectedRatio = GetRandomColorByRatio();

                    Brick unit = Instantiate(prefabBase, spawnPoint, prefabBase.transform.rotation);
                    stageData.bricks.Add(new BrickData { color = selectedRatio.color, position = spawnPoint });
                    unit.SetColor(selectedRatio.color);
                    unit.transform.SetParent(root);

                }
                j++;
            }
            i++;
        }
        SaveStageData();

        ClearNavMesh(); 
    }

    public void SpawnAllStage()
    {

        isSpawningAll = true;
        levelData.Clear();

        foreach(var st in stageList)
        {
            if (st == null)
            {
                continue;
            }

            root = st.transform;
            SpawnBrick();
        }

        isSpawningAll = false;
    }

    private ColorRatio GetRandomColorByRatio()
    {
        int totalRatio = 0;
        foreach (var cr in colorRatios)
        {
            totalRatio += cr.ratio;
        }

        int randomValue = Random.Range(0, totalRatio);
        int currentSum = 0;

        foreach (var cr in colorRatios)
        {
            currentSum += cr.ratio;
            if (randomValue < currentSum)
            {
                return cr;
            }
        }

        return colorRatios[0];
    }

    public void AddNavMeshSurface()
    {
        if(root.GetComponent<NavMeshModifier>() != null && root.GetComponent<NavMeshSurface>() != null)
        {
            modifier = root.GetComponent<NavMeshModifier>();
            surface = root.GetComponent<NavMeshSurface>();
            modifier.enabled = true;
            surface.enabled = true;
            surface.BuildNavMesh();
            return;
        }
        modifier = root.gameObject.AddComponent<NavMeshModifier>();
        surface = root.gameObject.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.MarkedWithModifier;
        surface.BuildNavMesh();
    }

    public void ClearNavMesh()
    {
        modifier.enabled = false;
        surface.enabled = false;
    }

    public void SaveStageData()
    {
        levelData.AddStage(stageData);

        UnityEditor.EditorUtility.SetDirty(levelData);
        UnityEditor.AssetDatabase.SaveAssets();
    }
}   


