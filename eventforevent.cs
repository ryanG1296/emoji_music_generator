using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class eventforevent : MonoBehaviour
{
    public bool hasTriggered2 = false;
    private bool hasTriggered = false; // 记录事件是否已触发

    private string[] firstArray = new string[]
    {
        "_1", "_2", "_3", "_4", "_5", "_6", "_7", "_8", "_9", "_10", "_11", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "Frame (1)", "Frame (2)", "Frame (3)", "Frame (4)", "Frame", "Group 33", "Group 34", "Group 35", "Group 77", "Group 78", "Group 79", "Group 80", "Group 81", "Group 82", "Group 83", "Group 84", "Group 85", "Group 86", "Group 87", "Group 88", "Group 89", "Group 90", "Group 91", "Group 92", "Group 93", "Group 94", "Group 95", "Group 96", "Group 97", "Group 98", "Group 99", "Group 100", "Group 101", "Group 102", "Group 103", "Group 104", "Group 105", "Group 106", "Group 107", "Group 108", "Group 109", "Group 110", "Group 111", "Group 112", "Group 113", "Group 114", "Group 115", "Group 116", "Group 117", "Group 118", "Group 119", "Group 120", "Group 121", "Group 122", "Group 123", "Group 124", "Group 124@2x", "Group 125", "Group 126", "Group 127", "Group 128", "Group 129", "Group 130", "Group 131", "Group 132", "Group 133", "Group 134", "Group 135", "Group 136", "Group 137", "Group 138", "Group 139", "Group 140", "Group 141", "Group 142", "Group 143", "Group 144", "Group 145", "Group 146", "Group 147", "Group 148", "Group 149", "Group 150", "Group 151", "Group 152", "Group 153", "Group 154", "Group 155", "Group 156", "Group 157", "Group 158", "Group 159", "Group 160", "Group 161", "Group 162", "Group 163", "Group 164", "Group 165", "Group 166", "Group 167", "Group 168", "Group 169", "Group 170", "Group 171", "Group 172", "Group 173", "Group 174", "Group 175", "Group 176", "Group 177", "Group 178", "Group 179", "Group 180", "Group 181", "Group 182", "Group 183", "Group 184", "Group 185", "Group 186", "Group 187", "Group 188", "Group 189", "Group 190", "Group 191"
    };

    private string[] secondArray = new string[]
    {
        "乡村", "R&B", "爵士", "拉丁", "POP", "民族音乐", "蓝调", "电子音乐", "摇滚", "HIP-HOP", "古典风", "风格", "笑脸", "忧郁", "哭泣", "大哭", "惊讶", "高兴", "吃惊", "痛苦", "大笑", "示爱", "爱慕", "哭笑", "得意", "调皮", "晕", "吃惊", "愤怒", "不屑", "高兴", "调皮", "难受", "中毒、恶心", "高兴", "魔鬼", "伤心", "爱心", "点赞", "小号", "小提琴", "萨克斯", "小木琴","笛子", "鼓", "钢琴", "吉他", "汉堡", "奶茶", "蛋糕", "糖果", "酒杯", "魔法球", "曲奇饼干", "书本", "四叶草", "拳击手套", "画板", "照相机", "小鱼", "戒指", "灯泡", "电脑", "话筒", "团团签", "汉堡", "冰淇淋", "咖啡", "圣诞树", "礼物", "蝴蝶结", "雪人", "云团", "炸弹", "奶茶", "金钱", "蛋糕", "糖果", "酒杯", "水晶球", "曲奇饼", "书", "四叶草", "拳击手套", "画板", "照相机", "贝果", "翅膀", "鱼", "戒指", "灯泡", "电脑", "话筒", "团子", "冰淇凌", "风景照", "咖啡", "圣诞树", "贝果", "钱", "炸弹", "云朵", "雪人", "蝴蝶结", "礼物", "翅膀", "汉堡", "奶茶", "蛋糕", "糖果", "酒杯", "水晶球", "曲奇饼", "书本", "四叶草", "拳击手套", "颜料", "相机", "小鱼", "戒指", "灯泡", "电脑", "话筒", "团团签", "冰淇凌", "咖啡", "圣诞树", "礼物", "蝴蝶结", "雪人", "云团", "炸弹", "钱", "贝果", "翅膀", "摩天轮", "海浪", "雪花", "叶子", "屋子", "火灾", "鲜花", "彩虹", "雏葵", "喝彩", "泡泡", "城堡", "椰子树", "伞", "帐篷", "烟花", "夜景", "山", "日出", "月亮", "土星", "玫瑰枯萎", "火球", "鲜花", "睡着", "困倦", "泡泡浴", "手风琴"
    };

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x > 50 && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(HandlePageReachedFifty());
        }
    }

    private IEnumerator HandlePageReachedFifty()
    {
        yield return new WaitForSeconds(1);

        string aminuosiPath = Path.Combine(Application.streamingAssetsPath, "aminuosi.txt");
        string ottoPath = Path.Combine(Application.streamingAssetsPath, "otto.txt");

        if (File.Exists(aminuosiPath))
        {
            UnityEngine.Debug.Log("aminuosi.txt exists.");

            string[] aminousiLines = File.ReadAllLines(aminuosiPath);
            List<string> descriptions = new List<string>();

            foreach (var line in aminousiLines)
            {
                for (int i = 0; i < firstArray.Length; i++)
                {
                    if (line.Trim() == firstArray[i])
                    {
                        descriptions.Add(secondArray[i]);
                        break;
                    }
                }
            }

            string result = string.Join(" ", descriptions);
            result += " ";

            // 清空 otto.txt 文件内容
            File.WriteAllText(ottoPath, string.Empty);

            File.WriteAllText(ottoPath, result);
            UnityEngine.Debug.Log("Content written to otto.txt: " + result);
            
            yield return new WaitForSeconds(3);
            
            // 启动 Python 程序
            Thread thread = new Thread(StartDemo3Exe);
            thread.Start();
        }
        else
        {
            UnityEngine.Debug.LogError("aminuosi.txt does not exist.");
        }
    }


    private void StartDemo3Exe()
    {
        if (hasTriggered2==true){return;}
        hasTriggered2 = true;
        
        string exePath = Path.Combine(Application.streamingAssetsPath, "demo3.exe");
        string logFilePath = Path.Combine(Application.streamingAssetsPath, "demo3_log.txt");

        UnityEngine.Debug.Log("Starting demo3.exe");

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = exePath;
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.OutputDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                UnityEngine.Debug.Log(args.Data);
                File.AppendAllText(logFilePath, args.Data + Environment.NewLine);
            }
        };
        process.ErrorDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                UnityEngine.Debug.LogError(args.Data);
                File.AppendAllText(logFilePath, "ERROR: " + args.Data + Environment.NewLine);
            }
        };

        try
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Failed to start demo3.exe: " + ex.Message);
        }
    }

    

}