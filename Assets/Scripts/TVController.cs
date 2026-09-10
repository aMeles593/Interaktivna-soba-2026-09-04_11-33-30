using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool isOn = false;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        TurnOff();
    }

    public void ToggleTV()
    {
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    void TurnOn()
    {
        isOn = true;

        videoPlayer.Play();

        Debug.Log("TV UPALJEN");
    }

    void TurnOff()
    {
        isOn = false;

        videoPlayer.Stop();

        Debug.Log("TV UGAŠEN");
    }
}