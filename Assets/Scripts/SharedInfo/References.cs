using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    private static References _instance;

    public static References Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Camera mainCam;
    public SoundHandler soundHandler;
    public PickupSpawnHandler itemSpawnHandler;
    public PlayerLevelHandler levelHandler;
    public PlayerHealthHandler playerHealthHandler;
    //public EffectHandler effectHandler;
    public AttackHandler attackHandler;
    public UIToggler uiToggler;
    public PickupEffectsHandler pickupEffectHandler;
    public MoneyHandler moneyHandler;
    public ItemAssets itemAssets;
    public PlayerOutfitHandler duckOutfitHandler;
    public AttackAssets attackAssets;
    public EnemySpawnHandler enemySpawnHandler;

    public SceneStager sceneStager;
    public BuildHandler buildHandler;
    public BuildAssets buildAssets;

    public StatsForTesting statsForTesting;
}
