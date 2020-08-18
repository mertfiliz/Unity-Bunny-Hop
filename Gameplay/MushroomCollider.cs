using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCollider : MonoBehaviour
{       
    void Update()
    {
        if(Movement.isPlayerAscending)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
