using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISystem : MonoBehaviour
{
    /// <summary>
    /// Thank you for purchasing my Inventory Asset!
    /// 
    /// by: Caio Flávio
    /// </summary>


    [Space(20)]
    [Header("Put the mouse over the variables to see the tips!")]
    

    [Tooltip("Your in-game cursor GameObject")]
    [SerializeField] private Transform cursor;

    [Tooltip("The dragged item image(child of cursor)")]
    [SerializeField] private SpriteRenderer cursorItemImg;

    //Selected uiSlot Transform(selected item)
    private Transform cursorItemObj;

    [Space(20)]

    [Tooltip("Your item database. I made a ScriptableObject database and put inside 'DAO' folder")]
    [SerializeField] private ItemDatabase allGameItems;

    [Tooltip("The parent object of all item slots inside Canvas")]
    [SerializeField] private Transform invBox;

    [Tooltip("All item slots inside invBox displayed on UI")]
    private List<Transform> uiSlot;
   
    [Header("Don't want that special items slots? Set SlotType size to '0'. Setting 0, you can place any item in any slot")]
    [Tooltip("Special items that the player can carry with him(The first of the UI and the bagItems) YOU CAN ADD OR REMOVE SPECIAL ITEMS FROM THIS LIST IF YOU WANT")]
    [SerializeField] private List<Item.type> slotType;

    [Tooltip("Max items that can be stacked in the same slot")]
    //Max items that can be stacked in the same slot
    private int maxAmountStack = 10;

    [Tooltip("Drop Amount Screen(to drop a fraction)")]
    [SerializeField] private Transform dropAmountScreen;

    [Tooltip("Where items are dropped in your scene (At the Player's feet?)")]
    [SerializeField] private Transform spawn;

    [Tooltip("One generic prefab to be used by all types of items(Add Sprite and Add Physics). Check DAO scriptableObject to see the items structure.")]
    [SerializeField] private Transform itemPrefab;

    [Tooltip("List of items the player is carrying in the bag")]
    [SerializeField] private List<Item> bagItems;

    void Awake()
    {
        //Hide cursor
        Cursor.visible = false;
        //initialize selected item slot with NULL
        cursorItemObj = null;

        //Initializing lists
        uiSlot = new List<Transform>();
        bagItems = new List<Item>();

        //uiSlot and bagItems needs to be at the same size!!! 
        //bagItems has the items that the Player is carring.
        //uiSlot shows all itens in the bagItems on the UI
        //You can carry as many itens as you want, but they wont be visible if you dont have enough UI Slots
        //Adding empty items to the backpack so that the vector is the same size as the number of UI Slots
        foreach (Transform c in invBox.transform)//Getting inventory uiSlot
        {
            uiSlot.Add(c);
            //The first bagItems index's are of special items. The other are NormalItems
            bagItems.Add(new Item { selectedType = uiSlot.Count <= slotType.Count ? slotType[uiSlot.Count-1] : Item.type.NormalItem});

        }
    }

    public void AddItemToBag(Item newItem)
    {
        //Placing new item in a empty slot AFTER special items slots
        int idx = bagItems.FindIndex(slotType.Count, f => f.id == -1);
        if (idx != -1)
        {
            //Add item to bag
            bagItems[idx] = newItem;

            //Update slot with new item
            UpdateSlot(idx, uiSlot[idx]);   
        }
    }

    public void SetCursorItemImg(Image value)
    {
        if (!Input.GetMouseButtonDown(0)) return;


        //If there is an item in the selected slot
        if (!value.name.Equals("0"))
        {
            //If any item was already being dragged( |item| -> |item| )
            if (cursorItemObj != null)
            {
                //Get item index from list
                int idxFirst = GetIndexOfObj(cursorItemObj);
                int idxLast = GetIndexOfObj(value.transform);

                //Switch items from bag
                SwitchBagItems(idxFirst, idxLast);

                //Update slots
                UpdateSlot(idxFirst, uiSlot[idxFirst]);
                UpdateSlot(idxLast, uiSlot[idxLast]);

                //Reset cursor item image
                cursorItemImg.sprite = null;
                //Reset cursor obj
                cursorItemObj = null;

            }
            else //( EmptyMouse grabbing an item )
            {

                //Add "shadow" to moved image
                value.color = Color.black;
                //Set cursor item image
                cursorItemImg.sprite = value.sprite;
                //Set cursor item object
                cursorItemObj = value.transform;

                /*
                 * int idx = GetIndexOfObj(cursorItemObj);
                 * if(bagItems[idx].type == "Potion")
                 * {
                 *     //PLAY POTION SOUND ("Ploof")
                 * }
                 * */
            }
        }
        else
        {
            //Placing item in the empty slot ( |item| -> | EmptySlot | )
            if (cursorItemObj != null)
            {
                //Get item index from list
                int idxFirst = GetIndexOfObj(cursorItemObj);
                int idxLast = GetIndexOfObj(value.transform);
                //

                //Switch items from bag
                SwitchBagItems(idxFirst, idxLast);

                //Update slots
                UpdateSlot(idxFirst, uiSlot[idxFirst]);
                UpdateSlot(idxLast, uiSlot[idxLast]);
               
                //Reset cursor item image
                cursorItemImg.sprite = null;
            }

            //Set cursor item object
            cursorItemObj = null;
        }
    }

    public void DropItem()
    {

        //If there is an item being dragged
        if (cursorItemObj != null)
        {
            //Get index of selected item
            int idx = GetIndexOfObj(cursorItemObj);

            //The dragged item is in the bag
            if(idx != -1)
            {
                //There are staked items?
                if(bagItems[idx].amount > 1)
                {
                    //Open amountScreen
                    dropAmountScreen.gameObject.SetActive(true);
                    //Update slider values
                    dropAmountScreen.GetChild(0).GetChild(1).GetComponent<Slider>().value = 1;
                    dropAmountScreen.GetChild(0).GetChild(1).GetComponent<Slider>().maxValue = bagItems[idx].amount;
                    //Set item image
                    dropAmountScreen.GetChild(0).GetChild(0).GetComponent<Image>().sprite = bagItems[idx].Image;
                    //Set 1 to amount txt from amount screen
                    dropAmountScreen.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = 1.ToString();

                    //Update slot name to (hasItem)
                    uiSlot[idx].name = 1.ToString();
                    uiSlot[idx].GetComponent<Image>().color = Color.white;
                }
                else
                {
                    //Reset cursor obj
                    cursorItemObj = null;

                    //Reset slot
                    uiSlot[idx].name = 0.ToString();
                    uiSlot[idx].GetComponent<Image>().sprite = null;
                    uiSlot[idx].GetComponent<Image>().color = Color.clear;
                    uiSlot[idx].GetChild(0).gameObject.SetActive(false);
                    //

                    //Spawn Item
                    InstantiateItemOnTheScene(1, idx);


                    //Reset item in bag
                    ResetItemSlot(idx);
                }
            }
            //Reset cursorItemimage
            cursorItemImg.sprite = null;
        }
    }

    public void DropAmount(Slider value)
    {   
        //Get index of selected item
        int idx = GetIndexOfObj(cursorItemObj);

        //Remove value from item amount 
        bagItems[idx].amount -= (int)value.value;

        //Reset cursor obj
        cursorItemObj = null;

        //Instantiate amount
        InstantiateItemOnTheScene((int)value.value, idx);
        
        //If selected all amount
        if (value.value == value.maxValue)
        {
            //Reset slot and bag
            uiSlot[idx].name = 0.ToString();
            uiSlot[idx].GetComponent<Image>().sprite = null;
            uiSlot[idx].GetComponent<Image>().color = Color.clear;
            uiSlot[idx].GetChild(0).gameObject.SetActive(false);
            ResetItemSlot(idx);
            //
        }
        else
        {
            //Is there one left? Turn off amount txt
            if (value.value == value.maxValue-1)
                uiSlot[idx].GetChild(0).gameObject.SetActive(false);

            //Show img sprite color
            uiSlot[idx].GetComponent<Image>().color = Color.white;
            //Set amount text
            uiSlot[idx].GetChild(0).GetChild(0).GetComponent<Text>().text = bagItems[idx].amount.ToString();
        }

        //Reset slider value to 1
        value.value = 1;
        //Turn amount screen off
        dropAmountScreen.gameObject.SetActive(false);
    }

    #region Helpers
    private void SwitchBagItems(int idxFirst, int idxLast)
    {
        //If Drag and Drop at the same place
        if (bagItems[idxFirst] == bagItems[idxLast]) return;

        //Can be stacked!
        if (bagItems[idxFirst].id == bagItems[idxLast].id)
        {
            int diff = bagItems[idxFirst].amount + bagItems[idxLast].amount;

            //Isn't more than maxStack
            if (diff <= maxAmountStack)
            {
                ResetItemSlot(idxFirst);
                bagItems[idxLast].amount = diff;
            }
            else//If more than maxStack, fulfill the last and put the rest inside the first
            {
                bagItems[idxFirst].amount = diff - maxAmountStack;
                bagItems[idxLast].amount = maxAmountStack;

            }
        }
        else
        {
            //The first item and the destination item are SPECIAL ITEMS?
            int idxFirstType = slotType.IndexOf(bagItems[idxFirst].selectedType);
            int idxLastType = slotType.IndexOf(bagItems[idxLast].selectedType);

            //If special Item is being moved FROM the special slotTypes
            if(idxFirst < slotType.Count)
            {
                //If Last Item is special too
  
                //If both items has the same type
                if ((idxLastType != -1 && idxFirstType == idxLastType) || //Dragged item is special and destination item too
                    (idxLast >= slotType.Count &&  bagItems[idxLast].id == -1)) //Dragging any item to Non-Special and non-empty slots
                {
                    Item auxItem = bagItems[idxFirst];
                    bagItems[idxFirst] = bagItems[idxLast];
                    bagItems[idxLast] = auxItem;

                    //Keep dragged item type on bagItem slot(See the Inspector at bagItems variable)
                    bagItems[idxFirst].selectedType = bagItems[idxLast].selectedType;

                }
                
            }
            else
            {
                //Special to special or all itens that can be moved along of uiSlots
                if (idxFirstType == idxLastType || idxLast >= slotType.Count)
                {
                    Item auxItem = bagItems[idxFirst];
                    bagItems[idxFirst] = bagItems[idxLast];
                    bagItems[idxLast] = auxItem;

                    if (idxFirst >= slotType.Count && bagItems[idxFirst].id == -1)
                        bagItems[idxFirst].selectedType = Item.type.NormalItem;
                }
            }
        }
    }

    private void ResetItemSlot(int idx)
    {
        bagItems[idx] = new Item { selectedType = idx < slotType.Count ? slotType[idx] : Item.type.NormalItem };
    }

    private void UpdateSlot(int itemIdx, Transform slot)
    {
        //Update slot image
        slot.GetComponent<Image>().sprite = bagItems[itemIdx].Image;
        //Update to a slot that has an item
        if (bagItems[itemIdx].id != -1)
        {
            //Set slot image visible
            slot.GetComponent<Image>().color = Color.white;
            //Rename slot to 1(is being used)
            slot.name = 1.ToString();
        }
        else//Update to an empty slot
        {
            //Rename slot to 0(is empty)
            slot.name = 0.ToString();
            //Set slot image to transparent
            slot.GetComponent<Image>().color = Color.clear;
        }

        //Is it a stack?
        if (bagItems[itemIdx].amount > 1)
        {
            //Show slot amount
            slot.GetChild(0).gameObject.SetActive(true);
            //Set slot amount txt
            slot.GetChild(0).GetChild(0).GetComponent<Text>().text = bagItems[itemIdx].amount.ToString();
        }
        else
        {
            //Hide slot amount
            slot.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void CloseDropAmountScreenWithoutDropping()
    {
        dropAmountScreen.gameObject.SetActive(false);
        cursorItemObj = null;
    }

    private void InstantiateItemOnTheScene(int amount, int itemIdx)
    {
        //The Generic item Prefab can be used in this way.
        for(int i = 0; i < amount; i++)
        {
            Transform it = Instantiate(itemPrefab, spawn.position, Quaternion.identity);
            it.GetComponent<SpriteRenderer>().sprite = bagItems[itemIdx].Image;
            it.gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    public void DropSliderOnValueChanged(Slider value)
    {
        //Update amount text from amount screen
        dropAmountScreen.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = value.value.ToString();
    }

    private int GetIndexOfObj(Transform obj)
    {
        //Return the item position inside uiSlot/BagItems
        return uiSlot.IndexOf(obj);
    }
    #endregion

    #region GameDeveloper useful Functions
    public void AddItem(Item capturedItemByTrigger)
    {
        //The player collided with an item from the ground
        AddItemToBag(capturedItemByTrigger);
    }

    public List<Item> ReturnBagItems()
    {
        return bagItems;
    }

    public int SearchForItemInBagItems( int itemId )
    {
        //If TRUE : returns the position of the item in the list
        //If FALSE: returns -1
        return bagItems.FindIndex(i => i.id == itemId);
    }

    public int UseItem( Transform value ) //UiSlot buttons has OnClick sending their own transforms
    {
        //Using a Potion...

        //Get index of selected item inside Slot List
        //The index will be returned... and after that you can call DeleteItem() function
        int idx = GetIndexOfObj(value);
        return idx;
    }

    public void DeleteItem(int idx)
    {
        ResetItemSlot(idx);
    }
    #endregion

    #region DELETE THIS FUNCTION
    public void CollectItemButton()
    {
        //Random an item from database
        int randItem = Random.Range(0, allGameItems.ItemList.Count);
        Item item = allGameItems.CopyItem(randItem);
        item.amount = Random.Range(1, 10);

        AddItemToBag(item);
    }
    #endregion

    void Update()
    {
        //Mouse cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -4;
        cursor.position = mousePos;
    }
}
