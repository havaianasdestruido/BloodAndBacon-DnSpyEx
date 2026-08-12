using System;
using System.Globalization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000F1 RID: 241
	public class goldenkey
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x001D97B0 File Offset: 0x001D79B0
		public goldenkey(ScreenManager ss, ContentManager cc)
		{
			this.content = cc;
			this.sc = ss;
			this.keyeffect = this.content.Load<Effect>("effects//keyeffect");
			this.skulleffect = this.content.Load<Effect>("effects//skulleffect");
			this.geartick = this.content.Load<SoundEffect>("audio//geartick");
			this.flash1 = this.content.Load<Texture2D>("texture//flash1");
			this.flashlight1 = this.content.Load<Model>("models//flashlight1");
			this.flash2 = this.content.Load<Texture2D>("texture//flash2");
			this.flashlight2 = this.content.Load<Model>("models//flashlight2");
			this.flash3 = this.content.Load<Texture2D>("texture//flash3");
			this.flashlight3 = this.content.Load<Model>("models//flashlight3");
			this.ammoboxTexture = this.content.Load<Texture2D>("texture//ammoboxTexture2");
			this.ammobox = this.content.Load<Model>("models//ammobox2");
			this.goggleTexture = this.content.Load<Texture2D>("texture//geigerTexture3");
			this.goggles1 = this.content.Load<Model>("models//goggles1");
			this.cog = this.content.Load<Model>("models//damagedCog2");
			this.cogTexture = this.content.Load<Texture2D>("texture//cogTexture");
			this.goldpile = this.content.Load<Model>("models//goldpile");
			this.tuskcc = this.content.Load<Model>("models//tusky");
			this.tuskTexture = this.content.Load<Texture2D>("texture//tuskCrystal");
			this.skullcc = this.content.Load<Model>("models//skullCC");
			this.skullTexture = this.content.Load<Texture2D>("texture//skullCrystal");
			this.skyboxTexture = this.content.Load<Texture2D>("texture//skybox");
			this.map = this.content.Load<Model>("models//map1");
			this.mapTexture = this.content.Load<Texture2D>("texture//mapTexture1");
			this.exitkey = this.content.Load<Model>("models//exitkey");
			this.exitkeyTexture = this.content.Load<Texture2D>("texture//exitkeyTexture");
			this.myRot = Matrix.CreateFromQuaternion(new Quaternion(0f, 0f, 0f, 0f));
			this.myRotold = this.myRot;
			for (int i = 1; i < 7; i++)
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				StreamReader streamReader = new StreamReader("ABCDE3/blimp1.txt");
				this.targetDist = 0f;
				this.targetX = new Curve();
				this.targetZ = new Curve();
				float num = 0f;
				float num2 = 0f;
				for (int j = 0; j < 51; j++)
				{
					float num3 = num;
					float num4 = num2;
					num = float.Parse(streamReader.ReadLine(), invariantCulture);
					num2 = float.Parse(streamReader.ReadLine(), invariantCulture);
					float num5 = (float)j / 50f;
					this.targetX.Keys.Add(new CurveKey(num5, num + 3000f));
					this.targetZ.Keys.Add(new CurveKey(num5, num2 + 3000f));
					if (j > 0)
					{
						this.targetDist += Vector2.Distance(new Vector2(num3, num4), new Vector2(num, num2));
					}
				}
				streamReader.Close();
				streamReader.Dispose();
				this.SetTangents(ref this.targetX, ref this.targetZ);
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x001D9E88 File Offset: 0x001D8088
		public void initTunnelItems()
		{
			this.kbPos = (this.kePos = (this.kmPos = (this.kgPos = Vector3.Zero)));
			this.kh1Pos = (this.kh2Pos = (this.kh3Pos = Vector3.Zero));
			this.ka1Pos = (this.ka2Pos = (this.ka3Pos = Vector3.Zero));
			this.kf1Pos = (this.kf2Pos = (this.kf3Pos = Vector3.Zero));
			this.kc1Pos = (this.kc2Pos = (this.kc3Pos = Vector3.Zero));
			this.kbHide = true;
			this.keHide = true;
			this.kmHide = true;
			this.kgHide = true;
			this.kh1Hide = true;
			this.kh2Hide = true;
			this.kh3Hide = true;
			this.ka1Hide = true;
			this.ka2Hide = true;
			this.ka3Hide = true;
			this.kf1Hide = true;
			this.kf2Hide = true;
			this.kf3Hide = true;
			this.kc1Hide = true;
			this.kc2Hide = true;
			this.kc3Hide = true;
			this.keyFlashlight1 = true;
			this.keyFlashlight2 = true;
			this.keyFlashlight3 = true;
			this.keyAmmobox = true;
			this.keyGoggles = true;
			this.keyMap = true;
			this.keyCog = true;
			this.keyExitkey = true;
			this.keySkull1 = true;
			this.keySkull2 = true;
			this.keySkull3 = true;
			this.keyTusk1 = true;
			this.keyTusk2 = true;
			this.keyTusk3 = true;
			this.rsFound = false;
			this.ttFound = false;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x001DA020 File Offset: 0x001D8220
		public void resetBlimp()
		{
			this.blimpTrans = new Vector3(1000f, 1500f, 1000f);
			Random random = new Random();
			this.blimpTrans = Vector3.Transform(new Vector3(0f, 0f, 1f), Matrix.CreateRotationY((float)random.Next(-15000, 15000) / 10f)) * (float)random.Next(2500, 5500);
			this.blimpTrans.X = this.blimpTrans.X + 3000f;
			this.blimpTrans.Z = this.blimpTrans.Z + 3000f;
			this.blimpTrans.Y = (float)random.Next(1400, 2000);
			this.blimpHit = 50;
			this.propHit = 5;
			this.carryHit = 5;
			this.loop = (float)random.Next(0, 99) / 100f;
			this.myRot = Matrix.CreateFromQuaternion(new Quaternion(0f, 0f, 0f, 0f));
			this.myRotold = this.myRot;
			this.speed = (float)random.Next(250, 370) / 100f;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x001DA164 File Offset: 0x001D8364
		public void SetTangents(ref Curve x, ref Curve y)
		{
			for (int i = 0; i < x.Keys.Count; i++)
			{
				int num = i - 1;
				if (num < 0)
				{
					num = i;
				}
				int num2 = i + 1;
				if (num2 == x.Keys.Count)
				{
					num2 = i;
				}
				CurveKey curveKey = x.Keys[num];
				CurveKey curveKey2 = x.Keys[num2];
				CurveKey curveKey3 = x.Keys[i];
				goldenkey.SetCurveKeyTangent(ref curveKey, ref curveKey3, ref curveKey2);
				x.Keys[i] = curveKey3;
				curveKey = y.Keys[num];
				curveKey2 = y.Keys[num2];
				curveKey3 = y.Keys[i];
				goldenkey.SetCurveKeyTangent(ref curveKey, ref curveKey3, ref curveKey2);
				y.Keys[i] = curveKey3;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x001DA244 File Offset: 0x001D8444
		private static void SetCurveKeyTangent(ref CurveKey prev, ref CurveKey cur, ref CurveKey next)
		{
			float num = next.Position - prev.Position;
			float num2 = next.Value - prev.Value;
			if (Math.Abs(num2) < 1E-45f)
			{
				cur.TangentIn = 0f;
				cur.TangentOut = 0f;
				return;
			}
			cur.TangentIn = num2 * (cur.Position - prev.Position) / num;
			cur.TangentOut = num2 * (next.Position - cur.Position) / num;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x001DA2CC File Offset: 0x001D84CC
		public string updatetunnel(Vector3 mypos, Vector3 campos, int altcam, float npctilt, float npcrot)
		{
			string text = "-1";
			bool flag = false;
			if (Vector3.Distance(campos, this.kePos) < 50f)
			{
				if (!this.keHide)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.keHide = true;
					text = this.ke;
					this.keyExitkey = false;
				}
				else if (flag)
				{
					text = this.ke;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kbPos) < 50f)
			{
				if (!this.kbHide)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.kbHide = true;
					text = this.kb;
				}
				else if (flag)
				{
					text = this.kb;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kmPos) < 50f)
			{
				if (!this.kmHide)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.2f, 0f);
					this.kmHide = true;
					text = this.km;
					this.keyMap = false;
				}
				else if (flag)
				{
					text = this.km;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kgPos) < 50f)
			{
				if (!this.kgHide)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.2f, 0f);
					this.kgHide = true;
					text = this.kg;
					this.keyGoggles = false;
				}
				else if (flag)
				{
					text = this.kg;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kh1Pos) < 50f)
			{
				if (!this.kh1Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kh1Hide = true;
					text = this.kh1;
				}
				else if (flag)
				{
					text = this.kh1;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kh2Pos) < 50f)
			{
				if (!this.kh2Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kh2Hide = true;
					text = this.kh2;
				}
				else if (flag)
				{
					text = this.kh2;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kh3Pos) < 50f)
			{
				if (!this.kh3Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kh3Hide = true;
					text = this.kh3;
				}
				else if (flag)
				{
					text = this.kh3;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kf1Pos) < 50f)
			{
				if (!this.kf1Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kf1Hide = true;
					text = this.kf1;
					this.keyFlashlight1 = false;
				}
				else if (flag)
				{
					text = this.kf1;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kf2Pos) < 50f)
			{
				if (!this.kf2Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kf2Hide = true;
					text = this.kf2;
					this.keyFlashlight2 = false;
				}
				else if (flag)
				{
					text = this.kf2;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kf3Pos) < 50f)
			{
				if (!this.kf3Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kf3Hide = true;
					text = this.kf3;
					this.keyFlashlight3 = false;
				}
				else if (flag)
				{
					text = this.kf3;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.ka1Pos) < 50f)
			{
				if (!this.ka1Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.ka1Hide = true;
					text = this.ka1;
				}
				else if (flag)
				{
					text = this.ka1;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.ka2Pos) < 50f)
			{
				if (!this.ka2Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.ka2Hide = true;
					text = this.ka2;
				}
				else if (flag)
				{
					text = this.ka2;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.ka3Pos) < 50f)
			{
				if (!this.ka3Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.ka3Hide = true;
					text = this.ka3;
				}
				else if (flag)
				{
					text = this.ka3;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kc1Pos) < 50f)
			{
				if (!this.kc1Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kc1Hide = true;
					text = this.kc1;
				}
				else if (flag)
				{
					text = this.kc1;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kc2Pos) < 50f)
			{
				if (!this.kc2Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kc2Hide = true;
					text = this.kc2;
				}
				else if (flag)
				{
					text = this.kc2;
				}
				return text;
			}
			if (Vector3.Distance(campos, this.kc3Pos) < 50f)
			{
				if (!this.kc3Hide)
				{
					this.sc.pickup1.Play(this.sc.ev, -0.1f, 0f);
					this.kc3Hide = true;
					text = this.kc3;
				}
				else if (flag)
				{
					text = this.kc3;
				}
				return text;
			}
			Vector3 vector = (this.flashlight2pos + this.gogglepos) / 2f;
			Vector3 vector2 = Vector3.Normalize(Vector3.Transform(new Vector3(0f, 0f, 1f), Matrix.CreateRotationX(npctilt) * Matrix.CreateRotationY(npcrot)));
			this.nearItems = Vector3.Distance(campos, vector) < 700f;
			this.nearExit = Vector3.Distance(campos, new Vector3(2885f, -250f, 4485f)) < 700f;
			if (this.nearItems)
			{
				if (Vector3.Distance(campos, this.flashlight1pos) < 70f && !this.keyFlashlight1)
				{
					Vector3 vector3 = Vector3.Normalize(this.flashlight1pos - campos);
					if (Vector3.Dot(vector2, vector3) > 0.65f)
					{
						text = "4";
					}
				}
				if (Vector3.Distance(campos, this.flashlight2pos) < 70f && !this.keyFlashlight2)
				{
					Vector3 vector4 = Vector3.Normalize(this.flashlight2pos - campos);
					if (Vector3.Dot(vector2, vector4) > 0.65f)
					{
						text = "5";
					}
				}
				if (Vector3.Distance(campos, this.flashlight3pos) < 70f && !this.keyFlashlight3)
				{
					Vector3 vector5 = Vector3.Normalize(this.flashlight3pos - campos);
					if (Vector3.Dot(vector2, vector5) > 0.65f)
					{
						text = "6";
					}
				}
				if (Vector3.Distance(campos, this.ammoboxpos) < 70f && !this.keyAmmobox && this.sc.ammoboxCount > 0)
				{
					Vector3 vector6 = Vector3.Normalize(this.ammoboxpos - campos);
					if (Vector3.Dot(vector2, vector6) > 0.65f)
					{
						text = "7";
					}
				}
				if (Vector3.Distance(campos, this.gogglepos) < 70f && !this.keyGoggles)
				{
					Vector3 vector7 = Vector3.Normalize(this.gogglepos - campos);
					if (Vector3.Dot(vector2, vector7) > 0.65f)
					{
						text = "8";
					}
				}
				this.atGears = false;
				if (Vector3.Distance(campos, this.cogpos) < 70f && !this.keyCog && this.sc.cogCount > 0)
				{
					Vector3 vector8 = Vector3.Normalize(this.cogpos - campos);
					if (Vector3.Dot(vector2, vector8) > 0.65f)
					{
						text = "9";
						this.atGears = true;
					}
				}
				if (Vector3.Distance(campos, this.mappos) < 70f && !this.keyMap)
				{
					Vector3 vector9 = Vector3.Normalize(this.mappos - campos);
					if (Vector3.Dot(vector2, vector9) > 0.65f)
					{
						text = "10";
					}
				}
				if (Vector3.Distance(campos, this.exitkeypos) < 70f && !this.keyExitkey)
				{
					Vector3 vector10 = Vector3.Normalize(this.exitkeypos - campos);
					if (Vector3.Dot(vector2, vector10) > 0.65f)
					{
						text = "11";
					}
				}
			}
			if (Vector3.Distance(campos, this.skullpos) < 70f && !this.keySkull1)
			{
				Vector3 vector11 = Vector3.Normalize(this.skullpos - campos);
				if (Vector3.Dot(vector2, vector11) > 0.65f)
				{
					return "13";
				}
			}
			if (Vector3.Distance(campos, this.skullpos) < 70f && !this.keySkull2)
			{
				Vector3 vector12 = Vector3.Normalize(this.skullpos - campos);
				if (Vector3.Dot(vector2, vector12) > 0.65f)
				{
					return "14";
				}
			}
			if (Vector3.Distance(campos, this.skullpos) < 70f && !this.keySkull3)
			{
				Vector3 vector13 = Vector3.Normalize(this.skullpos - campos);
				if (Vector3.Dot(vector2, vector13) > 0.65f)
				{
					return "15";
				}
			}
			if (Vector3.Distance(campos, this.rs2Pos) < 70f && this.rsFound)
			{
				Vector3 vector14 = Vector3.Normalize(this.rs2Pos - campos);
				if (Vector3.Dot(vector2, vector14) > 0.65f)
				{
					return "16";
				}
			}
			if (Vector3.Distance(campos, this.tt2Pos) < 70f && this.ttFound)
			{
				Vector3 vector15 = Vector3.Normalize(this.tt2Pos - campos);
				if (Vector3.Dot(vector2, vector15) > 0.65f)
				{
					return "17";
				}
			}
			if (Vector3.Distance(campos, this.tuskpos) < 70f && !this.keyTusk1)
			{
				Vector3 vector16 = Vector3.Normalize(this.tuskpos - campos);
				if (Vector3.Dot(vector2, vector16) > 0.65f)
				{
					return "18";
				}
			}
			if (Vector3.Distance(campos, this.tuskpos) < 70f && !this.keyTusk2)
			{
				Vector3 vector17 = Vector3.Normalize(this.tuskpos - campos);
				if (Vector3.Dot(vector2, vector17) > 0.65f)
				{
					return "19";
				}
			}
			if (Vector3.Distance(campos, this.tuskpos) < 70f && !this.keyTusk3)
			{
				Vector3 vector18 = Vector3.Normalize(this.tuskpos - campos);
				if (Vector3.Dot(vector2, vector18) > 0.65f)
				{
					return "20";
				}
			}
			return text;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x001DADDC File Offset: 0x001D8FDC
		public int update(Vector3 mypos, Vector3 campos, int altcam)
		{
			int num = -1;
			if (this.sc.dayTime == "pm" && this.sc.hats[2] == 0 && Vector3.Distance(mypos, this.keypos1) < 50f)
			{
				this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
				this.sc.hats[2] = 1;
				this.sc.hatindex = 2;
				this.sc.SaveEquipables();
				num = 2;
				bool flag = true;
				for (int i = 1; i < 6; i++)
				{
					if (this.sc.hats[i] == 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.sc.trophy.win(this.sc.trophy.madhatter);
				}
			}
			if (this.sc.hordemode)
			{
				if (this.sc.hats[9] == 0 && Vector3.Distance(mypos, this.keypos6) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hatindex = 9;
					if (this.sc.dlcreleased || this.sc.developer)
					{
						this.sc.hats[9] = 1;
						this.sc.SaveEquipables();
					}
					num = 9;
				}
				if (this.sc.hats[10] == 0 && Vector3.Distance(mypos, this.keypos7) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hatindex = 10;
					if (this.sc.dlcreleased || this.sc.developer)
					{
						this.sc.hats[10] = 1;
						this.sc.SaveEquipables();
					}
					num = 10;
				}
				if (this.sc.hats[11] == 0 && Vector3.Distance(mypos, this.keypos8) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hatindex = 11;
					if (this.sc.dlcreleased || this.sc.developer)
					{
						this.sc.hats[11] = 1;
						this.sc.SaveEquipables();
					}
					num = 11;
				}
			}
			else
			{
				if (this.sc.revengeDay > 0 && this.sc.hats[1] == 0 && Vector3.Distance(mypos, this.keypos2) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hats[1] = 1;
					this.sc.hatindex = 1;
					this.sc.SaveEquipables();
					num = 1;
					bool flag2 = true;
					for (int j = 1; j < 6; j++)
					{
						if (this.sc.hats[j] == 0)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						this.sc.trophy.win(this.sc.trophy.madhatter);
					}
				}
				if (this.sc.dayTime == "am" && this.sc.hats[3] == 0 && Vector3.Distance(campos, this.keypos3) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hats[3] = 1;
					this.sc.hatindex = 3;
					this.sc.SaveEquipables();
					num = 3;
					this.sc.trophy.win(this.sc.trophy.death);
					bool flag3 = true;
					for (int k = 1; k < 6; k++)
					{
						if (this.sc.hats[k] == 0)
						{
							flag3 = false;
							break;
						}
					}
					if (flag3)
					{
						this.sc.trophy.win(this.sc.trophy.madhatter);
					}
				}
				if (this.sc.currentDay % 20 == 0 && this.sc.hats[4] == 0 && Vector3.Distance(campos, this.keypos4) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hats[4] = 1;
					this.sc.hatindex = 4;
					this.sc.SaveEquipables();
					num = 4;
					bool flag4 = true;
					for (int l = 1; l < 6; l++)
					{
						if (this.sc.hats[l] == 0)
						{
							flag4 = false;
							break;
						}
					}
					if (flag4)
					{
						this.sc.trophy.win(this.sc.trophy.madhatter);
					}
				}
				if (altcam == 1 && this.sc.hats[5] == 0 && Vector3.Distance(campos, this.keypos5) < 50f)
				{
					this.sc.pickup1.Play(this.sc.ev, 0.1f, 0f);
					this.sc.hats[5] = 1;
					this.sc.hatindex = 5;
					this.sc.SaveEquipables();
					num = 5;
					this.sc.trophy.win(this.sc.trophy.death);
					bool flag5 = true;
					for (int m = 1; m < 6; m++)
					{
						if (this.sc.hats[m] == 0)
						{
							flag5 = false;
							break;
						}
					}
					if (flag5)
					{
						this.sc.trophy.win(this.sc.trophy.madhatter);
					}
				}
			}
			return num;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x001DB408 File Offset: 0x001D9608
		public void updateBlimp()
		{
			if (this.propHit <= 0)
			{
				this.speed = 1.5f;
			}
			this.loop += 1f / this.targetDist * this.speed;
			if (this.loop >= 1f)
			{
				this.loop = 0f;
			}
			this.tx = this.targetX.Evaluate(this.loop);
			this.tz = this.targetZ.Evaluate(this.loop);
			float num = -(float)Math.Atan2((double)(this.blimpTrans.Z - this.tz), (double)(this.blimpTrans.X - this.tx)) - 1.57f;
			float num2 = goldenkey.WrapAngle(num - this.blimpRot);
			float num3 = 0.035f;
			num2 = MathHelper.Clamp(num2, -num3, num3);
			this.blimpRot = goldenkey.WrapAngle(this.blimpRot + num2);
			this.myRotold = Matrix.CreateRotationY(this.blimpRot);
			if (this.rayAmt > 0.009f)
			{
				this.myRot *= Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(this.ray, Vector3.Up)), this.rayAmt);
				this.rayAmt *= 0.99f;
			}
			Vector3 vector;
			Quaternion quaternion;
			Vector3 vector2;
			this.myRot.Decompose(out vector, out quaternion, out vector2);
			Vector3 vector3;
			Quaternion quaternion2;
			Vector3 vector4;
			this.myRotold.Decompose(out vector3, out quaternion2, out vector4);
			Quaternion quaternion3 = Quaternion.Lerp(quaternion, quaternion2, 0.004f);
			this.myRot = Matrix.CreateFromQuaternion(quaternion3);
			Vector3 vector5 = Vector3.Transform(new Vector3(0f, 0f, 1f), this.myRot) * this.speed;
			this.blimpTrans += vector5;
			if (this.blimpTrans.Y < 1500f)
			{
				this.blimpTrans.Y = this.blimpTrans.Y + 1f;
			}
			if (this.blimpTrans.Y > 1500f)
			{
				this.blimpTrans.Y = this.blimpTrans.Y - 1f;
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x001DB62D File Offset: 0x001D982D
		private static float WrapAngle(float radians)
		{
			while (radians < -3.1415927f)
			{
				radians += 6.2831855f;
			}
			while (radians > 3.1415927f)
			{
				radians -= 6.2831855f;
			}
			return radians;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x001DB658 File Offset: 0x001D9858
		public void draw(Vector3 pos, Matrix view, Matrix proj, int altcam)
		{
			this.counter += 1f;
			if (this.sc.dayTime == "pm" && this.sc.hats[2] == 0)
			{
				this.DrawKey(this.keypos1, view, proj);
			}
			if (this.sc.hordemode)
			{
				if (this.sc.hats[9] == 0)
				{
					this.DrawKey(this.keypos6, view, proj);
				}
				if (this.sc.hats[10] == 0)
				{
					this.DrawKey(this.keypos7, view, proj);
				}
				if (this.sc.hats[11] == 0)
				{
					this.DrawKey(this.keypos8, view, proj);
					return;
				}
			}
			else
			{
				if (this.sc.revengeDay > 0 && this.sc.hats[1] == 0)
				{
					this.DrawKey(this.keypos2, view, proj);
				}
				if (this.sc.hats[3] == 0 && this.sc.dayTime == "am" && (this.sc.gameState != 1 || altcam == 1))
				{
					this.DrawKey(this.keypos3, view, proj);
				}
				if (this.sc.hats[4] == 0 && this.sc.currentDay % 20 == 0 && (this.sc.gameState != 1 || altcam == 1))
				{
					this.DrawKey(this.keypos4, view, proj);
				}
				if (this.sc.hats[5] == 0 && (this.sc.gameState != 1 || altcam == 1))
				{
					this.DrawKey(this.keypos5, view, proj);
				}
				if (this.sc.mustarDay && this.blimpHit > 0)
				{
					this.updateBlimp();
					this.DrawBlimp(view, proj);
				}
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x001DB81C File Offset: 0x001D9A1C
		public void DrawKey(Vector3 pos, Matrix view, Matrix proj)
		{
			float num = this.counter / 240f;
			Matrix matrix = Matrix.CreateScale(3f) * Matrix.CreateRotationY(num) * Matrix.CreateTranslation(pos.X, pos.Y, pos.Z);
			this.sc.hatPack.Meshes[35].MeshParts[0].Effect = this.keyeffect;
			this.keyeffect.Parameters["World"].SetValue(matrix);
			this.keyeffect.Parameters["View"].SetValue(view);
			this.keyeffect.Parameters["Projection"].SetValue(proj);
			this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3((float)Math.Sin((double)(this.counter / 15f)), 0.7f, (float)(-(float)Math.Cos((double)(this.counter / 15f))))));
			this.keyeffect.Parameters["DiffuseLight"].SetValue(0.9f);
			this.keyeffect.Parameters["AmbientLight"].SetValue(0.42f);
			if (this.counter % 95f == 0f)
			{
				this.keyeffect.Parameters["AmbientLight"].SetValue(2f);
			}
			this.keyeffect.Parameters["Texture"].SetValue(this.sc.goldkeyTexture);
			this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
			this.sc.hatPack.Meshes[35].Draw();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x001DBA0C File Offset: 0x001D9C0C
		public void DrawBlimp(Matrix view, Matrix proj)
		{
			float num = this.counter / 240f;
			Matrix matrix = this.myRot * Matrix.CreateTranslation(this.blimpTrans);
			this.sc.hatPack.Meshes[36].MeshParts[0].Effect = this.keyeffect;
			this.keyeffect.Parameters["World"].SetValue(matrix);
			this.keyeffect.Parameters["View"].SetValue(view);
			this.keyeffect.Parameters["Projection"].SetValue(proj);
			this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3(1f, 0.7f, 0f)));
			this.keyeffect.Parameters["DiffuseLight"].SetValue(0.9f);
			this.keyeffect.Parameters["AmbientLight"].SetValue(0.36f);
			this.keyeffect.Parameters["Texture"].SetValue(this.sc.blimpTexture);
			this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
			this.sc.hatPack.Meshes[36].Draw();
			if (this.carryHit > 0)
			{
				this.sc.hatPack.Meshes[37].MeshParts[0].Effect = this.keyeffect;
				this.sc.hatPack.Meshes[37].Draw();
			}
			if (this.propHit > 0)
			{
				this.sc.hatPack.Meshes[38].MeshParts[0].Effect = this.keyeffect;
				matrix = Matrix.CreateRotationZ(this.counter / 10f) * this.myRot * Matrix.CreateTranslation(this.blimpTrans);
				this.keyeffect.Parameters["World"].SetValue(matrix);
				this.sc.hatPack.Meshes[38].Draw();
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x001DBC7C File Offset: 0x001D9E7C
		public void drawTunnelKeys(Vector3 pos, Matrix view, Matrix proj, Vector3 campos, int altcam)
		{
			this.counter += 6f;
			if (this.atGears)
			{
				this.cogcounter += 0.3f;
			}
			this.gearticker++;
			if (this.gearticker % 87 == 0 && this.atGears)
			{
				this.geartick.Play(this.sc.ev * 1f, 0f, 0f);
			}
			if (!this.kbHide)
			{
				this.DrawGoldKey2(this.kbPos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kbPos, view, proj);
			}
			if (!this.keHide)
			{
				this.DrawGoldKey2(this.kePos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kePos, view, proj);
			}
			if (!this.kmHide)
			{
				this.DrawGoldKey2(this.kmPos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kmPos, view, proj);
			}
			if (!this.kgHide)
			{
				this.DrawGoldKey2(this.kgPos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kgPos, view, proj);
			}
			if (!this.kh1Hide)
			{
				this.DrawGoldKey2(this.kh1Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kh1Pos, view, proj);
			}
			if (!this.kh2Hide)
			{
				this.DrawGoldKey2(this.kh2Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kh2Pos, view, proj);
			}
			if (!this.kh3Hide)
			{
				this.DrawGoldKey2(this.kh3Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kh3Pos, view, proj);
			}
			if (!this.kf1Hide)
			{
				this.DrawGoldKey2(this.kf1Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kf1Pos, view, proj);
			}
			if (!this.kf2Hide)
			{
				this.DrawGoldKey2(this.kf2Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kf2Pos, view, proj);
			}
			if (!this.kf3Hide)
			{
				this.DrawGoldKey2(this.kf3Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kf3Pos, view, proj);
			}
			if (!this.kc1Hide)
			{
				this.DrawGoldKey2(this.kc1Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kc1Pos, view, proj);
			}
			if (!this.kc2Hide)
			{
				this.DrawGoldKey2(this.kc2Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kc2Pos, view, proj);
			}
			if (!this.kc3Hide)
			{
				this.DrawGoldKey2(this.kc3Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.kc3Pos, view, proj);
			}
			if (!this.ka1Hide)
			{
				this.DrawGoldKey2(this.ka1Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.ka1Pos, view, proj);
			}
			if (!this.ka2Hide)
			{
				this.DrawGoldKey2(this.ka2Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.ka2Pos, view, proj);
			}
			if (!this.ka3Hide)
			{
				this.DrawGoldKey2(this.ka3Pos, view, proj);
			}
			else
			{
				this.DrawGoldKeyPile(this.ka3Pos, view, proj);
			}
			if (this.nearItems)
			{
				if (!this.keyFlashlight1)
				{
					this.DrawInvModel(this.flashlight1matrix, view, proj, this.flashlight1, this.flash1);
				}
				if (!this.keyFlashlight2)
				{
					this.DrawInvModel(this.flashlight2matrix, view, proj, this.flashlight2, this.flash2);
				}
				if (!this.keyFlashlight3)
				{
					this.DrawInvModel(this.flashlight3matrix, view, proj, this.flashlight3, this.flash3);
				}
				if (!this.keyGoggles)
				{
					this.DrawInvModel(this.gogglematrix, view, proj, this.goggles1, this.goggleTexture);
				}
				if (!this.keyAmmobox && this.sc.ammoboxCount > 0)
				{
					this.DrawBoxes(this.ammoboxmatrix, view, proj, this.ammobox, this.ammoboxTexture);
				}
				if (!this.keyCog && this.sc.cogCount > 0)
				{
					this.DrawCogs(this.cogmatrix, view, proj, this.cog, this.cogTexture);
				}
				if (!this.keyExitkey)
				{
					this.DrawInvModel(this.exitkeymatrix, view, proj, this.exitkey, this.exitkeyTexture);
				}
				if (!this.keyMap)
				{
					this.DrawInvModel(this.mapmatrix, view, proj, this.map, this.mapTexture);
				}
			}
			Vector3 vector = new Vector3(0.7f, -0.7f, 0.2f);
			if (!this.keySkull1)
			{
				this.DrawInvSkull(new Vector3(0.1f, 0.3f, 1f), this.skullmatrix, view, proj, campos, this.skullcc, this.skullTexture, vector, 0f);
			}
			else if (!this.keySkull2)
			{
				this.DrawInvSkull(new Vector3(1f, 0.1f, 0.1f), this.skullmatrix, view, proj, campos, this.skullcc, this.skullTexture, vector, 0.1f);
			}
			else if (!this.keySkull3)
			{
				this.DrawInvSkull(new Vector3(0.2f, 1f, 0.25f), this.skullmatrix, view, proj, campos, this.skullcc, this.skullTexture, vector, 0f);
			}
			vector = new Vector3((float)Math.Sin((double)(this.sc.myTimer / 30f)), -0.4f, -(float)Math.Cos((double)(this.sc.myTimer / 30f)));
			if (!this.keyTusk1)
			{
				this.DrawInvTusk(new Vector3(0.1f, 0.3f, 1f), this.tuskmatrix, view, proj, campos, this.tuskcc, vector, 0.233f + this.sc.myTimer / 1500f);
			}
			if (!this.keyTusk2)
			{
				this.DrawInvTusk(new Vector3(1f, 0.1f, 0.1f), this.tuskmatrix, view, proj, campos, this.tuskcc, vector, 0f);
			}
			if (!this.keyTusk3)
			{
				this.DrawInvTusk(new Vector3(0.2f, 1f, 0.25f), this.tuskmatrix, view, proj, campos, this.tuskcc, vector, 5.2f - this.sc.myTimer / 1800f);
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x001DC274 File Offset: 0x001DA474
		public void drawSkullDisplay(int i, Matrix view, Matrix proj, Vector3 campos, Vector3 lite)
		{
			if (i == 1)
			{
				this.DrawInvSkull(new Vector3(0.1f, 0.3f, 1f), this.rs1Matrix, view, proj, campos, this.skullcc, this.skullTexture, lite, 0.233f + this.sc.myTimer / 1500f);
			}
			if (i == 2)
			{
				this.DrawInvSkull(new Vector3(1f, 0.1f, 0.1f), this.rs2Matrix, view, proj, campos, this.skullcc, this.skullTexture, lite, 0f);
			}
			if (i == 3)
			{
				this.DrawInvSkull(new Vector3(0.2f, 1f, 0.25f), this.rs3Matrix, view, proj, campos, this.skullcc, this.skullTexture, lite, 5.2f - this.sc.myTimer / 1800f);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x001DC358 File Offset: 0x001DA558
		public void drawTuskDisplay(int i, Matrix view, Matrix proj, Vector3 campos, Vector3 lite)
		{
			if (i == 1)
			{
				this.DrawInvTusk(new Vector3(0.1f, 0.1f, 1f), this.tt1Matrix, view, proj, campos, this.tuskcc, lite, 0.233f + this.sc.myTimer / 1500f);
			}
			if (i == 2)
			{
				this.DrawInvTusk(new Vector3(1f, 0.1f, 0.1f), this.tt2Matrix, view, proj, campos, this.tuskcc, lite, 0f);
			}
			if (i == 3)
			{
				this.DrawInvTusk(new Vector3(0.15f, 1f, 0.15f), this.tt3Matrix, view, proj, campos, this.tuskcc, lite, 5.2f - this.sc.myTimer / 1800f);
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x001DC428 File Offset: 0x001DA628
		public void DrawGoldKey2(Vector3 pos, Matrix view, Matrix proj)
		{
			if (pos == Vector3.Zero)
			{
				return;
			}
			float num = this.counter / 240f;
			Matrix matrix = Matrix.CreateScale(2f) * Matrix.CreateRotationY(num) * Matrix.CreateTranslation(pos.X, pos.Y, pos.Z);
			this.sc.hatPack.Meshes[35].MeshParts[0].Effect = this.keyeffect;
			this.keyeffect.Parameters["World"].SetValue(matrix);
			this.keyeffect.Parameters["View"].SetValue(view);
			this.keyeffect.Parameters["Projection"].SetValue(proj);
			this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3((float)Math.Sin((double)(this.counter / 25f)), 0.7f, (float)(-(float)Math.Cos((double)(this.counter / 25f))))));
			this.keyeffect.Parameters["DiffuseLight"].SetValue(0.9f);
			this.keyeffect.Parameters["AmbientLight"].SetValue(0.42f);
			if (this.counter % 145f == 0f)
			{
				this.keyeffect.Parameters["AmbientLight"].SetValue(2f);
			}
			this.keyeffect.Parameters["Texture"].SetValue(this.sc.goldkeyTexture);
			this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
			this.sc.hatPack.Meshes[35].Draw();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x001DC624 File Offset: 0x001DA824
		public void DrawGoldKeyPile(Vector3 pos, Matrix view, Matrix proj)
		{
			if (pos == Vector3.Zero)
			{
				return;
			}
			int num = (int)pos.Y;
			Random random = new Random(num);
			float num2 = (float)(random.Next(-8600, 8600) / 1000);
			Matrix matrix = Matrix.CreateScale(2f) * Matrix.CreateRotationY(num2) * Matrix.CreateTranslation(pos.X, pos.Y - (float)this.sc.tunnelUppy, pos.Z);
			this.goldpile.Meshes[0].MeshParts[0].Effect = this.keyeffect;
			this.keyeffect.Parameters["World"].SetValue(matrix);
			this.keyeffect.Parameters["View"].SetValue(view);
			this.keyeffect.Parameters["Projection"].SetValue(proj);
			this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3((float)Math.Sin((double)(this.counter / 265f)), 0.7f, (float)(-(float)Math.Cos((double)(this.counter / 265f))))));
			this.keyeffect.Parameters["DiffuseLight"].SetValue(0.6f);
			this.keyeffect.Parameters["AmbientLight"].SetValue(0.22f);
			this.keyeffect.Parameters["Texture"].SetValue(this.sc.goldkeyTexture);
			this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
			this.goldpile.Meshes[0].Draw();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x001DC80C File Offset: 0x001DAA0C
		public void DrawInvModel(Matrix pos, Matrix view, Matrix proj, Model model, Texture2D texture)
		{
			float num = this.counter / 240f;
			Matrix matrix = Matrix.CreateScale(0.8f) * pos;
			model.Meshes[0].MeshParts[0].Effect = this.keyeffect;
			this.keyeffect.Parameters["World"].SetValue(matrix);
			this.keyeffect.Parameters["View"].SetValue(view);
			this.keyeffect.Parameters["Projection"].SetValue(proj);
			this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3((float)Math.Sin((double)(this.counter / 65f)), 0.7f, (float)(-(float)Math.Cos((double)(this.counter / 65f))))));
			this.keyeffect.Parameters["DiffuseLight"].SetValue(0.85f);
			this.keyeffect.Parameters["AmbientLight"].SetValue(0.32f);
			this.keyeffect.Parameters["Texture"].SetValue(texture);
			this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
			model.Meshes[0].Draw();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x001DCA28 File Offset: 0x001DAC28
		public void DrawBoxes(Matrix pos, Matrix view, Matrix proj, Model model, Texture2D texture)
		{
			float num = this.counter / 240f;
			float[] array = new float[] { -21f, -12f, -5f, 3.9f, 13f, 21f, -27f, 27f, -16f, 16f };
			float[] array2 = new float[] { -3.2f, -2.5f, 1.5f, 1.4f, -2.5f, -3.1f, 6f, 6f, 7.5f, 7.6f };
			float[] array3 = new float[] { -0.5f, -0.35f, -0.015f, -0.02f, 0.34f, 0.51f, -0.65f, 0.65f, -0.7f, 0.99f };
			int[] array4 = new int[] { 2, 3, 1, 4, 0, 5, 6, 7, 8, 9 };
			for (int i = 0; i < this.sc.ammoboxCount; i++)
			{
				Matrix matrix = Matrix.CreateScale(0.95f) * Matrix.CreateRotationY(array3[array4[i]]) * Matrix.CreateTranslation(array[array4[i]], 0f, array2[array4[i]]) * pos;
				model.Meshes[0].MeshParts[0].Effect = this.keyeffect;
				this.keyeffect.Parameters["World"].SetValue(matrix);
				this.keyeffect.Parameters["View"].SetValue(view);
				this.keyeffect.Parameters["Projection"].SetValue(proj);
				this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3((float)Math.Sin((double)(this.counter / 65f)), 0.7f, (float)(-(float)Math.Cos((double)(this.counter / 65f))))));
				this.keyeffect.Parameters["DiffuseLight"].SetValue(0.9f);
				this.keyeffect.Parameters["AmbientLight"].SetValue(0.42f);
				this.keyeffect.Parameters["Texture"].SetValue(texture);
				this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
				model.Meshes[0].Draw();
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x001DCC3C File Offset: 0x001DAE3C
		public void DrawCogs(Matrix pos, Matrix view, Matrix proj, Model model, Texture2D texture)
		{
			Matrix matrix = Matrix.CreateTranslation(10.58f, 0.165f, 0f);
			float[] array = new float[] { 9.9f, -12f };
			float[] array2 = new float[] { -3.5f, -4.2f };
			float[] array3 = new float[] { 3.3415902f, 2.8115902f };
			float num = -MathHelper.ToRadians(this.cogcounter) * 0.5f + MathHelper.ToRadians(4f);
			Matrix matrix2 = Matrix.CreateRotationX(MathHelper.ToRadians(25.892f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-4.823f)) * Matrix.CreateTranslation(0f, 4.268f, 0f);
			int num2 = 1;
			if (this.sc.cogCount > 1)
			{
				num2 = 2;
			}
			for (int i = 0; i < num2; i++)
			{
				if (i == 1)
				{
					num *= 2.2f;
				}
				Matrix matrix3 = Matrix.CreateRotationY(num) * matrix2 * Matrix.CreateScale(0.85f) * Matrix.CreateRotationY(array3[i]) * Matrix.CreateTranslation(array[i], 0f, array2[i]) * pos;
				model.Meshes[0].MeshParts[0].Effect = this.keyeffect;
				this.keyeffect.Parameters["World"].SetValue(matrix3);
				this.keyeffect.Parameters["View"].SetValue(view);
				this.keyeffect.Parameters["Projection"].SetValue(proj);
				this.keyeffect.Parameters["LightDirection"].SetValue(Vector3.Normalize(new Vector3((float)Math.Sin((double)(this.counter / 65f)), 0.7f, (float)(-(float)Math.Cos((double)(this.counter / 65f))))));
				this.keyeffect.Parameters["DiffuseLight"].SetValue(0.9f);
				this.keyeffect.Parameters["AmbientLight"].SetValue(0.42f);
				this.keyeffect.Parameters["Texture"].SetValue(texture);
				this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
				model.Meshes[0].Draw();
				float num3 = this.cogcounter;
				if (i == 1)
				{
					num3 *= 2.2f;
				}
				matrix3 = Matrix.CreateRotationY(MathHelper.ToRadians(num3)) * matrix * matrix2 * Matrix.CreateScale(0.85f) * Matrix.CreateRotationY(array3[i]) * Matrix.CreateTranslation(array[i], 0f, array2[i]) * pos;
				model.Meshes[1].MeshParts[0].Effect = this.keyeffect;
				this.keyeffect.Parameters["World"].SetValue(matrix3);
				this.keyeffect.CurrentTechnique = this.keyeffect.Techniques["mykey"];
				model.Meshes[1].Draw();
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x001DCFC4 File Offset: 0x001DB1C4
		public void DrawInvSkull(Vector3 cc, Matrix pos, Matrix view, Matrix proj, Vector3 campos, Model model, Texture2D texture, Vector3 lite, float offset)
		{
			Matrix matrix = Matrix.CreateScale(0.25f) * pos;
			model.Meshes[0].MeshParts[0].Effect = this.skulleffect;
			Matrix matrix2 = Matrix.Transpose(Matrix.Invert(matrix * model.Meshes[0].ParentBone.Transform));
			this.skulleffect.Parameters["World"].SetValue(matrix * model.Meshes[0].ParentBone.Transform);
			this.skulleffect.Parameters["View"].SetValue(view);
			this.skulleffect.Parameters["Projection"].SetValue(proj);
			this.skulleffect.Parameters["move"].SetValue(this.sc.myTimer / 340f);
			this.skulleffect.Parameters["move2"].SetValue(this.sc.myTimer / 360f);
			this.skulleffect.Parameters["offsetz"].SetValue(offset);
			this.skulleffect.Parameters["tint"].SetValue(cc);
			this.skulleffect.Parameters["notch"].SetValue(this.sc.notch);
			this.skulleffect.Parameters["Texture"].SetValue(this.skullTexture);
			this.skulleffect.Parameters["ReflectionMap"].SetValue(this.skyboxTexture);
			this.skulleffect.Parameters["CameraPosition"].SetValue(campos);
			this.skulleffect.Parameters["lite"].SetValue(lite);
			this.skulleffect.Parameters["ReflectionView"].SetValue(matrix2);
			this.skulleffect.CurrentTechnique = this.skulleffect.Techniques[0];
			model.Meshes[0].Draw();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x001DD210 File Offset: 0x001DB410
		public void DrawInvTusk(Vector3 cc, Matrix pos, Matrix view, Matrix proj, Vector3 campos, Model model, Vector3 lite, float offset)
		{
			Matrix matrix = Matrix.CreateScale(0.25f) * pos;
			model.Meshes[0].MeshParts[0].Effect = this.skulleffect;
			Matrix matrix2 = Matrix.Transpose(Matrix.Invert(matrix * model.Meshes[0].ParentBone.Transform));
			this.skulleffect.Parameters["World"].SetValue(matrix * model.Meshes[0].ParentBone.Transform);
			this.skulleffect.Parameters["View"].SetValue(view);
			this.skulleffect.Parameters["Projection"].SetValue(proj);
			this.skulleffect.Parameters["move"].SetValue(this.sc.myTimer / 340f);
			this.skulleffect.Parameters["move2"].SetValue(this.sc.myTimer / 360f);
			this.skulleffect.Parameters["offsetz"].SetValue(offset);
			this.skulleffect.Parameters["tint"].SetValue(cc);
			this.skulleffect.Parameters["notch"].SetValue(this.sc.notch);
			this.skulleffect.Parameters["Texture"].SetValue(this.tuskTexture);
			this.skulleffect.Parameters["ReflectionMap"].SetValue(this.skyboxTexture);
			this.skulleffect.Parameters["CameraPosition"].SetValue(campos);
			this.skulleffect.Parameters["lite"].SetValue(lite);
			this.skulleffect.Parameters["ReflectionView"].SetValue(matrix2);
			this.skulleffect.CurrentTechnique = this.skulleffect.Techniques[0];
			model.Meshes[0].Draw();
		}

		// Token: 0x04002235 RID: 8757
		public string kb = "bonus";

		// Token: 0x04002236 RID: 8758
		public string ke = "exitkey";

		// Token: 0x04002237 RID: 8759
		public string kg = "goggles";

		// Token: 0x04002238 RID: 8760
		public string km = "minimap";

		// Token: 0x04002239 RID: 8761
		public string kc1 = "cog1";

		// Token: 0x0400223A RID: 8762
		public string kc2 = "cog2";

		// Token: 0x0400223B RID: 8763
		public string kc3 = "cog3";

		// Token: 0x0400223C RID: 8764
		public string ka1 = "ammo1";

		// Token: 0x0400223D RID: 8765
		public string ka2 = "ammo2";

		// Token: 0x0400223E RID: 8766
		public string ka3 = "ammo3";

		// Token: 0x0400223F RID: 8767
		public string kh1 = "hat1";

		// Token: 0x04002240 RID: 8768
		public string kh2 = "hat2";

		// Token: 0x04002241 RID: 8769
		public string kh3 = "hat3";

		// Token: 0x04002242 RID: 8770
		public string kf1 = "flashlight1";

		// Token: 0x04002243 RID: 8771
		public string kf2 = "flashlight2";

		// Token: 0x04002244 RID: 8772
		public string kf3 = "flashlight3";

		// Token: 0x04002245 RID: 8773
		private Texture2D flash1;

		// Token: 0x04002246 RID: 8774
		private Texture2D flash2;

		// Token: 0x04002247 RID: 8775
		private Texture2D flash3;

		// Token: 0x04002248 RID: 8776
		private Texture2D ammoboxTexture;

		// Token: 0x04002249 RID: 8777
		private Texture2D goggleTexture;

		// Token: 0x0400224A RID: 8778
		private Texture2D cogTexture;

		// Token: 0x0400224B RID: 8779
		private Texture2D mapTexture;

		// Token: 0x0400224C RID: 8780
		private Texture2D exitkeyTexture;

		// Token: 0x0400224D RID: 8781
		private Texture2D skullTexture;

		// Token: 0x0400224E RID: 8782
		private Texture2D tuskTexture;

		// Token: 0x0400224F RID: 8783
		private Texture2D skyboxTexture;

		// Token: 0x04002250 RID: 8784
		private Model flashlight1;

		// Token: 0x04002251 RID: 8785
		private Model flashlight2;

		// Token: 0x04002252 RID: 8786
		private Model flashlight3;

		// Token: 0x04002253 RID: 8787
		private Model ammobox;

		// Token: 0x04002254 RID: 8788
		private Model goggles1;

		// Token: 0x04002255 RID: 8789
		private Model exitkey;

		// Token: 0x04002256 RID: 8790
		private Model cog;

		// Token: 0x04002257 RID: 8791
		private Model map;

		// Token: 0x04002258 RID: 8792
		private Model skullcc;

		// Token: 0x04002259 RID: 8793
		private Model tuskcc;

		// Token: 0x0400225A RID: 8794
		private Model goldpile;

		// Token: 0x0400225B RID: 8795
		public bool nearItems;

		// Token: 0x0400225C RID: 8796
		public bool nearExit;

		// Token: 0x0400225D RID: 8797
		public bool lookatitems;

		// Token: 0x0400225E RID: 8798
		private float speed = 3f;

		// Token: 0x0400225F RID: 8799
		public int propHit = 5;

		// Token: 0x04002260 RID: 8800
		public int blimpHit = 50;

		// Token: 0x04002261 RID: 8801
		public int carryHit = 5;

		// Token: 0x04002262 RID: 8802
		public Vector3 blimpTrans = new Vector3(1000f, 1500f, 1000f);

		// Token: 0x04002263 RID: 8803
		public float blimpRot;

		// Token: 0x04002264 RID: 8804
		public Vector3 blimpVeloc = Vector3.Zero;

		// Token: 0x04002265 RID: 8805
		public Vector3 ray = Vector3.Zero;

		// Token: 0x04002266 RID: 8806
		public float rayAmt;

		// Token: 0x04002267 RID: 8807
		public Matrix myRot = Matrix.Identity;

		// Token: 0x04002268 RID: 8808
		private Matrix myRotold = Matrix.Identity;

		// Token: 0x04002269 RID: 8809
		private ScreenManager sc;

		// Token: 0x0400226A RID: 8810
		private Model key;

		// Token: 0x0400226B RID: 8811
		private Effect keyeffect;

		// Token: 0x0400226C RID: 8812
		private Effect skulleffect;

		// Token: 0x0400226D RID: 8813
		private ContentManager content;

		// Token: 0x0400226E RID: 8814
		private float counter;

		// Token: 0x0400226F RID: 8815
		private float cogcounter;

		// Token: 0x04002270 RID: 8816
		private int gearticker;

		// Token: 0x04002271 RID: 8817
		private Vector3 keypos1 = new Vector3(4860f, 968f, 1259f);

		// Token: 0x04002272 RID: 8818
		private Vector3 keypos2 = new Vector3(9000f, 30f, 3000f);

		// Token: 0x04002273 RID: 8819
		private Vector3 keypos3 = new Vector3(1227f, 25f, 2868f);

		// Token: 0x04002274 RID: 8820
		private Vector3 keypos4 = new Vector3(3547f, 230f, 4637f);

		// Token: 0x04002275 RID: 8821
		private Vector3 keypos5 = new Vector3(1664f, 28f, -1700f);

		// Token: 0x04002276 RID: 8822
		private Vector3 keypos6 = new Vector3(2012f, -130f, 5200f);

		// Token: 0x04002277 RID: 8823
		private Vector3 keypos7 = new Vector3(2750f, -130f, 4550f);

		// Token: 0x04002278 RID: 8824
		private Vector3 keypos8 = new Vector3(1800f, -130f, 3480f);

		// Token: 0x04002279 RID: 8825
		public Vector3 kePos;

		// Token: 0x0400227A RID: 8826
		public Vector3 kbPos;

		// Token: 0x0400227B RID: 8827
		public Vector3 kmPos;

		// Token: 0x0400227C RID: 8828
		public Vector3 kgPos;

		// Token: 0x0400227D RID: 8829
		public Vector3 kh1Pos;

		// Token: 0x0400227E RID: 8830
		public Vector3 kh2Pos;

		// Token: 0x0400227F RID: 8831
		public Vector3 kh3Pos;

		// Token: 0x04002280 RID: 8832
		public Vector3 ka1Pos;

		// Token: 0x04002281 RID: 8833
		public Vector3 ka2Pos;

		// Token: 0x04002282 RID: 8834
		public Vector3 ka3Pos;

		// Token: 0x04002283 RID: 8835
		public Vector3 kf1Pos;

		// Token: 0x04002284 RID: 8836
		public Vector3 kf2Pos;

		// Token: 0x04002285 RID: 8837
		public Vector3 kf3Pos;

		// Token: 0x04002286 RID: 8838
		public Vector3 kc1Pos;

		// Token: 0x04002287 RID: 8839
		public Vector3 kc2Pos;

		// Token: 0x04002288 RID: 8840
		public Vector3 kc3Pos;

		// Token: 0x04002289 RID: 8841
		public bool kbHide = true;

		// Token: 0x0400228A RID: 8842
		public bool keHide = true;

		// Token: 0x0400228B RID: 8843
		public bool kmHide = true;

		// Token: 0x0400228C RID: 8844
		public bool kgHide = true;

		// Token: 0x0400228D RID: 8845
		public bool kh1Hide = true;

		// Token: 0x0400228E RID: 8846
		public bool kh2Hide = true;

		// Token: 0x0400228F RID: 8847
		public bool kh3Hide = true;

		// Token: 0x04002290 RID: 8848
		public bool ka1Hide = true;

		// Token: 0x04002291 RID: 8849
		public bool ka2Hide = true;

		// Token: 0x04002292 RID: 8850
		public bool ka3Hide = true;

		// Token: 0x04002293 RID: 8851
		public bool kf1Hide = true;

		// Token: 0x04002294 RID: 8852
		public bool kf2Hide = true;

		// Token: 0x04002295 RID: 8853
		public bool kf3Hide = true;

		// Token: 0x04002296 RID: 8854
		public bool kc1Hide = true;

		// Token: 0x04002297 RID: 8855
		public bool kc2Hide = true;

		// Token: 0x04002298 RID: 8856
		public bool kc3Hide = true;

		// Token: 0x04002299 RID: 8857
		public Vector3 flashlight1pos;

		// Token: 0x0400229A RID: 8858
		public Vector3 flashlight2pos;

		// Token: 0x0400229B RID: 8859
		public Vector3 flashlight3pos;

		// Token: 0x0400229C RID: 8860
		public Vector3 ammoboxpos;

		// Token: 0x0400229D RID: 8861
		public Vector3 gogglepos;

		// Token: 0x0400229E RID: 8862
		public Matrix flashlight1matrix;

		// Token: 0x0400229F RID: 8863
		public Matrix flashlight2matrix;

		// Token: 0x040022A0 RID: 8864
		public Matrix flashlight3matrix;

		// Token: 0x040022A1 RID: 8865
		public Matrix ammoboxmatrix;

		// Token: 0x040022A2 RID: 8866
		public Matrix gogglematrix;

		// Token: 0x040022A3 RID: 8867
		public Vector3 mappos;

		// Token: 0x040022A4 RID: 8868
		public Vector3 cogpos;

		// Token: 0x040022A5 RID: 8869
		public Vector3 exitkeypos;

		// Token: 0x040022A6 RID: 8870
		public Vector3 skullpos;

		// Token: 0x040022A7 RID: 8871
		public Matrix mapmatrix;

		// Token: 0x040022A8 RID: 8872
		public Matrix cogmatrix;

		// Token: 0x040022A9 RID: 8873
		public Matrix exitkeymatrix;

		// Token: 0x040022AA RID: 8874
		public Matrix skullmatrix = Matrix.Identity;

		// Token: 0x040022AB RID: 8875
		public Matrix rs1Matrix = Matrix.Identity;

		// Token: 0x040022AC RID: 8876
		public Matrix rs2Matrix = Matrix.Identity;

		// Token: 0x040022AD RID: 8877
		public Matrix rs3Matrix = Matrix.Identity;

		// Token: 0x040022AE RID: 8878
		public bool rsFound;

		// Token: 0x040022AF RID: 8879
		public Vector3 rs2Pos = Vector3.Zero;

		// Token: 0x040022B0 RID: 8880
		public Vector3 tuskpos;

		// Token: 0x040022B1 RID: 8881
		public Matrix tuskmatrix = Matrix.Identity;

		// Token: 0x040022B2 RID: 8882
		public Matrix tt1Matrix = Matrix.Identity;

		// Token: 0x040022B3 RID: 8883
		public Matrix tt2Matrix = Matrix.Identity;

		// Token: 0x040022B4 RID: 8884
		public Matrix tt3Matrix = Matrix.Identity;

		// Token: 0x040022B5 RID: 8885
		public bool ttFound;

		// Token: 0x040022B6 RID: 8886
		public Vector3 tt2Pos = Vector3.Zero;

		// Token: 0x040022B7 RID: 8887
		public bool keyFlashlight1 = true;

		// Token: 0x040022B8 RID: 8888
		public bool keyFlashlight2 = true;

		// Token: 0x040022B9 RID: 8889
		public bool keyFlashlight3 = true;

		// Token: 0x040022BA RID: 8890
		public bool keyAmmobox = true;

		// Token: 0x040022BB RID: 8891
		public bool keyGoggles = true;

		// Token: 0x040022BC RID: 8892
		public bool keyMap = true;

		// Token: 0x040022BD RID: 8893
		public bool keySkull1 = true;

		// Token: 0x040022BE RID: 8894
		public bool keySkull2 = true;

		// Token: 0x040022BF RID: 8895
		public bool keySkull3 = true;

		// Token: 0x040022C0 RID: 8896
		public bool keyTusk1 = true;

		// Token: 0x040022C1 RID: 8897
		public bool keyTusk2 = true;

		// Token: 0x040022C2 RID: 8898
		public bool keyTusk3 = true;

		// Token: 0x040022C3 RID: 8899
		public bool keyCog = true;

		// Token: 0x040022C4 RID: 8900
		public bool keyExitkey = true;

		// Token: 0x040022C5 RID: 8901
		private SoundEffect geartick;

		// Token: 0x040022C6 RID: 8902
		private Curve targetX;

		// Token: 0x040022C7 RID: 8903
		private Curve targetZ;

		// Token: 0x040022C8 RID: 8904
		private float tx;

		// Token: 0x040022C9 RID: 8905
		private float tz;

		// Token: 0x040022CA RID: 8906
		public float targetRate;

		// Token: 0x040022CB RID: 8907
		public int targetWait;

		// Token: 0x040022CC RID: 8908
		public bool newAction;

		// Token: 0x040022CD RID: 8909
		public float targetDist;

		// Token: 0x040022CE RID: 8910
		public int curveIndex = 1;

		// Token: 0x040022CF RID: 8911
		public float loop;

		// Token: 0x040022D0 RID: 8912
		public float cuttyDistance = 20000f;

		// Token: 0x040022D1 RID: 8913
		public float fps;

		// Token: 0x040022D2 RID: 8914
		private bool atGears;
	}
}
