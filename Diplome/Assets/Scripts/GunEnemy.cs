using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    [SerializeField]
    private StateEnemy _stateenemy;

    [SerializeField]
    private Transform A_place;
    [SerializeField]
    private Transform B_place;
    [SerializeField]
    private Transform _raycastplace;

    private Transform _currentplace;
    private Transform _player;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timebetwenshoot;
    [SerializeField]
    private float _timestay;
    [SerializeField]
    private float _seeplayerdistance;
    [SerializeField]
    private float _atackplayerdistance;

    private Animator anim;

    //ƒополнителтьное_помогающее
    private bool Isdetectplayer;
    private void Start()
    {
        _currentplace = A_place;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Logic();
        CheckEnemyState();
    }
    private void Logic()
    {
        if(CheckPlayer())
        {
            _stateenemy = StateEnemy.Follow;
        }
        else if(Isdetectplayer)//≈сли мы видели игрока,но он скрылс€ из ввиду
        {
            _stateenemy = StateEnemy.Stay;
        }
        else if(_stateenemy != StateEnemy.Stay)
        {
            _stateenemy = StateEnemy.Patrule;
        }
        if(CheckPlayer() && Vector2.Distance(new Vector2(_player.position.x, 0), new Vector2(gameObject.transform.position.x, 0)) <= _atackplayerdistance)
        {//≈сли враг видет игрока и рассто€ние между ними равно расто€ние атаки то враг атакует
            _stateenemy = StateEnemy.Shoot;
        }
        if(_stateenemy == StateEnemy.Patrule && Vector2.Distance(new Vector2(_currentplace.position.x, 0), new Vector2(gameObject.transform.position.x, 0)) <= 0.5f)
        {//≈сли враг патрилирует и если он дошел до точки то мен€ем ему точку
            _stateenemy = StateEnemy.Stay;
            if (_currentplace == A_place)
            {
                _currentplace = B_place;
            }
            else if (_currentplace == B_place)
            {
                _currentplace = A_place;
            }
        }
    }
    private void CheckEnemyState()
    {
        switch(_stateenemy)
        {
            case StateEnemy.Follow:
            {
                    Following();
                    StopCoroutine(Staying());
                    StopCoroutine(Shooting());
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
                    StopCoroutine(Staying());
                    break;
            }
            case StateEnemy.Patrule:
            {
                    StopCoroutine(Shooting());
                    StopCoroutine(Staying());
                    Patruling();
                    break;
            }
        }
    }
    private bool CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(_raycastplace.position,transform.right,_seeplayerdistance);
        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>() || hit.collider.gameObject.GetComponent<shild1>())
            {
                Isdetectplayer = true;
                _player = hit.collider.gameObject.transform;
                return true;
            }
            else 
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
        if(transform.position.x > _currentplace.position.x)
        {
            transform.position -= new Vector3(_speed,0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.x < _currentplace.position.x)
        {
            transform.position += new Vector3(_speed, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        anim.SetBool("Run", true);
    }
    private void Following()
    {
        if(_player && Vector2.Distance(_player.position, gameObject.transform.position) > _atackplayerdistance)
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
        anim.SetBool("Run",false);
        yield return new WaitForSeconds(_timestay);
        Isdetectplayer = false;
        _stateenemy = StateEnemy.Patrule;
    }
    private IEnumerator Shooting()
    {
        if (gameObject.transform.position.x >= _player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (gameObject.transform.position.x <= _player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        anim.SetBool("Run", false);
        yield return new WaitForSeconds(_timebetwenshoot);
        
    }
}
public enum StateEnemy
{
    Follow,
    Stay,
    Shoot,
    Patrule
}
