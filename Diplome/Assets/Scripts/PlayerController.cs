using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerspeed;//PLAYER SPEED MOVE
    [SerializeField]
    private float jumpforce;//PLAYER JUMP FORCE
    [SerializeField]
    private float bulletspeed;//THIS SPEED FOR PLAYER BULLET
   [SerializeField]
    private int health;//PLAYER HEALTH
    [SerializeField]
    private Transform aim;//PLACE WHERE SPAWN BULLET, WHEN PLAYER SHOOT
    [SerializeField]
    private GameObject bullet;//THIS IS GAMEOBJECT OF BULLET FOR SHOOTING
    [SerializeField]
    private GameObject particle;//PARTICLE DAMAGE
    [SerializeField]
    private Slider slider_health;//SLIDER FOR HEALTH
    [SerializeField]
    private int bulletreload;//VALUE SHOOT BULLET FOR REALOAD
    [SerializeField]
    private float reloadtime;//TIME FOR RELOAD PLAYER

    private Rigidbody2D rb;
    private int bulletshoot; //THAT VALUE CURRENT SHOOT BULLET
    private Animator anim;
    private bool isjump;
    private bool Iscanshoot  = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CheckPlayerInput();
        UpdateSlider();
        CheckHealth();
        CheckValueBullet();
    }
    private void UpdateSlider()
    {
        slider_health.value = health;
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
        if(Iscanshoot)
        {
            GameObject _bullet = Instantiate(bullet, aim.position, aim.rotation);
            _bullet.GetComponent<Bullet>().SetBulletSpeed(bulletspeed);
            bulletshoot++;
            Destroy(_bullet, 3f);
        }
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
    public void ChangeHealth(int changehealth)
    {
        health += changehealth;
    }
    private void CheckHealth()
    {
        if(health <=0)
        {
            GameManager.ReloadLevel();
        }
    }
    private void CheckValueBullet()
    {
        if(bulletshoot == bulletreload)
        {
            Iscanshoot = false;
            StartCoroutine(Reload());
            bulletshoot = 0;
        }
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadtime);
        Iscanshoot = true;
    }
}
