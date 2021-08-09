using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private string _value;
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get => _sprite;}
    public string Value { get => _value; }
}
