using System.Collections.Generic;
using lab_metronomo.Assets.Scripts;
using UnityEngine;

public class MelodyPlayer : MonoBehaviour
{
    public MelodyGenerator melodyGenerator;
    public AudioSource melodyAudioSource;
    private int currentIndex = 0, ticksPlayed = 0;
    private List<MelodyNote> melodyNotes;
    private MelodyNote currentNote;

    public void GenerateMelody(int metric, List<Rythm> listaCompaces)
    {
        melodyNotes = melodyGenerator.GetMelody(listaCompaces);
        currentNote = melodyNotes[0];
        ticksPlayed = 0;
    }

    public void NextTick()
    {
        if (ticksPlayed == currentNote.duration)
        {
            ChangeCurrentNote();
            ticksPlayed = 0;
        }

        if (ticksPlayed == 0)
            PlayMelodyNote();
        
        ticksPlayed++;
    }

    public void ChangeCurrentNote()
    {
        currentIndex = (currentIndex + 1) % melodyNotes.Count;
        currentNote = melodyNotes[currentIndex];
    }

    public void PlayMelodyNote()
    {
        melodyAudioSource.pitch = currentNote.note.freq;
        melodyAudioSource.Play();
    }
}
