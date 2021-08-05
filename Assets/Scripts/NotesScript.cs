using System;
using System.Collections.Generic;
using UnityEngine;
using lab_metronomo.Assets.Scripts;

public class NotesScript : MonoBehaviour
{
    private string[] notes = {"do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la", "la#", "si"};
    public int initialNote = 2;

    // Start is called before the first frame update
    void Start()
    {
        string[] scale = MajorScale(initialNote);
        Debug.Log("LA ESCALA ES: " + string.Join(", ", scale));
        Debug.Log("LOS ACORDES SON:");
        List<Chord> chords = Chords(scale);
        foreach (Chord chord in chords)
            Debug.Log(chord.GetNotes() + " " + chord.chordType);
    }

    string[] MajorScale(int index)
    {
        string[] resNotes = {
            notes[index],
            notes[(index+2)%12],
            notes[(index+4)%12],
            notes[(index+5)%12],
            notes[(index+7)%12],
            notes[(index+9)%12],
            notes[(index+11)%12]
        };

        return resNotes;
    }

    List<Chord> Chords(string[] scale)
    {
        List<Chord> chords = new List<Chord>();

        for (int i = 0; i < 7; i++)
        {
            string note1 = scale[i];
            string note2 = scale[(i+2)%7];
            string note3 = scale[(i+4)%7];

            int noteIndex1 = Array.IndexOf(notes, note1);
            int noteIndex2 = Array.IndexOf(notes, note2);
            int noteIndex3 = Array.IndexOf(notes, note3);

            if (noteIndex1 > noteIndex2) noteIndex2 += 12;
            if (noteIndex2 > noteIndex3) noteIndex3 += 12;

            Note[] chordNotes = {
                new Note(note1, noteIndex1),
                new Note(note2, noteIndex2),
                new Note(note3, noteIndex3)
            };

            int range1 = Math.Abs(noteIndex2 - noteIndex1);
            int range2 = Math.Abs(noteIndex3 - noteIndex2);

            Chord chord = new Chord(chordNotes, range1, range2);
            chords.Add(chord);
        }

        Array.Reverse(notes);
        return chords;
    }
}