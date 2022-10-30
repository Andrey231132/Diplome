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
    private void CreateTriggerPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(3,0), transform.right, seeradius);
        if (hit && hit.collider.gameObject.GetComponent<PlayerController>())
        {
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
        CreateTriggerPlayer();
        CheckHealth();
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        
    }
}
