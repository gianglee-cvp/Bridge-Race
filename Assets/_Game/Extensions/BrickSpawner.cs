using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Brick prefabBase;
    [SerializeField] private int length; 
    [SerializeField] private int width; 
    [SerializeField] public float disX = 0.6f; 
    [SerializeField] public float disZ = 1f;

    [Header("Save Settings")]
    [SerializeField] private string savePath = "Assets/_Game/Resources/Level_1/map_new.json";

    private StageData stageData;
    private MapData newMapData;
    private bool isSpawningAll = false;

    [System.Serializable]
    public struct ColorRatio
    {
        public ENUM_COLOR color;
        [Tooltip("Trọng số tỉ lệ xuất hiện (Ví dụ: Red=50, Blue=30, Yellow=20)")]
        public int ratio; 
        [Tooltip("Prefab tương ứng với màu này để tô lên gạch")]
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

        if (newMapData == null)
        {
            newMapData = new MapData();
            newMapData.stages = new List<StageData>();
        }

        int i = -length/2; 
        int j = -width/2; 
        Vector3 spawnPoint = Vector3.up * 0.625f + root.position;
        
        while(i < length / 2)
        {
            j = -width/2;
            while (j < width / 2)
            {
                spawnPoint.x = root.position.x + disX * i;
                spawnPoint.z = root.position.z + disZ * j;

                NavMeshHit hit;
                if(NavMesh.SamplePosition(spawnPoint, out hit, 0.5f, NavMesh.AllAreas))
                {
                    ColorRatio selectedRatio = GetRandomColorByRatio();

                    Brick unit = Instantiate(prefabBase, spawnPoint, prefabBase.transform.rotation);
                    stageData.bricks.Add(new BrickData { color = selectedRatio.color, position = spawnPoint });
                    unit.SetColor(selectedRatio.color, selectedRatio.material);
                    unit.transform.SetParent(root);

                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.RegisterBrick(unit.colliderBrick, unit);
                    }
                }
                j++;
            }
            i++;
        }
        SaveStageData();

        if (!isSpawningAll)
        {
            SaveToNewFile();
        }

        ClearNavMesh(); 
    }

    public void SpawnAllStage()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null! Please run in Play Mode.");
            return;
        }

        isSpawningAll = true;
        newMapData = new MapData();
        newMapData.stages = new List<StageData>();

        List<Stage> stageList = GameManager.Instance.stageList;
        foreach(var st in stageList)
        {
            Debug.Log($"Spawning stage: {st.name}");
            root = st.transform;
            SpawnBrick();
        }

        SaveToNewFile();
        isSpawningAll = false;
        newMapData = null;
    }

    private void SaveToNewFile()
    {
        if (newMapData == null) return;

        string fullPath = System.IO.Path.Combine(Application.dataPath, "..", savePath).Replace("\\", "/");
        string directory = System.IO.Path.GetDirectoryName(fullPath);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }

        string json = JsonUtility.ToJson(newMapData, true);
        System.IO.File.WriteAllText(fullPath, json);
        Debug.Log($"Successfully saved map data to a new file: {savePath}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    // Chọn màu ngẫu nhiên dựa trên tỉ lệ (ratios) đã thiết lập
    private ColorRatio GetRandomColorByRatio()
    {
        int totalRatio = 0;
        foreach (var cr in colorRatios)
        {
            totalRatio += cr.ratio;
        }

        // Nếu không thiết lập ratio hoặc tổng bằng 0, trả về phần tử đầu tiên hoặc mặc định
        if (totalRatio <= 0)
        {
            return colorRatios.Count > 0 ? colorRatios[0] : new ColorRatio();
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
        if (newMapData != null)
        {
            newMapData.stages.Add(stageData);
        }
    }
}   


