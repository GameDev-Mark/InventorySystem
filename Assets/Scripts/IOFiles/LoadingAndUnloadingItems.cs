using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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


    public static void ReloadItems()
    {
        using (StreamReader sr = File.OpenText(StaticFileNames.savePath))
        {
            CountNumberOfItemsInInventory(sr); // read first line (string, stating amount of items in inventory)
            for (int i = 0; i < numberOfItemsInList; i++) // read and add "items" to the list
            {
                AddItemToList(sr.ReadLine());
            }
        }
    }

    private static void AddItemToList(string _itemName)
    {
        listOfItems.Add(_itemName);
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
                Debug.Log($"_tempNum: {_tempNum}");
            }
        }
        numberOfItemsInList = int.Parse(_tempNum);
        Debug.Log($"numberOfItemsInList: {numberOfItemsInList}");
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
    }
}