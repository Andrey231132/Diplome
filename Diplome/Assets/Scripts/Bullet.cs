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
    public void SetBulletSpeed(float speed)
    {
        bulletspeed = speed;
    }
    public int GetDamage()
    {
        return damage;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            if(gameObject.name != "playerbullet" && gameObject.name != "playerbullet(Clone)")
            {
                col.gameObject.GetComponent<PlayerController>().GetDamage(damage);
            }
        }
        Instantiate(particlecollision, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void BulletMove()
    {
        transform.position += transform.right * bulletspeed * Time.deltaTime;
    }
    private void Update()
    {
        BulletMove();
        if (gameObject.name == "playerbullet")
        {
            damage = GameManager.damage;
        }
    }
}
