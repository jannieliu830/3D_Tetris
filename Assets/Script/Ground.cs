using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour {

    public Shader shader;
    public PointLight[] pointLights;

    private const int MAX_LIGHTS = 10;

    // Use this for initialization
    void Start () {
        MeshFilter Plane = this.gameObject.AddComponent<MeshFilter>();
        Plane.mesh = this.CreatePlaneMesh();

        // Add a MeshRenderer component. This component actually renders the mesh that
        // is defined by the MeshFilter component.
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = shader;

        renderer.material.SetFloat("_AmbientCoeff", 1.0f);
        renderer.material.SetFloat("_DiffuseCoeff", 1.0f);
        renderer.material.SetFloat("_SpecularCoeff", 0.15f);
        renderer.material.SetFloat("_SpecularPower", 15.0f);
    }
	
	// Update is called once per frame
	void Update () {

        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Pass updated light positions to shader
        Vector3[] lightPositions = new Vector3[this.pointLights.Length];
        Color[] lightColors = new Color[this.pointLights.Length];
        for (int i = 0; i < this.pointLights.Length; i++)
        {
            lightPositions[i] = this.pointLights[i].GetWorldPosition();
            lightColors[i] = this.pointLights[i].color;
        }

        // Note: We need to be careful since we only have a fixed amount of memory
        // for the light sources in the shader (MAX_LIGHTS). It's easily possible to
        // overflow it if the pointLights array has more than MAX_LIGHTS, so might be 
        // worth doing an extra check like below. The only issue is if we change
        // MAX_LIGHTS in the shader, it also has to be correspondingly changed in 
        // this script.
        if (this.pointLights.Length > MAX_LIGHTS)
        {
            Debug.LogError("Number of lights exceeds the maximum shader limit");
        }
        else
        {
            // Pass the actual number of lights to the shader
            renderer.material.SetInt("_NumPointLights", this.pointLights.Length);

            // For Unity 5.3 and below; Unity 5.4 and above provides an array passing interface
            // via the material class itself (like SetInt() above)
            PassArrayToShader.Vector3(renderer.material, "_PointLightPositions", lightPositions);
            PassArrayToShader.Color(renderer.material, "_PointLightColors", lightColors);
        }
    }

    // Method to create a cube mesh with coloured vertices
    Mesh CreatePlaneMesh()
    {
        Mesh m = new Mesh();
        m.name = "Plane";

        // Define the vertices. These are the "points" in 3D space that allow us to
        // construct 3D geometry (by connecting groups of 3 points into triangles).
        m.vertices = new[] {
            new Vector3(-0.5f, 0.0f, -0.5f), // Bottom
            new Vector3(-0.5f, 0.0f, 9.5f),
            new Vector3(9.5f, 0.0f, 9.5f),
            new Vector3(-0.5f, 0.0f, -0.5f),
            new Vector3(9.5f, 0.0f, 9.5f),
            new Vector3(9.5f, 0.0f, -0.5f)
        };

        // Define the vertex colours
        m.colors = new[] {

            Color.gray, // Bottom
            Color.gray,
            Color.gray,
            Color.gray,
            Color.gray,
            Color.gray,

        };

        // Define the UV coordinates
        m.uv = new[] {

            new Vector2(0.0f, 0.0f), // Top
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(0.0f, 0.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f)
        };

        // Define the normals
        Vector3 topNormal = new Vector3(0.0f, 1.0f, 0.0f);

        m.normals = new[] {

            topNormal, // Bottom
            topNormal,
            topNormal,
            topNormal,
            topNormal,
            topNormal
        };

        // Define mesh tangents

        // Note that correctly defining mesh tangents typically depends on the orientation
        // of the texture on the object. There are methods to automatically generate them,
        // as with normals, but in in this case, we've defined them manually.
        Vector4 topTangent = new Vector3(1.0f, 0.0f, 0.0f);

        m.tangents = new[] {

            topTangent, // Bottom
            topTangent,
            topTangent,
            topTangent,
            topTangent,
            topTangent
        };

        // Automatically define the triangles based on the number of vertices
        int[] triangles = new int[m.vertices.Length];
        for (int i = 0; i < m.vertices.Length; i++)
            triangles[i] = i;

        m.triangles = triangles;

        return m;
    }
}
