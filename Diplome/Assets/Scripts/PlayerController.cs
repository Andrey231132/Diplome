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
    private float jumpforce;
    [SerializeField]
    private int health;
    [SerializeField]
    private Transform aim;//gun for bullet
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject particle;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isjump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CheckPlayerInput();
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
        if(Input.GetKeyDown(KeyCode.W) && isjump == false)
        {
            rb.AddForce(transform.up * jumpforce);
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
    public void GetDamage(int damage)
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        health -= damage;
    }
    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, aim.position, gun.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(aim.transform.right * bulletspeed);
        Destroy(_bullet, 3f);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        isjump = false;
    }
    private void OnCollisionExit2D()
    {
        isjump = true;
    }
    public GameObject GetBullet()
    {
        return bullet;
    }
    public void ChangeBulletSpeed(float change)
    {
        bulletspeed+=change;
    }
    public void ChangeHealth(int changehealth)
    {
        health += changehealth;
    }
}
