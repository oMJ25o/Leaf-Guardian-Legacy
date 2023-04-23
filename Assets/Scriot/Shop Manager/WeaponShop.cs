using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShop : ShopManager
{
    [SerializeField] protected int playerAttackDamageUpgrade;
    [SerializeField] protected int playerAttackDamageUpgradeCost;
    [SerializeField] protected TMP_Text playerAttackDamageUpgradeCostText;
    [SerializeField] protected TMP_Text playerAttackDamageUpgradeText;
    [SerializeField] protected TMP_Text playerAttackDamageUpgradeCountText;



    // Start is called before the first frame update
    void Start()
    {
        UpdateUpgradeCost(playerAttackDamageUpgradeCostText, playerAttackDamageUpgradeCost);
        playerAttackDamageUpgradeText.text = "Atk DMG: +" + playerAttackDamageUpgrade;
        playerAttackDamageUpgradeCountText.text = "+" + upgradeCount;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void BuyUpgrades()
    {
        if (CheckEnoughCurrency(playerAttackDamageUpgradeCost, playerController.lightLeaf))
        {
            Debug.Log("Bought Upgrade");
            playerController.lightLeaf -= playerAttackDamageUpgradeCost;
            playerController.attackDamage += playerAttackDamageUpgrade;
            upgradeCount += 1;
            playerController.UpdateLeafCount();
            UpdateUpgradeCost(playerAttackDamageUpgradeCostText, playerAttackDamageUpgradeCost);
            playerAttackDamageUpgradeCountText.text = "+" + upgradeCount;
        }
    }

}
