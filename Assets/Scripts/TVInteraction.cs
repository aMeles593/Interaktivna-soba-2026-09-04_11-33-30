using UnityEngine;

public class TVInteraction : MonoBehaviour, IInteractable
{
    public TVController tvController;

    public void Interact()
    {
        Debug.Log("TVInteraction radi!");

        if (tvController == null)
        {
            Debug.LogError("TVController NIJE POVEZAN!");
            return;
        }

        Debug.Log("Pozivam ToggleTV...");
        tvController.ToggleTV();
    }
}