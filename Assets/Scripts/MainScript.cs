using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Metronome metronome;
    public InputField inputField; 

    public void HandleDropDownInputData(int val)
    {
        switch (val)
        {
            case 0:
                metronome.Metric = 2;
                break;

            case 1:
                metronome.Metric = 3;
                break;

            case 2:
                metronome.Metric = 4;
                break;

            case 3:
                metronome.Metric = 5;
                break;

            case 4:
                metronome.Metric = 6;
                break;

            case 5:
                metronome.Metric = 7;
                break;

            default:
                metronome.Metric = 4;
                break;
        }
    }

    public void HandleInputData(int value)
    {
        int inputValue = Convert.ToInt32(inputField.text);
        metronome.BPM = inputValue;
    }
}
