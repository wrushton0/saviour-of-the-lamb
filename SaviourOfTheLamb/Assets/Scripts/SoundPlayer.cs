using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource source;
    public List<AudioClip> sounds;
    public enum EventToPlaySound { START, ON_ENABLE, MANUAL };
    public EventToPlaySound soundEvent;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        if (soundEvent == EventToPlaySound.START)
            PlaySound();
    }

    void OnEnable()
    {
        if (soundEvent == EventToPlaySound.ON_ENABLE)
            PlaySound();
    }

    public void PlaySound()
    {
        int soundIndex = Random.Range(0, sounds.Count);
        source.clip = sounds[soundIndex];
        source.Play();
    }
}
