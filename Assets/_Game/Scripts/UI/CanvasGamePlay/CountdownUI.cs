using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
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
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);

        IsFinished = true;
        countdownCoroutine = null;
        finishCallback?.Invoke();
        gameObject.SetActive(false);
    }
}
