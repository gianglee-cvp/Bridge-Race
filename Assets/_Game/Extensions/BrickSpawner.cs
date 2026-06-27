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

    private StageData stageData;
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
        Debug.Log("Spawn start");
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

                    GameManager.Instance.RegisterBrick(unit.colliderBrick, unit);

                }
                j++;
            }
            i++;
        }
        SaveStageData();
        GameManager.Instance.mapManager.SaveMap();
        ClearNavMesh(); 
    }
    public void SpawnAllStage()
    {
        List<GameObject> stageList = GameManager.Instance.stageList;
        GameManager.Instance.mapManager.mapData.stages.Clear();
        foreach(var st in stageList)
        {
            Debug.Log($"Spawning stage: {st.name}");
            root = st.transform;
            SpawnBrick();
        }
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
        GameManager.Instance.mapManager.mapData.stages.Add(stageData);
    }
}   


