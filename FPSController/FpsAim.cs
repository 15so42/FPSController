using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsAim : MonoBehaviour {
    [Header("灵敏度")]
    public float hSensitivity=10;
    public float vSensitivity=10;

    public GameObject HeadCamera;

    float horAngle;
    float verAngle;
    float mouseX;
    float mouseY;
    bool fired;
    float timer=0;
    Vector2 currentPos=Vector2.zero;
    
    
    Vector2 totalOffset;
    float horTotalOff;
    float verTotalOff;
    float horCurrent;
    float verCurrent;
	// Use this for initialization
	void Start () {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
        horAngle = transform.localEulerAngles.y;
        verAngle = transform.localEulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(totalOffset);
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        horAngle += mouseX * hSensitivity * Time.deltaTime;

        //压枪
        if (totalOffset.y > 0 && mouseY < 0)
        {
            totalOffset.y += mouseY * vSensitivity * Time.deltaTime;
        }
        else
        {
            verAngle += mouseY * vSensitivity * Time.deltaTime;
        }


        if (fired)//开了一枪,跳动
        {
            //horCurrent = Mathf.Lerp(horCurrent, horTotalOff, Time.deltaTime * 10);
            currentPos = Vector2.Lerp(currentPos, totalOffset, Time.deltaTime*10);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                //currentPos = Vector2.zero;
                fired = false;
            }
            
        }
        else//回位
        {
            //Debug.Log(currentPos.magnitude);
           if(totalOffset.magnitude>0.01)
            {
                Debug.Log("回");
                currentPos = Vector2.Lerp(totalOffset, Vector2.zero, Time.deltaTime );
                totalOffset = currentPos;
            }
            else
            {
                totalOffset = Vector2.zero;
                currentPos = Vector2.zero;
            }
           
        }

        //verAngle = Mathf.Clamp(verAngle+currentPos.y, -90, 90);
        transform.localRotation = Quaternion.Euler(0, horAngle+currentPos.x, 0);
        HeadCamera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(-(verAngle+currentPos.y),-90,90), 0, 0);

    }
    public void ShootUp(Vector2 offset,float time)//time是两次开枪的间隔,如果不是两次连续开枪就开始回位
    {
        // horTotalOff += offset.x;
        //verTotalOff += offset.y;
        
        totalOffset += offset;
       
        fired = true;
        timer = time;
    }
}
