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
    [SerializeField] private float turnDelay = 1;
    private int playerTurn = 1;
    private bool isGameRunning = true;

    [Header("Stats")]
    [SerializeField] private PlayerCharacter.PlayerStats playerInitialStats;

    // Start is called before the first frame update
    void Start()
    {
        // Select player behaviors and start game
        // Add restart game function
        player1.Initialize(playerInitialStats);
        player2.Initialize(playerInitialStats);

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(turnDelay);

        while (isGameRunning)
        {
            if (playerTurn == 1)
            {
                Debug.Log("Player 1 turn");
                int damageToOther = player1.MakeMove();
                Debug.Log($"Doing {damageToOther} damage");
                bool isDead = player2.TakeDamage(damageToOther);

                if (isDead)
                {
                    Debug.Log("Player 2 died");
                    isGameRunning = false;
                    // TODO: announce winner
                }

                playerTurn = 2;
                yield return new WaitForSeconds(turnDelay);
            }
            else if (playerTurn == 2)
            {
                Debug.Log("Player 2 turn");
                int damageToOther = player2.MakeMove();
                Debug.Log($"Doing {damageToOther} damage");
                bool isDead = player1.TakeDamage(damageToOther);

                if (isDead)
                {
                    Debug.Log("Player 1 died");
                    isGameRunning = false;
                    // TODO: announce winner
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

    void Update()
    {
        if(!isGameRunning)
        {
            // Wait for restart
            return;
        }
    }
}
