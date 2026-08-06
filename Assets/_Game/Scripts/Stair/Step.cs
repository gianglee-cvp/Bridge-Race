using System.Collections;
using UnityEngine;

public class Step : MonoBehaviour, IColor
{
    public Bridge stairHolder;
    public ColorType colorStep = ColorType.None;
    public Renderer meshRenderer;
    public bool isLastStep = false; 
    public void OnTriggerEnter(Collider other)
    {
        ProcessCharacterTrigger(other);
    }
    public void OnTriggerStay(Collider other)
    {
        ProcessCharacterTrigger(other);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.CharacterTag))
        {
            Character ch = CacheComponent<Collider, Character>.Get(other);
            if(ch is Player)
            {
                ch.CanMoveUp = true; 
                ch.BlockedStepForward = Vector3.zero;
            }
        }
    }
    public bool SetStopPoint(Character ch)
    {
        // gia tri tra ve la true neu character co the di len step 
        int brickCount = ch.CurrentBrickCount;
        int brickColor = (int)ch.colorCharacter;
        if(brickColor != (int) colorStep && brickCount == 0)
        {
            ch.CanMoveUp = false; 
            ch.BlockedStepForward = transform.forward;
            ch.OnBlockedByStep(transform.forward);
            return false;
        }
        else
        {
                ch.CanMoveUp = true;
                ch.BlockedStepForward = Vector3.zero;
                if(isLastStep)
                {
                    ch.ReachLastStep(this);
                }
                return true;
        }   
    }
    private void ProcessCharacterTrigger(Collider other)
    {
        Character ch = CacheComponent<Collider, Character>.Get(other);
        if(ch == null) return ;

        if(!ch.CheckCharacterGoUpStair(transform.forward)) return; 

        if ( SetStopPoint(ch))
        {   
            if(ch.colorCharacter != colorStep)
            {
                SetColor(ch.colorCharacter);
                stairHolder.stage.OnRemainBrick(ch.colorCharacter);
                ch.RemoveBrick();
                
                if(ch is Player)
                {
                    SoundManager.Instance.PlaySfx(ENUM_SOUND.StairStep);
                }
            }
        }
    }
    public void SetColor(ColorType color)
    {
        ((IColor)this).ISetColor(meshRenderer , ref colorStep , color); 
        meshRenderer.material.SetFloat("_Flash",0);
        StartCoroutine(FlashCoroutine());
    }
    private IEnumerator FlashCoroutine()
    {
        float duration = 0.35f; 
        float time = 0f; 
        while(time < duration)
        {
            time += Time.deltaTime;
            float value = 1 - time/duration;    
            meshRenderer.material.SetFloat("_Flash",value);

            yield return null; 
        }

        meshRenderer.material.SetFloat("_Flash", 0f);
        yield break;
    }


}
