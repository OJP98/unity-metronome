using System.Collections.Generic;
using UnityEngine;

namespace lab_metronomo.Assets.Scripts
{
    public enum Function {
        Tonica, Subdominante, Dominante, None
    }

    public enum ChordType {
        Major, Minor, Diminished, Augmented, None
    }

    public class Chord
    {
        private List<int> GRADO_TONICAS = new List<int>{1, 3, 6};
        private List<int> GRADO_SUBDOMINANTES = new List<int>{2, 4};
        private int GRADO_DOMINANTE = 5;
        public Note[] notes;
        public int range1;
        public int range2;
        public ChordType chordType;
        public bool isFlat;
        public int grade;
        public Function function;

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

        public Chord(bool isStrong) 
        {
            if (isStrong)
                this.function = ReturnStrongFunction();
            else
                this.function = ReturnWeakFunction();
            this.grade = ReturnRandomGrade();
        }

        public ChordType GetChordType()
        {
            if (range1 == 4 && range2 == 3)
                return ChordType.Major;
            
            if (range1 == 3 && range2 == 4)
                return ChordType.Minor;

            if (range1 == 3 && range2 == 3)
                return ChordType.Diminished;
            
            if (range1 == 4 && range2 == 4)
                return ChordType.Augmented;
            
            return ChordType.None;
        }

        public void AssignPropertiesToLastChord() {
            this.function = Function.Dominante;
            this.grade = GRADO_DOMINANTE;
        }

        private Function ReturnStrongFunction()
        {
            Function[] strongFunctions = {Function.Tonica, Function.Subdominante};
            int rand = Random.Range(0, 2);
            return strongFunctions[rand];

        }

        private Function ReturnWeakFunction()
        {
            Function[] weakFunctions = {Function.Dominante, Function.Subdominante};
            int rand = Random.Range(0, 2);
            return weakFunctions[rand];
        }

        public int ReturnRandomGrade()
        {
            if (this.function == Function.Tonica)
                return GetRandomTonicGrade();
            if (this.function == Function.Subdominante)
                return GetRandomSubdominantGrade();
            return GetRandomDominantGrade();
        }

        private int GetRandomTonicGrade()
        {
            // Random between 0 - 2
            int rand = Random.Range(0, 3);
            return GRADO_TONICAS[rand];
        }
        private int GetRandomSubdominantGrade()
        {
            // Random between 0 - 1
            int rand = Random.Range(0, 2);
            return GRADO_SUBDOMINANTES[rand];
        }
        private int GetRandomDominantGrade()
        {
            // Will return 5 cause thats the only grade for dominant 
            return GRADO_DOMINANTE;
        }

        public string GetNotes()
        {
            return notes[0].noteName 
                + ", " + notes[1].noteName 
                + ", " + notes[2].noteName;
        }

        public string GetData()
        {
            return function + " " + this.grade;
        }

    }
}