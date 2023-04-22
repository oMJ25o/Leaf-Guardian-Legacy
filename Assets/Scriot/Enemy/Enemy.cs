using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    protected int hp;
    protected int attackDamage;
    protected float moveSpeed;
    protected int exp;

    protected float attackCooldown = 1.5f;
    protected bool nearTarget = false;
    protected Animator enemyAnimator;

    protected void SetupEnemy()
    {
        hp = enemyData.hp;
        attackDamage = enemyData.attackDamage;
        moveSpeed = enemyData.moveSpeed;
        exp = enemyData.exp;

        enemyAnimator = GetComponent<Animator>();
    }

    protected virtual void MoveEnemy()
    {
        if (!nearTarget)
        {
            gameObject.transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
        }
    }

    protected virtual void Attack()
    {
        enemyAnimator.Play("Attack");
        Invoke("Attack", attackCooldown);
    }


}
