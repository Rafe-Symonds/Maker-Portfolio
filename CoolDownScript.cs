using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CoolDownScript : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public int timeInSeconds;  //duration
    DateTime startTime;
    DateTime currentTime;
    float degrees;
    bool started = false;


    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0.5f, 0, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(0, 0.5f, 0),
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0, 0, 0)
        };      
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    double DegreesToRadians(float firstNumber, float secondNumber)
    {
        return (double)(firstNumber * Math.PI / 180 - secondNumber * Math.PI / 180);
    }
    double DegreesToRadians(float number)
    {
        return (double)(number * Math.PI / 180);
    }


    void CalculateMesh()
    {
        CreateShape();
        if(started == true)
        {
            degrees = (float)(currentTime - startTime).TotalMilliseconds / (timeInSeconds * 1000) * 360f; //need to chnage 10 to timeInSeconds
            if (degrees >= 360)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            degrees = 0;
        }
        


        List<Vector3> tempTriangles = CreateTriangles();

        triangles = new int[tempTriangles.Count * 3];

        for(int i = 0; i <= tempTriangles.Count * 3 - 1; i += 3)
        {
            triangles[i] = (int)tempTriangles[i / 3].x;
            triangles[i + 1] = (int)tempTriangles[i / 3].y;
            triangles[i + 2] = (int)tempTriangles[i / 3].z;
        }

        if (degrees > 270)
        {
            if(degrees > 315)
            {
                vertices[9] = new Vector3((float)(-0.5 * Math.Tan(DegreesToRadians(360f, degrees))), 0.5f);
            }
            else
            {
                vertices[9] = new Vector3(-0.5f, (float)(0.5 * Math.Tan(DegreesToRadians(degrees, 270)))); 
            }
        }
        else if(degrees > 180)
        {
            if(degrees > 225)
            {
                vertices[9] = new Vector3(-0.5f, (float)(-0.5 * Math.Tan(DegreesToRadians(270, degrees))));
            }
            else
            {
                vertices[9] = new Vector3((float)(-0.5 * Math.Tan(DegreesToRadians(degrees, 180))), -0.5f);
            }
        }
        else if(degrees > 90)
        {
            if(degrees > 135)
            {
                vertices[9] = new Vector3((float)(0.5 * Math.Tan(DegreesToRadians(180, degrees))), -0.5f);
            }
            else
            {
                vertices[9] = new Vector3(0.5f, (float)(-0.5 * Math.Tan(DegreesToRadians(degrees, 90))));
            }
        }
        else if(degrees < 90)
        {
            if(degrees > 45)
            {
                vertices[9] = new Vector3(0.5f, (float)(0.5 * Math.Tan(DegreesToRadians(90, degrees))));
            }
            else
            {
                vertices[9] = new Vector3((float)(0.5 * Math.Tan(DegreesToRadians(degrees))), 0.5f);
            }
        }
        else
        {
            Debug.Log("Not assigned");
        }
        //Debug.Log("vertices in function = " + vertices[9]);
        UpdateMesh();
    }

    List<Vector3> CreateTriangles()
    {
        List<Vector3> tempTriangles = new List<Vector3>(); //This list is being used as a storage to add later to the array. These are
                                                           //not vectors
        if (degrees > 270)
        {
            if(degrees > 315)
            {
                tempTriangles.Add(new Vector3(9, 7, 4));
                return tempTriangles;
            }
            else
            {
                tempTriangles.Add(new Vector3(9, 6, 4));
                tempTriangles.Add(new Vector3(6, 7, 4));
                return tempTriangles;
            }
        }
        else
        {
            tempTriangles.Add(new Vector3(3, 6, 4));
            tempTriangles.Add(new Vector3(6, 7, 4));
        }

        if (degrees > 180)
        {
            if (degrees > 225)
            {
                tempTriangles.Add(new Vector3(9, 3, 4));
                return tempTriangles;
            }
            else
            {
                tempTriangles.Add(new Vector3(0, 3, 4));
                tempTriangles.Add(new Vector3(9, 0, 4));
                return tempTriangles;
            }
        }
        else
        {
            tempTriangles.Add(new Vector3(0, 3, 4));
            tempTriangles.Add(new Vector3(1, 0, 4));
        }

        if (degrees > 90)
        {
            if (degrees > 135)
            {
                tempTriangles.Add(new Vector3(9, 1, 4));
                return tempTriangles;
            }
            else
            {
                tempTriangles.Add(new Vector3(2, 1, 4));
                tempTriangles.Add(new Vector3(9, 2, 4));
                return tempTriangles;
            }
        }
        else
        {
            tempTriangles.Add(new Vector3(2, 1, 4));
            tempTriangles.Add(new Vector3(5, 2, 4));
        }

        if (degrees > 0)
        {
            if (degrees > 45)
            {
                tempTriangles.Add(new Vector3(9, 5, 4));
                return tempTriangles;
            }
            else
            {
                tempTriangles.Add(new Vector3(8, 5, 4));
                tempTriangles.Add(new Vector3(9, 8, 4));
                return tempTriangles;
            }
        }
        else
        {
            tempTriangles.Add(new Vector3(8, 5, 4));
            tempTriangles.Add(new Vector3(7, 8, 4));
        }
        return tempTriangles;
    }

    public void StartTimer()
    {
        startTime = TimeManagement.CurrentTime();
        UpdateMesh();
        started = true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void FixedUpdate()
    {
        currentTime = TimeManagement.CurrentTime();

        CalculateMesh();
        //Debug.Log("vertices = " + vertices[9] + "  Degrees = " + degrees);
        //Debug.Log("Degrees = " + degrees);
        //Debug.Log("Triangles = " + triangles);
    }
}
