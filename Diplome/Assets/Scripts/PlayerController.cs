using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerspeed;
    [SerializeField]
    private float playerforcejump;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        CheckPlayerInput();
    }
    private void CheckPlayerInput()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(playerspeed,0,0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(playerspeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * playerforcejump);
        }
    }
}
