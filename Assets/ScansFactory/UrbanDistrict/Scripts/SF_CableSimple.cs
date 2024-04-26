namespace SF_CableSimple
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [ExecuteInEditMode]
    public class SF_CableSimple : MonoBehaviour
    {
        [Header("Cables tension:")]
        public float tensionFactor = 1f;
        public bool randomTension = true;

        private float defaultDistance = 20f;
        private float scaleFactor1 = 1f;

        [Header("Cables visibility:")]
        public bool Cable1Visibility = true;
        public bool Cable2Visibility = true;

        [Header("Base end points")]
        public GameObject StartPoint;
        public GameObject EndPoint;
        public GameObject CableMesh1;
        public GameObject CableMesh2;

        private bool propertiesChanged = false;

#if UNITY_EDITOR
        private Vector3 previousStartPosition;
        private Vector3 previousEndPosition;
        private float previoustension = 1f;

        // 추가: 속성 변경 감지 변수 선언

        private void OnEnable()
        {
            EditorApplication.update += UpdateInEditor;
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateInEditor;
        }

        private void UpdateInEditor()
        {
            if (!Application.isPlaying)
            {
                if (StartPoint.transform.position != previousStartPosition ||
                    EndPoint.transform.position != previousEndPosition ||
                    tensionFactor != previoustension ||
                    propertiesChanged)  // 변경된 속성이 있을 경우에만 업데이트
                {
                    RotateAndScaleCableMesh();
                    previousStartPosition = StartPoint.transform.position;
                    previousEndPosition = EndPoint.transform.position;
                    previoustension = tensionFactor;
                    propertiesChanged = false;  // 속성 변경 플래그 초기화
                }
            }
        }
#endif

        private void Start()
        {
            RotateAndScaleCableMesh();
        }

        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                propertiesChanged = true;  // 속성이 변경되었음을 표시
            }
        }

        private void RotateAndScaleCableMesh()
        {
            if (StartPoint == null || EndPoint == null || CableMesh1 == null)
                return;

            // Cable mesh 1
            CableMesh1.SetActive(Cable1Visibility);  // set visibility for cable mesh

            if (Cable1Visibility)
            {
                Vector3 direction = EndPoint.transform.position - StartPoint.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                CableMesh1.transform.rotation = targetRotation;

                float distance = direction.magnitude;
                float scaleFactor = distance / defaultDistance;
                if (randomTension)
                {
                    CableMesh1.transform.localScale = new Vector3(1f, Random.Range(0.5f * tensionFactor, 1.5f * tensionFactor), scaleFactor) * this.scaleFactor1;
                }
                else
                {
                    CableMesh1.transform.localScale = new Vector3(1f, tensionFactor * 1f, scaleFactor) * this.scaleFactor1;
                }
            }

            // Cable mesh 2
            CableMesh2.SetActive(Cable2Visibility);  // set visibility for cable mesh

            if (Cable2Visibility)
            {
                Vector3 direction = EndPoint.transform.position - StartPoint.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                CableMesh2.transform.rotation = targetRotation;

                float distance = direction.magnitude;
                float scaleFactor = distance / defaultDistance;
                if (randomTension)
                {
                    CableMesh2.transform.localScale = new Vector3(1f, Random.Range(0.5f * tensionFactor, 1.5f * tensionFactor), scaleFactor) * this.scaleFactor1;
                }
                else
                {
                    CableMesh2.transform.localScale = new Vector3(1f, tensionFactor * 1f, scaleFactor) * this.scaleFactor1;
                }
            }
        }
    }
}
