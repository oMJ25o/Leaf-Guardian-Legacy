using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private GameObject fireParticlesObject;
    [SerializeField] private Toggle fireToggle;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject hudOptions;

    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        SetupVolume();
        SetupFireToggle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
        hudOptions.SetActive(false);
    }

    public void SetupFireToggle()
    {
        fireParticlesObject.SetActive(fireToggle.isOn);
    }

    public void ToggleFire()
    {
        fireParticlesObject.SetActive(fireToggle.isOn);
    }

    public void SetupVolume()
    {
        audioMixer.GetFloat("Volume", out volume);
        audioSlider.value = volume;
    }

    public void ChangeVolume()
    {
        audioMixer.SetFloat("Volume", audioSlider.value);
    }

}
