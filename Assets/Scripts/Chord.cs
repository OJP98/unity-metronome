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

        public Function GetFunction(int grade)
        {
            if (grade == 1 || grade == 3 || grade == 6)
                return Function.Tonica;
            
            if (grade == 2 || grade == 4)
                return Function.Subdominante;
            
            if (grade == 5)
                return Function.Dominante;

            return Function.None;
        }

        public void AssignPropertiesToLastChord() {
            this.isStrong = false;
            this.function = Function.Dominante;
        }

        public int Grade {
            get { return grade; }
            set
            {
                grade = value;
                function = GetFunction(grade);
            }
        }

        public void AssignFunction()
        {

        }

        public string GetNotes()
        {
            return notes[0].noteName + ", " + notes[1].noteName + ", " + notes[2].noteName;
        }

    }
}