﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MobfishCardboard
{
    public static class CardboardQrCode
    {
        private static IntPtr _encodedDeviceParams;
        private static int _paramsSize;

        [DllImport(CardboardUtility.DLLName)]
        private static extern void CardboardQrCode_scanQrCodeAndSaveDeviceParams();

        //todo is this correct?
        //https://developers.google.com/cardboard/reference/c/group/qrcode-scanner#cardboardqrcode_getsaveddeviceparams
        [DllImport(CardboardUtility.DLLName)]
        private static extern void CardboardQrCode_getSavedDeviceParams(ref IntPtr encoded_device_params, ref int size);

        public static void StartScanQrCode()
        {
            CardboardQrCode_scanQrCodeAndSaveDeviceParams();
        }

        public static void RetrieveDeviceParam()
        {
            CardboardQrCode_getSavedDeviceParams(ref _encodedDeviceParams, ref _paramsSize);
            Debug.Log("Feature Test RetrieveDeviceParam size=" + _paramsSize);
            char[] charArray = ReadCharArray(_encodedDeviceParams, _paramsSize);
            string result = new string(charArray);
            Debug.Log("Feature Test RetrieveDeviceParam paramsStr=" + result);
        }

        private static char[] ReadCharArray(IntPtr pointer, int size)
        {
            var result = new char[size];
            Marshal.Copy(pointer, result, 0, size);
            return result;
        }
    }
}