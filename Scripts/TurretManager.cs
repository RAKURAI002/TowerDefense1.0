using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEditor;

public class TurretManager : MonoBehaviour
{
    public GameObject selectedTurret;
    private Path path;

   /* public GameObject CannonTurret;
    public GameObject RocketTurret;
    public GameObject FlameTurret;
    public GameObject BananaTurret;
    public GameObject CSharpTurret;
    */



    private float buildCD = 1f;

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.Find("Path").GetComponent<Path>();
       /* CannonTurret = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/CannonTurret.prefab", typeof(GameObject));
        RocketTurret = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/RocketTurret.prefab", typeof(GameObject));
        FlameTurret = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/FlameTurret.prefab", typeof(GameObject));
        BananaTurret = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BananaTurret.prefab", typeof(GameObject));
        CSharpTurret = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/CSharpTurret.prefab", typeof(GameObject));*/
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;
        float mouseRatioX = 10*Input.mousePosition.x / Screen.width;
        float mouseRatioY = 10*Input.mousePosition.y / Screen.height;

        //Debug.Log(buildCD);
        // Vector3 mousePos = new Vector3(mouseRatioX, mouseRatioY, 0f);
        if (Input.GetMouseButton(0) && !path.isMouseOver && buildCD <=0)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 9.0f;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(selectedTurret, objectPos, Quaternion.identity);

            //StartCoroutine(SpawnTurret());
            buildCD = 1f;
        }
        buildCD -= Time.deltaTime;
    }

    IEnumerator SpawnTurret()
    {
       
        yield return new WaitForSeconds(2f);
        
    }
}
