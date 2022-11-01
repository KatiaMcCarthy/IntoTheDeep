using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPrepareCharge : MonoBehaviour
{
    private SharkBoss boss;
    [SerializeField] private float chargeTime;
    private bool initateCharge = false;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<SharkBoss>();
        boss.OnPrepareChargeCall += AimAttack;
        boss.OnResetChargeCall += ResetCharge;
    }

    void AimAttack()
    {
        float AngleRad = Mathf.Atan2(boss.player.position.y - transform.position.y, boss.player.position.x - transform.position.x);
        float angle = (180 / Mathf.PI) * AngleRad;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        if (initateCharge == false)
        {
            Invoke("Charge", chargeTime);
            initateCharge = true;
        }
    }

    private void Charge()
    {
        //change the state
        SharkBrain brain = boss.GetComponent<SharkBrain>();
        brain.ResetBrain();
        brain.lungeWeight = 100.0f;

        boss.GetComponent<SharkBrain>().DecideState();
    }

    //on state exit reset charge
    private void ResetCharge()
    {
        initateCharge = false;
    }


    private void OnDestroy()
    {
        boss.OnPrepareChargeCall -= AimAttack;
        boss.OnResetChargeCall -= ResetCharge;
    }
}
