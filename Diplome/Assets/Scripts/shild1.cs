using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shild1 : MonoBehaviour
{
    [SerializeField]
    private float numberbulletstobreakshild=1;
    private int numberbullet;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && col.gameObject.GetComponent<Bullet>())
        {
            numberbullet++;
            if(numberbullet >= numberbulletstobreakshild)
            {
                Destroy(gameObject);
            }
        }
    }
}
