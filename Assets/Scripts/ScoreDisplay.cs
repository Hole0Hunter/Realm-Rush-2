using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    Bank bank;
    TMP_Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

        string goldInStringFormat = bank.currentBalance.ToString();
        textComponent.text = "Gold: " + goldInStringFormat;
    }
}
