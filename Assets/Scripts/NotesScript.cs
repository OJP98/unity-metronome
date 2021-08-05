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
            Debug.Log(string.Join(", ", chord.notes) + " - " + chord.chordType);
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
            string[] chordNotes = {
                scale[i],
                scale[(i+2)%7],
                scale[(i+4)%7]
            };

            int noteIndex1 = Array.IndexOf(notes, chordNotes[0]);
            int noteIndex2 = Array.IndexOf(notes, chordNotes[1]);
            int noteIndex3 = Array.IndexOf(notes, chordNotes[2]);

            int range1 = (noteIndex2 - noteIndex1) % 6;
            int range2 = (noteIndex3 - noteIndex2) % 6;

            string chordType = GetChordType(range1, range2);

            Chord chord = new Chord(chordNotes, range1, range2, chordType);
            chords.Add(chord);
        }

        return chords;
    }

    private string GetChordType(int range1, int range2)
    {
        Debug.Log(range1 + " " + range2);
        range1 = Math.Abs(range1);
        range2 = Math.Abs(range2);

        if (range1 > 4) range1 %= 4;
        if (range2 > 4) range2 %= 4;
        if (range1 == 4 && range2 == 3)
            return "Major";
        
        if (range1 == 3 && range2 == 4)
            return "Minor";

        if (range1 == 3 && range2 == 3)
            return "Diminished";
        
        if (range1 == 4 && range2 == 4)
            return "Augmented";
        
        return "None";
    }
}