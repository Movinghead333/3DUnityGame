using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttacher : MonoBehaviour
{
    public Transform rightHand;

    private GameObject swordPrototype;

    private GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        swordPrototype = Resources.Load("Weapons/onehander_sword") as GameObject;
        currentWeapon = Instantiate(swordPrototype, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon.transform.position = rightHand.transform.position;
        currentWeapon.transform.rotation = rightHand.transform.rotation;
    }
}
