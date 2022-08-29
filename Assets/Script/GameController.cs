using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    //set spawn player,spawn enemy , control scene 
    public static int enemylife = 12;

    private SpriteRenderer SR;
    private GameObject Enem;
    private GameObject bullet;
    private Animator anim;
    private bool badend;
    AudioSource Audy;

    [SerializeField] private int sceneindex;
    [SerializeField] private int sceneindex2;

    [SerializeField] private float GetShoveltime;
    [SerializeField] private GameObject BASEWALL;
    [SerializeField] private GameObject INWALL;

    [SerializeField] private Image[] enemyLP;
    [SerializeField] private Sprite enmLive;
    [SerializeField] private Sprite enmDead;

    // Start is called before the first frame update
    void Start()
    {
        Audy = GetComponent<AudioSource>();
        Enem = GameObject.FindWithTag("Enemy");
        bullet = GameObject.FindWithTag("Bullet");
        anim = GetComponent<Animator>();

        badend = false;

        BASEWALL.SetActive(true);
        INWALL.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Updateenemy();

        if (gamevalue.getshovelIwall==true&&GetShoveltime>0)//get shovel 
        {
            GetShoveltime -= Time.deltaTime;
            BASEWALL.SetActive(false);
            INWALL.SetActive(true);
        }
        else if (gamevalue.getshovelIwall == true && GetShoveltime <= 0)//get shovel timeout
        {
            gamevalue.getshovelIwall = false;
            BASEWALL.SetActive(true);
            INWALL.SetActive(false);
            GetShoveltime = 10;
        }

        
        if (gamevalue.gettanklifeup <=0 || gamevalue.gameover==true)//game over
        {
            Audy.Stop();
            badend = true;
            anim.SetBool("gameover", badend);
            Invoke("scnechangetomenu", 2f);
        }
        else if(enemylife<=0 && gamevalue.gameover == false)//go to result scene
        {
            Enem = GameObject.FindWithTag("Enemy");
            bullet = GameObject.FindWithTag("Bullet");
            Destroy(Enem);
            Destroy(bullet);
            Invoke("scenetoresult", 2f);
        }
 
    }
    public void Updateenemy()
    {
        for (int i = 0; i < enemyLP.Length; i++)
        {
            if (i < GameController.enemylife)
            {
                enemyLP[i].sprite = enmLive;
            }
            else
            {
                enemyLP[i].sprite = enmDead;
            }
        }
    }

    void scnechangetomenu()
    {
        SceneManager.LoadScene(sceneindex2);
    }
    void scenetoresult()
    {
        SceneManager.LoadScene(sceneindex);
    }
    
}
