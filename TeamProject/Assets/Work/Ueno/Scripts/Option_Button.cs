using UnityEngine;
using System.Collections;

public class Option_Button : MonoBehaviour
{
    float ButtonWidth = 150;
    float ButtonHeight = 100;


    bool Option = false;
    bool Return = false;


    void OnGUI()
    {
        if (Return == false)
        {
            //Option
            if (GUI.Button(new Rect(Screen.width - 50,
                                    0,
                                    50, 50), "Option"))
            {
                Option = true;
                Return = true;

                Time.timeScale = 0;
            }
        }


        if (Option)
        {
            //Restart
            if (GUI.Button(new Rect(Screen.width / 2 - ButtonWidth / 2 + 100 ,
                                    Screen.height / 2, 
                                    ButtonWidth, ButtonHeight), "Restart"))
            {
                Option = false;

                Time.timeScale = 1;

                Application.LoadLevel("StageTest");
            }


            //Stage Select
            if (GUI.Button(new Rect(Screen.width / 2 - ButtonWidth / 2 - 100,
                                    Screen.height / 2,
                                    ButtonWidth, ButtonHeight), "Stage Select"))
            {
                Option = false;

                Time.timeScale = 1;

                Application.LoadLevel("StageSelect");
            }


            //Return
            if (GUI.Button(new Rect(Screen.width - 50,
                                    0,
                                    50, 50), "Return"))
            {
                Option = false;
                Return = false;

                Time.timeScale = 1;
            }
        }
    }

}
