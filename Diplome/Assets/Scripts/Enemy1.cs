using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    [SerializeField]
    private Transform guncontainer;
    [SerializeField]
    private Transform aim;
    [SerializeField]
    private float seeradius;

    private bool isdetected;

    private IEnumerator IsDetected()
    {
        while(true)
        {
            if (isdetected)
            {
                Shoot();
                yield return new WaitForSeconds(enemy.speedfire);
            }
            yield return null;
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(enemy.bullet, aim.position, guncontainer.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * enemy.speedbulet);
        Destroy(bullet, 2f);
    }
    private void RotateEnemy(Transform player)
    {
        if(player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
            guncontainer.transform.right = (player.transform.position - guncontainer.position);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            guncontainer.transform.right = -(player.transform.position - guncontainer.position);
        }
    }
    private void CreateCircleCastandCheckIt()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, seeradius);
        foreach(Collider2D col in cols)
        {
            if(col.gameObject.GetComponent<PlayerController>())
            {
                RotateEnemy(col.gameObject.transform);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(IsDetected());
    }
    private void Update()
    {
        CreateCircleCastandCheckIt();
    }
    public override void Die()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seeradius);
    }
}
