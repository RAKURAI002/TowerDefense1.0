using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static GameObject[] walkPaths;

    public GameObject walkpath;
    public Transform SpawnPoint;

    public Enemy enemy;

    public GameObject ShopCanvas;
    public GameObject ResultCanvas;
    public Text ResultText;
    public Text countdownText;
    public float countdown = 6;

    public Text HPText;
    public Text goldText;

    private float waveCooldown = 7f;
    private int waveAmount = 1;
    private int waveLimit = 8;

    private bool pauseGame = false;


    // Start is called before the first frame update
    void Awake()
    {
        walkPaths = new GameObject[walkpath.transform.childCount];
        for(int i = 0; i < walkPaths.Length; i++)
        {
            walkPaths[i] = walkpath.transform.GetChild(i).gameObject;

        }
        if(walkPaths == null)
        {

            Debug.Log("Can't find Path . . ");
        }
    }

    private void Start()
    {
        gm = this;
        countdown = 6;
        //Instantiate(enemy, SpawnPoint.transform);
    }
    // Update is called once per frame
    void Update()
    {
        goldText.text = " Gold:" + Player.gold.ToString() + "G";
        HPText.text = "Lives : " + Player.lives;
        if(waveCooldown <= 0 && waveLimit > 0)
        {
            waveCooldown = 6f;
           // Debug.Log("New Wave" + waveAmount);
            StartCoroutine("SpawnEnemy");
            waveLimit--;
            
            
            waveAmount+=2;
        }
       // Debug.Log(waveCooldown);
       if(countdown>=0)
        {
            countdownText.text = Mathf.Floor(countdown).ToString();
            countdown -= Time.deltaTime;
        }
       if(countdown <= 0)
        {
            countdownText.enabled = false;
        }
       
     
       waveCooldown -= Time.deltaTime;
        if (waveLimit == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            ShopCanvas.SetActive(false);
            ResultCanvas.SetActive(true);
            ResultText.fontSize = 60;
            ResultText.text = "Congrats, You trash your precious time to win this game.";
        }
        if(Player.lives <=0)
        {
            ShopCanvas.SetActive(false);
            ResultCanvas.SetActive(true);
            ResultText.text = "You get nothing, YOU LOSE !! ";
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseTheGame()
    {
        pauseGame = !pauseGame;
        if (pauseGame)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < waveAmount; i++)
        {
            Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
            //Debug.Log("Enemy : "  + i);
            yield return new WaitForSeconds(0.5f);
            
        }
        


    }
    public void PlusGold()
    {
        Player.gold ++;
    }

    public void EnemyReachEnd()
    {
        Player.lives--;
    }
}
