using System;
using System.Collections;
using UnityEngine;

public class PresserController : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveDuration = 1f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.up * moveDistance;
        StartCoroutine(Press());
    }

    IEnumerator Press()
    {
        while (true)
        {
            float pressedTime = 0f;
            while (pressedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, pressedTime / moveDuration);
                pressedTime += Time.deltaTime;
                yield return null;
            }

            pressedTime = 0f;
            while (pressedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(endPosition, startPosition, pressedTime / moveDuration);
                pressedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player의 hp - pressDamage , player의 사망상태에 접근
        }
    }
}
