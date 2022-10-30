using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;
    public GameObject bullet;
    public float speedfire;
    public float speedbulet;
    public GameObject partical;
    protected void CheckHealth()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public virtual void GetDamage(int damage)
    {
        health -= damage;
        Instantiate(partical, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Bullet>())
        {
            GetDamage(1);
        }
    }
    public abstract void Die();
}
