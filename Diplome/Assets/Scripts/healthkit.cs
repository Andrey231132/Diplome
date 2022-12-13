using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthkit : MonoBehaviour
{
    [SerializeField]
    private int heal;
    private void OnCollisionEnter2D(Collision2D col)
    {
        PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
        if(playerController)
        {
            playerController.SetHealth(heal);
            Destroy(gameObject);
        }
    }
}
