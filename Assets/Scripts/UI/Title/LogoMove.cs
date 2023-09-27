using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogoMove : MonoBehaviour
{
    //[SerializeField] GameObject startPanel = null;
    [SerializeField] GameObject accountUI = null;
    [SerializeField] RawImage _logoRI = null;

    private float moveDistance = 50f;
    private float moveDuration = 0.8f;

    private void Awake()
    {
        _logoRI = _logoRI.GetComponent<RawImage>();
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        float elapsedTime = 0;
        Vector3 startingPosition = _logoRI.rectTransform.position;

        Vector3 targetPositionDown = startingPosition - new Vector3(0, moveDistance, 0);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            _logoRI.rectTransform.position = Vector3.Lerp(startingPosition, targetPositionDown, elapsedTime / moveDuration);
            yield return null;
        }

        _logoRI.rectTransform.position = targetPositionDown;

        yield return new WaitForSeconds(0.2f);

        elapsedTime = 0;
        Vector3 targetPositionUp = startingPosition + new Vector3(0, moveDistance, 0);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            _logoRI.rectTransform.position = Vector3.Lerp(targetPositionDown, targetPositionUp, elapsedTime / moveDuration);
            yield return null;
        }

        _logoRI.rectTransform.position = targetPositionUp;

        this.gameObject.SetActive(false);
        accountUI.gameObject.SetActive(true);
    }
}