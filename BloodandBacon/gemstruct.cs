using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000073 RID: 115
	internal class gemstruct : GameScreen
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x000E59D0 File Offset: 0x000E3BD0
		public void LoadContent(ContentManager content, ScreenManager sc)
		{
			this.crunch = content.Load<SoundEffect>("astro\\Audio\\crunch");
			this.sc = sc;
			this.gr = sc.GraphicsDevice;
			this.rr = new Random();
			this.max = 300;
			this.Trans = new gemstruct.transcolor[this.max];
			this.xTrans = new gemstruct.transcolor[300];
			this.xInst = new gemDupe[300];
			this.shaleBuffer = new DynamicVertexBuffer(this.gr, gemstruct.vd, 3600, BufferUsage.WriteOnly);
			this.shale1Buffer = new DynamicVertexBuffer(this.gr, gemstruct.vd, 200, BufferUsage.WriteOnly);
			for (int i = 0; i < this.Trans.Length; i++)
			{
				Matrix matrix = Matrix.CreateFromYawPitchRoll((float)this.rr.Next(0, 880000) / 900f, (float)this.rr.Next(0, 880000) / 900f, (float)this.rr.Next(0, 880000) / 900f);
				float num = (float)this.rr.Next(10000, 36000) / 10000f;
				Vector3 vector = new Vector3(0f, 0f, 0f);
				this.Trans[i].Trans = Matrix.CreateScale(num) * matrix * Matrix.CreateTranslation(vector);
			}
			int num2 = 0;
			this.pipeShot = Vector3.Transform(this.pipeShot, Matrix.CreateScale(1.5f));
			foreach (Vector3 vector2 in this.origDumper1)
			{
				this.origDumper1[num2] = Vector3.Transform(this.origDumper1[num2], Matrix.CreateScale(1.5f));
				num2++;
			}
			num2 = 0;
			foreach (Vector3 vector3 in this.origRegion1)
			{
				this.origRegion1[num2] = Vector3.Transform(this.origRegion1[num2], Matrix.CreateScale(1.5f));
				num2++;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000E5C29 File Offset: 0x000E3E29
		public override void UnloadContent()
		{
			this.content.Unload();
			this.shaleBuffer.Dispose();
			this.shale1Buffer.Dispose();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000E5C4C File Offset: 0x000E3E4C
		public void uptheRamp(int val, ref Rover rover, gemDupe v, ref gemDupe[] gemxInst, ref int gemxCount, ref gemstruct.transcolor[] transx, ref int gemxtrack)
		{
			if (v.onramp == 5)
			{
				return;
			}
			if (v.onramp == 1)
			{
				v.rampy = gemstruct.setitRight;
				v.velocity += new Vector3(0f, 0.03f, 0.1f);
				if (v.mypos.Z > this.origRegion1[2].Z * 30f)
				{
					if (gemstruct.totalGems > 250)
					{
						this.crunch.Play(this.sc.ev, 0.3f, 0f);
						v.mypos = rover.position + Vector3.Transform(new Vector3(-14.25f, 94.5f, -7.9500003f), rover.orientation);
						v.velocity = Vector3.Transform(new Vector3((float)this.rr.Next(-100, 90) / 50f, 3f, 6f), rover.orientation);
						v.rampy = Matrix.Identity;
						v.onramp = 5;
						v.move = true;
						return;
					}
					v.onramp = 2;
					v.mypos = this.pipeShot * 30f;
					v.velocity = new Vector3((float)this.rr.Next(-300, 700) / 300f, (float)this.rr.Next(-300, 300) / 200f + (float)(gemstruct.totalGems / 60) * v.scaler / 15f, (float)this.rr.Next(400, 2400) / 300f);
					this.crunch.Play(this.sc.ev, 0f, 0f);
					gemstruct.totalGems++;
					v.onramp = -1;
					v.rampy = Matrix.Identity;
					v.move = false;
					if (gemxCount < this.maxgemx)
					{
						gemxInst[gemxtrack] = new gemDupe(this.sc, v.mypos, Vector3.Zero, true, this.rr.Next(2, 100));
					}
					gemxInst[gemxtrack].mypos = v.mypos;
					gemxInst[gemxtrack].velocity = v.velocity;
					gemxInst[gemxtrack].scaler = v.scaler;
					gemxInst[gemxtrack].onramp = 2;
					gemxCount++;
					if (gemxCount > this.maxgemx - 1)
					{
						gemxCount = this.maxgemx - 1;
					}
					gemxtrack++;
					if (gemxtrack > this.maxgemx - 1)
					{
						gemxtrack = 0;
						return;
					}
				}
				else
				{
					if (v.mypos.X > this.origRegion1[2].X * 30f || v.mypos.X < this.origRegion1[3].X * 30f)
					{
						v.velocity.X = -v.velocity.X / 2f;
					}
					v.velocity.X = v.velocity.X + (0f - v.mypos.X) / 140f;
					v.groundHeight = 0f;
					v.normal = Vector3.Up;
				}
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000E5FAC File Offset: 0x000E41AC
		public void hitDumper(gemDupe v)
		{
			if (v.onramp == 3)
			{
				v.rampy = gemstruct.inDumper;
				return;
			}
			if (v.onramp == 2)
			{
				v.rampy = gemstruct.inDumper;
				v.velocity.Y = v.velocity.Y + -0.2f;
				float num = v.scaler * 1f;
				float num2 = MathHelper.Lerp(this.origDumper1[1].X * 30f - num, this.origDumper1[2].X * 30f - num, (v.mypos.Z - this.origDumper1[0].Z * 30f) / (this.origDumper1[3].Z * 30f - this.origDumper1[0].Z * 30f));
				if (v.mypos.X > num2)
				{
					v.mypos.X = num2;
					v.velocity.X = -v.velocity.X / 1.5f;
				}
				else
				{
					num2 = MathHelper.Lerp(this.origDumper1[0].X * 30f + num, this.origDumper1[3].X * 30f + num, (v.mypos.Z - this.origDumper1[0].Z * 30f) / (this.origDumper1[3].Z * 30f - this.origDumper1[0].Z * 30f));
					if (v.mypos.X < num2)
					{
						v.mypos.X = num2;
						v.velocity.X = -v.velocity.X / 1.5f;
					}
				}
				if (v.mypos.Z > this.origDumper1[2].Z * 30f - num)
				{
					v.mypos.Z = this.origDumper1[2].Z * 30f - num;
					v.velocity.Z = -v.velocity.Z / 1.5f;
				}
				else if (v.mypos.Z < this.origDumper1[1].Z * 30f + num && v.velocity.Z < 0f)
				{
					v.mypos.Z = this.origDumper1[1].Z * 30f + num;
					v.velocity.Z = -v.velocity.Z / 1.5f;
				}
				v.groundHeight = this.origDumper1[0].Y * 30f + (float)(gemstruct.totalGems / 60) * v.scaler;
				v.normal = Vector3.Up;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000E62B0 File Offset: 0x000E44B0
		public void drawShale(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			this.DrawInstance(this.model, this.Trans, this.Inst.Count, this.shaleBuffer, light, amb, diff, view, proj);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000E62E8 File Offset: 0x000E44E8
		public void drawShaleInDumper(Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			this.DrawInstance(this.model, this.xTrans, this.xCount, this.shale1Buffer, light, amb, diff, view, proj);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000E631C File Offset: 0x000E451C
		public void DrawInstance(Model model, gemstruct.transcolor[] minstances, int cc, DynamicVertexBuffer dd, Vector3 light, Vector3 amb, Vector3 diff, Matrix view, Matrix proj)
		{
			if (cc == 0)
			{
				return;
			}
			ModelMeshPart modelMeshPart = model.Meshes[0].MeshParts[0];
			dd.SetData<gemstruct.transcolor>(minstances, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(dd, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.gr.Indices = modelMeshPart.IndexBuffer;
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["fastShader"];
			effect.Parameters["View"].SetValue(view);
			effect.Parameters["Projection"].SetValue(proj);
			effect.Parameters["LightDirection"].SetValue(light);
			effect.Parameters["diff"].SetValue(diff);
			effect.Parameters["amb"].SetValue(amb);
			effect.CurrentTechnique.Passes[0].Apply();
			this.gr.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x0400105C RID: 4188
		public gemstruct.gemtype gtype;

		// Token: 0x0400105D RID: 4189
		public static int totalGems = 0;

		// Token: 0x0400105E RID: 4190
		public static Matrix setitRight = Matrix.Identity;

		// Token: 0x0400105F RID: 4191
		public static Matrix setVacuum = Matrix.Identity;

		// Token: 0x04001060 RID: 4192
		public static Matrix inDumper = Matrix.Identity;

		// Token: 0x04001061 RID: 4193
		private Vector3[] origDumper1 = new Vector3[]
		{
			new Vector3(-0.555f, 1.421f, 0.057f),
			new Vector3(0.529f, 1.421f, 0.057f),
			new Vector3(0.673f, 1.446f, 1.894f),
			new Vector3(-0.699f, 1.446f, 1.894f)
		};

		// Token: 0x04001062 RID: 4194
		private Vector3[] origRegion1 = new Vector3[]
		{
			new Vector3(-0.536f, 0.05f, -2.2f),
			new Vector3(0.543f, 0.05f, -2.2f),
			new Vector3(0.498f, 0f, -0.007f),
			new Vector3(-0.525f, 0f, -0.007f),
			new Vector3(-0.536f, 0.388f, -1.937f)
		};

		// Token: 0x04001063 RID: 4195
		private Vector3 pipeShot = new Vector3(-0.31384f, 2.13954f, -0.19438f);

		// Token: 0x04001064 RID: 4196
		public Model model;

		// Token: 0x04001065 RID: 4197
		public List<gemDupe> Inst = new List<gemDupe>();

		// Token: 0x04001066 RID: 4198
		public int max = 300;

		// Token: 0x04001067 RID: 4199
		public int maxgemx = 200;

		// Token: 0x04001068 RID: 4200
		public gemDupe[] xInst;

		// Token: 0x04001069 RID: 4201
		public int xTrack;

		// Token: 0x0400106A RID: 4202
		private int bitmap = 290;

		// Token: 0x0400106B RID: 4203
		private int grid = 150;

		// Token: 0x0400106C RID: 4204
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x0400106D RID: 4205
		private DynamicVertexBuffer shaleBuffer;

		// Token: 0x0400106E RID: 4206
		private DynamicVertexBuffer shale1Buffer;

		// Token: 0x0400106F RID: 4207
		private static VertexDeclaration vd = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
		});

		// Token: 0x04001070 RID: 4208
		public int xCount;

		// Token: 0x04001071 RID: 4209
		public gemstruct.transcolor[] Trans;

		// Token: 0x04001072 RID: 4210
		public gemstruct.transcolor[] xTrans;

		// Token: 0x04001073 RID: 4211
		private ScreenManager sc;

		// Token: 0x04001074 RID: 4212
		private GraphicsDevice gr;

		// Token: 0x04001075 RID: 4213
		private Random rr;

		// Token: 0x04001076 RID: 4214
		private SoundEffect crunch;

		// Token: 0x04001077 RID: 4215
		private ContentManager content;

		// Token: 0x02000074 RID: 116
		public enum gemtype
		{
			// Token: 0x04001079 RID: 4217
			shale,
			// Token: 0x0400107A RID: 4218
			ruby,
			// Token: 0x0400107B RID: 4219
			sapphire,
			// Token: 0x0400107C RID: 4220
			chaff,
			// Token: 0x0400107D RID: 4221
			iron,
			// Token: 0x0400107E RID: 4222
			salt,
			// Token: 0x0400107F RID: 4223
			emerald
		}

		// Token: 0x02000075 RID: 117
		public struct transcolor : IVertexType
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x000E66DC File Offset: 0x000E48DC
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return gemstruct.transcolor.VertexDeclaration;
				}
			}

			// Token: 0x04001080 RID: 4224
			public Matrix Trans;

			// Token: 0x04001081 RID: 4225
			public float Color;

			// Token: 0x04001082 RID: 4226
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
			});
		}
	}
}
