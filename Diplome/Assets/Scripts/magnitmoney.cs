using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnitmoney : MonoBehaviour
{
    [SerializeField]
    private float magnitdistance;
    [SerializeField]
    private float magnitspeed;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void CheckDistance()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= magnitdistance)
        {
            Magnit();
        }
    }
    private void Update()
    {
        CheckDistance();
    }
    private void Magnit()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.position += new Vector3(-magnitspeed, 0,0);
        }
        if (transform.position.x < player.transform.position.x)
        {
            transform.position += new Vector3(magnitspeed, 0,0);
        }
    }
}