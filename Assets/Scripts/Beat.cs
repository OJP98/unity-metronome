using UnityEngine;

public class Beat : MonoBehaviour
{
    public ChordPlayer pianoPlayer;
    public DrumPlayer drumPlayer;
    public LabelsScript mainLabels;
    public double bpm = 120.0F;
    private double bpmInSeconds;
    private double nextTick = 0.0F;
    private bool isPlaying = true;
    private int[] metricOptions = new int[] {4, 3};
    private int metric;

    void Awake()
    {
        NewMelody();
        bpmInSeconds = 60 / bpm;
        nextTick = AudioSettings.dspTime + bpmInSeconds;
    }

    void Update() {
        PlayBeat();
    }

    public void NewMelody()
    {
        metric = metricOptions[Random.Range(0,2)];

        pianoPlayer.GenerateRythm(metric);
        drumPlayer.GenerateRythm(metric);
        mainLabels.SetLabels(drumPlayer, pianoPlayer, metric.ToString());
    }

    private void PlayBeat()
    {
        while (isPlaying && AudioSettings.dspTime >= nextTick)
        {
            pianoPlayer.NextTick();
            drumPlayer.NextTick();
            nextTick += bpmInSeconds;
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
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
