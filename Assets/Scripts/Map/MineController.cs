using System;
using System.Collections;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float exploseDamage = 100f;
    public float lightOnRange;
    public Light pointLight;
    public Player player;
    public StatusController stc;
    public AudioSource audioSource;
    public bool isPlaying;
    public Coroutine co;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        stc = FindObjectOfType<StatusController>();
    }

    
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < lightOnRange)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                pointLight.intensity = 1f;
                co = StartCoroutine(PlayMineSound());
            }
        }
        else
        {
            pointLight.intensity = 0f;
            isPlaying = false;
            if (co != null)
            {
                StopCoroutine(co);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Explose());
            
        }
    }

    IEnumerator Explose()
    {
        yield return new WaitForSeconds(2);
        stc.playerHp -= exploseDamage;
    }

    IEnumerator PlayMineSound()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(4);
        }
    }
}
