using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public Mesh lootMesh;
    public Collider BoxCollider;
    public string lootName;
    public string lootTag;
    public int dropChance;
    public Vector3 colliderSize;
    public Vector3 colliderCenter; 


    public Loot(string lootName, int dropChance) { 
    
        this.lootName = lootName;
        this.dropChance = dropChance;
        
    }
}
