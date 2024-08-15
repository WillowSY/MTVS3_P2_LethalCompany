using System.Collections;
using UnityEngine;

public class DogSoundController : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    public AudioClip breatheSound;
    public AudioClip walkSound;
    public AudioClip sprintSound;
    public AudioClip growlSound;
    public AudioClip roarSound;
    public AudioClip lungeSound;
    public AudioClip biteSound;

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
    
    void Start()
    {
        //anim = GetComponent<Animator>();
        //growlSound = Resources.Load<AudioClip>("Audio/Monster/Dog/BaseRoar");
        if (growlSound == null)
        {
            Debug.LogError("growlSound"+"이 존재하지 않습니다");
        }
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        //audioSource.volume = 0.3f;
        audioSource.playOnAwake = false;
        Debug.Log("AudioManager Start");
        // RoarSound 재생
        StartCoroutine(PlayRoarSound());
    }

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
            Debug.Log("State is On Growl");
            if (!isGrowling)
            {
                isGrowling = true;
                co = StartCoroutine(PlayGrowlSound());
            }
        }
        else if (currentStateHash == attack_Roar)
        {
            if (!isRoaring)
            {
                isRoaring = true;
            }
        }
        else
        {
            isGrowling = false;
            StopCoroutine(co);
        }
        Debug.Log("isGrowl " + isGrowling);
        
    }

    private IEnumerator PlayRoarSound()
    {
        yield return new WaitUntil(()=>isRoaring);
        isRoaring = false;
        audioSource.Stop();
        audioSource.clip = roarSound;
        audioSource.Play();
        yield return new WaitForSeconds(roarSound.length);
    }
    private IEnumerator PlayBreatheSound()
    {
        audioSource.PlayOneShot(breatheSound);
        yield return new WaitForSeconds(breatheSound.length + 3f);
    }
    private IEnumerator PlayGrowlSound()
    {
        while (true)
        {
            Debug.Log("Play Sound");
            audioSource.clip = growlSound;
            audioSource.Play();
            yield return new WaitForSeconds(growlSound.length + 3f);
        }
    }
    private IEnumerator PlayWalkSound()
    {
        audioSource.PlayOneShot(walkSound);
        yield return new WaitForSeconds(audioSource.clip.length);
    }
    
    // IEnumerator PlaySprintSound()
    // {
    //     audioSource.PlayOneShot(sprintSound);
    //     yield return new WaitForSeconds(audioSource.clip.length);
    // }
    //
    // IEnumerator PlayRoarSound()
    // {
    //     audioSource.PlayOneShot(roarSound);
    //     yield return new WaitForSeconds(audioSource.clip.length);
    // }
    //
    // IEnumerator PlayLungeSound()
    // {
    //     audioSource.PlayOneShot(lungeSound);
    //     yield return new WaitForSeconds(audioSource.clip.length);
    // }
    //
    // IEnumerator PlayBiteSound()
    // {
    //     audioSource.PlayOneShot(biteSound);
    //     yield return new WaitForSeconds(audioSource.clip.length);
    // }
}