using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemytank1 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D RB;
    private Transform TF;
    private string direction;
    private int directionanim;
    private float changedirectiontime;
    private Vector3 currentpos;
    private float shootingrate;
    private bool enable;
    private bool die;
   
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject bulletobj;
    [SerializeField] private GameObject explode;

    EnemyLP enmlp;
    // Start is called before the first frame update
    void Start()
    {
        enable = false;
        die = false;
        RB=GetComponent<Rigidbody2D>(); 
        TF=GetComponent<Transform>();
        int temp = Random.Range(1, 5);
        if (temp == 1) direction = "up";
        if (temp == 2) direction = "down";
        if (temp == 3) direction = "left";
        if (temp == 4) direction = "right";
        
        changedirectiontime = Random.Range(3f, 5f);
        shootingrate = Random.Range(1f, 5f);

    }

    void FixedUpdate()
    {
        animate();
        if (enable==true && die==false)
        {
            if(gamevalue.gettimerfreeze==false)
            {
                checkmove();
                shooting();
            }
            else if(gamevalue.gettimerfreeze == true)
            {
                RB.velocity = new Vector2(0,0);
            }
        }

        if(enable == true && die==true)
        {
            RB.velocity = new Vector2(0, 0);
        }

        if(enable == true && gamevalue.getgranadenuke==true)
        {
            RB.velocity = new Vector2(0, 0);
            die=true;
            anim.SetBool("dead", die);
        }
    }
    private void checkmove()
    {
        changedirectiontime -= Time.deltaTime;
        if (changedirectiontime < 0)
        {
            changedirectiontime = Random.Range(3f, 6f);
            int temp = Random.Range(1, 5);
            if (temp == 1) direction = "up";
            if (temp == 2) direction = "down";
            if (temp == 3) direction = "left";
            if (temp == 4) direction = "right";
        }

        if (direction == "up")//up
        {
            RB.velocity = new Vector2(0, speed);
            TF.localScale = new Vector3(0.5f, 0.5f, 1);
            directionanim = 1;

        }
        else if (direction == "down")//down
        {
            RB.velocity = new Vector2(0, -speed);
            TF.localScale = new Vector3(0.5f, -0.5f, 1);
            directionanim = 1;
        }
        else if (direction == "left")//left
        {
            RB.velocity = new Vector2(-speed, 0);
            TF.localScale = new Vector3(0.5f, 0.5f, 1);
            directionanim = 2;
        }
        else if (direction == "right")//right
        {
            RB.velocity = new Vector2(speed, 0);
            TF.localScale = new Vector3(-0.5f, 0.5f, 1);
            directionanim = 2;
        }


        currentpos = TF.position;
        if (currentpos.x < -23.64f && direction == "left") direction = "right";//left to right
        if (currentpos.x > -4.37f && direction == "right") direction = "left";//right to left
        if (currentpos.y < -5.66f && direction == "down") direction = "up";//down to up
        if (currentpos.y > 5.65f && direction == "up") direction = "down";//up to down
    }
    private void shooting()
    {
        shootingrate -= Time.deltaTime;
        if (shootingrate < 0)
        {
            shootingrate = 5f;
            GameObject bullet;
            bullet = Instantiate(bulletobj, TF.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().owner = "enemy";
            bullet.GetComponent<Bullet>().direction = direction;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.owner == "player"&& speed!=2.5f)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                Instantiate(explode, bullet.transform.position, Quaternion.identity);
                die = true;
                Destroy(bullet.gameObject);
                anim.SetBool("dead", die);
                gamevalue.scoreT++;
                gamevalue.score += 100;
            }
            else if (bullet.owner == "player" && speed == 2.5f)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                Debug.Log("yes");
                Instantiate(explode, bullet.transform.position, Quaternion.identity);
                die = true;
                Destroy(bullet.gameObject);
                anim.SetBool("dead", die);
                gamevalue.scoreFT++;
                gamevalue.score += 200;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            int temp = Random.Range(1, 5);
            if (temp == 1) direction = "up";
            if (temp == 2) direction = "down";
            if (temp == 3) direction = "left";
            if (temp == 4) direction = "right";
        }     
    }
   
    void waitforspawn()
    {
        enable = true;
    }
    void animate()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("move", directionanim);
        anim.SetBool("enable", enable);
    }
    void isdie()
    {
        GameController.enemylife -= 1;
        gamevalue.getgranadenuke = false;
        this.gameObject.SetActive(false);
    }
    void PlayFXDIE()
    {
        SoundManager.playsound("TankDie");
    }

}
