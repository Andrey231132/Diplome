using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithGun : BaseEnemy
{
    [SerializeField]
    private Transform righttrigger;
    [SerializeField]
    private Transform lefttrigger;
    [SerializeField]
    private float seeradius;
    [SerializeField]
    private Transform aim;//PLACE WHER BULLET SPAWN
    [SerializeField]
    private float bulletspeed;//SPEED BULLET
    [SerializeField]
    private Transform raycastplace;

    private Animator anim;
    private bool decectplayer;
    private bool isgoingleft;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Run", true);
    }
    private void Update()
    {
        CheckDetectPlayer();
        CheckPlayer();
        CheckHealth();
    }
    private bool isactive;
    private void CheckDetectPlayer()
    {
        if(decectplayer)
        {
            StopCoroutine(Patrule());
            isactive = false;
        }
        else if(!isactive && !decectplayer)
        {
            StartCoroutine(Patrule());
            isactive = true;
        }
    }
    private IEnumerator Patrule()
    {
        while(!decectplayer)
        {
            if(isgoingleft)
            {
                if (transform.position.x >= lefttrigger.transform.position.x)
                {
                    transform.position -= new Vector3(speed, 0, 0);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    isgoingleft = false;
                }
            } 
            else
            {

                if (transform.position.x <= righttrigger.transform.position.x)
                {
                    transform.position += new Vector3(speed, 0, 0);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    isgoingleft = true;
                }
            }
            yield return null;
        }
    }
    private void CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastplace.position, transform.right, seeradius);
        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>())
            {
                decectplayer = true;
            }
        }
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, Quaternion.identity);
        if (transform.eulerAngles.y == 0)
        { _bullet.GetComponent<Bullet>().SetBulletSpeed(bulletspeed); }
        else if (transform.eulerAngles.y == 180)
        { _bullet.GetComponent<Bullet>().SetBulletSpeed(-bulletspeed); }
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(raycastplace.position, transform.right * seeradius);
    }
    protected void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }
}

