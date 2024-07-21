using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    private static readonly int Open1 = Animator.StringToHash("Open");
    
    
    public void Open()
    {
        audioSource.Play();
        animator.SetTrigger(Open1);      
    }
}
