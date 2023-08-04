using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Picturesque.Darkbringer
{
    public class DarkbringerUIPreview : MonoBehaviour {

        // Use this for initialization
        public DarkbringerEffect dbe;

        public Toggle resToggle;
        public InputField rW, rH, strW, strH;

        public Toggle onebitDiToggle;
        public Toggle ColDi;

        public Slider dBleed, dShift;

        public Toggle palette;
        public Dropdown pOpts;

        public Texture2D[] pals;

        void Awake()
        {
            resToggle.isOn = dbe.CustomScreenSize;

            //rW.text = dbe.newScreenSize.x.ToString();// = System.Convert.ToInt32(rW.text.ToString());
            //rH.text = dbe.newScreenSize.y.ToString();

            //strW.text = dbe.screenStretching.x.ToString();
           // strH.text = dbe.screenStretching.y.ToString(); //System.Convert.ToSingle(strH.text.ToString());

            onebitDiToggle.isOn = dbe.OneBitColor;
            ColDi.isOn = dbe.Dithering;

            dBleed.value = dbe.ColorBleed;
            dShift.value = dbe.ColorShift;

            palette.isOn = dbe.Paletting;

            pals[pOpts.value] = dbe.flatLut;

        }


        public void UpdateParams()
        {
           // Debug.Log(rW.text);
            dbe.CustomScreenSize = resToggle.isOn;

            switch(pOpts.value)
            {
                case 0:
                    dbe.newScreenSize = new Vector2(160, 200);
                    dbe.screenStretching = new Vector2(2, 1);
                    break;
                case 1:
                    dbe.newScreenSize = new Vector2(256, 240);
                    dbe.screenStretching = new Vector2(1, 1);
                    break;
                case 2:
                    dbe.newScreenSize = new Vector2(160, 144);
                    dbe.screenStretching = new Vector2(1, 1);
                    break;
                case 3:
                    dbe.newScreenSize = new Vector2(320, 200);
                    dbe.screenStretching = new Vector2(1, 1);
                    break;
                case 4:
                    dbe.newScreenSize = new Vector2(320, 224);
                    dbe.screenStretching = new Vector2(1, 1);
                    break;

            }

            //dbe.newScreenSize.x = System.Convert.ToInt32(rW.text.ToString());
           // dbe.newScreenSize.y = System.Convert.ToInt32(rH.text.ToString());

            //dbe.screenStretching.x = System.Convert.ToSingle(strW.text.ToString());
           // dbe.screenStretching.y = System.Convert.ToSingle(strH.text.ToString());

            dbe.OneBitColor = onebitDiToggle.isOn;
            dbe.Dithering = ColDi.isOn;

            dbe.ColorBleed = dBleed.value;
            dbe.ColorShift = dShift.value;

            dbe.Paletting = palette.isOn;

            dbe.flatLut = pals[pOpts.value];

        }
        

        // Update is called once per frame
        void Update() {

        }
    }
}