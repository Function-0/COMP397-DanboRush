/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-04 12:23:28 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-04 16:47:19
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public List<InventoryItem> inventoryList;

    [Header("Inventory Slots")]
    public List<GameObject> inventorySlots;

    public PlayerBehaviour player;
    public GameObject boxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        inventoryList = new List<InventoryItem>();
        ResetInventorySlotStates();
        player = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(InventoryItem item)
    {
        int newItemIndex = inventoryList.Count;
        // 5 inventory slots in total
        if (newItemIndex < 5)
        {
            inventoryList.Add(item);
            RefreshInventorySlot();
            Debug.Log("Inventory Added");
        }
    }

    private void RefreshInventorySlot()
    {
        ResetInventorySlotStates();
        for (int i = 0; i < inventoryList.Count; i++)
        {
            int delegateIndex = i;
            inventorySlots[i].transform.GetChild(0).gameObject.SetActive(true); // Update UI
            inventorySlots[i].GetComponent<Button>().onClick.AddListener (delegate {OnInventorySlotClick(delegateIndex);}); // attach on click event at runtime
            inventorySlots[i].GetComponent<Button>().interactable = true;
        }
    }
    
    private void ResetInventorySlotStates()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].transform.GetChild(0).gameObject.SetActive(false); // Update UI
            inventorySlots[i].GetComponent<Button>().onClick.RemoveAllListeners(); // Remove on click event 
            inventorySlots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void OnInventorySlotClick(int index)
    {
        Debug.Log("OnInventorySlotClick Event triggered" + index);
        if (index < inventoryList.Count)
        {
            Debug.Log("Use Item" + index);
            inventoryList.RemoveAt(index);
            RefreshInventorySlot();
            Debug.Log(inventoryList.Count);
            UseItem();
        }
    }

    public void UseItem()
    {
        Debug.Log("Use Box!!!!!!!!!!");
        GameObject boxObject = Instantiate(boxPrefab, player.transform);
        boxObject.transform.position = player.transform.position;
        boxObject.transform.forward = player.transform.forward;
    }
}

public class InventoryItem
{
    public enum Type
    {
        Box,
    }

    public Type type;
    public int quantity;
}
