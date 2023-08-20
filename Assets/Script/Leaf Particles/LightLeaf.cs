using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaf : LeafController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.isGameOver)
        {
            playerController.leaf += 1;
            base.OnParticleCollision(other);
        }
    }
}
