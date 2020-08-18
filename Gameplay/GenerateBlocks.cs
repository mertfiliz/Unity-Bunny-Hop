using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GenerateBlocks : MonoBehaviour
{
    // Contains all blocks in BlockList
    public static List<GameObject> BlockList = new List<GameObject>();

    // Contains all carrots in CarrotList
    public static List<GameObject> CarrotList = new List<GameObject>();

    // Initial block
    public GameObject Start_Block;

    // Block types
    public GameObject LargeBlock, MiddleBlock, SmallBlock,
                      MoveLargeBlock, MoveMidBlock, MoveSmallBlock,
                      ToggleLargeBlock, ToggleMidBlock, ToggleSmallBlock,
                      BlueSpecialBlock, OrangeSpecialBlock, PurpleSpecialBlock;

    // Items
    public GameObject Mushroom, Carrot, Trap, Left_Trap, Right_Trap;

    public GameObject Player, Blocks, Carrots;

    Vector2 firstblock_pos, lastblock_pos, player_pos;

    public int level;
    
    // Sprites over grass
    public Sprite[] Large_OG, Mid_OG, Small_OG;
    
    Vector3 block_scale;

    public float Left_X_Object, Right_X_Object;

    public static float Time_Multiplier;
    public Text Level_Text, Score_Multiplier_Text;
    public int max_level_ct = 0;
    public static bool game_over;
    public int level_counter;
    public string level_UI;

    public float Score_Multiplier;
    public int Carrot_Spawn_Rate;

    void Start()
    {
        if(AdManager.AdManage.bannerView != null)
        {
            AdManager.AdManage.bannerView.Destroy();
        }
        
        // Limitting the block spawnings by checking edges of the screen
        Left_X_Object = GameObject.Find("Left_Block").transform.localPosition.x + 80;
        Right_X_Object = GameObject.Find("Right_Block").transform.localPosition.x - 80;
        block_scale = new Vector3(1, 1, 1);

        Left_Trap = GameObject.Find("Left_Trap");
        Right_Trap = GameObject.Find("Right_Trap");

        Player = GameObject.Find("Player");

        // Player mass changes with screen ratio.
        Player_Mass();

        Level_Text.text = "1";
        level = 1;
        level_counter = 0;
        level_UI = "0";
        Time_Multiplier = 1;

        Load_Settings();

        game_over = false;

        Movement.isPortalsActivated = false;

        Side_Traps_Deactivate();
        

        // Remove all blocks in start.
        if (BlockList != null)
        {
            foreach (var i in BlockList.ToArray())
            {
                BlockList.Remove(i);               
            }
        }

        // Remove all carrots in start.
        if (CarrotList != null)
        {
            foreach (var i in CarrotList.ToArray())
            {
                CarrotList.Remove(i);
            }
        }
     
        // Create a single start block at the beginning.
        BlockList.Add(Start_Block);

        // Create the rest of the blocks.
        Create_Blocks();
    }

    void Update()
    {
        Check_Player_Position();

        Score_Multiplier_Text.text = "x" + (Score_Multiplier * PlayerData.Score_Multiplier_Power).ToString("F1");
        Time.timeScale = Time_Multiplier;

        if (game_over == false)
        {
            Check_GameOver();
        }
    }

    public void Load_Settings()
    {
        PlayerDataUpgrade loadedData = SaveLoadUpgrade.LoadPlayer();
        Score_Multiplier = loadedData.Score_Multiplier;
        Carrot_Spawn_Rate = loadedData.Carrot_Spawn_Rate;
    }

    public void Side_Traps_Deactivate()
    {
        Left_Trap.GetComponent<Image>().enabled = false;
        Right_Trap.GetComponent<Image>().enabled = false;

        Left_Trap.GetComponent<BoxCollider2D>().enabled = false;
        Right_Trap.GetComponent<BoxCollider2D>().enabled = false;
    }

    private IEnumerator Side_Traps_Activate()
    {
        Left_Trap.GetComponent<Image>().enabled = true;
        Right_Trap.GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(0.2f);

        Left_Trap.GetComponent<Image>().enabled = false;
        Right_Trap.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(0.2f);

        Left_Trap.GetComponent<Image>().enabled = true;
        Right_Trap.GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(0.2f);

        Left_Trap.GetComponent<Image>().enabled = false;
        Right_Trap.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(0.2f);

        Left_Trap.GetComponent<Image>().enabled = true;
        Right_Trap.GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(0.2f);

        Left_Trap.GetComponent<Image>().enabled = false;
        Right_Trap.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(0.1f);

        Left_Trap.GetComponent<Image>().enabled = true;
        Right_Trap.GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        Left_Trap.GetComponent<Image>().enabled = false;
        Right_Trap.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(0.1f);

        Left_Trap.GetComponent<Image>().enabled = true;
        Right_Trap.GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        Left_Trap.GetComponent<Image>().enabled = false;
        Right_Trap.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(0.1f);
        Left_Trap.GetComponent<Image>().enabled = true;
        Right_Trap.GetComponent<Image>().enabled = true;
        Left_Trap.GetComponent<BoxCollider2D>().enabled = true;
        Right_Trap.GetComponent<BoxCollider2D>().enabled = true;
        yield return null;
    }

    // Destroy desired block.
    public static void Remove_Block(GameObject block)
    {
        BlockList.Remove(block);
        Destroy(block);
    }
    
    // Check if game is over or not.
    public void Check_GameOver()
    {
        if (player_pos.y < firstblock_pos.y - 2000)
        {
            game_over = true;
            GameObject.Find("Hit_Sound").GetComponent<AudioSource>().Play();
            StartCoroutine(CameraShake.CamShake.Camera_Shake());
            Invoke("GameOver", 0.1f);
        }
    }

    // Check Player position
    public void Check_Player_Position()
    {
        firstblock_pos = BlockList[0].transform.localPosition;
        lastblock_pos = BlockList[(BlockList.Count - 1)].transform.localPosition;
        
        player_pos = Player.transform.localPosition;

        // If player pos.y is lesser than first block's pos.y
        if(player_pos.y < firstblock_pos.y)
        {
            GameObject.Find("Game_Sound").GetComponent<AudioSource>().Stop();
            CameraMovement.Camera_Fall(firstblock_pos.y);
        }
        
        // If Blocklist has 20 or more blocks.
        if (BlockList.Count > 20)
        {    
            // If player pos.y is reached certain position of created blocks pos. y
            if (player_pos.y >= BlockList[BlockList.Count - 20].gameObject.transform.localPosition.y)
            {                
                // Increase level, call LevelManageri and Create Blocks.
                level_counter++;
                LevelManager(level_counter);
                Create_Blocks();
            }    
        }
    }

    public void LevelManager(int lv_ct)
    {
        if (lv_ct % 2 == 0)
        {
            level++;
            level_UI = level.ToString();
            Level_Text.text = level.ToString();

            if (level == 3)
            {
                StartCoroutine(Side_Traps_Activate());
            }
            if (level == 4)
            {
                Side_Traps_Deactivate();           
            }
            if (level == 6)
            {
                StartCoroutine(Side_Traps_Activate());
            }
            if (level == 7)
            {
                Side_Traps_Deactivate();
            }
            if (level == 9)
            {
                Movement.isPortalsActivated = true;
            }
            if (level == 12)
            {
                Time_Multiplier += 0.05f;
            }

            Score_Multiplier += 0.2f;
            Time_Multiplier += 0.05f;
        }        
    }

    public void GameOver()
    {
        GameOverController.GameOver();        
    }
    
    public static int Get_ScreenSize()
    {
        int screen_x = Screen.width;
        int screen_y = Screen.height;

        int screen_size_x = 410;

        if(screen_x == 1080 && screen_y == 1920)
        {
            screen_size_x = 410;
        }

        return screen_size_x;
    }   

    private void Player_Mass()
    {
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        rb.mass = 1;

        float screen_x = Screen.width;
        float screen_y = Screen.height;

        float screen_ratio = screen_y / screen_x;

        Debug.Log(screen_ratio);

        if (screen_ratio >= 2.17f)
        {
            rb.mass = 1;
        }

        else if(screen_ratio >= 2.16f && screen_ratio < 2.17f)
        {
            rb.mass = 1;
        }
        else if (screen_ratio >= 2.05f && screen_ratio < 2.16f)
        {
            rb.mass = 0.97f;
        }
        else if (screen_ratio >= 2.00f && screen_ratio < 2.05f)
        {
            rb.mass = 0.958f;
        }
        else if (screen_ratio >= 1.77f && screen_ratio < 2.00f)
        {
            rb.mass = 0.904f;
        }
        else if (screen_ratio >= 1.66f && screen_ratio < 1.77f)
        {
            rb.mass = 0.88f;
        }
        else if (screen_ratio >= 1.50f && screen_ratio < 1.66f)
        {
            rb.mass = 0.83f;
        }
        else if (screen_ratio < 1.50f)
        {
            rb.mass = 0.83f;
        }
    }

    public void Create_Blocks()
    {       
        if (level == 1)
        {
            var up_distance = 220;
            var block_distance = 120;

            for (int i = 0; i < 25; i++)
            {               
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);
                
                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (0%)
                if (rnd_block > 100)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    
                    // Regular Small Block (85%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {                            
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);                            
                        }
                        
                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);                           
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (10%)
                else if (rnd_block > 90)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    
                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }
                                
                // Large Block (90%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    
                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (0%)
                        if (rnd_large_OS > 100)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 95)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (95%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }                    

                        // Add list
                        BlockList.Add(largeblock);
                    }                    
                }

                //Debug.Log(BlockList[i]);

                // Carrot 12%
                if (rnd_block > 88 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count-1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 2)
        {
            var up_distance = 260;
            var block_distance = 120;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (0%)
                if (rnd_block > 100)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (85%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (10%)
                else if (rnd_block > 90)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }
                               
                // Large Block (90%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (0%)
                        if (rnd_large_OS > 100)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 95)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (95%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 14%
                if (rnd_block > 86 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 3)
        {
            var up_distance = 320;
            var block_distance = 120;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (0%)
                if (rnd_block > 100)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (85%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (20%)
                else if (rnd_block > 80)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }
             
                // Large Block (80%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (85%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (0%)
                        if (rnd_large_OS > 100)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 95)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (95%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 16%
                if (rnd_block > 84 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 4)
        {
            var up_distance = 380;
            var block_distance = 120;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (10%)
                if (rnd_block > 90)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (20%)
                else if (rnd_block > 70)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }                

                // Large Block (70%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (0%)
                        if (rnd_large_OS > 100)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 95)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (95%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 18%
                if (rnd_block > 82 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 5)
        {
            var up_distance = 440;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (20%)
                if (rnd_block > 80)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (80%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (20%)
                else if (rnd_block > 60)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (85%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }             

                // Large Block (60%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (85%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (5%)
                        if (rnd_large_OS > 95)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 90)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (90%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 20%
                if (rnd_block > 80 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 6)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (20%)
                if (rnd_block > 80)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (30%)
                else if (rnd_block > 50)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }
             
                // Large Block (60%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (5%)
                        if (rnd_large_OS > 95)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 90)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (90%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 22%
                if (rnd_block > 78 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 7)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (20%)
                if (rnd_block > 80)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (85%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (30%)
                else if (rnd_block > 50)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }               

                // Large Block (50%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (5%)
                    else if (rnd_blocktype > 90)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (8%)
                        if (rnd_large_OS > 92)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 87)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (87%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 24%
                if (rnd_block > 76 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 8)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (25%)
                if (rnd_block > 75)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (15%)
                    else if (rnd_blocktype > 80)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (80%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (35%)
                else if (rnd_block > 40)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (15%)
                    else if (rnd_blocktype > 80)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (80%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }
               
                // Large Block (40%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (15%)
                    else if (rnd_blocktype > 80)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (10%)
                        if (rnd_large_OS > 90)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 85)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (85%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 26%
                if (rnd_block > 74 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 9)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (20%)
                if (rnd_block > 75)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (20%)
                    else if (rnd_blocktype > 75)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (75%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (20%)
                else if (rnd_block > 40)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (20%)
                    else if (rnd_blocktype > 75)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }

                // Special Block (5%)
                else if (rnd_block > 35)
                {
                    // 1,2,3
                    int rnd_special = Random.Range(1, 4);

                    if (rnd_special == 1)
                    {
                        // Instantiate Blue Special Block
                        var bluespecialblock = Instantiate(BlueSpecialBlock, BlueSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        bluespecialblock.transform.localScale = block_scale;
                        bluespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(bluespecialblock);
                    }
                    else if (rnd_special == 2)
                    {
                        // Instantiate Orange Special Block
                        var orangespecialblock = Instantiate(OrangeSpecialBlock, OrangeSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        orangespecialblock.transform.localScale = block_scale;
                        orangespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(orangespecialblock);
                    }
                    else if (rnd_special == 3)
                    {
                        // Instantiate Purple Special Block
                        var purplespecialblock = Instantiate(PurpleSpecialBlock, PurpleSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        purplespecialblock.transform.localScale = block_scale;
                        purplespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(purplespecialblock);
                    }

                }

                // Large Block (55%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (20%)
                    else if (rnd_blocktype > 75)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (10%)
                        if (rnd_large_OS > 90)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 85)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (85%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 28%
                if (rnd_block > 72 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 10)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (20%)
                if (rnd_block > 80)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (25%)
                    else if (rnd_blocktype > 70)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (75%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (20%)
                else if (rnd_block > 60)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (25%)
                    else if (rnd_blocktype > 70)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }

                // Special Block (20%)
                else if (rnd_block > 40)
                {
                    // 1,2,3
                    int rnd_special = Random.Range(1, 4);

                    if (rnd_special == 1)
                    {
                        // Instantiate Blue Special Block
                        var bluespecialblock = Instantiate(BlueSpecialBlock, BlueSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        bluespecialblock.transform.localScale = block_scale;
                        bluespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(bluespecialblock);
                    }
                    else if (rnd_special == 2)
                    {
                        // Instantiate Orange Special Block
                        var orangespecialblock = Instantiate(OrangeSpecialBlock, OrangeSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        orangespecialblock.transform.localScale = block_scale;
                        orangespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(orangespecialblock);
                    }
                    else if (rnd_special == 3)
                    {
                        // Instantiate Purple Special Block
                        var purplespecialblock = Instantiate(PurpleSpecialBlock, PurpleSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        purplespecialblock.transform.localScale = block_scale;
                        purplespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(purplespecialblock);
                    }

                }

                // Large Block (40%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (25%)
                    else if (rnd_blocktype > 70)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (10%)
                        if (rnd_large_OS > 90)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 85)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (85%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 30%
                if (rnd_block > 70 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level == 11)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (30%)
                if (rnd_block > 70)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (50%)
                    else if (rnd_blocktype > 45)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (75%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (30%)
                else if (rnd_block > 40)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (50%)
                    else if (rnd_blocktype > 45)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }

                // Special Block (20%)
                else if (rnd_block > 40)
                {
                    // 1,2,3
                    int rnd_special = Random.Range(1, 4);

                    if (rnd_special == 1)
                    {
                        // Instantiate Blue Special Block
                        var bluespecialblock = Instantiate(BlueSpecialBlock, BlueSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        bluespecialblock.transform.localScale = block_scale;
                        bluespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(bluespecialblock);
                    }
                    else if (rnd_special == 2)
                    {
                        // Instantiate Orange Special Block
                        var orangespecialblock = Instantiate(OrangeSpecialBlock, OrangeSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        orangespecialblock.transform.localScale = block_scale;
                        orangespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(orangespecialblock);
                    }
                    else if (rnd_special == 3)
                    {
                        // Instantiate Purple Special Block
                        var purplespecialblock = Instantiate(PurpleSpecialBlock, PurpleSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        purplespecialblock.transform.localScale = block_scale;
                        purplespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(purplespecialblock);
                    }

                }

                // Large Block (20%)
                else if (rnd_block >= 1)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (50%)
                    else if (rnd_blocktype > 45)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (10%)
                        if (rnd_large_OS > 90)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 85)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (85%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 30%
                if (rnd_block > 70 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }

        if (level >= 12)
        {
            var up_distance = 500;
            var block_distance = 160;

            for (int i = 0; i < 25; i++)
            {
                // Getting last block y-position
                float lastblock_pos_y = BlockList[BlockList.Count - 1].transform.localPosition.y;
                // Random value between left and right position of the screen
                var next_x = Random.Range(Left_X_Object, Right_X_Object);
                // Max distance y-axis
                var next_y = lastblock_pos_y + up_distance;
                // Random y-axis starting with block distance value
                var rnd_y = Random.Range(lastblock_pos_y + block_distance, next_y);

                // 1,2,3 .. 100
                int rnd_block = Random.Range(1, 101);

                // Small Block (50%)
                if (rnd_block > 50)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Small Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Small Block
                        var smallblock = Instantiate(MoveSmallBlock, MoveSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }
                    // Toggle Small Block (75%)
                    else if (rnd_blocktype > 20)
                    {
                        // Instantiate Toggle Small Block
                        var smallblock = Instantiate(ToggleSmallBlock, ToggleSmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(smallblock);
                    }

                    // Regular Small Block (75%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Small Block
                        var smallblock = Instantiate(SmallBlock, SmallBlock.transform.position, Quaternion.identity, Blocks.transform);
                        smallblock.transform.localScale = block_scale;
                        smallblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of smallblock
                        var small_OS_Mushroom = smallblock.gameObject.transform.Find("Small_OS_Mushroom");
                        var small_OS_Trap = smallblock.gameObject.transform.Find("Small_OS_Trap");
                        var small_OG = smallblock.gameObject.transform.Find("Small_OG").GetComponent<Image>();

                        // Set Grass image as false
                        small_OG.enabled = false;

                        // Set Random Over Special element over the Small Block
                        var rnd_small_OS = Random.Range(1, 101);

                        // Trap Small Block (0%)
                        if (rnd_small_OS > 100)
                        {
                            Instantiate(Trap, small_OS_Trap.transform.position, Quaternion.identity, small_OS_Trap.transform);
                        }

                        // Jump Small Block (5%)
                        else if (rnd_small_OS > 95)
                        {
                            Instantiate(Mushroom, small_OS_Mushroom.transform.position, Quaternion.identity, small_OS_Mushroom.transform);
                        }

                        // OG Small Block (95%)
                        else if (rnd_small_OS >= 1)
                        {
                            // Set Random Over Grass element over the Small Block
                            var rnd_small_OG = Random.Range(0, Small_OG.Length);
                            small_OG.sprite = Small_OG[rnd_small_OG];
                            small_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(smallblock);
                    }

                }

                // Middle Block (30%)
                else if (rnd_block > 20)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Middle Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Middle Block
                        var middleblock = Instantiate(MoveMidBlock, MoveMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }
                    // Toggle Middle Block (75%)
                    else if (rnd_blocktype > 20)
                    {
                        // Instantiate Toggle Middle Block
                        var middleblock = Instantiate(ToggleMidBlock, ToggleMidBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(middleblock);
                    }

                    // Regular Middle Block (90%)
                    else if (rnd_blocktype >= 1)
                    {
                        // Instantiate Regular Middle Block
                        var middleblock = Instantiate(MiddleBlock, MiddleBlock.transform.position, Quaternion.identity, Blocks.transform);
                        middleblock.transform.localScale = block_scale;
                        middleblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of middleblock
                        var middle_OS_Mushroom = middleblock.gameObject.transform.Find("Middle_OS_Mushroom");
                        var middle_OS_Trap = middleblock.gameObject.transform.Find("Middle_OS_Trap");
                        var middle_OG = middleblock.gameObject.transform.Find("Middle_OG").GetComponent<Image>();

                        // Set Grass image as false
                        middle_OG.enabled = false;

                        // Set Random Over Special element over the Middle Block
                        var rnd_middle_OS = Random.Range(1, 101);

                        // Trap Middle Block (0%)
                        if (rnd_middle_OS > 100)
                        {
                            Instantiate(Trap, middle_OS_Trap.transform.position, Quaternion.identity, middle_OS_Trap.transform);
                        }

                        // Jump Middle Block (5%)
                        else if (rnd_middle_OS > 95)
                        {
                            Instantiate(Mushroom, middle_OS_Mushroom.transform.position, Quaternion.identity, middle_OS_Mushroom.transform);
                        }

                        // OG Middle Block (95%)
                        else if (rnd_middle_OS >= 1)
                        {
                            // Set Random Over Grass element over the Middle Block
                            var rnd_middle_OG = Random.Range(0, Mid_OG.Length);
                            middle_OG.sprite = Mid_OG[rnd_middle_OG];
                            middle_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(middleblock);
                    }

                }

                // Special Block (20%)
                else if (rnd_block >= 1)
                {
                    // 1,2,3
                    int rnd_special = Random.Range(1, 4);

                    if (rnd_special == 1)
                    {
                        // Instantiate Blue Special Block
                        var bluespecialblock = Instantiate(BlueSpecialBlock, BlueSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        bluespecialblock.transform.localScale = block_scale;
                        bluespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(bluespecialblock);
                    }
                    else if (rnd_special == 2)
                    {
                        // Instantiate Orange Special Block
                        var orangespecialblock = Instantiate(OrangeSpecialBlock, OrangeSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        orangespecialblock.transform.localScale = block_scale;
                        orangespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(orangespecialblock);
                    }
                    else if (rnd_special == 3)
                    {
                        // Instantiate Purple Special Block
                        var purplespecialblock = Instantiate(PurpleSpecialBlock, PurpleSpecialBlock.transform.position, Quaternion.identity, Blocks.transform);
                        purplespecialblock.transform.localScale = block_scale;
                        purplespecialblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        BlockList.Add(purplespecialblock);
                    }

                }

                // Large Block (0%)
                else if (rnd_block < -5)
                {
                    int rnd_blocktype = Random.Range(1, 100);

                    // Move Large Block (5%)
                    if (rnd_blocktype > 95)
                    {
                        // Instantiate Move Large Block
                        var largeblock = Instantiate(MoveLargeBlock, MoveLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }
                    // Toggle Large Block (50%)
                    else if (rnd_blocktype > 45)
                    {
                        // Instantiate Toggle Large Block
                        var largeblock = Instantiate(ToggleLargeBlock, ToggleLargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Add to list
                        BlockList.Add(largeblock);
                    }

                    // Regular Large Block (90%)
                    else
                    {
                        // Instantiate Regular Large Block
                        var largeblock = Instantiate(LargeBlock, LargeBlock.transform.position, Quaternion.identity, Blocks.transform);
                        largeblock.transform.localScale = block_scale;
                        largeblock.transform.localPosition = new Vector2(next_x, rnd_y);

                        // Get Child Component of largeblock
                        var large_OS_Mushroom = largeblock.gameObject.transform.Find("Large_OS_Mushroom");
                        var large_OS_Trap = largeblock.gameObject.transform.Find("Large_OS_Trap");
                        var large_US_Trap_v2 = largeblock.gameObject.transform.Find("Large_US_Trap_v2");
                        var large_OG = largeblock.gameObject.transform.Find("Large_OG").GetComponent<Image>();

                        // Set Grass image as false
                        large_OG.enabled = false;

                        // Set Random Over Special element over the Large Block
                        var rnd_large_OS = Random.Range(1, 101);

                        // Trap Large Block (10%)
                        if (rnd_large_OS > 90)
                        {
                            Instantiate(Trap, large_OS_Trap.transform.position, Quaternion.identity, large_OS_Trap.transform);
                        }

                        // Jump Large Block (5%)
                        else if (rnd_large_OS > 85)
                        {
                            Instantiate(Mushroom, large_OS_Mushroom.transform.position, Quaternion.identity, large_OS_Mushroom.transform);
                        }

                        // OG Large Block (85%)
                        else if (rnd_large_OS >= 1)
                        {
                            // Set Random Over Grass element over the Large Block
                            var rnd_large_OG = Random.Range(0, Large_OG.Length);
                            large_OG.sprite = Large_OG[rnd_large_OG];
                            large_OG.enabled = true;
                        }

                        // Add list
                        BlockList.Add(largeblock);
                    }
                }

                //Debug.Log(BlockList[i]);

                // Carrot 30%
                if (rnd_block > 70 - Carrot_Spawn_Rate)
                {
                    if (CarrotList.Count <= 0)
                    {
                        var min_carrot_pos = BlockList[0].transform.localPosition;
                        var min_x = -400;
                        var max_x = 400;

                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, min_carrot_pos.y + 80);

                        CarrotList.Add(carrot);
                    }
                    else
                    {
                        // var carrot_last = CarrotList[CarrotList.Count - 1].transform.localPosition;
                        // var carrot_pos = carrot_last.y + 50 + BlockList[i].transform.localPosition.y + 50;
                        // var carrot_pos = Random.Range(BlockList[i].transform.localPosition.y + 120, BlockList[i].transform.localPosition.y + 400);
                        var carrot_pos = Random.Range(BlockList[BlockList.Count - 1].transform.localPosition.y + 120, BlockList[BlockList.Count - 1].transform.localPosition.y + 400);
                        var min_x = -400;
                        var max_x = 400;
                        var rnd_x = Random.Range(min_x, max_x);

                        var carrot = Instantiate(Carrot, Carrot.transform.position, Quaternion.identity, Carrots.transform);
                        carrot.transform.localPosition = new Vector2(rnd_x, carrot_pos);

                        CarrotList.Add(carrot);
                    }
                }
            }
        }
    }  

    public static void Block_Hide()
    {
        foreach (var i in BlockList.ToArray())
        {
            i.GetComponent<Image>().color = new Color(255f, 255f, 255f, 130f);
            i.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public static void Block_Show()
    {
        foreach (var i in BlockList.ToArray())
        {
            i.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            i.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }          
}
