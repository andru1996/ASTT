using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    [SerializeField] private CardBundleData _cardData;

    private List<CardData> _possibleCards;

    public CardBundleData CardData { get => _cardData;}

    public void LoadNewPossibleCards()
    {
        _possibleCards = new List<CardData>();
        foreach (CardData card in _cardData.CardDatas)
        {
            _possibleCards.Add(card);
        }
    }

    public CardData RandomCardData()
    {
        int randomIndexCard = Random.Range(0, _possibleCards.Count);
        CardData card = _possibleCards[randomIndexCard];
        _possibleCards.Remove(card);
        return card;
    }
}
