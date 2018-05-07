using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeManager : MonoBehaviour
{

  public delegate void AnimationHandler();

	public static AnimeManager single;

  public AnimationClip Dead;
  public AnimationClip JumpDown;
  public AnimationClip JumpLoop;
  public AnimationClip JumpUp;
  public AnimationClip Rool;
  public AnimationClip Run;
  public AnimationClip TurnLeft;
  public AnimationClip TurnRight;
	int needRoolNumber = 2;
	int nowRoolNumber = 0;

  public AnimationHandler animationHandler;
  //[Assembly-CSharp, Assembly-CSharp-Editor]
  Animation animation;
  // Use this for initialization
  void Start()
  {
		single = this;
    animation = GetComponent<Animation>();
		animationHandler = PlayRun;
  }

  public void PlayDead()
  {
    animation.Play(Dead.name);
  }
	public void PlayJumpDown()
  {
    animation.Play(JumpDown.name);
  }
	public void PlayJumpUp()
  {
    animation.Play(JumpUp.name);
		if(animation[JumpUp.name].normalizedTime > 0.95f){
			animationHandler = PlayRun;
		}
  }
	public void PlayJumpDouble(){
		animation.Play(Rool.name);
		if(animation[Rool.name].normalizedTime > 0.95f){
				animationHandler = PlayJumpLoop;
		}
	}
	public void PlayJumpLoop()
  {
    animation.Play(JumpLoop.name);
  }
	public void PlayRool()
  {
    animation.Play(Rool.name);
		if(animation[Rool.name].normalizedTime > 0.95f){
			nowRoolNumber ++ ;
			if(nowRoolNumber >= needRoolNumber){
				nowRoolNumber = 0;
				animationHandler = PlayRun;
			} else {
				animation.Play(Run.name);
				animationHandler = PlayRool;
			}
		}
  }
	public void PlayRun()
  {
    animation.Play(Run.name);
  }
	public void PlayTurnLeft()
  {
    animation.Play(TurnLeft.name);
		if(animation[TurnLeft.name].normalizedTime > 0.95f){
			animationHandler = PlayRun;
		}
  }
	public void PlayTurnRight()
  {
    animation.Play(TurnRight.name);
		if(animation[TurnRight.name].normalizedTime > 0.95f){
			animationHandler = PlayRun;
		}
  }

  // Update is called once per frame
  void Update()
  {
    if (animationHandler != null)
    {
      animationHandler();
    }
  }
}
