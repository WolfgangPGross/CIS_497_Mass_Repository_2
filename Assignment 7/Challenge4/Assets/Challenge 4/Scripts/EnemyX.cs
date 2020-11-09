using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Wolfgang Gross
* Assignment 7
* Enemy Behavior
* Move to Player Goal
*/

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    public GameObject spawnManager;

    public float waveMult;
    public float waveMult2;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.FindGameObjectWithTag("Player Goal");
        spawnManager = GameObject.Find("Spawn Manager");
    }

    // Update is called once per frame
    void Update()
    {
        waveMult = spawnManager.GetComponent<SpawnManagerX>().waveCount;
        waveMult2 = waveMult / 2; 

        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime * (waveMult / 2));

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            spawnManager.GetComponent<SpawnManagerX>().gameOver = true;
            Destroy(gameObject);
        }

    }

}
