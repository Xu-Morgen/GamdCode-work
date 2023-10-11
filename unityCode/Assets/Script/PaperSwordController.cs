using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSwordController : MonoBehaviour
{

    public SignPaper PaperSword= new ("剑符",5,"射出一道飞剑");//初始化剑符咒
    private bool iscollide = false;
    void OnTriggerEnter2D(Collider2D other){
        PlayerController controller = other.GetComponent<PlayerController>();
        if(controller !=null && !iscollide){
            controller.AddSignPaper(PaperSword);//将剑符添加到玩家的符咒列表中
            Debug.Log("获得剑符");
            iscollide = true;
            Destroy(gameObject);//销毁剑符图标
        }
    }
}
