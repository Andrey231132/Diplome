using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shild1 : MonoBehaviour
{
    [SerializeField]
    private float timelive;
    private void Start()
    {
        Destroy(gameObject, timelive);
    }   
}
