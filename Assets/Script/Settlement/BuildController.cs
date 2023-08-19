using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    [SerializeField] private GameObject house1;
    [SerializeField] private GameObject house2;
    [SerializeField] private GameObject house3;
    [SerializeField] private GameObject playerUpgradeShop;

    [SerializeField] private GameObject houseCollider1;
    [SerializeField] private GameObject houseCollider2;
    [SerializeField] private GameObject houseCollider3;
    [SerializeField] private GameObject playerUpgradeShopCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerBuild(string plot)
    {
        switch (plot)
        {
            case "house1":
                house1.SetActive(true);
                houseCollider1.SetActive(false);
                break;
            case "house2":
                house2.SetActive(true);
                houseCollider2.SetActive(false);
                break;
            case "house3":
                house3.SetActive(true);
                houseCollider3.SetActive(false);
                break;
            case "playerUpgradeShop":
                playerUpgradeShop.SetActive(true);
                playerUpgradeShopCollider.SetActive(false);
                break;
        }
    }
}
