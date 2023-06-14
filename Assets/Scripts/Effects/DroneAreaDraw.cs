using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DroneAreaDraw : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] triangles;
    private Mesh mesh;
    private Vector2[] uvs;
    [SerializeField] private Drone drone;
    private void Start()
    {
        drone = transform.parent.GetComponent<Drone>();
        if (drone == null)
        {
            Debug.LogError("No drone in parent found");
            return;
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        SetVertices();
        UpdateMesh();
    }

    private void SetVertices()
    {
        Vector3[] points = drone.GetRayPoints();
        vertices = new Vector3[]
        {
            points[0] - drone.transform.position,
            transform.position - drone.transform.position,
            points[1] - drone.transform.position
        };
        uvs = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(.5f,1),
            new Vector2(1,0)
        };

        triangles = new int[]
        {
            2,1,0
        };
    }
    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        //mesh.RecalculateNormals();
        //mesh.RecalculateUVDistributionMetrics();
    }
    private void OnDrawGizmos()
    {
        if (drone == null) return;
        Vector3 pos = transform.position;
        pos.y -= drone.GetRange()/2f;
        //Gizmos.DrawWireSphere(pos, 1);
    }
}
