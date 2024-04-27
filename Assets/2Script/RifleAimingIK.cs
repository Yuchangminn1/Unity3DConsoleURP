//using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

//public class RifleAimingIK : MonoBehaviour
//{
//    public Animator animator;          // Animator 컴포넌트
//    public Transform rightHandTarget;  // 소총의 오른손 핸들 위치
//    public Transform leftHandTarget;   // 소총의 왼손 핸들 위치
//    public Transform rifle;            // 소총 오브젝트
//    public Transform target;

//    void OnAnimatorIK(int layerIndex)
//    {
//        if (animator)
//        {
//            // 오른손 IK 활성화
//            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
//            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
//            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
//            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);

//            // 왼손 IK 활성화
//            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
//            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
//            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
//            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
//        }
//    }

//    void Update()
//    {
//        // 소총이 타겟을 향하도록 조준
//        rifle.LookAt(target);  // 여기서 Target은 소총 총구가 향해야 할 대상의 Transform
//    }
//}
using UnityEngine;

public class RifleAimingIK : MonoBehaviour
{
    public Animator animator;
    public Transform leftHandTarget;

    public Transform rifle;            // 소총 오브젝트
    public Transform target;

    public bool isRifleIK = false;

    void OnAnimatorIK(int layerIndex)
    {
        if (animator && isRifleIK)
        {
            

            // 왼손 설정
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

        }
    }

    

    void Update()
    {
        // 소총이 타겟을 향하도록 조준
        //rifle.LookAt(target);  // 여기서 Target은 소총 총구가 향해야 할 대상의 Transform
    }
}