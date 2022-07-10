Shader "Custom/Warehouse"
{
	Properties
	{
		[IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0
	}
		SubShader
	{
		Tags {
			"RenderType" = "Opaque"
			"RenderPipeline" = "UniversalPipeline"
			"Queue" = "Geometry+1"
		}

		Pass {
			Blend Zero One
			ZWrite On

			Stencil {
				Ref[_StencilID]
				Comp Always
				Pass Replace
				Fail Keep
			}


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			struct v2o {
				half4 color : SV_Target;
				float depth : SV_Depth;
			};


		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			return o;
		}

		v2o frag(v2f i)
		{
			v2o o;
			o.color = half4(1, 1, 1, 1);
			o.depth = 1;
			return o;
		}
		ENDCG

		}
	}
}
