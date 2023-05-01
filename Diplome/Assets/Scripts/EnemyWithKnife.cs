using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithKnife : BaseEnemy
{
    [SerializeField]
    private float seeradius;
    [SerializeField]
    private Transform righttrigger;
    [SerializeField]
    private Transform lefttrigger;
    [SerializeField]
    private Transform raycastplace;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float timeBetweenPunch;
    [SerializeField]
    private float distancedamage;

    private bool istrigger;
    public bool move;
    private bool isnear;
    private bool isdamage;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckTrigger();
        CreateTriggerPlayer();
        CheckHealth();
    }
    private void CheckTrigger()
    {
        if (!istrigger)
        {
            Walk();
        }
        else
        {
            Follow();
        }
    }
    private void Walk()
    {
        if (move)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
            if(transform.position.x >= righttrigger.position.x)
            {
                move = false;
            }
        }
        else
        {
            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (transform.position.x <= lefttrigger.position.x)
            {
                move = true;
            }
        }
        anim.SetBool("Run", true);
    }
    private void Follow()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        anim.SetBool("Run", true);
        if (Vector2.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(player.position.x, 0, 0)) <= distancedamage && !isdamage&& istrigger)
        { 
            isdamage = true;
            isnear = true;
            StartCoroutine(Punch());
        }
        else
        {
            isnear = false;
        }
    }
    private void CreateTriggerPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastplace.position, transform.right, seeradius);
        if (hit && hit.collider.gameObject.GetComponent<PlayerController>() || hit && hit.collider.gameObject.GetComponent<shild1>())
        {
            istrigger = true;
        }
        else
        {
            istrigger = false;
        }
    }
    private IEnumerator Punch()
    {
        while(isnear)
        {
            player.gameObject.GetComponent<PlayerController>().GetDamage(damage);
            yield return new WaitForSeconds(timeBetweenPunch);
            isdamage = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(raycastplace.transform.position, transform.right * seeradius);
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
