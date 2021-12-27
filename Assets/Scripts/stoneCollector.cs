using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneCollector : MonoBehaviour
{
    [SerializeField] GameObject stoneVFX;
  private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.tag == "Player"){

            Instantiate(stoneVFX, transform.position, Quaternion.identity);

          if (this.gameObject.CompareTag("Stone1")){
            PlayerPrefs.SetInt("Stone1",1);
                Debug.LogError(PlayerPrefs.GetInt("Stone1"));
                Debug.LogError("huhi");
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
