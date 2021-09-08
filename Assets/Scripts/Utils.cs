using System.Collections.Generic;
using System.Linq;
using System;

namespace lab_metronomo.Assets.Scripts
{
  public class Utils
    {
        public string[] noteNames = {"do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la", "la#", "si"};
        public string[] majorScale;
        public int initialNote;

        public Utils(int initialNote = 0)
        {
            this.initialNote = initialNote;
            this.majorScale = GetMajorScaleFromInitialNote(this.initialNote);
        }

        public List<int> GenerateRandomKey(int maxValue, List<int> randomValues)
        {
            List<int> result = new List<int>();

            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                sum = result.Sum();
                if (sum < maxValue)
                {
                    int rand = randomValues[UnityEngine.Random.Range(0, randomValues.Count)];
                    if (rand + sum <= maxValue)
                        result.Add(rand);
                }
                else break;
            }

            return result;
        }

        public string PrintIntList(List<int> list) {
            return string.Join(",", list);
        }

        public string[] GetMajorScaleFromInitialNote(int index)
        {
            string[] resNotes = {
                noteNames[index],
                noteNames[(index+2)%12],
                noteNames[(index+4)%12],
                noteNames[(index+5)%12],
                noteNames[(index+7)%12],
                noteNames[(index+9)%12],
                noteNames[(index+11)%12]
            };

            return resNotes;
        }

        public Note[] GetChordFromGrade(int grade) 
        {
            grade -= 1;
            string note1 = majorScale[grade];
            string note2 = majorScale[(grade+2)%7];
            string note3 = majorScale[(grade+4)%7];

           int noteIndex1 = Array.IndexOf(noteNames, note1);
           int noteIndex2 = Array.IndexOf(noteNames, note2);
           int noteIndex3 = Array.IndexOf(noteNames, note3);

           if (noteIndex1 > noteIndex2) noteIndex2 += 12;
           if (noteIndex2 > noteIndex3) noteIndex3 += 13;

           return new Note[] {
               new Note(note1, noteIndex1),
               new Note(note2, noteIndex2),
               new Note(note3, noteIndex3)
           };
        }
    }

}