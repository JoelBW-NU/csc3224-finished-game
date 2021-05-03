using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class DevStats : MonoBehaviour
{
    // FPS variables
    int frames = 0;
    double differenceOverTime = 0.0f;
    double framesPerSecond = 0.0f;
    double updateRate = 4.0;

    [SerializeField]
    Text fps;

    // Memory variables
    double BYTES_TO_MEGABYTES = 1024.0 * 1024.0;
    double totalMemoryUsage = 0.0f;

    [SerializeField]
    Text memUsage;

    [SerializeField]
    Text objCount;

    [SerializeField]
    Text sessionDur;

    // Update is called once per frame
    void Update()
    {
        // Calculate FPS
        frames++;
        differenceOverTime += Time.deltaTime;
        // Update FPS if update time passed
        if (differenceOverTime > 1.0 / updateRate)
        {
            // New FPS
            framesPerSecond = frames / differenceOverTime;
            // Reset for next calculation
            frames = 0;
            differenceOverTime -= 1.0 / updateRate;
        }

        fps.text = framesPerSecond.ToString("F2");

        // Calculate Memory Usage
        totalMemoryUsage = Profiler.GetTotalAllocatedMemoryLong() / BYTES_TO_MEGABYTES;

        memUsage.text = totalMemoryUsage.ToString("F2") + "MB";

        objCount.text = ""+GameObject.FindObjectsOfType(typeof(MonoBehaviour)).Length;

        sessionDur.text = Time.realtimeSinceStartup.ToString("F2") + "s";

    }
}
