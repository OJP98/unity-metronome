using UnityEngine;

namespace lab_metronomo.Assets.Scripts
{
    public class Chord : MonoBehaviour
    {
        public string[] notes;
        public int range1;
        public int range2;
        public string chordType;

        public Chord(string[] notes, int range1, int range2, string chordType)
        {
            this.notes = notes;
            this.range1 = range1;
            this.range2 = range2;
            this.chordType = chordType;
        }
    }
}