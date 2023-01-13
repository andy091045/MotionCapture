using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCalibration : MonoBehaviour
{
    public GameObject gameobject;
    //Start is called before the first frame update
    void Start()
    {
        //gameobject.transform.rotation = Quaternion.Euler(ReadTxt.Instance.total[0][0],ReadTxt.Instance.total[0][1],ReadTxt.Instance.total[0][2]);
        //gameobject.transform.position = new Vector3(ReadTxt.Instance.total[1][0],ReadTxt.Instance.total[1][1],ReadTxt.Instance.total[1][2]);
    }

    
    void Update()
    {     
            if(gameobject.tag == "Camera0"){
                gameobject.transform.rotation = Quaternion.Euler(ReadTxt.Instance.total[0][0],ReadTxt.Instance.total[0][1],ReadTxt.Instance.total[0][2]);
                gameobject.transform.position = new Vector3(ReadTxt.Instance.total[1][0],ReadTxt.Instance.total[1][1],ReadTxt.Instance.total[1][2]);
             }else if(gameobject.tag == "Camera1"){
                gameobject.transform.rotation = Quaternion.Euler(ReadTxt.Instance.total[2][0],ReadTxt.Instance.total[2][1],ReadTxt.Instance.total[2][2]);
                gameobject.transform.position = new Vector3(ReadTxt.Instance.total[3][0],ReadTxt.Instance.total[3][1],ReadTxt.Instance.total[3][2]);
             }
        }
        
    }

