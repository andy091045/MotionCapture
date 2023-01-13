using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTxt : MonoBehaviour
{
    public static ReadTxt Instance;
    string text;
    public List<List<float>> total = new List<List<float>>();

    private void Awake() {
        
        Instance = this;
        var textFile = Resources.Load<TextAsset>("Text/calibration_data");
        text = textFile.ToString();
        ParseFile(text);
    }

    void ParseFile(string text)
    {       
        char[] separators = { '(' };
        char[] separators2 = { ']','['};
        string[] strValues = text.Split(separators);

        
        int i = 2;
        while(i<=8){
            string[] strValues2 = strValues[i].Split(separators2);
            List<float> a  = new List<float>();
            float x = float.Parse(strValues2[2]);
            a.Add(x);
            float y = float.Parse(strValues2[4]);
            a.Add(y);
            float z = float.Parse(strValues2[6]);
            a.Add(z);
            total.Add(a);
            i+=2;
        }
        for (int j = 0; j < 4; j++)
        {
            for (int b = 0; b < 3; b++)
            {
                Debug.Log(total[j][b]);
            }
            
        }
    }
}


