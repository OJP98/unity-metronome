using System.Collections;
using System.Collections.Generic;
using lab_metronomo.Assets.Scripts;
using UnityEngine;

public class PianoPlayer : MonoBehaviour
{
    public AudioSource note1, note2, note3;
    public ProgressionGeneratorScript progressionGenerator;
    private int currentIndex = 0, ticksPlayed = 0;
    private List<Rythm> rythmList;
    private Rythm currentRythm;

    public void GenerateRythm(int metric)
    {
        rythmList = progressionGenerator.GetRythmList(metric);
        currentRythm = rythmList[0];
        ticksPlayed = 0;
    }

    public void NextTick() 
    {
        if (ticksPlayed == currentRythm.duration) {
            ChangeCurrentRythm();
            ticksPlayed = 0;
        }

        if (ticksPlayed == 0)
            PlayPiano();
        
        ticksPlayed++;
    }

    public string ChordsDuration => progressionGenerator.ChordsDurationString;
    public string BaseNote => progressionGenerator.BaseNote;

    private void ChangeCurrentRythm()
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
}
