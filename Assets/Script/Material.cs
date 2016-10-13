using UnityEngine;
using System.Collections;
using System;

public class Material : MonoBehaviour {

    public Shader shader;
    public PointLight pointLight;
    // Use this for initialization
    void Start () {
        // Add a MeshRenderer component. This component actually renders the mesh that
        // is defined by the MeshFilter component.
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = shader;
    }
	
	// Update is called once per frame
	void Update () {
        // Add a MeshRenderer component. This component actually renders the mesh that
        // is defined by the MeshFilter component.
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        // Pass updated light positions to shader
        renderer.material.SetColor("_PointLightColor", this.pointLight.color);
        renderer.material.SetVector("_PointLightPosition", this.pointLight.GetWorldPosition());
    }
}
