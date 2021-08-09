using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    private Frame[] _frames;

    private AnimController _animController;

    public Frame[] Frames { get => _frames; set => _frames = value; }
    public AnimController AnimController { get => _animController; set => _animController = value; }

    private void Awake()
    {
        _frames = GetComponentsInChildren<Frame>();
        _animController = FindObjectOfType<AnimController>();
    }

}
