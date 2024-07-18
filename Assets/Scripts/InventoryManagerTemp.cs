using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerTemp : MonoBehaviour
{
    private string sword = "sword";

    private GameObject inventoryListText;
    private GameObject inventoryTemp;

    private bool isInventoryOpen;

    private void Awake()
    {
        inventoryTemp = GameObject.Find($"InventoryTemp");
        inventoryListText = GameObject.Find($"InventoryList_Text");
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
            inventoryListText.GetComponent<Text>().text += $"{i}) {LoadingAndUnloadingItems.ReturnListItems()[i]}\n";
        }
    }

    public void UI_AddItemToList() // attached to button add item
    {
        LoadingAndUnloadingItems.AddItemToListAndSaveFileAndIncrementCounter($"{sword}");
        Debug.Log($"Button: Add item to list: {sword}...");
        UI_RefreshInventory();
    }

    public void UI_RefreshInventory() // attached to button refresh inventory.. clear inventory text.. clear list and reload items.. reprint items onto UI
    {
        ClearInventoryText();
        LoadingAndUnloadingItems.ClearListAndReloadItems();
        UI_OutputListItems();
    }

    public void UI_ClearAndWipeInventory() // attached to button wipe inventory
    {
        ClearInventoryText();
        LoadingAndUnloadingItems.DeleteSavedItemsFromTextFile();
        UI_OutputListItems();
        UI_RefreshInventory();
    }

    public void UI_OpenOrCloseInventory()
    {
        if (ReturnInventoryOpenOrClosed())
        {
            inventoryTemp.SetActive(false);
        }
        else
        {
            inventoryTemp.SetActive(true);
            UI_RefreshInventory();
        }
    }

    private void ClearInventoryText() // empty inventory text
    {
        inventoryListText.GetComponent<Text>().text = string.Empty;
    }

    private bool ReturnInventoryOpenOrClosed()
    {
        isInventoryOpen = inventoryTemp.activeInHierarchy;
        return isInventoryOpen;
    }
}