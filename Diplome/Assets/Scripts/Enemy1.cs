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
    private Transform lefttrigger;
    [SerializeField]
    private Transform righttrigger;
    [SerializeField]
    private float seeradius;
   
    private bool isdetected;
    private bool isright;
    private Animator anim;
    private IEnumerator IsDetected()
    {
        while (true)
        {
            if (isdetected)
            {
                Shoot();
                anim.SetBool("Run", false);
                yield return new WaitForSeconds(speedfire);
            }
            else
            {
                CheckTriggerPlace();
            }
            yield return null;
        }
    }
    private void CheckTriggerPlace()
    {
        if(!isdetected)
        {
            anim.SetBool("Run", true);
            if(isright)
            {
                transform.position += new Vector3(speed,0,0) * Time.deltaTime;
                transform.eulerAngles = new Vector3(0,0,0);
                if (transform.position.x >= righttrigger.position.x) { isright = false; }
            }
            if(!isright)
            {
                transform.position -= new Vector3(speed, 0, 0 )* Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 180, 0);
                if (transform.position.x <= lefttrigger.position.x) { isright = true; }
            }
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1.6f,0), transform.right, seeradius);
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
        anim = GetComponent<Animator>();
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
        Gizmos.DrawRay(transform.position + new Vector3(1.5f,0,0), transform.right);
    }
}
