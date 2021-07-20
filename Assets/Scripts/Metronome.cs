using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public double bpm = 120.0F;
    public double bpmInSeconds;
    public AudioClip mainTick, altTick;
    private double nextTick = 0.0F;
    private bool isPlaying = false;
    private AudioSource audioSource;
    private int metric = 4, ticksPlayed = 0;

    void Start()
    {
        bpmInSeconds = 60 / bpm;
        nextTick = AudioSettings.dspTime + bpmInSeconds;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        while (AudioSettings.dspTime >= nextTick && isPlaying) {

            RegisterTick();
            audioSource.Play();
            nextTick += bpmInSeconds;
        }
    }

    private void RegisterTick() {
        audioSource.clip = (ticksPlayed % metric == 0 ? altTick : mainTick);
        ticksPlayed++;
    }

    public void StartMetronome() {
        isPlaying = true;
        ticksPlayed = 0;
    }
    
    public void StopMetronome() {
        isPlaying = false;
    }

    public int Metric {
        get { return metric; }
        set
        {
            metric = value;
            ticksPlayed = 0;
        }
    }

    public double BPM {
        get { return bpm; }
        set
        {
            if (value < 0) bpm = 0;
            else if (value > 999) bpm = 999;
            else bpm = value;

            bpmInSeconds = 60 / bpm;
        }
    }
}
