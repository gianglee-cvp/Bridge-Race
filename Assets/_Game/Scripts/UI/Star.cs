using System.Collections;
using UnityEngine; 
public class Star : MonoBehaviour
{
    [SerializeField] private Animator[] clip; 
    private float delay = 0.7f; 
    public void PlayAnim(int seed)
    {
        int rate = 4 - seed;
        StartCoroutine(PlayStar(rate)); 
    }
    private IEnumerator PlayStar(int rate)
    {
        yield return new WaitForSeconds(1f);
        for(int  i = 0 ; i < rate && i < clip.Length ; i++)
        {
            clip[i].SetTrigger("Show");
            yield return new WaitForSeconds(delay); 
        }
    }
    public void OnClose()
    {
        int l = clip.Length;
        for(int i = 0 ;i < l ; i++)
        {
            clip[i].SetTrigger("Close");
        }
    }

}