using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AmmoBar : VisulaBar
{
    PickupObject weopenPickUp;
    [SerializeField] GameObject[] weopenimageList;
    [SerializeField] TextMeshProUGUI ammotext;
    
    public  override void  Start()
    {
        base.Start();
        weopenPickUp = FindObjectOfType<PickupObject>();
        DisableAllWeopenImagesWhenNoWeopensPickedUp();
        SubscribeToNecessaryEventsAtStart(); 
    }
    /// <summary>
    /// this will make sure that whenever a gun is shot or switched the ammo bar will take the correct weopen values and update other ammo bar values at the right time .
    /// </summary>
    void SubscribeToNecessaryEventsAtStart()
    {
        weopenPickUp.WeopenHasBeenSwitched += SetNewCurrentValueAndMaxValueAndUpdateBar;
        weopenPickUp.newWeopenHasbeenEquipped += GetReferenceOfCurrentGunWhenWeopenSwitched_And_UpdateOtherAmmoBarValues;
        weopenPickUp.newWeopenHasbeenEquipped += UpdateAmmoTextAndColor;
    }

    void GetReferenceOfCurrentGunWhenWeopenSwitched_And_UpdateOtherAmmoBarValues()
    {
        GunClass currentWeopen = weopenPickUp.equipList[weopenPickUp.currentWeapon].GetComponent<GunClass>();

        currentWeopen.currentAmmoHasBeenChanged += DecreaseCircularBarValue;
        currentWeopen.currentAmmoHasBeenChanged += UpdateAmmoTextAndColor;

        UpdateCurrentWeopenImageWhenAWeopenSwitched();
    }

    void UpdateCurrentWeopenImageWhenAWeopenSwitched()
    {
        for(int i = 0; i < weopenimageList.Length; i++)
        {
            if(weopenPickUp.currentWeapon != i)
            {
                weopenimageList[i].SetActive(false);
            }
            if (weopenPickUp.currentWeapon == i)
            {
                weopenimageList[i].SetActive(true);
            }
        }
    }

    void DisableAllWeopenImagesWhenNoWeopensPickedUp()
    {
        if (weopenPickUp.equipList.Count == 0)
        {
            for (int i = 0; i < weopenimageList.Length; i++)
            {
                weopenimageList[i].SetActive(false);
            }
        }
    }

    // two variation are made so that it one can be compatiable with events and other can be compatible by passing in a  value 
    void UpdateAmmoTextAndColor(int currentAmmo )
    {
        ammotext.text = currentAmmo.ToString();
        ammotext.color = barGradient.Evaluate(currentAmmo / maxValue);
    }
    void UpdateAmmoTextAndColor()
    {
        ammotext.text = currentValue.ToString();
        ammotext.color = barGradient.Evaluate(currentValue / maxValue);
    }

}
