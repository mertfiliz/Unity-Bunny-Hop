using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    private bool swap = true;    

    void Update()
    {
        if (swap)
        {
            transform.localPosition = new Vector2(transform.localPosition.x + 2, transform.localPosition.y);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x - 2, transform.localPosition.y);
        }

        if (transform.localPosition.x <= -1 * GenerateBlocks.Get_ScreenSize())
        {
            swap = true;
        }

        if (transform.localPosition.x >= GenerateBlocks.Get_ScreenSize())
        {
            swap = false;
        }
    }
}
