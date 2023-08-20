using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject house1;
    [SerializeField] private GameObject house2;
    [SerializeField] private GameObject house3;
    [SerializeField] private GameObject playerUpgradeShop;
    [SerializeField] private SettlementController settlementController;

    [SerializeField] private TMP_Text[] houseCostTexts;
    [SerializeField] private TMP_Text playerUpgradeCostText;

    [SerializeField] private GameObject houseCollider1;
    [SerializeField] private GameObject houseCollider2;
    [SerializeField] private GameObject houseCollider3;
    [SerializeField] private GameObject playerUpgradeShopCollider;

    [SerializeField] private int houseCost;
    [SerializeField] private int playerUpgradeShopCost;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < houseCostTexts.Length; i++)
        {
            houseCostTexts[i].text = houseCost.ToString();
        }
        playerUpgradeCostText.text = playerUpgradeShopCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool CheckEnoughCurrency(int cost, int leaf)
    {
        return cost <= leaf;
    }

    public void PlayerBuild(string plot)
    {
        switch (plot)
        {
            case "house1":
                if (CheckEnoughCurrency(houseCost, playerController.leaf))
                {
                    playerController.leaf -= houseCost;
                    playerController.UpdateLeafCount();
                    house1.SetActive(true);
                    houseCollider1.SetActive(false);
                    settlementController.AddHouse();
                }
                break;
            case "house2":
                if (CheckEnoughCurrency(houseCost, playerController.leaf))
                {
                    playerController.leaf -= houseCost;
                    playerController.UpdateLeafCount();
                    house2.SetActive(true);
                    houseCollider2.SetActive(false);
                    settlementController.AddHouse();
                }
                break;
            case "house3":
                if (CheckEnoughCurrency(houseCost, playerController.leaf))
                {
                    playerController.leaf -= houseCost;
                    playerController.UpdateLeafCount();
                    house3.SetActive(true);
                    houseCollider3.SetActive(false);
                    settlementController.AddHouse();
                }
                break;
            case "playerUpgradeShop":
                if (CheckEnoughCurrency(playerUpgradeShopCost, playerController.leaf))
                {
                    playerController.leaf -= playerUpgradeShopCost;
                    playerController.UpdateLeafCount();
                    playerUpgradeShop.SetActive(true);
                    playerUpgradeShopCollider.SetActive(false);
                }
                break;
        }
    }
}
