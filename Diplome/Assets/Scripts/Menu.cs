using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Play()
    {
        SceneManager.LoadScene(1);
    }
    private void Info()
    {
        SceneManager.LoadScene(2);
    }
}
