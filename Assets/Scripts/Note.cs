using UnityEngine;

namespace lab_metronomo.Assets.Scripts
{
    public class Note
    {
        public string noteName;
        public bool isFlat;
        public int index;
        
        public Note(string noteName, int index)
        {
            this.noteName = noteName;
            this.index = index;
            this.isFlat = noteName.Contains("#");
        }
    }
}