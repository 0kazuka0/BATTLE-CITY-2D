using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletfx : MonoBehaviour
{
    void destructself()
    {
        Destroy(this.gameObject);
    }
}
