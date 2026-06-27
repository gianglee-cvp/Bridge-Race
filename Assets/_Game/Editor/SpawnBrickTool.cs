using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BrickSpawner))]
public class BrickSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Vẽ inspector mặc định
        DrawDefaultInspector();

        GUILayout.Space(10);

        BrickSpawner spawner = (BrickSpawner)target;

        if (GUILayout.Button("Spawn Brick"))
        {
            spawner.SpawnAllStage();
        }
    }
}