// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Grid"
{
	Properties
	{
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 worldPos : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 pos = i.worldPos.xy + 1000.5;
				float2 subPos = (pos+100) % 1;
				float tile = (pos.x % 2 > 1) != (pos.y % 2 > 1);
				tile = tile *0.1 + 0.8;
				float edge = 1;
				edge *= smoothstep(0.01, 0.05, subPos.x);
				edge *= smoothstep(0.01, 0.05, subPos.y);
				edge *= 1-smoothstep(0.95, 0.99, subPos.x);
				edge *= 1-smoothstep(0.95, 0.99, subPos.y);
				return fixed4(1,1,1,1)*edge*tile;
			}
			ENDCG
		}
	}
}
