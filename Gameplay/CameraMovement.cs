using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    public static GameObject Camera;
    private GameObject left,right, left_trap, right_trap;

    public static bool Camera_Fall_Stop;

    void Start()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("Main Camera");
        left = GameObject.Find("LeftScreen");
        right = GameObject.Find("RightScreen");
        left_trap = GameObject.Find("Left_Trap");
        right_trap = GameObject.Find("Right_Trap");

        Camera_Fall_Stop = false;
    }

    void Update()
    {
        transform.localPosition = new Vector3(0, Player.transform.localPosition.y, 0);
        left.transform.localPosition = new Vector3(left.transform.localPosition.x, Player.transform.localPosition.y, 0);
        right.transform.localPosition = new Vector3(right.transform.localPosition.x, Player.transform.localPosition.y, 0);
    }

    public static void Camera_Fall(float first_block_pos)
    {
        Camera_Fall_Stop = true;
        Camera.transform.localPosition = new Vector3(0, first_block_pos, -1650);
    }
}
