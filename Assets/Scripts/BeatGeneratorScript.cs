using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeatGeneratorScript : MonoBehaviour
{
    private List<int> randomKeyValues = new List<int>{2, 3};
    int subdivision_amount, subdivision_base;
    public List<int[]> metrics = new List<int[]>(){
        new[] {3,4},
        new[] {4,4}
    };
    private int[] metric;

    public List<int> GenerateBeat(int newMetric)
    {
        if (newMetric == 4)
            metric = new[] {4, 4};
        else
            metric = new[] {3, 4};

        int rythm = GenerateRythm(metric);
        List<int> key = GenerateRandomKey(rythm, randomKeyValues);
        List<int> filling = GenerateFilling(key, rythm);
        return filling;
    }

    public int GenerateRythm(int[] metric)
    {
        subdivision_amount = metric[0];
        subdivision_base = metric[1];

        List<int> subdivion_options = new List<int>{
            subdivision_amount,
            subdivision_amount*2,
            subdivision_amount*4
        };

        return subdivion_options[Random.Range(0, subdivion_options.Count)];
    }


    public List<int> GenerateRandomKey(int key, List<int> randomValues)
    {
        List<int> result = new List<int>();

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum = result.Sum();
            if (sum < key)
            {
                int rand = randomValues[Random.Range(0, randomValues.Count)];
                if (rand + sum <= key)
                    result.Add(rand);
            }
            else break;
        }

        int[] arrayOfItems = result.ToArray();

        return result;
    }

    public List<int> GenerateFilling(List<int> key, int sub_key)
    {
        List<int> result = new List<int>();
        
        foreach (var k in key)
        {
            if (k == 3) result.AddRange(new List<int>{0, 1, 1});
            else if (k == 2) result.AddRange(new List<int>{0, 1});
        }

        while (result.Count < sub_key)
            result.Add(Random.Range(0, 1));

        return result;
    }

    private int[] RandomMetric => metrics[Random.Range(0, metrics.Count)];
}