using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void SetScene(int number)
    {
        SceneManager.LoadScene(number);
    }
}
