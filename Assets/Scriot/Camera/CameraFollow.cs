using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject settlementHp;
    [SerializeField] private float yOffSet = 5f;
    [SerializeField] private float zPos = -21f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, yOffSet, zPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SettlementHealth"))
        {
            settlementHp.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SettlementHealth"))
        {
            settlementHp.SetActive(true);
        }
    }
}
