using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public int Type;
    [SerializeField] private float timesucide;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timesucide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
