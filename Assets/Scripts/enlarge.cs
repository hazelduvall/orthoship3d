using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enlarge : StateMachineBehaviour {

    public float end;
    public float speed;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    animator.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0);
    //}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(animator.gameObject.GetComponent<RectTransform>().localScale.x<end)
        {
            float change = (end - animator.gameObject.GetComponent<RectTransform>().localScale.x) / speed;
            animator.gameObject.GetComponent<RectTransform>().localScale += new Vector3(change, change, 0);
        }
        
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    animator.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.35f, 0.35f, 0);
    //}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
