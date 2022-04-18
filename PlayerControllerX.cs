using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 2f;
    private GameObject focalPoint;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 8;
    public ParticleSystem turboSmoke;
    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 24f;turboSmoke.Play();}else{
            speed = 7f;
        }
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed ); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, 0.2f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position-transform.position;
            awayFromPlayer = awayFromPlayer.normalized;
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
        if (other.gameObject.name == "Enemy Goal")
        {
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemigos.Length; i++)
            {
                Destroy(enemigos[i]);
            }
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().SpawnEnemyWave(GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().waveCount);
        } 
    }



}
