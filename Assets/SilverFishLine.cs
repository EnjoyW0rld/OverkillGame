using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Obsolete("SilverFishLine has been replaced with a generic LineForObjects")]
public class SilverFishLine : MonoBehaviour
{

    [SerializeField] GameObject SpiderPrefab;

    [Serializable]
    public struct SilverFishSetup
    {
        public int spiderSize;
        [Range(0, 1)]
        public float startPoint;
        public float speed;
        public bool goUp;


    }


    public List<SilverFishSetup> enemyList;

    [SerializeField] float xMin;
    [SerializeField] float xMax;


    Vector3 topPoint;
    Vector3 bottomPoint;


    // Start is called before the first frame update
    void Start()
    {
        topPoint = this.transform.position + new Vector3(xMax, 0);
        bottomPoint = this.transform.position - new Vector3(xMin, 0);

        foreach (SilverFishSetup setup in enemyList)
        {
            GameObject spiderGameObject = Instantiate(SpiderPrefab);

            SilverFishEnemy spiderScript = spiderGameObject.GetComponent<SilverFishEnemy>();

           // spiderScript.Setup(setup.speed, topPoint, bottomPoint, setup.startPoint, setup.spiderSize, setup.goUp);

        }


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnDrawGizmos()
    {

        Vector3 topPoint = this.transform.position + new Vector3(xMax, 0);
        Vector3 bottomPoint = this.transform.position - new Vector3(xMin, 0);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(topPoint, bottomPoint);

        // Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.up);

        Gizmos.color = Color.yellow;

        if (enemyList == null) return;

        foreach (SilverFishSetup spider in enemyList)
        {


            Vector3 startPoint = Vector3.Lerp(topPoint, bottomPoint, spider.startPoint);

            Gizmos.DrawWireCube(startPoint, new Vector3(spider.spiderSize / 2.0f, spider.spiderSize / 2.0f, spider.spiderSize / 2.0f));


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
