using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Mhz : MonoBehaviour
{
    [SerializeField] private Text ChannelFrequency;

    private float mhz;

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
            yield return new WaitForSeconds(1.15f);
            mhz = Random.Range(93.00f, 103.50f);
            ChannelFrequency.text = mhz.ToString();
        }
    }
}

