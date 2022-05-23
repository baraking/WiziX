using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
    [SerializeField] GameObject audioManager;

    public UnityEvent onLevelWin;
    public UnityEvent onLevelLose;

    public GameSessionData sessionData;

    public int numberOfPlayers;
    public int numberOfActivePlayers;

    public GameObject player1;//may be needed in the future
    public GameObject player2;

    private void Start()
    {
        if (ReferenceEquals(AudioManager.instance, null))
        {
            Instantiate(audioManager);
        }

        numberOfPlayers = sessionData.numberOfPlayers;
        numberOfActivePlayers = sessionData.numberOfPlayers;

        if (numberOfPlayers == 2 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            player2.SetActive(true);
        }
    }

    public void CheckForWin()
    {
        if (BallsManager.instance.allBalls.Count < 1)
        {
            onLevelWin?.Invoke();
            onLevelWin.RemoveAllListeners();
        }
    }

    public void CheckForLose()
    {
        if (numberOfActivePlayers < 1)
        {
            onLevelLose?.Invoke();
            onLevelLose.RemoveAllListeners();
        }
    }
}
