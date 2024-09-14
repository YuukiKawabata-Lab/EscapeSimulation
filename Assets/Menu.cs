using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickStart2()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void END()
    {
        UnityEngine.Application.Quit();
    }
}
