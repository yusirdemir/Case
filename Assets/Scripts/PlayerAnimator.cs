using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetTrigger("Run");
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
    }
}   
