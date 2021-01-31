Shader "Custom/Mirror"
{
    Properties
    {
        _InactiveColour ("Inactive Colour", Color) = (1, 1, 1, 1)
        _tintColor ("Tint Colour", Color) = (1, 0, 1, 1)
        _tintAmount ("Tint Amount", float) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _InactiveColour;
            float4 _tintColor;
            float _tintAmount;
            int displayMask; // set to 1 to display texture, otherwise will draw test colour
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.screenPos.xy / i.screenPos.w;
                fixed4 col = tex2D(_MainTex, uv*displayMask+(1-displayMask)*float2(1-uv.x, uv.y));
                //float4 col = portalCol * displayMask + _InactiveColour * (1-displayMask);
                return (col*(1-_tintAmount))+(_tintColor*_tintAmount);
            }
            ENDCG
        }
    }
    Fallback "Standard" // for shadows
}
