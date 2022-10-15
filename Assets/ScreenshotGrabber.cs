#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ScreenshotGrabber
{
    private static string _path = "Screenshots";
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {
        if (!System.IO.Directory.Exists(_path))
            System.IO.Directory.CreateDirectory(_path);

        string[] files = System.IO.Directory.GetFiles(_path);
        string length = (files.Length).ToString("0000");
        ScreenCapture.CaptureScreenshot($"{_path}/Screenshot_{length}.png", 1);
    }
}
#endif