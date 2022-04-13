using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using NaughtyAttributes;

public class Player : MonoBehaviour {

    [HorizontalLine]
    
    [BoxGroup("Other")] public float speed;
    [BoxGroup("Other")] public float changeSpeed;
    [BoxGroup("Other")] public bool zone;
    
    [HorizontalLine]
    
    [BoxGroup("For Average Speed")] public Vector3 startPoint;
    [BoxGroup("For Average Speed")] public float time;
    [BoxGroup("For Average Speed")] public float averageSpeed;
    [BoxGroup("For Average Speed")] public float _distance;

    [HorizontalLine]
    
    [BoxGroup("UI")] public GameObject warningPanel;
    [BoxGroup("UI")] public GameObject winPanel;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/
    
    public static bool warning;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private Transform _transform;
    private float _warningTime;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void Awake() => _transform = this.gameObject.GetComponent<Transform>();

    private void Start() => startPoint = _transform.position;

    private void Update(){

        if ( speed > 0 ){ time += Time.deltaTime; _distance = GetDistance(); }
        else if ( speed == 0 ){ time = 0; averageSpeed = 0; _distance = 0; }

        Warning();

        UpSpeed();
        DownSpeed();
        
        Scroll();
        
        Stop();
        
        if ( speed > 9 )
            speed += Time.deltaTime * -5;

        if ( changeSpeed != 0 )
            speed += Time.deltaTime * changeSpeed;
        
        else if ( changeSpeed == 0 && speed > 0 )
            speed += Time.deltaTime * -1;

        averageSpeed = _distance / time;

        transform.Translate( -speed * Time.deltaTime, 0, 0 );

    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void Warning(){

        if ( speed > 5 && averageSpeed < 5 )
            _warningTime += Time.deltaTime;
        
        if ( _warningTime >= 5 && warning ) 
            warningPanel.SetActive( true );
    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void Stop(){
        
        if ( !( speed < 0 ) ) return;
        
        speed = 0;
        time = 0;
        averageSpeed = 0;
        startPoint = gameObject.transform.position;
        _distance = 0;

    }

    private void Scroll(){
        
        if ( Input.GetAxisRaw( "Mouse ScrollWheel" ) > 0 ){
            NewPoint();
            speed += 2;
        }
        else if ( Input.GetAxisRaw( "Mouse ScrollWheel" ) < 0 ){
            NewPoint();
            speed -= 2;
        }

    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void UpSpeed(){
        
        if ( !Input.GetKeyDown( KeyCode.W ) || changeSpeed == 5 ) return;
        NewPoint();
        startPoint = _transform.position;
        time = 0;
        changeSpeed++;

    }

    private void DownSpeed(){
        
        if (!Input.GetKeyDown(KeyCode.S) || changeSpeed == 0) return;
        NewPoint();
        startPoint = _transform.position;
        time = 0;
        changeSpeed--;

    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    public float GetDistance(){ return  Vector3.Distance( _transform.position, startPoint ); }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void NewPoint() => startPoint = gameObject.transform.position;
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void OnTriggerEnter( Collider other ){
        
        switch ( other.name ){
            
            case "Zone":
                zone = true;
                break;
            
            case "Win":
                winPanel.SetActive( true );
                break;
            
        }
        
    }

    private void OnTriggerExit( Collider other ){
        
        if ( other.name == "Zone" )
            zone = false;
        
    }
    
}