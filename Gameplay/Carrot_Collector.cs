using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrot_Collector : MonoBehaviour
{
    public static Carrot_Collector Carrot_C;
    public AudioSource Blop_Sound;
    public int carrot_ct = 0, Carrot_Points;
    private float carrot_bar_ct = 0f, carrot_bar_max = 20f;
    public Text Carrot_Text, Carrot_Combo_Text, Carrot_Point_Text, Score;
    public GameObject Carrot_Bar_Back, Score_Multiplier, Carrot_Point_Collector;
    public Image Carrot_Bar_Image;
    private bool isCarrotBarActivated = false;
    private int Carrot_combo = 0, Carrot_Score_Combo = 0;

    void Start()
    {
        Carrot_C = this;
        Carrot_Text.text = carrot_ct.ToString();
        Carrot_Bar_Back.SetActive(false);
        LoadCarrotBonus();
    }

    void Update()
    {
        transform.localPosition = new Vector2(GameObject.Find("Player").transform.localPosition.x, GameObject.Find("Player").transform.localPosition.y + 100);

        if (Carrot_combo >= 3)
        {
            Carrot_Combo_Text.text = "x" + Carrot_combo.ToString();
            Carrot_Combo_Text.gameObject.SetActive(true);
        }else
        {
            Carrot_Combo_Text.gameObject.SetActive(false);
        }
    }

    public void LoadCarrotBonus()
    {
        PlayerDataUpgrade loadedData = SaveLoadUpgrade.LoadPlayer();
        Carrot_Points = loadedData.Carrot_Points;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {       
        if (coll.gameObject.tag == "carrot")
        {
            Carrot_combo++;
            StartCoroutine("Carrot_Combo");
            
            if(Carrot_Score_Combo > 0)
            {
                Movement.current_score += Carrot_Score_Combo;
                Carrot_Point_Text.text = Carrot_Score_Combo.ToString();
                Carrot_Point_Collector.transform.position = coll.transform.position;
                var carrot_point_text_ins = Instantiate(Carrot_Point_Text, coll.transform.position, Quaternion.identity, Carrot_Point_Collector.transform);
                StartCoroutine(DestroyCarrot_Text(carrot_point_text_ins));
            }   
            
            coll.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            carrot_ct += 1;
            Carrot_Bar_Back.SetActive(true);
            Carrot_Text.text = carrot_ct.ToString();
            Carrot_Bar_Event();
            coll.gameObject.GetComponent<Animator>().SetTrigger("carrot_animation");
            Blop_Sound.Play();
            StartCoroutine(Destroy_Carrot(coll.gameObject));
        }
    }

    IEnumerator DestroyCarrot_Text(Text carrot_text)
    {
        yield return new WaitForSeconds(2f);
        Destroy(carrot_text.gameObject);
        yield return null;
    }
    public void Carrot_Bar_Event()
    {       
        if(isCarrotBarActivated == false)
        {
            carrot_bar_ct += 1 / carrot_bar_max;
            Carrot_Bar_Image.fillAmount = carrot_bar_ct;
            if(carrot_bar_ct >= 1)
            {
                StartCoroutine(Carrot_Bar_Activate());
            }
            else {
                StartCoroutine(Carrot_Bar_Decrease());
            }
        }        
    }
    IEnumerator Carrot_Combo()
    {
        float start_time = 0f;
        var combo = Carrot_combo;
        while (start_time < 3f)
        {            
            if(combo != Carrot_combo)
            {
                start_time = 0f;
                combo = Carrot_combo;                
                if(Blop_Sound.pitch < 1.3f)
                {
                    Blop_Sound.pitch += 0.03f;
                }     
            }

            if (combo >= 10)
            {
                Carrot_Score_Combo = Carrot_Points + (combo * 100);
            }
            else if (combo >= 5)
            {
                Carrot_Score_Combo = Carrot_Points + (combo * 50);
            }
            else if (combo >= 2)
            {
                Carrot_Score_Combo = Carrot_Points + (combo * 20);                
            }
            else
            {
                Carrot_Score_Combo = Carrot_Points;
            }

            start_time+=0.1f;            
            
            yield return new WaitForSeconds(.1f);
        }

        Carrot_Score_Combo = Carrot_Points;
        Carrot_combo = 0;
        Blop_Sound.pitch = 1f;
        StopCoroutine("Carrot_Combo");
    }

    IEnumerator Carrot_Bar_Decrease()
    {
        while (carrot_bar_ct > 0f)
        {           
            if(!isCarrotBarActivated)
            {
                carrot_bar_ct -= 0.000035f;
                Carrot_Bar_Image.fillAmount = carrot_bar_ct;
            }           
            yield return new WaitForSeconds(0.1f);
        }      
        yield return null;
    }

    IEnumerator Carrot_Bar_Activate()
    {
        Color scorecolor = Score.color;
        Score_Multiplier.transform.localScale += new Vector3(0.4f, 0.4f, 0);
        Score.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        Score.color = new Color32(255, 204, 0, 255);
        carrot_bar_ct = 1f;
        isCarrotBarActivated = true;
        PlayerData.Score_Multiplier_Power = 2;

        yield return new WaitForSeconds(12f);       

        while (carrot_bar_ct > 0f)
        {
            carrot_bar_ct -= 0.01f;
            Carrot_Bar_Image.fillAmount = carrot_bar_ct;
            yield return new WaitForSeconds(0.05f);
        }

        PlayerData.Score_Multiplier_Power = 1;
        carrot_bar_ct = 0f;
        Carrot_Bar_Back.SetActive(false);
        isCarrotBarActivated = false;
        Score.color = scorecolor;
        Score.transform.localScale -= new Vector3(0.1f, 0.1f, 0);
        Score_Multiplier.transform.localScale += new Vector3(0.4f, 0.4f, 0);

        yield return null;
    }
    IEnumerator Destroy_Carrot(GameObject carrot)
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(carrot.gameObject);
        GenerateBlocks.CarrotList.Remove(carrot);
        yield return null;
    }
}
