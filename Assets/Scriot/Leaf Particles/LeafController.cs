using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeafController : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;

    // Start is called before the first frame update



    protected virtual void OnParticleCollision(GameObject other)
    {
        playerController.UpdateLeafCount();
    }
}
