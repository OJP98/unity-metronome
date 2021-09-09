using System.Collections.Generic;
using System.Linq;
using System;

namespace lab_metronomo.Assets.Scripts
{
  public class Utils
    {
        private int TOTAL_NOTES = 12;
        public string[] NOTE_NAMES = {"do", "do#", "re", "re#", "mi", "fa", "fa#", "sol", "sol#", "la", "la#", "si"};
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
                NOTE_NAMES[index],
                NOTE_NAMES[(index+2) % TOTAL_NOTES],
                NOTE_NAMES[(index+4) % TOTAL_NOTES],
                NOTE_NAMES[(index+5) % TOTAL_NOTES],
                NOTE_NAMES[(index+7) % TOTAL_NOTES],
                NOTE_NAMES[(index+9) % TOTAL_NOTES],
                NOTE_NAMES[(index+11) % TOTAL_NOTES]
            };

            return resNotes;
        }

        public Note[] GetChordFromGrade(int grade) 
        {
            grade -= 1;
            string noteName1 = majorScale[grade];
            string noteName2 = majorScale[(grade+2)%7];
            string noteName3 = majorScale[(grade+4)%7];

           int noteIndex1 = Array.IndexOf(NOTE_NAMES, noteName1);
           int noteIndex2 = Array.IndexOf(NOTE_NAMES, noteName2);
           int noteIndex3 = Array.IndexOf(NOTE_NAMES, noteName3);

           if (noteIndex1 > noteIndex2) noteIndex2 += TOTAL_NOTES;
           if (noteIndex2 > noteIndex3) noteIndex3 += TOTAL_NOTES;

           return new Note[] {
               new Note(noteName1, noteIndex1),
               new Note(noteName2, noteIndex2),
               new Note(noteName3, noteIndex3)
           };
        }
    }

}