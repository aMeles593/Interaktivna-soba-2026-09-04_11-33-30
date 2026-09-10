using UnityEngine;

public class TableLampInteraction : MonoBehaviour, IInteractable
{
    public Light lampLight;

    public Color offColor = Color.white;
    public Color onColor = Color.yellow;

    private Renderer lampRenderer;
    private Material lightMaterial;

    private bool isOn = false;

    void Start()
    {
        lampRenderer = GetComponent<Renderer>();

        if (lampRenderer != null)
        {
            foreach (Material material in lampRenderer.materials)
            {
                if (material.name.Contains("svjetlo"))
                {
                    lightMaterial = material;
                    break;
                }
            }
        }

        if (lightMaterial != null)
        {
            lightMaterial.color = offColor;
        }

        if (lampLight != null)
        {
            lampLight.enabled = false;
        }
    }

    public void Interact()
    {
        isOn = !isOn;

        // Uključi/isključi stvarno svjetlo
        if (lampLight != null)
        {
            lampLight.enabled = isOn;
        }

        // Promijeni boju materijala "svijetlo"
        if (lightMaterial != null)
        {
            lightMaterial.color = isOn ? onColor : offColor;
        }

        Debug.Log(isOn ? "Stolna lampa upaljena" : "Stolna lampa ugašena");
    }
}