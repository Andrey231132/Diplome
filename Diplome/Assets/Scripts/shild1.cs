using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shild1 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col!=null&&col.gameObject.GetComponent<Bullet>())
        {
            Destroy(gameObject);
        }
    }
}
