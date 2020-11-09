using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* Wolfgang Gross
* Assignment 7
* Manages Spawn of Enemies and Powerups
* Score management
*/

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;

    public EnemyX enemy;

    public GameObject player;

    public bool gameOver = false;
    public bool gameStarted = false;

    public string tutorialText2 = "AD - Pan Camera Left/Right" +
        "\nWS - Move Player Forward/Backward\n" +
        "\nKnock all enemies Into the opposing goal" +
        "\nin a round to proceed to the next wave" +
        "\nCollect Powerups to increase the knockback" +
        "\non enemies when you collide" +
        "\n(This will wear off after a little so use it wisely)\n" +
        "\nPress Spacebar to gain a swift boost forwards" +
        "\nReach and complete Wave 10 to win!\n" +
        "\nPress the SPACEBAR whenever you" +
        "\nare ready to start!";

    public Text textbox1; //Tutorial Text -> look for 'SpaceBar' to turn off and start wave == middle screen
    public Text textbox2; //Wavenumber == top-left screen
    public Text textbox3; //Win/lose cond output == middle screen
    public Text textbox4; //Press r to restart == below win/lose

    private void Start()
    {
        //enemy = FindObjectsOfType<EnemyX>();
    }


    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (!gameStarted)
        {
            //display tutorial text
            textbox1.text = tutorialText2;
            textbox2.text = " ";
            textbox3.text = " ";
            textbox4.text = " ";
            if (Input.GetKeyDown("space"))
            {
                SpawnEnemyWave(waveCount);
                 gameStarted = true;
            }
        }
        else
        {
            textbox1.text = " ";
            textbox2.text = "Wave Number: " + waveCount;

            if (!gameOver)
            {
                if (enemyCount == 0)
                {
                    if(waveCount > 10)
                    {
                        //Game Over
                        gameOver = true;
                    }
                    else
                    {
                        SpawnEnemyWave(waveCount);
                    }
                }
            }
            else
            {
                if(waveCount > 10)
                {
                    //You Win!
                    textbox3.text = "You Win!";
                    textbox4.text = "Press R to play again";
                }
                else
                {
                    //You lose!
                    textbox3.text = "You Lose!";
                    textbox4.text = "Press R to try again";
                }
                //Reset scene if R held down
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;
        ResetPlayerPosition(); // put player back at start

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
