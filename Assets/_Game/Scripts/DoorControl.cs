using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private int blockWallLayer => LayerMask.NameToLayer(Constants.LayerBlockWall);
    [SerializeField] private int stageIndex;

    private Dictionary<ENUM_COLOR, int> colorToLayerPass = new Dictionary<ENUM_COLOR, int>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constants.CharacterTag))
        {
            return;
        }

        Character character = LevelManager.Instance.GetCharacter(other);
        character.ReachNewStage(LevelManager.Instance.GetStage(stageIndex));
        AddColor(character.colorCharacter, character.gameObject.layer);
    }

    public void AddColor(ENUM_COLOR color, int characterLayer)
    {
        if (colorToLayerPass.ContainsKey(color))
        {
            return;
        }

        colorToLayerPass.Add(color, characterLayer);
        Physics.IgnoreLayerCollision(characterLayer, blockWallLayer, false);
    }

    public void OnEnd()
    {
        foreach (int characterLayer in colorToLayerPass.Values)
        {
            Physics.IgnoreLayerCollision(characterLayer, blockWallLayer, true);
        }

        colorToLayerPass.Clear();
    }
}
