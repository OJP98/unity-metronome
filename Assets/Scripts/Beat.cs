using UnityEngine;
using lab_metronomo.Assets.Scripts;
using System.Collections.Generic;

public class Beat : MonoBehaviour
{
    public PianoPlayer pianoPlayer;
    public DrumPlayer drumPlayer;
    public double bpm = 120.0F;
    private double bpmInSeconds;
    private double nextTick = 0.0F;
    private bool isPlaying = true;
    private int metric = 4, ticksPlayed = 0;

    void Start()
    {
        bpmInSeconds = 60 / bpm;
        nextTick = AudioSettings.dspTime + bpmInSeconds;
        isPlaying = true;
    }

    void Update() {
        PlayBeat();
    }

    private void PlayBeat()
    {
        while (isPlaying && AudioSettings.dspTime >= nextTick)
        {
            pianoPlayer.NextTick();
            drumPlayer.NextTick();
            ticksPlayed += 1;
            nextTick += bpmInSeconds;
        }
    }
}
