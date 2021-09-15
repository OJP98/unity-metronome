using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public double bpm = 120.0F;
    public AudioClip mainTick, altTick;
    private double bpmInSeconds;
    private double nextTick = 0.0F, nextSubdivisionTick = 0.0F;
    private bool isPlaying = true, noteSubdivision = false;
    public AudioSource mainAudioSource, secondaryAudioSource, subdivisionAudioSource;
    private int metric = 4, ticksPlayed = 0;
    private List<int> key = new List<int>(), iKey = new List<int>();

    void Start()
    {
        bpmInSeconds = 60 / bpm;
        nextTick = AudioSettings.dspTime + bpmInSeconds;
        nextSubdivisionTick = nextTick/2;
    }

    void Update()
    {

        if (key.Count > 0)
            PlayMusic();
        else 
        {
            PlayMetronome();
        }

    }

    private void PlayMetronome()
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

    private void PlayMusic()
    {

        while (isPlaying && AudioSettings.dspTime >= nextTick)
        {
            if (EvalMainTick())
                mainAudioSource.Play();
            else if (EvalSecondaryTick())
                secondaryAudioSource.Play();

            subdivisionAudioSource.Play();
            ticksPlayed++;
            double div = metric >= 4 ? metric/4 : 1.25;
            nextTick += bpmInSeconds/div;
        }

    }

    private bool EvalMainTick()
    {
        return key[ticksPlayed%metric] == 1 ? true : false ;
    }

    private bool EvalSecondaryTick()
    {
        return iKey[ticksPlayed%metric] == 1 ? false : true ;
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

    public void InvertArray(List<int> list) 
    {
        iKey = new List<int>(list);
        int amount = Random.Range(0, list.Count);
        for (int i = 0; i < amount; i++)
        {
            int pos = Random.Range(0, list.Count);
            iKey[pos] = (list[pos] == 1) ? 0 : 1;
        }
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
            else if (value > 300) bpm = 300;
            else bpm = value;

            bpmInSeconds = 60 / bpm;
        }
    }

    public bool NoteSubdivision
    {
        get { return noteSubdivision; }
        set { noteSubdivision = value; }
    }

    public List<int> Key
    {
        get { return key; }
        set
        { 
            key = value;
            InvertArray(value);
        }
    }
}
