using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimator : MonoBehaviour
{
    public List<Animator> animators = new List<Animator>();

    public bool callAppearOnStart;

    void Start()
    {
        if(callAppearOnStart)
        {
            CallAppearOnAllAnimators();
        }
    }

    public void CallAppearOnAllAnimators()
    {
      
    }

    public void CallDisappearOnAllAnimators()
    {
       
    }
}
