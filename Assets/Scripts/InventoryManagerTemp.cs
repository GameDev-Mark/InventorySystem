using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerTemp : MonoBehaviour
{
    public int amountOfInventorySlots = 16;
    private string sword = "sword";

    private GameObject inventoryListText;
    private GameObject inventoryTemp;
    private GameObject inventoryContainer;

    private bool isInventoryOpen;

    private void Awake()
    {
        inventoryTemp = GameObject.Find($"InventoryTemp");
        inventoryListText = GameObject.Find($"InventoryList_Text");
        inventoryContainer = inventoryTemp.GetComponent<Transform>().GetChild(0).gameObject;
    }

    private void Start()
    {
        InventoryCreation();
    }

    public void UI_AddItemToList() // attached to button add item
    {
        if (LoadingAndUnloadingItems.ReturnCurrentAmountOfItemsInList() < amountOfInventorySlots)
        {
            LoadingAndUnloadingItems.AddItemToListAndSaveFileAndIncrementCounter($"{sword}");
            Debug.Log($"Button: Add item to list: {sword}...");
            UI_RefreshInventory();
        }
    }

    public void UI_RefreshInventory() // attached to button refresh inventory.. clear inventory text.. clear list and reload items.. reprint items onto UI
    {
        ClearInventory();
        LoadingAndUnloadingItems.ClearListAndReloadItems();
        OutputSpriteItemsToInventory();
    }

    public void UI_ClearAndWipeInventory() // attached to button wipe inventory
    {
        ClearInventory();
        LoadingAndUnloadingItems.DeleteSavedItemsFromTextFile();
        OutputSpriteItemsToInventory();
        UI_RefreshInventory();
    }

    public void UI_OpenOrCloseInventory() // attched to button open/close inventory
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

    private void InventoryCreation() // populate inventory .. initial creation
    {
        for (int i = 0; i < amountOfInventorySlots; i++)
        {
            GameObject _inventorySlot = LoadResourceGameObject("InventorySlot");
            _inventorySlot.transform.SetParent(inventoryContainer.transform);
        }
    }

    private void OutputSpriteItemsToInventory()
    {
        //Debug.Log($"List count: {LoadingAndUnloadingItems.ReturnListItems().Count}...");
        for (int i = 0; i < LoadingAndUnloadingItems.ReturnListItems().Count; i++)
        {
            //Debug.Log($"OUTPUT: {LoadingAndUnloadingItems.ReturnListItems()[i]}...");
            if (i < amountOfInventorySlots)
            {
                Sprite _sprite = LoadResourceSprite("Sword");
                inventoryContainer.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = _sprite;
            }
        }
    }

    private GameObject LoadResourceGameObject(string _fileName)
    {
        GameObject _go = Instantiate(Resources.Load($"{_fileName}") as GameObject);
        Debug.Log($"gameObject created: {_go}..");
        return _go;
    }

    private Sprite LoadResourceSprite(string _fileName)
    {
        Sprite _sprite = Resources.Load<Sprite>($"Sprites/{_fileName}");
        Debug.Log($"Sprite created: {_sprite}");
        return _sprite;
    }

    private void ClearInventory() // empty inventory
    {
        foreach (Transform child in inventoryContainer.transform)
        {
            child.GetChild(0).GetComponent<Image>().sprite = null;
        }
    }

    private bool ReturnInventoryOpenOrClosed()
    {
        isInventoryOpen = inventoryTemp.activeInHierarchy;
        return isInventoryOpen;
    }
}