using UnityEngine;

public class Step : MonoBehaviour
{
    public Stair stairHolder; // TODO khong keo inspector nua cho vafo 1 dic<step, stair>
    public ENUM_COLOR colorStep = ENUM_COLOR.None;
    public Vector3 frontPointOffset; // local position
    public Vector3 backPointOffset; // local position
    public Vector3 rotateOffset; // local position
    public MeshRenderer meshRenderer;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character ch = GameManager.Instance.GetCharacter(other);
            if (ch.CheckCharacterGoUpStair())
            {
                
                if ( ch.colorCharacter != colorStep && SetStopPoint(ch))
                {
                    //  Debug.Log("Step: " + gameObject.name + " Character: " + ch.gameObject.name + " is on step");
                    SetColor(ch.colorCharacter);
                    stairHolder.stage.OnRemainBrick();
                    ch.RemoveBrick();
                }
            }
        }
    }
    // public void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag("Character"))
    //     {
    //         Character ch = GameManager.Instance.GetCharacter(other);
    //         if (ch.CheckDistanceToStep(transform) && ch.colorCharacter != colorStep)
    //         {
    //             //  Debug.Log("Step: " + gameObject.name + " Character: " + ch.gameObject.name + " is on step");
    //             SetColor(ch.colorCharacter);
    //             stairHolder.stage.OnRemainBrick();
    //             ch.RemoveBrick();
    //         }
    //     }
    // }

    public bool SetStopPoint(Character ch)
    {
        // gia tri tra ve la true neu character co the di len step 
        int brickCount = ch.currentBrickCount;
        int brickColor = (int)ch.colorCharacter;
        if(brickColor == (int) colorStep)
        {
            stairHolder.SetStopTransform(transform.TransformPoint(backPointOffset), transform.rotation );
            // Debug.Log(this.gameObject.name + ": 1");
            return true;
        }
        else
        {
            if(brickCount ==  0 )
            {
                stairHolder.SetStopTransform(transform.TransformPoint(frontPointOffset), transform.rotation );
                // Debug.Log(this.gameObject.name + ": 2");
                return false;
            }
            else
            {
                stairHolder.SetStopTransform( transform.TransformPoint(backPointOffset), transform.rotation );
             //   ch.RemoveBrick();
               // Debug.Log(this.gameObject.name + ": 3");
               return true;
            }
        }
    }
    public void SetColor(ENUM_COLOR color)
    {
        colorStep = color;
        meshRenderer.material = GameManager.Instance.colorDataSO.GetMaterial(color);
    }

}