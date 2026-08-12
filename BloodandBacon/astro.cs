using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;

namespace Blood
{
	// Token: 0x02000133 RID: 307
	public class astro : GameScreen
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x002DFC90 File Offset: 0x002DDE90
		public void LoadContent(ContentManager content, ScreenManager sc)
		{
			this.sc = sc;
			this.rand1 = new Random();
			this.parts = new List<int>(5);
			this.s = new int[5];
			this.man = default(astro.npc);
			this.man.alive = 0;
			this.man.alive2 = 55555;
			this.man.data = sc.astroModel.Tag as SkinningDataX;
			this.makeImage(this.man.data.Bones, this.man.data.Width, this.man.data.Hite, ref this.man.bitmap);
			this.man.model1 = sc.astroModel;
			this.man.man1Texture = content.Load<Texture2D>("astro\\npc\\SpacesuitRe");
			this.man.max = 600;
			this.man.dupe = new List<astroDupe>(this.man.max);
			this.man.dupe.Capacity = 600;
			this.man.displayBody1 = new astro.skinstream[this.man.max];
			this.man.displayBody2 = new astro.skinstream[this.man.max];
			this.man.displayHand1 = new astro.skinstream[this.man.max];
			this.man.displayFist = new astro.skinstream[this.man.max];
			this.man.displayPack1 = new astro.skinstream[this.man.max];
			this.man.displayHelm1 = new astro.skinstream[this.man.max];
			this.man.displayHead1 = new astro.skinstream[this.man.max];
			this.man.displayHammer = new astro.skinstream[this.man.max];
			this.man.bodybuffer1 = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.helmbuffer1 = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.handbuffer1 = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.fistbuffer = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.packbuffer4 = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.headbuffer5 = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.bodybuffer2 = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.hammerbuffer = new DynamicVertexBuffer(sc.GraphicsDevice, astro.vd, this.man.max, BufferUsage.WriteOnly);
			this.man.uv = new Vector3[9];
			this.man.uv[0] = new Vector3(50f, 50f, 1f);
			this.man.uv[1] = new Vector3(0.156f, 0.25300002f, 4f);
			this.man.uv[2] = new Vector3(0.575f, 0.195f, 3f);
			this.man.uv[3] = new Vector3(0.414f, 0.48000002f, 3f);
			this.man.uv[4] = new Vector3(0.58f, 0.48000002f, 3f);
			this.man.uv[5] = new Vector3(0.381f, 0.34899998f, 4f);
			this.man.uv[6] = new Vector3(0.618f, 0.356f, 4f);
			this.man.uv[7] = new Vector3(0.5f, 0.15f, 2f);
			this.man.uv[8] = new Vector3(0.5f, 0.58f, 1.4f);
			this.man.targ = new Matrix[8];
			this.man.targ[0] = Matrix.CreateTranslation(new Vector3(1f, 131f, -124f));
			this.man.targ[1] = Matrix.CreateTranslation(new Vector3(-36.3f, 157.6f, 155.7f));
			this.man.targ[2] = Matrix.CreateTranslation(new Vector3(29f, 157.5f, 155.7f));
			this.man.targ[3] = Matrix.CreateTranslation(new Vector3(-8f, 131.4f, -55f));
			this.man.targ[4] = Matrix.CreateTranslation(new Vector3(8f, 131.4f, -55f));
			this.man.targ[5] = Matrix.CreateTranslation(new Vector3(-30f, 164f, 50f));
			this.man.targ[6] = Matrix.CreateTranslation(new Vector3(30f, 164f, 50f));
			this.man.targ[7] = Matrix.CreateTranslation(new Vector3(-1f, 124f, 232f));
			this.man.bone = new int[] { 2, 7, 7, 24, 19, 16, 12, 10 };
			this.dayEffect = content.Load<Effect>("astro\\shaders\\EffectXbox");
			this.tintEffect = content.Load<Effect>("astro\\shaders\\EffectXboxTint");
			float num = 1f / (float)this.man.data.Width;
			float num2 = 1f / (float)this.man.data.Hite;
			float num3 = num / 2f;
			float num4 = num2 / 2f;
			this.dayEffect.Parameters["BoneDelta"].SetValue(num);
			this.dayEffect.Parameters["RowDelta"].SetValue(num2);
			this.dayEffect.Parameters["halfWidth"].SetValue(num3);
			this.dayEffect.Parameters["halfHeight"].SetValue(num4);
			this.dayEffect.Parameters["Texture"].SetValue(this.man.man1Texture);
			this.dayEffect.Parameters["AnimationTexture"].SetValue(this.man.bitmap);
			this.tintEffect.Parameters["BoneDelta"].SetValue(num);
			this.tintEffect.Parameters["RowDelta"].SetValue(num2);
			this.tintEffect.Parameters["halfWidth"].SetValue(num3);
			this.tintEffect.Parameters["halfHeight"].SetValue(num4);
			this.tintEffect.Parameters["Texture"].SetValue(this.man.man1Texture);
			this.tintEffect.Parameters["AnimationTexture"].SetValue(this.man.bitmap);
			this.man.eff = this.dayEffect;
			astroDupe.sics = new List<int>(2 * (this.man.data.Clips.Length + 1));
			astroDupe.sics.Add(this.man.data.Clips[0]);
			astroDupe.sics.Add(0);
			for (int i = 1; i < this.man.data.Clips.Length; i++)
			{
				astroDupe.sics.Add(this.man.data.Clips[i] - this.man.data.Clips[i - 1]);
				astroDupe.sics.Add(this.man.data.Clips[i - 1]);
			}
			this.spriteBatch = new SpriteBatch(sc.GraphicsDevice);
			this.basiceffect = new BasicEffect(sc.GraphicsDevice)
			{
				TextureEnabled = true,
				VertexColorEnabled = true
			};
			this.sphere = content.Load<Model>("astro\\models\\sphere");
			this.simpEffect = content.Load<Effect>("astro\\shaders\\simple");
			this.greensquare = new Texture2D(sc.GraphicsDevice, 1, 1);
			this.greensquare.SetData<Color>(new Color[] { Color.Green });
			this.redsquare = new Texture2D(sc.GraphicsDevice, 1, 1);
			this.redsquare.SetData<Color>(new Color[] { Color.DarkRed });
			this.whitesquare = new Texture2D(sc.GraphicsDevice, 1, 1);
			this.whitesquare.SetData<Color>(new Color[] { Color.Gray });
			this.buildSeats();
			this.resetStates();
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x002E069E File Offset: 0x002DE89E
		public override void UnloadContent()
		{
			this.man.bitmap.Dispose();
			this.content.Unload();
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x002E06BC File Offset: 0x002DE8BC
		private void makeImage(Matrix[] m, int width, int hite, ref Texture2D tt)
		{
			tt = new Texture2D(this.sc.GraphicsDevice, width, hite, false, SurfaceFormat.Vector4);
			Vector4[] array = new Vector4[width * hite];
			int num = 0;
			for (int i = 0; i < m.Length; i++)
			{
				array[num] = new Vector4(m[i].M11, m[i].M21, m[i].M31, m[i].M41);
				num++;
				array[num] = new Vector4(m[i].M12, m[i].M22, m[i].M32, m[i].M42);
				num++;
				array[num] = new Vector4(m[i].M13, m[i].M23, m[i].M33, m[i].M43);
				num++;
			}
			tt.SetData<Vector4>(array);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x002E07D8 File Offset: 0x002DE9D8
		private void resetStates()
		{
			this.man.hitindex = 0;
			this.man.dupe.Clear();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x002E07F8 File Offset: 0x002DE9F8
		public void dropAstronaut(int seed, Vector2 pos, int amt, astroDupe.emotion emo)
		{
			pos.X = (float)((int)((pos.X + 75f) / 150f) * 150 - 75);
			pos.Y = (float)((int)((pos.Y + 75f) / 150f) * 150 - 75);
			Vector3 zero = Vector3.Zero;
			int num = this.rand1.Next(9000, 22000);
			zero = new Vector3(pos.X, 0f, pos.Y);
			this.man.dupe.Add(new astroDupe(zero, 1, num, 0, this.sc, emo));
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x002E08A8 File Offset: 0x002DEAA8
		public void dropFacilityAstronaut(int seed, Vector2 pos, astroDupe.emotion emo, float rotter)
		{
			int num = this.rand1.Next(14, Facility.reachPlot.Count / 2);
			Vector2 vector = new Vector2(Facility.reachPlot[num * 2], Facility.reachPlot[num * 2 + 1]);
			Vector2 vector2 = new Vector2((pos.X - Facility.offset.X) * 4f, (pos.Y - Facility.offset.Z) * 4f);
			int num2 = this.rand1.Next(9000, 22000);
			Vector3 zero = Vector3.Zero;
			zero = new Vector3(pos.X, Facility.offset.Y, pos.Y);
			this.man.dupe.Add(new astroDupe(zero, 2, num2, 0, this.sc, emo, rotter));
			int num3 = this.man.dupe.Count - 1;
			try
			{
				this.man.dupe[num3].botPath.Clear();
				string text = this.botPath(ref Facility.reachPlot, ref this.man.dupe[num3].botPath, vector2, vector);
				if (text == "good")
				{
					this.man.dupe[num3].move = 2;
					this.man.dupe[num3].botStart = 0f;
				}
				else
				{
					this.man.dupe[num3].move = 2;
					this.man.dupe[num3].botStart = 0f;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x002E0A74 File Offset: 0x002DEC74
		public void newDestination(int i, Vector2 pos)
		{
			int num = this.rand1.Next(0, Facility.reachPlot.Count / 2);
			Vector2 vector = new Vector2(Facility.reachPlot[num * 2], Facility.reachPlot[num * 2 + 1]);
			Vector2 vector2 = new Vector2((pos.X - Facility.offset.X) * 4f, (pos.Y - Facility.offset.Z) * 4f);
			this.myDest = vector;
			try
			{
				string text = this.botPath(ref Facility.reachPlot, ref this.man.dupe[i].botPath, vector2, vector);
				if (text == "good")
				{
					this.man.dupe[i].move = 2;
					this.man.dupe[i].botStart = 0f;
				}
				else
				{
					vector.X /= 4f;
					vector.X += Facility.offset.X;
					vector.Y /= 4f;
					vector.Y += Facility.offset.Z;
					this.man.dupe[i].mypos.X = vector.X;
					this.man.dupe[i].mypos.Z = vector.Y;
					this.man.dupe[i].move = 2;
					this.man.dupe[i].botStart = 0f;
					this.man.dupe[i].botPath.Clear();
					this.man.dupe[i].botPath.Add(vector);
					this.myStart = vector2;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x002E0CB0 File Offset: 0x002DEEB0
		private string botPath(ref List<float> floorplot, ref List<Vector2> botPath, Vector2 startpos, Vector2 destiny)
		{
			List<Vector2> list = new List<Vector2>();
			float num = 800f;
			int num2 = -1;
			for (int i = 0; i < floorplot.Count; i += 2)
			{
				list.Add(new Vector2(floorplot[i] + 0f, floorplot[i + 1] + 0f));
				float num3 = Vector2.Distance(new Vector2(floorplot[i] + 0f, floorplot[i + 1] + 0f), startpos);
				if (num3 <= num)
				{
					num = num3;
					num2 = i;
				}
			}
			if (num2 == -1)
			{
				return "lost early";
			}
			Vector2 vector = new Vector2(floorplot[num2] + 0f, floorplot[num2 + 1] + 0f);
			List<Vector2> search = new List<Vector2>();
			List<Vector2> list2 = new List<Vector2>();
			list.ForEach(delegate(Vector2 item)
			{
				search.Add(item);
			});
			bool flag = this.buildbotPath(ref search, ref list2, vector, destiny);
			botPath.Clear();
			for (int j = 0; j < list2.Count; j++)
			{
				botPath.Add(list2[j]);
			}
			botPath.Add(destiny);
			if (!flag)
			{
				return "lost here ";
			}
			return "good";
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x002E0DF4 File Offset: 0x002DEFF4
		private bool buildbotPath(ref List<Vector2> source, ref List<Vector2> result, Vector2 start, Vector2 end)
		{
			bool flag = true;
			Random random = new Random();
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < source.Count; i++)
			{
				float num = Vector2.Distance(start, source[i]);
				if (num > 50f && num <= 400f)
				{
					list.Add(source[i]);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			List<Vector2> list2 = new List<Vector2>();
			while (list.Count > 0)
			{
				int num2 = random.Next(0, list.Count);
				list2.Add(list[num2]);
				list.RemoveAt(num2);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (Vector2.Distance(end, list2[j]) <= 500f)
				{
					result.Add(list2[j]);
					return true;
				}
				start = list2[j];
				source.Remove(start);
				result.Add(start);
				flag = this.buildbotPath(ref source, ref result, start, end);
				if (flag)
				{
					return true;
				}
				if (!flag)
				{
					result.Remove(start);
				}
			}
			return flag;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x002E0F0C File Offset: 0x002DF10C
		public void buildSeats()
		{
			this.seats.Clear();
			astro.seat seat = default(astro.seat);
			seat.rotoffset = 3.14f;
			seat.posoffset = new Vector3(0f, 0f, 121f);
			this.seats.Add(seat);
			seat = default(astro.seat);
			seat.rotoffset = -1.57f;
			seat.posoffset = new Vector3(61f, 0f, 35f);
			this.seats.Add(seat);
			seat = default(astro.seat);
			seat.rotoffset = 1.57f;
			seat.posoffset = new Vector3(-63f, 0f, 49f);
			this.seats.Add(seat);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x002E0FD5 File Offset: 0x002DF1D5
		public void Update(ref int[,] heightData, ref Vector3[,] normalData, int type, Vector3 campos, Vector3 camlookpos, bool hidden)
		{
			this.type = type;
			this.campos = campos;
			this.camlookpos = camlookpos;
			this.myframe++;
			this.updateAstro(ref this.man, ref heightData, ref normalData, hidden);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x002E100C File Offset: 0x002DF20C
		private void updateAstro(ref astro.npc n, ref int[,] heightData, ref Vector3[,] normalData, bool hiddenInLander)
		{
			this.lastmanAlive = -1;
			this.doorList.Clear();
			n.handindex1 = 0;
			n.fistindex1 = 0;
			n.bodyindex1 = 0;
			n.bodyindex2 = 0;
			n.packindex1 = 0;
			n.helmetindex1 = 0;
			n.headindex1 = 0;
			n.hammerindex1 = 0;
			this.drawOverheads.Clear();
			bool flag = false;
			if (astro.rovveloc.Length() > 5f)
			{
				flag = true;
			}
			n.alive = 0;
			float num = 14400f;
			this.chosen = -1;
			this.norm2 = Vector3.Normalize(this.camlookpos - this.campos);
			astro.someonedied = false;
			for (int i = 0; i < n.dupe.Count; i++)
			{
				if (n.dupe[i].emo == astroDupe.emotion.justsaved && !n.dupe[i].employed)
				{
					n.dupe[i].employed = true;
					n.dupe[i].emo = astroDupe.emotion.safe;
					astro.teamCount++;
				}
				float num2 = Vector3.DistanceSquared(new Vector3(n.dupe[i].mypos.X, n.dupe[i].mypos.Y + 60f, n.dupe[i].mypos.Z), this.campos);
				bool flag2 = num2 > 225000000f;
				bool flag3 = n.dupe[i].move >= 2;
				if (flag3 && !Facility.inFacility)
				{
					flag2 = true;
				}
				if (flag2 && this.myframe % 60 != 0)
				{
					if (n.dupe[i].emo == astroDupe.emotion.flipping)
					{
						n.dupe.RemoveAt(i);
					}
				}
				else
				{
					this.norm1 = Vector3.Normalize(new Vector3(n.dupe[i].mypos.X, n.dupe[i].mypos.Y + 60f, n.dupe[i].mypos.Z) - this.campos);
					this.dot = Vector3.Dot(this.norm1, this.norm2);
					if (num2 < num && this.dot > 0.9f && n.dupe[i].loc == astroDupe.where.outofTruck)
					{
						num = num2;
						this.chosen = i;
					}
					if (n.dupe[i].move == 1)
					{
						n.dupe[i].UpdateNormally(ref heightData, ref normalData);
						if (flag && (!n.dupe[i].flipinspace || n.dupe[i].flipTimer > 3f) && n.dupe[i].emo == astroDupe.emotion.safe && n.dupe[i].loc == astroDupe.where.outofTruck && Vector3.Distance(astro.rovpos, n.dupe[i].mypos) < 50f)
						{
							astro.someonedied = true;
							this.sc.manyell.Play(this.sc.ev, (float)this.rand1.Next(-20, 30) / 100f, 0f);
							this.manhit = true;
							n.dupe[i].flipinspace = true;
							n.dupe[i].flipVeloc = Vector3.Normalize(astro.rovveloc) * MathHelper.Min(30f, astro.rovveloc.Length() * 0.7f);
							n.dupe[i].flipVeloc.Y = (float)this.rand1.Next(-270, 270) / 100f;
							if (this.rand1.Next(1, 100) < 10)
							{
								n.dupe[i].flipVeloc.Y = (float)this.rand1.Next(220, 470) / 100f;
							}
							n.dupe[i].flipDir = Vector3.Normalize(new Vector3(astro.rovveloc.X, 0.1f, astro.rovveloc.Z));
							n.dupe[i].flipSpeed = (float)this.rand1.Next(4, 18) / 100f;
							if (this.rand1.Next(1, 100) < 50)
							{
								n.dupe[i].flipSpeed *= -1f;
							}
							n.dupe[i].flipRot = Matrix.CreateFromAxisAngle(Vector3.Cross(n.dupe[i].flipDir, Vector3.Up), n.dupe[i].flipSpeed);
							n.dupe[i].flipTimer = 0f;
							n.dupe[i].timer = 0f;
							if (n.dupe[i].employed)
							{
								astro.teamCount--;
								astro.oldteamCount--;
								if (astro.teamCount < 0)
								{
									astro.teamCount = 0;
								}
								if (astro.oldteamCount < 0)
								{
									astro.oldteamCount = 0;
								}
							}
							flag = false;
						}
						Matrix.CreateRotationY(n.dupe[i].myRot, out this.m2);
						if (n.dupe[i].clip1 == n.dupe[i].flipClip)
						{
							n.dupe[i].flipRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(n.dupe[i].flipDir, Vector3.Up), n.dupe[i].flipSpeed);
							this.m2 *= Matrix.CreateTranslation(new Vector3(0f, -43f, 0f)) * n.dupe[i].flipRot * Matrix.CreateTranslation(new Vector3(0f, 43f, 0f));
						}
						Matrix.CreateTranslation(n.dupe[i].mypos.X, n.dupe[i].mypos.Y, n.dupe[i].mypos.Z, out this.m4);
						Matrix.Multiply(ref this.m2, ref this.m4, out n.dupe[i].transform);
						if (n.dupe[i].loc == astroDupe.where.enteringTruck)
						{
							n.dupe[i].destination = Vector3.Transform(n.dupe[i].posOffset, astro.inDumper);
						}
						if (n.dupe[i].loc == astroDupe.where.inTruck)
						{
							n.dupe[i].mypos = Vector3.Transform(n.dupe[i].posOffset, astro.inDumper);
							n.dupe[i].myRot = this.facingRover + n.dupe[i].rotOffset;
							n.dupe[i].transform = Matrix.CreateRotationY(n.dupe[i].rotOffset) * Matrix.CreateTranslation(n.dupe[i].posOffset) * astro.inDumper;
						}
						if (n.dupe[i].loc == astroDupe.where.leavingTruck)
						{
							n.dupe[i].mypos = Vector3.Transform(n.dupe[i].posOffset, astro.inDumper);
							n.dupe[i].myRot = this.facingRover + n.dupe[i].rotOffset + 0f;
							n.dupe[i].transform = Matrix.CreateRotationY(n.dupe[i].rotOffset) * Matrix.CreateTranslation(n.dupe[i].posOffset) * astro.inDumper;
						}
					}
					if (n.dupe[i].move > 1)
					{
						n.dupe[i].updateFacilityAI(ref Facility.heightData, ref normalData);
						if (n.dupe[i].move == 3)
						{
							n.dupe[i].move = 2;
							this.newDestination(i, new Vector2(n.dupe[i].mypos.X, n.dupe[i].mypos.Z));
						}
						Matrix.CreateRotationY(n.dupe[i].myRot, out this.m2);
						Matrix.CreateTranslation(n.dupe[i].mypos.X, n.dupe[i].mypos.Y, n.dupe[i].mypos.Z, out this.m4);
						Matrix.Multiply(ref this.m2, ref this.m4, out n.dupe[i].transform);
						if (Facility.inFacility)
						{
							int num3 = Facility.clickable[(int)MathHelper.Clamp((float)Math.Round((double)((n.dupe[i].mypos.X - Facility.offset.X) * 4f / 100f)), 0f, 175f), (int)MathHelper.Clamp((float)Math.Round((double)((n.dupe[i].mypos.Z - Facility.offset.Z) * 4f / 100f)), 0f, 175f)];
							if (num3 > -1)
							{
								this.doorList.Add(num3);
							}
						}
					}
					if (n.dupe[i].loc != astroDupe.where.inTruck || !hiddenInLander)
					{
						if (this.sc.cheat_astro)
						{
							astro.overhead overhead = new astro.overhead();
							overhead.overheadpos = n.dupe[i].mypos;
							overhead.mess1 = n.dupe[i].emo.ToString();
							overhead.mess2 = n.dupe[i].loc.ToString();
							this.drawOverheads.Add(overhead);
						}
						this.tempySkin.Transformation = n.dupe[i].transform;
						this.tempySkin.tween = n.dupe[i].tween;
						this.tempySkin.frame1 = (float)n.dupe[i].frame1;
						this.tempySkin.frame2 = (float)n.dupe[i].frame2;
						this.tempySkin.tint = (float)n.dupe[i].tint;
						if (n.dupe[i].bodytype == 1)
						{
							n.displayBody1[n.bodyindex1] = this.tempySkin;
							n.bodyindex1++;
						}
						if (n.dupe[i].bodytype == 2)
						{
							n.displayBody2[n.bodyindex2] = this.tempySkin;
							n.bodyindex2++;
						}
						if (n.dupe[i].clip1 == n.dupe[i].hammerClip)
						{
							n.displayFist[n.fistindex1] = this.tempySkin;
							n.fistindex1++;
							n.displayHammer[n.hammerindex1] = this.tempySkin;
							n.hammerindex1++;
						}
						else
						{
							n.displayHand1[n.handindex1] = this.tempySkin;
							n.handindex1++;
						}
						if (n.dupe[i].packType == 2)
						{
							n.displayPack1[n.packindex1] = this.tempySkin;
							n.packindex1++;
						}
						if (n.dupe[i].headType == 1)
						{
							n.displayHelm1[n.helmetindex1] = this.tempySkin;
							n.helmetindex1++;
						}
						if (n.dupe[i].headType == 2)
						{
							n.displayHead1[n.headindex1] = this.tempySkin;
							n.headindex1++;
						}
						this.lastmanAlive = i;
					}
				}
			}
			if (this.chosen != -1 && this.saluteRequest)
			{
				if (n.dupe[this.chosen].emo == astroDupe.emotion.underground)
				{
					n.dupe[this.chosen].makeSalute(true);
				}
				else if (n.dupe[this.chosen].loc == astroDupe.where.outofTruck)
				{
					if (this.seats.Count > 0)
					{
						Vector3 vector = Vector3.Zero;
						int num4 = 0;
						float num5 = 100000f;
						for (int j = 0; j < this.seats.Count; j++)
						{
							vector = Vector3.Transform(this.seats[j].posoffset, astro.inDumper);
							float num6 = Vector3.Distance(n.dupe[this.chosen].mypos, vector);
							if (num6 < num5)
							{
								num4 = j;
								num5 = num6;
							}
						}
						n.dupe[this.chosen].posOffset = this.seats[num4].posoffset;
						n.dupe[this.chosen].rotOffset = this.seats[num4].rotoffset;
						this.seats.RemoveAt(num4);
						n.dupe[this.chosen].destination = Vector3.Transform(n.dupe[this.chosen].posOffset, astro.inDumper);
						this.rider.Add(this.chosen);
						n.dupe[this.chosen].makeSalute(true);
					}
					else
					{
						n.dupe[this.chosen].makeSalute(false);
					}
				}
			}
			this.saluteRequest = false;
			if (this.rider.Count > 0 && this.everyoneOut && this.chosen == -1)
			{
				int num7 = this.rider[0];
				if (n.dupe[num7].loc == astroDupe.where.inTruck)
				{
					this.rider.RemoveAt(0);
					astro.seat seat = default(astro.seat);
					seat.rotoffset = n.dupe[num7].rotOffset;
					seat.posoffset = n.dupe[num7].posOffset;
					this.seats.Add(seat);
					n.dupe[num7].leaveTruck();
				}
			}
			this.everyoneOut = false;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x002E1FEC File Offset: 0x002E01EC
		public void Draw(Matrix view, Matrix proj, Vector3 light, Vector3 diff, Vector3 amb, float Yscale, float camradian)
		{
			if (this.man.bodyindex1 < 1 && this.man.bodyindex2 < 1)
			{
				return;
			}
			this.camradian = camradian;
			this.view = view;
			this.proj = proj;
			this.light = light;
			this.amb = amb;
			this.diff = diff;
			string text = "outdoors";
			if (Facility.inFacility)
			{
				text = "indoors";
			}
			this.man.eff = this.tintEffect;
			this.man.eff.CurrentTechnique = this.man.eff.Techniques[text];
			this.man.eff.Parameters["Scale"].SetValue(Matrix.CreateScale(1f, Yscale, 1f));
			this.man.eff.Parameters["LightDirection"].SetValue(light);
			this.man.eff.Parameters["diff"].SetValue(diff);
			this.man.eff.Parameters["amb"].SetValue(amb);
			this.man.eff.Parameters["View"].SetValue(view);
			this.man.eff.Parameters["Projection"].SetValue(proj);
			this.man.eff.CurrentTechnique.Passes[0].Apply();
			if (this.man.handindex1 > 0)
			{
				this.DrawAstroHand(ref this.man, this.man.handindex1);
			}
			if (this.man.fistindex1 > 0)
			{
				this.DrawAstroFist(ref this.man, this.man.fistindex1);
			}
			this.man.eff = this.dayEffect;
			this.man.eff.CurrentTechnique = this.man.eff.Techniques[text];
			this.man.eff.Parameters["Scale"].SetValue(Matrix.CreateScale(1f, Yscale, 1f));
			this.man.eff.Parameters["LightDirection"].SetValue(light);
			this.man.eff.Parameters["diff"].SetValue(diff);
			this.man.eff.Parameters["amb"].SetValue(amb);
			this.man.eff.Parameters["View"].SetValue(view);
			this.man.eff.Parameters["Projection"].SetValue(proj);
			this.man.eff.CurrentTechnique.Passes[0].Apply();
			if (this.man.bodyindex1 > 0)
			{
				this.DrawAstroBody1(ref this.man, this.man.bodyindex1);
			}
			if (this.man.bodyindex2 > 0)
			{
				this.DrawAstroBody2(ref this.man, this.man.bodyindex2);
			}
			if (this.man.hammerindex1 > 0)
			{
				this.DrawAstroHammer(ref this.man, this.man.hammerindex1);
			}
			if (this.man.helmetindex1 > 0)
			{
				this.DrawAstroHelmet(ref this.man, this.man.helmetindex1);
			}
			if (this.man.packindex1 > 0)
			{
				this.DrawAstroPack(ref this.man, this.man.packindex1);
			}
			if (this.man.headindex1 > 0)
			{
				this.DrawAstroHead(ref this.man, this.man.headindex1);
			}
			if (this.sc.cheat_astro)
			{
				this.DrawHeadQuad();
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x002E23E4 File Offset: 0x002E05E4
		private void DrawAstroBody1(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["body1"].MeshParts[0];
			n.bodybuffer1.SetData<astro.skinstream>(n.displayBody1, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.bodybuffer1, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x002E24BC File Offset: 0x002E06BC
		private void DrawAstroBody2(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["body2"].MeshParts[0];
			n.bodybuffer2.SetData<astro.skinstream>(n.displayBody2, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.bodybuffer2, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x002E2594 File Offset: 0x002E0794
		private void DrawAstroHand(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["hands1"].MeshParts[0];
			n.handbuffer1.SetData<astro.skinstream>(n.displayHand1, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.handbuffer1, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x002E266C File Offset: 0x002E086C
		private void DrawAstroFist(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["hands2"].MeshParts[0];
			n.fistbuffer.SetData<astro.skinstream>(n.displayFist, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.fistbuffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x002E2744 File Offset: 0x002E0944
		private void DrawAstroHammer(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["hammer1"].MeshParts[0];
			n.hammerbuffer.SetData<astro.skinstream>(n.displayHammer, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.hammerbuffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x002E281C File Offset: 0x002E0A1C
		private void DrawAstroHelmet(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["helmet1"].MeshParts[0];
			n.helmbuffer1.SetData<astro.skinstream>(n.displayHelm1, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.helmbuffer1, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x002E28F4 File Offset: 0x002E0AF4
		private void DrawAstroPack(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["pack1"].MeshParts[0];
			n.packbuffer4.SetData<astro.skinstream>(n.displayPack1, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.packbuffer4, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x002E29CC File Offset: 0x002E0BCC
		private void DrawAstroHead(ref astro.npc n, int cc)
		{
			ModelMeshPart modelMeshPart = n.model1.Meshes["head1"].MeshParts[0];
			n.headbuffer5.SetData<astro.skinstream>(n.displayHead1, 0, cc, SetDataOptions.Discard);
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(n.headbuffer5, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, cc);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x002E2AA4 File Offset: 0x002E0CA4
		private void DrawHeadQuad()
		{
			for (int i = 0; i < this.drawOverheads.Count; i++)
			{
				string text = this.drawOverheads[i].mess1;
				text = text.ToLower();
				string text2 = this.drawOverheads[i].mess2;
				text2 = text2.ToLower();
				Vector3 overheadpos = this.drawOverheads[i].overheadpos;
				this.basiceffect.View = this.view;
				this.basiceffect.Projection = this.proj;
				Vector2 vector = this.sc.landerfont.MeasureString(text) / 2f;
				float num = 0.25f;
				this.basiceffect.World = Matrix.CreateScale(-1f, -1f, 1f) * Matrix.CreateRotationY(-this.camradian - 3.14f) * Matrix.CreateTranslation(overheadpos.X, overheadpos.Y + 95f, overheadpos.Z);
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, DepthStencilState.Default, RasterizerState.CullNone, this.basiceffect);
				this.spriteBatch.DrawString(this.sc.landerfont, text, Vector2.Zero, Color.White, 0f, vector, num, SpriteEffects.None, 0f);
				this.spriteBatch.End();
				num = 0.18f;
				vector = this.sc.landerfont.MeasureString(text2) / 2f;
				this.basiceffect.World = Matrix.CreateScale(-1f, -1f, 1f) * Matrix.CreateRotationY(-this.camradian - 3.14f) * Matrix.CreateTranslation(overheadpos.X, overheadpos.Y + 80f, overheadpos.Z);
				this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, DepthStencilState.Default, RasterizerState.CullNone, this.basiceffect);
				this.spriteBatch.DrawString(this.sc.landerfont, text2, Vector2.Zero, Color.White, 0f, vector, num, SpriteEffects.None, 0f);
				this.spriteBatch.End();
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x002E2CEC File Offset: 0x002E0EEC
		private void drawFloorPlot(Matrix world, Texture2D tt)
		{
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.None;
			this.sphere.Meshes[0].MeshParts[0].Effect = this.simpEffect;
			this.simpEffect.CurrentTechnique = this.simpEffect.Techniques["straight"];
			this.simpEffect.Parameters["val"].SetValue(1);
			this.simpEffect.Parameters["campos"].SetValue(this.campos);
			this.simpEffect.Parameters["modelTexture"].SetValue(tt);
			this.simpEffect.Parameters["world"].SetValue(world);
			this.simpEffect.Parameters["view"].SetValue(this.view);
			this.simpEffect.Parameters["projection"].SetValue(this.proj);
			this.simpEffect.CurrentTechnique.Passes[0].Apply();
			this.sphere.Meshes[0].Draw();
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
		}

		// Token: 0x04002D93 RID: 11667
		public static int teamCount = 0;

		// Token: 0x04002D94 RID: 11668
		public static int oldteamCount = 0;

		// Token: 0x04002D95 RID: 11669
		public static bool someonedied = false;

		// Token: 0x04002D96 RID: 11670
		public static Matrix inDumper = Matrix.Identity;

		// Token: 0x04002D97 RID: 11671
		public static Vector3 rovpos;

		// Token: 0x04002D98 RID: 11672
		public static Vector3 rovveloc;

		// Token: 0x04002D99 RID: 11673
		private Vector3 campos;

		// Token: 0x04002D9A RID: 11674
		private Vector3 camlookpos;

		// Token: 0x04002D9B RID: 11675
		public int chosen = -1;

		// Token: 0x04002D9C RID: 11676
		private int type;

		// Token: 0x04002D9D RID: 11677
		private Vector3 norm1;

		// Token: 0x04002D9E RID: 11678
		private Vector3 norm2;

		// Token: 0x04002D9F RID: 11679
		private float dot;

		// Token: 0x04002DA0 RID: 11680
		private int myframe;

		// Token: 0x04002DA1 RID: 11681
		public float facingRover;

		// Token: 0x04002DA2 RID: 11682
		public List<int> doorList = new List<int>();

		// Token: 0x04002DA3 RID: 11683
		private BasicEffect basiceffect;

		// Token: 0x04002DA4 RID: 11684
		public List<astro.overhead> drawOverheads = new List<astro.overhead>();

		// Token: 0x04002DA5 RID: 11685
		public List<astro.seat> seats = new List<astro.seat>();

		// Token: 0x04002DA6 RID: 11686
		private List<int> rider = new List<int>();

		// Token: 0x04002DA7 RID: 11687
		public bool manhit;

		// Token: 0x04002DA8 RID: 11688
		public bool saluteRequest;

		// Token: 0x04002DA9 RID: 11689
		public bool everyoneOut;

		// Token: 0x04002DAA RID: 11690
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x04002DAB RID: 11691
		private static VertexDeclaration vd = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
			new VertexElement(68, VertexElementFormat.Single, VertexElementUsage.Fog, 1),
			new VertexElement(72, VertexElementFormat.Single, VertexElementUsage.Fog, 2),
			new VertexElement(76, VertexElementFormat.Single, VertexElementUsage.Fog, 3)
		});

		// Token: 0x04002DAC RID: 11692
		private astro.conductor tempConduct = default(astro.conductor);

		// Token: 0x04002DAD RID: 11693
		public astro.npc man;

		// Token: 0x04002DAE RID: 11694
		public astro.skinstream tempySkin = default(astro.skinstream);

		// Token: 0x04002DAF RID: 11695
		private ScreenManager sc;

		// Token: 0x04002DB0 RID: 11696
		private ContentManager content;

		// Token: 0x04002DB1 RID: 11697
		private float camradian;

		// Token: 0x04002DB2 RID: 11698
		private Matrix view;

		// Token: 0x04002DB3 RID: 11699
		private Matrix proj;

		// Token: 0x04002DB4 RID: 11700
		private Vector3 light;

		// Token: 0x04002DB5 RID: 11701
		private Vector3 diff;

		// Token: 0x04002DB6 RID: 11702
		private Vector3 amb;

		// Token: 0x04002DB7 RID: 11703
		private int lastmanAlive = -1;

		// Token: 0x04002DB8 RID: 11704
		private List<int> parts;

		// Token: 0x04002DB9 RID: 11705
		private int[] s;

		// Token: 0x04002DBA RID: 11706
		private Effect dayEffect;

		// Token: 0x04002DBB RID: 11707
		private Effect tintEffect;

		// Token: 0x04002DBC RID: 11708
		private Vector3 v1;

		// Token: 0x04002DBD RID: 11709
		private Vector3 v2;

		// Token: 0x04002DBE RID: 11710
		private Vector3 v3;

		// Token: 0x04002DBF RID: 11711
		private Vector3 vZero = Vector3.Zero;

		// Token: 0x04002DC0 RID: 11712
		private Matrix m1;

		// Token: 0x04002DC1 RID: 11713
		private Matrix m2;

		// Token: 0x04002DC2 RID: 11714
		private Matrix m3;

		// Token: 0x04002DC3 RID: 11715
		private Matrix m4;

		// Token: 0x04002DC4 RID: 11716
		private float f1;

		// Token: 0x04002DC5 RID: 11717
		private float f2;

		// Token: 0x04002DC6 RID: 11718
		private float f3;

		// Token: 0x04002DC7 RID: 11719
		private Random rand1;

		// Token: 0x04002DC8 RID: 11720
		private SpriteBatch spriteBatch;

		// Token: 0x04002DC9 RID: 11721
		private Model sphere;

		// Token: 0x04002DCA RID: 11722
		private Vector2 myStart;

		// Token: 0x04002DCB RID: 11723
		private Vector2 myDest;

		// Token: 0x04002DCC RID: 11724
		private Effect simpEffect;

		// Token: 0x04002DCD RID: 11725
		private Texture2D whitesquare;

		// Token: 0x04002DCE RID: 11726
		private Texture2D greensquare;

		// Token: 0x04002DCF RID: 11727
		private Texture2D redsquare;

		// Token: 0x02000134 RID: 308
		public class overhead
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000B32 RID: 2866 RVA: 0x002E2FB8 File Offset: 0x002E11B8
			// (set) Token: 0x06000B33 RID: 2867 RVA: 0x002E2FC0 File Offset: 0x002E11C0
			public Vector3 overheadpos { get; set; }

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000B34 RID: 2868 RVA: 0x002E2FC9 File Offset: 0x002E11C9
			// (set) Token: 0x06000B35 RID: 2869 RVA: 0x002E2FD1 File Offset: 0x002E11D1
			public string mess1 { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000B36 RID: 2870 RVA: 0x002E2FDA File Offset: 0x002E11DA
			// (set) Token: 0x06000B37 RID: 2871 RVA: 0x002E2FE2 File Offset: 0x002E11E2
			public string mess2 { get; set; }
		}

		// Token: 0x02000135 RID: 309
		public struct seat
		{
			// Token: 0x04002DD3 RID: 11731
			public Vector3 posoffset;

			// Token: 0x04002DD4 RID: 11732
			public float rotoffset;
		}

		// Token: 0x02000136 RID: 310
		public struct skinstream : IVertexType
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000B39 RID: 2873 RVA: 0x002E2FF3 File Offset: 0x002E11F3
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return astro.skinstream.VertexDeclaration;
				}
			}

			// Token: 0x04002DD5 RID: 11733
			public Matrix Transformation;

			// Token: 0x04002DD6 RID: 11734
			public float frame1;

			// Token: 0x04002DD7 RID: 11735
			public float frame2;

			// Token: 0x04002DD8 RID: 11736
			public float tween;

			// Token: 0x04002DD9 RID: 11737
			public float tint;

			// Token: 0x04002DDA RID: 11738
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
				new VertexElement(68, VertexElementFormat.Single, VertexElementUsage.Fog, 1),
				new VertexElement(72, VertexElementFormat.Single, VertexElementUsage.Fog, 2),
				new VertexElement(76, VertexElementFormat.Single, VertexElementUsage.Fog, 3)
			});
		}

		// Token: 0x02000137 RID: 311
		public struct conductor
		{
			// Token: 0x04002DDB RID: 11739
			public byte type;

			// Token: 0x04002DDC RID: 11740
			public int id;

			// Token: 0x04002DDD RID: 11741
			public byte action;

			// Token: 0x04002DDE RID: 11742
			public byte bodypart;

			// Token: 0x04002DDF RID: 11743
			public int frame;

			// Token: 0x04002DE0 RID: 11744
			public int time;

			// Token: 0x04002DE1 RID: 11745
			public bool died;

			// Token: 0x04002DE2 RID: 11746
			public Vector3 veloc;
		}

		// Token: 0x02000138 RID: 312
		public struct npc
		{
			// Token: 0x04002DE3 RID: 11747
			public ushort alive;

			// Token: 0x04002DE4 RID: 11748
			public ushort alive2;

			// Token: 0x04002DE5 RID: 11749
			public Model model1;

			// Token: 0x04002DE6 RID: 11750
			public Model model2;

			// Token: 0x04002DE7 RID: 11751
			public astro.skinstream[] displayBody1;

			// Token: 0x04002DE8 RID: 11752
			public astro.skinstream[] displayBody2;

			// Token: 0x04002DE9 RID: 11753
			public astro.skinstream[] displayHelm1;

			// Token: 0x04002DEA RID: 11754
			public astro.skinstream[] displayHand1;

			// Token: 0x04002DEB RID: 11755
			public astro.skinstream[] displayFist;

			// Token: 0x04002DEC RID: 11756
			public astro.skinstream[] displayPack1;

			// Token: 0x04002DED RID: 11757
			public astro.skinstream[] displayHead1;

			// Token: 0x04002DEE RID: 11758
			public astro.skinstream[] displayHammer;

			// Token: 0x04002DEF RID: 11759
			public DynamicVertexBuffer bodybuffer1;

			// Token: 0x04002DF0 RID: 11760
			public DynamicVertexBuffer bodybuffer2;

			// Token: 0x04002DF1 RID: 11761
			public DynamicVertexBuffer helmbuffer1;

			// Token: 0x04002DF2 RID: 11762
			public DynamicVertexBuffer handbuffer1;

			// Token: 0x04002DF3 RID: 11763
			public DynamicVertexBuffer fistbuffer;

			// Token: 0x04002DF4 RID: 11764
			public DynamicVertexBuffer packbuffer4;

			// Token: 0x04002DF5 RID: 11765
			public DynamicVertexBuffer headbuffer5;

			// Token: 0x04002DF6 RID: 11766
			public DynamicVertexBuffer hammerbuffer;

			// Token: 0x04002DF7 RID: 11767
			public int handindex1;

			// Token: 0x04002DF8 RID: 11768
			public int fistindex1;

			// Token: 0x04002DF9 RID: 11769
			public int bodyindex1;

			// Token: 0x04002DFA RID: 11770
			public int bodyindex2;

			// Token: 0x04002DFB RID: 11771
			public int headindex1;

			// Token: 0x04002DFC RID: 11772
			public int helmetindex1;

			// Token: 0x04002DFD RID: 11773
			public int packindex1;

			// Token: 0x04002DFE RID: 11774
			public int hammerindex1;

			// Token: 0x04002DFF RID: 11775
			public List<int> explodelist;

			// Token: 0x04002E00 RID: 11776
			public List<int> shockList;

			// Token: 0x04002E01 RID: 11777
			public List<int> shatterList;

			// Token: 0x04002E02 RID: 11778
			public List<astroDupe> dupe;

			// Token: 0x04002E03 RID: 11779
			public List<astro.conductor> conductor;

			// Token: 0x04002E04 RID: 11780
			public int max;

			// Token: 0x04002E05 RID: 11781
			public SkinningDataX data;

			// Token: 0x04002E06 RID: 11782
			public Texture2D bitmap;

			// Token: 0x04002E07 RID: 11783
			public Texture2D man1Texture;

			// Token: 0x04002E08 RID: 11784
			public Texture2D man2Texture;

			// Token: 0x04002E09 RID: 11785
			public Texture2D boneTexture;

			// Token: 0x04002E0A RID: 11786
			public Texture2D charTexture;

			// Token: 0x04002E0B RID: 11787
			public Effect eff;

			// Token: 0x04002E0C RID: 11788
			public Vector3[] uv;

			// Token: 0x04002E0D RID: 11789
			public Matrix[] targ;

			// Token: 0x04002E0E RID: 11790
			public int[] bone;

			// Token: 0x04002E0F RID: 11791
			public int hitindex;
		}
	}
}
