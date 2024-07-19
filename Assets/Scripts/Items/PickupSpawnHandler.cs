using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnHandler : MonoBehaviour
{
    public Transform itemParent;

    //Leaves
    //private static float leaf1SpawnRate = 0.80f;
    private static float leaf2SpawnRate = 0.1f;
    private static float leaf3SpawnRate = 0.06f;
    private static float leaf4SpawnRate = 0.04f;

    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject leaf3;
    public GameObject leaf4;

    //Patience
    //private static float nailpolishSpawnRate = 0.7f;
    private static float rainbowPolishSpawnRate = 0.2f;
    private static float teaSpawnRate = 0.1f;

    public GameObject nailpolish;
    public GameObject rainbowPolish;
    public GameObject tea;

    //Loot
    private static float dogBagSpawnRate = 0.2f;
    //private static float catBagSpawnRate = 0.8f;

    public GameObject catBag;
    public GameObject dogBag;

    //Pickup Changer
    private static float cloverSpawnRate = 0.5f;
    //private static float dinoEggSpawnRate = 0.5f;

    public GameObject clover;
    public GameObject dinoEgg;

    //Status Effects
    //private static float vacuumSpawnRate = 0.7f;
    private static float leafBlowerSpawnRate = 0.3f;

    public GameObject vacuum;
    public GameObject leafBlower;

    //Annoyance Effects
    private static float megaphoneSpawnRate = 0.34f;
    private static float romanceNovelSpawnRate = 0.33f;
    //private static float beansSpawnRate = 0.33f;

    public GameObject megaphone;
    public GameObject novel;
    public GameObject bean;

    //Bread
    //private static float breadSpawnRate = 0.98f;
    private static float loafSpawnRate = 0.02f;

    public GameObject bread;
    public GameObject loaf;


    private void Start()
    {
        SortedList<int, GameObject> droppableItems = new SortedList<int, GameObject>()
                                    {
                                        {3,megaphone},
                                        {5, novel},
                                        {1, bean}
                                    };
    }

    public void SpawnPickup(Vector3 pos)
    {
        float rand = Random.Range(0f, 1f);

        if (rand < 0.8)
        {
            SpawnBread(pos);
        }
        else if (rand < 0.9)
        {
            SpawnLeaf(pos);
        }
        else if (rand < 0.92)
        {
            SpawnPatience(pos);
        }
        else if (rand < 0.94)
        {
            SpawnLoot(pos);
        }
        else if (rand < 0.97)
        {
            SpawnStatusEffect(pos);
        }
        else if (rand < 0.98)
        {
            SpawnEnemyEffect(pos);
        }
        else
        {
            SpawnItemEffect(pos);
        }
    }

    private void SpawnEnemyEffect(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < megaphoneSpawnRate)
            inst = Instantiate(megaphone, pos, bread.transform.rotation, itemParent);
        else if (rand < romanceNovelSpawnRate + megaphoneSpawnRate)
            inst = Instantiate(novel, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(bean, pos, bread.transform.rotation, itemParent);
    }

    private void SpawnLeaf(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < leaf4SpawnRate)
            inst = Instantiate(leaf4, pos, bread.transform.rotation, itemParent);
        else if (rand < leaf3SpawnRate)
            inst = Instantiate(leaf3, pos, bread.transform.rotation, itemParent);
        else if (rand < leaf2SpawnRate)
            inst = Instantiate(leaf2, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(leaf1, pos, bread.transform.rotation, itemParent);
    }

    private void SpawnPatience(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < teaSpawnRate)
            inst = Instantiate(tea, pos, bread.transform.rotation, itemParent);
        else if (rand < rainbowPolishSpawnRate)
            inst = Instantiate(rainbowPolish, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(nailpolish, pos, bread.transform.rotation, itemParent);
    }

    private void SpawnLoot(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < dogBagSpawnRate)
            inst = Instantiate(dogBag, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(catBag, pos, bread.transform.rotation, itemParent);
    }

    private void SpawnBread(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < loafSpawnRate)
            inst = Instantiate(loaf, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(bread, pos, bread.transform.rotation, itemParent);
    }


    private void SpawnStatusEffect(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < cloverSpawnRate)
            inst = Instantiate(clover, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(dinoEgg, pos, bread.transform.rotation, itemParent);
    }


    private void SpawnItemEffect(Vector3 pos)
    {
        var rand = Random.Range(0f, 1f);

        GameObject inst;

        if (rand < leafBlowerSpawnRate)
            inst = Instantiate(leafBlower, pos, bread.transform.rotation, itemParent);
        else
            inst = Instantiate(vacuum, pos, bread.transform.rotation, itemParent);
    }
}








/*
private void Drop()
{
    System.Random generator = new System.Random();
    const int probabilityWindow = 30;
    int randomChance = generator.Next(0, 100);

    if (randomChance < probabilityWindow)
    {
        // spawn
    }
}*/




/*
     public enum PickupEffects
{
    Leaf1,
    Leaf2,
    Leaf3,
    Leaf4,
    NailPolish,
    RainbowPolish,
    Tea,
    DogBag,
    CatBag,
    Bread,
    Loaf,
    Clover,
    Megaphone,
    Beans,
    RomanceNovel,
    DinoEggs,
    Vaccuum,
    LeafBlower,

}

 */