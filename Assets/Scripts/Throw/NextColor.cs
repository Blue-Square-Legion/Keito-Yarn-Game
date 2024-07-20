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

    public Queue<Color> NextColors = new();
    public Queue<GameObject> NextYarns = new();
    int colorindex = 0;
    bool _e;
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
    void Start()
    {
      _e= script._rand;
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
        NextYarns.Dequeue();
        NextColors.Dequeue();
        Add();
    }

    private void Add()
    {
<<<<<<< Updated upstream
        GameObject go = GetRandomPrefab();
=======
        ColorSO nextColor = ThreeColors();
        GameObject go = nextColor.YarnPrefab;
        colorindex++;
        NextColorQueue.Enqueue(nextColor);
>>>>>>> Stashed changes
        NextYarns.Enqueue(go);
        NextColors.Enqueue(go.GetComponent<Renderer>().sharedMaterial.color);
    }

    private GameObject GetRandomPrefab()
    {
        return _gameManager.GetRandomColorYarn();
    }
<<<<<<< Updated upstream
=======

    private ColorSO GetRandomColorSO()
    {
        return _gameManager.GetRandomColorSO();
    }

    private ColorSO ThreeColors()
    {
   
        if( _gameManager._ColorChangeRand==true)
              return _gameManager.GetRandomColorSO();
        else
        {
            if (colorindex == 3)
                colorindex = 0;
            return _gameManager.GetIndexColorSO(colorindex);
        }

      
    }
>>>>>>> Stashed changes
}
