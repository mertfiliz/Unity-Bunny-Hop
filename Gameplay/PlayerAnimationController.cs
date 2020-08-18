using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{  
    public static GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");        
    }       

    void Update()
    {
        transform.localPosition = new Vector2(Player.transform.localPosition.x, Player.transform.localPosition.y - 140f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {       
        if (coll.gameObject.tag != "carrotbox" && coll.gameObject.tag != "carrot" && coll.gameObject.tag != "mushroom" && coll.gameObject.name != "Player")
        {
            //Debug.Log(coll.gameObject.name);
            Player.GetComponent<Animator>().SetBool("isPlayerDownTrigger", true);
            Player.GetComponent<Animator>().SetBool("isPlayerDown", false);
            Player.GetComponent<Animator>().SetBool("isPlayerUp", false);
        }
    }
}
