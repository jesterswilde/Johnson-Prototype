Shader "Unlit/RGB"
{
	Properties
	{
		_Color("RGB", Color) = (1, 1, 1)
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }

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
				return o;
			}
			
			fixed3 _Color;

			fixed3 frag (v2f i) : SV_Target
			{
				fixed3 col = _Color;

				return col;
			}
			ENDCG
		}
	}
}
