using UnityEngine;
using RenderHeads.Media.AVProVideo;
using System.IO;
using System;

public class VideoPlayerMetricsLogger : MonoBehaviour
{
    public MediaPlayer mediaPlayer; // Assign this in Unity Editor
    private string logFilePath = "Assets/VideoPlayerMetricsLog.txt"; // Path for log file
    private float lastTimeChecked;

    // For throughput calculation (Simulated data)
    private long lastDataReceived = 0; // Last data received in bytes
    private long totalDataReceived = 0; // Total data received for session in bytes
    private float lastUpdateTime = 0; // Last time the data was updated

    void Start()
    {
        if (mediaPlayer == null)
        {
            Debug.LogError("MediaPlayer is not assigned.");
            return;
        }

        // Subscribe to media player events
        mediaPlayer.Events.AddListener(OnMediaPlayerEvent);

        // Prepare the log file
        PrepareLogFile();

        lastTimeChecked = Time.time;
        lastUpdateTime = Time.time;
    }

    void Update()
    {
        // Simulate data reception every second for throughput calculation
        if (Time.time - lastUpdateTime >= 1.0f)
        {
            // Simulate the reception of data (e.g., 240KB per second)
            long receivedData = 240 * 1024;
            totalDataReceived += receivedData;
            lastUpdateTime = Time.time;
        }

        // Log the throughput every 5 seconds
        if (Time.time - lastTimeChecked >= 4.0f)
        {
            LogThroughput();
            lastTimeChecked = Time.time;
        }
    }

    private void PrepareLogFile()
    {
        // Create or clear the log file at startup
        string header = "Timestamp, Event, Video Width, Video Height, Bitrate, Buffered Range\n";
        File.WriteAllText(logFilePath, header);
    }

    private void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        string logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}, {et}," +
                            $"{mp.Info.GetVideoWidth()}x{mp.Info.GetVideoHeight()}," +
                            $"{mp.Info.GetVideoFrameRate()}," +
                            $"{mp.Control.GetBufferedTimes()}\n";

        File.AppendAllText(logFilePath, logMessage);
    }

    private void LogThroughput()
    {
        float timeSpan = Time.time - lastTimeChecked;
        float throughput = (totalDataReceived - lastDataReceived) / timeSpan; // bytes per second

        string logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}, Throughput, , , {throughput} B/s\n";
        File.AppendAllText(logFilePath, logMessage);

        lastDataReceived = totalDataReceived;
    }

    void OnDestroy()
    {
        mediaPlayer.Events.RemoveListener(OnMediaPlayerEvent);
    }
}
