using System.Collections;
using UnityEngine;

public class SpiderAudioManager : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    public AudioClip footStepSound;
    public AudioClip attackSound;
    public AudioClip deathSound;
    
    private UnityEngine.AnimatorStateInfo stateInfo;
    
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
        GameObject spider = GameObject.FindGameObjectWithTag("Enemy");
        spider.GetComponent<Spider>().enabled = false;
        spider.GetComponent<BaseTracking>().enabled = false;
        spider.GetComponent<BasePatrol>().enabled = false;
        spider.GetComponent<MonsterHpController>().enabled = false;
    }
}