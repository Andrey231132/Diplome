using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text moneystext;
    [SerializeField]
    private int max_level_update = 3;

    private int current_speed_update;
    private int current_timerecord_update;
    private int current_damage_update;
    public void Timerecord()
    {
        if (GameManager.Money() > 0 && current_timerecord_update !< max_level_update)
        {
            GameManager.timerecord -= 0.1f;
            current_timerecord_update++;
            GameManager.Buy(1);
        }
    }
    public void Speed()
    {
        if (GameManager.Money() > 0 && current_speed_update! < max_level_update)
        {
            GameManager.speed += 0.05f;
            current_speed_update++;
            GameManager.Buy(1);
        }
    }
    public void Damage()
    {
        if (GameManager.Money() > 0 && current_damage_update! < max_level_update)
        {
            GameManager.damage += 1;
            current_damage_update++;
            GameManager.Buy(1);
        }
    }
    private void Update()
    {
        moneystext.text = GameManager.Money().ToString();
    }
}
