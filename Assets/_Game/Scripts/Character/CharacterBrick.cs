using UnityEngine;
using System.Collections;

public class CharacterBrick : GameUnit
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject[] smoke;
    public void OnCollect(ENUM_COLOR color, Character character, Vector3 startPos, Quaternion startRot)
    {
        meshRenderer.material = GameManager.Instance.GetMaterial(color);
        gameObject.layer = LayerMask.NameToLayer(
                GameManager.Instance.listColorLayerName[(int)color]
        );

        Transform targetHolder = character.parentBrick;
        Vector3 targetLocalPos = new Vector3(0, character.currentBrickCount * 0.15f, 0);

        transform.SetParent(null);
        transform.position = startPos;
        transform.rotation = startRot;

        StopAllCoroutines();
        StartCoroutine(FlyToCharacter(targetHolder, targetLocalPos, startPos, startRot));
    }

    private IEnumerator FlyToCharacter(Transform targetHolder, Vector3 targetLocalPos, Vector3 startPos, Quaternion startRot)
    {
        foreach(var p in smoke)
        {
            p.SetActive(true);
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 1.5f;

            Vector3 endPos = targetHolder.TransformPoint(targetLocalPos);
            Vector3 controlPoint = (startPos + endPos) * 0.5f + Vector3.up * 2f;
            Vector3 basePos = CalculateBezier(startPos, controlPoint, endPos, t);

            float angle = t * Mathf.PI * 2f;
            float radius = Mathf.Sin(t * Mathf.PI) * 3f; 
            Vector3 circleOffset = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);

            transform.position = basePos + circleOffset;
            transform.rotation = Quaternion.Slerp(startRot, targetHolder.rotation, t);

            yield return null;
        }

        foreach(var p in smoke)
        {
            p.SetActive(false);
        }

        transform.SetParent(targetHolder);
        transform.localPosition = targetLocalPos;
        transform.localRotation = Quaternion.identity;
    }

    private Vector3 CalculateBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}
