Shader "Hidden/MaskShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskColor("MaskColor", Color) = (0,0,0,1)

    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Tags{
            "Queue" = "Transparent"
        }

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha

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
				float4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);

                o.uv = v.uv;
                return o;
            }
            
            sampler2D _MainTex;
			fixed4 _MaskColor;
			sampler2D _M; // Get Torch screen capture from FlashLight Shader

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col2 = tex2D(_M, i.screenPos);
                return fixed4(_MaskColor.r, _MaskColor.g, _MaskColor.b, 1-col2.a);
            }
            ENDCG
        }
    }
}
