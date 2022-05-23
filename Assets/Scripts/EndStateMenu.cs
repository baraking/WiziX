using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStateMenu : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    public Image fadeImage;

    [SerializeField] AudioSource myAudioSource;
    [SerializeField] AudioClip levelWinSFX;
    [SerializeField] AudioClip levelLoseSFX;

    private void Start()
    {
        fadeImage.enabled = false;

        LevelManager.instance.onLevelWin.AddListener(OnWin);
        LevelManager.instance.onLevelLose.AddListener(OnLose);

        myAudioSource = GetComponent<AudioSource>();
    }

    void OnWin()
    {
        DisablePlayersUI();
        fadeImage.enabled = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        winUI.SetActive(true);

        AudioManager.instance.VolumeDown();
        myAudioSource.clip = levelWinSFX;
        myAudioSource.Play();
    }

    void OnLose()
    {
        DisablePlayersUI();
        fadeImage.enabled = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        loseUI.SetActive(true);

        AudioManager.instance.VolumeDown();
        myAudioSource.clip = levelLoseSFX;
        myAudioSource.Play();
    }

    private void DisablePlayersUI()
    {
        if (!ReferenceEquals(LevelManager.instance.player1, null) && LevelManager.instance.player1.gameObject.active)
        {
            LevelManager.instance.player1.GetComponentInChildren<Canvas>().enabled = false;
        }

        if (!ReferenceEquals(LevelManager.instance.player2, null) && LevelManager.instance.player2.gameObject.active)
        {
            LevelManager.instance.player2.GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    public void NextLevel()
    {
        Debug.Log("Scenes count: " + SceneManager.sceneCountInBuildSettings);
        if(SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            ReturnToMainMenu();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ReturnToMainMenu()
    {
        AudioManager.instance.PlayMenuTheme();
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
