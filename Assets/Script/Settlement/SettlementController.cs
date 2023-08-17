using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlementController : MonoBehaviour
{
    public float settlementMaxHp = 100f;
    public int settlementLvl = 1;
    public float settlementCurrentExp = 0;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int settlementExpToLevelUp = 50;
    [SerializeField] private ParticleSystem hurtParticles;
    [SerializeField] private ParticleSystem hurtParticles2;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject settlementHpBar;
    [SerializeField] private GameObject settlementExpBar;
    [SerializeField] private TMP_Text settlementLevelText;

    [HideInInspector] public float settlementCurrentHp;
    private float settlementMaxExp;
    // Start is called before the first frame update
    void Start()
    {
        settlementMaxExp = settlementExpToLevelUp;
        settlementCurrentHp = settlementMaxHp;
        SettlementDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayHurtParticles()
    {
        hurtParticles.Play();
        hurtParticles2.Play();
        int leafInt = Random.Range(0, 2);
        if (leafInt == 0 && playerController.lightLeaf > 0)
        {
            playerController.lightLeaf -= Random.Range(1, 4);
            if (playerController.lightLeaf < 0)
            {
                playerController.lightLeaf = 0;
            }
        }
        else if (leafInt == 1 && playerController.darkLeaf > 0)
        {
            playerController.darkLeaf -= Random.Range(1, 4);
            if (playerController.darkLeaf < 0)
            {
                playerController.darkLeaf = 0;
            }
        }
        playerController.UpdateLeafCount();
    }

    public void SettlementDisplay()
    {
        settlementLevelText.text = "Lvl: " + settlementLvl;
        settlementHpBar.transform.localScale = new Vector3((settlementCurrentHp / settlementMaxHp), settlementHpBar.transform.localScale.y, settlementHpBar.transform.localScale.z);
        settlementExpBar.transform.localScale = new Vector3((settlementCurrentExp / settlementMaxExp), settlementExpBar.transform.localScale.y, settlementExpBar.transform.localScale.z);
    }

    public void CheckExpToLevelUp()
    {
        if (settlementCurrentExp >= settlementMaxExp)
        {
            settlementCurrentExp = 0;
            settlementLvl += 1;
            settlementMaxExp *= settlementLvl;
            settlementMaxHp += 100;
            settlementCurrentHp = settlementMaxHp;
            SettlementDisplay();
            spawnManager.SetupRampageEvent();
        }
    }

}
