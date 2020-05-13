using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class TurretManager : MonoBehaviour
{
    public static TurretManager tm;
    public GameObject selectedTurret;
    private Path path;

    public GameObject CannonTurret;
    public GameObject RocketTurret;
    public GameObject FlameTurret;
    public GameObject BananaTurret;
    public GameObject CSharpTurret;

    public bool BuildPermission = false;

    // Start is called before the first frame update
    void Start()
    {
        tm = this;
    
        path = GameObject.Find("Path").GetComponent<Path>();

        CannonTurret = Resources.Load<GameObject>("Prefabs/CannonTurret");//(GameObject)Resources.Load("Assets /Prefabs/CannonTurret.prefab", typeof(GameObject));
        RocketTurret = Resources.Load<GameObject>("Prefabs/RocketTurret");
        FlameTurret = Resources.Load<GameObject>("Prefabs/FlameTurret");
        BananaTurret = Resources.Load<GameObject>("Prefabs/BananaTurret");
        CSharpTurret = Resources.Load<GameObject>("Prefabs/CSharpTurret");
        //selectedTurret = RocketTurret;
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null || selectedTurret == null || BuildPermission == false)
            return;
       
        if(Input.GetMouseButton(0) && !path.isMouseOver)
        {
            float mouseRatioX = 10 * Input.mousePosition.x / Screen.width;
            float mouseRatioY = 10 * Input.mousePosition.y / Screen.height;

            Shop.shop.Payment(selectedTurret);

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 9.0f;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(selectedTurret, objectPos, Quaternion.identity);
            BuildPermission = false;
           
            

           
        }
       

    }

    public void BuildTurret()            
    {
        
        
    }


}
