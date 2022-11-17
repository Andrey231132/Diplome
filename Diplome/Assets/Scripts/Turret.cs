using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BaseEnemy
{
    [SerializeField]
    private float distanceY;
    [SerializeField]
    private float distanceX;
    [SerializeField]
    private float seeradius;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform turretgun;
    [SerializeField]
    private Transform aim;

    private Vector2 direction;
    private bool isdetected;
    private void Start()
    {
        StartCoroutine(IsDetected());
    }
    private void Update()
    {
        CheckHealth();
        SpawnSeeRadius();
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private IEnumerator IsDetected()
    {
        while (true)
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
        GameObject _bullet = Instantiate(bullet, aim.position, turretgun.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(aim.transform.up * speedbulet);
        Destroy(_bullet, 2f);
    }
    private void SpawnSeeRadius()
    {
        direction = player.position - transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(distanceX, distanceY, 0), direction, seeradius);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit && hit.collider.gameObject.GetComponent<PlayerController>())
            {
                turretgun.up = direction;
                isdetected = true;
                break;
            }
            else
            {
                isdetected = false;
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + new Vector3(distanceX,distanceY,0), direction * seeradius);
    }
}
