using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyGameStudio.paint
{
    public class Rotate_self : MonoBehaviour
    {
        [Header("rotate speed")]
        public float auto_speed;

        [Header("speed")]
        public float speed;

        [Header("is auto rotate")]
        public bool is_auto_rotate;

        [Header("is auto rotate")]
        //public Toggle toggle;

        [Header("X, Y, Z")]
        public int x = 0;
        public int y = -1;
        public int z = 0;

         [Header("rotate speed")]

        private bool is_left_rotate;
        private bool is_right_rotate;

        float num = 0;
        // Update is called once per frame
        void Update()
        {
            if (this.is_auto_rotate)
            {
                num += Time.deltaTime * this.auto_speed;

                if (x == -1)
                {
                    transform.Rotate(Vector3.right, Time.deltaTime * auto_speed);
                }
                else if (y == -1)
                {
                    transform.Rotate(Vector3.back, Time.deltaTime * auto_speed);
                }
                else if(z==-1)
                {
                    transform.Rotate(Vector3.down, Time.deltaTime * auto_speed);
                }
                
            }
            else
            {
                if (this.is_left_rotate)
                {
                    if (x == -1)
                    {
                        transform.Rotate(Vector3.right, Time.deltaTime * speed);
                       
                    }
                    else if (y == -1)
                    {
                        transform.Rotate(Vector3.back, Time.deltaTime * speed);
                    }
                    else if (z == -1)
                    {
                        transform.Rotate(Vector3.down, Time.deltaTime * speed);
                    }
                }
                else if (this.is_right_rotate)
                {
                    if (x == -1)
                    {
                        transform.Rotate(Vector3.right, Time.deltaTime * speed);
                    }
                    else if (y == -1)
                    {
                        transform.Rotate(Vector3.forward, Time.deltaTime * speed);
                    }
                    else if (z == -1)
                    {
                        transform.Rotate(Vector3.up, Time.deltaTime * speed);
                    }
                }
            }

            /*if (this.toggle.isOn == true)
            {

                this.is_auto_rotate = true;
            }
            else
            {
                this.is_auto_rotate = false;
            }*/

        }


        public void pointer_left_down()
        {
            this.is_left_rotate = true;
        }

        public void pointer_left_up()
        {
            this.is_left_rotate = false;
        }

        public void pointer_right_down()
        {
            this.is_right_rotate = true;
        }

        public void pointer_right_up()
        {
            this.is_right_rotate = false;
        }

    }
}
