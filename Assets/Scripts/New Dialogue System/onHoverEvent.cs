using UnityEngine;
using UnityEngine.EventSystems;

public class onHoverEvent : MonoBehaviour
{
    [SerializeField] 
    private GameObject uiToDisplay;

    public float scalePopDuration = 0.3f; //time for the scale tween
    private RectTransform uiRectTransform;
    private Coroutine currentTween;

    public AudioClip adios;

    private void Awake()
    {
        // Cache the RectTransform of the UI component to display
        if (uiToDisplay != null)
        {
            uiRectTransform = uiToDisplay.GetComponent<RectTransform>();
            uiToDisplay.SetActive(false); //hidden at the start
        }
    }

    public void OnPointerEnter()
    {
        if (currentTween != null) StopCoroutine(currentTween);
        uiToDisplay.SetActive(true);
        uiRectTransform.localScale = Vector3.zero; // Set initial scale
        currentTween = StartCoroutine(ScaleTween(uiRectTransform, Vector3.zero, Vector3.one, scalePopDuration));

        AudioSource.PlayClipAtPoint(adios, transform.position);
    }

    public void OnPointerExit()
    {
        if (currentTween != null) StopCoroutine(currentTween);
        currentTween = StartCoroutine(ScaleTween(uiRectTransform, uiRectTransform.localScale, Vector3.zero, scalePopDuration, () =>
        {
            uiToDisplay.SetActive(false);
        }));
    }
    private System.Collections.IEnumerator ScaleTween(RectTransform target, Vector3 startScale, Vector3 endScale, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = endScale;

        onComplete?.Invoke();
    }
}
