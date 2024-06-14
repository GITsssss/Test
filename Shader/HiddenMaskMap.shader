Shader "Hidden/MaskMap"
{
    Properties
    {
        _MainTex("Texture",2D) = "white"{}
        _Metallic("Texture", 2D) = "black"{}
        _Occlusion("Texture", 2D) = "black"{}
        _DetailMask("Texture",2D) = "black"{}
        _Smoothness("Texture",2D) = "black"{}
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Metallic;
            sampler2D _Occlusion;
            sampler2D _DetailMask;
            sampler2D _Smoothness;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.r = tex2D(_Metallic, i.uv).r;
                col.g = tex2D(_Occlusion, i.uv).g;
                col.b = tex2D(_DetailMask, i.uv).b;
                col.a = tex2D(_Smoothness, i.uv).g;                
                col.a = pow(col.a, 0.45);      //线性空间会对a通道做gamma校正，要自己校正         
                return col;
            }
            ENDCG
        }
    }
}