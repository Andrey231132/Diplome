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
    [SerializeField]
    private float distancetorunplayer;

    private Animator anim;
    private bool decectplayer;
    private bool isgoingleft;
    private Transform player;
    private bool shoot;
    private AudioSource audio;
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
    private void CheckDetectPlayer()
    {
        if(decectplayer)
        {
            Atack();
            if(shoot == false)
            {
                StartCoroutine(Shooting());
                shoot = true;
            }
        }
        else
        {
            Patrule();
        }
    }
    private void Atack()
    {
        if (Vector2.Distance(player.position, transform.position) != distancetorunplayer)
        {
            if (player.position.x > transform.position.x)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
            else
            {
                transform.position -= new Vector3(speed, 0, 0);
            }
        }
    }
    private void Patrule()
    {
        if (isgoingleft)
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
    }
    private void CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastplace.position, transform.right, seeradius);
        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>() || hit.collider.gameObject.GetComponent<shild1>())
            {
                decectplayer = true;
                player = hit.collider.gameObject.GetComponent<Transform>();
            }
            else
            {
                decectplayer = false;;
            }
        }
    }
    private IEnumerator Shooting()
    {
        Shoot();
        yield return new WaitForSeconds(speedfire);
        shoot = false;
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, Quaternion.identity);
        if (transform.eulerAngles.y == 0)
        { _bullet.GetComponent<Bullet>().SetBulletSpeed(bulletspeed); }
        else if (transform.eulerAngles.y == 180)
        { _bullet.GetComponent<Bullet>().SetBulletSpeed(-bulletspeed); }
    }
    public override void GetDamage(int damage)
    {
        audio = GetComponent<AudioSource>();
        health -= damage;
        Instantiate(partical, transform.position, Quaternion.identity);
        Instantiate(money, transform.position, Quaternion.identity);
        audio.Play();
        if(isgoingleft == true)
        {
            isgoingleft = false;
        }
        else
        {
            isgoingleft = true;
        }
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

