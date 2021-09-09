using UnityEngine;
using lab_metronomo.Assets.Scripts;
using System.Collections.Generic;

public class Beat : MonoBehaviour
{
    public double bpm = 120.0F;
    public AudioClip audioClip;
    private double bpmInSeconds;
    private double nextTick = 0.0F;
    private bool isPlaying = true;
    public AudioSource note1, note2, note3;
    public ProgressionGeneratorScript progressionGenerator;
    private int metric = 4, ticksPlayed = 0, currentIndex = 0;
    private List<Rythm> rythmList;
    private Rythm currentRythm;

    void Start()
    {
        bpmInSeconds = 60 / bpm;
        nextTick = AudioSettings.dspTime + bpmInSeconds;
        rythmList = progressionGenerator.GetRythmList();
        currentRythm = rythmList[0];
    }

    void Update() {
        PlayBeat();
    }

    private void PlayBeat()
    {
        while (isPlaying && AudioSettings.dspTime >= nextTick)
        {
            if (ticksPlayed == currentRythm.duration) {
                ticksPlayed = 0;
                currentIndex += 1;
                currentIndex %= rythmList.Count;
                currentRythm = rythmList[currentIndex];
            }

            if (ticksPlayed == 0)
                PlaySound();

            ticksPlayed++;
            nextTick += bpmInSeconds;
        }
    }

    private void PlaySound() {
        SetPitchFromNotes(currentRythm.chord.notes);
        PlayChord();
    }

    private void SetPitchFromNotes(Note[] notes) {
        note1.pitch = notes[0].freq;
        note2.pitch = notes[1].freq;
        note3.pitch = notes[2].freq;
    }

    private void PlayChord() {
        note1.PlayOneShot(audioClip);
        note2.PlayOneShot(audioClip);
        note3.PlayOneShot(audioClip);
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
            else if (value > 300) bpm = 300;
            else bpm = value;

            bpmInSeconds = 60 / bpm;
        }
    }
}
