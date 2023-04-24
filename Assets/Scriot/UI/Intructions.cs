using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intructions : MonoBehaviour
{
    [SerializeField] private GameObject instructionHud;
    [SerializeField] private GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMainMenu()
    {
        instructionHud.SetActive(false);
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

}
