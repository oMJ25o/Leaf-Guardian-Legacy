using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected GameObject enemyHpBar;
    [HideInInspector] public float currentHp;
    [HideInInspector] public float maxHp;
    [HideInInspector] public int attackDamage;
    protected float moveSpeed;
    [HideInInspector] public int exp;

    protected float attackCooldown = 1.5f;
    protected bool nearTarget = false;
    protected Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        SetupEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    public void TakeDamage(int damageTaken)
    {
        enemyAnimator.Play("TakeDamage");
        currentHp -= damageTaken;
        UpdateHealthBar();
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        enemyHpBar.transform.localScale = new Vector3((currentHp / maxHp), enemyHpBar.transform.localScale.y, enemyHpBar.transform.localScale.z);
    }

    private void SetupEnemy()
    {
        currentHp = enemyData.hp;
        maxHp = enemyData.hp;
        attackDamage = enemyData.attackDamage;
        moveSpeed = enemyData.moveSpeed;
        exp = enemyData.exp;
        enemyAnimator = GetComponent<Animator>();
    }

    private void MoveEnemy()
    {
        if (!nearTarget)
        {
            gameObject.transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        enemyAnimator.Play("Attack");
        Invoke("Attack", attackCooldown);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            nearTarget = true;
            enemyAnimator.SetBool("NearTarget", nearTarget);
            Attack();
        }
    }


}
