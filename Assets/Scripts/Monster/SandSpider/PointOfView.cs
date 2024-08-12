using UnityEngine;

public class PointOfView : MonoBehaviour
{
    public Animator anim;
    
    /* 거미줄과 플레이어 콜라이더 충돌 시 isDetected 활성화 */
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spider : POV Trigger");
            anim.SetBool("isDetected", true);
        }
    }
}