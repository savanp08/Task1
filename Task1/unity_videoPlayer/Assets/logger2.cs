using UnityEngine;
using RenderHeads.Media.AVProVideo;
using System.IO;

public class AdvancedVideoMetricsLogger : MonoBehaviour
{
    public MediaPlayer mediaPlayer; // Assign this in the Unity Editor
    private string logFilePath = "Assets/AdvancedVideoMetricsLogs.txt"; // Path to log file

    private float lastTimeChecked;
    private float updateInterval = 3.0f; // How often to log metrics (in seconds)
    private float lastFrameTime;
    private float videoStartPlayTime;

    void Start()
    {
        if (mediaPlayer == null)
        {
            Debug.LogError("MediaPlayer is not assigned.");
            return;
        }

        // Subscribe to events
        mediaPlayer.Events.AddListener(OnMediaPlayerEvent);

        // Create or clear the log file at startup
        if (!File.Exists(logFilePath))
        {
            File.WriteAllText(logFilePath, "Time, Event, Additional Info\n");
        }
        else
        {
            File.AppendAllText(logFilePath, "Session Start\n");
        }

        lastTimeChecked = Time.time;
        lastFrameTime = Time.time;
        videoStartPlayTime = 0f;
    }

    private void Update()
    {
        if (Time.time - lastTimeChecked >= updateInterval)
        {
            LogMetrics();
            lastTimeChecked = Time.time;
        }
    }

    private void LogMetrics()
    {
        float currentTime = Time.time;
        float videoPlayTime = currentTime - videoStartPlayTime;
        string metrics = $"{currentTime}, Playback Info, " +
                         $"IsPlaying: {mediaPlayer.Control.IsPlaying()}, " +
                         $"Current Resolution: {mediaPlayer.Info.GetVideoWidth()}x{mediaPlayer.Info.GetVideoHeight()}, " +
                         $"Current Framerate: {(mediaPlayer.Info.HasVideo() ? mediaPlayer.Info.GetVideoDisplayRate() : 0)}, " +
                         $"Current Frames duration:{ (mediaPlayer.Info.HasVideo() ? mediaPlayer.Info.GetDurationFrames() : 0)}," +
                         $"Playback Time: {videoPlayTime}\n";

        File.AppendAllText(logFilePath, metrics);
    }

    private void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        string eventDescription = "";
        switch (et)
        {
            case MediaPlayerEvent.EventType.FirstFrameReady:
                videoStartPlayTime = Time.time; // Log when first frame is ready
                eventDescription = "First Frame Ready";
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                eventDescription = "Finished Playing";
                break;
            case MediaPlayerEvent.EventType.Error:
                eventDescription = $"Error: {errorCode}";
                break;
            default:
                eventDescription = et.ToString();
                break;
        }

        string logMessage = $"{Time.time}, {eventDescription}\n";
        File.AppendAllText(logFilePath, logMessage);
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (mediaPlayer != null)
        {
            mediaPlayer.Events.RemoveListener(OnMediaPlayerEvent);
        }

        File.AppendAllText(logFilePath, "Session End\n");
    }
}
