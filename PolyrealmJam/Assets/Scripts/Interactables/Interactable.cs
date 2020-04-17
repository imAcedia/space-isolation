using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    None,
    Fix,
    Use,
}

public abstract class Interactable : MonoBehaviour
{
    public abstract InteractionType InteractionType { get; }
    public abstract string InteractionMessage { get; }
    public abstract bool CanInteract { get; }

    public abstract void Interact(PlayerInput player);
}
