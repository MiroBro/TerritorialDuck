using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToggler : MonoBehaviour
{
    public GameObject standardUI;

    public GameObject upgradeUI;
    public Transform upgradeParent;
    public GameObject upgradeBtn;

    public GameObject bagUpgradeUi;
    public Transform bagUpgradeParent;
    public GameObject bagUpgradeBtn;

    public GameObject introUI;
    public GameObject giveUpUI;
    public GameObject settingsUI;

    public GameObject showNewItemUI;
    public Image newItemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;

    private CoroutineQueue queue;

    // --To make ui open one after another
    public static bool timeSensitiveClosed;

    public GameObject uiGlitter;

    public GameObject[] uiDucks;

    public GameObject dailyChoiceUI;

    public GameObject buildUi;

    public GameObject chaseUi;

    private void DisableAllUi()
    {
        dailyChoiceUI.SetActive(false);
        buildUi.SetActive(false);
        introUI.SetActive(false);
        giveUpUI.SetActive(false);
        bagUpgradeUi.SetActive(false);
        upgradeBtn.SetActive(false);
        //standardUI.SetActive(false);
    }

    public void EnableChaseUi()
    {
        DisableAllUi();
        chaseUi.SetActive(true);
    }

    public void EnableBuildUi()
    {
        DisableAllUi();
        buildUi.SetActive(true);
    }

    public void OpenChoiceUi()
    {
        DisableAllUi();
        dailyChoiceUI.SetActive(true);
        
    }


    private void EnableRandomUiDuck()
    {
        foreach (var duck in uiDucks)
        {
            duck.SetActive(false);
        }

        int rand = Random.Range(0, uiDucks.Length);
        uiDucks[rand].SetActive(true);
    }

    private void ToggleGlitter(bool state)
    {
        uiGlitter.SetActive(state);
    }

    void Start()
    {
        // use "this" monobehaviour as the coroutine owner
        timeSensitiveClosed = true;
        queue = new CoroutineQueue(this);
        queue.StartLoop();
    }


    private bool IsSensitiveUIClosed()
    {
        //Debug.Log(timeSensitiveClosed);
        return timeSensitiveClosed;
    }

    IEnumerator OpenTimeSensitiveUI(System.Action openMethod)
    {
        yield return new WaitUntil(IsSensitiveUIClosed);
        openMethod();
        yield return new WaitUntil(IsSensitiveUIClosed);
    }
    // --END - To make ui open one after another

    public void OpenBagUpgradeUi()
    {
        //StartCoroutine(OpenTimeSensitiveUI(() => OpenBagUpgradeUiP()));
        queue.EnqueueAction(OpenTimeSensitiveUI(() => OpenBagUpgradeUiP()));
    }

    private void OpenBagUpgradeUiP()
    {
        timeSensitiveClosed = false;
        References.Instance.soundHandler.PlayClickBtn();
        References.Instance.soundHandler.SilenceMusic();
        //References.Instance.soundHandler.PlayLevelUpSound();
        References.Instance.soundHandler.PlayFabricSound();

        DeleteAllBtns(bagUpgradeParent);

        SpawnUpgradeBtns(3, bagUpgradeBtn, bagUpgradeParent);
        bagUpgradeUi.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenUpgradeUI()
    {
        queue.EnqueueAction(OpenTimeSensitiveUI(() => OpenUpgradeUiP()));
        //StartCoroutine(OpenTimeSensitiveUI(() => OpenUpgradeUiP()));
    }

    private void OpenUpgradeUiP()
    {
        ToggleGlitter(true);
        //timeSensitiveClosed = false;
        timeSensitiveClosed = false;

        //Debug.Log("this open upgrade is called!");
        References.Instance.soundHandler.PlayClickBtn();
        References.Instance.soundHandler.SilenceMusic();

        DeleteAllBtns(upgradeParent);


        SpawnUpgradeBtns(3, upgradeBtn, upgradeParent);
        upgradeUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseUpgraedUI()
    {
        ToggleGlitter(false);
        References.Instance.soundHandler.PlayClickBtn();
        References.Instance.soundHandler.EnableMusic();
        upgradeUI.SetActive(false);
        bagUpgradeUi.SetActive(false);
        DeleteAllBtns(upgradeParent);
        DeleteAllBtns(bagUpgradeParent);
        Time.timeScale = 1;
        timeSensitiveClosed = true;

        References.Instance.soundHandler.StopLvlUpSound();
    }

    public void OpenNewItemDisplay(Sprite iconImage, string name, string desc)
    {
        queue.EnqueueAction(OpenTimeSensitiveUI(() => OpenNewItemDisplayP(iconImage, name, desc)));
    }

    private void OpenNewItemDisplayP(Sprite iconImage, string name, string desc)
    {
        timeSensitiveClosed = false;
        References.Instance.soundHandler.PlayNewItem();
        References.Instance.soundHandler.SilenceMusic();
        //standardUI.SetActive(false);
        newItemIcon.sprite = iconImage;
        itemName.text = name;
        itemDesc.text = desc;
        EnableRandomUiDuck();
        showNewItemUI.SetActive(true);
        Time.timeScale = 0;
    }



    public void CloseNewItemDisplay()
    {
        //References.Instance.soundHandler.StopLvlUpSound();
        References.Instance.soundHandler.StopPlayNewItem();
        EnableStandardUI();
        timeSensitiveClosed = true;
    }


    private void DeleteAllBtns(Transform parent)
    {
        if (instBtns.Count != 0)
        {
            foreach (var btn in instBtns)
            {
                Destroy(btn.gameObject);
            }
        }


        /*
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }*/
    }

    private List<GameObject> instBtns = new();
    private void SpawnUpgradeBtns(int amountOfUpgrades, GameObject upgradeBtn, Transform upgradeParent)
    {
        var allEffects = AllEffects.GetRandomEffects(amountOfUpgrades);
        foreach (var effect in allEffects)
        {
            if (effect.Key != Enums.PowerUpEffect.Default)
            {
                var inst = Instantiate(upgradeBtn, upgradeParent);
                inst.SetActive(true);
                var btnEffect = inst.GetComponent<BtnAddBallEffect>();
                btnEffect.SetName(effect.Value.effectName);
                btnEffect.SetDescription(effect.Value.description);
                btnEffect.SetImage(effect.Value.icon);
                btnEffect.SetEffect(effect.Key);

                instBtns.Add(inst);
            }
        }
    }


    //---------------------------------------------------------- REGULAR UI BELOW
    public void EnableStandardUI()
    {
        References.Instance.soundHandler.EnableMusic();
        showNewItemUI.SetActive(false);
        dailyChoiceUI.SetActive(false);
        standardUI.SetActive(true);
        Time.timeScale = 1;
    }


    //Close functions are often called from the buttons in the editor
    public void CloseSettings()
    {
        References.Instance.soundHandler.PlayClickBtn();
        settingsUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenSettings()
    {
        References.Instance.soundHandler.PlayClickBtn();
        settingsUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenIntroUI()
    {
        dailyChoiceUI.SetActive(false);
        introUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseIntroUI()
    {
        References.Instance.soundHandler.PlayClickBtn();
        introUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenGiveUp()
    {
        giveUpUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseGiveUp()
    {
        References.Instance.soundHandler.PlayClickBtn();
        giveUpUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToggleSettingsUI()
    {
        if (settingsUI.activeSelf)
        {
            CloseSettings();
        }
        else
        {
            OpenSettings();
        }
    }

}
