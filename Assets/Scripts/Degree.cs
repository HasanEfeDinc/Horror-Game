using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Degree : MonoBehaviour
{
    [SerializeField] private Text ThermometerDegree;

    private int degree;

    private void Start()
    {
        StartCoroutine(randomdegreee());
    }

    private void OnEnable()
    {
        StartCoroutine(randomdegreee());
    }


    IEnumerator randomdegreee()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            degree = Random.Range(0, 5);
            ThermometerDegree.text = degree.ToString();
        }
    }
}
