Shader "HLVR/TeleportMarker"
{
    Properties
    {
         _MainTex ("Texture", 2D) = "white" {}
         [HDR]_ColorA("ColorA", Color) = (0, 0.7445183, 1, 1)
         [Enum(HLVRShaderRenderType)]_RenderType("RenderType",Int)=1
    }
    SubShader
    {
        Tags {"[Queue]"="[Transparent]"  "RenderType"="Transparent" }
        Blend  DstColor 	SrcAlpha
      
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                 UNITY_VERTEX_INPUT_INSTANCE_ID //插入
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
 
                float4 vertex : SV_POSITION;
                  UNITY_VERTEX_OUTPUT_STEREO //插入
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _ColorA;
          
            v2f vert (appdata v)
            {
              
                v2f o;
                 UNITY_SETUP_INSTANCE_ID(v); //插入
                UNITY_INITIALIZE_OUTPUT(v2f, o); //插入
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //插入 
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv)*_ColorA;
              
                //clip(col.a -_Alpha);
                return fixed4(col.rgb,col.a);
            }
            ENDCG
        }
    }
}
