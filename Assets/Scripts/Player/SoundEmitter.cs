using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEmitter : MonoBehaviour
{
    public float soundIntensity;
    public float soundAttenuation;
    public GameObject emitterObject;
    public Dictionary<int, SoundReceiver> receiverDic;
    private AudioSource footstepSource;
    
    public AudioClip footstepSound; // 발자국 AudioClip

    [SerializeField]
    private AudioClip dropItemSound; //아이템 드롭사운드

    [SerializeField] 
    private AudioClip scanSound; //스캔사운드 

    [SerializeField] 
    private AudioClip hitShovel; //공격 사운드

    [SerializeField] 
    private AudioClip playerDamaged; //플레이어 데미지 입는 사운드
    
    void Start()
    {
        footstepSource = gameObject.GetComponent<AudioSource>();
        receiverDic = new Dictionary<int, SoundReceiver>();
        if (emitterObject == null)
            emitterObject = gameObject;
    }

    public void Update()    
    {
        Emit();
    }
    
    public void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Emitter : Receiver is In");
        SoundReceiver receiver;
        receiver = coll.gameObject.GetComponent<SoundReceiver>();
        if (receiver == null)       // receiver가 존재하지 않으면 청각감지 없는 컴포넌트
            return;
        int objId = coll.gameObject.GetInstanceID();
        receiverDic.Add(objId, receiver);
        
    }
    
    public void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("SoundReceiver"))
        {
            Debug.Log("Emitter: Receiver is Out");
            SoundReceiver receiver;
            receiver = coll.gameObject.GetComponent<SoundReceiver>();
            if (receiver == null)   // receiver가 존재하지 않으면 청각감지 덦는 컴포넌트
                return;
            int objId = coll.gameObject.GetInstanceID();
            receiverDic.Remove(objId);
        }
    }
    
    public void Emit()
    {
        GameObject srObj;
        Vector3 srPos;
        float intensity;
        float distance;
        Vector3 emitterPos = emitterObject.transform.position;

        if (receiverDic != null)
        {
            foreach (SoundReceiver sr in receiverDic.Values)
            {
                srObj = sr.gameObject;
                srPos = srObj.transform.position;
                distance = Vector3.Distance(srPos, emitterPos);
                intensity = soundIntensity;
                intensity -= soundAttenuation * distance;
            }
        }
    }

    /* 플레이어 관련 소리 재생 */
    public void PlayFootStepSound()
    {
        //Debug.Log("PlaySound");
        footstepSource.PlayOneShot(footstepSound);      // 플레이어 발자국 소리 재생
        if (receiverDic!=null)
        {
            foreach (SoundReceiver sr in receiverDic.Values)
            {
                sr.addWeight(1f);       // SoundReceiver 소리 세기 증가
            }
        }
    }

    public void PlayDropItem()
    {
        footstepSource.PlayOneShot(dropItemSound);
        if (receiverDic!=null)
        {
            foreach (SoundReceiver sr in receiverDic.Values)
            {
                sr.addWeight(2f);       // SoundReceiver 소리 세기 증가
            }
        }
    }

    public void PlayAttackSound()
    {
        footstepSource.PlayOneShot(hitShovel);
    }

    public void PlayScanSound()
    {
        footstepSource.PlayOneShot(scanSound);
    }

    public void PlayDamaged()
    {
        footstepSource.PlayOneShot(playerDamaged);
    }
}
