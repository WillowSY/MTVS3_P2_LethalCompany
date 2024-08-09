using System;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float exploseDamage = 100f;
    public float lightOnRange;
    public Light pointLight;
    public Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < lightOnRange)
        {
            pointLight.intensity = 1f;
        }
        else
        {
            pointLight.intensity = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //vfx효과를 instantiate시키고 캐릭터의 체력 - exploseDamage 하는코드를 작성
        }
    }
}
