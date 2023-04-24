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
    private Transform player;//WHERE TURRET SHOOT
    [SerializeField]
    private Transform turretgun;//SPRITE OF GUN
    [SerializeField]
    private Transform aim;// PLACE WHERE BULLET SPAWN
    [SerializeField]
    private float bulletspeed;//THIS SPEED FOR TURRET BULLET

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
        GameObject _bullet = Instantiate(bullet, aim.position, aim.rotation);
        _bullet.GetComponent<Bullet>().SetBulletSpeed(bulletspeed);
        Destroy(_bullet, 2f);
    }
    private void SpawnSeeRadius()
    {
        direction = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(distanceX, distanceY, 0), direction, seeradius);
        if (hit && hit.collider.gameObject.GetComponent<PlayerController>())
        {
            turretgun.up = direction;
            isdetected = true;
        }
        else
        {
            isdetected = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + new Vector3(distanceX,distanceY,0), direction * seeradius);
    }
}
