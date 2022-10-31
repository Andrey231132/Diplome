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
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        float x = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(playerspeed,0,0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(playerspeed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if(x!=0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, gun.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(aim.transform.right * bulletspeed);
        Destroy(_bullet, 3f);
    }
}
