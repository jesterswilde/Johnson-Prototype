using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Matrix : MonoBehaviour
{
    //consider change this name to something like rows or boxRows
    [SerializeField]
    int voxelInWidth = 10;
    [SerializeField]
    int voxelInHeight = 10;
    [SerializeField]
    int voxelInDepth = 1;
    [SerializeField]
    float width = 6.0f;
    [SerializeField]
    float height = 6.0f;

    [SerializeField]
    float size = 0.2f;
    Mesh mesh;
    Vector3[] vertices;

    private void Start()
    {
        Generator();
    }

    //vertices test
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    if (vertices == null)
    //    {
    //        return;
    //    }
    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Gizmos.DrawSphere(vertices[i], 0.05f);
    //    }
    //}

    void Generator()
    {
        //create mesh object
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        float stripe = width / voxelInWidth;
        //why 2? 
        int vertexInWidth = voxelInWidth * 2;
        int vertexInHeight = voxelInHeight * 2;
        int vertexInDepth = voxelInDepth * 2;

        //generate vertices
        //Why 2? And why is depth not in here? 
        vertices = new Vector3[vertexInWidth * vertexInHeight * 2];
        int index = 0;
        for (int z = 0; z < vertexInDepth; z++)
        {
            for (int y = 0; y < vertexInHeight; y++)
            {
                for (int x = 0; x < vertexInWidth; x++)
                {
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        vertices[index] = new Vector3(x * stripe, y * stripe, z * size);
                    }
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 1 && y % 2 == 0)
                    {
                        vertices[index] = new Vector3((x - 1) * stripe + size, y * stripe, z * size);
                    }
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 0 && y % 2 == 1)
                    {
                        vertices[index] = new Vector3(x * stripe, (y - 1) * stripe + size, z * size);
                    }
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 1 && y % 2 == 1)
                    {
                        vertices[index] = new Vector3((x - 1) * stripe + size, (y - 1) * stripe + size, z * size);
                    }
                    index ++;
                }
            }
        }
        mesh.vertices = vertices;

        //generate uv

        //generate triangles
        //What do the 2 6's mean? 
        int[] triangles = new int[voxelInWidth * voxelInHeight * 6 * 6];
        //ti and vi are terrible names.  
        int ti = 0, vi = 0;
        Debug.Log(vertices.Length + " | " + triangles.Length);

        //draw front faces
        for (int y = 0; y < voxelInHeight; y++)
        {
            for (int x = 0; x < voxelInWidth; x++)
            {
                if (x == 0 && y != 0)
                {
                    vi += vertexInWidth;
                    Debug.Log("jump to next row");
                }
                triangles[ti] = vi;
                //why * 2? 
                triangles[ti + 1] = triangles[ti + 4] = voxelInWidth * 2 + vi;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = voxelInWidth * 2 + 1 + vi;
                ti += 6;
                vi += 2;
            }
        }

        //draw back faces
        for (int y = 0; y < voxelInHeight; y++)
        {
            for (int x = 0; x < voxelInWidth; x++)
            {
                if (x == 0)
                {
                    vi += vertexInWidth;
                    Debug.Log("jump to the next row");
                }
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = voxelInWidth * 2 + vi + 1;
                triangles[ti + 2] = triangles[ti + 5] = voxelInWidth * 2 + vi;
                ti += 6;
                vi += 2;
            }
        }

		//draw bottom
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
                triangles[ti + 1] = triangles[ti + 3] = vi + 1;
                //Why are you multiplying width and height? 
                triangles[ti + 2] = triangles[ti + 5] = vertexInWidth * vertexInHeight + vi;
                triangles[ti + 4] = vertexInWidth * vertexInHeight + vi + 1;
                ti += 6;
                vi += 2;
            }
        }

        //draw top
        vi = 0;
        for (int y = 0; y < voxelInHeight; y++)
        {
            for (int x = 0; x < voxelInWidth; x++)
            {
                if (x == 0)
                {
                    vi += vertexInWidth;
                }
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vertexInWidth * vertexInHeight + vi;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vertexInWidth * vertexInHeight + vi + 1;
                //Why 6 and why 2? 
                ti += 6;
                vi += 2;
            }
        }

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
        vi = 1;
        for (int y = 0; y < voxelInHeight; y++)
        {
            for (int x = 0; x < voxelInWidth; x++)
            {
                if (x == 0 && y != 0)
                {
                    vi += vertexInWidth;
                }
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 3] = vi + vertexInWidth;
                triangles[ti + 2] = triangles[ti + 5] = vi + vertexInWidth * vertexInHeight;
                triangles[ti + 4] = vi + vertexInWidth * vertexInHeight + vertexInWidth;
                ti += 6;
                vi += 2;//2 4  
            }
        }
        mesh.triangles = triangles;
    }
}

/*
    I would break your methods into shorter, easier to understand chunks. For example: 

    for (int z = 0; z < vertexInDepth; z++)
        {
            for (int y = 0; y < vertexInHeight; y++)
            {
                for (int x = 0; x < vertexInWidth; x++)
                {
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        vertices[index] = new Vector3(x * stripe, y * stripe, z * size);
                    }
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 1 && y % 2 == 0)
                    {
                        vertices[index] = new Vector3((x - 1) * stripe + size, y * stripe, z * size);
                    }
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 0 && y % 2 == 1)
                    {
                        vertices[index] = new Vector3(x * stripe, (y - 1) * stripe + size, z * size);
                    }
                    //DESCRIBE THIS CONDITION
                    if (x % 2 == 1 && y % 2 == 1)
                    {
                        vertices[index] = new Vector3((x - 1) * stripe + size, (y - 1) * stripe + size, z * size);
                    }
                    index ++;
                }
            }
        }

        Should Become:
        for (int z = 0; z < vertexInDepth; z++)
        {
            for (int y = 0; y < vertexInHeight; y++)
            {
                for (int x = 0; x < vertexInWidth; x++)
                {
                    GenerateVertex(vertices, x, y, z);
                    index ++;
                }
            }
        }

        GenerateVertex(Vector3[] vertices, int x, int y, int z){
            //DESCRIBE THIS CONDITION
            if (x % 2 == 0 && y % 2 == 0)
                {
                    vertices[index] = new Vector3(x * stripe, y * stripe, z * size);
                }
                //DESCRIBE THIS CONDITION
                if (x % 2 == 1 && y % 2 == 0)
                {
                    vertices[index] = new Vector3((x - 1) * stripe + size, y * stripe, z * size);
                }
                //DESCRIBE THIS CONDITION
                if (x % 2 == 0 && y % 2 == 1)
                {
                    vertices[index] = new Vector3(x * stripe, (y - 1) * stripe + size, z * size);
                }
                //DESCRIBE THIS CONDITION
                if (x % 2 == 1 && y % 2 == 1)
                {
                    vertices[index] = new Vector3((x - 1) * stripe + size, (y - 1) * stripe + size, z * size);
                }
        }

 */