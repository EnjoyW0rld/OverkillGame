using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{


    Respawning_Player playerRespawn;

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
