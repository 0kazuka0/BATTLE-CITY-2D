using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject T;
    private Animator anim;
    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private Transform TF;
    private string direction = "down"; 
    private int directionanim = 5; // 5stay,8up,4left,6right,2down
    private bool canmove;
    AudioSource Audy;

    [SerializeField] private float Speed;

    private bool GethelmetTank = false;
    [SerializeField] private float helmettimeleft;
    [SerializeField] private float timertimefreeze;

    public float dmg;

    [SerializeField] private GameObject aura;
    [SerializeField] private GameObject bulletobj;
    [SerializeField] private GameObject explode;
    [SerializeField] private GameObject Tankexplode;
    [SerializeField] private float firerate = 0.1f;
    [SerializeField] private float nextfire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        aura.SetActive(false);
        RB = GetComponent<Rigidbody2D>();
        SR= GetComponent<SpriteRenderer>();
        TF = GetComponent<Transform>();
        canmove = false;
        Audy = GetComponent<AudioSource>();
        T = GameObject.FindWithTag("Enemy");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(gamevalue.score);
            //Debug.Log(gamevalue.gameover);
        }
        animate();

        if(gamevalue.gettimerfreeze == true && timertimefreeze>0)
        {
            timertimefreeze -= Time.deltaTime;
        }
        else if(gamevalue.gettimerfreeze == true && timertimefreeze <= 0)
        {
            gamevalue.gettimerfreeze = false;
            timertimefreeze = 10;
        }

        if(GethelmetTank==true && helmettimeleft > 0)
        {
            aura.SetActive(true);
            helmettimeleft -= Time.deltaTime;            
        }
        else if(GethelmetTank == true && helmettimeleft <= 0)
        {
            GethelmetTank = false;
            aura.SetActive(false);
            helmettimeleft = 15;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canmove == true && gamevalue.gameover == false && GameController.enemylife > 0) 
        {
            if (Input.GetKey("up"))
            {
                Audy.Play();
                RB.velocity = new Vector2(0, Speed);
                TF.localScale = new Vector3(0.5f, 0.5f, 1);           
                direction = "up";
                directionanim = 8;
            }
            else if (Input.GetKey("down"))
            {
                Audy.Play();
                RB.velocity = new Vector2(0, -Speed);
                TF.localScale = new Vector3(0.5f, -0.5f, 1);
                direction = "down";
                directionanim = 8;
            }
            else if (Input.GetKey("left"))
            {
                Audy.Play();
                RB.velocity = new Vector2(-Speed, 0);
                TF.localScale = new Vector3(0.5f, 0.5f, 1);
                direction = "left";
                directionanim = 4;
            }
            else if (Input.GetKey("right"))
            {
                Audy.Play();
                RB.velocity = new Vector2(Speed, 0);
                TF.localScale = new Vector3(-0.5f, 0.5f, 1);
                direction = "right";
                directionanim = 4;
            }
            else
            {
                Audy.Stop();
                RB.velocity = new Vector2(0, 0);
                directionanim = 5;
            }

            nextfire += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && nextfire >= firerate)
            {
                SoundManager.playsound("GunShot");
                nextfire = 0;
                GameObject bullet;
                bullet = Instantiate(bulletobj, TF.transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().direction = direction;
                bullet.GetComponent<Bullet>().owner = "player";
            }

        }
        else if(canmove == true && gamevalue.gameover == false && GameController.enemylife <= 0)
        {
            RB.velocity = new Vector2(0, 0);
            directionanim = 5;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            switch (other.GetComponent<powerup>().Type)
            {
                // 0 - Grenade
                // 1 - Helmet
                // 2 - Tank
                // 3 - Timer
                // 4 - Shovel

                case 0:
                    SoundManager.playsound("Powup3");
                    GetGrenade();
                    break;
                case 1:
                    SoundManager.playsound("Powup2");
                    GetHelmet();
                    break;
                case 2:
                    SoundManager.playsound("Powup1");
                    GetTank();
                    break;
                case 3:
                    SoundManager.playsound("Powup3");
                    GetTimer();
                    break;
                case 4:
                    SoundManager.playsound("Powup1");
                    GetShovel();
                    break;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.owner == "enemy")
            {
                if(GethelmetTank==false)
                {
                    SoundManager.playsound("TankDie");
                    gamevalue.gettanklifeup--;
                    Destroy(this.gameObject);
                    Instantiate(explode, bullet.transform.position, Quaternion.identity);
                    Instantiate(Tankexplode, this.gameObject.transform.position, Quaternion.identity);
                    Destroy(bullet.gameObject);
                    Destroy(gameObject);
                }
                else 
                {
                    Instantiate(explode, bullet.transform.position, Quaternion.identity);
                    Destroy(bullet.gameObject);
                }
            }
        }
    }
    void GetGrenade()//destroy all enemy
    {
        if(T!=null)
        {
            gamevalue.getgranadenuke = true;
        }
        else if(T==null)
        {
            gamevalue.getgranadenuke = false;
        }
    }  
    void GetHelmet()//protect player from bullet of enemy
    {
        GethelmetTank = true;
    }
    void GetTank()//get extralife
    {
        gamevalue.gettanklifeup++;
    }  
    void GetTimer()//stop time
    {
        if(T!=null)
        {
            gamevalue.gettimerfreeze = true;
        }
        else if(T==null)
        {
            gamevalue.gettimerfreeze = false;
        }
    }
    void GetShovel()
    {
        gamevalue.getshovelIwall = true;
    }

    void animate()
    {
         anim=GetComponent<Animator>();
        anim.SetInteger("move", directionanim);
        anim.SetBool("canmove", canmove);
    }
    void cantmove()
    {
        canmove = true;
    }
}
