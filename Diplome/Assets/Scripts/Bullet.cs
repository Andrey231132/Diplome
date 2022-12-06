using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject particlecollision;//THIS IS PARTICLE,THEY SPAWN WHEN BULLET TOUCH ANYTHING
    [SerializeField]
    private int damage;//THIS IS BULLET DAMAGE
   
    private float bulletspeed;//THIS IS BULLET SPEED.
    private Rigidbody2D rb;//THIS IS RIGIBOODY BULLET
    public void SetBulletSpeed(float speed)
    {
        bulletspeed = speed;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            col.gameObject.GetComponent<PlayerController>().GetDamage(damage);
        }
        Instantiate(particlecollision, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void BulletMove()
    {
        rb.velocity = transform.right * bulletspeed;
    }
    private void Update()
    {
        BulletMove();
    }
}
