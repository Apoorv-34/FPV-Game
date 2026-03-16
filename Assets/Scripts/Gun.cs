using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage;
    public float range;
    public float fireRate;
    
    [Header("Ammo Settings")]
    public int magSize;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    [Header("Scope Settings")]
    public float scopedFOV = 20f; 
    public float normalFOV = 60f;
    public float zoomSpeed = 10f;
    private bool isScoped = false;
    public GameObject scopeOverlay; // Drag your Scope Image from Canvas here

    [Header("References")]
    public Camera fpsCam; 
    private Transform firePoint; 
    private Animator animator; // Reference to the Animator component

    [Header("New UI References")]
    public TextMeshProUGUI currentAmmoText; 
    public TextMeshProUGUI maxAmmoText;     

    private float nextTimeToFire = 0f;
    private string currentGunName;

    void Start()
    {
        animator = GetComponent<Animator>();
        EquipWeapon();
    }

    void EquipWeapon()
    {
        StopAllCoroutines();
        isReloading = false;

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        WeaponData data = WeaponSelectionManager.SelectedWeapon;

        if (data != null)
        {
            damage = data.damage;
            range = data.range;
            fireRate = data.fireRate;
            magSize = data.magSize;
            scopedFOV = data.zoomFOV; 
            currentAmmo = magSize; 
            currentGunName = data.weaponName;

            if (data.gunPrefab != null)
            {
                GameObject newGun = Instantiate(data.gunPrefab, transform.position, transform.rotation * data.gunPrefab.transform.rotation, transform);                
                firePoint = newGun.transform.Find("Muzzle");
                
                if (firePoint == null)
                {
                    Debug.LogWarning("Muzzle point not found on prefab!");
                }
            }
            
            UpdateAmmoUI(); 
        }
    }

    void Update()
    {
        if (isReloading) return; 

        // Reload Logic
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize)
        {
            StartCoroutine(Reload());
            return;
        }

        // Shooting Logic
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (currentAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        // Scope Logic
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = true;
            StartCoroutine(OnScoped());
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isScoped = false;
            OnUnscoped();
        }

        float targetFOV = isScoped ? scopedFOV : normalFOV;
        fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    IEnumerator OnScoped()
    {
        // Small delay to let the animation start before showing overlay
        yield return new WaitForSeconds(0.15f);
        if(isScoped) // Check if still holding right click
        {
            if (scopeOverlay != null) scopeOverlay.SetActive(true);
            
            // Optional: Hide the weapon model when scoped to prevent clipping
            SetGunModelVisibility(false);
        }
    }

    void OnUnscoped()
    {
        if (scopeOverlay != null) scopeOverlay.SetActive(false);
        SetGunModelVisibility(true);
    }

    void SetGunModelVisibility(bool visible)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(visible);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        
        // Trigger Reload Animation
        if (animator != null) animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = magSize;
        UpdateAmmoUI(); 

        if (animator != null) animator.SetBool("isReloading", false);
        isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI(); 
        
        // Trigger Recoil Animation
        if (animator != null) animator.SetTrigger("isShooting");

        if (firePoint != null && WeaponSelectionManager.SelectedWeapon.muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(WeaponSelectionManager.SelectedWeapon.muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);
            Destroy(flash, 0.2f);
        }

        int layerMask = ~(1 << 6); 
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            ZombieAI zombie = hit.transform.GetComponent<ZombieAI>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }

    public void UpdateAmmoUI()
    {
        if (currentAmmoText != null)
        {
            currentAmmoText.text = currentAmmo.ToString();
        }

        if (maxAmmoText != null)
        {
            maxAmmoText.text = "/ " + magSize.ToString();
        }
    }
}