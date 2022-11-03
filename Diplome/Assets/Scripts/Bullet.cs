using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject particlecollision;
    [SerializeField]
    private int damage;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            col.gameObject.GetComponent<PlayerController>().GetDamage(damage);
        }
        Instantiate(particlecollision, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
