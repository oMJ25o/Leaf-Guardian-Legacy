using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Enemy
{
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            nearTarget = true;
            enemyAnimator.SetBool("NearTarget", nearTarget);
            Attack();
        }
    }

}
