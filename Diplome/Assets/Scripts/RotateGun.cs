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
        transform.right = -((Vector2)transform.position - GetMousePosition());
        if (transform.position.x > GetMousePosition().x)
        {
            transform.localScale = new Vector3(transform.localScale.x, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        }
    }
}
