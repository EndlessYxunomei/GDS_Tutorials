using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] float keyboardInputSensitivity = 1f;
        [SerializeField] float mouseInputSensitivity = 1f;
        [SerializeField] bool continius = true;
        Transform bottomLeftBorder;
        Transform topRightBorder;
        Vector3 input;
        Vector3 pointOfOrigin;

        // Start is called before the first frame update
        void Start()
        {
            bottomLeftBorder = FindObjectOfType<StageManager>().ldCorner;
            topRightBorder = FindObjectOfType<StageManager>().ruCorner;
        }

        // Update is called once per frame
        void Update()
        {
            NullInput();
            MoveCameraInput();
            MoveCamera();
        }

        private void NullInput()
        {
            input.x = 0;
            input.y = 0;
            input.z = 0;
        }
        private void MoveCameraInput()
        {
            AxisInput();
            MouseInput();
        }
        private void AxisInput()
        {
            input.x += keyboardInputSensitivity * Input.GetAxisRaw("Horizontal");
            input.z += keyboardInputSensitivity * Input.GetAxisRaw("Vertical");

        }
        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointOfOrigin = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseInput = Input.mousePosition;
                //y - ������������ ���, � ������ ���������� z
                input.x += mouseInputSensitivity * (mouseInput.x - pointOfOrigin.x);
                input.z += mouseInputSensitivity * (mouseInput.y - pointOfOrigin.y);
                if (continius == false)
                {
                    pointOfOrigin = mouseInput;
                }
            }
        }
        private void MoveCamera()
        {
            Vector3 position = transform.position;
            position += Time.deltaTime * input;
            position.x = Mathf.Clamp(position.x, bottomLeftBorder.position.x, topRightBorder.position.x);
            position.z = Mathf.Clamp(position.z, bottomLeftBorder.position.z, topRightBorder.position.z);

            transform.position = position;
        }
    }
}