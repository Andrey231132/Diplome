using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerspeed;
    [SerializeField]
    private float playerforcejump;
    [SerializeField]
    private float bulletspeed;
    [SerializeField]
    private int health;
    [SerializeField]
    private Slider healthslider;
    [SerializeField]
    private Transform aim;//gun for bullet
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private GameObject bullet;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        CheckPlayerInput();
        UpdateSlider();
    }
    private void UpdateSlider()
    {
        healthslider.value = health;
    }
    private void CheckPlayerInput()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(playerspeed,0,0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(playerspeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0,1f) * playerforcejump);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, gun.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(_bullet.transform.right * bulletspeed);
        Destroy(_bullet, 3f);
    }
}
