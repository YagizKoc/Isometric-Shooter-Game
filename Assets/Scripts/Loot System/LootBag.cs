using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems() {
    
        int randomNumber = Random.Range(1, 101);
        List<Loot> droppedItems = new List<Loot>();
        foreach (Loot item in lootList) {

            if (randomNumber <= item.dropChance) { 
            
                droppedItems.Add(item);
                
            }  
        }
        return droppedItems;

        //this part was for to drop only one item. It's left for further development choices.
        /*if (possibleItems.Count > 0) {

            int i = 0;
            while (i < possibleItems.Count)
            {
                Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
                return droppedItem;
            }
        }
        Debug.Log("No loot dropped");
        return null;*/
    }

    public void InstantiateLoot(Vector3 spawnPosition) { 
    
        List<Loot> droppedItems = GetDroppedItems();
        if (droppedItems != null)
        {
            foreach (Loot item in droppedItems)
            {
                //Spawn
                GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
                lootGameObject.GetComponent<MeshFilter>().mesh = item.lootMesh;

                //Collider

                //Tag
                lootGameObject.tag = item.lootTag;

                //Drop effect
                float dropForce = 50f;
                Vector3 dropDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                lootGameObject.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);

            }
        }
    }

   
}
