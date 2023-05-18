using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    [SerializeField] float sanity = 100.0f;
    [SerializeField] float reduceSpeed = 1.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ReduceSanity(reduceSpeed);

    }


    public void ReduceSanity(float amount)
    {
        sanity -= amount * Time.deltaTime;
    }

    public void AddSanity(float amount)
    {
        
        sanity += amount * Time.deltaTime;

        sanity = Mathf.Min(100.0f, sanity);
    }
}
