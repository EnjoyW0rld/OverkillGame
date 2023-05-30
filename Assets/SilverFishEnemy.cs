using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverFishEnemy : MonoBehaviour
{

    private float currentPositionPercentage;

    private Vector3 topPoint;
    private Vector3 bottomPoint;

    [SerializeField] private float speed = .001f;
    // Start is called before the first frame update


    public void Setup(float speed, Vector3 topPoint, Vector3 bottomPoint, float startPoint, float size, bool goUp)
    {
        if (goUp) speed *= -1f;

        this.speed = speed;

        this.topPoint = topPoint;
        this.bottomPoint = bottomPoint;

        this.transform.localScale = new Vector3(size, size, size);

        currentPositionPercentage = startPoint;

        this.transform.position = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);

    }

    // Update is called once per frame
    void Update()
    {

        currentPositionPercentage += Time.deltaTime * speed;

        currentPositionPercentage = Mathf.Clamp(currentPositionPercentage, 0, 1);

        if (currentPositionPercentage == 0 || currentPositionPercentage == 1) speed *= -1;
        this.transform.position = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);

    }
}
