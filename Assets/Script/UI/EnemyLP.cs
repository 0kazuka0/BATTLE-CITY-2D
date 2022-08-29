using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLP : MonoBehaviour
{
    private SpriteRenderer SR;

    [SerializeField] private Image[] enemyLP;
    [SerializeField] private Sprite enmLive;
    [SerializeField] private Sprite enmDead;
    // Start is called before the first frame update
    void Start()
    {
        
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
    // Update is called once per frame
    void Update()
    {
        Updateenemy();
    }
}
