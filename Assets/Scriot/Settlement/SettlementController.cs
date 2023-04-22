using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlementController : MonoBehaviour
{
    public float settlementMaxHp = 100f;
    public int settlementLvl = 1;
    public float settlementCurrentExp = 0;
    [SerializeField] private int settlementExpToLevelUp = 50;

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
            settlementMaxExp = ((settlementExpToLevelUp * settlementLvl) - (25 * settlementLvl));
            settlementLvl += 1;
            settlementMaxHp += 20;
            settlementCurrentHp = settlementMaxHp;
            SettlementDisplay();
        }
    }

}
