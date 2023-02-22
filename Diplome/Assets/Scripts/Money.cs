using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static int moneys;
    public static void getmoney(int money)
    {
        moneys += money;
    }
}
