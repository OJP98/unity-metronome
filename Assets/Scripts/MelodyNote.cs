using UnityEngine;
namespace lab_metronomo.Assets.Scripts
{
    public class MelodyNote
    {
        public Note note;
        public int duration;
        
        public MelodyNote(Note note, int duration)
        {
            this.note = note;
            this.duration = duration;
        }
    }
}