using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Еуые : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public float patrolRange;
    public float attackRange;
    public float attackCooldown;
    public float idleTime;

    private int currentTargetIndex;
    private Vector3[] patrolPoints;
    private GameObject player;
    private bool isAttacking;
    private float attackTimer;
    private bool isIdle;
    private float idleTimer;

    void Start()
    {
        // Назначаем патрульные точки
        patrolPoints = new Vector3[2];
        patrolPoints[0] = transform.position;
        patrolPoints[1] = new Vector3(transform.position.x + patrolRange, transform.position.y, transform.position.z);

        currentTargetIndex = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        isAttacking = false;
        attackTimer = 0;
        isIdle = false;
        idleTimer = 0;
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Двигаемся к текущей патрульной точке
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentTargetIndex], moveSpeed * Time.deltaTime);

            // Если достигли точки, ждем N секунд и меняем цель
            if (transform.position == patrolPoints[currentTargetIndex])
            {
                isIdle = true;
                idleTimer += Time.deltaTime;
                if (idleTimer >= waitTime)
                {
                    isIdle = false;
                    idleTimer = 0;
                    if (currentTargetIndex == 0)
                    {
                        currentTargetIndex = 1;
                    }
                    else
                    {
                        currentTargetIndex = 0;
                    }
                }
            }

            // Если игрок близко, начинаем преследование
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                isAttacking = true;
            }
        }
        else
        {
            // Двигаемся к игроку
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            // Если достигли игрока, начинаем атаку
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                if (attackTimer <= 0)
                {
                    // При необходимости, здесь можно добавить код атаки
                    attackTimer = attackCooldown;
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                }
            }
            // Если игрок убежал, попытаемся его найти снова или переходим в айдл
            else
            {
                if (Vector3.Distance(transform.position, player.transform.position) > attackRange * 2)
                {
                    isAttacking = false;
                    isIdle = true;
                    idleTimer = 0;
                }
            }
        }

        // Если в режиме айдл, ждем N секунд
        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTime)
            {
                isIdle = false;
                idleTimer = 0;
            }
        }
    }
}


