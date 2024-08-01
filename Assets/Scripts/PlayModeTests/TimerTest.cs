using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerTest
{
    GameManager testManager = new GameObject().AddComponent<GameManager>();
    BestTimeSO testSO = ScriptableObject.CreateInstance<BestTimeSO>();

    /// <summary>
    /// Tests if the current time is better/less than the best time and overrides it if it is
    /// </summary>
    [Test]
    public void BestTimeTest()
    {
        testManager.CurrentTime = 10;
        testSO.SetBestTimeForCurrentScene(20);
        testSO.SetBestTimeForCurrentScene(testManager.CurrentTime);

        Assert.AreEqual(10, testSO.GetBestTimeForCurrentScene());
    }

    /// <summary>
    /// Tests if the current time is not better/less than the best time and does not override it if it is
    /// </summary>
    [Test]
    public void CurrentTimeTest()
    {
        testManager.CurrentTime = 20;
        testSO.SetBestTimeForCurrentScene(10);
        testSO.SetBestTimeForCurrentScene(testManager.CurrentTime);

        Assert.AreEqual(10, testSO.GetBestTimeForCurrentScene());
    }
}
