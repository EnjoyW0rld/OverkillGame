using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    Quaternion startRotation;



    public void Awake()
    {
        startRotation= transform.localRotation;
    }
    public void ResetRotationObject(float time = 1)
    {
        StartCoroutine(ResetRotationIEnumarator(time));
    }


    private IEnumerator ResetRotationIEnumarator(float time)

    {
        Debug.Log("dadad");
        Quaternion rotation = this.transform.localRotation;
        float timeSpend = 0;

        do
        {

            //   Debug.Log(Quaternion.Slerp(rotation, startRotation, timeSpend / time));

         //   Debug.Log(timeSpend / time);
            Quaternion newRotation = Quaternion.Slerp(rotation, startRotation, timeSpend / time);

            transform.localRotation = newRotation;

            Debug.Log(transform.localRotation);
            Debug.Log(newRotation);
            Debug.Log(transform.localRotation == newRotation);
            timeSpend += Time.deltaTime;
            yield return 0;

        } while (timeSpend <= time);
    }
}
