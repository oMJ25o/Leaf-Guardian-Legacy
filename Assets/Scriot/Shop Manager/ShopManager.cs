using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected int upgradeCount;
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
    public virtual void BuyUpgrades() { }
    public virtual void BuyUpgrades(string upgradeName) { }
    protected virtual void UpdateUpgradeCost(TMP_Text updateUpgrade, int upgradeCost) { updateUpgrade.text = "" + CalculateCost(upgradeCost); }
    protected virtual int CalculateCost(int upgradeCost) { return upgradeCost * (upgradeCount + 1); }
    protected virtual bool CheckEnoughCurrency(int upgradeCost, int playerCurrency) { return upgradeCost <= playerCurrency; }

}
