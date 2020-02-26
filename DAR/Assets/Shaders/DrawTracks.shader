Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Coordinates ("Coordinates" , Vector) = (0,0,0,0)
		_Color ("Draw Color", Color) = (1,0,0,0)
		_Impact ("Impact of the brush", Range(1,500)) = 10
		_ExactSize("Maximum size of the track", float) = 0.0005
		_Strength ("Strength", Range(0,1)) = 0.3
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
			fixed4 _Coordinate;
			fixed4 _Color;
			float _Impact;
			float _Strength;
			float _ExactSize;
			int value;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				if (distance(i.uv, _Coordinate.xy) < _ExactSize) {
					value = 1;
				}
				else {
					value = 0;
				}
				float draw = pow(saturate(1 - distance(i.uv, _Coordinate.xy)) * value, 500 / _Impact);
				fixed4 drawcol = _Color * (draw * _Strength);
				return saturate(col + drawcol);

                return col;
            }
            ENDCG
        }
    }
}
