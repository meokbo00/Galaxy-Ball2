using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAudioPlay : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnItemIconDestroyed()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
