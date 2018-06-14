using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] selectionVoice;
    private AudioClip indexedClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void AudioSelected()
    {
        int index = Random.Range(0, selectionVoice.Length);
        indexedClip = selectionVoice[index];
        audioSource.PlayOneShot(indexedClip);
    }
}
