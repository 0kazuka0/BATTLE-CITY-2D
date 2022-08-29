using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySPAWN : MonoBehaviour
{
    private bool timeenable=false;

    [SerializeField] private Transform[] spawnposition;
    [SerializeField] private GameObject[] enemypref;

    [SerializeField] private float timeforspawn;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if(timeenable==false)
        {
            timeforspawn -= Time.deltaTime;
        }
        int randomenemy = Random.Range(0, enemypref.Length);
        int randoposition = Random.Range(0, spawnposition.Length);

        if (GameController.enemylife>0 && timeforspawn<=0)
        {
            timeforspawn = 7f;
            Instantiate(enemypref[randomenemy], spawnposition[randoposition].position, Quaternion.identity);
            
        }
        else if(GameController.enemylife <= 0)
        {
            timeenable = true;
        }
    }
}
