using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static bool isGameOver = false;

    [SerializeField] private GameObject warningBox;
    [SerializeField] private int maxWave;
    [SerializeField] private GameObject[] enemyWave1Prefabs;
    [SerializeField] private GameObject[] enemyWave2Prefabs;
    [SerializeField] private GameObject[] enemyWave3Prefabs;
    [SerializeField] private GameObject[] enemyWave4Prefabs;
    [SerializeField] private GameObject[] enemyWave5Prefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private SettlementController settlementController;
    [SerializeField] private CameraFollow cam;

    private int currentWave = 1;
    private bool isWaveStart = false;

    void Update()
    {
        if (isWaveStart && GameObject.FindGameObjectWithTag("Enemy") == null && currentWave <= maxWave)
        {
            isWaveStart = false;
            currentWave++;
            warningBox.SetActive(true);
            settlementController.AddLeafToPlayer();
        }
    }

    public void PrepareWave()
    {
        warningBox.SetActive(false);
        switch (currentWave)
        {
            case 1:
                StartCoroutine(Spawn(enemyWave1Prefabs));
                break;
            case 2:
                StartCoroutine(Spawn(enemyWave2Prefabs));
                break;
            case 3:
                StartCoroutine(Spawn(enemyWave3Prefabs));
                break;
            case 4:
                StartCoroutine(Spawn(enemyWave4Prefabs));
                break;
            case 5:
                StartCoroutine(Spawn(enemyWave5Prefabs));
                break;
        }
    }

    IEnumerator Spawn(GameObject[] spawnWave)
    {
        for (int i = 0; i < spawnWave.Length; i++)
        {
            Instantiate(spawnWave[i], spawnWave[i].transform.position, spawnWave[i].transform.rotation);
            if (!isWaveStart)
            {
                isWaveStart = true;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }


}
