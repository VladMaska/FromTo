using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WarningPanel : MonoBehaviour {

    public void CloseWindow(){

        Player.warning = false;
        this.gameObject.SetActive( false );
        
    }

    public void Again() => SceneManager.LoadScene( 0 );

}