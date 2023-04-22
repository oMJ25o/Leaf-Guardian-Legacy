using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float rollSpeed = 2f;
    [SerializeField] private float rollLength = 0.5f;
    [SerializeField] private float rollCoolDown = 1f;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    [SerializeField] private float attackCooldown = 2f;


    private Animator playerAnimator;

    private float newPlayerSpeed;
    private float rollCounter;
    private float rollCoolCounter;
    private float horizontalInput;
    private float startCooldown;

    private bool isAttack = false;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        newPlayerSpeed = playerSpeed;

        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RollPlayer();
        PlayerAttack();
    }

    private void PlayerAttack()
    {
        Debug.Log(canAttack);
        if (Input.GetMouseButtonDown(0) && canAttack)
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
            Debug.Log("Can Attack");
            canAttack = true;
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
            GetComponent<Animator>().SetFloat("Horizontal", horizontalInput);
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
        if (Input.GetKeyDown(KeyCode.Space) && (horizontalInput > 0 || horizontalInput < 0))
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
        Debug.Log("Particle Triggered!");
    }

}
