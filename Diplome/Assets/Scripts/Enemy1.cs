using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    [SerializeField]
    private Transform righttrigger;
    [SerializeField]
    private Transform lefttrigger;
    [SerializeField]
    private float seeradius;
    [SerializeField]
    private Transform aim;

    private bool isdetected;
    private bool isright;
    private bool shoot;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckTriggerPlace();
        CreateTriggerPlayer();
        CheckHealth();
    }
    private IEnumerator Detected()
    {
        while (isdetected)
        {
            Shoot();
            anim.SetBool("Run", false);
            yield return new WaitForSeconds(speedfire);
        }
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, Quaternion.identity);
    }
    private void CheckTriggerPlace()
    {
        if (!isdetected)
        {
            anim.SetBool("Run", true);
            if (isright)
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 0, 0);
                if (transform.position.x >= righttrigger.position.x) { isright = false; }
            }
            if (!isright)
            {
                transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 180, 0);
                if (transform.position.x <= lefttrigger.position.x) { isright = true; }
            }
            shoot = false;
        }
        else if (!shoot)
        {
            StartCoroutine(Detected());
            shoot = true;
        }
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void CreateTriggerPlayer()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, seeradius);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<PlayerController>())
                {
                    isdetected = true;
                    break;
                }
                isdetected = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * seeradius);
    }
}
