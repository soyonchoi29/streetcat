using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;

    public virtual void Enable()
    {
        contextClue.SetActive(true);
    }

    public virtual void Disable()
    {
        contextClue.SetActive(false);
    }

}
