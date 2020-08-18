using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRemover : MonoBehaviour
{
    public GameObject Camera;  
    
    void Update()
    {
        // Block Remover follows our Camera/Player.
        transform.localPosition = new Vector2(0, Camera.transform.localPosition.y - 1800);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Send the gameobject for destroy.
        GenerateBlocks.Remove_Block(col.gameObject);
    }
}
