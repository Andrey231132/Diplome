using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<PlayerController>())
        {
            GameManager.NextLevel();
        }
    }
}
