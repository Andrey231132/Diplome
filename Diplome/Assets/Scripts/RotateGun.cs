using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    private void Update()
    {
        RotationGun();
    }
    private Vector2 GetMousePosition()
    {
        Vector2 mouse = Input.mousePosition;
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(mouse);
        return mouseposition;
    }
    private void RotationGun()
    {
        if(transform.eulerAngles.z > 90f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            transform.right = ((Vector2)transform.position - GetMousePosition());
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,0, transform.eulerAngles.z);
            transform.right = -((Vector2)transform.position - GetMousePosition());
        }
    }
}
