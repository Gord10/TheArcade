using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    private static readonly int Open1 = Animator.StringToHash("Open");
    //public Collider Collider;
    
    public void Open()
    {
        animator.SetTrigger(Open1);      
    }
}
