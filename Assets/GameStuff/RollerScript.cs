using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof (Rigidbody))]
public class RollerScript : MonoBehaviourPun
{

    [SerializeField] int rotationSpeed = 1;
    [SerializeField] int rotationMultiplyer = 2000;
    [SerializeField] string diceName;
    [SerializeField] Slider slider = null;
    int diceCount = 0;
    bool rolled = false;

    private float sliderValue = 0;
    [SerializeField] float sliderFillSpeed = 1f;
    private bool sliderIsFilling = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine) return;

        
        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            StartCoroutine("Roll");
        }else if(GetComponent<Rigidbody>().velocity == Vector3.zero && rolled)
        {
            CheckDieValue();
        }
    }

    private void CheckDieValue()
    {
        //Debug.Log(Vector3.Dot(transform.forward, Vector3.up));
        if (Vector3.Dot(transform.forward, Vector3.up) > 0.6f)
            diceCount = 1;
        if (Vector3.Dot(-transform.forward, Vector3.up) > 0.6f)
            diceCount = 6;
        if (Vector3.Dot(transform.up, Vector3.up) > 0.6f)
            diceCount = 5;
        if (Vector3.Dot(-transform.up, Vector3.up) > 0.6f)
            diceCount = 2;
        if (Vector3.Dot(transform.right, Vector3.up) > 0.6f)
            diceCount = 3;
        if (Vector3.Dot(-transform.right, Vector3.up) > 0.6f)
            diceCount = 4;
        Debug.Log($"{diceName} Rolled: {diceCount}");
        rolled = false;
    }

    private void FixedUpdate() {
        if(GetComponent<Rigidbody>().velocity != Vector3.zero) return;

        if (sliderIsFilling)
        {
            sliderValue += sliderFillSpeed * Time.deltaTime;
            slider.value = sliderValue;

            if (sliderValue >= slider.maxValue)
            {
                sliderIsFilling = false;
            }
        }
        else
        {
            sliderValue -= sliderFillSpeed * Time.deltaTime;
            slider.value = sliderValue;

            if (sliderValue <= slider.minValue)
            {
                sliderIsFilling = true;
            }
        }
    }

    IEnumerator Roll(){
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(12, 15), Random.Range(-2.5f, 2.5f));
        if(slider.value >= 0.475f && slider.value <= 0.525f){
            GetComponent<BoxCollider>().enabled = true;
        }else{
            GetComponent<BoxCollider>().enabled = false;
        }

        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-rotationSpeed, rotationSpeed + 1) * rotationMultiplyer,
                                                         Random.Range(-rotationSpeed, rotationSpeed + 1)  * rotationMultiplyer, 
                                                         Random.Range(-rotationSpeed, rotationSpeed + 1)  * rotationMultiplyer));
        rolled = true;
    }
}
