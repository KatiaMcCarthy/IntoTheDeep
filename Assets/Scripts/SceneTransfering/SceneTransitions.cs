using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        transitionAnim = this.GetComponent<Animator>();
    }
    
    public void LoadScene(string sceneName)
    {
        Debug.Log("clicked button");
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string name)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.7f);//full duration of the fade
        SceneManager.LoadScene(name);

    }

}
