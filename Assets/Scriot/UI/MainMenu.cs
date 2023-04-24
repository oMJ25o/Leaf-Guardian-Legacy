using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject hudOptions;
    [SerializeField] private GameObject instructionObject;
    [SerializeField] private GameObject instructionHudTile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenIntructions()
    {
        instructionObject.SetActive(true);
        instructionHudTile.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenOptionMenu()
    {
        gameObject.SetActive(false);
        hudOptions.SetActive(true);
        optionMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
