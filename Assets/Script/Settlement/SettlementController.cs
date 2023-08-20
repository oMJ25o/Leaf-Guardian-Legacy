using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;
using System.ComponentModel;

public class SettlementController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] public int houseBuilt;
    public bool IsPlayerUpgradeShopBuilt;

    public void AddHouse()
    {
        houseBuilt += 1;
    }

}
