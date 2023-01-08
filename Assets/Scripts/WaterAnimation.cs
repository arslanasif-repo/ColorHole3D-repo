using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    private Material MyMaterial;
    [SerializeField] private float AnimationSpeed;

    private void OnEnable()
    {
        MyMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        MyMaterial.mainTextureOffset += new Vector2(0.5f,0.25f) * Time.deltaTime * AnimationSpeed;
    }
}
