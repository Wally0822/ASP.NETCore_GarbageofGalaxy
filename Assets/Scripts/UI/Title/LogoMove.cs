using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogoMove : MonoBehaviour
{
    [SerializeField] RawImage logoRI = null;
    [SerializeField] GameObject accountUI = null;

    private float moveDistance = 50f;
    private float moveDuration = 0.8f;

    private void Awake()
    {
        logoRI.rectTransform.anchoredPosition = Vector2.zero;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float elapsedTime = 0;
        Vector2 startingPosition = logoRI.rectTransform.anchoredPosition;

        Vector2 targetPositionDown = startingPosition - new Vector2(0, moveDistance);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            logoRI.rectTransform.anchoredPosition = Vector2.Lerp(startingPosition, targetPositionDown, elapsedTime / moveDuration);
            yield return null;
        }

        logoRI.rectTransform.anchoredPosition = targetPositionDown;

        yield return new WaitForSeconds(0.2f);

        elapsedTime = 0;
        Vector2 targetPositionUp = startingPosition + new Vector2(0, moveDistance);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            logoRI.rectTransform.anchoredPosition = Vector2.Lerp(targetPositionDown, targetPositionUp, elapsedTime / moveDuration);
            yield return null;
        }

        logoRI.rectTransform.anchoredPosition = targetPositionUp;

        this.gameObject.SetActive(false);
        accountUI.gameObject.SetActive(true);
    }
}