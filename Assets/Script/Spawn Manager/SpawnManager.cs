using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static bool isGameOver = false;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SettlementController settlementController;
    [SerializeField] private GameObject rampageEventText;
    [SerializeField] private CameraFollow cam;
    [SerializeField] private int rampageEventTimer;
    [SerializeField] private int finalRampageTimer;

    private int rampageCurrentTimer;
    private float spawnInterval;
    private bool isRampage = false;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        SetupRampageEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            StopAllCoroutines();
        }
    }

    public void SetupRampageEvent()
    {
        if (!isGameOver && settlementController.settlementLvl < 6)
        {
            cam.PlayNormalMusic();
            rampageEventText.SetActive(false);
            isRampage = false;
            StopAllCoroutines();
            rampageCurrentTimer = rampageEventTimer;
            StartCoroutine("StartRampageCountDown");
        }

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


    private void SpawnEnemy()
    {
        if (!GameManager.Instance.isGameOver)
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
                    Instantiate(enemyPrefabs[3], enemyPrefabs[3].transform.position, enemyPrefabs[3].transform.rotation);
                    break;
                case 5:
                    Instantiate(enemyPrefabs[4], enemyPrefabs[4].transform.position, enemyPrefabs[4].transform.rotation);
                    break;
                case >= 6:
                    rampageCurrentTimer = finalRampageTimer;
                    rampageEventText.SetActive(true);
                    rampageEventText.GetComponent<TMPro.TMP_Text>().text = "Final Rampage: " + rampageCurrentTimer;
                    StopAllCoroutines();
                    StartCoroutine("StartFinalRampageTimer");
                    break;
            }

            if (!isRampage)
            {
                spawnInterval = Random.Range(5, 10);
            }
            else if (isRampage)
            {
                spawnInterval = Random.Range(0.5f, 1.5f);
            }

            if (settlementController.settlementLvl < 6)
            {
                Invoke("SpawnEnemy", (spawnInterval));
            }
        }
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

            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomIndex], enemyPrefabs[randomIndex].transform.position, enemyPrefabs[randomIndex].transform.rotation);
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
