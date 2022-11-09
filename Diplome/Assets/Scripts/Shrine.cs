using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shrine : MonoBehaviour
{
    [SerializeField]
    private GameObject boxtext;
    [SerializeField]
    private float timelivetext;
    [SerializeField]
    private float speedup;
    [SerializeField]
    private Sprite completeshrinesprite;

    private bool iscomplete;
    private Transform currentboxtext;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && col.gameObject.GetComponent<PlayerController>()&& !iscomplete)
        {
            ChangeGunSettings(col.gameObject.GetComponent<PlayerController>());
            spriteRenderer.sprite = completeshrinesprite;
            iscomplete = true;
        }
    }
    private void SpawnText(string text, Vector2 position)
    {
        GameObject _boxtext = Instantiate(boxtext, position, Quaternion.identity);
        currentboxtext = _boxtext.transform;
        _boxtext.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        Destroy(_boxtext, timelivetext);
    }
    private void ChangeGunSettings(PlayerController player)
    {
        int random = Random.Range(1,7);
        switch (random)
        {
            case 1: player.GetBullet().GetComponent<Bullet>().ChangeDamage(1);SpawnText("BulletDamage + 1", transform.position); break;
            case 2: player.GetBullet().GetComponent<Bullet>().ChangeDamage(-1); SpawnText("BulletDamage - 1", transform.position);break;
            case 3: player.ChangeBulletSpeed(400f);SpawnText("BulletSpeed + 400", transform.position); break;
            case 4: player.ChangeBulletSpeed(400f); SpawnText("BulletDamage - 400", transform.position); break;
            case 5: player.ChangeHealth(-1);SpawnText("Health - 1", transform.position); break;
            case 6: player.ChangeHealth(1);SpawnText("Health + 1", transform.position); break;
        }      
    }
    private void Update()
    {
        if(currentboxtext)
        {
            currentboxtext.position += new Vector3(0f, speedup, 0f) * Time.deltaTime;
        }
    }
}
