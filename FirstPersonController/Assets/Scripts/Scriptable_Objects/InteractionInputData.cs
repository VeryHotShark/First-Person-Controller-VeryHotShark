using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "FirstPersonController/Data/InteractionInputData", order = 0)]
public class InteractionInputData : ScriptableObject
{
    private bool holdInteract;
    private bool interact;

    public bool Interact
    {
        get => interact;
        set => interact = value;
    }

    public bool HoldInteract
    {
        get => holdInteract;
        set => holdInteract = value;
    }
}