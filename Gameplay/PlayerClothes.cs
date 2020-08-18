using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothes : MonoBehaviour
{
    public Sprite[] Head_Sprites;
    public Sprite[] Body_Sprites;
    public Sprite[] Left_Leg_Sprites;
    public Sprite[] Right_Leg_Sprites;
    public Sprite[] Left_Arm_Sprites;
    public Sprite[] Right_Arm_Sprites;
    public GameObject Blue_Trail, Orange_Trail, Purple_Trail, Green_Trail, Snow_Trail, Water_Trail, Star_Trail, Line_Trail;
    public GameObject Select_Trail;
    public GameObject TrailCollector;
    public int trail_no;

    void Start()
    {
        PlayerDataCustomize loadedData = SaveLoadCustomize.LoadPlayer();
        Trail_Change(loadedData.trail_selection);
        Hat_Change(loadedData.hat_selection);
        Body_Change(loadedData.body_selection);
        Legs_Change(loadedData.legs_selection);
        Hands_Change(loadedData.hands_selection);
    }

    public void Trail_Change(int trail_no)
    {
        if (trail_no == 1)
        {
            if (TrailCollector.transform.childCount > 0)
            {
                Destroy(TrailCollector.transform.GetChild(0).gameObject);                
            }
        }
        else if (trail_no == 2)
        {
            Select_Trail = Blue_Trail;
            LoadParticle(Select_Trail);
        }
        else if (trail_no == 3)
        {
            Select_Trail = Orange_Trail;
            LoadParticle(Select_Trail);
        }
        else if (trail_no == 4)
        {
            Select_Trail = Purple_Trail;
            LoadParticle(Select_Trail);
        }
        else if (trail_no == 5)
        {
            Select_Trail = Snow_Trail;
            LoadParticle(Select_Trail);
        }
        else if (trail_no == 6)
        {
            Select_Trail = Water_Trail;
            LoadParticle(Select_Trail);
        }
        else if (trail_no == 7)
        {
            Select_Trail = Star_Trail;
            LoadParticle(Select_Trail);
        }
        else if (trail_no == 8)
        {
            Select_Trail = Line_Trail;
            LoadParticle(Select_Trail);
        }
    }

    public void Hat_Change(int hat_no)
    {
        if (hat_no == 1)
        {
            GameObject.Find("Player").transform.Find("head").GetComponent<SpriteRenderer>().sprite = Head_Sprites[0];
        }
        else if (hat_no == 2)
        {
            GameObject.Find("Player").transform.Find("head").GetComponent<SpriteRenderer>().sprite = Head_Sprites[1];
        }
        else if (hat_no == 3)
        {
            GameObject.Find("Player").transform.Find("head").GetComponent<SpriteRenderer>().sprite = Head_Sprites[2];
        }
        else if (hat_no == 4)
        {
            GameObject.Find("Player").transform.Find("head").GetComponent<SpriteRenderer>().sprite = Head_Sprites[3];
        }
        else if (hat_no == 5)
        {
            GameObject.Find("Player").transform.Find("head").GetComponent<SpriteRenderer>().sprite = Head_Sprites[4];
        }
    }

    public void Body_Change(int body_no)
    {
        if (body_no == 1)
        {
            GameObject.Find("Player").transform.Find("body").GetComponent<SpriteRenderer>().sprite = Body_Sprites[0];
        }
        else if (body_no == 2)
        {
            GameObject.Find("Player").transform.Find("body").GetComponent<SpriteRenderer>().sprite = Body_Sprites[1];
        }
        else if (body_no == 3)
        {
            GameObject.Find("Player").transform.Find("body").GetComponent<SpriteRenderer>().sprite = Body_Sprites[2];
        }
        else if (body_no == 4)
        {
            GameObject.Find("Player").transform.Find("body").GetComponent<SpriteRenderer>().sprite = Body_Sprites[3];
        }
        else if (body_no == 5)
        {
            GameObject.Find("Player").transform.Find("body").GetComponent<SpriteRenderer>().sprite = Body_Sprites[4];
        }
    }

    public void Legs_Change(int legs_no)
    {
        if (legs_no == 1)
        {
            GameObject.Find("Player").transform.Find("left leg").GetComponent<SpriteRenderer>().sprite = Left_Leg_Sprites[0];
            GameObject.Find("Player").transform.Find("right leg").GetComponent<SpriteRenderer>().sprite = Right_Leg_Sprites[0];
        }
        else if (legs_no == 2)
        {
            GameObject.Find("Player").transform.Find("left leg").GetComponent<SpriteRenderer>().sprite = Left_Leg_Sprites[1];
            GameObject.Find("Player").transform.Find("right leg").GetComponent<SpriteRenderer>().sprite = Right_Leg_Sprites[1];
        }
        else if (legs_no == 3)
        {
            GameObject.Find("Player").transform.Find("left leg").GetComponent<SpriteRenderer>().sprite = Left_Leg_Sprites[2];
            GameObject.Find("Player").transform.Find("right leg").GetComponent<SpriteRenderer>().sprite = Right_Leg_Sprites[2];
        }
        else if (legs_no == 4)
        {
            GameObject.Find("Player").transform.Find("left leg").GetComponent<SpriteRenderer>().sprite = Left_Leg_Sprites[3];
            GameObject.Find("Player").transform.Find("right leg").GetComponent<SpriteRenderer>().sprite = Right_Leg_Sprites[3];
        }
        else if (legs_no == 5)
        {
            GameObject.Find("Player").transform.Find("left leg").GetComponent<SpriteRenderer>().sprite = Left_Leg_Sprites[4];
            GameObject.Find("Player").transform.Find("right leg").GetComponent<SpriteRenderer>().sprite = Right_Leg_Sprites[4];
        }
    }

    public void Hands_Change(int hands_no)
    {
        if (hands_no == 1)
        {
            GameObject.Find("Player").transform.Find("left arm").GetComponent<SpriteRenderer>().sprite = Left_Arm_Sprites[0];
            GameObject.Find("Player").transform.Find("right arm").GetComponent<SpriteRenderer>().sprite = Right_Arm_Sprites[0];
        }
        else if (hands_no == 2)
        {
            GameObject.Find("Player").transform.Find("left arm").GetComponent<SpriteRenderer>().sprite = Left_Arm_Sprites[1];
            GameObject.Find("Player").transform.Find("right arm").GetComponent<SpriteRenderer>().sprite = Right_Arm_Sprites[1];
        }
        else if (hands_no == 3)
        {
            GameObject.Find("Player").transform.Find("left arm").GetComponent<SpriteRenderer>().sprite = Left_Arm_Sprites[2];
            GameObject.Find("Player").transform.Find("right arm").GetComponent<SpriteRenderer>().sprite = Right_Arm_Sprites[2];
        }
        else if (hands_no == 4)
        {
            GameObject.Find("Player").transform.Find("left arm").GetComponent<SpriteRenderer>().sprite = Left_Arm_Sprites[3];
            GameObject.Find("Player").transform.Find("right arm").GetComponent<SpriteRenderer>().sprite = Right_Arm_Sprites[3];
        }
        else if (hands_no == 5)
        {
            GameObject.Find("Player").transform.Find("left arm").GetComponent<SpriteRenderer>().sprite = Left_Arm_Sprites[4];
            GameObject.Find("Player").transform.Find("right arm").GetComponent<SpriteRenderer>().sprite = Right_Arm_Sprites[4];
        }
    }

    public void LoadParticle(GameObject particle)
    {
        if (TrailCollector.transform.childCount == 0)
        {
            Instantiate(Select_Trail, TrailCollector.transform.localPosition, Quaternion.identity, TrailCollector.transform);
        }
        else if (TrailCollector.transform.childCount > 0)
        {
            Destroy(TrailCollector.transform.GetChild(0).gameObject);
            Instantiate(Select_Trail, TrailCollector.transform.localPosition, Quaternion.identity, TrailCollector.transform);
        }
    }
}
