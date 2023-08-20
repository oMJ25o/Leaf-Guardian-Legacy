using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShop : ShopManager
{
    [SerializeField] protected int playerAttackDamageUpgrade;
    [SerializeField] protected int playerAttackDamageUpgradeCost;
    [SerializeField] protected float playerAttackSpeedUpgrade;
    [SerializeField] protected int playerAttackSpeedUpgradeCost;
    [SerializeField] protected TMP_Text playerAttackDamageUpgradeCostText;
    [SerializeField] protected TMP_Text playerAttackSpeedUpgradeCostText;
    [SerializeField] protected TMP_Text playerAttackDamageUpgradeCountText;
    [SerializeField] protected TMP_Text playerAttackSpeedUpgradeCountText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUpgradeCost(playerAttackDamageUpgradeCostText, playerAttackDamageUpgradeCost, weaponUpgradeCount);
        playerAttackDamageUpgradeCountText.text = "x" + weaponUpgradeCount;
        playerAttackSpeedUpgradeCountText.text = "x" + glovesUpgradeCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BuyUpgrades(string upgradeName)
    {
        switch (upgradeName)
        {
            case "AttackDamage":
                if (CheckEnoughCurrency(CalculateCost(playerAttackDamageUpgradeCost, weaponUpgradeCount), playerController.leaf))
                {
                    //audioSource.PlayOneShot(buySfx);
                    playerController.leaf -= CalculateCost(playerAttackDamageUpgradeCost, weaponUpgradeCount);
                    playerController.attackDamage += playerAttackDamageUpgrade;
                    weaponUpgradeCount += 1;
                    playerController.UpdateLeafCount();
                    UpdateUpgradeCost(playerAttackDamageUpgradeCostText, playerAttackDamageUpgradeCost, weaponUpgradeCount);
                    playerAttackDamageUpgradeCountText.text = "x" + weaponUpgradeCount;
                }
                break;
            case "AttackSpeed":
                if (CheckEnoughCurrency(CalculateCost(playerAttackSpeedUpgradeCost, glovesUpgradeCount), playerController.leaf))
                {
                    //audioSource.PlayOneShot(buySfx);
                    playerController.leaf -= CalculateCost(playerAttackSpeedUpgradeCost, glovesUpgradeCount);
                    playerController.attackSpeed += playerAttackSpeedUpgrade;
                    glovesUpgradeCount += 1;
                    playerController.UpdateLeafCount();
                    UpdateUpgradeCost(playerAttackSpeedUpgradeCostText, playerAttackSpeedUpgradeCost, glovesUpgradeCount);
                    playerAttackSpeedUpgradeCountText.text = "x" + glovesUpgradeCount;
                }
                break;
        }
    }

}
