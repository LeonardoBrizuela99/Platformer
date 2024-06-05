using UnityEngine;
 
[CreateAssetMenu(fileName = "NewPlayerSpeed", menuName = "Custom/Player Speed")]
public class PlayerSpeed : ScriptableObject
{

    [SerializeField] public float speed;
   
}
