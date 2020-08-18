using UnityEngine;
using UnityEngine.UI;

public class Portals : MonoBehaviour
{
    public Sprite[] WorldImages = new Sprite[] { };
    public Sprite[] PortalImages = new Sprite[] { };
    public static int CurrentWorld;
    public static int modWorld;

    public Image CurrentColor, LeftColor, RightColor;
    public GameObject Left_Portal, Right_Portal;

    public GameObject Worlds_Bar;
    
    void Start()
    {
        CurrentWorld = 1;
        Worlds_Bar.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Movement.isPortalsActivated)
        {
            Worlds_Bar.gameObject.SetActive(true);

            if (CurrentWorld == 0)
            {                
                Left_Portal.GetComponent<Image>().sprite = PortalImages[2];
                LeftColor.sprite = WorldImages[2];

                CurrentColor.sprite = WorldImages[0];

                Right_Portal.GetComponent<Image>().sprite = PortalImages[1];
                RightColor.sprite = WorldImages[1];
            }
            else if(CurrentWorld == 1)
            {
                Left_Portal.GetComponent<Image>().sprite = PortalImages[0];
                LeftColor.sprite = WorldImages[0];

                CurrentColor.sprite = WorldImages[1];

                Right_Portal.GetComponent<Image>().sprite = PortalImages[2];
                RightColor.sprite = WorldImages[2];
            }
            else if (CurrentWorld == 2)
            {
                Left_Portal.GetComponent<Image>().sprite = PortalImages[1];
                LeftColor.sprite = WorldImages[1];

                CurrentColor.sprite = WorldImages[2];

                Right_Portal.GetComponent<Image>().sprite = PortalImages[0];
                RightColor.sprite = WorldImages[0];
            }
        }
    }
}
