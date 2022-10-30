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
    [SerializeField]
    private Transform player;

    private bool isdetected;

    private IEnumerator IsDetected()
    {
        while(true)
        {
            if (isdetected)
            {
                Shoot();
                yield return new WaitForSeconds(speedfire);
            }
            yield return null;
        }
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, guncontainer.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(aim.transform.up * speedbulet);
        Destroy(_bullet, 2f);
    }
    private void RotateGun()
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.7f), -(transform.position + new Vector3(0,0.7f) - player.position), seeradius);
        if (hit && hit.collider.gameObject.GetComponent<PlayerController>())
        {
            RotateGun();
            isdetected = true;
        }
        else
        {
            isdetected = false;
        }
    }
    private void Start()
    {
        StartCoroutine(IsDetected());
    }
    private void Update()
    {
        CreateCircleCastandCheckIt();
        CheckHealth();
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.7f), -(transform.position + new Vector3(0, 0.7f) - player.position));
    }
}
