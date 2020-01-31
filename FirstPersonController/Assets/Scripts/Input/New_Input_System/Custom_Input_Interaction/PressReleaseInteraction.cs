using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
    using UnityEditor;
    [InitializeOnLoad]
#endif
public class PressReleaseInteraction : IInputInteraction
{
    static PressReleaseInteraction() => InputSystem.RegisterInteraction<PressReleaseInteraction>();

    public void Process(ref InputInteractionContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Waiting:
            {
                if(context.ReadValue<float>() == 1f)
                    context.Started();

                break;
            }

            case InputActionPhase.Started:
            {
                if(context.ReadValue<float>() == 0f)
                    context.Canceled();

                break;
            }
        }
    }

    public void Reset() { }
}
