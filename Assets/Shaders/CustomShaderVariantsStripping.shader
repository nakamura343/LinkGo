Shader "Custom/ShaderVariantsStripping"
{
	SubShader
	{
		Pass
		{
			Name "ShaderVariantsStripping/Pass"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile COLOR_ORANGE COLOR_VIOLET COLOR_GREEN COLOR_GRAY
			#pragma multi_compile OP_ADD OP_MUL OP_SUB

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 get_color()
			{
				#if defined(COLOR_ORANGE)
					return fixed4(1.0, 0.5, 0.0, 1.0);
				#elif defined(COLOR_VIOLET)
					return fixed4(0.8, 0.2, 0.8, 1.0);
				#elif defined(COLOR_GREEN)
					return fixed4(0.5, 0.9, 0.3, 1.0);
				#elif defined(COLOR_GRAY)
					return fixed4(0.5, 0.9, 0.3, 1.0);
				#else
					#error "Unknown 'color' keyword"
				#endif
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 diffuse = tex2D(_MainTex, i.uv);
				fixed4 color = get_color();

				#if defined(OP_ADD)
					return diffuse + color;
				#elif defined(OP_MUL)
					return diffuse * color;
				#elif defined(OP_SUB)
					return diffuse - color;
				#else
					#error "Unknown 'op' keyword"
				#endif
			}
			ENDCG
		}
	}
}
