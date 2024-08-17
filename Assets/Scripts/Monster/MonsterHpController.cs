using UnityEngine;
using UnityEngine.UI;

public class MonsterHpController : MonoBehaviour
{
    private int maxHealth = 3;
    public int curHealth;
    public Animator anim;

    public Slider HpSlider;
    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        HpSlider.value = (float) curHealth / maxHealth;
        if (curHealth == 0)
        {
            anim.SetBool("isDead", true);
        }
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
}
