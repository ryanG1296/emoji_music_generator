using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Threading;

public class lyrics : MonoBehaviour
{
    private string filePath = Path.Combine(Application.streamingAssetsPath, "lyricsreturn.txt");
    private string lastFileContent = "";
    private bool hasTriggered = false;
    public TextMeshProUGUI OutputText; // 引用 UI TextMeshPro 组件
    private AudioClip microphoneClip;
    private float[] samples;
    private const int sampleWindow = 128;
    private const float soundThreshold = 0.1f; // 声音阈值

    // Start is called before the first frame update
    void Start()
    {
        // 检查 OutputText 是否已分配
        if (OutputText == null)
        {
            Debug.LogError("OutputText is not assigned in the Inspector.");
            return;
        }

        // 初始化时读取文件内容
        if (File.Exists(filePath))
        {
            lastFileContent = File.ReadAllText(filePath);
            Debug.Log("Initial file content: " + lastFileContent);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        

        // 开始定时检查文件内容变化
        StartCoroutine(CheckFileContentPeriodically());
    }

    // Update is called once per frame
    void Update()
    {
        if (File.Exists(filePath))
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string currentFileContent = sr.ReadToEnd();
                    if (currentFileContent != lastFileContent)
                    {
                        lastFileContent = currentFileContent;
                        StartCoroutine(CheckFileAndMoveCamera());
                    }
                }
            }
            catch (IOException e)
            {
                Debug.LogError($"IOException: {e.Message}");
            }
        }
    }

    private IEnumerator CheckFileContentPeriodically()
    {
        while (true)
        {
            Debug.Log("Checking file content...");
            yield return new WaitForSeconds(0.5f); // 每0.5秒检查一次文件内容
            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string currentFileContent = sr.ReadToEnd();
                        if (currentFileContent != lastFileContent)
                        {
                            Debug.Log("File content has changed.");
                            lastFileContent = currentFileContent;
                            StartCoroutine(CheckFileAndMoveCamera());
                        }
                        else
                        {
                            Debug.Log("File content has not changed.");
                        }
                    }
                }
                catch (IOException e)
                {
                    Debug.LogError($"IOException: {e.Message}");
                }
            }
            else
            {
                Debug.LogWarning($"File {filePath} does not exist.");
            }
        }
    }

    private void CheckFileContent()
    {
        Debug.Log("Checking file content...");
        if (File.Exists(filePath))
        {
            Debug.Log("File exists: " + filePath);
            string currentFileContent = File.ReadAllText(filePath);
            Debug.Log("Current file content: " + currentFileContent);
            if (currentFileContent != lastFileContent)
            {
                Debug.Log("File content updated: " + currentFileContent);
                lastFileContent = currentFileContent;

                // 更新当前对象的内容
                UpdateLyricsContent(currentFileContent);

                // 开始页面变化和歌词显示
                StartCoroutine(CheckFileAndMoveCamera());
            }
            else
            {
                Debug.Log("File content has not changed.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    private IEnumerator CheckFileAndMoveCamera()
    {
        Debug.Log("Started CheckFileAndMoveCamera coroutine.");
        CheckFileContent();
        // 移动 Main Camera
        Camera.main.transform.position += new Vector3(11, 0, 0);
        Debug.Log("Camera position after first move: " + Camera.main.transform.position);
        yield return new WaitForSeconds(3);

        Camera.main.transform.position += new Vector3(11, 0, 0);
        Debug.Log("Camera position after second move: " + Camera.main.transform.position);
        yield return new WaitForSeconds(2);
        OutputText.text = lastFileContent;
        yield return new WaitForSeconds(2);

        // 开始缓慢向 y 轴正方向移动
        StartCoroutine(MoveUpwards());
    }

    private void UpdateLyricsContent(string content)
    {
        // 更新当前对象的内容
        if (OutputText != null)
        {   
            Thread.Sleep(3000);
            OutputText.text = content;
            Debug.Log("Updated lyrics content: " + content);
        }
        else
        {
            Debug.LogError("OutputText is not assigned.");
        }
    }

    private IEnumerator MoveUpwards()
    {
        while (true)
        {
            transform.position += new Vector3(0, 0.05f, 0); // 缓慢向 y 轴正方向移动
            yield return new WaitForSeconds(0.2f);
        }
    }
}