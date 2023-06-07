using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    //ADD SO PLAYER RESETS AT GRASS WHEN SANITY ENTIRELY DEPLETED

    public void OnTriggerStay(Collider other)
    {


        if (other.gameObject.TryGetComponent<BodyController>(out BodyController bc))
        {
            Debug.Log("DIED");
        }
    }
}
