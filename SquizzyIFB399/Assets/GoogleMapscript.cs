using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoogleMapscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    Scene Current_Scene = SceneManager.GetActiveScene();
    string scene_name = Current_Scene.name;
    //works in start but not in function
    if(scene_name == "RomaStParklandsMap"){
        Application.OpenURL("https://www.google.com/maps/place/Roma+Street+Parkland/@-27.4624483,153.018816,17z/data=!3m1!4b1!4m5!3m4!1s0x6b915a01f6a82779:0x6b3cecc042d084b8!8m2!3d-27.4624483!4d153.018816");
    }
    if(scene_name =="BotanicGardensMap"){
        Application.OpenURL("https://www.google.com/maps/place/Brisbane+City+Botanic+Gardens/@-27.4759597,153.0254849,16z/data=!3m1!4b1!4m5!3m4!1s0x6b915a108730630b:0xf02a35bd721b830!8m2!3d-27.4757456!4d153.0300037");  
    }
    if(scene_name == "NewFarmParkMap"){
        Application.OpenURL("https://www.google.com/maps/place/New+Farm+Park/@-27.4689447,153.049541,17z/data=!3m1!4b1!4m5!3m4!1s0x6b915a2819cfb5bf:0xc749b5e56decac75!8m2!3d-27.4689447!4d153.0517297");
    }
    if(scene_name == "KangarooPointMap"){
        Application.OpenURL("https://www.google.com/maps/place/Kangaroo+Point+Park/@-27.4765847,153.0341638,18z/data=!3m1!4b1!4m5!3m4!1s0x6b915b18e16f568b:0x15538d94aef30099!8m2!3d-27.4765847!4d153.035227");
    }
    if(scene_name == "MtCoothaGardens"){
        Application.OpenURL("https://www.google.com/maps/place/Mount+Coot-tha+Botanic+Gardens,+152+Mount+Coot-Tha+Rd,+Mount+Coot-Tha+QLD+4066/@-27.4774245,152.9716162,16z/data=!3m1!4b1!4m5!3m4!1s0x6b9150e7a8bf60f1:0x778f9da1b8de8b7f!8m2!3d-27.4763118!4d152.9750264");
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
