using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    // Reference to the player character
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the water
        if (other.gameObject == player)
        {
            // Trigger death sequence and reset player position
            Debug.Log("Player has fallen in water!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}
