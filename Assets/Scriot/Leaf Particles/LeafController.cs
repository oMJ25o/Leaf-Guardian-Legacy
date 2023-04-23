using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeafController : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected AudioClip collectSfx;
    [SerializeField] protected AudioSource audioSource;

    // Start is called before the first frame update

    protected virtual void SetupLeaf()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        audioSource.PlayOneShot(collectSfx);
        playerController.UpdateLeafCount();
    }
}
