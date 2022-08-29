using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform TF;
    private SpriteRenderer SR;
    public string direction = "left";
    public string owner = "noone";

    [SerializeField] private float speed = 5f;
    [SerializeField] private Sprite SP_UP, SP_LEFT,SP_DOWN,SP_RIGHT;
    [SerializeField] private GameObject explode;


    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        TF = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(direction == "left")
        {
            SR.sprite = SP_LEFT;
            TF.position += new Vector3(-speed *Time.deltaTime, 0, 0);
            
        }
        else if (direction == "right")
        {
            SR.sprite = SP_RIGHT;
            TF.position += new Vector3(speed * Time.deltaTime, 0, 0);
            
        }
        else if (direction == "up")
        {
            SR.sprite = SP_UP;
            TF.position += new Vector3(0, speed * Time.deltaTime, 0);
            
        }
        else if (direction == "down")
        {
            SR.sprite = SP_DOWN;
            TF.position += new Vector3(0, -speed * Time.deltaTime, 0);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BLICK")
        {
        // Debug.Log("hit");
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            Instantiate(explode, TF.transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "BLICK2")
        {
          //Debug.Log("hit");
            Destroy(this.gameObject);
            Instantiate(explode, TF.transform.position, Quaternion.identity);
        }
        if(other.gameObject.tag == "FLAG")
        {
            SoundManager.playsound("TankDie");
            Destroy(this.gameObject);
            Instantiate(explode, TF.transform.position, Quaternion.identity);
            gamevalue.gameover = true;
        }
    }

}
