using System;
using UnityEngine;
using UnityEngine.UI;
using lab_metronomo.Assets.Scripts;

public class MainMelodyScript : MonoBehaviour
{
    public Text metricLbl, fillingLbl, chordDurationLbl, scaleNoteLbl;
    public Beat beat;
    private Utils utils;
    private DrumPlayer drumPlayer;
    private PianoPlayer pianoPlayer;
    private ProgressionGeneratorScript pg;
    public InputField inputField; 
    private string scaleNote, chordDurationString, drumFilling, metric;

    public void SetLabels(DrumPlayer drumPlayer, PianoPlayer pianoPlayer, string metric)
    {
        metricLbl.text = "Metrica: " + metric + "/4";
        fillingLbl.text = "Relleno: " + string.Join(",", drumPlayer.Key);
        chordDurationLbl.text = "Duración acordes: " + pianoPlayer.ChordsDuration;
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