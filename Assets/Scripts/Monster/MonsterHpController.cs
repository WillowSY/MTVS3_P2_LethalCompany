using UnityEngine;
using UnityEngine.UI;

public class MonsterHpController : MonoBehaviour
{
    private int maxHealth = 5;
    public int curHealth;

    public Slider HpSlider;
    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        HpSlider.value = (float)curHealth / maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
}
