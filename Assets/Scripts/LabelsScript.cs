using System;
using UnityEngine;
using UnityEngine.UI;

public class LabelsScript : MonoBehaviour
{
    public Text metricLbl, fillingLbl, chordDurationLbl, scaleNoteLbl;
    public Beat beat;
    public InputField inputField; 

    public void SetLabels(DrumPlayer drumPlayer, ChordPlayer pianoPlayer, string metric)
    {
        metricLbl.text = "Metrica: " + metric + "/4";
        fillingLbl.text = drumPlayer.KeysLabelText();
        chordDurationLbl.text = $@"Duración de acordes:
  Sección A: {pianoPlayer.ChordsDuration[0]}
  Sección B: {pianoPlayer.ChordsDuration[1]}
  Sección C: {pianoPlayer.ChordsDuration[2]}
  Orden: A, B, A, B, C
        ";
        scaleNoteLbl.text = "Nota base: " + pianoPlayer.BaseNote;
    }

    public void HandleInputData(int value)
    {
        int inputValue = Convert.ToInt32(inputField.text);

        if (inputValue <= 60) {
            inputValue = 60;
            inputField.text = "60";
        }
        else if (inputValue >= 300) {
            inputValue = 300;
            inputField.text = "300";
        }
        beat.BPM = inputValue;
    }
}