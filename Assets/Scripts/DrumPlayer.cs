using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPlayer : MonoBehaviour
{
    public AudioSource drumAudioSource, snareAudioSource, beatAudioSource; 
    private List<int> key = new List<int>();
    private List<int> invertedKey = new List<int>();
    private int ticksPlayed = 0, metric = 4;
    private int[] metricList = new[] {4, 4};
    public MusicGeneratorScript musicGenerator;

    void Start()
    {
        int rythm = musicGenerator.GenerateRythm(metricList);
        List<int> key = musicGenerator.GenerateRandomKey(rythm, new List<int>{2, 3});
        Key = musicGenerator.GenerateFilling(key, rythm);
    }

    // Update is called once per frame
    void Update()
    {
        // PlayDrums();
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

    private bool EvalDrumHit => key[ticksPlayed % metric] == 1;

    private bool EvalSnareHit => invertedKey[ticksPlayed % metric] == 1;

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
