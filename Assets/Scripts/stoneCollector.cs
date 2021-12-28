using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneCollector : MonoBehaviour
{
    [SerializeField] GameObject stoneVFX;
  private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.tag == "Player"){

            Instantiate(stoneVFX, transform.position, Quaternion.identity);
            audio_Manager.instance.Play("key");
          if (this.gameObject.CompareTag("Stone1")){
            PlayerPrefs.SetInt("Stone1",1);
                UIManager.instance.UpdateStoneUI();
                Destroy(gameObject);
            }
          if(this.gameObject.CompareTag("Stone2")){
            PlayerPrefs.SetInt("Stone2",1);
                UIManager.instance.UpdateStoneUI();
                Destroy(gameObject);
            }
          if(this.gameObject.CompareTag("Stone3")){
            PlayerPrefs.SetInt("Stone3",1);
                UIManager.instance.UpdateStoneUI();
                Destroy(gameObject);
            }
          if(this.gameObject.CompareTag("Stone4")){
                GameObject.FindObjectOfType<Boss>().stoneCollected = true;
                Destroy(gameObject);
          } 
      }
  }
}
