using UnityEngine;
using System.Collections.Generic;
using lab_metronomo.Assets.Scripts;
using System.Linq;

public class ProgressionGeneratorScript : MonoBehaviour
{
    private Utils utils = new Utils();
    private List<int> tiemposCompletos = new List<int>{1, 2, 4};
    private List<int> tiemposDivididos = new List<int>{2, 4};
    private int CANT_COMPASES = 8;
    private int NEGRAS_POR_COMPAS = 4;
    private int MAX_NEGRAS; 
    private List<int> compaces;

    void Start() {
        MAX_NEGRAS = CANT_COMPASES * NEGRAS_POR_COMPAS;
        compaces = utils.GenerateRandomKey(CANT_COMPASES, tiemposCompletos);
        Debug.Log(utils.PrintIntList(compaces));

        compaces = SubdividirCompas(compaces);
        Debug.Log(utils.PrintIntList(compaces));

    }

    List<int> SubdividirCompas(List<int> compaces) {
        List<int> result = new List<int>();
        foreach (var compas in compaces)
        {
            if (compas > 1)
            {
                result.Add(compas * NEGRAS_POR_COMPAS);
                continue;
            }
            // 50 - 50 separamos o no
            int separar = Random.Range(0, 2);
            if (separar == 0)
            {
                result.Add(compas * NEGRAS_POR_COMPAS);
                continue;
            }

            int randomIndex = Random.Range(0, 2);
            int subdivisiones = tiemposDivididos[randomIndex];
            result.AddRange(Enumerable.Repeat((subdivisiones == 4 ? 1 : 2), subdivisiones));
        }
        return result;
    }
}
