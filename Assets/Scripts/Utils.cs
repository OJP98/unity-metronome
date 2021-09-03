using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace lab_metronomo.Assets.Scripts
{
    public class Utils
    {
        public List<int> GenerateRandomKey(int maxValue, List<int> randomValues)
        {
            List<int> result = new List<int>();

            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                sum = result.Sum();
                if (sum < maxValue)
                {
                    int rand = randomValues[Random.Range(0, randomValues.Count)];
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
    }

}