using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* Wolfgang Gross
* Assignment 7
* Manages Spawns of Enemies and Powerups
* Score Management
*/

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9;

    public int enemyCount;
    public int waveNumber = 1;

    public bool gameStarted = false;
    public bool gameOver = false;

    private string tutorialText = 
        "AD - Pan Camera Left/Right" +
        "\nWS - Move Player Forward/Backward\n" +
        "\nKnock all enemies off the platform " +
        "\nin a round to proceed to the next wave\n\n\n\n" +
        "\nCollect Powerups to increase the knockback" +
        "\non enemies when you collide" +
        "\n(This will wear off after a little so use it wisely)\n" +
        "\nReach and complete Wave 10 to win!\n" +
        "\nPress the SPACEBAR whenever you" +
        "\nare ready to start!";

    public Text textbox1; //Tutorial Text -> look for 'SpaceBar' to turn off and start wave == middle screen
    public Text textbox2; //Wavenumber == top-left screen
    public Text textbox3; //Win/lose cond output == middle screen
    public Text textbox4; //Press r to restart == below win/lose

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 randPos = GenerateSpawnPosition();
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            //Instantiate the enemy in the random position
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

        }
    }

    private void SpawnPowerup(int powerupsToSpawn)
    {
        for (int i = 0; i < powerupsToSpawn; i++)
        {
            //Instantiate the enemy in the random position
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        //Generating a random position on the platform
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(!gameStarted)
        {
            //display tutorial text
            textbox1.text = tutorialText;
            textbox2.text = " ";
            textbox3.text = " ";
            textbox4.text = " ";
            if (Input.GetKeyDown("space"))
            {
                SpawnEnemyWave(waveNumber);
                SpawnPowerup(1);
                gameStarted = true;
            }
        }
        else
        {
            textbox1.text = " ";
            textbox2.text = "Wave Number: " + waveNumber;
            
            if (gameOver)
            {
                if (waveNumber > 10)
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
            else
            {
                if (enemyCount == 0)
                {
                    waveNumber++;
                    if (waveNumber > 10)
                    {
                        //Make sure don't spawn after 10
                        gameOver = true;
                    }
                    else
                    {
                        SpawnEnemyWave(waveNumber);
                        SpawnPowerup(1);
                    }
                }
            }
        }
        
    }
}