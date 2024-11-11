using UnityEngine;
using UnityEngine.UI;
using System;

namespace AirFishLab.ScrollingList.Demo
{
    public class ListEventDemo : MonoBehaviour
    {
        [SerializeField]
        private CircularScrollingList _list;
        //[SerializeField]
        //private Text _selectedContentText;
        //[SerializeField]
        //private Text _requestedContentText;
        //[SerializeField]
        //private Text _autoUpdatedContentText;

        //get vaue color
        public string nameEvent; 
        private string _headColorEvent , _bodyColorEvent , _tailColorEvent;
        public Action<string> OnHeadColorEventChanged;
        public Action<string> OnBodyColorEventChanged;
        public Action<string> OnTailColorEventChanged;

        public string HeadColorEvent
        {
            get => _headColorEvent;
            private set
            {
                _headColorEvent = value;
                OnHeadColorEventChanged?.Invoke(_headColorEvent); // Trigger callback when colorEvent changes
            }
        }

        public string BodyColorEvent
        {
            get => _bodyColorEvent;
            private set
            {
                _bodyColorEvent = value;
                OnBodyColorEventChanged?.Invoke(_bodyColorEvent); // Trigger callback when colorEvent changes
            }
        }

        public string TailColorEvent
        {
            get => _tailColorEvent;
            private set
            {
                _tailColorEvent = value;
                OnTailColorEventChanged?.Invoke(_tailColorEvent); // Trigger callback when colorEvent changes
            }
        }



        public void DisplayFocusingContent()
        {
            var contentID = _list.GetFocusingContentID();
            var centeredContent =
                (IntListContent)_list.ListBank.GetListContent(contentID);
            //_requestedContentText.text =
            //    $"Focusing content: {centeredContent.Value}";
        }

        public void OnBoxSelected(ListBox listBox)
        {
            var content =
                (IntListContent)_list.ListBank.GetListContent(listBox.ContentID);
            //_selectedContentText.text =
            //    $"Selected content ID: {listBox.ContentID}, Content: {content.Value}";
           
        }

        public void OnFocusingBoxChanged(
            ListBox prevFocusingBox, ListBox curFocusingBox)
        {
            //_autoUpdatedContentText.text =
            //    "(Auto updated)\nFocusing content: "
            //    + $"{((IntListBox) curFocusingBox).Content}";

            // Debug.Log(curFocusingBox.GetComponent<Image>().color);
            string hexColor = ColorToHex(curFocusingBox.GetComponent<Image>().color);
            switch (nameEvent)
            {
                case "head":
                   // string hexColor1 = ColorToHex(curFocusingBox.GetComponent<Image>().color);
                    HeadColorEvent = hexColor;
                    break;

                case "body":
                    //string hexColor2 = ColorToHex(curFocusingBox.GetComponent<Image>().color);
                    BodyColorEvent = hexColor;
                    break;

                case "tail":
                    //string hexColor3 = ColorToHex(curFocusingBox.GetComponent<Image>().color);
                    TailColorEvent = hexColor;
                    break;

            }

           // if (nameEvent == "head")
           // {
           //     string hexColor = ColorToHex(curFocusingBox.GetComponent<Image>().color);
           //     HeadColorEvent = hexColor;
           // }
           //else if(nameEvent == "body")
           // {
           //     string hexColor = ColorToHex(curFocusingBox.GetComponent<Image>().color);
           //     BodyColorEvent = hexColor;
           // }
           //else if(nameEvent == "tail")
           // {
           //    string hexColor = ColorToHex(curFocusingBox.GetComponent<Image>().color);
           //     TailColorEvent = hexColor;
           // }
        }

        private string ColorToHex(Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        public void OnMovementEnd()
        {
           // Debug.Log("Movement Ends");
        }
    }
}
