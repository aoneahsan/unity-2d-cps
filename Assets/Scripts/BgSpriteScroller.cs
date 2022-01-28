using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSpriteScroller : MonoBehaviour
{
    [SerializeField]
    Vector2 moveSpeed;

    Material material;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (material != null)
        {
            material.mainTextureOffset += moveSpeed * Time.deltaTime;
        }
    }
}
