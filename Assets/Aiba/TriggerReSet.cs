using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReSet : StateMachineBehaviour
{
    [SerializeField] string triggerName;
    PlayerKatanaAttack playerKatanaAttack;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);

        playerKatanaAttack = GameObject.FindObjectOfType<PlayerKatanaAttack>();
        playerKatanaAttack.Count++;
    }
}
