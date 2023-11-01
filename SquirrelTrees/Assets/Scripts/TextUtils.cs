using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUtils : MonoBehaviour
{
    private string acornSymbol = "<sprite name=\"Acorn_emoji_score\">";
    private string treeSymbol = "<sprite name=\"Tree_emoji_score\">";
    
    public string getAcornScoreText(int score){
        return acornSymbol + score;
    }
    public string getTreeScoreText(int score){
        return treeSymbol + score;
    }
    public string getScoreText(int acornScore, int treeScore, string delimiter = "\n"){
        return getAcornScoreText(acornScore) + 
            delimiter + 
            getTreeScoreText(treeScore);
    }
}
