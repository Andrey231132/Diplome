using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}
    private static int moneys;

    public static float size=1f;
    public static float timerecord;
    public static int damage;
    public static void GetMoney(int coins)
    {
        moneys += coins;
    }
    public static void Buy(int price)
    {
        moneys-=price;
    }
    public static int Money()
    {
        return moneys;
    }
    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    public static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
        
    }
}
