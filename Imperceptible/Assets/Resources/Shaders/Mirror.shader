Shader "Custom/Mirror"
{
    Properties
    {
        _InactiveColour ("Inactive Colour", Color) = (1, 1, 1, 1)
        _tintColor ("Tint Colour", Color) = (1, 0, 1, 1)
        _tintAmount ("Tint Amount", float) = 0.3
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            //float4 _MainTex_ST;
            float4 _InactiveColour;
            float4 _tintColor;
            float _tintAmount;
            int displayMask; // set to 1 to display texture, otherwise will draw test colour
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; //TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col = col * displayMask + _InactiveColour * (1-displayMask);
                return (col*(1-_tintAmount))+(_tintColor*_tintAmount);
            }
            ENDCG
        }
    }
    Fallback "Standard" // for shadows
}
