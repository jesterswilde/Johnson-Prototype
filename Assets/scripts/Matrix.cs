using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Matrix : MonoBehaviour
{
    int numInWidth = 10;
    int numInHeight = 10;

    float gap = 0.45f;
    float size = 0.15f;
    Mesh mesh;

    void Generator()
    {
        //create mesh object
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        float stripe = gap + size;

        //generate vertices
        int verticesNum = numInWidth * 2 * numInHeight * 2;
        Vector3[] vertices = new Vector3[verticesNum];
        int index = 0;
        for (int x = 0; x < numInWidth * 2; x++)
        {
            for (int y = 0; y < numInHeight * 2; y++)
            {
                vertices[index] = new Vector3(x * stripe / 2, y * stripe / 2, 0);
            }
        }
        mesh.vertices = vertices;

        //generate uv

        //draw triangles
    }
}
