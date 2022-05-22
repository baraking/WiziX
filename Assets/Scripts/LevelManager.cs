using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : GenericSingleton<LevelManager>
{
    public UnityEvent onLevelWin;
    public UnityEvent onLevelLose;

    public GameSessionData sessionData;

    public int numberOfPlayers;
    public int numberOfActivePlayers;

    //add list of players.

    private void Start()
    {
        numberOfPlayers = sessionData.numberOfPlayers;
        numberOfActivePlayers = sessionData.numberOfPlayers;

        //instantiate player 2 if needed
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
