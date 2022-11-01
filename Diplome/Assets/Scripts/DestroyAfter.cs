using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField]
    private float timedie;
    void Start()
    {
        Destroy(gameObject, timedie);
    }
}
