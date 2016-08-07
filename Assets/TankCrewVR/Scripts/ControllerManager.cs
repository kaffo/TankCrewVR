using UnityEngine;
using System.Collections;
using VRTK;

public class ControllerManager : MonoBehaviour {
    public GameObject leftController;
    public GameObject rightController;
    public GameObject turret;
    public GameObject barrel;
    public GameObject gun;

    public GameObject HBase;
    public GameObject VBase;

    public GameObject shellFireAudio;
    public GameObject clickAudio;
    public GameObject reloadAudio;

    public GameObject shellTemplate;

    private bool canFire = true;
    private float fireTimer = 5f;

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
        rightController.GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoRightGripPressed);

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
            Vector3 toMove;
            if (leftController.transform.localPosition.x < -0.05)
            {
                HBase.transform.localEulerAngles = new Vector3(0, -leftController.transform.localPosition.x * 45, 0);
                toMove = new Vector3(0, 1f * leftController.transform.localPosition.x, 0);
                if (toMove.y < -0.5f) { toMove.y = -0.5f; }
                turret.transform.localEulerAngles += toMove;
            }
            else if (leftController.transform.localPosition.x > 0.05)
            {
                HBase.transform.localEulerAngles = new Vector3(0, -leftController.transform.localPosition.x * 45, 0);
                toMove = new Vector3(0, 1f * leftController.transform.localPosition.x, 0);
                if (toMove.y > 0.5f) { toMove.y = 0.5f; }
                turret.transform.localEulerAngles += toMove;
            }
            yield return null;
        }
        HBase.transform.localEulerAngles = Vector3.zero;
        yield break;
    }

    IEnumerator PitchTurret()
    {
        while (rightController.GetComponent<VRTK_ControllerEvents>().triggerPressed == true)
        {
            Vector3 toMove;
            //Debug.Log("Pos: " + rightController.transform.localPosition);
            //Debug.Log("Ang: " + gun.transform.localEulerAngles.x);
            if (rightController.transform.localPosition.y < -0.05 && gun.transform.localEulerAngles.x < 358)
            {
                //Debug.Log("Pos: " + rightController.transform.localPosition);
                VBase.transform.localEulerAngles = new Vector3(rightController.transform.localPosition.y * 90, 0, 90);
                toMove = new Vector3(1f * -rightController.transform.localPosition.y, 0, 0);
                if (toMove.x > 0.5f) { toMove.x = 0.5f; }
                gun.transform.localEulerAngles += toMove;
            }
            else if (rightController.transform.localPosition.y > 0.05 && (gun.transform.localEulerAngles.x > 300 || gun.transform.localEulerAngles.x < 25))
            {
                //Debug.Log("Pos: " + rightController.transform.localPosition);
                VBase.transform.localEulerAngles = new Vector3(rightController.transform.localPosition.y * 90, 0, 90);
                toMove = new Vector3(1f * -rightController.transform.localPosition.y, 0, 0);
                if (toMove.x < -0.5f) { toMove.x = -0.5f; }
                gun.transform.localEulerAngles += toMove;
            }
            yield return null;
        }
        VBase.transform.localEulerAngles = new Vector3(0, 0, 90);
        yield break;
    }

    IEnumerator TurretShellFired(GameObject shell)
    {
        Quaternion origRot = turret.transform.localRotation;
        yield return new WaitUntil(() => shell.activeSelf);
        for (int i = 0; i < 2; i++)
        {
            //Debug.Log("1: " + turret.transform.localEulerAngles);
            turret.transform.localEulerAngles += new Vector3(-10f, 0, 0);
            yield return null;
        }
        for (int i = 0; i < 20; i++)
        {
            //Debug.Log("2: " + turret.transform.localEulerAngles);
            turret.transform.localRotation = Quaternion.RotateTowards(turret.transform.localRotation, origRot, 0.1f);
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

    private void DoRightGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (canFire)
        {
            GameObject shell = Instantiate(shellTemplate);
            ShellController shellController = shell.GetComponent<ShellController>();

            shellController.barrel = barrel;
            shellFireAudio.GetComponent<AudioSource>().Play();
            canFire = false;
            StartCoroutine("TurretShellFired", shell);
        }
        else
        {
            clickAudio.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update () {
        if (canFire == false && fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
        else if (canFire == false && fireTimer <= 0)
        {
            canFire = true;
            fireTimer = 5f;
            reloadAudio.GetComponent<AudioSource>().Play();
        }
    }
}
