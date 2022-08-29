using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLAG : MonoBehaviour
{
    private SpriteRenderer SR;
    [SerializeField] private Sprite FLAGDEAD;
    // Start is called before the first frame update

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(gamevalue.gameover==true)
        {
            SR.sprite = FLAGDEAD;
            gamevalue.gameover = true;
        }
    }
}
