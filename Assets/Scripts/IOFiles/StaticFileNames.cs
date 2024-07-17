using System.IO;
using UnityEngine;

public static class StaticFileNames
{
    public static FileStream saveFile;
    public static string savePathFolder = Path.Combine(Application.dataPath, $"JsonFiles");
    public static string savePath = Path.Combine(Application.dataPath, $"{savePathFolder}/SaveFile.txt");

    public static string amountOfItemsInInventory = $"Amount of items in inventory:";
}