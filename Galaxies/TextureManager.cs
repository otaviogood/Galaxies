using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Galaxies
{
	public class TextureManager
	{
		public Dictionary<string, Texture> texDict = new Dictionary<string, Texture>();

		public TextureManager()
		{
			DateTime dt = DateTime.Now;
			string[] allfiles = Directory.GetFiles(@"Content", "*.png");
			allfiles = allfiles.Concat(Directory.GetFiles(@"Content", "*.jpg")).ToArray();
			allfiles = allfiles.Concat(Directory.GetFiles(@"Content", "*.bmp")).ToArray();
			allfiles = allfiles.Concat(Directory.GetFiles(@"Content", "*.dds")).ToArray();
			foreach (string s in allfiles)
			{
				string chopped = s.Replace(".png", "");
				chopped = chopped.Replace(".jpg", "");
				chopped = chopped.Replace(".bmp", "");
				chopped = chopped.Replace(".dds", "");
				chopped = chopped.Replace(".tiff", "");
				chopped = chopped.Replace(".tif", "");
				string[] split = chopped.Split(@"\/".ToCharArray());
				chopped = split[split.Length - 1];
				Texture texTemp = new Texture();

				//var ili = new ImageLoadInformation();
				//ili.OptionFlags = ResourceOptionFlags.
				SharpDX.Direct3D11.Resource tex = Texture2D.FromFile<Texture2D>(Global._G.device, s);
				//SharpDX.Direct3D11.Texture2D tex = Texture2D.FromFile<Texture2D>(Global._G.device, s);
				texTemp.textureView = new ShaderResourceView(Global._G.device, tex);
				tex.Dispose();
				texDict.Add(chopped, texTemp);
			}
			Debug.WriteLine("Texture manager load time: " + (DateTime.Now - dt).TotalMilliseconds);
			// time to load 8192x8192:
			// jpg (high compression)			2.5 sec		9.37MB
			// png 24 bit						3.8 sec		36.3MB
			// bmp 24 bit						2.3 sec		192MB
			// dds R8G8B8						3.5 sec		30.6MB
			// dds dxt1							3.5 sec		32.0MB
		}
	}
}
