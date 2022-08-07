using UnityEngine;

public class LowCohesionCharacter : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shout;
    float health = 100f;

    public void KnockBack()
    {
        animator.SetTrigger("KnockBack");
    }

    public void Shout()
    {
        audioSource.PlayOneShot(shout);
    }

    public void TakeDamage()
    {
        health -= 10;
    }

}
