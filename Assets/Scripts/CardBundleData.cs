using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardBundleData", menuName = "Card Bundle Data", order = 10)]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private CardData[] _cardDatas;

    public CardData[] CardDatas { get => _cardDatas;}
}
