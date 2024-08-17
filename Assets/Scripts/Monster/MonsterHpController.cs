using UnityEngine;
using UnityEngine.UI;

public class MonsterHpController : MonoBehaviour
{
    private int maxHealth = 3;
    public int curHealth;
    public Animator anim;

    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
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
