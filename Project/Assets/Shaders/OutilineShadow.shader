Shader "Hidden/BoundShadow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", Float) = 1
    }
    SubShader
    {
        // No culling or depth
        //Cull Off ZWrite Off ZTest Always
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
		
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineSize;
			
			/*
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                col.rgb = 1 - col.rgb;
                return col;
            } */
			
			float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);

				float shadowAlpha = 0.0;
				float2 offset = _OutlineSize / _ScreenParams.xy;

				shadowAlpha += tex2D(_MainTex, i.uv + offset * float2(1,0)).a * 0.25;
				shadowAlpha += tex2D(_MainTex, i.uv + offset * float2(-1,0)).a * 0.25;
				shadowAlpha += tex2D(_MainTex, i.uv + offset * float2(0,1)).a * 0.25;
				shadowAlpha += tex2D(_MainTex, i.uv + offset * float2(0,-1)).a * 0.25;

				shadowAlpha = saturate(shadowAlpha); // Clampa entre 0 e 1

				if (col.a < 0.1 && shadowAlpha > 0.0)
					return float4(_OutlineColor.rgb, shadowAlpha); // Agora com alpha suave
                
                return col;
            }
			
            ENDCG
        }
    }
}
