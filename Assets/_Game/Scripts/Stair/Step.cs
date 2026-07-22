using UnityEngine;
using UnityEngine.Rendering;

public class Step : MonoBehaviour, IColor
{
    public Stair stairHolder; // TODO khong keo inspector nua cho vafo 1 dic<step, stair>
    public ENUM_COLOR colorStep = ENUM_COLOR.None;
    public Vector3 frontPointOffset; // local position
    public Vector3 backPointOffset; // local position
    public Vector3 rotateOffset; // local position
    public Renderer meshRenderer;
    public bool isLastStep = false; // neu la step cuoi cung thi se khong cho character di len nua
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character ch = GameManager.Instance.GetCharacter(other);

            if(!ch.CheckCharacterGoUpStair() ) return; 

            if ( SetStopPoint(ch))
            {   
                if(ch.colorCharacter != colorStep)
                {
                    SetColor(ch.colorCharacter);
                    stairHolder.stage.OnRemainBrick(ch.colorCharacter);
                    ch.RemoveBrick();                
                }
            }
        }   
    }

    public bool SetStopPoint(Character ch)
    {
        // gia tri tra ve la true neu character co the di len step 
        int brickCount = ch.currentBrickCount;
        int brickColor = (int)ch.colorCharacter;
        if(brickColor != (int) colorStep && brickCount == 0)
        {
                stairHolder.SetStopTransform(transform.TransformPoint(frontPointOffset), transform.rotation , ch.colorCharacter);
                return false;
        }
        else
        {
                stairHolder.SetStopTransform( transform.TransformPoint(backPointOffset), transform.rotation , ch.colorCharacter);
                if(isLastStep)
                {
                    ch.ReachLastStep(this);
                }
                return true;
        }
    }
    public void SetColor(ENUM_COLOR color)
    {
        // colorStep = color;
        // meshRenderer.material = GameManager.Instance.colorDataSO.GetMaterial(color);
        ((IColor)this).ISetColor(meshRenderer , ref colorStep , color); 
    }
    public void SetStopPointOnLastStep(Character ch)
    {
        Vector3 offset = new Vector3(0, 4.5f, 0f);
        stairHolder.SetStopTransform( transform.TransformPoint(backPointOffset + offset), transform.rotation , ch.colorCharacter);
    }

}