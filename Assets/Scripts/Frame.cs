using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    private Image _icon;
    private string _value;
    private RectTransform _frameTransform;
    private RectTransform _iconTransform;
    private ParticleSystem[] _particles;

    private EventTrigger _eventTrigger;

    public RectTransform FrameTransform { get => _frameTransform; }
    public RectTransform IconTransform { get => _iconTransform; }
    public ParticleSystem[] Particles { get => _particles;}
    public string Value { get => _value; set => _value = value; }
    public Image Icon { get => _icon; set => _icon = value; }

    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[2];
        _iconTransform = GetComponentsInChildren<RectTransform>()[3];
        _frameTransform = GetComponent<RectTransform>();
        _particles = GetComponentsInChildren<ParticleSystem>();

        _eventTrigger = GetComponent<EventTrigger>();
    }

    private void Start()
    {
        ClickOn();
    }

    public void ClickOn()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { FindObjectOfType<GameController>().PlayerAnswer(this); });
        _eventTrigger.triggers.Add(entry);
    }

    public void ClickOff()
    {
        _eventTrigger.triggers.RemoveRange(0, _eventTrigger.triggers.Count);
    }
}
