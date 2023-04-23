using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioClip attackSfx;
    [SerializeField] private TMP_Text lightLeafCountText;
    [SerializeField] private TMP_Text darkLeafCountText;
    [SerializeField] private GameObject attackPointRight;
    [SerializeField] private GameObject attackPointLeft;
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private GameObject settlementShop;

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float rollSpeed = 2f;
    [SerializeField] private float rollLength = 0.5f;
    [SerializeField] private float rollCoolDown = 1f;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 1f;


    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    [HideInInspector] public int attackDamage;
    [HideInInspector] public int lightLeaf;
    [HideInInspector] public int darkLeaf;
    private float newPlayerSpeed;
    private float rollCounter;
    private float rollCoolCounter;
    private float horizontalInput;
    private float startCooldown;

    private bool isAttack = false;
    private bool canAttack = true;

    private Collider2D[] hitEnemies;

    // Start is called before the first frame update
    void Start()
    {
        attackDamage = playerData.attackDamage;
        lightLeaf = playerData.lightleaf;
        darkLeaf = playerData.darkleaf;
        UpdateLeafCount();

        newPlayerSpeed = playerSpeed;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RollPlayer();
        PlayerAttack();
    }

    private void PlayAttackSFX()
    {
        GetComponent<AudioSource>().PlayOneShot(attackSfx);
    }

    public void UpdateLeafCount()
    {
        lightLeafCountText.text = "x" + lightLeaf;
        darkLeafCountText.text = "x" + darkLeaf;
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack && !playerAnimator.GetBool("SpaceBool"))
        {
            canAttack = false;
            isAttack = true;
            playerAnimator.Play("Attack");
            playerAnimator.SetBool("IsAttacking", isAttack);
            startCooldown = attackCooldown;
        }

        if (startCooldown > 0)
        {
            startCooldown -= Time.deltaTime;
        }

        if (startCooldown <= 0)
        {
            canAttack = true;
        }
    }

    private void HitEnemies()
    {
        if (attackPointRight.activeSelf)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.transform.position, attackRange, enemyLayers);
        }
        else if (attackPointLeft.activeSelf)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.transform.position, attackRange, enemyLayers);
        }

        if (hitEnemies.Length >= 1)
        {
            GetComponent<AudioSource>().PlayOneShot(attackSfx);
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void AttackFinish()
    {
        isAttack = false;
        playerAnimator.SetBool("IsAttacking", isAttack);
    }

    private void MovePlayer()
    {
        if (!isAttack)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            switch (horizontalInput)
            {
                case 1:
                    playerAnimator.SetBool("IsMoving", true);
                    attackPointRight.SetActive(true);
                    attackPointLeft.SetActive(false);
                    playerSpriteRenderer.flipX = false;
                    break;
                case -1:
                    playerAnimator.SetBool("IsMoving", true);
                    attackPointLeft.SetActive(true);
                    attackPointRight.SetActive(false);
                    playerSpriteRenderer.flipX = true;
                    break;
                case 0:
                    playerAnimator.SetBool("IsMoving", false);
                    break;
            }

            gameObject.transform.Translate(transform.right * newPlayerSpeed * Time.deltaTime * horizontalInput);
            if (gameObject.transform.position.x < -leftBorder)
            {
                gameObject.transform.position = new Vector3(-leftBorder, transform.position.y, transform.position.z);
            }
            else if (gameObject.transform.position.x > rightBorder)
            {
                gameObject.transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
            }
        }
    }

    private void RollPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack && (horizontalInput > 0 || horizontalInput < 0))
        {
            if (rollCoolCounter <= 0 && rollCounter <= 0)
            {
                newPlayerSpeed = playerSpeed * rollSpeed;
                rollCounter = rollLength;
                GetComponent<Animator>().SetBool("SpaceBool", true);
            }
        }

        if (rollCounter > 0)
        {
            rollCounter -= Time.deltaTime;
        }

        if (rollCoolCounter > 0)
        {
            rollCoolCounter -= Time.deltaTime;
        }
    }

    private void RollPlayerFinish()
    {
        GetComponent<Animator>().SetBool("SpaceBool", false);
        newPlayerSpeed = playerSpeed;
        rollCoolCounter = rollCoolDown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WeaponShop"))
        {
            weaponShop.SetActive(true);
        }
        else if (other.gameObject.CompareTag("SettlementShop"))
        {
            settlementShop.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WeaponShop"))
        {
            weaponShop.SetActive(false);
        }
        else if (other.gameObject.CompareTag("SettlementShop"))
        {
            settlementShop.SetActive(false);
        }

    }

}
