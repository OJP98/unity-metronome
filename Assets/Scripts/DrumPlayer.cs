using System.Collections.Generic;
using UnityEngine;

public class DrumPlayer : MonoBehaviour
{
    private int MAX_NEGRAS = 32;
    private List<int> currentKey = new List<int>(), mainKey = null;
    private List<int> alternativeKey = null;
    private List<int> invertedKey = new List<int>();
    private int ticksPlayed = 0, metric;
    public BeatGeneratorScript beatGenerator;
    public AudioSource drumAudioSource, snareAudioSource, beatAudioSource; 

    public void GenerateRythm(int newMetric)
    {

        metric = newMetric;
        Key = beatGenerator.GenerateBeat(metric);
        SetMaxTicks();
        GenerateAlternativeFilling();
        ticksPlayed = 0;
    }

    public void NextTick() 
    {
        if (ticksPlayed == MAX_NEGRAS)
        {
            ticksPlayed = 0;
            CheckAlternativeFilling();
            Debug.Log("Ahora cambiando de ritmo!!");
        }

        if (EvalDrumHit)
            drumAudioSource.Play();
        else if (EvalSnareHit)
            snareAudioSource.Play();
        
        beatAudioSource.Play();
        ticksPlayed++;
    }

    public string KeysLabelText()
    {
        if (alternativeKey == null)
            return $@"Relleno: {string.Join(",", Key)}";
        
        return $@"Relleno:
          Sección A & C: {string.Join(",", Key)}
          Sección B: {string.Join(",", alternativeKey)}
        ";
    }

    private void GenerateAlternativeFilling()
    {
        int randomResult = Random.Range(0, 2);
        if (randomResult == 1)
            SetAlternativeFilling();
    }

    private void SetMaxTicks()
    {
        if (metric == 4)
            MAX_NEGRAS = 32;
        else
            MAX_NEGRAS = 24;
    }

    private void SetAlternativeFilling()
    {
        alternativeKey = beatGenerator.GenerateBeat(metric);
    }

    private void CheckAlternativeFilling()
    {
        if (alternativeKey != null)
            Key = alternativeKey;
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

    private bool EvalDrumHit => currentKey[ticksPlayed % currentKey.Count] == 1;

    private bool EvalSnareHit => invertedKey[ticksPlayed % currentKey.Count] == 0;

    public List<int> Key
    {
        get { return currentKey; }
        set
        {
            mainKey = currentKey;
            currentKey = value;
            InvertKeyArray(value);
        }
    }
}
