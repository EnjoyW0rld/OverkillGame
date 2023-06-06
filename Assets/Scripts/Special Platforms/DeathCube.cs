using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{

    //RespawnScript of the player (Only one player in game)
    Respawning_Player playerRespawn;

    /// <summary>
    /// Respawns the player when it touches the cube
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body")
        {
            if (playerRespawn == null)
            {
                playerRespawn = other.gameObject.GetComponent<Respawning_Player>();
            }

            playerRespawn.Respawn();
        }
    }
}
