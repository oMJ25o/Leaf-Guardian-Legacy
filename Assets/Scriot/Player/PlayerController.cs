using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        MovePlayer(horizontalInput);
    }

    private void MovePlayer(float horizontalInput)
    {
        GetComponent<Animator>().SetFloat("Horizontal", horizontalInput);
        gameObject.transform.Translate(transform.right * playerSpeed * Time.deltaTime * horizontalInput);
    }

}
