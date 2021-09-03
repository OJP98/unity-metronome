using UnityEngine;
using System.Collections.Generic;
using lab_metronomo.Assets.Scripts;
using System.Linq;

public class ProgressionGeneratorScript : MonoBehaviour
{
    private Utils utils = new Utils();
    private List<int> tiemposCompletos = new List<int>{1, 2, 4};
    // Para una metrica 3/4, esto debería de ser 2 y 3
    private List<int> tiemposDivididos = new List<int>{2, 4};
    private int CANT_COMPASES = 8;
    private int NEGRAS_POR_COMPAS = 4;
    private int MAX_NEGRAS; 
    private List<int> compaces;
    private List<Chord> acordes;

    void Start() {
        MAX_NEGRAS = CANT_COMPASES * NEGRAS_POR_COMPAS;
        compaces = utils.GenerateRandomKey(CANT_COMPASES, tiemposCompletos);
        Debug.Log(utils.PrintIntList(compaces));

        compaces = SubdividirCompas(compaces);
        Debug.Log(utils.PrintIntList(compaces));

        acordes = GenerateChordList(compaces);
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

    private List<Chord> GenerateChordList(List<int> durationList) {
        List<Chord> result = new List<Chord>();
        int listCount = durationList.Count;
        int prevDuration = 1;
        bool prevIsStrong = false;

        for (int i = 0; i < listCount; i++)
        {
            int duration = durationList[i];
            bool isStrong = true;

            // Is the last chord duration same as this one?
            if (duration % prevDuration == 0 && prevIsStrong)
                isStrong = false;

            // Create a new chord and add it to the list
            Chord chord = new Chord(duration, isStrong);
            result.Add(chord);

            // Save the previous values
            prevDuration = duration;
            prevIsStrong = isStrong;
        }
        result[listCount - 1].AssignPropertiesToLastChord();
        return result;
    }
}
