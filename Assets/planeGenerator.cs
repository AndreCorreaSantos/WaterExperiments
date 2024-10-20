using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class planeGenerator : MonoBehaviour
{
    [Range(10, 1000)]
    public int width = 10;      // Number of quads along the width
     [Range(10, 1000)]
    public int height = 10;     // Number of quads along the height
    public float quadSize = 1f; // Size of each quad

    private Mesh mesh;

    void Awake()
    {
        GenerateDensePlane();
    }

    void GenerateDensePlane()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int numVertices = (width + 1) * (height + 1); // Vertices needed
        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[width * height * 6]; // 2 triangles per quad (6 indices per quad)

        // Calculate offsets to center the plane at (0,0,0)
        float offsetX = width * quadSize * 0.5f;
        float offsetY = height * quadSize * 0.5f;

        // Create vertices and UVs
        int vertIndex = 0;
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                // Offset each vertex so the plane is centered at (0,0,0)
                vertices[vertIndex] = new Vector3(x * quadSize - offsetX, 0, y * quadSize - offsetY);
                uv[vertIndex] = new Vector2((float)x / width, (float)y / height);
                vertIndex++;
            }
        }

        // Create triangles
        int triIndex = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int vertexIndex = y * (width + 1) + x;

                // First triangle
                triangles[triIndex + 0] = vertexIndex;
                triangles[triIndex + 1] = vertexIndex + width + 1;
                triangles[triIndex + 2] = vertexIndex + 1;

                // Second triangle
                triangles[triIndex + 3] = vertexIndex + 1;
                triangles[triIndex + 4] = vertexIndex + width + 1;
                triangles[triIndex + 5] = vertexIndex + width + 2;

                triIndex += 6;
            }
        }

        // Apply to mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Recalculate normals for lighting and collisions
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
