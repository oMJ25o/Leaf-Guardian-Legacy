using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrant : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetupEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void MoveEnemy()
    {
        throw new System.NotImplementedException();
    }
}
