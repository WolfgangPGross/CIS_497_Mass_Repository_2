using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Wolfgang Gross
* Assignment 7
* Controls the player
* --
*/

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed;
    private float forwardInput;

    private GameObject focalPoint;

    public bool hasPowerup;
    private float powerupStrength = 15f;
    private float playerYPos;

    public GameObject powerupIndicator;

    public SpawnManager spawn;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
        
    }

    // Update is called once per frame
    void Update()
    {
        playerYPos = gameObject.transform.position.y;

        if(spawn.gameStarted == false)
        {
            if (playerYPos < -5)
            {
                playerRB.velocity = Vector3.zero;
                playerRB.angularVelocity = Vector3.zero;


                gameObject.transform.position = new Vector3(0, 0, 0);
                //gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
        else if(playerYPos < -5)
        {
            spawn.gameOver = true;
        }

        forwardInput = Input.GetAxis("Vertical");

        //Move our powerup indicator to the ground below our player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
    }
    private void FixedUpdate()
    {
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Player collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);

            //Get local ref to enemy rb
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            //Set a Vector3 with a direction away from the player
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            //Add force away from player
            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
    }
}
