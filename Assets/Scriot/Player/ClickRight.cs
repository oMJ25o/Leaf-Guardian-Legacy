using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRight : PlayerClick
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnMouseDown()
    {
        playerController.isAttackingRight = true;
        playerController.isAttackingLeft = false;
        playerController.isAttacking = true;
        playerController.PlayerAttack();
    }

}
