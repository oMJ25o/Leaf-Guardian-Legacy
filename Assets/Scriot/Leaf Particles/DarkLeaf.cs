using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLeaf : LeafController
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
        if (other.CompareTag("Player"))
        {
            playerController.darkLeaf += 1;
            base.OnParticleCollision(other);
        }
    }

}
