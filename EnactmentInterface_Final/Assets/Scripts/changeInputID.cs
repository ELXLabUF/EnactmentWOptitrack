using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeInputID : MonoBehaviour {

    public int currentValue = 0;
    public string currentString = "0";
    public InputField inputID;


    /*Functions for inputting the participant's ID number on first screen after selecting 'New Story'*/
    /*Allows for a range of 0-99*/

    public void addOne() {
        if (inputID.text != "" && currentValue < 99) { currentValue++; }
        else {currentValue = 0; }
        inputID.text = currentValue.ToString();
    }

    public void minOne() {
        if (inputID.text != "" && currentValue > 0) { currentValue--; }
        else { currentValue = 0; }
        inputID.text = currentValue.ToString();
    }

    public void endEditID(InputField input) {
        if (input.text != "") { currentValue = int.Parse(input.text); }
    }
}
