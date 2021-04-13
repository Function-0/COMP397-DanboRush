using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBox : MonoBehaviour
{
    private int count = 1;
    public ThrowBoxInventory throwBoxInventory;
    public InventoryController inventoryController;

    private int hitpoints = 1;

    
    // Observer Pattern - Observable(Subject)
	public delegate void BoxPickedUp();
	public static event BoxPickedUp BoxPickedUpEvent;

    // Start is called before the first frame update
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitpoints --; // to prevent triggered twice
            if (hitpoints > -1)
            {
                inventoryController.AddItem( new InventoryItem {type = InventoryItem.Type.Box, quantity = 1} );
                if (BoxPickedUpEvent != null)
                {
                    BoxPickedUpEvent();
                }
                // throwBoxInventory.counter(count);
            }
            
            Destroy(gameObject);
        }
    }
}
