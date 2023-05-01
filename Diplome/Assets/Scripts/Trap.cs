using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float timebetweendamage;
    [SerializeField]
    private int damage=1;

    private bool isactive;
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>() && !isactive)
        {
            StartCoroutine(Damager(col.gameObject.GetComponent<PlayerController>()));
        }
    }
    private IEnumerator Damager(PlayerController player)
    {
        isactive = true;
        player.GetDamage(damage);
        yield return new WaitForSeconds(timebetweendamage);
        isactive = false;
    }
}
