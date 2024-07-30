using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NextColor
{
    [SerializeField, Tooltip("Number of yarn balls in queue")] private int _count = 3;
    [SerializeField] private GameManager _gameManager;

    public Queue<Color> NextColors = new();
    public Queue<YarnAttributesSO> NextYarnBall = new();

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
        return NextYarnBall.Peek().color;
    }

    public GameObject GetPrefab()
    {
        return NextYarnBall.Peek().color.YarnPrefab;
    }

    public Color GetColor()
    {
        return NextColors.Peek();
    }

    public void Remove()
    {
        NextColors.Dequeue();
        NextYarnBall.Dequeue();
        Add();
    }

    private void Add()
    {
        YarnAttributesSO nextColor = GetRandomColorSO();
        NextColors.Enqueue(nextColor.color.Color);
        NextYarnBall.Enqueue(nextColor);
    }

    private YarnAttributesSO GetRandomColorSO()
    {
        return _gameManager.GetRandomColorSO();
    }
}
