using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float startingAngle;
    public bool foundPlayer = false;

    [SerializeField] private LayerMask layerMask;

    private Mesh mesh;
    public Vector3 origin;
    private float fieldOfView;
    private float viewDistance;


    private Vector3 VectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float AngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fieldOfView = 40f;
        viewDistance = 20f;
        //startingAngle = 45f;
    }
    private void LateUpdate()
    {

        int rayCount = 80;
        float angle = startingAngle;
        float angleIncrease = fieldOfView / rayCount;
        //origin = transform.position;

        Vector3[] verticies = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[verticies.Length];
        int[] triangles = new int[rayCount * 3];

        verticies[0] = origin - transform.position;

        int vertexIndex = 1;
        int triangleIndex = 0;
        foundPlayer = false;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, VectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {

                vertex = origin - transform.position + VectorFromAngle(angle) * viewDistance;
            }
            else
            {
                if (raycastHit2D.transform.gameObject.layer == GameManager.PlayerLayer)
                {

                    foundPlayer = true;
                }
                vertex = raycastHit2D.point - new Vector2(transform.position.x, transform.position.y);
            }
            verticies[vertexIndex] = vertex;

            if (i > 0)
            {


                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex += 1;
            angle -= angleIncrease;
        }



        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);



    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = AngleFromVector(aimDirection) + fieldOfView / 2f;
    }

}
