using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSPAWN : MonoBehaviour
{
    [SerializeField] private Transform spawnposition;
    [SerializeField] private GameObject playerpref;
     private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null && gamevalue.gettanklifeup>0)
        {
            Instantiate(playerpref, spawnposition.position, Quaternion.identity);
            Player = GameObject.FindWithTag("Player");
        }
    }
}
