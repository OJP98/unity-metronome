using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public double bpm = 120.0F;
    public AudioClip mainTick, altTick;
    private double bpmInSeconds;
    private double nextTick = 0.0F, nextSubdivisionTick = 0.0F;
    private bool isPlaying = false, noteSubdivision = false;
    public AudioSource mainAudioSource, subdivisionAudioSource;
    private int metric = 4, ticksPlayed = 0;

    void Start()
    {
        bpmInSeconds = 60 / bpm;
        nextTick = AudioSettings.dspTime + bpmInSeconds;
        nextSubdivisionTick = nextTick/2;
    }

    void Update()
    {

        while (isPlaying && AudioSettings.dspTime >= nextSubdivisionTick)
        {
            if (AudioSettings.dspTime >= nextTick) 
            {
                if (RegisterTick())
                    mainAudioSource.Play();
                ticksPlayed++;
                nextTick += bpmInSeconds;
            }

            if (noteSubdivision)
                subdivisionAudioSource.Play();

            nextSubdivisionTick += bpmInSeconds/2;
        }
    }

    private bool RegisterTick() {
        mainAudioSource.clip = (ticksPlayed % metric == 0 ? altTick : mainTick);

        if (noteSubdivision && ticksPlayed % metric != 0)
            return false;

        return true;
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

    public bool NoteSubdivision
    {
        get { return noteSubdivision; }
        set { noteSubdivision = value; }
    }
}
