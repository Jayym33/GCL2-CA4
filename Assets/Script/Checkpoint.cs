using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Detect when something enters the checkpoint
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the player
        if (other.CompareTag("Player"))
        {
            // Get the RespawnManager from the player
            RespawnManager respawnManager = other.GetComponent<RespawnManager>();

            // If the player has a RespawnManager
            if (respawnManager != null)
            {
                // Set this checkpoint as the player's new respawn point
                respawnManager.SetCheckpoint(transform);
            }
        }
    }
}
