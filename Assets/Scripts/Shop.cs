using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop shop;

    public GameObject turret = null;

    public GameObject shopCanvas;

    private bool shopEnable = false;

    Text goldText;

    GameObject[] buttons = new GameObject[5];

    GameObject cannonButton;
    GameObject rocketButton;
    GameObject flameButton;
    GameObject bananaButton;
    GameObject cSharpButton;

    int[] prices = new int[5];

    public const int cannonPrice = 10;
    public const int rocketPrice = 25;
    public const int flamePrice = 50;
    public const int bananaPrice = 40;
    public const int cSharpPrice = 100;



    // Start is called before the first frame update
    void Start()
    {
        

        shop = this;

        shopCanvas = GameObject.Find("ShopCanvas");


        

        goldText = shopCanvas.transform.Find("Gold").gameObject.GetComponent<Text>();

        cannonButton = gameObject.transform.Find("CannonButton").gameObject;
        rocketButton = gameObject.transform.Find("RocketButton").gameObject;
        flameButton = gameObject.transform.Find("FlameButton").gameObject;
        bananaButton = gameObject.transform.Find("BananaButton").gameObject;
        cSharpButton = gameObject.transform.Find("CSharpButton").gameObject;

        buttons[0] = cannonButton;  prices[0] = cannonPrice;
        buttons[1] = rocketButton; prices[1] = rocketPrice;
        buttons[2] = flameButton; prices[2] = flamePrice;
        buttons[3] = bananaButton; prices[3] = bananaPrice;
        buttons[4] = cSharpButton; prices[4] = cSharpPrice;



    }

    // Update is called once per frame
    void Update()
    {
        if (shopCanvas == null)
            return;

        

        for(int i =0;  i < 5 ; i++)
        {
            if(Player.gold < prices[i])
            {
                buttons[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }

     
    }
    public void ShopEnable()
    {
        
        shopEnable = !shopEnable;
        gameObject.SetActive(shopEnable);
        Debug.Log("Shop Toggled !!" + shopEnable);

    }
    public void Payment(GameObject turret)
    {
        if (turret == TurretManager.tm.CannonTurret)
            Player.gold -= cannonPrice;
        else if (turret == TurretManager.tm.RocketTurret)
            Player.gold -= rocketPrice;
        else if (turret == TurretManager.tm.FlameTurret)
            Player.gold -= flamePrice;
        else if (turret == TurretManager.tm.BananaTurret)
            Player.gold -= bananaPrice;
        else if (turret == TurretManager.tm.CSharpTurret)
            Player.gold -= cSharpPrice;

    }
    public void BuyCannonTurret()
    {
        if (Player.gold >= cannonPrice)
        {
            TurretManager.tm.selectedTurret = TurretManager.tm.CannonTurret;
            TurretManager.tm.BuildPermission = true;
        }
        else
        {
            Debug.Log("Not enough money . . . ");
        }
    }
    
    public void BuyRocketTurret()
    {
        if (Player.gold >= rocketPrice)
        {
            TurretManager.tm.selectedTurret = TurretManager.tm.RocketTurret;
            TurretManager.tm.BuildPermission = true;
        }
        else
        {
            Debug.Log("Not enough money . . . ");
        }
    }
    public void BuyFlameTurret()
    {
        if (Player.gold >= flamePrice)
        {
            TurretManager.tm.selectedTurret = TurretManager.tm.FlameTurret;
            TurretManager.tm.BuildPermission = true;
        }
        else
        {
            Debug.Log("Not enough money . . . ");
        }
    }
    public void BuyBananaTurret()
    {
        if (Player.gold >= bananaPrice)
        {
            TurretManager.tm.selectedTurret = TurretManager.tm.BananaTurret;
            TurretManager.tm.BuildPermission = true;
        }
        else
        {
            Debug.Log("Not enough money . . . ");
        }
    }
    public void BuyCSharpTurret()
    {
        if (Player.gold >= cSharpPrice)
        {
            TurretManager.tm.selectedTurret = TurretManager.tm.CSharpTurret;
            TurretManager.tm.BuildPermission = true;
        }
        else
        {
            Debug.Log("Not enough money . . . ");
        }
    }

}
