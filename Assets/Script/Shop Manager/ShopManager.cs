using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected int weaponUpgradeCount;
    [SerializeField] protected int glovesUpgradeCount;
    [SerializeField] protected AudioClip buySfx;
    [SerializeField] protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void SetupShop() { }
    public virtual void BuyUpgrades(string upgradeName) { }
    protected virtual void UpdateUpgradeCost(TMP_Text updateUpgrade, int upgradeCost, int upgradeCount) { updateUpgrade.text = "" + CalculateCost(upgradeCost, upgradeCount); }
    protected virtual int CalculateCost(int upgradeCost, int upgradeCount) { return upgradeCost * (upgradeCount + 1); }
    protected virtual bool CheckEnoughCurrency(int upgradeCost, int playerCurrency) { return upgradeCost <= playerCurrency; }

}
