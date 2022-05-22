﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStateMenu : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    public Image fadeImage;

    private void Start()
    {
        fadeImage.enabled = false;

        LevelManager.instance.onLevelWin.AddListener(OnWin);
        LevelManager.instance.onLevelLose.AddListener(OnLose);
    }

    void OnWin()
    {
        fadeImage.enabled = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        winUI.SetActive(true);
    }

    void OnLose()
    {
        fadeImage.enabled = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        loseUI.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < .75f)
        {
            percent += Time.deltaTime * speed;
            fadeImage.color = Color.Lerp(from, to, percent);
            yield return null;
        }
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
