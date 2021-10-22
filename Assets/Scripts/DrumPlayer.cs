using System.Collections.Generic;
using UnityEngine;

public class DrumPlayer : MonoBehaviour
{
    private int MAX_NEGRAS = 32;
    private List<int> currentKey = new List<int>(), tempKey = null;
    private List<int> alternativeKey = null;
    private List<int> invertedKey = new List<int>(), invertedAlternativeKey = null;
    private int ticksPlayed = 0, loops = 0, metric;
    public BeatGeneratorScript beatGenerator;
    public AudioSource drumAudioSource, snareAudioSource, beatAudioSource; 

    public void GenerateRythm(int newMetric)
    {
        ticksPlayed = 0;
        loops = 0;
        metric = newMetric;
        SetMaxTicks();
        GenerateAlternativeFilling();
        currentKey = beatGenerator.GenerateBeat(metric);
        invertedKey = InvertArray(currentKey);
    }

    public void NextTick() 
    {
        if (ticksPlayed == MAX_NEGRAS)
        {
            ticksPlayed = 0;
            CheckAlternativeFilling();
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

        MAX_NEGRAS *= 2;
    }

    private void SetAlternativeFilling()
    {
        alternativeKey = beatGenerator.GenerateBeat(metric);
        invertedAlternativeKey = InvertArray(alternativeKey);
    }

    private void CheckAlternativeFilling()
    {
        loops += 1;
        if (alternativeKey != null && loops != 5)
        {
            List<int> tempKey = currentKey;
            currentKey = alternativeKey;
            alternativeKey = tempKey;

            tempKey = invertedKey;
            invertedKey = invertedAlternativeKey;
            invertedAlternativeKey = tempKey;

        } else if (loops == 5)
            loops = 0;
    }

    private List<int> InvertArray(List<int> list)
    {
        List<int> tempKey = new List<int>(list);
        int amount = Random.Range(0, list.Count);
        for (int i = 0; i < amount; i++)
        {
            int pos = Random.Range(0, list.Count);
            tempKey[pos] = (list[pos] == 1) ? 0 : 1;
        }

        return tempKey;
    }

    private bool EvalDrumHit => currentKey[ticksPlayed % currentKey.Count] == 1;

    private bool EvalSnareHit => invertedKey[ticksPlayed % currentKey.Count] == 0;

    public List<int> Key
    {
        get { return currentKey; }
        set { currentKey = value; }
    }
}
