using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform A_place;
    [SerializeField]
    private Transform B_place;
    [SerializeField]
    private Transform _raycastplace;

    private Transform _currentplace;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timestay;
    [SerializeField]
    private float _seeplayerdistance;

    private bool IsFollow;
    private bool IsPatrule=true;
    private bool IsShoot;
    private bool IsStay;

    private Animator anim;

    //Дополнителтьное_помогающее
    private void Start()
    {
        _currentplace = A_place;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(IsPatrule)
        {
            StartCoroutine(Patruling());
        }
        if (IsStay)
        {
            StartCoroutine(Staying());
        }
        if(CheckPlayer())//Если метод возращает true враг начинает преследовать.Метод проверяет рейкаст.
        {
            IsPatrule = false;
            IsStay = false;
            IsFollow = true;
        }
    }
    private bool CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(_raycastplace.position, transform.right, _seeplayerdistance);
        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>() || hit.collider.gameObject.GetComponent<shild1>())
            {
                Debug.Log("Player");
                return true;
            }
            else
            {
                Debug.Log("No Player");
                return false;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_raycastplace.position, transform.right * -_seeplayerdistance);
    }
    private IEnumerator Patruling()
    {
        if (gameObject.transform.position.x >= _currentplace.position.x)
        {
            transform.position -= new Vector3(_speed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (gameObject.transform.position.x <= _currentplace.position.x)
        {
            transform.position += new Vector3(_speed, 0, 0) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(_currentplace.position.x - gameObject.transform.position.x) <= 1f)//Если дистанция между точкой и врагом меньше или равно числу то смени точку
        {
            if (_currentplace == A_place)
            {
                _currentplace = B_place;
                IsStay = true;
                IsPatrule = false;
            }
            else if (_currentplace == B_place)
            {
                _currentplace = A_place;
                IsStay = true;
                IsPatrule = false;
            }
        }
        anim.SetBool("Run", true);
        yield return null;
    }
    private IEnumerator Following()
    {
        yield return null;
    }
    private IEnumerator Staying()
    {
        anim.SetBool("Run",false);
        yield return new WaitForSeconds(_timestay);
        IsPatrule = true;
        IsStay = false;
    }
    private IEnumerator Shooting()
    {
        yield return null;
    }
}
