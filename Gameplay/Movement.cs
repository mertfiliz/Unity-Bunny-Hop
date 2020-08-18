using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 10f;

    public Vector2 player_min_pos, player_pos;

    Rigidbody2D rb;    

    public List<GameObject> BlockList = new List<GameObject>();

    public static GameObject Player;

    public Sprite Bunny_Left, Bunny_Right;

    public Text score;

    public static float temp_score = 0, current_score = 0, final_score = 0;

    public AudioSource Mushroom_Sound, Grass_Sound, Break_Sound, Ouch_Sound, Blop_Sound, Game_Sound, Hit_Sound;

    public static bool isPlayerAscending = false;

    public bool isBunnyAir, isBunnyJump;

    public static bool StartGame = false;

    public static bool StartGameCountdownEnded = false;

    public Sprite large_green, mid_green, small_green, large_orange, mid_orange, small_orange, large_purple, mid_purple, small_purple;

    public GameObject Large_Prefab, Mid_Prefab, Small_Prefab;

    public Sprite Green_Background, Orange_Background, Purple_Background;

    public float max_height = 0, temp_height = 0;

    public static bool isPortalsActivated = false;

    public static GameObject Left_Portal, Right_Portal, Current_World_Img, Left_World_Img, Right_World_Img;

    public Vector2 Bg_LastPos;

    public float Score_Multiplier;

    private bool isPlayerLeft = true;

    public GameObject Item;

    public GameObject Space_Img;

    void Awake()
    {
        // START GREEN INITIALLY - WORLD 1
        Large_Prefab.GetComponent<Image>().sprite = large_green;
        Mid_Prefab.GetComponent<Image>().sprite = mid_green;
        Small_Prefab.GetComponent<Image>().sprite = small_green;

        Set_GameSounds();
    }
        
    void Start()
    {
        Space_Img.SetActive(false);
        AdManager.AdManage.bannerView.Destroy();
        AdManager.AdManage.Request_Banner();

        GameOverController.End_Panel.SetActive(false);
        StartGame = false;
        StartGameCountdownEnded = false;
        current_score = 0;
        temp_score = 0;

        Application.targetFrameRate = 200;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Player = GameObject.Find("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        Left_Portal = GameObject.Find("Left_Portal");
        Right_Portal = GameObject.Find("Right_Portal");
        Current_World_Img = GameObject.Find("Current_World_Img");
        Left_World_Img = GameObject.Find("Left_World_Img");
        Right_World_Img = GameObject.Find("Right_World_Img");

        Game_Sound.Play();

        BlockList_Hide();
        CarrotList_Hide();
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        PlayerDataUpgrade loadedData = SaveLoadUpgrade.LoadPlayer();
        Score_Multiplier = loadedData.Score_Multiplier;
    }

    public void Set_GameSounds()
    {
        PlayerDataSettings loadedData = SaveLoadSettings.LoadPlayer();
        int music_vol = loadedData.music_vol_selection;
        int effect_vol = loadedData.effect_vol_selection;

        if(music_vol == 0)
        {
            Game_Sound.volume = 0f;
        }
        else if(music_vol == 1)
        {
            Game_Sound.volume = 0.05f;
        }
        else if (music_vol == 2)
        {
            Game_Sound.volume = 0.10f;
        }
        else if (music_vol == 3)
        {
            Game_Sound.volume = 0.20f;
        }
        else if (music_vol == 4)
        {
            Game_Sound.volume = 0.30f;
        }
        else if (music_vol == 5)
        {
            Game_Sound.volume = 0.40f;
        }        
        
        if(effect_vol == 0)
        {
            Mushroom_Sound.volume = 0f;
            Grass_Sound.volume = 0f;
            Break_Sound.volume = 0f;
            Ouch_Sound.volume = 0f;
            Blop_Sound.volume = 0f;
            Hit_Sound.volume = 0f;
        }
        else if (effect_vol == 1)
        {
            Mushroom_Sound.volume = 0.1f;
            Grass_Sound.volume = 0.02f;
            Break_Sound.volume = 0.05f;
            Ouch_Sound.volume = 0.2f;
            Blop_Sound.volume = 0.2f;
            Hit_Sound.volume = 0.2f;
        }
        else if (effect_vol == 2)
        {
            Mushroom_Sound.volume = 0.2f;
            Grass_Sound.volume = 0.04f;
            Break_Sound.volume = 0.1f;
            Ouch_Sound.volume = 0.4f;
            Blop_Sound.volume = 0.4f;
            Hit_Sound.volume = 0.4f;
        }
        else if (effect_vol == 3)
        {
            Mushroom_Sound.volume = 0.3f;
            Grass_Sound.volume = 0.06f;
            Break_Sound.volume = 0.15f;
            Ouch_Sound.volume = 0.6f;
            Blop_Sound.volume = 0.6f;
            Hit_Sound.volume = 0.6f;
        }
        else if (effect_vol == 4)
        {
            Mushroom_Sound.volume = 0.4f;
            Grass_Sound.volume = 0.08f;
            Break_Sound.volume = 0.22f;
            Ouch_Sound.volume = 0.8f;
            Blop_Sound.volume = 0.8f;
            Hit_Sound.volume = 0.8f;
        }
        else if (effect_vol == 5)
        {
            Mushroom_Sound.volume = 0.5f;
            Grass_Sound.volume = 0.1f;
            Break_Sound.volume = 0.3f;
            Ouch_Sound.volume = 1f;
            Blop_Sound.volume = 1f;
            Hit_Sound.volume = 1f;
        }
    }

    public static void BlockList_Hide()
    {
        for (int i = 1; i < GenerateBlocks.BlockList.Count; i++)
        {
            GenerateBlocks.BlockList[i].SetActive(false);
        }
    }

    public static void CarrotList_Hide()
    {   
        for (int i = 0; i < GenerateBlocks.CarrotList.Count; i++)
        {
            GenerateBlocks.CarrotList[i].SetActive(false);
        }              
    }

    void Update()
    {     
        AccelerometerMove();

        // Player Up 
        if (rb.velocity.y > 0)
        {
            temp_height = Player.transform.localPosition.y;
            isPlayerAscending = true;

            GenerateBlocks.Block_Hide();
            Player.GetComponent<Animator>().SetBool("isPlayerUp", true);
            Player.GetComponent<Animator>().SetBool("isPlayerDown", false);

            //Player.transform.Find("Down_Anim_Box").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Down_Anim_Box").GetComponent<BoxCollider2D>().enabled = false;
        }

        // Player Down 
        if (rb.velocity.y <= 0)
        {
            isPlayerAscending = false;
            GenerateBlocks.Block_Show();
            Player.GetComponent<Animator>().SetBool("isPlayerUp", false);
            Player.GetComponent<Animator>().SetBool("isPlayerDown", true);
            //Player.transform.Find("Down_Anim_Box").GetComponent<BoxCollider2D>().enabled = true;
            GameObject.Find("Down_Anim_Box").GetComponent<BoxCollider2D>().enabled = true;
        }

        // Current Score
        if (max_height < temp_height)
        {
            max_height = temp_height;
            current_score += 10 * Score_Multiplier * PlayerData.Score_Multiplier_Power;
        }
        score.text = current_score.ToString("F0");
       
        if (!CameraMovement.Camera_Fall_Stop)
        {
            if(Player.transform.position.y > Item.transform.position.y)
            {
                Debug.Log("PASSED");
                Space_Img.SetActive(true);                  
            }
            else
            {
                Bg_LastPos = GameObject.Find("Background").transform.position = new Vector3(GameObject.Find("Background").transform.position.x, Player.transform.position.y - Player.transform.position.y / 18, 1);
            }
            Space_Img.transform.position = new Vector3(Space_Img.transform.position.x, Player.transform.position.y, 1);
        }
        else
        {
            GameObject.Find("Background").transform.position = new Vector3(GameObject.Find("Background").transform.position.x, GameObject.Find("Background").transform.position.y);
        }

        if(!isPortalsActivated)
        {
            Left_Portal.GetComponent<Image>().enabled = false;
            Right_Portal.GetComponent<Image>().enabled = false;
        }
        else
        {
            Left_Portal.GetComponent<Image>().enabled = true;
            Right_Portal.GetComponent<Image>().enabled = true;
        }  
    }

    void FixedUpdate()
    {
        if(StartGame && StartGameCountdownEnded)
        {
            if (Input.GetKey(KeyCode.A))
            {
                isPlayerLeft = true;
                Player.transform.position += Vector3.left * 14f * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                isPlayerLeft = false;
                Player.transform.position += Vector3.right * 14f * Time.deltaTime;
            }
        }       
    }

    void LateUpdate()
    {
        if(isPlayerLeft)
        {
            Player.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            Player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public float getAcceleration()
    {
        float x_axis = Input.acceleration.x;
        return x_axis;
    }

    void AccelerometerMove()
    {
        if (StartGame && StartGameCountdownEnded)
        {
            float x = getAcceleration();

            if (x < 0.01)
            {
                isPlayerLeft = true;
            }
            if (x > 0.01)
            {
                isPlayerLeft = false;
            }

            if (x < -0.035f)
            {                
                rb.velocity = new Vector2(30f * x, rb.velocity.y);
            }

            if (x > 0.035f)
            {                
                rb.velocity = new Vector2(30f * x, rb.velocity.y);         
            } 
        }       
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        rb.drag = 0;       
        player_min_pos = Player.transform.localPosition;
        float x = 0;

        Player.GetComponent<Animator>().SetBool("isPlayerDownTrigger", false);
        Player.GetComponent<Animator>().SetBool("isPlayerDown", false);
        Player.GetComponent<Animator>().SetBool("isPlayerUp", true);

        if (StartGame && StartGameCountdownEnded)
        {
            x = getAcceleration();
        }        

        if (col.gameObject.tag == "jumpblock")
        {
            Player.GetComponent<Animator>().SetTrigger("isPlayerJump");
            col.gameObject.GetComponent<Animator>().SetTrigger("MushroomJump");
            Mushroom_Sound.Play();           
            rb.AddForce(new Vector2(rb.velocity.x, 40f), ForceMode2D.Impulse);
        }
        else if (col.gameObject.tag == "toggle")
        {
            Break_Sound.Play();
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(20 * x, 20f), ForceMode2D.Impulse);
        }

        else if(col.gameObject.tag == "startblock")
        {          
            if(StartGameCountdownEnded)
            {
                for (int i = 0; i < GenerateBlocks.BlockList.Count; i++)
                {
                    GenerateBlocks.BlockList[i].SetActive(true);
                }
                for (int i = 0; i < GenerateBlocks.CarrotList.Count; i++)
                {
                    GenerateBlocks.CarrotList[i].SetActive(true);
                }

                StartGameScript.ins.Countdown.text = "Go!";
                StartGameScript.Top_Panel.SetActive(true);
                StartGame = true;
            }      
        }

        else if(col.gameObject.tag == "trap")
        {
            StartCoroutine(CameraShake.CamShake.Camera_Shake());
            Game_Sound.Stop();
            Ouch_Sound.Play();
            Player.transform.localPosition = new Vector2(0, Player.transform.localPosition.y);
            StartGame = false;
            GameOver();
        }

        else
        {
            Grass_Sound.Play();
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(20 * x, 20f), ForceMode2D.Impulse);
        }
    }

    public void GameOver()
    {        
        GameOverController.GameOver();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        float x = 0;
        if(StartGame && StartGameCountdownEnded)
        {
            x = getAcceleration();
        }
        rb.velocity = Vector2.zero;
        player_min_pos = Player.transform.localPosition;      

        if(col.gameObject.tag == "jumpblock")
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(20* x, 40f), ForceMode2D.Impulse);
        }
        else if (col.gameObject.tag == "toggle")
        {
            Break_Sound.Play();
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(20 * x, 20f), ForceMode2D.Impulse);
        }
        else if (col.gameObject.tag == "trap")
        {
          
        }        
        else
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(20 * x, 20f), ForceMode2D.Impulse);
        }
    }       

    void OnTriggerEnter2D(Collider2D coll)
    {        
        if (coll.gameObject.name == "LeftScreen")
        {                       
            Player.transform.localPosition = new Vector2(GameObject.Find("RightScreen").transform.localPosition.x - 100, transform.localPosition.y);
            
            if(isPortalsActivated)
            {
                if (Portals.CurrentWorld == 0)
                {
                    Portals.CurrentWorld = 2;
                }
                else if (Portals.CurrentWorld == 1)
                {
                    Portals.CurrentWorld = 0;
                }
                else if (Portals.CurrentWorld == 2)
                {
                    Portals.CurrentWorld = 1;
                }

                Change_World_Settings(Portals.CurrentWorld);
            }
        }

        if (coll.gameObject.name == "RightScreen")
        {      
            Player.transform.localPosition = new Vector2(GameObject.Find("LeftScreen").transform.localPosition.x + 100, transform.localPosition.y);

            if (isPortalsActivated)
            {
                if (Portals.CurrentWorld == 0)
                {
                    Portals.CurrentWorld = 1;
                    Change_World_Settings(1);
                }
                else if (Portals.CurrentWorld == 1)
                {
                    Portals.CurrentWorld = 2;
                    Change_World_Settings(2);
                }
                else if (Portals.CurrentWorld == 2)
                {
                    Portals.CurrentWorld = 0;
                    Change_World_Settings(0);
                }        
            }
        }
    }
   

    public void Change_World_Settings(int world_no)
    {
        // Yellow
        if(world_no == 0)
        {
            // New generated blocks will be correct.
            Large_Prefab.GetComponent<Image>().sprite = large_orange;
            Mid_Prefab.GetComponent<Image>().sprite = mid_orange;
            Small_Prefab.GetComponent<Image>().sprite = small_orange;

            // Background Change
            GameObject.Find("Background").transform.GetChild(0).GetComponent<Image>().sprite = Orange_Background;

            // Current block's image change.
            for (int i = 0; i < GenerateBlocks.BlockList.Count; i++)
            {
                if(GenerateBlocks.BlockList[i].gameObject.tag == "largeblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = large_orange;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "midblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = mid_orange;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "smallblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = small_orange;
                }

                if (GenerateBlocks.BlockList[i].gameObject.tag == "orangespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "bluespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = false;
                }               
                if (GenerateBlocks.BlockList[i].gameObject.tag == "purplespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }            
        }

        // Blue
        else if(world_no == 1)
        {
            // New generated blocks will be correct.
            Large_Prefab.GetComponent<Image>().sprite = large_green;
            Mid_Prefab.GetComponent<Image>().sprite = mid_green;
            Small_Prefab.GetComponent<Image>().sprite = small_green;

            // Background Change
            GameObject.Find("Background").transform.GetChild(0).GetComponent<Image>().sprite = Green_Background;

            // Current block's image change.
            for (int i = 0; i < GenerateBlocks.BlockList.Count; i++)
            {
                if (GenerateBlocks.BlockList[i].gameObject.tag == "largeblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = large_green;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "midblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = mid_green;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "smallblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = small_green;
                }

                if (GenerateBlocks.BlockList[i].gameObject.tag == "orangespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "bluespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = true;
                }               
                if (GenerateBlocks.BlockList[i].gameObject.tag == "purplespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        else if (world_no == 2)
        {
            // New generated blocks will be correct.
            Large_Prefab.GetComponent<Image>().sprite = large_purple;
            Mid_Prefab.GetComponent<Image>().sprite = mid_purple;
            Small_Prefab.GetComponent<Image>().sprite = small_purple;

            // Background Change
            GameObject.Find("Background").transform.GetChild(0).GetComponent<Image>().sprite = Purple_Background;

            // Current block's image change.
            for (int i = 0; i < GenerateBlocks.BlockList.Count; i++)
            {
                if (GenerateBlocks.BlockList[i].gameObject.tag == "largeblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = large_purple;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "midblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = mid_purple;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "smallblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<Image>().sprite = small_purple;
                }

                if (GenerateBlocks.BlockList[i].gameObject.tag == "orangespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "bluespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                if (GenerateBlocks.BlockList[i].gameObject.tag == "purplespecialblock")
                {
                    GenerateBlocks.BlockList[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }
}
