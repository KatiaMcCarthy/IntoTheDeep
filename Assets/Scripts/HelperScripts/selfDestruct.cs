using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public int lifetime;

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
}
