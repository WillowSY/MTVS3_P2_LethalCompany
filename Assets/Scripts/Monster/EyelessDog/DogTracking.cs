using UnityEngine;
using UnityEngine.AI;

public class DogTracking :MonoBehaviour
{
    public bool isAlertedSoundReady = true;
    
    public void FreezeVelocity(Rigidbody rigid)
    {
        rigid.linearVelocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    // FIXME : 소음 감지 시 소음발생 방향으로 천천히 돌리는 거 필요 없으면 제거.
    // FIXME: 대기 & 코루틴 구현해야하는지 판단 후 수정하기
    //IEnumerator TurnToPlayer();
    public void TurnToPlayer(NavMeshAgent nav, Transform target, AudioSource audioSource, AudioClip alertSound, Vector3 followPosition)
    {
        //transform.LookAt(target);
        if (isAlertedSoundReady)
        {
            Snarl(audioSource, alertSound);
        }
        // localRotate = transform.InverseTransformPoint(target.position);
        // angle = Mathf.Atan2(localRotate.x, localRotate.z) * Mathf.Rad2Deg;
        // float maxRotation = turnSpeed * Time.deltaTime;
        // float turnAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);
        // transform.Rotate(0, turnAngle, 0);
        Debug.Log(followPosition);
        nav.SetDestination(followPosition);
        //return angle;
        // yield return new WaitforSeocnds(2); 

        
    }

    public void Snarl(AudioSource audioSource, AudioClip alertSound)
    {
        audioSource.PlayOneShot(alertSound);
        isAlertedSoundReady = false;
    }
    
    // public override void Pause()
    // {
    //     FreezeVelocity();
    // }
}
