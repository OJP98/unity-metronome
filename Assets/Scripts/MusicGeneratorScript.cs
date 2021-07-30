using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicGeneratorScript : MonoBehaviour
{
    public int subdivision_amount, subdivision_base;
    public List<int[]> metrics = new List<int[]>(){
        new[] {3,4},
        new[] {4,4}
    };
    public int[] metric;

    void Start()
    {
        // Random.seed = 1234;
        metric = RandomMetric;
        int key = GenerateKey(metric);
        GenerateRandomSubdivision(key);
    }

    private int GenerateKey(int[] metric)
    {
        int subdivision_amount = metric[0];
        int subdivision_base = metric[1];

        List<int> subdivion_options = new List<int>{
            subdivision_amount,
            subdivision_amount*2,
            subdivision_amount*4
        };

        return subdivion_options[Random.Range(0, subdivion_options.Count)];
    }

    private void GenerateRandomSubdivision(int key)
    {
        Debug.Log(key);
        List<int> result = new List<int>();
        List<int> values = new List<int>{ 2, 3 };

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum = result.Sum();
            if (sum < key)
            {
                int rand = values[Random.Range(0, values.Count)];
                if (rand + sum <= key)
                    result.Add(rand);
            }
            else break;
        }
        foreach (var item in result)
        {
            Debug.Log(item);
        }
    }

    private int[] RandomMetric => metrics[Random.Range(0, metrics.Count)];


    /* FROM HERE ARE ALL FUNCTIONS THAT I PROBABLY WON'T USE */
    private int GetSubdivisionBase() => Random.Range(3, 4);

    private int SubdivisionAmount => 4;

    private List<int[]> Combinations(int min, int max, int key)
    {

        List<int[]> combinations = new List<int[]>();

        var range = Enumerable.Range(2,2);

        var result_len6 =
            from d1 in range
            from d2 in range
            from d3 in range
            from d4 in range
            from d5 in range
            from d6 in range
            select new { d1, d2, d3, d4, d5, d6 };
        
        var result_len5 =
            from d1 in range
            from d2 in range
            from d3 in range
            from d4 in range
            from d5 in range
            select new { d1, d2, d3, d4, d5 };

        var result_len4 =
            from d1 in range
            from d2 in range
            from d3 in range
            from d4 in range
            select new { d1, d2, d3, d4 };

        foreach(var r in result_len6)
            if (r.d1 + r.d2 + r.d3 + r.d4 + r.d5 + r.d6 == key)
                combinations.Add(new [] { r.d1, r.d2, r.d3, r.d4, r.d5, r.d6 });
            
        foreach(var r in result_len5)
            if (r.d1 + r.d2 + r.d3 + r.d4 + r.d5 == key)
                combinations.Add(new [] { r.d1, r.d2, r.d3, r.d4, r.d5 });

        foreach(var r in result_len4)
            if (r.d1 + r.d2 + r.d3 + r.d4 == key)
                combinations.Add(new [] { r.d1, r.d2, r.d3, r.d4 });

        foreach (var item in combinations)
        {
            Debug.Log("LIST:");
            foreach (var element in item)
                Debug.Log(element);
        }

        return combinations;
    }
}