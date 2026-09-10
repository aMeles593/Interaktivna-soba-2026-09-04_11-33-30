using UnityEngine;

public class LampInteraction : MonoBehaviour, IInteractable
{
    public Light lampLight;

    private bool isOn = false;

    void Start()
    {
        if (lampLight != null)
        {
            lampLight.enabled = false;
        }
    }

    public void Interact()
    {
        isOn = !isOn;

        if (lampLight != null)
        {
            lampLight.enabled = isOn;
        }

        Debug.Log(isOn ? "Svjetiljka upaljena" : "Svjetiljka ugašena");
    }
}