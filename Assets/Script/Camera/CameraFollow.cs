using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject settlementHudObject;
    [SerializeField] private GameObject settlementHpBar;
    [SerializeField] private AudioClip rampageEventMusic;
    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SettlementController settlementController;
    [SerializeField] private float yOffSet = 13.6f;
    [SerializeField] private float zPos = -10f;
    [SerializeField] private float maxXPosRight;
    [SerializeField] private float maxXPosLeft;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, yOffSet, zPos);
        if (gameObject.transform.position.x > maxXPosRight)
        {
            gameObject.transform.position = new Vector3(maxXPosRight, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x < maxXPosLeft)
        {
            gameObject.transform.position = new Vector3(maxXPosLeft, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        //UpdateSettlementDisplay();
    }

    public void PlayRampageEventMusic()
    {
        audioSource.clip = rampageEventMusic;
        audioSource.Play();
    }

    public void PlayNormalMusic()
    {
        if (audioSource.clip != normalMusic)
        {
            audioSource.clip = normalMusic;
            audioSource.Play();
        }
    }

    public void PlayGameOverMusic()
    {
        audioSource.clip = gameOverMusic;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            settlementHudObject.SetActive(false);
            settlementHpBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            settlementHudObject.SetActive(true);
            settlementHpBar.SetActive(false);
        }
    }
}
