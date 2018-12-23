using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Matrix : MonoBehaviour
{
    int voxelInWidth = 4;
    int voxelInHeight = 4;
    float width = 4.0f;
    float height = 4.0f;

    float size = 0.2f;
    Mesh mesh;
    Vector3[] vertices;

	private void Start()
	{
        Generator();
	}
    //vertices test
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.black;
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
	}

	void Generator()
    {
        //create mesh object
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        float stripe = width / voxelInWidth;
        int vertexInWidth = voxelInWidth * 2;
        int vertexInHeight = voxelInHeight * 2;

        //generate vertices
        vertices = new Vector3[vertexInWidth * vertexInHeight];
        int index = 0;
        for (int y = 0; y < vertexInHeight; y++)
        {
            for (int x = 0; x < vertexInWidth; x++)
            {
                if(x % 2 == 0 && y % 2 == 0)
                {
                    vertices[index] = new Vector3(x * stripe, y * stripe);
                }
                if(x % 2 == 1 && y % 2 == 0)
                {
                    vertices[index] = new Vector3((x - 1) * stripe + size, y * stripe);
                }
                if(x % 2 == 0 && y % 2 == 1)
                {
                    vertices[index] = new Vector3(x * stripe, (y - 1) * stripe + size);
                }
                if(x % 2 ==1 && y % 2 == 1)
                {
                    vertices[index] = new Vector3((x - 1) * stripe + size, (y - 1) * stripe + size);
                }
                index += 1;
            }
        }
        mesh.vertices = vertices;

        //generate uv

        //draw triangles
        int[] triangles = new int[voxelInWidth * voxelInHeight * 6 * 2];
        int ti = 0, vi = 0;
        for (int y = 0; y < voxelInHeight * 1; y++)
        {
            for (int x = 0; x < voxelInWidth * 1; x++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 3] = vi + 1;
                triangles[ti + 2] = triangles[ti + 4] = voxelInWidth * 2 + vi + 1;
                triangles[ti + 5] = voxelInWidth * 2 + vi + 2;
                ti += 6;
                vi += 2;
            }
        }
       // mesh.triangles = triangles;
    }
}
