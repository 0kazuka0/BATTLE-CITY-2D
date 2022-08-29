using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSPAWN : MonoBehaviour
{
    private bool timeenable = false;

    [SerializeField] private GameObject[] itempref;

    [SerializeField] private float timeforspawn;
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if (timeenable == false)
        {
            timeforspawn -= Time.deltaTime;
        }
        int randomitem = Random.Range(0, itempref.Length);
        Vector3 randomposition = new Vector3(Random.Range(-23.64f, -4.37f), Random.Range(-3.53f, 5.65f), 0);

        if (GameController.enemylife > 0 && timeforspawn <= 0)
        {
            timeforspawn = 7f;
            Instantiate(itempref[randomitem], randomposition, Quaternion.identity);

        }
        else if (GameController.enemylife <= 0)
        {
            timeenable = true;
        }
    }
}
