using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public Enemy enemy;
    private void Update()
    {
        CheckHealth();
    }
    private void CheckHealth()
    {
        if(enemy.health <= 0)
        {
            Die();
        }
    }
    public virtual void GetDamage(int damage)
    {
        enemy.health -= damage;
    }
    
    public abstract void Die();
}
