using UnityEngine;
using System.Collections;

public class PrefabShader : MonoBehaviour {

    public Shader shader;
    private PointLight[] pointLights;

    private const int MAX_LIGHTS = 10;

    // Use this for initialization
    void Start () {

        MeshFilter block = this.gameObject.AddComponent<MeshFilter>();
        block.mesh = this.gameObject.GetComponent<Mesh>();

        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = shader;

        renderer.material.SetFloat("_AmbientCoeff", 1.0f);
        renderer.material.SetFloat("_DiffuseCoeff", 1.0f);
        renderer.material.SetFloat("_SpecularCoeff", 0.15f);
        renderer.material.SetFloat("_SpecularPower", 15.0f);

        pointLights = GameObject.Find("PointLight").GetComponents<PointLight>();
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
}
