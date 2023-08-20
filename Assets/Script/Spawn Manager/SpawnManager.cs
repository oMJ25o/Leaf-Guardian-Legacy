using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static bool isGameOver = false;

    [SerializeField] private GameObject warningBox;
    [SerializeField] private GameObject[] enemyWave1Prefabs;
    [SerializeField] private GameObject[] enemyWave2Prefabs;
    [SerializeField] private GameObject[] enemyWave3Prefabs;
    [SerializeField] private GameObject[] enemyWave4Prefabs;
    [SerializeField] private GameObject[] enemyWave5Prefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private SettlementController settlementController;
    [SerializeField] private GameObject rampageEventText;
    [SerializeField] private CameraFollow cam;
    [SerializeField] private int rampageEventTimer;
    [SerializeField] private int finalRampageTimer;

    private int currentWave = 1;
    private int rampageCurrentTimer;
    private bool isRampage = false;
    private bool isWaveStart = false;

    public void SetupRampageEvent()
    {
        cam.PlayNormalMusic();
        rampageEventText.SetActive(false);
        isRampage = false;
        StopAllCoroutines();
        rampageCurrentTimer = rampageEventTimer;
        StartCoroutine("StartRampageCountDown");

    }

    IEnumerator StartRampageCountDown()
    {
        while (rampageCurrentTimer > 0)
        {
            yield return new WaitForSeconds(1);
            rampageCurrentTimer -= 1;
            if (rampageCurrentTimer == 5)
            {
                rampageEventText.SetActive(true);
                rampageEventText.GetComponent<TMPro.TMP_Text>().text = "Incoming Rampage";
            }
        }
        isRampage = true;
        rampageEventText.GetComponent<TMPro.TMP_Text>().text = "Rampage Event";
        rampageEventText.GetComponent<Animator>().Play("Entrance");
        cam.PlayRampageEventMusic();
        StartCoroutine("StartRampageTimer");
    }

    IEnumerator StartRampageTimer()
    {
        yield return new WaitForSeconds(33);
        isRampage = false;
        rampageEventText.SetActive(false);
        cam.PlayNormalMusic();
        rampageCurrentTimer = rampageEventTimer;
        StartCoroutine("StartRampageCountDown");
    }

    public void PrepareWave()
    {
        isWaveStart = true;
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
            yield return new WaitForSeconds(spawnInterval);
        }
        isWaveStart = false;
    }

    IEnumerator StartFinalRampageTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            rampageCurrentTimer -= 1;
            rampageEventText.GetComponent<TMPro.TMP_Text>().text = "Final Rampage: " + rampageCurrentTimer;

            if (rampageCurrentTimer <= 0)
            {
                cam.PlayRampageEventMusic();
                rampageCurrentTimer = 90;
                StartCoroutine("StartFinalRampage");
                break;
            }
        }
    }

    IEnumerator StartFinalRampage()
    {
        while (true)
        {
            if (rampageCurrentTimer >= 90)
            {
                rampageEventText.GetComponent<TMPro.TMP_Text>().text = "Final Rampage: Survive!";
                rampageEventText.GetComponent<Animator>().Play("Entrance");
            }

            int randomIndex = Random.Range(0, enemyWave1Prefabs.Length);
            Instantiate(enemyWave1Prefabs[randomIndex], enemyWave1Prefabs[randomIndex].transform.position, enemyWave1Prefabs[randomIndex].transform.rotation);
            yield return new WaitForSeconds(1);
            rampageCurrentTimer -= 1;

            if (rampageCurrentTimer <= 0)
            {
                //Start Victory Screen
                GameManager.Instance.isFinalRampageFinish = true;
                break;
            }
        }
    }

}
