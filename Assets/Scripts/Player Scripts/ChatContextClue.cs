using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatContextClue : ContextClue
{
    private Animator animator;

    private void Start()
    {
        animator = contextClue.GetComponent<Animator>();
    }

    public override void Enable()
    {
        contextClue.SetActive(true);
        animator.enabled = true;
    }

    public override void Disable()
    {
        contextClue.SetActive(false);
        animator.enabled = false;
    }
}
