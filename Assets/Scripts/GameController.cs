using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _taskText;

    private FrameController _frameController;
    [SerializeField] private DataController _dataController;
    [SerializeField] private FrameController[] _levelFrameController;

    private int _level;

    private CardData _taskCard;

    [SerializeField] private Image _restartBackgraund;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _level = 0;
        _dataController.LoadNewPossibleCards();

        LoadLevel();

        foreach (Frame frame in _frameController.Frames)
        {
            _frameController.AnimController.AnimBounceFrame(frame);
        }

        _frameController.AnimController.AnimFadeInText(_taskText);
    }

    private void LoadLevel()
    {
        ActiveFrame();
        _taskCard = _dataController.RandomCardData();

        FillFrames();

        SetTask();
    }

    private void ActiveFrame()
    {
        foreach (FrameController frameController in _levelFrameController)
        {
            frameController.gameObject.SetActive(false);
        }

        _levelFrameController[_level].gameObject.SetActive(true);
        _frameController = _levelFrameController[_level];
    }

    private void SetTask()
    {
        _taskText.text = "Find " + _taskCard.Value;
    }

    private void FillFrames()
    {
        List<CardData> cards = new List<CardData>();
        cards.Add(_taskCard);

        if(_dataController.CardData.CardDatas.Length >= _frameController.Frames.Length)
        {
            while (cards.Count != _frameController.Frames.Length)
            {
                int randomIndexCard = Random.Range(0, _dataController.CardData.CardDatas.Length);
                if (!cards.Contains(_dataController.CardData.CardDatas[randomIndexCard]))
                {
                    cards.Add(_dataController.CardData.CardDatas[randomIndexCard]);
                }
            }
        }
        else
        {
            Debug.LogError("Массив объектов меньше массива ячеек");
        }

        for(int i=0; i < _frameController.Frames.Length; i++)
        {
            int randomIndexCard = Random.Range(0, cards.Count);
            _frameController.Frames[i].Icon.sprite = cards[randomIndexCard].Sprite;
            _frameController.Frames[i].Value = cards[randomIndexCard].Value;

            cards.Remove(cards[randomIndexCard]);
        }
    }

    public void PlayerAnswer(Frame frame)
    {
        if(_taskCard.Value == frame.Value)
        {
            CorrectAnswer(frame);
        }
        else
        {
            IncorrectAnswer(frame);
        }
    }

    private void CorrectAnswer(Frame frame)
    {
        _frameController.AnimController.AnimCorrectAnswer(frame);
        SetClickOn(false);
        Invoke("NextLevel", _levelFrameController[_level].AnimController.AnimCorrectAnswerDuration);
    }

    private void NextLevel()
    {
        _level++;
        if(_level >= _levelFrameController.Length)
        {
            EndGame();
        }
        else
        {
            SetClickOn(true);
            LoadLevel();
        }
    }

    private void EndGame()
    {
        SetActiveRestart(true);

        _frameController.AnimController.AnimFadeInImage(_restartBackgraund);
        _frameController.AnimController.AnimFadeInImage(_restartButton.image);

    }

    private void SetActiveRestart(bool active)
    {
        _restartButton.gameObject.SetActive(active);
        _restartBackgraund.gameObject.SetActive(active);
    }

    public void RestartGame()
    {
        SetActiveRestart(false);

        SetClickOn(true);

        StartGame();
    }

    private void IncorrectAnswer(Frame frame)
    {
        _frameController.AnimController.AnimIncorrectAnswer(frame);
    }

    private void SetClickOn(bool active)
    {
        if(active)
        {
            foreach (Frame frame in _frameController.Frames)
            {
                frame.ClickOn();
            }
        }
        else
        {
            foreach (Frame frame in _frameController.Frames)
            {
                frame.ClickOff();
            }
        }
    }

}
