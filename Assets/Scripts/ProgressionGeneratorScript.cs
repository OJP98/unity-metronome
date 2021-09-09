using UnityEngine;
using System.Collections.Generic;
using lab_metronomo.Assets.Scripts;
using System.Linq;

public class ProgressionGeneratorScript : MonoBehaviour
{
    private Utils utils;
    private List<int> tiemposCompletos = new List<int>{1, 2, 4};
    // Para una metrica 3/4, esto debería de ser 2 y 3
    private List<int> tiemposDivididos = new List<int>{2, 4};
    private int CANT_COMPASES = 8;
    private int NEGRAS_POR_COMPAS = 4;
    private int MAX_NEGRAS; 
    private List<int> compaces;
    private List<Rythm> ritmos;
    private int initialNote;
    private string[] majorScale;

    void Start() {
        // initialNote = Random.Range(0, 13);
        initialNote = 0;
        utils = new Utils(initialNote);

        MAX_NEGRAS = CANT_COMPASES * NEGRAS_POR_COMPAS;
        compaces = utils.GenerateRandomKey(CANT_COMPASES, tiemposCompletos);
        Debug.Log(utils.PrintIntList(compaces));

        compaces = SubdividirCompas(compaces);
        Debug.Log(utils.PrintIntList(compaces));

        ritmos = GenerateRythmList(compaces);
        foreach (Rythm ritmo in ritmos) {
            Chord acorde = ritmo.chord;
            Note[] notes = utils.GetChordFromGrade(acorde.grade);
            acorde.notes = notes;
            Debug.Log(ritmo.LogRythm());
            // Debug.Log(acorde.GetData());
            // Debug.Log(acorde.GetNotes());
        }
    }

    private List<int> SubdividirCompas(List<int> compaces) {
        List<int> result = new List<int>();

        foreach (var size in compaces)
        {
            // We don't care about complete times
            if (size > 1)
            {
                result.Add(size * NEGRAS_POR_COMPAS);
                continue;
            }
            // 50 - 50 should we split?
            int split = Random.Range(0, 2);
            if (split == 0)
            {
                result.Add(size * NEGRAS_POR_COMPAS);
                continue;
            }

            // Decide wether we devide in 2 o 4
            int randomIndex = Random.Range(0, 2);
            int subdivisions = tiemposDivididos[randomIndex];
            // Add the number n times
            result.AddRange(
                Enumerable.Repeat((subdivisions == 4 ? 1 : 2), subdivisions)
            );
        }
        return result;
    }

    private List<Rythm> GenerateRythmList(List<int> durationList) {
        List<Rythm> result = new List<Rythm>();
        int listCount = durationList.Count;
        Rythm prevRythm = null;

        for (int i = 0; i < listCount; i++)
        {
            int duration = durationList[i];
            bool isStrong = true;

            // Is the last chord duration same as this one?
            if (prevRythm != null && duration % prevRythm.duration == 0 && prevRythm.isStrong)
                isStrong = false;

            // Create a new chord and add it to the list
            // remember this constructor assigns duration and function
            Chord chord = new Chord(isStrong);
            Rythm rythm = new Rythm(duration, isStrong, chord);

            if (i == listCount - 1)
                rythm.AssignPropertiesToLastRythm();
            

            // Check if we have two dominants in a row
            CheckPreviousFunctionTone(prevRythm != null ? prevRythm.chord : null, chord);
            prevRythm = rythm;
            result.Add(rythm);
        }
        return result;
    }

    private void CheckPreviousFunctionTone(Chord previousChord, Chord currentChord)
    {
        if (previousChord == null)
            return;


        // If last chord is weak and is dominant
        if (previousChord.function == Function.Dominante
            && currentChord.function == Function.Dominante)
        {
            previousChord.function = Function.Subdominante;
            previousChord.grade = previousChord.ReturnRandomGrade();
        }
    }
}
