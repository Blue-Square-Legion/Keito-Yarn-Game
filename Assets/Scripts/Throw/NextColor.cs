using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class NextColor
{
    [SerializeField, Tooltip("Number of yarn balls in queue")] private int _count = 3;
    [SerializeField] private GameManager _gameManager;

    public Queue<ColorSO> NextColorQueue = new();
    public Queue<Color> NextColors = new();
    public Queue<GameObject> NextYarns = new();
    int colorindex = 0;
//     bool _e;
    public  ThreeColors script;
    public void Setup(GameManager gameManager)
    {
        _gameManager = gameManager;
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        for (int i = 0; i < _count; i++)
        {
            Add();
        }
    }
//     void Start()
//     {
//       _e= script._rand;
//     }

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
        ColorSO nextColor = GetNextColors().color;
        GameObject go = nextColor.YarnPrefab;
        NextColorQueue.Enqueue(nextColor);
        NextYarns.Enqueue(go);
        NextColors.Enqueue(nextColor.Color);
    }

    private YarnAttributesSO GetRandomColorSO()
    {
        return _gameManager.GetRandomColorSO();
    }

    private YarnAttributesSO GetNextColors()
    {
   
        if( _gameManager._ColorChangeRand)
        {
              return _gameManager.GetRandomColorSO();
        }
        else
        {
            colorindex++;
            if (colorindex >= _gameManager.NumberOfColors)
            {
                colorindex = 0;
            }
            return _gameManager.GetIndexColorSO(colorindex);
        }

      
    }

}
