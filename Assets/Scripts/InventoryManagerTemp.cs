using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerTemp : MonoBehaviour
{
    private string sword = "sword";

    private GameObject inventoryListText;

    private void Awake()
    {
        inventoryListText = GameObject.Find("InventoryList_Text");
    }

    private void Start()
    {
        UI_OutputListItems();
    }

    public void UI_OutputListItems()
    {
        //Debug.Log($"List count: {LoadingAndUnloadingItems.ReturnListItems().Count}...");
        for (int i = 0; i < LoadingAndUnloadingItems.ReturnListItems().Count; i++)
        {
            //Debug.Log($"OUTPUT: {LoadingAndUnloadingItems.ReturnListItems()[i]}...");
            inventoryListText.GetComponent<Text>().text += $"{LoadingAndUnloadingItems.ReturnListItems()[i]}\n";
        }
    }


    public void UI_AddItemToList()
    {
        LoadingAndUnloadingItems.AddItemToListAndSaveFileAndIncrementCounter($"{sword}");
        Debug.Log($"Button: Add item to list: {sword}...");
    }
}
