using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{ 
    public float fieldOfView = 90f;
    public int rayCount = 2;
    public float viewDistance = 8f;
    private Mesh mesh;

    private LayerMask layerMask;
    // private int playerLayer;
    // private int obstructionLayer;

    // public enum HitType
    // {
    //     None = -1,
    //     Player,
    //     Obstruction
    // }

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        layerMask = LayerMask.GetMask("Player", "Obstruction");
        // playerLayer = LayerMask.NameToLayer("Player");
        // obstructionLayer = LayerMask.NameToLayer("Obstruction");
    }

    private void Update(){
        Vector3 origin = Vector3.zero;
        float angle = 0f;
        float angleIncrement = fieldOfView / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D hit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (hit.collider == null) {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else {
                vertex = hit.point;
                print("player hit!");
            }
            
            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -=angleIncrement;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }


    private static Vector3 GetVectorFromAngle(float angle) 
    {    
        float angleRadius = angle * (Mathf.PI / 180f);
        
        return new Vector3(Mathf.Cos(angleRadius), Mathf.Sin(angleRadius));
    }
}