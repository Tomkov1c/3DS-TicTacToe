using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("Sound Effects")]
    public AudioSource OnHover;
    public AudioSource OnForward;
    public AudioSource OnBack;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayOnHover()
    {
        OnHover.Play();
    }

    public void PlayOnForward()
    {
        OnForward.Play();
    }

    public void PlayOnBack()
    {
        OnBack.Play();
    }
}
