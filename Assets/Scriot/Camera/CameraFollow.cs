using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject settlementHpObject;
    [SerializeField] private GameObject settlementLvlObject;
    [SerializeField] private GameObject settlementHpBar;
    [SerializeField] private AudioClip rampageEventMusic;
    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SettlementController settlementController;
    [SerializeField] private TMP_Text settlementLvlText;
    [SerializeField] private float yOffSet = 5f;
    [SerializeField] private float zPos = -21f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, yOffSet, zPos);
        UpdateSettlementDisplay();
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
        if (other.gameObject.CompareTag("SettlementHealth"))
        {
            settlementHpObject.SetActive(false);
            settlementLvlObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SettlementHealth"))
        {
            settlementHpObject.SetActive(true);
            settlementLvlObject.SetActive(true);
            UpdateSettlementDisplay();
        }
    }

    public void UpdateSettlementDisplay()
    {
        settlementHpBar.transform.localScale = new Vector3((settlementController.settlementCurrentHp / settlementController.settlementMaxHp), settlementHpBar.transform.localScale.y, settlementHpBar.transform.localScale.z);
        settlementLvlText.text = "" + settlementController.settlementLvl;
    }
}
