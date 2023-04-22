using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject enemyHpBar;
    [HideInInspector] public float currentHp;
    [HideInInspector] public float maxHp;
    [HideInInspector] public int attackDamage;
    private float moveSpeed;
    [HideInInspector] public int exp;

    private float attackCooldown = 1.5f;
    private bool nearTarget = false;
    private Animator enemyAnimator;
    private SettlementController settlementController;

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

    private void SetupEnemy()
    {
        currentHp = enemyData.hp * settlementController.settlementLvl;
        maxHp = enemyData.hp * settlementController.settlementLvl;
        attackDamage = enemyData.attackDamage * settlementController.settlementLvl;
        moveSpeed = enemyData.moveSpeed;
        exp = enemyData.exp;
        enemyAnimator = GetComponent<Animator>();
        settlementController = GameObject.Find("Tower Tile Set").GetComponent<SettlementController>();
    }

    public void TakeDamage(int damageTaken)
    {
        enemyAnimator.Play("TakeDamage");
        currentHp -= damageTaken;
        UpdateHealthBar();
        if (currentHp <= 0)
        {
            settlementController.settlementCurrentExp += exp;
            settlementController.SettlementDisplay();
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        enemyHpBar.transform.localScale = new Vector3((currentHp / maxHp), enemyHpBar.transform.localScale.y, enemyHpBar.transform.localScale.z);
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
        settlementController.settlementCurrentHp -= attackDamage;
        settlementController.SettlementDisplay();
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
