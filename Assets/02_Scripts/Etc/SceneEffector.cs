using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneEffector : MonoBehaviour
{
    public static SceneEffector Instance { get; private set; }

    [Header("# Fade Settings")]
    [SerializeField] private Image _fadeImage;

    private bool _isFading;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void FadeIn(float duration)
    {
        if (_isFading) return;

        StartCoroutine(FadeRoutine(1f, 0f, duration));
    }

    public void FadeOut(float duration)
    {
        if (_isFading) return;

        StartCoroutine(FadeRoutine(0f, 1f, duration));
    }

    private IEnumerator FadeRoutine(float start, float end, float duration)
    {
        _isFading = true;

        Color fadeColor = _fadeImage.color;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(start, end, timer / duration);

            _fadeImage.color = fadeColor;

            yield return null;
        }
        fadeColor.a = end;

        _isFading = false;
    }
}
