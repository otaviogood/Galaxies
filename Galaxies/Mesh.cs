using System;
using System.Collections.Generic;
using System.Diagnostics;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Otavio.Math;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace Galaxies
{
	public struct VertexPNT
	{
		public float3 _pos;
		public float3 normal;
		public float2 uv0;
	}

	public class Mesh
	{
		public string name;
		InputLayout layout;
		Buffer vertices;
		Buffer indexBuffer;
		int numIndexes;
		//SamplerState sampler;
		List<ShaderResourceView> textureView = new List<ShaderResourceView>();

		public Mesh()
		{
		}
		~Mesh()	// should this be a finalizer instead?
		{
			vertices.Dispose();
			indexBuffer.Dispose();
			layout.Dispose();
		}

/*		public void GenCube()
		{
			//int indexBufSizeInBytes = 4*3;
			int[] indexes = new int[] { 0, 1, 2 };
//			SharpDX.Direct3D11.BufferDescription indexBufDesc = new BufferDescription(indexBufSizeInBytes, ResourceUsage.Default,
//				BindFlags.IndexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
			indexBuffer = Buffer.Create(Global._G.device, BindFlags.IndexBuffer, indexes);

			// Layout from VertexShader input signature
			//ShaderBytecode passSignature = pass.Description.Signature;
			ShaderSignature sig = ShaderSignature.GetInputSignature(Global._G.vertexShaderByteCode);
			layout = new InputLayout(Global._G.device, sig, new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                    new InputElement("NORMAL", 0, Format.R32G32B32_Float, 16, 0),
					new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16+12, 0)
					//new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
					//new InputElement("TEXCOORD", 0, Format.R32G32B32A32_Float, 32, 0)
                });
			// Instantiate Vertex buiffer from vertex data
			//Buffer vertices = Buffer.Create(device, BindFlags.VertexBuffer, new[]
			//                      {
			//                          new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 0.0f),
			//                          new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 0.0f),
			//                          new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), new Vector4(1.0f, 1.0f, 0.0f, 0.0f),
			//                      });
			// Instantiate Vertex buiffer from vertex data
			vertices = Buffer.Create(Global._G.device, BindFlags.VertexBuffer, new[]
                                  {
                                      // 3D coordinates              UV Texture coordinates
                                      -1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f, // Front
                                      -1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 0.0f,
                                       1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                      -1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                                       1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                       1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 1.0f,

                                      -1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f, // BACK
                                       1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                                      -1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 1.0f,
                                      -1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                       1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 0.0f,
                                       1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,

                                      -1.0f, 1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f, // Top
                                      -1.0f, 1.0f,  1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 0.0f,
                                       1.0f, 1.0f,  1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                      -1.0f, 1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                                       1.0f, 1.0f,  1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                       1.0f, 1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 1.0f,

                                      -1.0f,-1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f, // Bottom
                                       1.0f,-1.0f,  1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                                      -1.0f,-1.0f,  1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 1.0f,
                                      -1.0f,-1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                       1.0f,-1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 0.0f,
                                       1.0f,-1.0f,  1.0f,  1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,

                                      -1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f, // Left
                                      -1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 0.0f,
                                      -1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                      -1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                                      -1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                      -1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 1.0f,

                                       1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f, // Right
                                       1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                                       1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 1.0f,
                                       1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    1.0f, 0.0f,
                                       1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 0.0f,
                                       1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 0.0f, 0.0f,    0.0f, 1.0f,
                            });


			SharpDX.Direct3D11.Resource tex = Texture2D.FromFile<Texture2D>(Global._G.device, "noiseRGBA.bmp");
			textureView = new ShaderResourceView(Global._G.device, tex);

			sampler = new SamplerState(Global._G.device, new SamplerStateDescription()
			{
				Filter = Filter.MinMagMipLinear,
				AddressU = TextureAddressMode.Wrap,
				AddressV = TextureAddressMode.Wrap,
				AddressW = TextureAddressMode.Wrap,
				BorderColor = Color.Black,
				ComparisonFunction = Comparison.Never,
				MaximumAnisotropy = 16,
				MipLodBias = 0,
				MinimumLod = 0,
				MaximumLod = 16,
			});
			numIndexes = 3;
		}*/

		public float3 SphericalCoord(float2 latLon, out float3 pos)
		{
			pos = new float3(
				-(float)(Math.Cos(latLon.x) * Math.Cos(latLon.y)),
				-(float)Math.Sin(latLon.y),
				(float)(Math.Sin(latLon.x) * Math.Cos(latLon.y))
				);
			float3 normal = pos.Normalize();
			float angleXY = (float)Math.Atan2(normal.x, normal.y) * 0.0f;
			float angleYZ = (float)Math.Atan2(normal.y, normal.z) * 1.0f;
			//float angleZX = Math.Atan2(normal.z, normal.x)*8;
			float finalR = ((float)Math.Cos(angleXY)) * (1.0f - Math.Abs(normal.z));
			finalR *= ((float)Math.Cos(angleYZ)) * (1.0f - Math.Abs(normal.x));
			//finalR += (sin(angleZX)) * (1.0-abs(normal.y));

			return pos;// *finalR;
		}

/*		public void MakeLatLonSphere(int latDivs, int lonDivs)
		{
			List<VertexPNT> verts = new List<VertexPNT>();
			for (int lat = -latDivs / 2; lat < latDivs / 2; lat++)
			{
				for (int lon = 0; lon < lonDivs; lon++)
				{
					float2 pos = new float2((float)lon / lonDivs, (float)lat / latDivs);
					float2 posO = new float2((float)(lon + 1) / lonDivs, (float)lat / latDivs);
					float2 posA = new float2((float)lon / lonDivs, ((float)lat + 1) / latDivs);
					float2 posAO = new float2(((float)lon + 1) / lonDivs, ((float)lat + 1) / latDivs);
					float2 radScale = new float2((float)(2 * Math.PI), (float)(Math.PI));
					//pos *= radScale;
					//posO *= radScale;
					//posA *= radScale;
					//posAO *= radScale;

					float3 posXYZ = new float3();
					VertexPNT v0 = new VertexPNT();
					v0._pos = SphericalCoord(pos * radScale, out posXYZ);
					v0.normal = posXYZ.Normalize();
					v0.uv0 = pos +new float2(0, 0.5f);
					verts.Add(v0);

					VertexPNT v1 = new VertexPNT();
					v1._pos = SphericalCoord(posA * radScale, out posXYZ);
					v1.normal = posXYZ.Normalize();
					v1.uv0 = posO +new float2(0, 0.5f);
					verts.Add(v1);

					VertexPNT v2 = new VertexPNT();
					v2._pos = SphericalCoord(posO * radScale, out posXYZ);
					v2.normal = posXYZ.Normalize();
					v2.uv0 = posA +new float2(0, 0.5f);
					verts.Add(v2);

					VertexPNT v3 = new VertexPNT();
					v3._pos = SphericalCoord(posAO * radScale, out posXYZ);
					v3.normal = posXYZ.Normalize();
					v3.uv0 = posAO +new float2(0, 0.5f);
					verts.Add(v3);
					verts.Add(v2);
					verts.Add(v1);
				}
			}

			int[] faceList = GenerateDefaultFaceList(verts);
			indexBuffer = Buffer.Create(Global._G.device, BindFlags.IndexBuffer, faceList);
			numIndexes = faceList.Length;

			ShaderSignature sig = ShaderSignature.GetInputSignature(Global._G.vertexShaderByteCode);
			layout = new InputLayout(Global._G.device, sig, new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0),
					new InputElement("TEXCOORD", 0, Format.R32G32_Float, 12+12, 0)
					//new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
					//new InputElement("TEXCOORD", 0, Format.R32G32B32A32_Float, 32, 0)
                });
			vertices = Buffer.Create(Global._G.device, BindFlags.VertexBuffer, verts.ToArray());

			SharpDX.Direct3D11.Resource tex = Texture2D.FromFile<Texture2D>(Global._G.device, "world.200401.3x540x270.jpg");
			textureView = new ShaderResourceView(Global._G.device, tex);

			sampler = new SamplerState(Global._G.device, new SamplerStateDescription()
			{
				Filter = Filter.MinMagMipLinear,
				AddressU = TextureAddressMode.Wrap,
				AddressV = TextureAddressMode.Wrap,
				AddressW = TextureAddressMode.Wrap,
				BorderColor = Color.Black,
				ComparisonFunction = Comparison.Never,
				MaximumAnisotropy = 16,
				MipLodBias = 0,
				MinimumLod = 0,
				MaximumLod = 16,
			});

//			CreateVertexBuffer(faces);
	//		m_shader = "simpleGlobeShade";
		}*/

		public void MakeLatLonSphere2(int latDivs, int lonDivs)
		{
			List<VertexPNT> verts = new List<VertexPNT>();
			List<int> faceTemp = new List<int>();
			int index = 0;
			for (int lat = -latDivs / 2; lat <= latDivs / 2; lat++)
			{
				for (int lon = 0; lon <= lonDivs; lon++)
				{
					float2 pos = new float2((float)lon / lonDivs, (float)lat / latDivs);
					float2 radScale = new float2((float)(2 * Math.PI), (float)(Math.PI));

					if ((lon != lonDivs) && (lat != latDivs /2))
					{
						int row = lonDivs + 1;
						faceTemp.Add(index);
						faceTemp.Add(index + row);
						faceTemp.Add(index + 1);
						faceTemp.Add(index + row);
						faceTemp.Add(index + row + 1);
						faceTemp.Add(index + 1);
					}

					float3 posXYZ = new float3();
					VertexPNT v0 = new VertexPNT();
					v0._pos = SphericalCoord(pos * radScale, out posXYZ);
					v0.normal = posXYZ.Normalize();
					v0.uv0 = new float2(1.0f-pos.x, pos.y + 0.5f);
					verts.Add(v0);
					index++;
				}
			}

			int[] faceList = faceTemp.ToArray();// GenerateDefaultFaceList(verts);
			indexBuffer = Buffer.Create(Global._G.device, BindFlags.IndexBuffer, faceList);
			numIndexes = faceList.Length;

			//public EffectPass pass;
			EffectPass pass = Global._G.technique.GetPassByIndex(0);
			var passSignature = pass.Description.Signature;
			layout = new InputLayout(Global._G.device, passSignature, new[]
			//ShaderSignature sig = ShaderSignature.GetInputSignature(Global._G.vertexShaderByteCode);
			//layout = new InputLayout(Global._G.device, sig, new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0),
					new InputElement("TEXCOORD", 0, Format.R32G32_Float, 12+12, 0)
                });
			vertices = Buffer.Create(Global._G.device, BindFlags.VertexBuffer, verts.ToArray());

			//SharpDX.Direct3D11.Resource tex = Texture2D.FromFile<Texture2D>(Global._G.device, @"Content\world.200401.3x540x270.jpg");
			////SharpDX.Direct3D11.Resource tex = Texture2D.FromFile<Texture2D>(Global._G.device, @"C:\dev\Globe\Content\Custom\world.200401.3x5400x2700.jpg");
			//textureView = new ShaderResourceView(Global._G.device, tex);
			//tex.Dispose();
			textureView.Add(Global._G.texMan.texDict["world.200401.3x540x270"].textureView);
			//textureView.Add(Global._G.texMan.texDict["world.200407.3x5400x2700"].textureView);
			//textureView.Add(Global._G.texMan.texDict["world.200408.3x8192x8192"].textureView);

			// This is handled the the shader effects file. Maybe someday we can make an override if needed.
			//sampler = new SamplerState(Global._G.device, new SamplerStateDescription()
			//{
			//    Filter = Filter.Anisotropic,
			//    AddressU = TextureAddressMode.Wrap,
			//    AddressV = TextureAddressMode.Wrap,
			//    AddressW = TextureAddressMode.Wrap,
			//    BorderColor = Color.Black,
			//    ComparisonFunction = Comparison.Never,
			//    MaximumAnisotropy = 16,
			//    MipLodBias = 0,
			//    MinimumLod = 0,
			//    MaximumLod = 16,
			//});

			//			CreateVertexBuffer(faces);
			//		m_shader = "simpleGlobeShade";
		}

		public int[] GenerateDefaultFaceList(List<VertexPNT> verts)
		{
			int[] faces = new int[verts.Count];
			for (int count = 0; count < verts.Count; count++)
			{
				faces[count] = count;
			}
			return faces;
		}


		public void Render()
		{
			//BlendStateDescription desc = new BlendStateDescription();
			//desc.RenderTarget[0].SourceBlend = BlendOption.SourceAlpha;
			//desc.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
			//desc.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
			//desc.RenderTarget[0].DestinationAlphaBlend = BlendOption.InverseSourceAlpha;
			//SharpDX.Direct3D11.BlendState bs = new BlendState(Global._G.device, desc);
			//Global._G.context.OutputMerger.SetBlendState(bs, null, -1);
			//bs.Dispose();

			// Prepare All the stages
			Global._G.context.InputAssembler.InputLayout = layout;
			Global._G.context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
			//Global._G.dxState.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, Utilities.SizeOf<Vector3>() + Utilities.SizeOf<Vector3>() + Utilities.SizeOf<Vector2>(), 0));
			int lamez = System.Runtime.InteropServices.Marshal.SizeOf(new VertexPNT());
			Global._G.context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, lamez, 0));
			Global._G.context.InputAssembler.SetIndexBuffer(indexBuffer, Format.R32_UInt, 0);

			int numPasses = Global._G.technique.Description.PassCount;
			for (int count = 0; count < numPasses; count++)
			{
				EffectPass pass = Global._G.technique.GetPassByIndex(count);
				pass.Apply(Global._G.context);
				Global._G.context.VertexShader.SetConstantBuffer(0, Global._G.constantBuffer);
				Global._G.context.PixelShader.SetConstantBuffer(0, Global._G.constantBuffer);
				//Global._G.pass
				//Global._G.dxState.VertexShader.Set(Global._G.vertexShader);
				//Global._G.dxState.PixelShader.Set(Global._G.pixelShader);
				//Global._G.context.PixelShader.SetSampler(0, sampler);
				foreach (var tv in textureView) Global._G.context.PixelShader.SetShaderResource(0, tv);
				//Global._G.dxState.Draw(36, 0);
				Global._G.context.DrawIndexed(numIndexes, 0, 0);
			}

			//pass = Global._G.technique.GetPassByIndex(1);
			//pass.Apply(Global._G.context);
			//Global._G.context.VertexShader.SetConstantBuffer(0, Global._G.constantBuffer);
			//Global._G.context.PixelShader.SetConstantBuffer(0, Global._G.constantBuffer);
			////Global._G.context.PixelShader.SetSampler(0, sampler);
			//foreach (var tv in textureView) Global._G.context.PixelShader.SetShaderResource(0, tv);
			//Global._G.context.DrawIndexed(numIndexes, 0, 0);
		}

	}
}
