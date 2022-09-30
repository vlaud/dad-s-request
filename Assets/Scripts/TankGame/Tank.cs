using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    /// <summary>
    /// 탱크 상단
    /// </summary>
    public Transform myTop;
    /// <summary>
    /// 포신
    /// </summary>
    public Transform myBarrel;
    /// <summary>
    /// 포신 제어각
    /// </summary>
    public Vector2 BarrelRange = new Vector2(0.0f, 60.0f);
    /// <summary>
    /// 발사 위치
    /// </summary>
    public Transform myMuzzle;
    /// <summary>
    /// 폭탄 파일
    /// </summary>
    public GameObject orgBomb;
    public float MoveSpeed = 1.0f;
    public float RotateSpeed = 90.0f;
    public float TopRotate = 45.0f;
    public float MuzzleSpeed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs\\Target")) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(orgBomb, myMuzzle);
            obj.GetComponent<Bomb>().OnFire();
            
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
           
            transform.Rotate(Vector3.down * RotateSpeed * Time.deltaTime);
           
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myTop.Rotate(Vector3.down * TopRotate * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myTop.Rotate(Vector3.up * TopRotate * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {

            myBarrel.Rotate(Vector3.forward * MuzzleSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            myBarrel.Rotate(Vector3.back * MuzzleSpeed * Time.deltaTime);

        }
        Vector3 rot = myBarrel.localRotation.eulerAngles;
        
        if (rot.z > 180.0f)
        {
           
            rot.z -= 360.0f;
        }

        //Debug.Log(rot.z);
        /*
        if(rot.z < 0.0f)
        {
            rot.z = 0.0f;
        }
        if (rot.z > 60.0f)
        {
            rot.z = 60.0f;
        }*/
        rot.z = Mathf.Clamp(rot.z, BarrelRange.x, BarrelRange.y);
        myBarrel.localRotation = Quaternion.Euler(rot);
    }
}
