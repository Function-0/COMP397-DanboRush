/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-04 12:23:28 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-04 20:48:16
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

    [Header("Sound Effect")]
    public AudioSource pickUpBoxSound;

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
            pickUpBoxSound.Play();
            RefreshInventorySlot();
        }
    }

    private void RefreshInventorySlot()
    {
        ResetInventorySlotStates();
        for (int i = 0; i < inventoryList.Count; i++)
        {
            inventorySlots[i].transform.GetChild(0).gameObject.SetActive(true); // Update UI
        }
    }
    
    private void ResetInventorySlotStates()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].transform.GetChild(0).gameObject.SetActive(false); // Update UI
        }
    }

    public void UseItem(int index)
    {
        if (index < inventoryList.Count)
        {
            Debug.Log("Use Item" + index);
            inventoryList.RemoveAt(index);
            RefreshInventorySlot();
            ThrowBox();
        }
    }

    public void ThrowBox()
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
