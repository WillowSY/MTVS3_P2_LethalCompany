using System;
using System.Collections;
using UnityEngine;

public class PresserController : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    public float pressDamage = 100f;
    public StatusController stc;
    public AudioSource audioSource;

    private Vector3 startPosition;
    private Vector3 endPosition;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.up * moveDistance;
        StartCoroutine(Press());
        stc = FindObjectOfType<StatusController>();
        audioSource.Play(4);
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

    IEnumerator PressSound()
    {
        yield return new WaitForSeconds(2);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stc.playerHp -= pressDamage;
        }
    }
}
