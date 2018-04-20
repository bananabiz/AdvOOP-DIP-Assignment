using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffset : MonoBehaviour {

    public float scrollSpeed = 0.01F;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;

        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }
}