using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioClip attackSfx;
    [SerializeField] private Animator attackFXRight;
    [SerializeField] private Animator attackFXLeft;
    [SerializeField] private TMP_Text playerHPText;
    [SerializeField] private TMP_Text leafCountText;
    [SerializeField] private GameObject attackPointRight;
    [SerializeField] private GameObject attackPointLeft;
    [SerializeField] private GameObject houseSign1;
    [SerializeField] private GameObject houseSign2;
    [SerializeField] private GameObject houseSign3;
    [SerializeField] private GameObject playerUpgradeSign;
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

    [HideInInspector] public int playerCurrentHP;
    [HideInInspector] public int playerMaxHP;
    [HideInInspector] public int attackDamage;
    [HideInInspector] public int leaf;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isAttackingRight = false;
    [HideInInspector] public bool isAttackingLeft = false;
    private float newPlayerSpeed;
    private float rollCounter;
    private float rollCoolCounter;
    private float horizontalInput;
    private float startCooldown;

    private bool isAttack = false;
    private bool canAttack = true;
    private bool isInsideSettlement = true;

    private Collider2D[] hitEnemies;
    private Animator trueAttackFX;

    // Start is called before the first frame update
    void Start()
    {
        attackDamage = playerData.attackDamage;
        leaf = playerData.leaf;
        playerCurrentHP = playerData.maxHP;
        playerMaxHP = playerData.maxHP;
        UpdateLeafCount();
        UpdateHP();

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

        /*
        if (GameManager.Instance.isGameOver)
        {
            weaponShop.SetActive(false);
            weaponShop.SetActive(false);
        }*/
    }

    private void PlayAttackSFX()
    {
        GetComponent<AudioSource>().PlayOneShot(attackSfx);
    }

    public void UpdateHP()
    {
        playerHPText.text = playerCurrentHP + "/" + playerMaxHP;
    }

    public void UpdateLeafCount()
    {
        leafCountText.text = leaf.ToString();
    }

    public void PlayerAttack()
    {
        if (isAttacking && canAttack && !playerAnimator.GetBool("SpaceBool") /*&& !isInsideSettlement && !GameManager.Instance.isGameOver*/)
        {
            canAttack = false;
            isAttack = true;

            if (isAttackingRight && playerSpriteRenderer.flipX == true)
            {
                PlayerFaceRight();
            }
            else if (isAttackingLeft && playerSpriteRenderer.flipX == false)
            {
                PlayerFaceLeft();
            }

            playerAnimator.Play("Attack");
            playerAnimator.SetBool("IsAttacking", isAttack);
            startCooldown = attackCooldown;
        }

        isAttacking = false;

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
            trueAttackFX = attackFXRight;
            hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.transform.position, attackRange, enemyLayers);
        }
        else if (attackPointLeft.activeSelf)
        {
            trueAttackFX = attackFXLeft;
            hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.transform.position, attackRange, enemyLayers);
        }

        if (hitEnemies.Length >= 1)
        {
            trueAttackFX.Play("AttackFX");
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
        isAttacking = false;
        isAttackingRight = false;
        isAttackingLeft = false;
        playerAnimator.SetBool("IsAttacking", isAttack);
    }

    private void MovePlayer()
    {
        if (!isAttack /*&& !GameManager.Instance.isGameOver*/)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            switch (horizontalInput)
            {
                case 1:
                    playerAnimator.SetBool("IsMoving", true);
                    PlayerFaceRight();
                    break;
                case -1:
                    playerAnimator.SetBool("IsMoving", true);
                    PlayerFaceLeft();
                    break;
                case 0:
                    playerAnimator.SetBool("IsMoving", false);
                    break;
            }

            gameObject.transform.Translate(transform.right * newPlayerSpeed * Time.deltaTime * horizontalInput);
            /*if (gameObject.transform.position.x < -leftBorder)
            {
                gameObject.transform.position = new Vector3(-leftBorder, transform.position.y, transform.position.z);
            }
            else if (gameObject.transform.position.x > rightBorder)
            {
                gameObject.transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
            }*/
        }
    }

    private void PlayerFaceRight()
    {
        attackPointRight.SetActive(true);
        attackPointLeft.SetActive(false);
        playerSpriteRenderer.flipX = false;
    }

    private void PlayerFaceLeft()
    {
        attackPointLeft.SetActive(true);
        attackPointRight.SetActive(false);
        playerSpriteRenderer.flipX = true;
    }

    private void RollPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack && (horizontalInput > 0 || horizontalInput < 0) /*&& !GameManager.Instance.isGameOver*/)
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
        if (other.gameObject.CompareTag("BuildSign"))
        {
            switch (other.name)
            {
                case "HouseSignCollider1":
                    houseSign1.SetActive(true);
                    break;
                case "HouseSignCollider2":
                    houseSign2.SetActive(true);
                    break;
                case "HouseSignCollider3":
                    houseSign3.SetActive(true);
                    break;
                case "PlayerUpgradeSignCollider":
                    playerUpgradeSign.SetActive(true);
                    break;
            }
        }
        else if (other.gameObject.CompareTag("WeaponShop") && !GameManager.Instance.isGameOver)
        {
            weaponShop.SetActive(true);
        }
        else if (other.gameObject.CompareTag("SettlementShop") && !GameManager.Instance.isGameOver)
        {
            settlementShop.SetActive(true);
        }
        else if (other.gameObject.CompareTag("InsideSettlement") && !GameManager.Instance.isGameOver)
        {
            isInsideSettlement = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BuildSign"))
        {
            switch (other.name)
            {
                case "HouseSignCollider1":
                    houseSign1.SetActive(false);
                    break;
                case "HouseSignCollider2":
                    houseSign2.SetActive(false);
                    break;
                case "HouseSignCollider3":
                    houseSign3.SetActive(false);
                    break;
                case "PlayerUpgradeSignCollider":
                    playerUpgradeSign.SetActive(false);
                    break;
            }
        }
        else if (other.gameObject.CompareTag("WeaponShop"))
        {
            weaponShop.SetActive(false);
        }
        else if (other.gameObject.CompareTag("SettlementShop"))
        {
            settlementShop.SetActive(false);
        }
        else if (other.gameObject.CompareTag("InsideSettlement"))
        {
            isInsideSettlement = false;
        }

    }

}
