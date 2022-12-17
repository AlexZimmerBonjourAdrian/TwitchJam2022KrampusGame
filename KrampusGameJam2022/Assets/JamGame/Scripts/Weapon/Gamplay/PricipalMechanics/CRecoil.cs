using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DL;
public class CRecoil : MonoBehaviour
{
    //[SerializeField] private CPlayer Player_Script;

    //private bool isAiming;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

   

    [SerializeField] private CManagerWeapon loadOut;

   

   

    //public void RecoilFire()
    //{
    //    //targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    //    targetRotation += new Vector3(loadOut.GetCurrentWeapon().GetComponent<CArmed>().GetRecoilX(), Random.Range(-loadOut.GetCurrentWeapon().GetComponent<CArmed>().GetRecoilY(), loadOut.GetCurrentWeapon().GetComponent<CArmed>().GetRecoilY()), Random.Range(-loadOut.GetCurrentWeapon().GetComponent<CArmed>().GetRecoilZ(), loadOut.GetCurrentWeapon().GetComponent<CArmed>().GetRecoilZ()));

    //}
}
