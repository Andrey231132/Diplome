using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{ 
    [SerializeField]
    private Element element;

    public int health;
    public float speed;
    public GameObject bullet;
    public GameObject money;
    public float speedfire;
    public float speedbulet;
    public GameObject partical;
    private AudioSource audio;
    private void Awake()
    {
        SetAudio();
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
        Instantiate(money, transform.position, Quaternion.identity);
        audio.Play();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Bullet>())
        {
            if(col.gameObject.name != "enemubullet" && col.gameObject.name != "enemybullet(Clone)")
            {
                GetDamage(col.gameObject.GetComponent<Bullet>().GetDamage());
                if (col.gameObject.GetComponent<Bullet>().GetElement() == element)
                {
                    GetDamage(col.gameObject.GetComponent<Bullet>().GetDamage());
                }
            }
            if (gameObject.GetComponent<EnemyWithGun>())
            {
                if (col.gameObject.transform.position.x > transform.position.x)
                {
                    gameObject.GetComponent<EnemyWithGun>().isgoingleft = false;
                }
                if(col.gameObject.transform.position.x < transform.position.x)
                {
                    gameObject.GetComponent<EnemyWithGun>().isgoingleft = true;
                }
            }
            if (gameObject.GetComponent<EnemyWithKnife>())
            {
                if (col.gameObject.transform.position.x > transform.position.x)
                {
                    gameObject.GetComponent<EnemyWithKnife>().move = true;
                }
                if (col.gameObject.transform.position.x < transform.position.x)
                {
                    gameObject.GetComponent<EnemyWithKnife>().move = false;
                }
            }
        }
    }
    private void SetAudio()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("musicvalue");
    }
    public abstract void Die();
}
