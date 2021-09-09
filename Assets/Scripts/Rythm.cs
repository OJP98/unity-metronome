namespace lab_metronomo.Assets.Scripts
{
  public class Rythm
    {
        public int duration;
        public bool isStrong;
        public Chord chord = null;
        
        public Rythm(int duration, bool isStrong, Chord chord)
        {
            this.duration = duration;
            this.isStrong = isStrong;
            this.chord = chord;
        }
        public void AssignPropertiesToLastRythm() {
            this.isStrong = false;
            this.chord.AssignPropertiesToLastChord();
        }

        public string LogRythm()
        {
            string data = $@"
                Duration: {this.duration}
                isStrong: {this.isStrong}
                ChordType: {this.chord.GetData()}
                ChordNotes: {this.chord.GetNotes()}
            ";
            return data;
        }
    }
}