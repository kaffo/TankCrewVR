using UnityEngine;
using System.Collections;
using VRTK;

public class ControllerManager : MonoBehaviour {
    public GameObject leftController;
    public GameObject rightController;
    public GameObject turret;

    public void Start() {
        /*if (GetComponent<VRTK_ControllerEvents>() == null)
        {
            Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the VRTK_ControllerEvents script attached to it");
            return;
        }*/

        //Setup controller event listeners
        leftController.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoLeftTriggerPressed);
        //leftController.GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoLeftTriggerReleased);

        rightController.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoRightTriggerPressed);

        /*leftController.GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);
        
        leftController.GetComponent<VRTK_ControllerEvents>().ApplicationMenuPressed += new ControllerInteractionEventHandler(DoApplicationMenuPressed);
        leftController.GetComponent<VRTK_ControllerEvents>().ApplicationMenuReleased += new ControllerInteractionEventHandler(DoApplicationMenuReleased);

        leftController.GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
        leftController.GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(DoGripReleased);

        leftController.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
        leftController.GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);

        leftController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouchStart);
        leftController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

        leftController.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);*/
    }

    IEnumerator TurnTurret()
    {
        while (leftController.GetComponent<VRTK_ControllerEvents>().triggerPressed == true)
        {
            if (leftController.transform.localPosition.x < -0.1)
            {
                turret.transform.localEulerAngles += new Vector3(0, -0.5f, 0);
            }
            else if (leftController.transform.localPosition.x > 0.1)
            {
                turret.transform.localEulerAngles += new Vector3(0, 0.5f, 0);
            }
            yield return null;
        }
        yield break;
    }

    IEnumerator PitchTurret()
    {
        while (rightController.GetComponent<VRTK_ControllerEvents>().triggerPressed == true)
        {
            if (rightController.transform.localPosition.y < -0.1)
            {
                //Debug.Log("Pos: " + rightController.transform.localPosition);
                turret.transform.localEulerAngles += new Vector3(0.5f, 0, 0);
            }
            else if (rightController.transform.localPosition.y > 0.1)
            {
                //Debug.Log("Pos: " + rightController.transform.localPosition);
                turret.transform.localEulerAngles += new Vector3(-0.5f, 0, 0);
            }
            yield return null;
        }
        yield break;
    }

    private void DoLeftTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        StartCoroutine("TurnTurret");
        //DebugLogger(e.controllerIndex, "TRIGGER", "pressed down", e);
    }

    private void DoRightTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        StartCoroutine("PitchTurret");
        //DebugLogger(e.controllerIndex, "TRIGGER", "pressed down", e);
    }

    // Update is called once per frame
    void Update () {
    }
}
