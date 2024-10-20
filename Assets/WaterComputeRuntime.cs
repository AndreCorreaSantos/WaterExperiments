using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class WaterCompute : MonoBehaviour
{

    public ComputeShader shader;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private ComputeBuffer vertexBuffer;
    private int kernelHandle;
    private int vertexCount;
    private Material material;
    private Vector3[] vertices;

    [Range(1, 100)]
    public int gridScale = 10;

    [Range(-10.0f, 10.0f)]
    public float timeMultiplier = 1.0f;

    [Range(0.001f, 10.0f)]
    public float heightMultiplier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        vertexCount = vertices.Length;

        // Compute Buffer vertex positions
        vertexBuffer = new ComputeBuffer(vertexCount, 12); // float = 4 bytes, xyz = 12 bytes
        vertexBuffer.SetData(vertices);
        kernelHandle = shader.FindKernel("CSMain");

    }

    // Update is called once per frame
    void Update()
    {
        // setting shader variables
        shader.SetFloat("_Time", Time.time);
        shader.SetFloat("_TimeMultiplier",timeMultiplier);
        shader.SetFloat("_HeightMultiplier",heightMultiplier);
        shader.SetInt("_VertexCount", vertexCount);
        shader.SetInt("_CellScale",gridScale);
        int threadGroups = Mathf.CeilToInt(vertexCount / 1024f); // 1024,1,1 threads per threadgroup
        shader.SetBuffer(kernelHandle,"vertices", vertexBuffer);
        // dispatch the shader
        shader.Dispatch(kernelHandle, threadGroups, 1, 1);
        // read output 
        vertexBuffer.GetData(vertices);

        // update mesh 
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    void OnDestroy()
    {
        // Release the compute buffer when the script is destroyed
        if (vertexBuffer != null)
        {
            vertexBuffer.Release();
        }
    }
}
