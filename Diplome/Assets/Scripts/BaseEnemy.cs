using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject bullet;
    public float speedfire;
    public float speedbulet;
    public GameObject partical;
    private AudioSource audio;
    private void Start()
    {
        Debug.Log(audio);
    }
    protected void CheckHealth()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public virtual void GetDamage(int damage)
    {
        audio = GetComponent<AudioSource>();
        health -= damage;
        Instantiate(partical, transform.position, Quaternion.identity);
        audio.Play();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Bullet>())
        {
            GetDamage(col.gameObject.GetComponent<Bullet>().GetDamage());
        }
    }
    public abstract void Die();
}
