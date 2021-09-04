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
        public List<int> gradoTonicas = new List<int>{1, 3, 6};
        public List<int> gradoSudominantes = new List<int>{2, 4};
        public int gradoDominante = 5;
        public Note[] notes;
        public int range1;
        public int range2;
        public ChordType chordType;
        public bool isFlat;
        public int grade;
        public Function function;
        public int duration;
        public bool isStrong;

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

        public Chord(int duration, bool isStrong) 
        {
            this.duration = duration;
            this.isStrong = isStrong;
            this.function = ReturnFunctionType();
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
            this.isStrong = false;
            this.function = Function.Dominante;
            this.grade = gradoDominante;
        }

        private Function ReturnFunctionType()
        {
            Function[] strongFunctions = {Function.Tonica, Function.Subdominante};
            Function[] weakFunctions = {Function.Dominante, Function.Subdominante};

            int rand = Random.Range(0, 2);
            if (this.isStrong)
                return strongFunctions[rand];

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
            return gradoTonicas[rand];
        }
        private int GetRandomSubdominantGrade()
        {
            // Random between 0 - 1
            int rand = Random.Range(0, 2);
            return gradoSudominantes[rand];
        }
        private int GetRandomDominantGrade()
        {
            // Will return 5 cause thats the only grade for dominant 
            return gradoDominante;
        }

        public string GetNotes()
        {
            return notes[0].noteName + ", " + notes[1].noteName + ", " + notes[2].noteName;
        }

        public string GetData()
        {
            return this.duration + " " + this.isStrong + " " + this.function + " " + this.grade;
        }

    }
}