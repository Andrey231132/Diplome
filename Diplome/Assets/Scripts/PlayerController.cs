using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private AudioSource playersoundrun;//JUST PLAYER SOUND -  RUN
    [SerializeField]
    private AudioSource playersoundjump;//JUST PLAYER SOUND -  JUMP
    [SerializeField]
    private AudioSource playersoundshoot;//JUST PLAYER SOUND -  SHOOT
    [SerializeField]
    private LayerMask Ground;

    private Rigidbody2D rb;
    private int bulletshoot; //THAT VALUE CURRENT SHOOT BULLET
    private Animator anim;
    //private bool isgroundstay;
    private bool Iscanshoot  = true;
    private int moneysonlevel;
    private Element playerelement;
    private CapsuleCollider2D coll;
    void Awake()
    {
        GameManager.timerecord = reloadtime;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        SetAudio();
    }
    void Update()
    {
        CheckPlayerInput();
        UpdateSlider();
        CheckHealth();
        CheckValueBullet();
        playerspeed = GameManager.speed;
        reloadtime = GameManager.timerecord;
        //Debug.Log(GameManager.Money());
    }
    private void SetAudio()
    {
        playersoundrun.volume = PlayerPrefs.GetFloat("musicvalue");
        playersoundjump.volume = PlayerPrefs.GetFloat("musicvalue");
        playersoundshoot.volume = PlayerPrefs.GetFloat("musicvalue");
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
        if(Input.GetKeyDown(KeyCode.W) && IsJump() == true)
        {
            rb.AddForce(transform.up * jumpforce);
            playersoundjump.Play();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if(x!=0)
        {
            anim.SetBool("Run", true);
            if(isgroundstay())
            {
                playersoundrun.mute = false;
            }
            else
            {
                playersoundrun.mute = true;
            }
        }
        else
        {
            anim.SetBool("Run", false);
            playersoundrun.mute = true;
        }
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        Instantiate(particle, transform.position, Quaternion.identity);
    }
    private void Shoot()
    {   
        if(Iscanshoot)
        {
            GameObject _bullet = Instantiate(bullet, aim.position, aim.rotation);
            _bullet.GetComponent<Bullet>().SetBulletSpeed(bulletspeed);
            _bullet.GetComponent<Bullet>().SetElement(playerelement);
            bulletshoot++;
            playersoundshoot.Play();
            Destroy(_bullet, 3f);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        isgroundstay();
        if(col.gameObject.name == "money" ||col.gameObject.name == "money(Clone)")
        {
            moneysonlevel++;
            GameManager.GetMoney(1);
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Shrine")
        {
            playerelement = col.gameObject.GetComponent<Shrine1>().GetElement();
        }
    }
    private bool IsJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0,1f,0), -Vector2.up, 0.1f);
        if(hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public GameObject GetBullet()
    {
        return bullet;
    }
    public void SetHealth(int h)
    {
        health+=h;
    }
    private void CheckHealth()
    {
        if(health <=0)
        {
            GameManager.GetMoney(-moneysonlevel);
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
    private bool isgroundstay()
    {
        var collider = GetComponent<Collider2D>();
        return Physics2D.OverlapCircle(collider.bounds.center, collider.bounds.size.x);
    }
    private void OnDrawGizmos()
    {
        var collider = GetComponent<Collider2D>();
        Gizmos.DrawWireSphere(collider.bounds.center, collider.bounds.size.x);
    }
}
