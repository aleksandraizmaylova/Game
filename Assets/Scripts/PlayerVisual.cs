using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;

    private const string IS_RUNNING_W = "IsRunningW";
    private const string IS_RUNNING_A = "IsRunningA";
    private const string IS_RUNNING_S = "IsRunningS";
    private const string IS_RUNNING_D = "IsRunningD";

    private void Awake() => animator = GetComponent<Animator>();

    private void Update()
    {
        animator.SetBool(IS_RUNNING_W, Player.Instance.IsRunningW());
        animator.SetBool(IS_RUNNING_A, Player.Instance.IsRunningA());
        animator.SetBool(IS_RUNNING_S, Player.Instance.IsRunningS());
        animator.SetBool(IS_RUNNING_D, Player.Instance.IsRunningD());
    }
}
