using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    private int score;

    public bool isGameActive;

    public Button restartButton;

    public GameObject titleScreen;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }


    public void startGame(int difficulty)
    {
        spawnRate /= difficulty;

        scoreText.gameObject.SetActive(true);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);

    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score < 0)
        {
            score = 0;
        }

        scoreText.text = "Score: " + score;
    }


    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            //Wait 1 second
            yield return new WaitForSeconds(spawnRate);
            
            //Pick a random index between 0 and number of prefabs
            int index = Random.Range(0, targets.Count);
            
            //Spawn the prefab at the randomly selected index
            Instantiate(targets[index]);

            //Testing UpdateScore by adding 5 every time a new target spawns
            //UpdateScore(5);
        }
    }



    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
