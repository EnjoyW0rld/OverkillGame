using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Obsolete("SpiderString has been replaced with a generic LineForObjects")]
public class SpiderString : MonoBehaviour
{
    [SerializeField] GameObject SpiderPrefab;

    [Serializable]
    public struct SpiderSetup
    {
        public int spiderSize;
        [Range(0,1)]
        public float startPoint;
        public float speed;
        public bool goUp;


    }


    public List<SpiderSetup> enemyList;

    [SerializeField] float YMax = 5;
    [SerializeField] float YMin = 5;


    Vector3 topPoint;
    Vector3 bottomPoint;


    // Start is called before the first frame update
    void Start()
    {
        topPoint = this.transform.position + new Vector3(0, YMax);
        bottomPoint = this.transform.position - new Vector3(0, YMin);

        foreach(SpiderSetup setup in enemyList)
        {
            GameObject spiderGameObject = Instantiate(SpiderPrefab);

            SpiderEnemy spiderScript = spiderGameObject.GetComponent<SpiderEnemy>();

          //  spiderScript.Setup(setup.speed, topPoint, bottomPoint, setup.startPoint, setup.spiderSize, setup.goUp);

        }
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnDrawGizmos()
    {

        Vector3 topPoint = this.transform.position + new Vector3(0, YMax);
        Vector3 bottomPoint = this.transform.position - new Vector3(0, YMin);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(topPoint, bottomPoint);

       // Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.up);

        Gizmos.color = Color.yellow;

        if (enemyList == null) return;

        foreach(SpiderSetup spider in enemyList)
        {


            Vector3 startPoint = Vector3.Lerp(topPoint, bottomPoint, spider.startPoint);

            Gizmos.DrawWireSphere(startPoint, spider.spiderSize / 2.0f);


            Vector3 direction = Vector3.up * spider.speed;
            Vector3 lineRight = (Vector3.down + Vector3.right).normalized * 0.5f;
            Vector3 lineLeft = (Vector3.down + Vector3.left).normalized * 0.5f;

            if (!spider.goUp)
            {
                direction *= -1f;

                lineRight.y *= -1f;
                lineLeft.y *= -1f;

            }
            
            Vector3 point = startPoint + direction;

            Gizmos.DrawLine(startPoint, point);
            Gizmos.DrawLine(point, lineRight + point);
            Gizmos.DrawLine(point, lineLeft + point);

        }




    }
    
}
