using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject enemyHpBar;
    [SerializeField] private AudioClip attackSfx;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isDrop;

    [HideInInspector] public float currentHp;
    [HideInInspector] public float maxHp;
    [HideInInspector] public int attackDamage;
    private float moveSpeed;
    [HideInInspector] public int exp;

    private float attackCooldown = 1.5f;
    private bool nearTarget = false;
    private Animator enemyAnimator;
    private SettlementController settlementController;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        SetupEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyAnimator.GetBool("Dead"))
        {
            MoveEnemy();
        }
    }

    private void SetupEnemy()
    {
        settlementController = GameObject.Find("SettlementManager").GetComponent<SettlementController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        maxHp = enemyData.hp;
        currentHp = maxHp;
        attackDamage = enemyData.attackDamage;
        moveSpeed = enemyData.moveSpeed;
        exp = enemyData.exp;
        enemyAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageTaken)
    {
        if (!enemyAnimator.GetBool("Dead"))
        {
            enemyAnimator.Play("TakeDamage");
            currentHp -= damageTaken;
            if (currentHp <= 0)
            {
                currentHp = 0;
                enemyAnimator.SetBool("Dead", true);
            }
            UpdateHealthBar();
        }
    }

    public void EnemyDespawn()
    {
        if (isDrop)
        {
            playerController.leaf += 1;
            playerController.UpdateLeafCount();
        }
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        enemyHpBar.transform.localScale = new Vector3((currentHp / maxHp), enemyHpBar.transform.localScale.y, enemyHpBar.transform.localScale.z);
    }

    private void MoveEnemy()
    {
        if (!nearTarget && !enemyAnimator.GetBool("Dead"))
        {
            gameObject.transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (!GameManager.Instance.isGameOver && !enemyAnimator.GetBool("Dead"))
        {
            enemyAnimator.Play("Attack");
            Invoke("Attack", attackCooldown);
        }
    }

    private void PlayAttackSFX()
    {
        audioSource.PlayOneShot(attackSfx);
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
