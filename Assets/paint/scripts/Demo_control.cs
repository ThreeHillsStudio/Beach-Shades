using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyGameStudio.paint
{
    public class Demo_control : MonoBehaviour
    {
        public AudioSource audio_source;
        public AudioClip ka;

        public GameObject[] game_objects;
   
        public string[] titles;
        public Text text_title;

        private int index = 0;

        public static Demo_control instance;

        [NonSerialized]
        public GameObject currentActiveGameObject;

        [NonSerialized] public Quaternion startRotation;

        public bool isAutoRotate;

        void Start()
        {
            this.index = 0;
            currentActiveGameObject = game_objects[this.index];
            foreach (var obj in game_objects)
            {
                if (obj.activeSelf)
                {
                    currentActiveGameObject = obj;
                    index = Array.IndexOf(game_objects, obj);
                    break;
                }
            }
            transform.LookAt(Vector3.zero);
            instance = this;
            currentActiveGameObject.GetComponent<Rotate_self>().is_auto_rotate = isAutoRotate;
            currentActiveGameObject.GetComponent<Paint>().on_reset_btn();
        }

        public void on_previous_btn()
        {
            this.index--;
            if (this.index <= -1)
                this.index = (this.game_objects.Length - 1);


            for (int i = 0; i < this.game_objects.Length; i++)
            {
                if (i == this.index)
                {
                    this.game_objects[i].SetActive(true);
                    currentActiveGameObject = this.game_objects[i];
                }
                else
                {
                    this.game_objects[i].SetActive(false);
                }
            }

            this.text_title.text = this.titles[this.index];
            this.audio_source.PlayOneShot(this.ka);
            currentActiveGameObject.GetComponent<Rotate_self>().is_auto_rotate = isAutoRotate;
        }

        public void on_next_btn()
        {
            this.index++;
            if (this.index >= this.game_objects.Length)
                this.index = 0;

            for (int i = 0; i < this.game_objects.Length; i++)
            {
                if (i == this.index)
                {
                    this.game_objects[i].SetActive(true);
                    currentActiveGameObject = this.game_objects[i];
                }
                else
                {
                    this.game_objects[i].SetActive(false);
                }
            }

            this.text_title.text = this.titles[this.index];
            this.audio_source.PlayOneShot(this.ka);
            currentActiveGameObject.GetComponent<Rotate_self>().is_auto_rotate = isAutoRotate;
        }
    }
}
