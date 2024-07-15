using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NextColor
{
    [SerializeField, Tooltip("Number of yarn balls in queue")] private int _count = 3;
    [SerializeField] private GameManager _gameManager;

    public Queue<ColorSO> NextColorQueue = new();
    public Queue<Color> NextColors = new();
    public Queue<GameObject> NextYarns = new();

    public void Setup(GameManager gameManager)
    {
        _gameManager = gameManager;
        Random.InitState(System.DateTime.Now.Millisecond);
        for (int i = 0; i < _count; i++)
        {
            Add();
        }
    }

    public ColorSO GetColorSO()
    {
        return NextColorQueue.Peek();
    }

    public GameObject GetPrefab()
    {
        return NextYarns.Peek();
    }

    public Color GetColor()
    {
        return NextColors.Peek();
    }

    public void Remove()
    {
        NextColorQueue.Dequeue();
        NextYarns.Dequeue();
        NextColors.Dequeue();
        Add();
    }

    private void Add()
    {
        ColorSO nextColor = GetRandomColorSO();
        GameObject go = nextColor.YarnPrefab;
        NextColorQueue.Enqueue(nextColor);
        NextYarns.Enqueue(go);
        NextColors.Enqueue(nextColor.Color);
    }

    private GameObject GetRandomPrefab()
    {
        return _gameManager.GetRandomColorYarn();
    }

    private ColorSO GetRandomColorSO()
    {
        return _gameManager.GetRandomColorSO();
    }
}
