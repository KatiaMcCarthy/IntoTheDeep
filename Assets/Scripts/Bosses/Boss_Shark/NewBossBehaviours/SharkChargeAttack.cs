using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkChargeAttack : MonoBehaviour
{
    private SharkBoss boss;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<SharkBoss>();
        animator = GetComponent<Animator>();
        boss.OnChargeAttackCall += Charge;
    }

    void Charge()
    {
        animator.SetTrigger("Lunge");
    }

    //called in animation
    private void EndLunge()
    {
        //change the state
        SharkBrain brain = boss.GetComponent<SharkBrain>();
        brain.ResetBrain();
        brain.swipeWeight = 100.0f;

        boss.GetComponent<SharkBrain>().DecideState();
    }

    private void OnDestroy()
    {
        boss.OnChargeAttackCall -= Charge;
    }
}
