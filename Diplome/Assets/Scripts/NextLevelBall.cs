using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelBall : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textleveltime;

    private float timelevel;
    private void Start()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        while(true)
        {
            timelevel++;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
    private void Update()
    {
        textleveltime.text = timelevel.ToString() + "s";
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<PlayerController>())
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, timelevel);
            GameManager.NextLevel();
        }
    }
}
