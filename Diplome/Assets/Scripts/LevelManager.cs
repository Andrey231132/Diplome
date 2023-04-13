using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("currentlevel", SceneManager.GetActiveScene().buildIndex);
    }
    public void SetScene(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void ContinueButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentlevel"));
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
