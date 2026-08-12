using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Blood
{
	// Token: 0x02000013 RID: 19
	public abstract class ParticleSystem : GameScreen
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0002B149 File Offset: 0x00029349
		public ParticleSystem(Game game, ContentManager content)
		{
			this.content = content;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0002B163 File Offset: 0x00029363
		public void Initialize()
		{
			this.InitializeSettings(this.settings);
		}

		// Token: 0x06000107 RID: 263
		protected abstract void InitializeSettings(ParticleSettings settings);

		// Token: 0x06000108 RID: 264 RVA: 0x0002B171 File Offset: 0x00029371
		public void unloadContent()
		{
			this.content.Unload();
			this.vertexBuffer.Dispose();
			this.indexBuffer.Dispose();
			this.particles = new ParticleSystem.ParticleVertex[0];
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0002B1A0 File Offset: 0x000293A0
		public void LoadContent(GraphicsDevice gr)
		{
			this.gr = gr;
			this.particles = new ParticleSystem.ParticleVertex[this.settings.MaxParticles * 4];
			for (int i = 0; i < this.settings.MaxParticles; i++)
			{
				this.particles[i * 4].Corner = new Short2(-1f, -1f);
				this.particles[i * 4 + 1].Corner = new Short2(1f, -1f);
				this.particles[i * 4 + 2].Corner = new Short2(1f, 1f);
				this.particles[i * 4 + 3].Corner = new Short2(-1f, 1f);
			}
			this.LoadParticleEffect();
			this.vertexBuffer = new DynamicVertexBuffer(gr, ParticleSystem.ParticleVertex.VertexDeclaration, this.settings.MaxParticles * 4, BufferUsage.WriteOnly);
			uint[] array = new uint[this.settings.MaxParticles * 6];
			for (int j = 0; j < this.settings.MaxParticles; j++)
			{
				array[j * 6] = (uint)(j * 4);
				array[j * 6 + 1] = (uint)(j * 4 + 1);
				array[j * 6 + 2] = (uint)(j * 4 + 2);
				array[j * 6 + 3] = (uint)(j * 4);
				array[j * 6 + 4] = (uint)(j * 4 + 2);
				array[j * 6 + 5] = (uint)(j * 4 + 3);
			}
			this.indexBuffer = new IndexBuffer(gr, typeof(uint), array.Length, BufferUsage.WriteOnly);
			this.indexBuffer.SetData<uint>(array);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0002B32C File Offset: 0x0002952C
		private void LoadParticleEffect()
		{
			Effect effect = this.content.Load<Effect>("effects\\ParticleEffect");
			Texture2D texture2D = this.content.Load<Texture2D>("particles\\" + this.settings.TextureName);
			this.particleEffect = effect.Clone();
			EffectParameterCollection parameters = this.particleEffect.Parameters;
			this.effectViewParameter = parameters["View"];
			this.effectProjectionParameter = parameters["Projection"];
			this.effectTimeParameter = parameters["CurrentTime"];
			this.effectTexture = parameters["Texture"];
			parameters["Duration"].SetValue((float)this.settings.Duration.TotalSeconds);
			parameters["DurationRandomness"].SetValue(this.settings.DurationRandomness);
			parameters["Gravity"].SetValue(this.settings.Gravity);
			parameters["EndVelocity"].SetValue(this.settings.EndVelocity);
			parameters["MinColor"].SetValue(this.settings.MinColor.ToVector4());
			parameters["MaxColor"].SetValue(this.settings.MaxColor.ToVector4());
			parameters["RotateSpeed"].SetValue(new Vector2(this.settings.MinRotateSpeed, this.settings.MaxRotateSpeed));
			parameters["StartSize"].SetValue(new Vector2(this.settings.MinStartSize, this.settings.MaxStartSize));
			parameters["EndSize"].SetValue(new Vector2(this.settings.MinEndSize, this.settings.MaxEndSize));
			parameters["Texture"].SetValue(texture2D);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0002B50C File Offset: 0x0002970C
		public void Update(GameTime gameTime)
		{
			if (this.particles.Length <= 0)
			{
				return;
			}
			this.currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			this.RetireActiveParticles();
			this.FreeRetiredParticles();
			if (this.firstActiveParticle == this.firstFreeParticle)
			{
				this.currentTime = 0f;
			}
			if (this.firstRetiredParticle == this.firstActiveParticle)
			{
				this.drawCounter = 0;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0002B57B File Offset: 0x0002977B
		public void resetParticleTime()
		{
			this.firstActiveParticle = 0;
			this.firstNewParticle = 0;
			this.firstFreeParticle = 0;
			this.firstRetiredParticle = 0;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0002B59C File Offset: 0x0002979C
		private void RetireActiveParticles()
		{
			float num = (float)this.settings.Duration.TotalSeconds;
			while (this.firstActiveParticle != this.firstNewParticle)
			{
				float num2 = this.currentTime - this.particles[this.firstActiveParticle * 4].Time;
				if (num2 < num)
				{
					return;
				}
				this.particles[this.firstActiveParticle * 4].Time = (float)this.drawCounter;
				this.firstActiveParticle++;
				if (this.firstActiveParticle >= this.settings.MaxParticles)
				{
					this.firstActiveParticle = 0;
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0002B638 File Offset: 0x00029838
		private void FreeRetiredParticles()
		{
			while (this.firstRetiredParticle != this.firstActiveParticle)
			{
				int num = this.drawCounter - (int)this.particles[this.firstRetiredParticle * 4].Time;
				if (num < 4)
				{
					return;
				}
				this.firstRetiredParticle++;
				if (this.firstRetiredParticle >= this.settings.MaxParticles)
				{
					this.firstRetiredParticle = 0;
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public void Draw(int tech)
		{
			if (this.firstNewParticle != this.firstFreeParticle)
			{
				this.AddNewParticlesToVertexBuffer();
			}
			if (this.firstActiveParticle != this.firstFreeParticle)
			{
				this.gr.BlendState = this.settings.BlendState;
				this.effectTimeParameter.SetValue(this.currentTime);
				this.gr.SetVertexBuffer(this.vertexBuffer);
				this.gr.Indices = this.indexBuffer;
				this.particleEffect.CurrentTechnique.Passes[tech].Apply();
				if (this.firstActiveParticle < this.firstFreeParticle)
				{
					this.gr.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, this.firstActiveParticle * 4, (this.firstFreeParticle - this.firstActiveParticle) * 4, this.firstActiveParticle * 6, (this.firstFreeParticle - this.firstActiveParticle) * 2);
				}
				else
				{
					this.gr.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, this.firstActiveParticle * 4, (this.settings.MaxParticles - this.firstActiveParticle) * 4, this.firstActiveParticle * 6, (this.settings.MaxParticles - this.firstActiveParticle) * 2);
					if (this.firstFreeParticle > 0)
					{
						this.gr.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.firstFreeParticle * 4, 0, this.firstFreeParticle * 2);
					}
				}
			}
			this.drawCounter++;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0002B800 File Offset: 0x00029A00
		private void AddNewParticlesToVertexBuffer()
		{
			int num = 36;
			if (this.firstNewParticle < this.firstFreeParticle)
			{
				this.vertexBuffer.SetData<ParticleSystem.ParticleVertex>(this.firstNewParticle * num * 4, this.particles, this.firstNewParticle * 4, (this.firstFreeParticle - this.firstNewParticle) * 4, num, SetDataOptions.NoOverwrite);
			}
			else
			{
				this.vertexBuffer.SetData<ParticleSystem.ParticleVertex>(this.firstNewParticle * num * 4, this.particles, this.firstNewParticle * 4, (this.settings.MaxParticles - this.firstNewParticle) * 4, num, SetDataOptions.NoOverwrite);
				if (this.firstFreeParticle > 0)
				{
					this.vertexBuffer.SetData<ParticleSystem.ParticleVertex>(0, this.particles, 0, this.firstFreeParticle * 4, num, SetDataOptions.NoOverwrite);
				}
			}
			this.firstNewParticle = this.firstFreeParticle;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public void setPaintball(Texture2D tt, Vector4 min, Vector4 max)
		{
			EffectParameterCollection parameters = this.particleEffect.Parameters;
			parameters["Texture"].SetValue(tt);
			parameters["MinColor"].SetValue(min);
			parameters["MaxColor"].SetValue(max);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0002B90C File Offset: 0x00029B0C
		public void setDuration(float secs)
		{
			this.settings.Duration = TimeSpan.FromSeconds((double)secs);
			EffectParameterCollection parameters = this.particleEffect.Parameters;
			parameters["Duration"].SetValue((float)this.settings.Duration.TotalSeconds);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0002B958 File Offset: 0x00029B58
		public void SetCamera(Matrix view, Matrix projection)
		{
			this.effectViewParameter.SetValue(view);
			this.effectProjectionParameter.SetValue(projection);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0002B974 File Offset: 0x00029B74
		public void AddParticle(Vector3 position, Vector3 velocity)
		{
			int num = this.firstFreeParticle + 1;
			if (num >= this.settings.MaxParticles)
			{
				num = 0;
			}
			if (num == this.firstRetiredParticle)
			{
				return;
			}
			velocity *= this.settings.EmitterVelocitySensitivity;
			Color color = new Color((int)((byte)ParticleSystem.random.Next(255)), (int)((byte)ParticleSystem.random.Next(255)), (int)((byte)ParticleSystem.random.Next(255)), (int)((byte)ParticleSystem.random.Next(255)));
			for (int i = 0; i < 4; i++)
			{
				this.particles[this.firstFreeParticle * 4 + i].Position = position;
				this.particles[this.firstFreeParticle * 4 + i].Velocity = velocity;
				this.particles[this.firstFreeParticle * 4 + i].Random = color;
				this.particles[this.firstFreeParticle * 4 + i].Time = this.currentTime;
			}
			this.firstFreeParticle = num;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0002BA84 File Offset: 0x00029C84
		public void AddParticle2(Vector3 position, Vector3 velocity, int mysize, int mycolor)
		{
			int num = this.firstFreeParticle + 1;
			if (num >= this.settings.MaxParticles)
			{
				num = 0;
			}
			if (num == this.firstRetiredParticle)
			{
				return;
			}
			velocity *= this.settings.EmitterVelocitySensitivity;
			Color color = new Color((int)((byte)ParticleSystem.random.Next(255)), (int)((byte)ParticleSystem.random.Next(255)), (int)((byte)(mysize + 1)), (int)((byte)(mycolor + 1)));
			for (int i = 0; i < 4; i++)
			{
				this.particles[this.firstFreeParticle * 4 + i].Position = position;
				this.particles[this.firstFreeParticle * 4 + i].Velocity = velocity;
				this.particles[this.firstFreeParticle * 4 + i].Random = color;
				this.particles[this.firstFreeParticle * 4 + i].Time = this.currentTime;
			}
			this.firstFreeParticle = num;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0002BB7C File Offset: 0x00029D7C
		public void AddParticle3(Vector3 position, Vector3 velocity, int mycolor)
		{
			int num = this.firstFreeParticle + 1;
			if (num >= this.settings.MaxParticles)
			{
				num = 0;
			}
			if (num == this.firstRetiredParticle)
			{
				return;
			}
			velocity *= this.settings.EmitterVelocitySensitivity;
			Color color = new Color((int)((byte)ParticleSystem.random.Next(255)), (int)((byte)ParticleSystem.random.Next(255)), (int)((byte)ParticleSystem.random.Next(255)), (int)((byte)(mycolor + 1)));
			for (int i = 0; i < 4; i++)
			{
				this.particles[this.firstFreeParticle * 4 + i].Position = position;
				this.particles[this.firstFreeParticle * 4 + i].Velocity = velocity;
				this.particles[this.firstFreeParticle * 4 + i].Random = color;
				this.particles[this.firstFreeParticle * 4 + i].Time = this.currentTime;
			}
			this.firstFreeParticle = num;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0002BC80 File Offset: 0x00029E80
		public void AddParticle5(Vector3 position, Vector3 velocity, int mysize, int mycolorx, int mycolory, int mycolorz)
		{
			int num = this.firstFreeParticle + 1;
			if (num >= this.settings.MaxParticles)
			{
				num = 0;
			}
			if (num == this.firstRetiredParticle)
			{
				return;
			}
			velocity *= this.settings.EmitterVelocitySensitivity;
			Color color = new Color((int)((byte)mysize), (int)((byte)mycolorx), (int)((byte)mycolory), (int)((byte)mycolorz));
			for (int i = 0; i < 4; i++)
			{
				this.particles[this.firstFreeParticle * 4 + i].Position = position;
				this.particles[this.firstFreeParticle * 4 + i].Velocity = velocity;
				this.particles[this.firstFreeParticle * 4 + i].Random = color;
				this.particles[this.firstFreeParticle * 4 + i].Time = this.currentTime;
			}
			this.firstFreeParticle = num;
		}

		// Token: 0x040005C7 RID: 1479
		public ParticleSettings settings = new ParticleSettings();

		// Token: 0x040005C8 RID: 1480
		public static BlendState lastBlend = new BlendState();

		// Token: 0x040005C9 RID: 1481
		private ContentManager content;

		// Token: 0x040005CA RID: 1482
		private Effect particleEffect;

		// Token: 0x040005CB RID: 1483
		private EffectParameter effectViewParameter;

		// Token: 0x040005CC RID: 1484
		private EffectParameter effectProjectionParameter;

		// Token: 0x040005CD RID: 1485
		private EffectParameter effectTimeParameter;

		// Token: 0x040005CE RID: 1486
		private EffectParameter effectTexture;

		// Token: 0x040005CF RID: 1487
		private ParticleSystem.ParticleVertex[] particles;

		// Token: 0x040005D0 RID: 1488
		private DynamicVertexBuffer vertexBuffer;

		// Token: 0x040005D1 RID: 1489
		private IndexBuffer indexBuffer;

		// Token: 0x040005D2 RID: 1490
		private int firstActiveParticle;

		// Token: 0x040005D3 RID: 1491
		private int firstNewParticle;

		// Token: 0x040005D4 RID: 1492
		private int firstFreeParticle;

		// Token: 0x040005D5 RID: 1493
		private int firstRetiredParticle;

		// Token: 0x040005D6 RID: 1494
		private float currentTime;

		// Token: 0x040005D7 RID: 1495
		private int drawCounter;

		// Token: 0x040005D8 RID: 1496
		private static Random random = new Random();

		// Token: 0x040005D9 RID: 1497
		public GraphicsDevice gr;

		// Token: 0x02000014 RID: 20
		private struct ParticleVertex
		{
			// Token: 0x040005DA RID: 1498
			public const int SizeInBytes = 36;

			// Token: 0x040005DB RID: 1499
			public Short2 Corner;

			// Token: 0x040005DC RID: 1500
			public Vector3 Position;

			// Token: 0x040005DD RID: 1501
			public Vector3 Velocity;

			// Token: 0x040005DE RID: 1502
			public Color Random;

			// Token: 0x040005DF RID: 1503
			public float Time;

			// Token: 0x040005E0 RID: 1504
			public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Short2, VertexElementUsage.Position, 0),
				new VertexElement(4, VertexElementFormat.Vector3, VertexElementUsage.Position, 1),
				new VertexElement(16, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
				new VertexElement(28, VertexElementFormat.Color, VertexElementUsage.Color, 0),
				new VertexElement(32, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 0)
			});
		}
	}
}
