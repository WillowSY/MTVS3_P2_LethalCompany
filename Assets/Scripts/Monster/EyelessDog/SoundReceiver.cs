using TMPro;
using UnityEngine;
public class SoundReceiver : MonoBehaviour
{
   public float weight = 0f;              // 소음 세기
   public Animator receiverAnim;          // 해당 
   /* 테스트 씬 용 */
   // public  TextMeshProUGUI noiseText;
   public void Receive(float intensity, Vector3 position)
   {
      //FIXME : 사운드 감지 시 행위 추가
   }

   public void Update()
   {
      // 시간에 따라 소음 웨이트 감소.
      addWeight(-Time.deltaTime);
      /* 테스트 씬 용 */
      //string txt = "Noise : " + weight.ToString();
      //noiseText.text = txt;
   }

   public void addWeight(float delta)
   {
      weight = Mathf.Clamp(weight + delta, 0f, 20f);
      receiverAnim.SetFloat("noise", weight);
      //Debug.Log(weight);
   }
}
