using System.Collections.Generic;
using UnityEngine;

public class DrumPlayer : MonoBehaviour
{
    private List<int> key = new List<int>();
    private List<int> invertedKey = new List<int>();
    private int ticksPlayed = 0, metric;
    public BeatGeneratorScript beatGenerator;
    public AudioSource drumAudioSource, snareAudioSource, beatAudioSource; 

    public void GenerateRythm(int newMetric)
    {
        metric = newMetric;
        Key = beatGenerator.GenerateBeat(metric);
        ticksPlayed = 0;
    }

    public void NextTick() 
    {
        if (EvalDrumHit)
            drumAudioSource.Play();
        else if (EvalSnareHit)
            snareAudioSource.Play();
        
        beatAudioSource.Play();
        ticksPlayed++;
    }

    private void InvertKeyArray(List<int> list)
    {
        invertedKey = new List<int>(list);
        int amount = Random.Range(0, list.Count);
        for (int i = 0; i < amount; i++)
        {
            int pos = Random.Range(0, list.Count);
            invertedKey[pos] = (list[pos] == 1) ? 0 : 1;
        }
    }

    private bool EvalDrumHit => key[ticksPlayed % key.Count] == 1;

    private bool EvalSnareHit => invertedKey[ticksPlayed % key.Count] == 0;

    public List<int> Key
    {
        get { return key; }
        set
        {
            key = value;
            InvertKeyArray(value);
        }
    }
}
