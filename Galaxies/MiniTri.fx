cbuffer MatrixBuffer
{
	matrix worldMat;
	matrix viewMat;
	matrix projMat;
	float4 cameraPos;
	float4 sunDir;
	float4 sunColor;
	float4 atmosphereColorAndRadius;
}
//Texture2D txDiffuse : register( t0 );
Texture2D <float4> txDiffuse[3];// : register( t0 );
//Texture2D <float4> txNoise;// : register( t1 );
//SamplerState mySampler;// : register( s0 );
//sampler samLinear;

struct VS_IN
{
	float3 pos : POSITION;
	float3 normal : NORMAL;
	float2 tex : TEXCOORD;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	//float4 col : COLOR;
	float2 tex : TEXCOORD0;
	float3 worldPos : TEXCOORD1;
	float3 normal : NORMAL;
};

PS_IN EarthSurfaceVS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	//input.pos.w = 1.0f;
//	output.pos = mul( float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat );
	output.pos = mul( float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat );
	output.pos = mul( output.pos, viewMat );
	output.pos = mul( output.pos, projMat );
	output.worldPos = input.pos;
	//output.worldPos = mul(float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat).xyz;
	output.normal = input.normal;
	output.tex = input.tex;
	return output;
}

PS_IN AtmosphereVS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	//input.pos.w = 1.0f;
	float scale = 1.02f;
	//output.pos = mul( float4(input.pos.x*scale, input.pos.y*scale, input.pos.z*scale, 1), worldMat );
	output.pos = mul( float4(input.pos.x*scale, input.pos.y*scale, input.pos.z*scale, 1), worldMat );
	output.pos = mul( output.pos, viewMat );
	output.pos = mul( output.pos, projMat );
	output.worldPos = input.pos*scale;
	//output.worldPos = mul(float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat).xyz;
	output.normal = input.normal;
	output.tex = input.tex;
	return output;
}

void IntersectSphereAndRay(float3 pos, float radius, float3 posA, float3 posB, out float3 intersectA2, out float3 intersectB2)
{
	// Use dot product along line to find closest point on line
	float3 eyeVec2 = normalize(posB-posA);
	float dp = dot(eyeVec2, pos - posA);
	float3 pointOnLine = eyeVec2 * dp + posA;
	// Clamp that point to line end points if outside
	//if ((dp - radius) < 0) pointOnLine = posA;
	//if ((dp + radius) > (posB-posA).Length()) pointOnLine = posB;
	// Distance formula from that point to sphere center, compare with radius.
	float distance = length(pointOnLine - pos);
	float ac = radius*radius - distance*distance;
	float rightLen = 0;
	if (ac >= 0) rightLen = sqrt(ac);
	intersectA2 = pointOnLine - eyeVec2 * rightLen;
	intersectB2 = pointOnLine + eyeVec2 * rightLen;
	//if (distance > radius) return false;
	//return true;
}

SamplerState samWrap
{
	Filter = MIN_MAG_MIP_LINEAR;
	//Filter = ANISOTROPIC;
	AddressU = Wrap;
	AddressV = Wrap;
};

