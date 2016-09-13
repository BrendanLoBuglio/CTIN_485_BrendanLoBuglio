using UnityEngine;
using System.Collections;

public class BasicThirdPerson : MonoBehaviour 
{
    //Editor References:
    [SerializeField] private Camera     mCamera;
    [SerializeField] private Transform  meshPivot;
    [SerializeField] private Animator   characterAnimator;

    //Tweakables:
    [SerializeField] private float  runSpeed;
    [SerializeField] private float  walkSpeed;
    
    //Internal References:
    private CharacterController mController;
    
    private void Start()
    {
        mController = GetComponent<CharacterController>();
    }

    void Update ()
    {
        Vector2 playerInput         = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool nonZeroInput           = playerInput.magnitude > 0.01f;
        bool runInput               = Input.GetKey(KeyCode.Mouse0);
        
        Vector3 worldSpaceInput     = mCamera.transform.right * playerInput.x + mCamera.transform.forward * playerInput.y;
        worldSpaceInput             = new Vector3(worldSpaceInput.x, 0f, worldSpaceInput.z);

        Vector3 moveDelta           = Time.deltaTime * worldSpaceInput * (runInput ? runSpeed : walkSpeed);
        
        mController.Move(moveDelta);
        if(nonZeroInput)
        {
            meshPivot.rotation = Quaternion.LookRotation(moveDelta, Vector3.up);
        }

        characterAnimator.SetBool("Walking",    nonZeroInput);
        characterAnimator.SetBool("RunMode",    runInput);
	}
}
