using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] private GameObject settlementLvl;
    [SerializeField] private GameObject settlementHp;
    [SerializeField] private GameObject inventory;
    [SerializeField] private CameraFollow cam;
    [SerializeField] private GameObject gameOverUI;

    [HideInInspector] public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            settlementLvl.SetActive(false);
            settlementHp.SetActive(false);
            inventory.SetActive(false);
        }
    }

    public void StartGameOver()
    {
        if (isGameOver)
        {
            cam.PlayGameOverMusic();
            gameOverUI.SetActive(true);
        }

    }
}
