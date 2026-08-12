using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000E1 RID: 225
	internal class InstancedModelPart
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x001CAE14 File Offset: 0x001C9014
		internal InstancedModelPart(ContentReader input, GraphicsDevice graphicsDevice)
		{
			this.graphicsDevice = graphicsDevice;
			this.indexCount = input.ReadInt32();
			this.vertexCount = input.ReadInt32();
			this.vertexStride = input.ReadInt32();
			this.vertexDeclaration = input.ReadObject<VertexDeclaration>();
			this.vertexBuffer = input.ReadObject<VertexBuffer>();
			this.indexBuffer = input.ReadObject<IndexBuffer>();
			input.ReadSharedResource<Effect>(delegate(Effect value)
			{
				this.effect = value;
			});
			this.content = new ContentManager(input.ContentManager.ServiceProvider, "Content");
			int num = 65535 / this.vertexCount;
			this.maxInstances = Math.Min(num, 60);
			this.ReplicateIndexData();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x001CAF00 File Offset: 0x001C9100
		private void ReplicateIndexData()
		{
			ushort[] array = new ushort[this.indexCount];
			this.indexBuffer.GetData<ushort>(array);
			this.indexBuffer.Dispose();
			ushort[] array2 = new ushort[this.indexCount * this.maxInstances];
			int num = 0;
			for (int i = 0; i < this.maxInstances; i++)
			{
				int num2 = i * this.vertexCount;
				for (int j = 0; j < this.indexCount; j++)
				{
					array2[num] = (ushort)((int)array[j] + num2);
					num++;
				}
			}
			this.indexBuffer = new IndexBuffer(this.graphicsDevice, typeof(VertexPositionColor), 2 * array2.Length, BufferUsage.None);
			this.indexBuffer.SetData<ushort>(array2);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x001CAFB1 File Offset: 0x001C91B1
		public void DrawGems(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, Vector3 vpos)
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x001CAFB3 File Offset: 0x001C91B3
		private void DrawShader60gems(ref Matrix[] iTT)
		{
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x001CAFB5 File Offset: 0x001C91B5
		public void breakage(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, int count)
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x001CAFB8 File Offset: 0x001C91B8
		public void boulders(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, int count)
		{
			this.graphicsDevice.Indices = this.indexBuffer;
			this.effect.CurrentTechnique = this.effect.Techniques["boulders"];
			this.effect.Parameters["View"].SetValue(view);
			this.effect.Parameters["Projection"].SetValue(projection);
			this.effect.Parameters["VertexCount"].SetValue(this.vertexCount);
			this.effect.Parameters["LightDirection"].SetValue(light);
			this.effect.Parameters["DiffuseLight"].SetValue(diffu);
			this.effect.Parameters["AmbientLight"].SetValue(amb);
			this.DrawShaderRocks(ref instanceTransforms, count);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x001CB0AC File Offset: 0x001C92AC
		private void DrawShaderRocks(ref Matrix[] iTT, int count)
		{
			for (int i = 0; i < count; i += this.maxInstances)
			{
				int num = count - i;
				if (num > this.maxInstances)
				{
					num = this.maxInstances;
				}
				Array.Copy(iTT, i, this.tempMatrices, 0, num);
				this.effect.Parameters["InstanceTransforms"].SetValue(this.tempMatrices);
				this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, num * this.vertexCount, 0, num * this.indexCount / 3);
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x001CB12F File Offset: 0x001C932F
		public void smallGems(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x001CB131 File Offset: 0x001C9331
		private void DrawShaderGems(ref Matrix[] iTT)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x001CB133 File Offset: 0x001C9333
		public void chain(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, int start)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x001CB135 File Offset: 0x001C9335
		private void DrawChain(ref Matrix[] iTT, int start)
		{
		}

		// Token: 0x04001FCA RID: 8138
		private const int MaxShaderMatrices = 60;

		// Token: 0x04001FCB RID: 8139
		private const int SizeOfVector4 = 16;

		// Token: 0x04001FCC RID: 8140
		private const int SizeOfMatrix = 64;

		// Token: 0x04001FCD RID: 8141
		private int indexCount;

		// Token: 0x04001FCE RID: 8142
		private int vertexCount;

		// Token: 0x04001FCF RID: 8143
		private int vertexStride;

		// Token: 0x04001FD0 RID: 8144
		public List<int> show = new List<int>();

		// Token: 0x04001FD1 RID: 8145
		private VertexDeclaration vertexDeclaration;

		// Token: 0x04001FD2 RID: 8146
		private VertexBuffer vertexBuffer;

		// Token: 0x04001FD3 RID: 8147
		private IndexBuffer indexBuffer;

		// Token: 0x04001FD4 RID: 8148
		private Effect effect;

		// Token: 0x04001FD5 RID: 8149
		private GraphicsDevice graphicsDevice;

		// Token: 0x04001FD6 RID: 8150
		private int maxInstances;

		// Token: 0x04001FD7 RID: 8151
		private Matrix[] tempMatrices = new Matrix[60];

		// Token: 0x04001FD8 RID: 8152
		private Matrix[] fortyMatrices = new Matrix[40];

		// Token: 0x04001FD9 RID: 8153
		private int[] tempColors = new int[40];

		// Token: 0x04001FDA RID: 8154
		private ContentManager content;
	}
}
