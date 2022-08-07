using UnityEngine;

public class HighCohesionCharacter : MonoBehaviour, IAttackable
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shout;
    float health = 100f;

    public void TakeHit()
    {
        animator.SetTrigger("KnockBack");
        audioSource.PlayOneShot(shout);
        health -= 10;
    }

}
