using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    private StateEnemy _stateenemy;

    [SerializeField]
    private Element _element;

    [SerializeField]
    private Transform A_place;
    [SerializeField]
    private Transform B_place;
    [SerializeField]
    private Transform _raycastplace;
    [SerializeField]
    private GameObject _particaldamage;
    [SerializeField]
    private GameObject _dropmoney;
    [SerializeField]
    private GameObject _shootbullet;
    [SerializeField]
    private Transform aim;//ќткуда вылетают пули

    private Transform _currentplace;
    private Transform _player;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _bulletspeed;
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _timebetwenshoot;
    [SerializeField]
    private float _timestay;
    [SerializeField]
    private float _seeplayerdistance;
    [SerializeField]
    private float _atackplayerdistance;

    private Animator anim;
    private AudioSource audio;

    //ƒополнителтьное_помогающее
    private bool Isdetectplayer;
    private bool IsShoot;
    private void Start()
    {
        _currentplace = A_place;

        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        SetAudio();
    }
    private void Update()
    {
        Logic();
        CheckEnemyState();
        CheckHealth();
    }
    private void Logic()
    {
        if(CheckPlayer() && Isdetectplayer && _stateenemy != StateEnemy.Shoot)
        {
            _stateenemy = StateEnemy.Follow;
        }
        if (!CheckPlayer() && Isdetectplayer)//≈сли мы видели игрока,но он скрылс€ из ввиду
        {
            _stateenemy = StateEnemy.Stay;
        }
        if (!CheckPlayer()  && !Isdetectplayer)
        {
            _stateenemy = StateEnemy.Patrule;
        }
        if (CheckPlayer() && Vector2.Distance(new Vector2(_player.position.x, 0), new Vector2(gameObject.transform.position.x, 0)) <= _atackplayerdistance)
        {//≈сли враг видет игрока и рассто€ние между ними равно расто€ние атаки то враг атакует
            _stateenemy = StateEnemy.Shoot;
        }
        if (Vector2.Distance(new Vector2(_currentplace.position.x, 0), new Vector2(gameObject.transform.position.x, 0)) <= 0.1f && _stateenemy == StateEnemy.Patrule)
        {//≈сли враг патрилирует и если он дошел до точки то мен€ем ему точку
            _stateenemy = StateEnemy.Stay;
            ChangePlacePatrule();
        }
    }
    private void ChangePlacePatrule()
    {
        if (_currentplace == A_place)
        {
            _currentplace = B_place;
        }
        else if (_currentplace == B_place)
        {
            _currentplace = A_place;
        }
    }
    private void CheckEnemyState()
    {
        switch (_stateenemy)
        {
            case StateEnemy.Follow:
                {
                    Following();
                    break;
                }
            case StateEnemy.Stay:
                {
                    StartCoroutine(Staying());
                    break;
                }
            case StateEnemy.Shoot:
                {
                    StartCoroutine(Shooting());
                    break;
                }
            case StateEnemy.Patrule:
                {
                    Patruling();
                    break;
                }
        }
    }
    private bool CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(_raycastplace.position, transform.right, _seeplayerdistance);
        if (hit)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<PlayerController>() || hit.collider.gameObject.GetComponent<shild1>())
            {
                Isdetectplayer = true;
                _player = hit.collider.gameObject.transform;
                return true;
            }
            else if(!hit.collider.gameObject.GetComponent<PlayerController>() || !hit.collider.gameObject.GetComponent<shild1>())
            {
                return false;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_raycastplace.position, transform.right * _seeplayerdistance);
    }
    private void Patruling()
    {
        if (transform.position.x > _currentplace.position.x)
        {
            transform.position -= new Vector3(_speed, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.x < _currentplace.position.x)
        {
            transform.position += new Vector3(_speed, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        anim.SetBool("Run", true);
        if (Vector2.Distance(new Vector2(_currentplace.position.x, 0), new Vector2(gameObject.transform.position.x, 0)) <= 0.1f && _stateenemy == StateEnemy.Patrule)
        {//≈сли враг патрилирует и если он дошел до точки то мен€ем ему точку
            _stateenemy = StateEnemy.Stay;
        }
    }
    private void Following()
    {
        if (_player && Vector2.Distance(_player.position, gameObject.transform.position) > _atackplayerdistance)
        {
            if (gameObject.transform.position.x >= _player.position.x)
            {
                transform.position -= new Vector3(_speed, 0, 0) * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (gameObject.transform.position.x <= _player.position.x)
            {
                transform.position += new Vector3(_speed, 0, 0) * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    private IEnumerator Staying()
    {
        anim.SetBool("Run", false);
        yield return new WaitForSeconds(_timestay);
        Isdetectplayer = false;
    }
    private IEnumerator Shooting()
    {
        if(!IsShoot)
        {
            IsShoot = true;
            if (gameObject.transform.position.x >= _player.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (gameObject.transform.position.x <= _player.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            anim.SetBool("Run", false);
            SpawnBullet();
            yield return new WaitForSeconds(_timebetwenshoot);
            IsShoot = false;
        }
    }
    private void SpawnBullet()
    {
        GameObject _bullet = Instantiate(_shootbullet, aim.position, Quaternion.identity);
        if (transform.eulerAngles.y == 0)
        {
            _bullet.GetComponent<Bullet>().SetBulletSpeed(_bulletspeed);
        }
        else if (transform.eulerAngles.y == 180)
        {
            _bullet.GetComponent<Bullet>().SetBulletSpeed(-_bulletspeed);
        }
    }
    private void SetAudio()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("musicvalue");
    }
    public void GetDamage(int damage)
    {
        audio = GetComponent<AudioSource>();
        _health -= damage;
        Instantiate(_particaldamage, transform.position, Quaternion.identity);
        Instantiate(_dropmoney, transform.position, Quaternion.identity);
        audio.Play();
    }
    private void CheckHealth()
    {
        if (_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Instantiate(_particaldamage, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            Bullet bullet = col.gameObject.GetComponent<Bullet>();
            if (col.gameObject.tag == "PlayerBullet" && !CheckElementBullet(bullet))
            {
                GetDamage(bullet.GetDamage());
            }
            else if (col.gameObject.tag == "PlayerBullet" && CheckElementBullet(bullet))
            {
                GetDamage(bullet.GetDamage() * 2);
            }
        }
    }
    private bool CheckElementBullet(Bullet bul)
    {
        if (bul.GetElement() == _element)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public enum StateEnemy
{
    Follow,
    Stay,
    Shoot,
    Patrule
}