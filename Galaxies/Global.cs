using System;
using System.Collections.Generic;
using System.Diagnostics;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace Galaxies
{
	public struct ShaderParamStruct
	{
		public Matrix world;
		public Matrix view;
		public Matrix proj;
		public Vector4 cameraPos;
		public Vector4 sunDir;
		public Vector4 sunColor;
		public Vector4 atmosphereColorAndRadius;
	}

	public sealed class Global
	{
		// The singleton!!!
		public static readonly Global _G = new Global();

		public Device device;
		public SwapChain swapChain;
		public DeviceContext context;
		public Buffer constantBuffer;
		//public VertexShader vertexShader;
		//public CompilationResult vertexShaderByteCode;
		//public PixelShader pixelShader;
		//public CompilationResult pixelShaderByteCode;
		public DepthStencilView depthView;
		public RenderTargetView renderView;
		public Texture2D backBuffer;
		public Factory factory;

		// effects
		public Effect effect;

		public ShaderParamStruct shaderParams;

		public Camera camera;
		public TextureManager texMan;
		public RenderForm form; 

		private Global()
		{
			
		}

		public void InitDX(RenderForm form)
		{
            // SwapChain description
			SwapChainDescription desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
					new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.R10G10B10A2_UNorm),//.R8G8B8A8_UNorm),
				IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

			// Create Device and SwapChain
#if DEBUG
			Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, desc, out device, out swapChain);
#else
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
#endif

			context = device.ImmediateContext;

			// Ignore all windows events
			factory = swapChain.GetParent<Factory>();
			factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

			// New RenderTargetView from the backbuffer
			backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
			renderView = new RenderTargetView(device, backBuffer);

			//// Compile Vertex and Pixel shaders
#if DEBUG
            CompilationResult effectByteCode = ShaderBytecode.CompileFromFile("MiniTri.fx", "fx_5_0", ShaderFlags.Debug | ShaderFlags.SkipOptimization | ShaderFlags.WarningsAreErrors /*| ShaderFlags.ForcePsSoftwareNoOpt*/, EffectFlags.None);
#else
            CompilationResult effectByteCode = ShaderBytecode.CompileFromFile("MiniTri.fx", "fx_5_0", ShaderFlags.None, EffectFlags.None);
#endif
            effect = new Effect(device, effectByteCode);
			// Compile Vertex and Pixel shaders
			//vertexShaderByteCode = ShaderBytecode.CompileFromFile("MiniTri.fx", "VS", "vs_5_0");
			//vertexShader = new VertexShader(device, vertexShaderByteCode);

			//pixelShaderByteCode = ShaderBytecode.CompileFromFile("MiniTri.fx", "PS", "ps_5_0");
			//pixelShader = new PixelShader(device, pixelShaderByteCode);

			// Create Constant Buffer
			//constantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
			int tempSize = System.Runtime.InteropServices.Marshal.SizeOf(new ShaderParamStruct());
			constantBuffer = new Buffer(device, tempSize, ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

			// Create Depth Buffer & View
			var depthBuffer = new Texture2D(device, new Texture2DDescription()
			{
				Format = Format.D32_Float_S8X24_UInt,
				ArraySize = 1,
				MipLevels = 1,
				Width = form.ClientSize.Width,
				Height = form.ClientSize.Height,
				SampleDescription = new SampleDescription(1, 0),
				Usage = ResourceUsage.Default,
				BindFlags = BindFlags.DepthStencil,
				CpuAccessFlags = CpuAccessFlags.None,
				OptionFlags = ResourceOptionFlags.None
			});

			depthView = new DepthStencilView(device, depthBuffer);
			//RasterizerStateDescription rsd = new RasterizerStateDescription() { IsMultisampleEnabled = true };
			//rsd.CullMode = CullMode.Back;
			//rsd.FillMode = FillMode.Solid;
			//rsd.IsMultisampleEnabled = true;
			//rsd.IsAntialiasedLineEnabled = false;
			//rsd.IsDepthClipEnabled = false;
			//rsd.IsScissorEnabled = false;
			//RasterizerState rs = new RasterizerState(device, rsd);
			//device.ImmediateContext.Rasterizer.State = rs;
			//rs.Dispose();

			depthBuffer.Dispose();

			texMan = new TextureManager();
		}

		public void ShutdownDX()
		{
			//vertexShaderByteCode.Dispose();
			//vertexShader.Dispose();
			//pixelShaderByteCode.Dispose();
			//pixelShader.Dispose();
			renderView.Dispose();
			backBuffer.Dispose();
			context.ClearState();
			context.Flush();
			device.Dispose();
			context.Dispose();
			swapChain.Dispose();
			factory.Dispose();
		}

	}
}
