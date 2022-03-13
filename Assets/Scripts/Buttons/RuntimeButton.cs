using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RuntimeButton : MonoBehaviour
{
    //An action is just a kind of delegate.
    Action clickBehaviour;

    //This is called from another script to define what method gets called when button is clicked.
    public void SetClickBehaviour(Action someMethodFromSomewhereElse)
    {
        clickBehaviour = someMethodFromSomewhereElse;
    }

    // This is linked in button inspector to the OnClick event.
    public void ClickBehaviour()
    {
        clickBehaviour.Invoke();
    }

}

