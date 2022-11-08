using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    [SerializeField]
    private GameObject playershield;
    [SerializeField]
    private GameObject particle;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col!=null&&col.gameObject.GetComponent<PlayerController>())
        {
            SpawnPlayerShield(col.gameObject.transform);
            Destroy(gameObject);
        }
    }
    private void SpawnPlayerShield(Transform player)
    {
        Instantiate(particle, player.position, Quaternion.identity);
        GameObject obj = Instantiate(playershield, player.position, Quaternion.identity);
        obj.transform.parent = player;
    }
}
