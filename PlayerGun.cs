using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    private WeaponInfo myWeaponInfo;

    public GameObject gun;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;

    [SerializeField] float grenadeThrowStrength = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Vector3 startPos = GetComponentInChildren<Camera>().transform.position;
            Vector3 direction = GetComponentInChildren<Camera>().transform.forward;
            Ray ray = new Ray(startPos, direction);

            float range = 3000.0f;
            switch (myWeaponInfo.weaponType)
            {
                case WeaponType.Laser:

                    if (Physics.Raycast(ray, out hit, range))
                    {
                        print(hit.collider.gameObject.name);

                        Vector3 localBarrelOffset = GetComponentInChildren<Camera>().transform.rotation * myWeaponInfo.barrelEndOffset;

                        Debug.DrawLine(gun.transform.position + localBarrelOffset, hit.point, Color.magenta, 5.0f);
                        gun.GetComponent<AudioSource>().Play();

                        if (hit.collider.gameObject.name == "DoorControlPanel")
                        {
                            GameObject.FindObjectOfType<Door>().isActive = true;
                        }

                    }
                    break;
                case WeaponType.Bullet:
                    GameObject bullet = Instantiate(bulletPrefab, GameObject.Find("BulletMother").transform);
                    //Bullet position
                    bullet.transform.position = gun.transform.position + myWeaponInfo.barrelEndOffset;

                    //Bullet Rotation
                    Physics.Raycast(ray, out hit);
                    if(hit.collider != null)
                    {
                        bullet.transform.LookAt(hit.point);
                        print(" bullet is heading toward: " + hit.collider.name);
                    } else
                    {
                        bullet.transform.rotation = GetComponentInChildren<Camera>().transform.rotation;
                    }
                    break;
                case WeaponType.Grenade:
                    GameObject grenade = Instantiate(grenadePrefab, GameObject.Find("BulletMother").transform);
                    grenade.transform.position = gun.transform.position + myWeaponInfo.barrelEndOffset;

                    grenade.GetComponent<Rigidbody>().AddForce(GetComponentInChildren<Camera>().transform.forward * grenadeThrowStrength);
                    break;
                default:
                    //some code
                    break;
            }
        }
    }

    public void SetWeaponInfo(WeaponInfo weaponInfo)
    {
        myWeaponInfo = weaponInfo;
        print("My Weapon is: " + myWeaponInfo.weaponName);
        SpawnGun();
    }

    private bool SpawnGun()
    {
        gun.GetComponent<MeshFilter>().mesh = myWeaponInfo.weaponMesh;
        gun.GetComponent<MeshRenderer>().material = myWeaponInfo.weaponMaterial;
        return true;
    }
}
