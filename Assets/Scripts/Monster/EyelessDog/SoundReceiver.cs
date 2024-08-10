using UnityEngine;

public class SoundReceiver : MonoBehaviour
{
   public float weight = 0f;
   public float alertThreshold = 5f;
   public float attackThreshold = 10f;
   public Animator receiverAnim;
   public void Receive(float intensity, Vector3 position)
   {
      //TODO : 사운드 감지 시 행위
      //Debug.Log("사운드 감지");
   }

   public void Update()
   {
      addWeight(-Time.deltaTime);
   }

   // public bool IsDetected()
   // {
   //    if (weight > alertThreshold && weight < attackThreshold)
   //    {
   //       return true;
   //    }
   //    else
   //    {
   //       return false;
   //    }
   // }
   // public bool IsSprint()
   // {
   //    if (weight > attackThreshold)
   //    {
   //       return true;
   //    }
   //    else
   //    {
   //       return false;
   //    }
   // }
   //
   // public bool IsPatorl()
   // {
   //    if (weight < alertThreshold)
   //    {
   //       return true;
   //    }
   //    else
   //    {
   //       return false;
   //    }
   // }

   public void addWeight(float delta)
   {
      weight = Mathf.Clamp(weight + delta, 0f, 20f);
      receiverAnim.SetFloat("noise", weight);
      //Debug.Log(weight);
   }
}
