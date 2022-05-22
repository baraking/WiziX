using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void OnePlayerOption()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TwoPlayeresOption()
    {
        Debug.Log("2 playeres option");
    }

    public void ExitGameOption()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }
}
