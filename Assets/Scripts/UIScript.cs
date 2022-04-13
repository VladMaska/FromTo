using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIScript : MonoBehaviour {

    public GameObject gameOverPanel;
    
    public Player _player;
    public GameObject TextGameObject;
    public GameObject sliderGameObject;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private Text _text;
    private Slider _slider;
    private float time;

    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void Start(){
        
        _text = TextGameObject.gameObject.GetComponent<Text>();
        _slider = sliderGameObject.GetComponent<Slider>();

    }

    private void Update(){
        
        time += Time.deltaTime;

        if ( time >= 1 )
            Minus();
        
        _text.text = $"Speed: { (int)( _player.speed ) }\nDistance: { (int)_player.GetDistance() }\nAverage Speed: {(int)_player.averageSpeed}";

        if ( _slider.value != 0 ) return;
        gameOverPanel.SetActive( true );
        _player.changeSpeed = 0;

    }
    
    /*––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––*/

    private void Minus(){

        if ( _player.zone ){
            
            if ( _player.speed >= 7 )
                _slider.value -= 1f;
        
            else if ( _player.speed >= 9 && _player.zone )
                _slider.value -= 2f;

        }
        else
        {
            
            if ( _player.speed < 7 )
                _slider.value -= 1f;
        
            else if ( _player.speed > 7 )
                _slider.value -= 2f;
            
            else if ( _player.speed >= 9 )
                _slider.value -= 4f;
            
        }

        time = 0;
        
    }

}