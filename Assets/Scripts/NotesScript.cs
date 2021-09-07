using System;
using System.Collections.Generic;
using UnityEngine;
using lab_metronomo.Assets.Scripts;

public class NotesScript : MonoBehaviour
{
    private string[] noteNames = {"do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la", "la#", "si"};
    [SerializeField]
    public int grado;
    public int initialNote;

    void Start()
    {
        string[] scale = MajorScaleFromIndex(initialNote);
        List<Chord> chords = ListOfChordsFromScale(scale);

        Debug.Log(noteNames[initialNote]);
        Debug.Log(chords[grado - 1].GetNotes() + " - " + chords[grado - 1].GetChordType());
        Debug.Log(GetFunctionFromGrade(grado));
    }

    public string[] MajorScaleFromIndex(int index)
    {
        string[] resNotes = {
            noteNames[index],
            noteNames[(index+2)%12],
            noteNames[(index+4)%12],
            noteNames[(index+5)%12],
            noteNames[(index+7)%12],
            noteNames[(index+9)%12],
            noteNames[(index+11)%12]
        };

        return resNotes;
    }

    public List<Chord> ListOfChordsFromScale(string[] scale)
    {
        List<Chord> chords = new List<Chord>();

        for (int i = 0; i < 7; i++)
        {
            string note1 = scale[i];
            string note2 = scale[(i+2)%7];
            string note3 = scale[(i+4)%7];

            int noteIndex1 = Array.IndexOf(noteNames, note1);
            int noteIndex2 = Array.IndexOf(noteNames, note2);
            int noteIndex3 = Array.IndexOf(noteNames, note3);

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
        return chords;
    }

    Function GetFunctionFromGrade(int grado)
    {
        if (grado == 1 || grado == 3 || grado == 6)
            return Function.Tonica;
        
        if (grado == 2 || grado == 4)
            return Function.Subdominante;
        
        if (grado == 5)
            return Function.Dominante;

        return Function.None;
    }
}