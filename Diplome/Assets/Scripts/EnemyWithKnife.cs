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
    private Transform player;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float timeBetweenPunch;
    [SerializeField]
    private float distancedamage;

    private bool istrigger;
    private bool move;
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
                    istrigger = true;
                    break;
                }
                istrigger = false;
            }
        }
    }
    private IEnumerator Punch()
    {
        while(isnear)
        {
            player.gameObject.GetComponent<PlayerController>().GetDamage(damage);
            yield return new WaitForSeconds(timeBetweenPunch);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * seeradius);
    }
    public override void Die()
    {
        Instantiate(partical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
