using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject particlecollision;
    private void OnCollisionEnter2D()
    {
        Instantiate(particlecollision, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
