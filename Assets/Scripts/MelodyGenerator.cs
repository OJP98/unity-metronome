using System.Collections.Generic;
using System.Linq;
using lab_metronomo.Assets.Scripts;
using UnityEngine;

public class MelodyGenerator : MonoBehaviour
{
    private Utils utils = new Utils();
    private List<int> NOTE_DURATIONS = new List<int>{1, 2, 4};
    private Note noteBase;
    private int metric;

    public List<MelodyNote> GetMelody(List<Rythm> listaCompaces)
    {
        List<MelodyNote> melodyNotes = new List<MelodyNote>();

        foreach (Rythm ritmo in listaCompaces)
            melodyNotes = melodyNotes.Concat(SelectRandomNotes(ritmo)).ToList();

        return melodyNotes;
    }

    private List<MelodyNote> SelectRandomNotes(Rythm rythm)
    {
        List<MelodyNote> melodyNotes = new List<MelodyNote>();
        List<int> durations = RandomDurations(rythm.duration);
        foreach (int dur in durations)
        {
            Note randomNote = SelectRandomNoteFromChord(rythm.chord);
            melodyNotes.Add(new MelodyNote(randomNote, dur));
        }
        return melodyNotes;
    }

    private Note SelectRandomNoteFromChord(Chord chord) 
    {
        Note[] notes = chord.notes;
        int randomIndex = Random.Range(0, notes.Length);
        return notes[randomIndex];
    }

    private List<int> RandomDurations(int rythmDuration)
    {
        return utils.GenerateRandomKey(rythmDuration, NOTE_DURATIONS);
    }

}
