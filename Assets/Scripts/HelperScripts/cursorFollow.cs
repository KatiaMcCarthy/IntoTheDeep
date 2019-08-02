using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorFollow : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
