using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float timebetweendamage;
    [SerializeField]
    private int damage;

    private bool isactive;
    private IEnumerator SetDamage(GameObject player)
    {
        while(isactive)
        {
            player.GetComponent<PlayerController>().GetDamage(damage);
            yield return new WaitForSeconds(timebetweendamage);
        }
        yield return null;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject && col.gameObject.GetComponent<PlayerController>())
        {
            isactive = true;
            StartCoroutine(SetDamage(col.gameObject));
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject && col.gameObject.GetComponent<PlayerController>())
        {
            isactive = false;
        }
    }
}
