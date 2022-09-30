using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StudyUI : MonoBehaviour
{
    public GameObject myImage;
    public Toggle myToggle;
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        //myToggle.interactable = false;
        myButton.onClick.AddListener(MoveUpImage);
        myButton.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowImage(bool v)
    {
        myImage.SetActive(v);
    }
    
    public void MoveUpImage()
    {
        myImage.transform.Translate(Vector3.up * 30.0f);
    }
}
