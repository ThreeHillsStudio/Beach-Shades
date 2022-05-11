using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace EasyGameStudio.paint
{
    public class Paint : MonoBehaviour
    {
        [Header("Render texture, Used to be drawn")]
        public RenderTexture render_texture_paint;
        public RenderTexture render_texture_erase;

        [Header("Texture Brush")]
        public Texture texture_brush;

        [Header("Thickness")]
        public float brush_thickness = 1; //Brush thickness, 

        [Header("Canvas")]
        public Canvas canvas;

        [Header("Image Pen, pen and erase sprite")]
        //public Image image_cursor;
        public Sprite sprite_pen;
        public GameObject spray;

        [Header("Color Paint, Color array, color btns")]
        public Color[] color_array;
        public Button[] btns_color_choose;

        [Header("Audio Source, Audio Clip")]
        public AudioSource audio_source;
        public AudioClip audio_clip_btn;

        [Header("Transparent Material, Used to achiveve the erase function")]
        public Material mat_brush_erase;

        [Header("Transparent Material, Used to achiveve the paint function")]
        public Material mat_brush_paint;   //color choose paint on paint render texture
        public Material mat_brush_paint_1; //color black paint on erase render texture

        private int _layerMask;

        void OnEnable()
        {
            _layerMask = LayerMask.GetMask("Background");
           // this.image_cursor.sprite = this.sprite_pen;

            //choose color
            for (int i = 0; i < this.btns_color_choose.Length; i++)
            {
                if (0 == i)
                {
                    // this.btns_color_choose[i].transform.GetChild(0).gameObject.SetActive(true);
                    var x = btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition.x;
                    btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x, 120, -3.48f);


                    this.mat_brush_paint.SetColor("_color", this.color_array[i]);
                    Spray.instance.SetColor(color_array[i]);
                }
                else
                {
                    var x = btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition.x;
                    btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x, 77, -3.48f);

                    //this.btns_color_choose[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            //set thickness
            //this.image_cursor.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32) + new Vector2(brush_thickness * 5, brush_thickness * 5) * 20;


            //set brush
            this.mat_brush_paint.SetTexture("_texture", this.texture_brush);
            this.mat_brush_paint_1.SetTexture("_texture", this.texture_brush);
        }

        void Update()
        {
            if (SceneController.instance.currentScene != 1) return;
            
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch (0).fingerId) && !MouseRotation.instance.isOverCylinder)
            {
                if (Spray.instance.isSprayActive == false)
                {
                    SceneController.instance.AudioSource.clip = SceneController.instance.spraySound;
                    SceneController.instance.AudioSource.loop = true;
                    SceneController.instance.AudioSource.Play();
                    
                }
                Spray.instance.isSprayActive = true;
                
                
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit1;
                if (Physics.Raycast(ray1, out hit1, Mathf.Infinity, _layerMask))
                {
                    if (hit1.collider.gameObject.tag != "paint") MoveSpray(hit1);
                }

                var screenPoint = Camera.main.WorldToScreenPoint(Spray.instance.sprayPosition.transform.position);
               // RaycastHit hit;
               //var isRay = Physics.Raycast(Spray.instance.sprayPosition.transform.position, Spray.instance.sprayPosition.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
            
               Ray ray = Camera.main.ScreenPointToRay(screenPoint);
               RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "paint")
                    {
                        int x = (int)(hit.textureCoord.x * this.render_texture_paint.width);
                        int y = (int)(this.render_texture_paint.height - hit.textureCoord.y * this.render_texture_paint.height);
                        
                        this.paint(x, y, this.render_texture_paint, this.render_texture_erase, this.texture_brush, this.brush_thickness, this.mat_brush_paint, this.mat_brush_paint_1);
                        
                    }
                    
                }

                return;
            }

            //show the cursor
            if (Cursor.visible == false) Cursor.visible = true;
           // if (this.image_cursor.gameObject.activeSelf == true) this.image_cursor.gameObject.SetActive(false);
           if (Spray.instance.isSprayActive == true)
           {
               Spray.instance.isSprayActive = false;
               SceneController.instance.AudioSource.Stop();
           }
            //if( spray && spray.activeSelf) spray.SetActive(false);
        }

        public void MoveSpray(RaycastHit hit)
        {
            if (spray)
            {
                spray.SetActive(true);
                spray.transform.position = new Vector3(hit.point.x, hit.point.y - 4, 4);// hit.point - Vector3.back * 2f + Vector3.down * 0.5f;
                //spray.transform.LookAt(hit.point);
                spray.transform.rotation = Quaternion.Euler(20, 180f, spray.transform.rotation.z);
            }
        }
        //clear the rendertexture
        public void clear(RenderTexture render_texture_erase, RenderTexture render_texture_paint)
        {
            RenderTexture.active = render_texture_erase;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = null;

            RenderTexture.active = render_texture_paint;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = null;
        }
        
        //paint
        private void paint(int x, int y, RenderTexture render_texture_paint, RenderTexture render_texture_erase, Texture texture_brush, float brush_thickness, Material mat_brush_paint, Material mat_brush_paint_1)
        {
            RenderTexture.active = render_texture_paint;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, render_texture_paint.width, render_texture_paint.height, 0);

            //set position
            x -= (int)(texture_brush.width * 0.5f * brush_thickness);
            y -= (int)(texture_brush.height * 0.5f * brush_thickness);

            //paint on render_texture_paint
            Rect rect = new Rect(x, y, texture_brush.width * brush_thickness, texture_brush.height * brush_thickness);
            Graphics.DrawTexture(rect, texture_brush, mat_brush_paint, 0);
            GL.PopMatrix();


            //paint on render_texture_erase---------------------------------------------------------------------------------------------------------------------------
            RenderTexture.active = render_texture_erase;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, render_texture_erase.width, render_texture_erase.height, 0);
            Graphics.DrawTexture(rect, texture_brush, mat_brush_paint_1, 0);
            GL.PopMatrix();
            RenderTexture.active = null;
        }

        //ui event
        #region 

        public void on_reset_btn()
        {
            this.audio_source.PlayOneShot(this.audio_clip_btn);
            this.clear(this.render_texture_erase,this.render_texture_paint);
        }
        public void on_color_choose_btn(int index)
        {
            this.audio_source.PlayOneShot(this.audio_clip_btn);

            for (int i = 0; i < this.btns_color_choose.Length; i++)
            {
                if (index == i)
                {
                    //this.btns_color_choose[i].transform.GetChild(0).gameObject.SetActive(true);
                    var x = btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition.x;
                    btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x, 120, -3.48f);

                    this.mat_brush_paint.SetColor("_color", this.color_array[i]);
                    Spray.instance.SetColor(color_array[i]);
                }
                else
                {
                    var x = btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition.x;
                    btns_color_choose[i].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x, 77, -3.48f);
                    //this.btns_color_choose[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        #endregion
    }

}
