using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlementShop : ShopManager
{
    [SerializeField] protected SettlementController settlementController;
    [SerializeField] protected TMP_Text settlementHpUpgradeCostText;
    [SerializeField] protected TMP_Text settlementHpRepairCostText;
    [SerializeField] protected TMP_Text settlementHpUpgradeText;
    [SerializeField] protected int settlementHpUpgrade;
    [SerializeField] protected int settlementHpUpgradeCost;
    [SerializeField] protected int settlementRepairCost;


    // Start is called before the first frame update
    void Start()
    {
        UpdateUpgradeCost(settlementHpUpgradeCostText, settlementHpUpgradeCost);
        UpdateUpgradeCost(settlementHpRepairCostText, settlementRepairCost);
        settlementHpUpgradeText.text = "HP: +" + settlementHpUpgrade;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BuyUpgrades(string upgradeName)
    {
        switch (upgradeName)
        {
            case "SettlementHp":
                if (CheckEnoughCurrency(settlementHpUpgradeCost, playerController.darkLeaf))
                {
                    playerController.darkLeaf -= settlementHpUpgradeCost;
                    settlementController.settlementMaxHp += settlementHpUpgrade;
                    settlementController.settlementCurrentHp += settlementHpUpgrade;
                    settlementController.SettlementDisplay();
                    playerController.UpdateLeafCount();
                    upgradeCount += 1;
                    UpdateUpgradeCost(settlementHpUpgradeCostText, settlementHpUpgradeCost);
                }
                return;
            case "Repair":
                if (CheckEnoughCurrency(settlementRepairCost, playerController.darkLeaf))
                {
                    playerController.darkLeaf -= settlementRepairCost;
                    settlementController.settlementCurrentHp = settlementController.settlementMaxHp;
                    settlementController.SettlementDisplay();
                    playerController.UpdateLeafCount();
                }
                return;
        }
    }

}
