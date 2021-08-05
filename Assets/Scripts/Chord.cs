using UnityEngine;

namespace lab_metronomo.Assets.Scripts
{
    public class Chord
    {
        public Note[] notes;
        public int range1;
        public int range2;
        public string chordType;
        public bool isFlat;

        public Chord(Note[] notes, int range1, int range2)
        {
            this.notes = notes;
            this.range1 = range1;
            this.range2 = range2;
            foreach (Note note in notes)
            {
                if (note.isFlat)
                {
                    isFlat = true;
                    break;
                }
            }
            this.chordType = GetChordType();
        }
        private string GetChordType()
        {
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

        public string GetNotes()
        {
            return notes[0].noteName + ", " + notes[1].noteName + ", " + notes[2].noteName;
        }
    }
}