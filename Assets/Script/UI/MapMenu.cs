using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject MainMenu;
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
        Map.SetActive(false);
        MainMenu.SetActive(true);
    }
}
