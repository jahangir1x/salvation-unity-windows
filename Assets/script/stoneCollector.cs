using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneCollector : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.tag == "Player"){
         // Debug.Log("Player");
          if(this.gameObject.CompareTag("Stone1")){
            PlayerPrefs.SetInt("Stone1",1);
                Destroy(gameObject);
            }
          if(this.gameObject.CompareTag("Stone2")){
            PlayerPrefs.SetInt("Stone2",1);
                Destroy(gameObject);
            }
          if(this.gameObject.CompareTag("Stone3")){
            PlayerPrefs.SetInt("Stone3",1);
                Destroy(gameObject);
            }
          if(this.gameObject.CompareTag("Stone4")){
                GameObject.FindObjectOfType<Boss>().stoneCollected = true;
                Destroy(gameObject);
          } 
      }
  }
}
