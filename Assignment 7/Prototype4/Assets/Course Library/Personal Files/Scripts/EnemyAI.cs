using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Wolfgang Gross
* Assignment 7
* Enemy Behavior
* Move toward player
*/

public class EnemyAI : MonoBehaviour
{
    public Rigidbody enemyRB;
    public GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Add force toward the direction from the player to the enemy
        Vector3 lookDirection = (player.transform.position - transform.position).normalized; //Only direction

        //Add force toward player
        enemyRB.AddForce(lookDirection * speed);

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
