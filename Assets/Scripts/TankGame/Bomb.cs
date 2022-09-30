using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ �߻� �ý���
/// </summary>
public class Bomb : MonoBehaviour
{
    /// <summary>
    /// �����̽� Ű Ȯ��
    /// </summary>
    bool bFire = false;
    public LayerMask crashMask;
    public float MoveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bFire)
        {
            float delta = MoveSpeed * Time.deltaTime;
            Ray ray = new Ray(transform.position, transform.up);
            if(Physics.Raycast(ray, out RaycastHit hit, delta, crashMask))
            {
                Invoke("CreateTarget", 2.0f);
                hit.transform.GetComponent<Target>()?.OnDelete();
            }
            transform.Translate(Vector3.up * delta);
        }
    }

    public void OnFire()
    {
        bFire = true;
        //transform.parent = null;
        transform.SetParent(null);
    }

    /// <summary>
    /// ������ �浹 �Ͼ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        bFire = false;
        Destroy(this.gameObject);
        //if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        /*
        if((crashMask & 1 << collision.gameObject.layer) != 0)
        {
            Destroy(collision.gameObject);
        }*/
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }

    /// <summary>
    /// �������� ������ �ѹ�
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if ((crashMask & 1 << other.gameObject.layer) != 0)
        {
            //Invoke("CreateTarget", 2.0f);
            //other.GetComponent<Target>().OnDelete();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }

    void CreateTarget()
    {
        Destroy(gameObject);
        GameObject obj = Instantiate(Resources.Load("Prefabs\\Target")) as GameObject;
    }
    /// <summary>
    /// �������� ���
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
       
    }
}
