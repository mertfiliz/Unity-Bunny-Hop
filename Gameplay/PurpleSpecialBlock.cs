using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSpecialBlock : MonoBehaviour
{
    void Start()
    {
        // PURPLE INACTIVE IN WORLD 0
        if (Portals.CurrentWorld == 0)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        // PURPLE INACTIVE IN WORLD 1
        if (Portals.CurrentWorld == 1)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        // PURPLE ACTIVE IN WORLD 2
        if (Portals.CurrentWorld == 2)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    void Update()
    {
        // If special block position.y is lesser than BlockRemover position.y, destroy special block.
        if (GameObject.Find("BlockRemover").transform.localPosition.y >= this.transform.localPosition.y)
        {
            GenerateBlocks.Remove_Block(this.gameObject);
        }
    }
}
