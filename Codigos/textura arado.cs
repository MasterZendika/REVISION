using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AradoMeshGenerator : MonoBehaviour
{
    public int width = 100; // Ancho del terreno
    public int length = 100; // Largo del terreno
    public float spacing = 1.0f; // Espaciado entre los surcos
    public float depth = 0.2f; // Profundidad de los surcos

    void Start()
    {
        Mesh mesh = new Mesh
        {
            name = "Arado"
        };
        GetComponent<MeshFilter>().mesh = mesh;

        // Crear vértices
        Vector3[] vertices = new Vector3[width * length];
        for (int z = 0, i = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++, i++)
            {
                float y = Mathf.Sin(x * spacing) * depth; // Simula los surcos
                vertices[i] = new Vector3(x, y, z);
            }
        }

        // Crear triángulos
        int[] triangles = new int[(width - 1) * (length - 1) * 6];
        for (int ti = 0, vi = 0, y = 0; y < length - 1; y++, vi++)
        {
            for (int x = 0; x < width - 1; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + width;
                triangles[ti + 5] = vi + width + 1;
            }
        }

        // Crear UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / width, vertices[i].z / length);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        // Añadir un MeshCollider y asignarle el mesh generado
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
}
