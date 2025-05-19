using UnityEngine;

public class WolfVisual : MonoBehaviour
{
    private Animator animator;
    private void Awake() => animator = GetComponent<Animator>();

    private void Update()
    {
        animator.SetTrigger("WolfSaid");
    }
}
