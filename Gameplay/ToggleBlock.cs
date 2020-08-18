using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBlock : MonoBehaviour
{
    public bool toggle = false;
    public Sprite Toggle_Show_Img, Toggle_Hide_Img;
    public GameObject BlockRemover;

    public Sprite Invisible_1, Invisible_2;

    void Start()
    {
        BlockRemover = GameObject.Find("BlockRemover");
        Show_Block();
    }

    void Update()
    {
       if(BlockRemover.transform.localPosition.y > this.gameObject.transform.localPosition.y)
       {
            GenerateBlocks.BlockList.Remove(this.gameObject);
            Destroy(this.gameObject);
       }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine("Anim_ToggleBlock");
    }

    public void Show_Block()
    {
        this.gameObject.GetComponent<Image>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        toggle = true;
    }

    public void Hide_Block()
    {
        this.gameObject.GetComponent<Image>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        toggle = false;
        Invoke("Show_Block", 4f);
    }

    public IEnumerator Anim_ToggleBlock()
    {       
        this.gameObject.GetComponent<Image>().sprite = Invisible_2;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Vector2 start_pos_temp = this.gameObject.transform.localPosition;
        Vector2 start_pos = this.gameObject.transform.localPosition;
        Vector2 end_pos = new Vector2(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y -500);

        while(start_pos != end_pos)
        {
            start_pos = Vector2.MoveTowards(start_pos, end_pos, 15f);
            this.gameObject.transform.localPosition = start_pos;
            yield return new WaitForSeconds(0.01f);
        }

        this.gameObject.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<Image>().enabled = true;
        this.gameObject.GetComponent<Image>().sprite = Invisible_1;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        start_pos = start_pos_temp;
        this.gameObject.transform.localPosition = start_pos;

        yield return null;
    }
}
