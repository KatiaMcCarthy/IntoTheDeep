using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//class that manages ui elements
public class UIManager : MonoBehaviour
{
    
    public Slider bossHPBar;

    // Start is called before the first frame update
    void Start()
    {
        bossHPBar = FindObjectOfType<Slider>();
        bossHPBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
