using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private CanvasGroup canvasGroup;

    private Coroutine countdownCoroutine;
    private System.Action finishCallback;

    public bool IsFinished { get; private set; }

    public void PlayCountdown(System.Action onFinished)
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        finishCallback = onFinished;
        countdownCoroutine = StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        IsFinished = false;
    
        gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);

        yield return AnimateText("3");
        yield return AnimateText("2");
        yield return AnimateText("1");
        yield return AnimateText("GO!", 0.8f);

        countdownText.gameObject.SetActive(false);

        IsFinished = true;
        countdownCoroutine = null;

        finishCallback?.Invoke();

        gameObject.SetActive(false);
    }

  private IEnumerator AnimateText(string value, float duration = 0.6f)
    {

    countdownText.text = value;

    countdownText.transform.localScale = Vector3.one * 0.4f;
    canvasGroup.alpha = 1f;

    float time = 0f;

    Vector3 endScale = value == "GO!"
        ? Vector3.one * 1.5f
        : Vector3.one * 1.2f;

    while (time < duration)
    {
        time += Time.deltaTime;
        float t = time / duration;

        // Ease out 
        float ease = 1f - Mathf.Pow(1f - t, 3f);

        // Scale
        countdownText.transform.localScale =
            Vector3.Lerp(Vector3.one * 0.4f, endScale, ease);

        // Fade 
        canvasGroup.alpha = t < 0.7f
            ? 1f
            : Mathf.Lerp(1f, 0f, (t - 0.7f) / 0.3f);

        yield return null;
    }

    canvasGroup.alpha = 0f;

    yield return new WaitForSeconds(0.05f);
    }
}