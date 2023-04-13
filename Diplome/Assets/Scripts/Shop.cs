using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void Timerecord()
    {
        if (GameManager.Money() > 0)
        {
            GameManager.timerecord -= 0.1f;
            GameManager.Buy(1);
        }
    }
    public void Size()
    {
        if (GameManager.Money() > 0)
        {
            GameManager.size += 0.1f;
            GameManager.Buy(1);
        }
    }
    public void Damage()
    {
        if (GameManager.Money() > 0)
        {
            GameManager.damage += 1;
            GameManager.Buy(1);
        }
    }
}
