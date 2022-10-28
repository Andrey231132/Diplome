using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CreateEnemy")]
public class Enemy : ScriptableObject
{
    public int health;
    public float speed;
    public int damage;
    public GameObject bullet;
    public float speedfire;
    public float speedbulet;
}
