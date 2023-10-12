using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerJumpForce = 10.0f; // 角色跳跃力量
    public float PlayerSpeed = 3.0f; // 角色移动速度
    public float PlayerMaxHealth = 100.0f;//角色最大生命值
    public int PlayerMaxJump = 2; // 角色最大跳跃次数 
    private Rigidbody2D PlayerRigid; // 角色刚体 
    private BoxCollider2D PlayerFeet; // 角色脚部碰撞箱
    private int PlayerJumpTime; //记录角色能跳跃的次数
    private float PlayerCurrentHealth; // 角色当前生命值
    private List<SignPaper> PlayerSignPaperList = new List<SignPaper>();//创建一个角色拥有的符咒的列表
    public GameObject SignPaperForShootPrefab;//一个符咒实体

    private void Start()
    {
        
        PlayerCurrentHealth = PlayerMaxHealth;//赋予角色当前生命值为最大值
        PlayerJumpTime = PlayerMaxJump;//赋予角色当前跳跃次数为最大条约次数
        PlayerRigid = GetComponent<Rigidbody2D>(); // 获取角色刚体
        PlayerFeet = GetComponent<BoxCollider2D>();//获取角色脚步碰撞箱
    }

    private void Update()
    {
        // 检测跳跃输入同时跳跃次数大于1则触发跳跃
        if (PlayerJumpTime>1 && Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
        //检测地面接触
        if(CheckGround()){
            PlayerJumpTime = PlayerMaxJump;
        }
        //检测到c输入时报告当前符咒列表内符咒的数量
        if (Input.GetKeyDown(KeyCode.M)){
            Debug.Log(PlayerSignPaperList.Count);
        }
        if(Input.GetKeyDown(KeyCode.C)){
            UseSighPaper();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    //玩家跳跃
    private void PlayerJump()
    {
        // 应用跳跃力量
        PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x, PlayerJumpForce);
        //减少跳跃次数
        PlayerJumpTime--;
    }
    //玩家移动
    private void PlayerMove()
    {
        //检测键盘左右输入
        float horizontalInput = Input.GetAxis("Horizontal");

        // 根据水平输入来计算速度
        Vector2 movement = new Vector2(horizontalInput * PlayerSpeed, PlayerRigid.velocity.y);

        // 设置刚体的速度
        PlayerRigid.velocity = movement;
    }

    //若角色人物的脚部碰撞箱触碰地面则返回true
    private bool CheckGround(){
        return PlayerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
   //更改生命值
    public void ChangeHealth(int amount)
    {
        PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth + amount, 0, PlayerMaxHealth);//更改角色生命值，变化量为amount，不能高于或低于极值
    }
    //添加符咒
    public void AddSignPaper(SignPaper ReceiveSignpaper){
        PlayerSignPaperList.Add(ReceiveSignpaper);
    }
    //使用符咒（主要是攻击和射击）
    public void UseSighPaper(){
        GameObject SignPaperForShoot = Instantiate(SignPaperForShootPrefab, PlayerRigid.position + Vector2.up * 0.5f, Quaternion.identity);
        if(SignPaperForShoot.GetComponent<SignPaperController>()!=null){
            SignPaperController SignPaper = SignPaperForShoot.GetComponent<SignPaperController>();
            SignPaper.PlayerSignPaper(Vector2.right, 300);
        }
        else{
            Debug.Log(SignPaperForShoot.GetComponent<SignPaperController>());
        }

    }

}
