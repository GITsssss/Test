using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Bindings;
public class ShaderEnum 
{

    //[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
    public enum HLVRShaderRenderType 
    {
       Transform=1,
       Opaque=2,

       
    }



    //public static Dictionary<HLVRShaderRenderType, string> statusTypeToString = new Dictionary<HLVRShaderRenderType, string>()
    //{
    //    {HLVRShaderRenderType.Opaque, "Transform"},
    //    {HLVRShaderRenderType.Transform, "Opaque"},
    //}
}