SamplerState samWrapAniso
{
	//Filter = MIN_MAG_MIP_LINEAR;
	Filter = ANISOTROPIC;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 EarthSurfacePS( PS_IN input ) : SV_Target
{
	float3 sunTrans = mul(sunDir.xyz, transpose((float3x3)worldMat)).xyz;
	float3 camTrans = mul(cameraPos.xyz, transpose((float3x3)worldMat)).xyz;
	float3 normal = normalize(input.normal);
	float3 eyeVec = normalize(input.worldPos - camTrans.xyz);
	float3 bounceNorm = dot(eyeVec, normal)*normal;
	float3 bounce = (bounceNorm - eyeVec) + bounceNorm;
	float3 highlight = saturate(dot(bounce, sunTrans.xyz));

	float3 light = dot(normal, -sunTrans.xyz) * sunColor.xyz;
	//float3 tex = txDiffuse.Sample( mySampler, input.tex ).xyz;
	float3 tex = pow(txDiffuse[0].Sample( samWrapAniso, input.tex ).xyz, 2);
	tex = lerp(tex, tex.rrr, 0.3f);
	//tex.b = saturate(tex.b-218);
	float3 clouds = pow(txDiffuse[2].Sample( samWrapAniso, input.tex ).xyz, 2);
	//float3 finalColor = float3(0.3f, 0.6f, 0.99f) * tex;// * light;
	float edgeGlow = pow(saturate(1.0f-dot(-eyeVec, normal)), 2.0f)*0.07f+0.93f;
	float3 finalColor = lerp(float3(0.05f, 0.25f, 1.0f), tex * 0.75 + clouds, edgeGlow) * light;//0.975
	finalColor *= 0.5f;
	float4 noise = txDiffuse[1].Sample(samWrapAniso, input.tex*float2(8,8)).xyzw;
	float grayNoise = lerp(dot(noise.xyzw, float4(1,2,4,0)).x, 1, 0.9f);
	finalColor += pow(highlight, 30) * sunColor.xyz*0.35f * saturate(1.0f - (tex.r + tex.g - (5/255.0f))*64) * grayNoise * (1.0f - pow(clouds.r, 0.2));
	//finalColor += saturate(0.003f/(1.0f-highlight)-0.02f) * sunColor.xyz * saturate(1.0f - (tex.r + tex.g - (5/255.0f))*64) * grayNoise;
	finalColor = sqrt(finalColor);
//finalColor = edgeGlow.xxx;
	return float4(finalColor.x, finalColor.y, finalColor.z, 1.0f);
}

float4 AtmospherePS( PS_IN input ) : SV_Target
{
	//float3 tex = txDiffuse.Sample( mySampler, input.tex ).xyz;
	//float3 tex = txDiffuse.Sample( samWrap, input.tex ).xyz;
	//return float4(tex.x, tex.y, tex.z, 0.5);
	//float3 clouds = pow(txDiffuse[2].Sample( samWrap, input.tex ).xyz, 2);
	float3 camTrans = mul(cameraPos.xyz, transpose((float3x3)worldMat)).xyz;
	float3 eyeVec = normalize(input.worldPos - camTrans.xyz);
	float3 intersectA, intersectB;
	IntersectSphereAndRay(float3(0,0,0), 1.02f, camTrans.xyz, camTrans.xyz + eyeVec, intersectA, intersectB);
	float3 innerA, innerB;
	IntersectSphereAndRay(float3(0,0,0), 1.0f, camTrans.xyz, camTrans.xyz + eyeVec, innerA, innerB);
	float3 sunTrans = mul(sunDir.xyz, transpose((float3x3)worldMat)).xyz;
	float3 light = dot(input.normal, -sunTrans.xyz) * sunColor.xyz;
	float3 finalColor = (float3(0.05f, 0.25f, 1.0f)) * light;
	float alpha = length(intersectA-intersectB) - length(innerA-innerB);// - length(innerA-innerB);//*0.5f;
	//alpha = saturate(alpha + saturate(1.0f-dot(-eyeVec, input.normal))*0.4f);
	//float alpha = length(innerA-innerB);//*0.5f;
//finalColor = alpha.xxx;//-0.5f;
//alpha = 1;
	finalColor *= 0.5f;
	finalColor = sqrt(finalColor);
	return float4(finalColor.x, finalColor.y, finalColor.z, alpha);
}



BlendState AlphaBlending
{
	AlphaToCoverageEnable = FALSE;
	BlendEnable[0] = TRUE;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ONE;
	BlendOpAlpha = ADD;
	RenderTargetWriteMask[0] = 0x0F;
};

BlendState NoBlending
{
	AlphaToCoverageEnable = FALSE;
	BlendEnable[0] = FALSE;
};

technique11 EarthRender
{
	pass P0
	{
		SetBlendState( NoBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		//SetBlendState( AlphaBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		//AlphaBlendEnable = true;
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_5_0, EarthSurfaceVS() ) );
		SetPixelShader( CompileShader( ps_5_0, EarthSurfacePS() ) );
	}
	pass P1
	{
		//SetBlendState( NoBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		SetBlendState( AlphaBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		//AlphaBlendEnable = true;
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_5_0, AtmosphereVS() ) );
		SetPixelShader( CompileShader( ps_5_0, AtmospherePS() ) );
	}
}


