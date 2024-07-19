using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class LoadingAndUnloadingItems
{
    private static List<string> listOfItems = new List<string>();
    private static int numberOfItemsInList = 0;

    public static List<string> ReturnListItems()
    {
        return listOfItems;
    }

    public static void AddItemToListAndSaveFileAndIncrementCounter(string _itemName)
    {
        IncrementNumberCount();
        AddItemToList(_itemName);
        AddItemToSaveFile();
    }

    // re add items to the inventory from the saved text file .. called from CreateIOFiles.cs on reloading
    public static void ReloadItems()
    {
        using (StreamReader sr = File.OpenText(StaticFileNames.savePath))
        {
            if (sr.Peek() > -1 )
            {
                CountNumberOfItemsInInventory(sr); // read first line (string, stating amount of items in inventory)
                Debug.Log($"reloading items .. numberOfItemsInList: {numberOfItemsInList}..");
                for (int i = 0; i < numberOfItemsInList; i++) // read and add "items" to the list
                {
                    AddItemToList(sr.ReadLine());
                }
            }
            else
            {
                Debug.LogWarning($"text file is empty..");
            }
        }
        AssetDatabase.Refresh();
    }

    public static void ClearListAndReloadItems()
    {
        ReturnListItems().Clear();
        ResetItemCount();
        ReloadItems();
    }

    public static void DeleteSavedItemsFromTextFile()
    {
        using (StreamWriter sw = File.CreateText(StaticFileNames.savePath))
        {
            sw.Close();
        }
        AssetDatabase.Refresh();
    }

    public static int ReturnCurrentAmountOfItemsInList()
    {
        return numberOfItemsInList;
    }

    private static void AddItemToList(string _itemName)
    {
        listOfItems.Add(_itemName);
    }

    private static void ResetItemCount()
    {
        numberOfItemsInList = 0;
    }

    private static void IncrementNumberCount()
    {
        numberOfItemsInList++;
    }

    private static void CountNumberOfItemsInInventory(StreamReader sr)
    {
        string _tempNum = "";
        foreach (char child in sr.ReadLine())
        {
            if (char.IsDigit(child))
            {
                _tempNum += Convert.ToString(child);
            }
        }
        numberOfItemsInList = int.Parse(_tempNum);
        //Debug.Log($"numberOfItemsInList: {numberOfItemsInList}");
    }

    private static void AddItemToSaveFile()
    {
        using (StreamWriter sw = File.CreateText(StaticFileNames.savePath))
        {
            sw.WriteLine($"{StaticFileNames.amountOfItemsInInventory} ({numberOfItemsInList.ToString()})"); // amount of items

            for (int i = 0; i < listOfItems.Count; i++) // print each item in list
            {
                sw.WriteLine($"{listOfItems[i]}");
            }
        }
        AssetDatabase.Refresh();
    }
}