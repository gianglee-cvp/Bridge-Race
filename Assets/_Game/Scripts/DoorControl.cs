using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private int blockWallLayer;
    [SerializeField] private int stageIndex;

    private Dictionary<ENUM_COLOR, int> colorToLayerPass = new Dictionary<ENUM_COLOR, int>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Character"))
        {
            return;
        }

        Character character = GameManager.Instance.GetCharacter(other);
        character.ReachNewStage(GameManager.Instance.stageList[stageIndex]);
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
