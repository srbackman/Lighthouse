using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
public class WavyPlane : MonoBehaviour
{
    [SerializeField] private int width = 100;
    [SerializeField] private int height = 100;
    [SerializeField] private float scale = 1f;

    [SerializeField] private float waveHeight = 1f;
    [SerializeField] private float waveSpeed = 1f;
    [SerializeField] private float waveFrequency = 1f;


    private Mesh m_mesh;
    private Vector3[] m_vertices = null;
    private Vector3[] m_baseVertices = null;

    private void Start()
    {
        m_mesh = new Mesh();
        m_mesh.name = "WaveMesh";
        GetComponent<MeshFilter>().mesh = m_mesh;

        RebuildMesh();
    }
    private void Update()
    {
        AnimateVertices();
    }
    void RebuildMesh()
    {
        m_vertices = new Vector3[(width + 1) * (height + 1)];
        m_baseVertices = new Vector3[m_vertices.Length];

        int i = 0;
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                Vector3 v = new Vector3(x * scale, 0, z * scale);
                m_vertices[i] = v;
                m_baseVertices[i] = v;
                i++;
            }
        }

        int[] triangles = new int[width * height * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + width + 1;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + width + 1;
                triangles[tris + 5] = vert + width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        m_mesh.vertices = m_vertices;
        m_mesh.triangles = triangles;
        m_mesh.RecalculateNormals();
    }
    void AnimateVertices()
    {
        if (m_vertices == null)
            return;

        for (int i = 0; i < m_vertices.Length; i++)
        {
            Vector3 v = m_baseVertices[i];

            if (i % 2 == 0)
            {

            }
            v.y = Mathf.Sin(Time.time * waveSpeed + v.x * waveFrequency) * waveHeight;
            m_vertices[i] = v;
        }
        m_mesh.vertices = m_vertices;
        m_mesh.RecalculateNormals();
    }
}


