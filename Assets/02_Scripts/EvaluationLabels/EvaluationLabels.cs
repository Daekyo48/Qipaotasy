using UnityEngine;
using TMPro;
using System.Collections;

public class TextController : MonoBehaviour
{
    [SerializeField] GameObject perfectObject;
    [SerializeField] GameObject greatObject;
    [SerializeField] GameObject missObject;

    [SerializeField] float fadeSpeed = 1.0f;



    private Coroutine hideCoroutine;
    [SerializeField] private float displayTime = 0.5f;

    void Start()
    {
        perfectObject.SetActive(false);
        greatObject.SetActive(false);
        missObject.SetActive(false);
    }


    public void ShowJudgment(JudgmentType judgment)
    {

        perfectObject.SetActive(false);
        greatObject.SetActive(false);
        missObject.SetActive(false);

        switch (judgment)
        {
            case JudgmentType.Perfect:
                perfectObject.SetActive(true);
                break;

            case JudgmentType.Great:
                greatObject.SetActive(true);
                break;

            case JudgmentType.Miss:
                missObject.SetActive(true);
                break;

            default:
                return;
        }

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideJudgment());
    }

    private IEnumerator HideJudgment()
    {
        yield return new WaitForSeconds(displayTime);

        perfectObject.SetActive(false);
        greatObject.SetActive(false);
        missObject.SetActive(false);

        hideCoroutine = null;
    }
}