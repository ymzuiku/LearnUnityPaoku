using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
  public static PlayerController2 single;
  public float[] xList = new float[3] { -1.7f, 0f, 1.7f };
  public float runSpeed = 2.0f;
  float lastRunSpeed = 1f;
  // Use this for initialization
  MouseDirection inputDirection;
  Vector3 mousePos;
  bool activeInput = false;
  int standPosition;
  public Vector3 xDirection;
  public float nowLine;
  public float xMoveSpeed = 5.0f;
  public float jumpValue = 5f;
  public float gravity = 20f;
  public int canJumpNumber = 2;
  public int nowJumpNumber = 0;
  public Vector3 moveDircetion;
  float quickMoveTime = 3f;
  public float nowQuickMoveTime = 0;
  bool isQuickMove = false;
  CharacterController characterController;

  void Start()
  {
    single = this;
    standPosition = 1;
    characterController = GetComponent<CharacterController>();
    StartCoroutine(UpdateAction());
  }

  IEnumerator UpdateAction()
  {
    while (true)
    {
      GetInputDirection();
      MoveLeftRight();
      MoveForward();
      yield return 0;
    }
  }

  void GetInputDirection()
  {
    inputDirection = MouseDirection.NULL;
    if (Input.GetMouseButtonDown(0))
    {
      activeInput = true;
      mousePos = Input.mousePosition;
    }
    if (Input.GetMouseButton(0) && activeInput)
    {
      Vector3 vec = Input.mousePosition - mousePos;
      // vec.normalized 向量归一
      // Vector2.up 方向向上的向量
      if (vec.magnitude > 20)
      {
        var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.up)) * Mathf.Rad2Deg;
        var angleX = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.right)) * Mathf.Rad2Deg;
        if (angleY <= 45)
        {
          inputDirection = MouseDirection.up;
        }
        else if (angleY >= 135)
        {
          inputDirection = MouseDirection.down;
        }
        else if (angleX <= 45)
        {
          inputDirection = MouseDirection.right;
        }
        else if (angleX >= 135)
        {
          inputDirection = MouseDirection.left;
        }
        activeInput = false;
      }

    }
  }

  void MoveLeftRight()
  {
    if (inputDirection == MouseDirection.left && standPosition != 0)
    {
      standPosition -= 1;
      GetComponent<Animation>().Stop();
      AnimeManager.single.animationHandler = AnimeManager.single.PlayTurnLeft;
      xDirection = Vector3.left * xMoveSpeed;
    }
    else if (inputDirection == MouseDirection.right && standPosition != xList.Length - 1)
    {
      standPosition += 1;
      GetComponent<Animation>().Stop();
      AnimeManager.single.animationHandler = AnimeManager.single.PlayTurnRight;
      xDirection = Vector3.right * xMoveSpeed;
    }

    nowLine = xList[standPosition];
    // 由左往右移动
    if (xDirection.x > 0 && transform.position.x > nowLine)
    {
      xDirection = Vector3.zero;
      Vector3 p = transform.position;
      p.x = nowLine;
      transform.position = p;
    }
    // 由右往左移动
    else if (xDirection.x < 0 && transform.position.x < nowLine)
    {
      xDirection = Vector3.zero;
      Vector3 p = transform.position;
      p.x = nowLine;
      transform.position = p;
    }
  }

  void MoveForward()
  {
    if (inputDirection == MouseDirection.down)
    {
      AnimeManager.single.animationHandler = AnimeManager.single.PlayRool;
    }
    // 如果玩家在地面,并且不在滚动和转身,就播放跑步动画
    if (characterController.isGrounded)
    {
      nowJumpNumber = 0;
      if (AnimeManager.single.animationHandler != AnimeManager.single.PlayRool &&
      AnimeManager.single.animationHandler != AnimeManager.single.PlayTurnLeft &&
      AnimeManager.single.animationHandler != AnimeManager.single.PlayTurnRight
      )
      {
        AnimeManager.single.animationHandler = AnimeManager.single.PlayRun;
      }
      if (inputDirection == MouseDirection.up)
      {
        Debug.Log("aaa");
        JumpUp();
      }
    }
    else
    {
      if (inputDirection == MouseDirection.down)
      {
        QuickGround();
      }
      else if (inputDirection == MouseDirection.up)
      {
        if (canJumpNumber > nowJumpNumber)
        {
          JumpDouble();
        }
      }

      // 因为执行顺序的问题, AnimeManager.single可能是空的,可以去修改执行顺序
      // 如果玩家在空中,当不播放PlayJumpUp时就播放PlayJumpLoop,
      if (AnimeManager.single.animationHandler != AnimeManager.single.PlayJumpUp &&
      AnimeManager.single.animationHandler != AnimeManager.single.PlayRool &&
      AnimeManager.single.animationHandler != AnimeManager.single.PlayJumpDouble
      )
      {
        AnimeManager.single.animationHandler = AnimeManager.single.PlayJumpLoop;
      }
    }
  }
  void QuickGround()
  {
    moveDircetion.y -= jumpValue * 2;
  }
  public void QuickMove()
  {
    nowQuickMoveTime = 0;
    if (isQuickMove == false)
    {
      isQuickMove = true;
      lastRunSpeed = runSpeed;
      runSpeed += 20;
    }

  }
  void _updateQuickMove()
  {
    if (isQuickMove == true)
    {
      nowQuickMoveTime += Time.deltaTime;
      if (nowQuickMoveTime > quickMoveTime)
      {
        isQuickMove = false;
        runSpeed = lastRunSpeed;
      }
    }
  }

  void JumpDouble()
  {
    nowJumpNumber += 1;
    AnimeManager.single.animationHandler = AnimeManager.single.PlayJumpDouble;
    if (moveDircetion.y < 0)
    {
      moveDircetion.y = 0;
    }
    moveDircetion.y += jumpValue * 1.2f;
  }
  void JumpUp()
  {
    nowJumpNumber += 1;
    AnimeManager.single.animationHandler = AnimeManager.single.PlayJumpUp;
    if (moveDircetion.y < 0)
    {
      moveDircetion.y = 0;
    }
    moveDircetion.y += jumpValue;
  }
  // Update is called once per frame
  void Update()
  {
    _updateQuickMove();
    moveDircetion.z = runSpeed;
    moveDircetion.y -= gravity * Time.deltaTime;
    characterController.Move((moveDircetion + xDirection) * Time.deltaTime);
  }
}


public enum MouseDirection
{
  NULL,
  left,
  right,
  up,
  down,
}