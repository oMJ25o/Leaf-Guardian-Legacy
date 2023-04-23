using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip fixSfx;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SettlementController settlementController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayFixSFX()
    {
        audioSource.PlayOneShot(fixSfx);
        settlementController.SettlementDisplay();

    }

    private void FixFinish()
    {
        gameObject.SetActive(false);
    }

}
