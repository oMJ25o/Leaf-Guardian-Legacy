using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] private GameObject settlementLvl;
    [SerializeField] private GameObject settlementHp;
    [SerializeField] private GameObject inventory;
    [SerializeField] private CameraFollow cam;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Animator fadeInAnimator;


    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public bool isFinalRampageFinish = false;
    [HideInInspector] public bool isPlayerVictory = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver || isPlayerVictory)
        {
            settlementLvl.SetActive(false);
            settlementHp.SetActive(false);
            inventory.SetActive(false);
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && isFinalRampageFinish == true)
        {
            isPlayerVictory = true;
            StartFadeIn();
        }
    }

    public void StartGameOver()
    {
        if (isGameOver && !isPlayerVictory)
        {
            cam.PlayGameOverMusic();
            gameOverUI.SetActive(true);
        }

    }

    public void StartFadeIn()
    {
        fadeInAnimator.Play("FadeIn");
    }

    public void StartVictoryScreen()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
