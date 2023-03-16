using System.Collections.Generic;
using New.Player.Absctract;
using UnityEngine;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private Transform root;
    private PlayerUi _playerUi;
    private GameObject[] lettersGo;
    private List<LetterUi> letters;

    public void Setup(string wordGoal,PlayerUi playerUi)
    {
        if (lettersGo != null && _playerUi != null)
        {
            // if we call this func on onother level we need to refresh the ui
            _playerUi.OnItemTouched -= OnItemTouched;
            _playerUi = null;
            foreach (var gameObject in lettersGo)
            {
                Destroy(gameObject);
            }
        }
        _playerUi = playerUi;
        letters = new List<LetterUi>();
        lettersGo = new GameObject[wordGoal.Length];
        Debug.Log(wordGoal.Length);

        for (int i = 0; i < wordGoal.Length; i++)
        {
            var letter = Instantiate(letterPrefab, root);
            lettersGo[i] = letter;
            var letterUi = lettersGo[i].GetComponent<LetterUi>();
            letterUi.Setup(wordGoal[i]);
            letters.Add(letterUi);
        }
        _playerUi.OnItemTouched += OnItemTouched;
    }

    private void OnItemTouched(InventoryItemMono obj)
    {
        foreach (var letterUi in letters)
        {
            if (letterUi.m_letter == obj.info.letter)
            {
                letterUi.SetActive(true,obj.info.color);
                letters.Remove(letterUi);
                break;
            }
        }
    }
}
