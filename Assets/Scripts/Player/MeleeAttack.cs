using System;
using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator animator;
    public Inventory inventory;
    public PlayerRaycast playerRaycast;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public string enemyTag = "Enemy"; //몬스터에 태그 Enemy
    
    private bool _isAttacking = false;
    
    void Update()
    {
        if (inventory.scraps[playerRaycast.currentQuickSlot] != null &&
            inventory.scraps[playerRaycast.currentQuickSlot].IsShovel)
        {
            if (Input.GetMouseButtonDown(0) && !_isAttacking)
            {
                StartCoroutine(AttackDecision());
                animator.SetTrigger("attackShovel");
                Invoke("Attack",0.9f);
                Debug.Log("attack");
            }
        }
    }
    
    private void Attack()
    {
        // 공격 판정
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                MonsterHpController enemyComponent = enemy.GetComponent<MonsterHpController>();
                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.Log("Enemy 컴포넌트가 없습니다!");
                }
            }
        }
    }

    IEnumerator AttackDecision()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
    }
    

    // 공격 범위 시각화 (디버깅 용도)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}