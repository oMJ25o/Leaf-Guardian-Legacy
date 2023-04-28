using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject nextTitle;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject optionButton;
    [SerializeField] private AudioClip explosionSFX;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartParticles()
    {
        audioSource.PlayOneShot(explosionSFX);
        particles.SetActive(true);
    }

    private void StartNextTitle()
    {
        nextTitle.SetActive(true);
        if (gameObject.name == "Legacy")
        {
            startButton.SetActive(true);
            optionButton.SetActive(true);
        }
    }


}
