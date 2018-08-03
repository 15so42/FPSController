using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
    [Header("======配置======")]
    public Text bulletCount;
    public Text MaxbulletCount;
    public int maxCarryAmmo;
    public int maxMagAmmo;
    public float upForce=0.8f;
    public float leftForce=0.8f;
    [Header("每分钟子弹数")]
    public int fireRate=900;
    [Header("======音效======")]
    public AudioClip fireClip;
    public AudioClip ReloadClip1;
    public AudioClip ReloadClip2;
    public AudioClip ReloadClip3;
    public AudioClip NoBulletClip;
    [Header("======HitFx=====")]
    public GameObject[] hitFx;
    [Header("组件")]
    public Shooter shooter;



    int curCarryAmmo;
    int curMagAmmo;
    GameObject mainCamera;
    GameObject Player;
    Animator anim;
    AudioSource ad;
    float lastFireTime=0;
    bool fire;
    bool reloading;
    bool noBullet=true;


	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main.gameObject;
        anim = GetComponent<Animator>();
        ad = GetComponent<AudioSource>();
        curCarryAmmo = maxCarryAmmo;
        curMagAmmo = maxMagAmmo;
        lastFireTime = Time.time;
        UpdateUI();
	}

    RaycastHit hit;
    Ray fireRay;
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            fire = true;
        if (Input.GetMouseButtonUp(0))
        {
            fire = false;
            noBullet = true;
        }
        
        if(fire&&lastFireTime+(float)60/fireRate<=Time.time)
        {
            if (curMagAmmo == 0 && noBullet)
            {
                ad.PlayOneShot(NoBulletClip);
                noBullet = false;
            }
            if (curMagAmmo > 0)
            {
                ad.PlayOneShot(fireClip);
                anim.SetTrigger("Fire");
                
                shooter.Shoot();
                lastFireTime = Time.time;
                curMagAmmo -= 1;
                //后坐力
                float rx = Random.Range(-leftForce, leftForce);
                float ry = Random.Range(0,upForce);
                //Debug.Log(rx + "," + ry);
                
                FireRay();
                Player.GetComponent<FpsAim>().ShootUp(new Vector2(rx, ry), (float)60 / fireRate);
                UpdateUI();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Show");
        }
        //换弹
        
    }
    void UpdateUI()
    {
        bulletCount.text = curMagAmmo.ToString();
        MaxbulletCount.text = curCarryAmmo.ToString();
    }  
    void Reload()
    {
       

        if (curMagAmmo < maxMagAmmo && curCarryAmmo > 0 && fire == false)
        {
           
            anim.SetTrigger("Reload");
            reloading = true;
            
            
            
        }
    }
    public void Reload1()
    {
        ad.PlayOneShot(ReloadClip1);
    }
    public void Reload2()
    {
        ad.PlayOneShot(ReloadClip2);
    }
    public void Reload3()
    {
        ad.PlayOneShot(ReloadClip3);
        if (curCarryAmmo > 0)
        {
            if (curCarryAmmo > maxMagAmmo)
            {
                curCarryAmmo -= maxMagAmmo;
                curCarryAmmo += curMagAmmo;
                curMagAmmo = maxMagAmmo;

            }
            else
            {
                curMagAmmo = curCarryAmmo;
                curCarryAmmo = 0;
            }
        }
        UpdateUI();
        reloading = false;
        noBullet = true;
    }
   

    void FireRay()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if(Physics.Raycast(ray,out hit,1000))
        {
            
            if (hit.collider.gameObject == transform.parent.parent.gameObject)
            {
                return;
            }
            else
            {

                GameObject fx = Instantiate(hitFx[0], hit.point, Quaternion.identity);
                GameObject hole =Instantiate(hitFx[1], hit.point, Quaternion.identity);
                fx.transform.forward = -transform.forward;
                
                hole.transform.forward = -transform.forward;
                hole.transform.SetParent(hit.collider.transform);
                if (hit.collider.tag == "Enemy")
                {
                    
                    Destroy(hit.collider.gameObject);
                }
            }
           
        }
    }


}
