using System.Collections.Generic;
using System.Linq;
using lab_metronomo.Assets.Scripts;
using UnityEngine;

public class ChordPlayer : MonoBehaviour
{
    public AudioSource note1, note2, note3;
    public RythmGeneratorScript rythmGenerator;
    private int currentIndex = 0, ticksPlayed = 0;
    private List<Rythm> rythmList;
    private Rythm currentRythm;
    private List<string> chordsDuration;

    public void GenerateSong(int metric)
    {
        ticksPlayed = 0;
        currentIndex = 0;
        chordsDuration = new List<string>();

        Debug.Log("Sección A:");
        List<Rythm> rythm1 = rythmGenerator.GetRythmList(metric);
        chordsDuration.Add(rythmGenerator.ChordsDurationString);

        Debug.Log("Sección B:");
        List<Rythm> rythm2 = rythmGenerator.GetRythmList(
            metric,
            rythmGenerator.BaseNoteIndex,
            rythm1.Average(r => r.duration)
        );
        chordsDuration.Add(rythmGenerator.ChordsDurationString);

        Debug.Log("Sección C:");
        List<Rythm> rythm3 = rythmGenerator.GetRythmList(
            metric,
            rythmGenerator.BaseNoteIndex
        );
        chordsDuration.Add(rythmGenerator.ChordsDurationString);

        rythmList = rythm1.Concat(rythm2).ToList();
        rythmList = rythmList.Concat(rythm1).ToList();
        rythmList = rythmList.Concat(rythm2).ToList();
        rythmList = rythmList.Concat(rythm3).ToList();

        currentRythm = rythmList[0];
    }

    public void NextTick() 
    {
        if (ticksPlayed == currentRythm.duration) {
            ChangeCurrentChord();
            ticksPlayed = 0;
        }

        if (ticksPlayed == 0)
            PlayPiano();
        
        ticksPlayed++;
    }

    public List<string> ChordsDuration => chordsDuration;
    public string BaseNote => rythmGenerator.BaseNoteName;

    private void ChangeCurrentChord()
    {
        currentIndex = (currentIndex + 1) % rythmList.Count;
        currentRythm = rythmList[currentIndex];
    }

    private void PlayPiano() {
        SetPitchFromNotes(currentRythm.chord.notes);
        PlayNotes();
    }

    private void SetPitchFromNotes(Note[] notes) {
        note1.pitch = notes[0].freq;
        note2.pitch = notes[1].freq;
        note3.pitch = notes[2].freq;
    }

    private void PlayNotes() {
        note1.Play();
        note2.Play();
        note3.Play();
    }

    public void StopPlaying() {
        note1.Stop();
        note2.Stop();
        note3.Stop();
    }

    public List<Rythm> RythmList
    {
        get { return rythmList; }
    }
}
