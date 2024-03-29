using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private PlayerCharacter player1;
    [SerializeField] private PlayerCharacter player2;
    [SerializeField] private int startPlayer = 1;
    [SerializeField] private float turnDelay = 1.0f;

    // Game loop
    private int playerTurn = 1;
    private bool isGameRunning = true;
    private Coroutine gameLoop = null;
    private int matchCount = 0;
    private int turnCount = 0;

    // Player stats
    public PlayerCharacter.PlayerStats GetCurrentPlayerStats()
    {
        if(playerTurn == 1)
        {
            return player1.Stats;
        }
        else
        {
            return player2.Stats;
        }
    }

    public PlayerCharacter.PlayerStats GetEnemyPlayerStats()
    {
        if (playerTurn == 1)
        {
            return player2.Stats;
        }
        else
        {
            return player1.Stats;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameLoop = StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        FileWriter file = FileWriter.instance;

        ++matchCount;
        file.NewFile($"MatchResults\\Match{matchCount}.txt");

        playerTurn = startPlayer;
        turnCount = 0;
        file.WriteToFile("Starting a new game with player 1 (");
        player1.Initialize();
        file.WriteToFile(") and player 2 (");
        player2.Initialize();
        file.WriteToFile($")\nFirst turn goes to player {startPlayer}\n\n");
        //Debug.Log($"First turn goes to player {startPlayer}");

        yield return new WaitForSeconds(turnDelay);

        while (isGameRunning)
        {
            if (playerTurn == 1)
            {
                ++turnCount;

                file.WriteToFile("Player 1: ");
                player1.RechargeEnergy();

                yield return new WaitForSeconds(turnDelay);

                int damageToOther = player1.MakeMove();
                bool isDead = player2.TakeDamage(damageToOther);

                WritePlayerStatsToFile();

                if (isDead)
                {
                    file.WriteToFile("Player 1 WON\n");
                    file.WriteToFile($"Battle took {turnCount} turns\n");
                    Debug.Log($"Winner: player 1; Duration: {turnCount} turns\n");
                    isGameRunning = false;
                    gameLoop = null;
                }

                playerTurn = 2;
                yield return new WaitForSeconds(turnDelay);
            }
            else if (playerTurn == 2)
            {
                ++turnCount;

                file.WriteToFile("Player 2: ");
                player2.RechargeEnergy();

                yield return new WaitForSeconds(turnDelay);

                int damageToOther = player2.MakeMove();
                bool isDead = player1.TakeDamage(damageToOther);

                WritePlayerStatsToFile();

                if (isDead)
                {
                    file.WriteToFile("Player 2 WON\n");
                    file.WriteToFile($"Battle took {turnCount} turns\n");
                    Debug.Log($"Winner: player 2; Duration: {turnCount} turns\n");
                    isGameRunning = false;
                    gameLoop = null;
                }

                playerTurn = 1;
                yield return new WaitForSeconds(turnDelay);
            }
            else
            {
                Debug.Log("invalid player turn");
            }
        }
    }

    private void WritePlayerStatsToFile()
    {
        FileWriter file = FileWriter.instance;
        file.WriteToFile("\nPlayer 1: ");
        player1.WriteStatsToFile();
        file.WriteToFile("\nPlayer 2: ");
        player2.WriteStatsToFile();
        file.WriteToFile("\n\n");
    }

    private void OnRestartGame()
    {
        if(isGameRunning)
        {
            StopCoroutine(gameLoop);
        }

        isGameRunning = true;
        gameLoop = StartCoroutine(GameLoop());
    }
}
