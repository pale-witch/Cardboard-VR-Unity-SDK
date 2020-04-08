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
            byte[] dataArray = ReadByteArray(_encodedDeviceParams, _paramsSize);
            Debug.Log("Feature Test RetrieveDeviceParam params byte=\r\n " + string.Join(" , ", dataArray));

        }

        public static (IntPtr, int) GetDeviceParamsPointer()
        {
            return (_encodedDeviceParams, _paramsSize);
        }

        private static byte[] ReadByteArray(IntPtr pointer, int size)
        {
            var result = new byte[size];
            Marshal.Copy(pointer, result, 0, size);
            return result;
        }

    }
}