using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BrickSpawner))]
public class BrickSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        BrickSpawner spawner = (BrickSpawner)target;

        if (GUILayout.Button("Spawn Brick"))
        {
            spawner.SpawnAllStage();
        }
        if (GUILayout.Button("Collect Brick"))
        {
            spawner.CollectData();
        }
    }
}