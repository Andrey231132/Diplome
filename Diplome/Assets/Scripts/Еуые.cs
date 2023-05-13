using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Еуые : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float patrolSpeed = 1f;
    public float waitTime = 3f;
    public float chaseSpeed = 2f;
    public float chaseDistance = 4f;
    public float attackDistance = 1f;
    public float chaseTime = 5f;

    private Transform target;
    private bool chasing = false;
    private float chaseStartTime;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToTarget(point1.position, point2.position, patrolSpeed));
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(MoveToTarget(point2.position, point1.position, patrolSpeed));
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToTarget(Vector3 start, Vector3 end, float speed)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(start, end);

        while (transform.position != end)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(start, end, fracJourney);
            yield return null;
        }
    }

    void Update()
    {
        if (chasing)
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, target.position) <= attackDistance)
                {
                    // Attack the player
                }
                else if (Time.time - chaseStartTime >= chaseTime)
                {
                    chasing = false;
                    StartCoroutine(Patrol());
                }
            }
            else
            {
                chasing = false;
                StartCoroutine(Patrol());
            }
        }
        else if (target != null && Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            chasing = true;
            chaseStartTime = Time.time;
        }
    }

}
