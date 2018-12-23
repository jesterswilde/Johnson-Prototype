using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Matrix : MonoBehaviour
{
    int voxelInWidth = 2;
    int voxelInHeight = 2;
    int voxelInDepth = 1;
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
            Gizmos.DrawSphere(vertices[i], 0.05f);
        }
    }

    void Generator()
    {
        //create mesh object
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        float stripe = width / voxelInWidth;
        int vertexInWidth = voxelInWidth * 2;
        int vertexInHeight = voxelInHeight * 2;
        int vertexInDepth = 2;

        //generate vertices
        vertices = new Vector3[vertexInWidth * vertexInHeight * 2];
        int index = 0;
        for (int z = 0; z < vertexInDepth; z++)
        {
            for (int y = 0; y < vertexInHeight; y++)
            {
                for (int x = 0; x < vertexInWidth; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        vertices[index] = new Vector3(x * stripe, y * stripe, z * size);
                    }
                    if (x % 2 == 1 && y % 2 == 0)
                    {
                        vertices[index] = new Vector3((x - 1) * stripe + size, y * stripe, z * size);
                    }
                    if (x % 2 == 0 && y % 2 == 1)
                    {
                        vertices[index] = new Vector3(x * stripe, (y - 1) * stripe + size, z * size);
                    }
                    if (x % 2 == 1 && y % 2 == 1)
                    {
                        vertices[index] = new Vector3((x - 1) * stripe + size, (y - 1) * stripe + size, z * size);
                    }
                    index += 1;
                }
            }
        }
        mesh.vertices = vertices;

        //generate uv

        //generate triangles
        int[] triangles = new int[voxelInWidth * voxelInHeight * 6 * 6];
        int ti = 0, vi = 0;
        Debug.Log(vertices.Length + " | " + triangles.Length);

  //      //draw front faces
  //      for (int y = 0; y < voxelInHeight; y++)
  //      {
  //          for (int x = 0; x < voxelInWidth; x++)
  //          {
  //              if (x == 0 && y != 0)
  //              {
  //                  vi += vertexInWidth;
  //                  Debug.Log("jump to next row");
  //              }
  //              triangles[ti] = vi;
  //              triangles[ti + 1] = triangles[ti + 4] = voxelInWidth * 2 + vi;
  //              triangles[ti + 2] = triangles[ti + 3] = vi + 1;
  //              triangles[ti + 5] = voxelInWidth * 2 + 1 + vi;
  //              ti += 6;
  //              vi += 2;
  //          }
  //      }

  //      //draw back faces
  //      for (int y = 0; y < voxelInHeight; y++)
  //      {
  //          for (int x = 0; x < voxelInWidth; x++)
  //          {
  //              if (x == 0)
  //              {
  //                  vi += vertexInWidth;
  //                  Debug.Log("jump to the next row");
  //              }
  //              triangles[ti] = vi;
  //              triangles[ti + 1] = vi + 1;
  //              triangles[ti + 4] = voxelInWidth * 2 + vi + 1;
  //              triangles[ti + 2] = voxelInWidth * 2 + vi;
  //              triangles[ti + 3] = vi + 1;
  //              triangles[ti + 5] = voxelInWidth * 2 + vi;
  //              ti += 6;
  //              vi += 2;
  //          }
  //      }

		////draw bottom
		//vi = 0;
        //for (int y = 0; y < voxelInHeight; y++)
        //{
        //    for (int x = 0; x < voxelInWidth; x++)
        //    {
        //        if(x == 0 && y != 0)
        //        {
        //            vi += vertexInWidth;
        //        }
        //        triangles[ti] = vi;
        //        triangles[ti + 1] = triangles[ti + 3] = vi + 1;
        //        triangles[ti + 2] = triangles[ti + 5] = vertexInWidth * vertexInHeight + vi;
        //        triangles[ti + 4] = vertexInWidth * vertexInHeight + vi + 1;
        //        ti += 6;
        //        vi += 2;
        //    }
        //}

        ////draw top
        //vi = 0;
        //for (int y = 0; y < voxelInHeight; y++)
        //{
        //    for (int x = 0; x < voxelInWidth; x++)
        //    {
        //        if (x == 0)
        //        {
        //            vi += vertexInWidth;
        //        }
        //        triangles[ti] = vi;
        //        triangles[ti + 1] = triangles[ti + 4] = vertexInWidth * vertexInHeight + vi;
        //        triangles[ti + 2] = triangles[ti + 3] = vi + 1;
        //        triangles[ti + 5] = vertexInWidth * vertexInHeight + vi + 1;
        //        ti += 6;
        //        vi += 2;
        //    }
        //}

        //draw left side
        vi = 0;
        for (int y = 0; y < voxelInHeight; y++)
        {
            for (int x = 0; x < voxelInWidth; x++)
            {
                if(x == 0 && y != 0)
                {
                    vi += vertexInWidth;
                }
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 3] = vi + vertexInWidth * vertexInHeight;
                triangles[ti + 2] = triangles[ti + 5] = vi + vertexInWidth;
                triangles[ti + 4] = vi + vertexInWidth * vertexInHeight + vertexInWidth;
                ti += 6;
                vi += 2;//2 4  
            }
        }
        //draw right side

        mesh.triangles = triangles;
    }
}
