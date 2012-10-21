using System;
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
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            RenderForm form = new RenderForm("Galaxies - test");
			form.ClientSize = new System.Drawing.Size(800, 800);

#if DEBUG
            Configuration.EnableObjectTracking = true;	// some sharpDX COM object tracking that slows performance.
#endif


			Global._G.InitDX(form);

			Mesh mesh = new Mesh();
			//mesh.GenCube();
			mesh.MakeLatLonSphere2(64, 64);

			// Prepare All the stages
			Global._G.context.Rasterizer.SetViewports(new Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height, 0.0f, 1.0f));
			Global._G.context.OutputMerger.SetTargets(Global._G.depthView, Global._G.renderView);

			// Prepare matrices
			var view = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
			var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, form.ClientSize.Width / (float)form.ClientSize.Height, 0.1f, 100.0f);
			var viewProj = Matrix.Multiply(view, proj);
			Global._G.shaderParams.sunPos = new Vector4(0, 5, 0, 1);
			Global._G.shaderParams.cameraPos = new Vector4(0, 0, -3, 1);
			Global._G.shaderParams.view = Matrix.LookAtLH(new Vector3(0, 0, -3), new Vector3(0, 0, 0), Vector3.UnitY);
			Global._G.shaderParams.proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, form.ClientSize.Width / (float)form.ClientSize.Height, 0.1f, 100.0f);
			Global._G.shaderParams.view.Transpose();
			Global._G.shaderParams.proj.Transpose();


			// Use clock
			var clock = new Stopwatch();
			clock.Start();

			// Main loop
			RenderLoop.Run(form, () =>
			{
				var time = clock.ElapsedMilliseconds / 1000.0f;

				// Clear views
				Global._G.context.ClearDepthStencilView(Global._G.depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
				Global._G.context.ClearRenderTargetView(Global._G.renderView, Color.Black);

				// Update WorldViewProj Matrix
				var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;
				Global._G.shaderParams.world = Matrix.RotationX(time) *Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);
				Global._G.shaderParams.world.Transpose();
				worldViewProj.Transpose();
				//Global._G.context.UpdateSubresource(ref worldViewProj, Global._G.constantBuffer);
				Global._G.context.UpdateSubresource(ref Global._G.shaderParams, Global._G.constantBuffer);

				// Draw the cube
				mesh.Render();

				// Present!
				Global._G.swapChain.Present(0, PresentFlags.None);
			});

			// Release all resources
			Global._G.ShutdownDX();
		}
    }
}

