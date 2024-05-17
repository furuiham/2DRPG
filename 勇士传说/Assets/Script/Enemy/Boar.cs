using UnityEngine;

public class Boar : Enemy {

   public override void Move() {
      base.Move();
      anim.SetBool("walk", true);
   }
}