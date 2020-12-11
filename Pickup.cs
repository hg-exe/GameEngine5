using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Laser,
    Bullet,
    Grenade
}

public struct WeaponInfo
{
    public string weaponName;
    public int damage;
    public WeaponType weaponType;
    public Mesh weaponMesh;
    public Material weaponMaterial;
    public Vector3 barrelEndOffset;
}

public class Pickup : MonoBehaviour
{
    public WeaponType weaponType;
    public WeaponInfo myWeaponInfo;

    void Start()
    {
        AssignWeaponInfo();
    }

    void AssignWeaponInfo()
    {
        myWeaponInfo.weaponType = weaponType;
        myWeaponInfo.weaponMesh = GetComponent<MeshFilter>().mesh;
        myWeaponInfo.weaponMaterial = GetComponent<MeshRenderer>().materials[0];
        myWeaponInfo.weaponName = gameObject.name;
        switch (weaponType)
        {
            case WeaponType.Laser:
                myWeaponInfo.damage = 5;
                myWeaponInfo.barrelEndOffset = new Vector3(0, 0.089f, 0.305f);
                break;
            case WeaponType.Bullet:
                myWeaponInfo.damage = 10;
                myWeaponInfo.barrelEndOffset = new Vector3(0, 0.106f, 0.2668f);
                break;
            case WeaponType.Grenade:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Player has walked into pickup");
            other.gameObject.GetComponent<PlayerGun>().SetWeaponInfo(myWeaponInfo);
        }
    }

}
