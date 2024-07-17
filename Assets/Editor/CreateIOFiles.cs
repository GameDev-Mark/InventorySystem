using UnityEngine;
using System.IO;
using UnityEditor;

public static class CreateIOFiles
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnRuntimeInitializeSaveFile()
    {
        if (Directory.Exists($"{StaticFileNames.savePathFolder}")) // if folder exists 
        {
            if (!File.Exists($"{StaticFileNames.savePath}")) // if text file does not exist.. create text file
            {
                StaticFileNames.saveFile = File.Create($"{StaticFileNames.savePath}");
                StaticFileNames.saveFile.Close();
                Debug.Log($"Created json SaveFile");
            }
            else // text file exists.. do something? re load? 
            {
                LoadingAndUnloadingItems.ReloadItems();
                Debug.Log($"json SaveFile already exists.. reload text file");
            }
        }
        else // if folder does not exists.. create folder and text file 
        {
            Directory.CreateDirectory($"{StaticFileNames.savePathFolder}");
            Debug.Log($"Created path folder");
            StaticFileNames.saveFile = File.Create($"{StaticFileNames.savePath}");
            StaticFileNames.saveFile.Close();
            Debug.Log($"Created json SaveFile");
        }

        AssetDatabase.Refresh(); // refresh assetDatabase
    }
}