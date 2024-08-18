using System.Collections;
using UnityEngine;

public class DogSoundController : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    public AudioClip breatheSound;
    public AudioClip stepSound;
    public AudioClip growlSound;
    public AudioClip roarSound;
    public AudioClip lungeSound;
    public AudioClip biteSound;

    public GameObject dustEffect;
    public Transform effectSpawnPoint;
    
    private UnityEngine.AnimatorStateInfo stateInfo;

    private int idlePatrolHash = Animator.StringToHash("Base Layer.IdlePatrol");
    private int patrolPauseHash = Animator.StringToHash("Base Layer.PatrolPause");
    private int combatPatrolHash = Animator.StringToHash("Base Layer.CombatPatrol");
    private int attack_Roar = Animator.StringToHash("Base Layer.Attack_Roar");
    private int attack_Tracking = Animator.StringToHash("Base Layer.Attack_Tracking");
    private int attack_Lunge = Animator.StringToHash("Base Layer.Attack_Lunge");

    private bool isBreathing = false;
    private bool isGrowling = false;
    private bool isRoaring = false;
    
    private Coroutine co;

    void Update()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        int currentStateHash = stateInfo.fullPathHash;
        if (currentStateHash == idlePatrolHash || currentStateHash == patrolPauseHash)
        {
            if (!isBreathing)
            {
                isBreathing = true;
                co = StartCoroutine(PlayBreatheSound());
            }
        }
        else if ( currentStateHash == combatPatrolHash)
        {
            if (!isGrowling)
            {
                isGrowling = true;
                co = StartCoroutine(PlayGrowlSound());
            }
        }
        else
        {
            isGrowling = false;
            StopCoroutine(co);
        }
        
    }

    public void PlayLungeSound()
    {
        audioSource.clip = lungeSound;
        audioSource.Play();
        PlaySprintEffect();
    }
    public void PlayRoarSound()
    {
        // audioSource.clip = roarSound;
        // audioSource.Play();
        audioSource.PlayOneShot(roarSound);
    }
    private IEnumerator PlayBreatheSound()
    {
        audioSource.clip = breatheSound;
        audioSource.Play();
        yield return new WaitForSeconds(breatheSound.length + 3f);
    }
    private IEnumerator PlayGrowlSound()
    {
        while (true)
        {
            audioSource.clip = growlSound;
            audioSource.Play();
            yield return new WaitForSeconds(growlSound.length + 3f);
        }
    }
   
    public void PlaySprintEffect()
    {
        if (dustEffect != null && effectSpawnPoint != null)
        {
            GameObject effectInstance = Instantiate(dustEffect, effectSpawnPoint.position, effectSpawnPoint.rotation);

            ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
                //audioSource.clip = stepSound;
                //audioSource.Play();
                //audioSource.PlayOneShot(stepSound);
            }
            Destroy(effectInstance, 2f);
        }
        else
        {
            Debug.LogWarning("DogAudioManager : dustEffect is null");
        }
    }
    
    
}