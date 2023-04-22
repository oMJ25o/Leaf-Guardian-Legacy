using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private SettlementController settlementController;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void SpawnEnemy()
    {
        switch (settlementController.settlementLvl)
        {
            case 1:
                Instantiate(enemyPrefabs[0], enemyPrefabs[0].transform.position, enemyPrefabs[0].transform.rotation);
                break;
            case 2:
                Instantiate(enemyPrefabs[1], enemyPrefabs[1].transform.position, enemyPrefabs[1].transform.rotation);
                break;
            case 3:
                Instantiate(enemyPrefabs[2], enemyPrefabs[2].transform.position, enemyPrefabs[2].transform.rotation);
                break;
            case 4:
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[randomIndex], enemyPrefabs[randomIndex].transform.position, enemyPrefabs[randomIndex].transform.rotation);
                break;
        }

        spawnInterval = Random.Range(2, 7);
        Invoke("SpawnEnemy", (spawnInterval));
    }

}
