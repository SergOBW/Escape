using System;
using System.Collections.Generic;
using New.Player.Absctract;
using UnityEngine;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private Transform root;
    private List<LetterUi> letters;

    public void Setup(string wordGoal,PlayerUi playerUi)
    {
        letters = new List<LetterUi>();
        foreach (var wordChar in wordGoal)
        {
            var letter = Instantiate(letterPrefab, root);
            var letterUi = letter.GetComponent<LetterUi>();
            letterUi.Setup(wordChar);
            letters.Add(letterUi);
        }
        playerUi.OnItemTouched += OnItemTouched;
    }

    private void OnItemTouched(InventoryItemMono obj)
    {
        foreach (var letterUi in letters)
        {
            if (letterUi.m_letter == obj.info.letter)
            {
                letterUi.SetActive(true);
                letters.Remove(letterUi);
                break;
            }
        }
    }
}
