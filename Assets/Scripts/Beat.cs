using UnityEngine;

public class Beat : MonoBehaviour
{
    public ChordPlayer chordPlayer;
    public DrumPlayer drumPlayer;
    public MelodyPlayer melodyPlayer;
    public LabelsScript mainLabels;
    public double bpm = 120.0F;
    private double bpmInSeconds;
    private double nextTick = 0.0F;
    private bool isPlaying = false;
    private int[] metricOptions = new int[] {4, 3};
    private int metric, ticks;

    void Awake()
    {
        bpmInSeconds = 60 / bpm;
        nextTick = (AudioSettings.dspTime + bpmInSeconds) / 2;
        NewMelody();
    }

    void Update() {
        PlayBeat();
    }

    public void NewMelody()
    {
        metric = metricOptions[Random.Range(0,2)];
        ticks = 0;

        chordPlayer.GenerateSong(metric);
        drumPlayer.GenerateRythm(metric);
        // melodyPlayer.GenerateMelody(metric, chordPlayer.RythmList);
        mainLabels.SetLabels(drumPlayer, chordPlayer, metric.ToString());
    }

    private void PlayBeat()
    {
        while (isPlaying && AudioSettings.dspTime >= nextTick)
        {
            ticks++;
            if (ticks % 2 == 0)
            {
                chordPlayer.NextTick();
                // melodyPlayer.NextTick();
            }
            drumPlayer.NextTick();
            nextTick += bpmInSeconds/2;
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
        chordPlayer.StopPlaying();
    }

    public double BPM
    {
        get { return bpm; }
        set
        {
            bpm = value;
            bpmInSeconds = 60 / bpm;
        }
    }
}
