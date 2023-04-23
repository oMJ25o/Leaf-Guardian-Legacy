using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static bool isGameOver = false;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private SettlementController settlementController;
    [SerializeField] private GameObject rampageEventText;
    [SerializeField] private CameraFollow cam;


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
        if (!isGameOver)
        {
            cam.PlayNormalMusic();
            rampageEventText.SetActive(false);
            isRampage = false;
            StopAllCoroutines();
            StartCoroutine("StartRampageCountDown");
        }

    }

    IEnumerator StartRampageCountDown()
    {
        yield return new WaitForSeconds(50);
        isRampage = true;
        rampageEventText.SetActive(true);
        cam.PlayRampageEventMusic();
        StartCoroutine("StartRampageTimer");
    }

    IEnumerator StartRampageTimer()
    {
        yield return new WaitForSeconds(33);
        isRampage = false;
        rampageEventText.SetActive(false);
        cam.PlayNormalMusic();
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
                    int randomIndex = Random.Range(0, enemyPrefabs.Length);
                    Instantiate(enemyPrefabs[randomIndex], enemyPrefabs[randomIndex].transform.position, enemyPrefabs[randomIndex].transform.rotation);
                    break;
            }

            if (!isRampage)
            {
                spawnInterval = Random.Range(5, 10);
            }
            else if (isRampage)
            {
                spawnInterval = 1.5f;
            }

            Invoke("SpawnEnemy", (spawnInterval));
        }
    }

}
