using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void Timerecord()
    {
        GameManager.timerecord -= 0.1f;
    }
    public void Size()
    {
        GameManager.size += 0.1f;
    }
}
