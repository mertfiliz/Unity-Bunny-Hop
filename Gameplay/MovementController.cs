using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool isMoveLeft = false, isMoveRight = false;  

    void FixedUpdate()
    {
        if (isMoveLeft)
        {
            Movement.Player.transform.position += Vector3.left * 1f;
        }
        if (isMoveRight)
        {
            Movement.Player.transform.position += Vector3.right * 1f;
        }
    }

    public void MoveLeftDown()
    {
        isMoveLeft = true;
    }

    public void MoveRightDown()
    {
        isMoveRight = true;
    }

    public void MoveLeftUp()
    {
        isMoveLeft = false;
    }

    public void MoveRightUp()
    {
        isMoveRight = false;
    }
}
