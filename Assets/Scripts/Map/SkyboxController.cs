using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material skyboxMaterial;
    public float blendDuration = 10f;

    private float timeElapsed = 0f;
    private bool isBlending = false;
    public float blendSpeed = 0.005f;  // 블렌딩 속도
    private float blendValue = 0.0f; // 현재 블렌딩 값

    
    
    // Update is called once per frame
    /*void Update()
    {
        if (isBlending)
        {
            timeElapsed += Time.deltaTime;
            float blendValue = Mathf.Clamp01(timeElapsed / blendDuration);
            skyboxMaterial.SetFloat("SkyBoxMaterial", blendValue);

            if (timeElapsed >= blendDuration)
            {
                isBlending = false;
            }
        }
    } */

    public void StartBlending()
    {
        blendValue += Time.deltaTime * blendSpeed;
        blendValue = Mathf.PingPong(blendValue, 1.0f); // 0과 1 사이로 제한

        // 머티리얼의 블렌딩 속성 업데이트
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_Blend", blendValue);
        }
    }
}
