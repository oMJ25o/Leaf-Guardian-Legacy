using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapController : MonoBehaviour
{
    [SerializeField] private GameObject arrowCave;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        arrowCave.SetActive(true);
    }

    private void OnMouseExit()
    {
        arrowCave.SetActive(false);
    }
}
