using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {


        if (other.gameObject.TryGetComponent<BodyController>(out BodyController bc))
        {
            Debug.Log("DIED");
        }
    }
}
