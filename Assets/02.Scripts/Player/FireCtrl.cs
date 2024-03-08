using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCtrl : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem cartridgeEject;
    [SerializeField] private Image magImage;
    [SerializeField] private Text magTxt;
    [SerializeField] private Animator animator;

    Player_HumanoidCtrl playerCtrl;

    private float timePrev;
    private float fireRate = 0.1f;  // 발사 간격 시간
    private float ammo = 0f;
    private readonly float maxAmmo = 10.0f;
    private readonly int hashReload = Animator.StringToHash("reloadTrigger");
    private bool isReloading = false;

    void Start()
    {
        bulletPrefab = Resources.Load("Weapons/Bullet") as GameObject;
        firePos = transform.GetChild(0).GetChild(0).GetChild(0).transform;
        source = GetComponent<AudioSource>();
        fireClip = Resources.Load("Sounds/p_ak_1") as AudioClip;
        muzzleFlash = firePos.GetChild(0).GetComponent<ParticleSystem>();
        cartridgeEject = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
        playerCtrl = GetComponent<Player_HumanoidCtrl>();
        magImage = GameObject.Find("Magazine_Image").GetComponent<Image>();
        magTxt = GameObject.Find("Magazine_Text").GetComponent<Text>();
        animator = GetComponent<Animator>();
        muzzleFlash.Stop();
        cartridgeEject.Stop();
        ammo = maxAmmo;
        ammo = Mathf.Clamp(ammo, 0f, 10f);
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time - timePrev > fireRate)
        {
            if (playerCtrl.isRun || isReloading) return;
                Fire();
                timePrev = Time.time;
            if (ammo <= 0)
            {
                StartCoroutine(Reloading());
            }
        }
        else
        {
            muzzleFlash.Stop();
            cartridgeEject.Stop();
        }
    }
    void Fire()
    {
        --ammo;
        source.PlayOneShot(fireClip, 1.0f);
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        muzzleFlash.Play();
        cartridgeEject.Play();
        magazineTextShow();
        magImage.fillAmount = ammo / maxAmmo;
    }

    private void magazineTextShow()
    {
        magTxt.text = "<color=#ff0000>" + ammo.ToString() + "</color>" + " / " + maxAmmo.ToString();
    }
    IEnumerator Reloading()
    {
        magazineTextShow();
        isReloading = true;
        animator.SetTrigger(hashReload);
        yield return new WaitForSeconds(1.5f);
        magImage.fillAmount = 1.0f;
        isReloading= false;
        ammo = maxAmmo;
        magazineTextShow();
    }
}
