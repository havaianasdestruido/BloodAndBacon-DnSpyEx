using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;
using SkinnedModel;

namespace Blood
{
	// Token: 0x02000115 RID: 277
	internal class Cutty
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0025BF10 File Offset: 0x0025A110
		public Cutty(int index, int type, string name, int healthpoints)
		{
			Cutty.shortestDistance = 20000f;
			Cutty.bit = 0;
			Cutty.uvIndex = 0;
			Cutty.xcoord = 0;
			Cutty.ycoord = 0;
			Cutty.cuttyCount = 0;
			Cutty.allplayersReady = false;
			Cutty.cuttyDoneSpeech = false;
			Cutty.homingLocal = -1;
			Cutty.homingRemote = -1;
			Cutty.speeches = 0;
			Cutty.whichPigTalks = 0;
			Cutty.someoneTalking = false;
			Cutty.gonnaHealindex = -1;
			Cutty.homingLocal = -1;
			Cutty.homingRemote = -1;
			Cutty.nameBuild.Length = 0;
			Cutty.nameBuild.Append(name);
			Random random = new Random();
			this.health = (ushort)healthpoints;
			this.startHealth = (ushort)healthpoints;
			if (type == 3)
			{
				this.myIndex = index;
				this.myType = 3;
				this.speechList = new List<int> { 2 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6440f, 0f, 4526f);
				this.cuttyScale = 0.8f;
				this.speechInclude = new List<int> { 0 };
				this.speechCurveindex = 4;
				this.startCurveindex = 1;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 0)
			{
				this.myIndex = index;
				this.myType = 0;
				this.speechList = new List<int> { 17, 18 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6440f, 0f, 4526f);
				this.cuttyScale = 0.92f;
				this.speechInclude = new List<int> { 0 };
				this.speechCurveindex = 4;
				this.startCurveindex = 1;
				this.startLoop = (float)random.Next(10, 95) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 1)
			{
				this.myIndex = index;
				this.myType = 1;
				this.speechList = new List<int> { 17, 18 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(5440f, 0f, 1500f);
				this.cuttyScale = 0.6f;
				this.speechInclude = new List<int> { 1 };
				this.speechCurveindex = 5;
				this.startCurveindex = 2;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 11f;
				this.targetRate = 0f;
			}
			if (type == 2)
			{
				this.myIndex = index;
				this.myType = 2;
				this.speechList = new List<int> { 17, 18 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6140f, 0f, 5826f);
				this.cuttyScale = 0.66f;
				this.speechInclude = new List<int> { 13 };
				this.speechCurveindex = 6;
				this.startCurveindex = 2;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 8f;
				this.targetRate = 0f;
			}
			if (type == 13)
			{
				this.myIndex = index;
				this.myType = 13;
				this.speechList = new List<int> { 13, 14 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6440f, 0f, 4526f);
				this.cuttyScale = 0.87f;
				this.speechInclude = new List<int> { 0 };
				this.speechCurveindex = 4;
				this.startCurveindex = 1;
				this.startLoop = (float)random.Next(10, 95) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 14)
			{
				this.myIndex = index;
				this.myType = 14;
				this.speechList = new List<int> { 13, 14 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(5440f, 0f, 1500f);
				this.cuttyScale = 0.6f;
				this.speechInclude = new List<int> { 1 };
				this.speechCurveindex = 5;
				this.startCurveindex = 2;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 11f;
				this.targetRate = 0f;
			}
			if (type == 4)
			{
				this.myIndex = index;
				this.myType = 4;
				this.speechList = new List<int> { 15, 16 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6440f, 0f, 4526f);
				this.cuttyScale = 0.5f;
				this.speechInclude = new List<int> { 0 };
				this.speechCurveindex = 4;
				this.startCurveindex = 1;
				this.startLoop = (float)random.Next(10, 95) / 100f;
				this.startRate = 7f;
				this.targetRate = 0f;
			}
			if (type == 5)
			{
				this.myIndex = index;
				this.myType = 5;
				this.speechList = new List<int> { 15, 16 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(5440f, 0f, 1500f);
				this.cuttyScale = 1f;
				this.speechInclude = new List<int> { 1 };
				this.speechCurveindex = 5;
				this.startCurveindex = 2;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 6)
			{
				this.myIndex = index;
				this.myType = 6;
				this.speechList = new List<int> { 21, 22 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(5440f, 0f, 1500f);
				this.cuttyScale = 0.9f;
				this.speechInclude = new List<int> { 1 };
				this.speechCurveindex = 5;
				this.startCurveindex = 3;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 7)
			{
				this.myIndex = index;
				this.myType = 7;
				this.speechList = new List<int> { 21, 22 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6140f, 0f, 5826f);
				this.cuttyScale = 0.85f;
				this.speechInclude = new List<int> { 13 };
				this.speechCurveindex = 6;
				this.startCurveindex = 1;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 8)
			{
				this.myIndex = index;
				this.myType = 3;
				this.speechList = new List<int> { 21, 22 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6440f, 0f, 4526f);
				this.cuttyScale = 0.82f;
				this.speechInclude = new List<int> { 0 };
				this.speechCurveindex = 4;
				this.startCurveindex = 2;
				this.startLoop = (float)random.Next(10, 95) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			if (type == 11)
			{
				this.myIndex = index;
				this.myType = 11;
				this.speechList = new List<int> { 19, 20 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(6440f, 0f, 4526f);
				this.cuttyScale = 0.7f;
				this.speechInclude = new List<int> { 0 };
				this.speechCurveindex = 4;
				this.startCurveindex = 1;
				this.startLoop = (float)random.Next(10, 95) / 100f;
				this.startRate = 7f;
				this.targetRate = 0f;
			}
			if (type == 12)
			{
				this.myIndex = index;
				this.myType = 12;
				this.speechList = new List<int> { 19, 20 };
				Cutty.speeches = 0;
				this.cuttyPos = new Vector3(5440f, 0f, 1500f);
				this.cuttyScale = 0.8f;
				this.speechInclude = new List<int> { 1 };
				this.speechCurveindex = 5;
				this.startCurveindex = 2;
				this.startLoop = (float)random.Next(20, 90) / 100f;
				this.startRate = 9f;
				this.targetRate = 0f;
			}
			this.oldcuttyPos = this.cuttyPos;
			this.cuttyRot = 3.14f + (float)random.Next(-10, 10) / 100f;
			this.curveIndex = this.speechCurveindex;
			this.loop = 0.5f;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0025CCE0 File Offset: 0x0025AEE0
		public void LoadContent(ScreenManager sc, ContentManager cc)
		{
			this.sc = sc;
			this.content = cc;
			this.contentB = new ContentManager(sc.Game.Services, "Content");
			this.setbreakLevels();
			this.oldDF = this.df;
			this.spriteBatch = new SpriteBatch(sc.GraphicsDevice);
			this.rr = new Random();
			this.gallop = new SoundEffects(1);
			this.gallop.sound[0] = this.contentB.Load<SoundEffect>("audio\\gallop").CreateInstance();
			this.gallop.sound[0].IsLooped = true;
			this.gallop.sound[0].Play();
			this.gallop.sound[0].Volume = 0f;
			this.burning = new SoundEffects(1);
			this.burning.sound[0] = this.contentB.Load<SoundEffect>("audio\\burning").CreateInstance();
			this.burning.sound[0].IsLooped = true;
			this.burning.sound[0].Play();
			this.burning.sound[0].Volume = 0f;
			this.pigModel = sc.pigModel;
			this.pigIndex = 0;
			this.eyeglow = this.content.Load<Model>("Models\\eyeglow");
			this.solidSkin = this.content.Load<Effect>("effects\\SolidSkinEffect");
			this.solidSkin.Parameters["World"].SetValue(Matrix.CreateTranslation(0f, 0f, 0f));
			this.glowEffect = this.content.Load<Effect>("effects\\glowEffect2");
			this.glowEffect.CurrentTechnique = this.glowEffect.Techniques["EdgeDetect"];
			float num = 30f;
			Vector2[] array = new Vector2[12];
			for (int i = 0; i < 12; i++)
			{
				float num2 = (float)(-(float)i);
				array[i] = new Vector2((float)Math.Sin((double)MathHelper.ToRadians(num2 * num)), -1.4f * (float)Math.Cos((double)MathHelper.ToRadians(180f + num2 * num)));
			}
			this.glowEffect.Parameters["offsets"].SetValue(array);
			this.glowEffect.Parameters["glowdist"].SetValue(0.005f);
			if (this.myType == 0 || this.myType == 3 || this.myType == 4 || this.myType == 5)
			{
				this.sparks = new shockSystem(sc.Game, this.contentB);
			}
			if (this.myType == 1)
			{
				this.sparks = new shock2System(sc.Game, this.contentB);
			}
			if (this.myType == 2)
			{
				this.sparks = new shock3System(sc.Game, this.contentB);
			}
			if (this.myType == 6)
			{
				this.sparks = new shock3System(sc.Game, this.contentB);
			}
			if (this.myType == 7)
			{
				this.sparks = new shock4System(sc.Game, this.contentB);
			}
			if (this.myType == 11)
			{
				this.sparks = new shock5System(sc.Game, this.contentB);
			}
			if (this.myType == 12)
			{
				this.sparks = new shock5System(sc.Game, this.contentB);
			}
			if (this.myType == 13)
			{
				this.sparks = new shock4System(sc.Game, this.contentB);
			}
			if (this.myType == 14)
			{
				this.sparks = new shock4System(sc.Game, this.contentB);
			}
			this.sparks.Initialize();
			this.sparks.LoadContent(sc.GraphicsDevice);
			this.dots = new dotSystem(sc.Game, this.contentB);
			this.dots.Initialize();
			this.dots.LoadContent(sc.GraphicsDevice);
			Model model = this.contentB.Load<Model>("Models//block1");
			if (this.myType == 3)
			{
				this.eyeTexture1 = this.contentB.Load<Texture2D>("texture\\eyeglow1");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture1);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1");
				model = this.contentB.Load<Model>("Models//block1");
			}
			if (this.myType == 0)
			{
				this.eyeTexture1 = this.contentB.Load<Texture2D>("texture\\eyeglow5");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture1);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1Blue");
				model = this.contentB.Load<Model>("Models//blockBlu");
			}
			if (this.myType == 1)
			{
				this.eyeTexture2 = this.contentB.Load<Texture2D>("texture\\eyeglow3");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture2);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1Grn2");
				model = this.contentB.Load<Model>("Models//blockGrn2");
			}
			if (this.myType == 2)
			{
				this.eyeTexture2 = this.contentB.Load<Texture2D>("texture\\eyeglow3");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture2);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1Grn");
				model = this.contentB.Load<Model>("Models//blockGrn");
			}
			if (this.myType == 13)
			{
				this.eyeTexture1 = this.contentB.Load<Texture2D>("texture\\eyeglow2");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture1);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1Red2");
				model = this.contentB.Load<Model>("Models//blockRed");
			}
			if (this.myType == 14)
			{
				this.eyeTexture2 = this.contentB.Load<Texture2D>("texture\\eyeglow2");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture2);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1Red");
				model = this.contentB.Load<Model>("Models//blockRed");
			}
			if (this.myType == 4 || this.myType == 5)
			{
				this.eyeTexture2 = this.contentB.Load<Texture2D>("texture\\eyeglow5");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture2);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1Gray");
				model = this.contentB.Load<Model>("Models//blockGray");
			}
			if (this.myType == 6)
			{
				this.eyeTexture2 = this.contentB.Load<Texture2D>("texture\\eyeglow6");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture2);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\veprBrown2");
				model = this.contentB.Load<Model>("Models//block1");
			}
			if (this.myType == 7)
			{
				this.eyeTexture1 = this.contentB.Load<Texture2D>("texture\\eyeglow6");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture1);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\vepr1");
				model = this.contentB.Load<Model>("Models//block1");
			}
			if (this.myType == 11)
			{
				this.eyeTexture1 = this.contentB.Load<Texture2D>("texture\\eyeglow12");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture1);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\veprWhite");
				model = this.contentB.Load<Model>("Models//block1");
			}
			if (this.myType == 12)
			{
				this.eyeTexture1 = this.contentB.Load<Texture2D>("texture\\eyeglow12");
				this.eyeEffect = this.contentB.Load<Effect>("effects\\eyeEffect");
				this.eyeEffect.Parameters["Texture"].SetValue(this.eyeTexture1);
				this.pigTexture = this.contentB.Load<Texture2D>("npc\\veprWhite");
				model = this.contentB.Load<Model>("Models//block1");
			}
			SkinningData skinningData = this.pigModel.Tag as SkinningData;
			this.pig1 = new AnimationPlayer[7];
			this.pig1[0] = new AnimationPlayer(skinningData);
			AnimationClip animationClip = skinningData.AnimationClips["cuttyrun"];
			this.pig1[0].StartClip(animationClip);
			this.pig1[1] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["cuttywalk"];
			this.pig1[1].StartClip(animationClip);
			this.pig1[2] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["cuttywait"];
			this.pig1[2].StartClip(animationClip);
			this.pig1[3] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["cuttybite"];
			this.pig1[3].StartClip(animationClip);
			this.pig1[4] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["cuttydie"];
			this.pig1[4].StartClip(animationClip);
			this.pig1[5] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["cuttyjump"];
			this.pig1[5].StartClip(animationClip);
			this.pig1[6] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["cuttyheal"];
			this.pig1[6].StartClip(animationClip);
			this.a = this.pig1[0];
			this.b = this.pig1[0];
			this.clipIndexA = 0;
			this.clipRate = new float[7];
			this.clipRate[0] = 7f;
			this.clipRate[1] = 2f;
			this.clipRate[2] = 0f;
			this.clipRate[3] = 0f;
			this.clipRate[4] = 0f;
			this.clipRate[5] = 0f;
			this.clipRate[6] = 0f;
			this.pigJaw = new float[2200];
			this.pigSkin = this.contentB.Load<Effect>("effects/cuttySkin2");
			this.pigSkin.Parameters["World"].SetValue(Matrix.CreateTranslation(0f, 0f, 0f));
			this.pigSkin.Parameters["Texture"].SetValue(this.pigTexture);
			this.fireball = default(Cutty.hole);
			this.fireball.stainR = new Vector4[80];
			this.fireball.stainR[0] = new Vector4(0f, 0f, 133f, 200f);
			this.fireball.stainR[1] = new Vector4(798f, 0f, 133f, 200f);
			this.fireball.stainR[2] = new Vector4(532f, 400f, 133f, 200f);
			this.fireball.stainR[3] = new Vector4(665f, 800f, 133f, 200f);
			this.fireball.stainR[4] = new Vector4(1064f, 400f, 133f, 200f);
			this.fireball.stainR[5] = new Vector4(1197f, 600f, 133f, 200f);
			this.fireball.stainR[6] = new Vector4(1463f, 800f, 133f, 200f);
			this.fireball.stainR[7] = new Vector4(931f, 1000f, 133f, 200f);
			this.fireball.stainR[8] = new Vector4(1197f, 1000f, 133f, 200f);
			this.fireball.stainR[9] = new Vector4(133f, 0f, 133f, 200f);
			this.fireball.stainR[10] = new Vector4(266f, 0f, 133f, 200f);
			this.fireball.stainR[11] = new Vector4(0f, 200f, 133f, 200f);
			this.fireball.stainR[12] = new Vector4(133f, 200f, 133f, 200f);
			this.fireball.stainR[13] = new Vector4(266f, 200f, 133f, 200f);
			this.fireball.stainR[14] = new Vector4(399f, 0f, 133f, 200f);
			this.fireball.stainR[15] = new Vector4(532f, 0f, 133f, 200f);
			this.fireball.stainR[16] = new Vector4(399f, 200f, 133f, 200f);
			this.fireball.stainR[17] = new Vector4(665f, 0f, 133f, 200f);
			this.fireball.stainR[18] = new Vector4(532f, 200f, 133f, 200f);
			this.fireball.stainR[19] = new Vector4(665f, 200f, 133f, 200f);
			this.fireball.stainR[20] = new Vector4(798f, 200f, 133f, 200f);
			this.fireball.stainR[21] = new Vector4(0f, 400f, 133f, 200f);
			this.fireball.stainR[22] = new Vector4(133f, 400f, 133f, 200f);
			this.fireball.stainR[23] = new Vector4(0f, 600f, 133f, 200f);
			this.fireball.stainR[24] = new Vector4(266f, 400f, 133f, 200f);
			this.fireball.stainR[25] = new Vector4(133f, 600f, 133f, 200f);
			this.fireball.stainR[26] = new Vector4(399f, 400f, 133f, 200f);
			this.fireball.stainR[27] = new Vector4(0f, 800f, 133f, 200f);
			this.fireball.stainR[28] = new Vector4(266f, 600f, 133f, 200f);
			this.fireball.stainR[29] = new Vector4(133f, 800f, 133f, 200f);
			this.fireball.stainR[30] = new Vector4(399f, 600f, 133f, 200f);
			this.fireball.stainR[31] = new Vector4(665f, 400f, 133f, 200f);
			this.fireball.stainR[32] = new Vector4(266f, 800f, 133f, 200f);
			this.fireball.stainR[33] = new Vector4(532f, 600f, 133f, 200f);
			this.fireball.stainR[34] = new Vector4(798f, 400f, 133f, 200f);
			this.fireball.stainR[35] = new Vector4(399f, 800f, 133f, 200f);
			this.fireball.stainR[36] = new Vector4(665f, 600f, 133f, 200f);
			this.fireball.stainR[37] = new Vector4(532f, 800f, 133f, 200f);
			this.fireball.stainR[38] = new Vector4(798f, 600f, 133f, 200f);
			this.fireball.stainR[39] = new Vector4(798f, 800f, 133f, 200f);
			this.fireball.stainR[40] = new Vector4(931f, 0f, 133f, 200f);
			this.fireball.stainR[41] = new Vector4(1064f, 0f, 133f, 200f);
			this.fireball.stainR[42] = new Vector4(931f, 200f, 133f, 200f);
			this.fireball.stainR[43] = new Vector4(1197f, 0f, 133f, 200f);
			this.fireball.stainR[44] = new Vector4(1064f, 200f, 133f, 200f);
			this.fireball.stainR[45] = new Vector4(1330f, 0f, 133f, 200f);
			this.fireball.stainR[46] = new Vector4(931f, 400f, 133f, 200f);
			this.fireball.stainR[47] = new Vector4(1197f, 200f, 133f, 200f);
			this.fireball.stainR[48] = new Vector4(1463f, 0f, 133f, 200f);
			this.fireball.stainR[49] = new Vector4(1330f, 200f, 133f, 200f);
			this.fireball.stainR[50] = new Vector4(931f, 600f, 133f, 200f);
			this.fireball.stainR[51] = new Vector4(1596f, 0f, 133f, 200f);
			this.fireball.stainR[52] = new Vector4(1197f, 400f, 133f, 200f);
			this.fireball.stainR[53] = new Vector4(1463f, 200f, 133f, 200f);
			this.fireball.stainR[54] = new Vector4(1064f, 600f, 133f, 200f);
			this.fireball.stainR[55] = new Vector4(1729f, 0f, 133f, 200f);
			this.fireball.stainR[56] = new Vector4(1330f, 400f, 133f, 200f);
			this.fireball.stainR[57] = new Vector4(931f, 800f, 133f, 200f);
			this.fireball.stainR[58] = new Vector4(1596f, 200f, 133f, 200f);
			this.fireball.stainR[59] = new Vector4(1463f, 400f, 133f, 200f);
			this.fireball.stainR[60] = new Vector4(1064f, 800f, 133f, 200f);
			this.fireball.stainR[61] = new Vector4(1729f, 200f, 133f, 200f);
			this.fireball.stainR[62] = new Vector4(1330f, 600f, 133f, 200f);
			this.fireball.stainR[63] = new Vector4(1596f, 400f, 133f, 200f);
			this.fireball.stainR[64] = new Vector4(1197f, 800f, 133f, 200f);
			this.fireball.stainR[65] = new Vector4(1463f, 600f, 133f, 200f);
			this.fireball.stainR[66] = new Vector4(1729f, 400f, 133f, 200f);
			this.fireball.stainR[67] = new Vector4(1330f, 800f, 133f, 200f);
			this.fireball.stainR[68] = new Vector4(1596f, 600f, 133f, 200f);
			this.fireball.stainR[69] = new Vector4(1729f, 600f, 133f, 200f);
			this.fireball.stainR[70] = new Vector4(1596f, 800f, 133f, 200f);
			this.fireball.stainR[71] = new Vector4(1729f, 800f, 133f, 200f);
			this.fireball.stainR[72] = new Vector4(0f, 1000f, 133f, 200f);
			this.fireball.stainR[73] = new Vector4(133f, 1000f, 133f, 200f);
			this.fireball.stainR[74] = new Vector4(266f, 1000f, 133f, 200f);
			this.fireball.stainR[75] = new Vector4(399f, 1000f, 133f, 200f);
			this.fireball.stainR[76] = new Vector4(532f, 1000f, 133f, 200f);
			this.fireball.stainR[77] = new Vector4(665f, 1000f, 133f, 200f);
			this.fireball.stainR[78] = new Vector4(798f, 1000f, 133f, 200f);
			this.fireball.stainR[79] = new Vector4(1064f, 1000f, 133f, 200f);
			this.fireball.stainMax = 0;
			this.fireball.stainIndex = 0;
			this.fireball.stainCapacity = 65;
			this.fireball.location = new Vector3[this.fireball.stainCapacity];
			this.fireball.bone = new int[this.fireball.stainCapacity];
			this.fireball.fade = new float[this.fireball.stainCapacity];
			this.fireball.frame = new int[this.fireball.stainCapacity];
			this.fireball.scale = new Matrix[this.fireball.stainCapacity];
			this.fireball.stainTrans = new Cutty.hitStream[this.fireball.stainCapacity];
			this.fireball.stainBuffer = new DynamicVertexBuffer(sc.GraphicsDevice, Cutty.vd2, this.fireball.stainCapacity, BufferUsage.WriteOnly);
			this.col_Bone = new int[] { 5, 7, 8, 2, 13, 17, 21, 26 };
			this.col_Scale[0] = 96.39428f;
			this.col_Pos[0] = new Vector3(-1.1695552f, 92.69917f, 23.254398f);
			this.col_Scale[1] = 66.92236f;
			this.col_Pos[1] = new Vector3(-2.6943295f, 89.26898f, 132.9659f);
			this.col_Scale[2] = 46.044353f;
			this.col_Pos[2] = new Vector3(-2.6942043f, 54.365963f, 213.43404f);
			this.col_Scale[3] = 59.374912f;
			this.col_Pos[3] = new Vector3(-1.1697056f, 68.89146f, -86.95499f);
			this.col_Scale[4] = 35.39259f;
			this.col_Pos[4] = new Vector3(43.993885f, -7.3778005f, 83.83201f);
			this.col_Scale[5] = 35.39259f;
			this.col_Pos[5] = new Vector3(-47.84444f, -5.906173f, 80.97031f);
			this.col_Scale[6] = 30.392591f;
			this.col_Pos[6] = new Vector3(34.47692f, -9.459024f, -102.13862f);
			this.col_Scale[7] = 30.392591f;
			this.col_Pos[7] = new Vector3(-36.929737f, -9.249221f, -103.242676f);
			this.targetDist = new float[7];
			this.targetX = new Curve[7];
			this.targetZ = new Curve[7];
			for (int j = 1; j < 7; j++)
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				StreamReader streamReader = new StreamReader("ABCDE3/target" + j + ".txt");
				this.targetDist[j] = 0f;
				this.targetX[j] = new Curve();
				this.targetZ[j] = new Curve();
				float num3 = 0f;
				float num4 = 0f;
				for (int k = 0; k < 51; k++)
				{
					float num5 = num3;
					float num6 = num4;
					num3 = float.Parse(streamReader.ReadLine(), invariantCulture);
					num4 = float.Parse(streamReader.ReadLine(), invariantCulture);
					float num7 = (float)k / 50f;
					this.targetX[j].Keys.Add(new CurveKey(num7, num3 + 3000f));
					this.targetZ[j].Keys.Add(new CurveKey(num7, num4 + 3000f));
					if (k > 0)
					{
						this.targetDist[j] += Vector2.Distance(new Vector2(num5, num6), new Vector2(num3, num4));
					}
				}
				streamReader.Close();
				streamReader.Dispose();
				this.SetTangents(ref this.targetX[j], ref this.targetZ[j]);
			}
			this.chunk = default(Cutty.shell);
			this.chunk.max = 0;
			this.chunk.type = 0;
			this.chunk.maxCapacity = 300;
			this.chunk.index = 0;
			this.chunk.offset = new Matrix[this.chunk.maxCapacity];
			this.chunk.bone = new int[this.chunk.maxCapacity];
			this.chunk.model = model;
			this.chunk.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Cutty.instanceDec, this.chunk.maxCapacity, BufferUsage.WriteOnly);
			this.chunk.displayList = new Cutty.instancedObject[this.chunk.maxCapacity];
			this.chunk.dupe = new chunkDupe[this.chunk.maxCapacity];
			for (int l = 0; l < this.chunk.maxCapacity; l++)
			{
				this.chunk.dupe[l] = new chunkDupe(l);
			}
			this.chunk.stream = new Cutty.instancedObject[this.chunk.maxCapacity];
			this.faceChunk = default(Cutty.shell);
			this.faceChunk.max = 0;
			this.faceChunk.type = 0;
			this.faceChunk.maxCapacity = 300;
			this.faceChunk.index = 0;
			this.faceChunk.offset = new Matrix[this.faceChunk.maxCapacity];
			this.faceChunk.bone = new int[this.faceChunk.maxCapacity];
			this.faceChunk.model = model;
			this.faceChunk.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Cutty.instanceDec, this.faceChunk.maxCapacity, BufferUsage.WriteOnly);
			this.faceChunk.displayList = new Cutty.instancedObject[this.faceChunk.maxCapacity];
			this.faceChunk.dupe = new chunkDupe[this.faceChunk.maxCapacity];
			for (int m = 0; m < this.faceChunk.maxCapacity; m++)
			{
				this.faceChunk.dupe[m] = new chunkDupe(m);
			}
			this.faceChunk.stream = new Cutty.instancedObject[this.faceChunk.maxCapacity];
			this.assChunk = default(Cutty.shell);
			this.assChunk.max = 0;
			this.assChunk.type = 0;
			this.assChunk.maxCapacity = 300;
			this.assChunk.index = 0;
			this.assChunk.offset = new Matrix[this.assChunk.maxCapacity];
			this.assChunk.bone = new int[this.assChunk.maxCapacity];
			this.assChunk.model = model;
			this.assChunk.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Cutty.instanceDec, this.assChunk.maxCapacity, BufferUsage.WriteOnly);
			this.assChunk.displayList = new Cutty.instancedObject[this.assChunk.maxCapacity];
			this.assChunk.dupe = new chunkDupe[this.assChunk.maxCapacity];
			for (int n = 0; n < this.assChunk.maxCapacity; n++)
			{
				this.assChunk.dupe[n] = new chunkDupe(n);
			}
			this.assChunk.stream = new Cutty.instancedObject[this.assChunk.maxCapacity];
			Matrix[] array2 = new Matrix[]
			{
				Matrix.CreateScale(0.708298f, 0.5569278f, 0.5991605f) * Matrix.CreateRotationX(0.21000183f) * Matrix.CreateRotationY(0.35086495f) * Matrix.CreateRotationZ(-1.0666703f) * Matrix.CreateTranslation(-0.05301531f, 177.23495f, 90.23178f),
				Matrix.CreateScale(0.5166327f, 0.50418854f, 0.5311612f) * Matrix.CreateRotationX(0.007827965f) * Matrix.CreateRotationY(-0.39642987f) * Matrix.CreateRotationZ(1.3952734f) * Matrix.CreateTranslation(-3.1802356f, 186.39288f, 87.43123f),
				Matrix.CreateScale(0.7999118f, 0.85444087f, 0.89364165f) * Matrix.CreateRotationX(0.13574441f) * Matrix.CreateRotationY(0.15914473f) * Matrix.CreateRotationZ(-1.2454927f) * Matrix.CreateTranslation(-3.2269325f, 188.27335f, 79.07738f),
				Matrix.CreateScale(0.5828592f, 0.5635248f, 0.78294665f) * Matrix.CreateRotationX(0.008781614f) * Matrix.CreateRotationY(-0.266933f) * Matrix.CreateRotationZ(1.4075471f) * Matrix.CreateTranslation(-3.1469f, 192.51654f, 71.58341f),
				Matrix.CreateScale(0.6400639f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(0.039364208f) * Matrix.CreateRotationY(-0.040494837f) * Matrix.CreateRotationZ(0.43435404f) * Matrix.CreateTranslation(-10.199868f, 183.82703f, 70.40799f),
				Matrix.CreateScale(0.39832914f, 0.22749387f, 0.4011678f) * Matrix.CreateRotationX(0.039364208f) * Matrix.CreateRotationY(-0.040494837f) * Matrix.CreateRotationZ(0.43435404f) * Matrix.CreateTranslation(-25.459875f, 177.1684f, 67.78193f),
				Matrix.CreateScale(0.7384201f, 0.50194556f, 0.950838f) * Matrix.CreateRotationX(-0.24494185f) * Matrix.CreateRotationY(0.3924641f) * Matrix.CreateRotationZ(0.29889435f) * Matrix.CreateTranslation(-39.005756f, 164.4879f, 57.19427f),
				Matrix.CreateScale(0.6400639f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(-0.20839427f) * Matrix.CreateRotationY(-0.040494837f) * Matrix.CreateRotationZ(0.43435404f) * Matrix.CreateTranslation(-19.704409f, 177.29863f, 58.745743f),
				Matrix.CreateScale(0.5828592f, 0.5635248f, 0.78294665f) * Matrix.CreateRotationX(-0.121878006f) * Matrix.CreateRotationY(0.110657275f) * Matrix.CreateRotationZ(1.4026057f) * Matrix.CreateTranslation(-3.3895175f, 192.44653f, 50.809284f),
				Matrix.CreateScale(0.7999118f, 0.85444087f, 0.89364165f) * Matrix.CreateRotationX(0.09980399f) * Matrix.CreateRotationY(-0.033211946f) * Matrix.CreateRotationZ(-1.0830402f) * Matrix.CreateTranslation(-0.404371f, 190.25781f, 59.084976f),
				Matrix.CreateScale(1.0154847f, 0.8137221f, 1.3076043f) * Matrix.CreateRotationX(0.03200087f) * Matrix.CreateRotationY(0.045253176f) * Matrix.CreateRotationZ(-0.04561152f) * Matrix.CreateTranslation(8.424231f, 174.60968f, 61.667908f),
				Matrix.CreateScale(1.0154847f, 0.60699457f, 0.67436534f) * Matrix.CreateRotationX(0.0071849837f) * Matrix.CreateRotationY(0.3197445f) * Matrix.CreateRotationZ(-0.4001006f) * Matrix.CreateTranslation(24.994572f, 172.32332f, 65.43521f),
				Matrix.CreateScale(1.0154847f, 0.9243026f, 1.3076043f) * Matrix.CreateRotationX(-0.36164764f) * Matrix.CreateRotationY(-0.08340433f) * Matrix.CreateRotationZ(-0.47222218f) * Matrix.CreateTranslation(26.236425f, 162.80861f, 41.711525f),
				Matrix.CreateScale(0.9093862f, 0.8277309f, 1.170985f) * Matrix.CreateRotationX(-0.044190314f) * Matrix.CreateRotationY(-0.32683676f) * Matrix.CreateRotationZ(-1.2942373f) * Matrix.CreateTranslation(-0.96077144f, 186.28342f, 41.940975f),
				Matrix.CreateScale(1.0154847f, 0.9243026f, 1.3076043f) * Matrix.CreateRotationX(-0.30139926f) * Matrix.CreateRotationY(-0.0028894034f) * Matrix.CreateRotationZ(-0.10339836f) * Matrix.CreateTranslation(5.9047933f, 167.077f, 27.918955f),
				Matrix.CreateScale(0.6539163f, 0.6488131f, 0.78294665f) * Matrix.CreateRotationX(-0.07386185f) * Matrix.CreateRotationY(0.3200513f) * Matrix.CreateRotationZ(1.4239929f) * Matrix.CreateTranslation(-0.22045895f, 188.09914f, 29.350624f),
				Matrix.CreateScale(0.5240074f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(-1.1680813f) * Matrix.CreateRotationY(-1.2621994f) * Matrix.CreateRotationZ(1.5120116f) * Matrix.CreateTranslation(-10.137637f, 179.63403f, 50.76101f),
				Matrix.CreateScale(0.6400639f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(-0.32027432f) * Matrix.CreateRotationY(-0.040494837f) * Matrix.CreateRotationZ(0.43435404f) * Matrix.CreateTranslation(-21.200918f, 173.67262f, 49.196167f),
				Matrix.CreateScale(0.6400639f, 0.2944064f, 0.28351352f) * Matrix.CreateRotationX(-0.32027432f) * Matrix.CreateRotationY(-0.040494837f) * Matrix.CreateRotationZ(0.43435404f) * Matrix.CreateTranslation(-13.32279f, 176.57603f, 42.04959f),
				Matrix.CreateScale(0.6400639f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(-1.5545334f) * Matrix.CreateRotationY(-1.2507498f) * Matrix.CreateRotationZ(1.9745901f) * Matrix.CreateTranslation(-27.298658f, 168.95055f, 38.27991f),
				Matrix.CreateScale(0.5240074f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(-1.2617631f) * Matrix.CreateRotationY(-1.1686635f) * Matrix.CreateRotationZ(1.6118176f) * Matrix.CreateTranslation(-14.713638f, 172.31055f, 31.776146f),
				Matrix.CreateScale(0.9093862f, 0.8277309f, 1.170985f) * Matrix.CreateRotationX(-0.045005485f) * Matrix.CreateRotationY(-0.37660113f) * Matrix.CreateRotationZ(-1.2918718f) * Matrix.CreateTranslation(-0.054333784f, 179.60506f, 19.556162f),
				Matrix.CreateScale(0.50662977f, 0.2944064f, 0.28351352f) * Matrix.CreateRotationX(-0.31550753f) * Matrix.CreateRotationY(-0.06909058f) * Matrix.CreateRotationZ(0.3473774f) * Matrix.CreateTranslation(-9.074999f, 169.20471f, 20.59569f),
				Matrix.CreateScale(0.453718f, 0.2944064f, 0.6959109f) * Matrix.CreateRotationX(-0.3212951f) * Matrix.CreateRotationY(0.088174075f) * Matrix.CreateRotationZ(0.39162627f) * Matrix.CreateTranslation(-23.632992f, 164.3047f, 20.9647f),
				Matrix.CreateScale(0.82637864f, 0.56173587f, 1.0640991f) * Matrix.CreateRotationX(-0.23651274f) * Matrix.CreateRotationY(0.1308642f) * Matrix.CreateRotationZ(0.55283076f) * Matrix.CreateTranslation(-39.889256f, 155.20958f, 31.045595f),
				Matrix.CreateScale(1.0154847f, 0.9243026f, 1.3076043f) * Matrix.CreateRotationX(-0.37383264f) * Matrix.CreateRotationY(0.11123756f) * Matrix.CreateRotationZ(-0.63449585f) * Matrix.CreateTranslation(23.578161f, 150.72693f, 16.186039f),
				Matrix.CreateScale(0.6400639f, 0.2944064f, 0.4616332f) * Matrix.CreateRotationX(-1.911975f) * Matrix.CreateRotationY(-1.0574344f) * Matrix.CreateRotationZ(2.4914885f) * Matrix.CreateTranslation(-39.814713f, 150.03421f, 8.667492f),
				Matrix.CreateScale(0.5333285f, 0.36253324f, 0.79965556f) * Matrix.CreateRotationX(-0.37774083f) * Matrix.CreateRotationY(0.02756262f) * Matrix.CreateRotationZ(0.3302053f) * Matrix.CreateTranslation(-28.23632f, 155.48392f, 7.453938f),
				Matrix.CreateScale(0.453718f, 0.2944064f, 0.6959109f) * Matrix.CreateRotationX(-0.3212951f) * Matrix.CreateRotationY(0.088174075f) * Matrix.CreateRotationZ(0.39162627f) * Matrix.CreateTranslation(-13.275413f, 161.79802f, 9.130991f),
				Matrix.CreateScale(0.6539163f, 0.6488131f, 0.78294665f) * Matrix.CreateRotationX(-0.07995875f) * Matrix.CreateRotationY(0.5012596f) * Matrix.CreateRotationZ(1.4087833f) * Matrix.CreateTranslation(-0.0020924984f, 178.28697f, 8.222643f),
				Matrix.CreateScale(0.71686894f, 0.8277309f, 0.9487003f) * Matrix.CreateRotationX(-0.166182f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(-0.7827156f, 173.97246f, 1.3644682f),
				Matrix.CreateScale(0.8365833f, 0.5147443f, 1.0772393f) * Matrix.CreateRotationX(-0.31722963f) * Matrix.CreateRotationY(0.0121787675f) * Matrix.CreateRotationZ(-0.016896978f) * Matrix.CreateTranslation(1.6741506f, 155.5527f, -4.3608685f),
				Matrix.CreateScale(0.8497695f, 0.77346724f, 1.0942186f) * Matrix.CreateRotationX(-0.47398552f) * Matrix.CreateRotationY(0.30852166f) * Matrix.CreateRotationZ(-0.6299423f) * Matrix.CreateTranslation(12.518083f, 144.5464f, -13.799041f),
				Matrix.CreateScale(0.6539163f, 0.6488131f, 0.78294665f) * Matrix.CreateRotationX(-0.07995875f) * Matrix.CreateRotationY(0.5012596f) * Matrix.CreateRotationZ(1.4087833f) * Matrix.CreateTranslation(-0.1792772f, 167.4633f, -9.034153f),
				Matrix.CreateScale(0.6539163f, 0.7550428f, 0.6800631f) * Matrix.CreateRotationX(-0.17043039f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(-0.46539086f, 163.92056f, -15.060584f),
				Matrix.CreateScale(0.42149806f, 0.28651586f, 0.54274845f) * Matrix.CreateRotationX(-0.46243003f) * Matrix.CreateRotationY(-0.34424174f) * Matrix.CreateRotationZ(0.68739104f) * Matrix.CreateTranslation(-31.138388f, 147.32751f, -5.527931f),
				Matrix.CreateScale(0.58599514f, 0.35243034f, 0.80873215f) * Matrix.CreateRotationX(-0.37774083f) * Matrix.CreateRotationY(0.02756262f) * Matrix.CreateRotationZ(0.3302053f) * Matrix.CreateTranslation(-16.01753f, 154.60179f, -6.506987f),
				Matrix.CreateScale(0.4695873f, 0.3192048f, 0.6046713f) * Matrix.CreateRotationX(-0.3445082f) * Matrix.CreateRotationY(-0.3912871f) * Matrix.CreateRotationZ(0.5804604f) * Matrix.CreateTranslation(-21.772762f, 146.24084f, -17.66944f),
				Matrix.CreateScale(0.6539163f, 0.71167594f, 0.8653891f) * Matrix.CreateRotationX(0.063659675f) * Matrix.CreateRotationY(0.5069416f) * Matrix.CreateRotationZ(1.5504088f) * Matrix.CreateTranslation(1.6671053f, 155.39842f, -25.441198f),
				Matrix.CreateScale(0.4695873f, 0.3192048f, 0.6046713f) * Matrix.CreateRotationX(-0.51408106f) * Matrix.CreateRotationY(-0.7301555f) * Matrix.CreateRotationZ(0.8454387f) * Matrix.CreateTranslation(-11.250593f, 145.33586f, -29.37115f),
				Matrix.CreateScale(0.4695873f, 0.3192048f, 0.6046713f) * Matrix.CreateRotationX(-0.38116747f) * Matrix.CreateRotationY(0.2682071f) * Matrix.CreateRotationZ(0.23683934f) * Matrix.CreateTranslation(-6.3735056f, 151.78041f, -20.773891f),
				Matrix.CreateScale(0.6539163f, 0.7550428f, 0.8653891f) * Matrix.CreateRotationX(-0.17043039f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(-0.33552048f, 155.774f, -25.083172f),
				Matrix.CreateScale(0.45236987f, 0.47996533f, 0.42950025f) * Matrix.CreateRotationX(-0.17043039f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(0.27977854f, 149.41898f, -42.103573f),
				Matrix.CreateScale(0.42703608f, 0.63575184f, 0.5851349f) * Matrix.CreateRotationX(0.06155733f) * Matrix.CreateRotationY(0.4416417f) * Matrix.CreateRotationZ(1.5458081f) * Matrix.CreateTranslation(1.2731643f, 149.7724f, -40.886696f),
				Matrix.CreateScale(0.42703608f, 0.63575184f, 0.5851349f) * Matrix.CreateRotationX(-0.067014605f) * Matrix.CreateRotationY(0.4416417f) * Matrix.CreateRotationZ(1.5458081f) * Matrix.CreateTranslation(0.7915f, 143.36597f, -54.458267f),
				Matrix.CreateScale(0.45236987f, 0.3768922f, 0.42950025f) * Matrix.CreateRotationX(-0.17043039f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(-0.15296115f, 143.77997f, -52.36023f),
				Matrix.CreateScale(0.45236987f, 0.3768922f, 0.42950025f) * Matrix.CreateRotationX(-0.17043039f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(-0.52406496f, 138.77298f, -61.38392f),
				Matrix.CreateScale(0.31564054f, 0.53192335f, 0.4712606f) * Matrix.CreateRotationX(-0.05475918f) * Matrix.CreateRotationY(0.4416417f) * Matrix.CreateRotationZ(1.5458081f) * Matrix.CreateTranslation(0.6470251f, 136.9542f, -68.02329f),
				Matrix.CreateScale(0.35304898f, 0.3768922f, 0.3485293f) * Matrix.CreateRotationX(-0.17043039f) * Matrix.CreateRotationY(-0.45548734f) * Matrix.CreateRotationZ(-1.2885604f) * Matrix.CreateTranslation(-1.2745597f, 135.53868f, -70.456604f)
			};
			for (int num8 = 0; num8 < 49; num8++)
			{
				this.dropChunk(ref this.chunk, array2[num8], 5);
			}
			Matrix[] array3 = new Matrix[]
			{
				Matrix.CreateScale(0.5413472f, 0.42853388f, 0.443344f) * Matrix.CreateRotationX(6.538176f) * Matrix.CreateRotationY(0.22786514f) * Matrix.CreateRotationZ(-1.5430124f) * Matrix.CreateTranslation(30.514662f, 66.49369f, 184.6844f),
				Matrix.CreateScale(0.5413472f, 0.42853388f, 0.443344f) * Matrix.CreateRotationX(2.903015f) * Matrix.CreateRotationY(0.11509425f) * Matrix.CreateRotationZ(-1.6335382f) * Matrix.CreateTranslation(-36.289993f, 65.29824f, 178.94395f),
				Matrix.CreateScale(0.47165838f, 0.37336776f, 0.46046123f) * Matrix.CreateRotationX(0.2564029f) * Matrix.CreateRotationY(-0.5665432f) * Matrix.CreateRotationZ(1.1469673f) * Matrix.CreateTranslation(-34.414818f, 76.255905f, 187.20638f),
				Matrix.CreateScale(1f, 0.5922924f, 1f) * Matrix.CreateRotationX(0.52396405f) * Matrix.CreateRotationY(-0.009579088f) * Matrix.CreateRotationZ(0.0004069961f) * Matrix.CreateTranslation(-4.378996f, 87.816475f, 205.46991f),
				Matrix.CreateScale(0.63741755f, 0.5045838f, 0.6222853f) * Matrix.CreateRotationX(0.5618843f) * Matrix.CreateRotationY(0.4032182f) * Matrix.CreateRotationZ(-0.74408966f) * Matrix.CreateTranslation(15.523255f, 86.96487f, 193.86084f),
				Matrix.CreateScale(0.6704292f, 0.42853388f, 0.443344f) * Matrix.CreateRotationX(7.0325704f) * Matrix.CreateRotationY(-1.1366687f) * Matrix.CreateRotationZ(-2.0834444f) * Matrix.CreateTranslation(26.555515f, 78.005585f, 189.31316f),
				Matrix.CreateScale(0.74821585f, 0.5922924f, 0.73045325f) * Matrix.CreateRotationX(2.5845833f) * Matrix.CreateRotationY(0.2716956f) * Matrix.CreateRotationZ(-2.2202864f) * Matrix.CreateTranslation(-22.519411f, 83.236885f, 197.55461f),
				Matrix.CreateScale(0.6223866f, 0.49268517f, 0.6076112f) * Matrix.CreateRotationX(3.0446942f) * Matrix.CreateRotationY(0.22268786f) * Matrix.CreateRotationZ(-1.6934097f) * Matrix.CreateTranslation(-31.726833f, 65.11813f, 191.83823f),
				Matrix.CreateScale(0.74821585f, 0.5922924f, 0.73045325f) * Matrix.CreateRotationX(2.821042f) * Matrix.CreateRotationY(0.34955114f) * Matrix.CreateRotationZ(-2.074376f) * Matrix.CreateTranslation(-19.760626f, 76.6856f, 208.95462f),
				Matrix.CreateScale(0.5413472f, 0.42853388f, 0.443344f) * Matrix.CreateRotationX(2.903015f) * Matrix.CreateRotationY(0.11509425f) * Matrix.CreateRotationZ(-1.6335382f) * Matrix.CreateTranslation(-28.842787f, 60.843918f, 204.71378f),
				Matrix.CreateScale(0.49000353f, 0.38788992f, 0.40129536f) * Matrix.CreateRotationX(6.6105833f) * Matrix.CreateRotationY(0.104882136f) * Matrix.CreateRotationZ(-1.4559963f) * Matrix.CreateTranslation(24.045212f, 63.590496f, 203.82614f),
				Matrix.CreateScale(0.74821585f, 0.5922924f, 0.73045325f) * Matrix.CreateRotationX(2.8573503f) * Matrix.CreateRotationY(-0.1884174f) * Matrix.CreateRotationZ(-4.1276503f) * Matrix.CreateTranslation(13.591358f, 78.109344f, 208.54057f),
				Matrix.CreateScale(0.74821585f, 0.5922924f, 0.73045325f) * Matrix.CreateRotationX(2.9056542f) * Matrix.CreateRotationY(-0.2746308f) * Matrix.CreateRotationZ(-4.101493f) * Matrix.CreateTranslation(12.086349f, 70.64876f, 226.45514f),
				Matrix.CreateScale(0.5413472f, 0.42853388f, 0.443344f) * Matrix.CreateRotationX(6.6173825f) * Matrix.CreateRotationY(-0.010621262f) * Matrix.CreateRotationZ(-1.3996271f) * Matrix.CreateTranslation(19.864847f, 61.85692f, 215.46294f),
				Matrix.CreateScale(0.74821585f, 0.5922924f, 0.73045325f) * Matrix.CreateRotationX(2.7833774f) * Matrix.CreateRotationY(0.037689615f) * Matrix.CreateRotationZ(-3.1556976f) * Matrix.CreateTranslation(-4.3825116f, 77.8521f, 229.15524f),
				Matrix.CreateScale(0.74821585f, 0.5922924f, 0.73045325f) * Matrix.CreateRotationX(3.070413f) * Matrix.CreateRotationY(0.35330334f) * Matrix.CreateRotationZ(-1.8884395f) * Matrix.CreateTranslation(-18.235523f, 69.75004f, 225.52905f),
				Matrix.CreateScale(0.39633247f, 0.42853388f, 0.3915252f) * Matrix.CreateRotationX(3.0076532f) * Matrix.CreateRotationY(0.05079993f) * Matrix.CreateRotationZ(-1.6609201f) * Matrix.CreateTranslation(-28.567139f, 59.18479f, 215.66902f),
				Matrix.CreateScale(0.24571283f, 0.3378071f, 0.2780271f) * Matrix.CreateRotationX(2.903015f) * Matrix.CreateRotationY(0.11509425f) * Matrix.CreateRotationZ(-1.6335382f) * Matrix.CreateTranslation(-26.71854f, 56.022095f, 223.8292f),
				Matrix.CreateScale(0.24571283f, 0.3378071f, 0.6332214f) * Matrix.CreateRotationX(2.912676f) * Matrix.CreateRotationY(-0.019249836f) * Matrix.CreateRotationZ(-1.5989511f) * Matrix.CreateTranslation(-23.743523f, 54.481155f, 236.29242f),
				Matrix.CreateScale(0.5944707f, 0.47058678f, 0.805785f) * Matrix.CreateRotationX(0.48466545f) * Matrix.CreateRotationY(1.4270855f) * Matrix.CreateRotationZ(-4.524961f) * Matrix.CreateTranslation(-18.944372f, 67.76714f, 242.68057f),
				Matrix.CreateScale(0.24571283f, 0.3378071f, 0.2780271f) * Matrix.CreateRotationX(2.910827f) * Matrix.CreateRotationY(0.0031944546f) * Matrix.CreateRotationZ(-1.6168964f) * Matrix.CreateTranslation(-21.125467f, 53.939556f, 247.27448f),
				Matrix.CreateScale(0.31518826f, 0.11374161f, 0.25396627f) * Matrix.CreateRotationX(-0.18741952f) * Matrix.CreateRotationY(0.88269943f) * Matrix.CreateRotationZ(-6.0198503f) * Matrix.CreateTranslation(-17.980186f, 80.97128f, 247.91756f),
				Matrix.CreateScale(0.27000344f, 0.11374161f, 0.11784676f) * Matrix.CreateRotationX(-0.013534301f) * Matrix.CreateRotationY(-0.04546181f) * Matrix.CreateRotationZ(-6.114901f) * Matrix.CreateTranslation(-9.500427f, 81.634125f, 242.11665f),
				Matrix.CreateScale(0.27000344f, 0.11374161f, 0.11784676f) * Matrix.CreateRotationX(0.0017460219f) * Matrix.CreateRotationY(-0.04740022f) * Matrix.CreateRotationZ(-6.441197f) * Matrix.CreateTranslation(-0.5809272f, 81.74283f, 241.96536f),
				Matrix.CreateScale(0.33587113f, 0.3203402f, 0.41663173f) * Matrix.CreateRotationX(4.2282066f) * Matrix.CreateRotationY(1.1329254f) * Matrix.CreateRotationZ(-2.6906886f) * Matrix.CreateTranslation(9.903984f, 78.27347f, 245.56984f),
				Matrix.CreateScale(0.59982836f, 0.5282872f, 0.6118648f) * Matrix.CreateRotationX(-0.47799104f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(-2.2552125f, 79.89647f, 252.56284f),
				Matrix.CreateScale(0.20473702f, 0.11374161f, 0.25396627f) * Matrix.CreateRotationX(0.16329938f) * Matrix.CreateRotationY(0.9414379f) * Matrix.CreateRotationZ(-5.485871f) * Matrix.CreateTranslation(-14.074507f, 81.472885f, 252.08722f),
				Matrix.CreateScale(0.20473702f, 0.11374161f, 0.25396627f) * Matrix.CreateRotationX(2.21588f) * Matrix.CreateRotationY(0.9094076f) * Matrix.CreateRotationZ(-5.2658863f) * Matrix.CreateTranslation(13.221695f, 76.05087f, 250.12073f),
				Matrix.CreateScale(0.51713544f, 0.49322268f, 0.6414812f) * Matrix.CreateRotationX(2.6200602f) * Matrix.CreateRotationY(1.2401886f) * Matrix.CreateRotationZ(-4.904619f) * Matrix.CreateTranslation(13.968251f, 65.73363f, 242.17244f),
				Matrix.CreateScale(0.39501378f, 0.31269544f, 0.32350218f) * Matrix.CreateRotationX(6.4541397f) * Matrix.CreateRotationY(-0.0062056226f) * Matrix.CreateRotationZ(-1.5967679f) * Matrix.CreateTranslation(18.48424f, 57.37408f, 236.86407f),
				Matrix.CreateScale(0.272924f, 0.21604839f, 0.22351502f) * Matrix.CreateRotationX(6.4541397f) * Matrix.CreateRotationY(-0.0062056226f) * Matrix.CreateRotationZ(-1.5967679f) * Matrix.CreateTranslation(18.36457f, 56.867107f, 245.13284f),
				Matrix.CreateScale(0.24683672f, 0.13713008f, 0.3061889f) * Matrix.CreateRotationX(2.090404f) * Matrix.CreateRotationY(0.5680416f) * Matrix.CreateRotationZ(-5.365757f) * Matrix.CreateTranslation(15.802671f, 57.96174f, 250.37051f),
				Matrix.CreateScale(0.20473702f, 0.11374161f, 0.25396627f) * Matrix.CreateRotationX(1.5666511f) * Matrix.CreateRotationY(0.9414379f) * Matrix.CreateRotationZ(-5.485871f) * Matrix.CreateTranslation(9.297062f, 80.531166f, 251.52928f),
				Matrix.CreateScale(1f, 0.5922924f, 1f) * Matrix.CreateRotationX(1.3328656f) * Matrix.CreateRotationY(0.0015964331f) * Matrix.CreateRotationZ(-3.1350098f) * Matrix.CreateTranslation(-4.417528f, 75.36524f, 251.05641f),
				Matrix.CreateScale(0.6032753f, 0.3573154f, 0.6032753f) * Matrix.CreateRotationX(1.5368155f) * Matrix.CreateRotationY(-0.0033300421f) * Matrix.CreateRotationZ(-6.2694535f) * Matrix.CreateTranslation(3.2282434f, 61.03654f, 252.71452f),
				Matrix.CreateScale(0.6032753f, 0.3573154f, 0.6032753f) * Matrix.CreateRotationX(1.5623136f) * Matrix.CreateRotationY(-0.0033300421f) * Matrix.CreateRotationZ(-6.2694535f) * Matrix.CreateTranslation(-14.294163f, 59.011375f, 250.2981f),
				Matrix.CreateScale(0.39501378f, 0.31269544f, 0.32350218f) * Matrix.CreateRotationX(6.6173825f) * Matrix.CreateRotationY(-0.010621262f) * Matrix.CreateRotationZ(-1.3996271f) * Matrix.CreateTranslation(19.8525f, 58.305656f, 226.53386f),
				Matrix.CreateScale(0.5413472f, 0.42853388f, 0.443344f) * Matrix.CreateRotationX(6.538176f) * Matrix.CreateRotationY(0.22786514f) * Matrix.CreateRotationZ(-1.5430124f) * Matrix.CreateTranslation(24.438065f, 65.51811f, 194.37152f)
			};
			Matrix[] array4 = new Matrix[]
			{
				Matrix.CreateScale(0.41209254f, 0.37184042f, 0.39321324f) * Matrix.CreateRotationX(-0.40591684f) * Matrix.CreateRotationY(0.6359218f) * Matrix.CreateRotationZ(-2.2937894f) * Matrix.CreateTranslation(25.651388f, 47.262676f, 174.60236f),
				Matrix.CreateScale(0.59486216f, 0.46973562f, 0.53222954f) * Matrix.CreateRotationX(-1.4731348f) * Matrix.CreateRotationY(0.4942707f) * Matrix.CreateRotationZ(3.1379888f) * Matrix.CreateTranslation(12.402101f, 39.680252f, 172.0124f),
				Matrix.CreateScale(0.59486216f, 0.46973562f, 0.53222954f) * Matrix.CreateRotationX(-1.4845778f) * Matrix.CreateRotationY(0.079992615f) * Matrix.CreateRotationZ(3.0984514f) * Matrix.CreateTranslation(-4.063797f, 40.202026f, 168.49992f),
				Matrix.CreateScale(0.59486216f, 0.46973562f, 0.53222954f) * Matrix.CreateRotationX(-1.4802774f) * Matrix.CreateRotationY(-0.31894705f) * Matrix.CreateRotationZ(3.063091f) * Matrix.CreateTranslation(-20.474829f, 41.05769f, 168.88998f),
				Matrix.CreateScale(0.59486216f, 0.46973562f, 0.53222954f) * Matrix.CreateRotationX(-1.4379157f) * Matrix.CreateRotationY(-0.86601084f) * Matrix.CreateRotationZ(2.9900727f) * Matrix.CreateTranslation(-34.225147f, 42.309566f, 175.41817f),
				Matrix.CreateScale(0.57867235f, 0.42933777f, 0.38095102f) * Matrix.CreateRotationX(-2.5379071f) * Matrix.CreateRotationY(-0.6213276f) * Matrix.CreateRotationZ(-1.5362663f) * Matrix.CreateTranslation(-32.8095f, 36.1086f, 174.39253f),
				Matrix.CreateScale(0.45985523f, 0.43392494f, 0.38299063f) * Matrix.CreateRotationX(0.097626336f) * Matrix.CreateRotationY(-0.95469826f) * Matrix.CreateRotationZ(1.6976305f) * Matrix.CreateTranslation(-36.739456f, 43.585545f, 179.97751f),
				Matrix.CreateScale(0.4600257f, 0.43416417f, 0.3826378f) * Matrix.CreateRotationX(3.22182f) * Matrix.CreateRotationY(-1.0716554f) * Matrix.CreateRotationZ(1.5003428f) * Matrix.CreateTranslation(27.945503f, 47.37914f, 179.94324f),
				Matrix.CreateScale(0.5302833f, 0.5015824f, 0.5205079f) * Matrix.CreateRotationX(-2.976646f) * Matrix.CreateRotationY(-1.1710324f) * Matrix.CreateRotationZ(1.2978466f) * Matrix.CreateTranslation(27.207254f, 36.915855f, 184.3698f),
				Matrix.CreateScale(0.41209254f, 0.37184042f, 0.39321324f) * Matrix.CreateRotationX(-0.3627832f) * Matrix.CreateRotationY(0.4619855f) * Matrix.CreateRotationZ(-2.2114716f) * Matrix.CreateTranslation(26.584015f, 39.54975f, 176.53145f),
				Matrix.CreateScale(0.6822789f, 0.5580407f, 0.6317565f) * Matrix.CreateRotationX(-2.5137024f) * Matrix.CreateRotationY(-0.4502413f) * Matrix.CreateRotationZ(0.27259254f) * Matrix.CreateTranslation(12.054021f, 32.703133f, 175.46548f),
				Matrix.CreateScale(1.02198f, 0.5039385f, 0.8545515f) * Matrix.CreateRotationX(-0.9450256f) * Matrix.CreateRotationY(0.035311695f) * Matrix.CreateRotationZ(-3.1174731f) * Matrix.CreateTranslation(-2.2312105f, 27.807878f, 177.89836f),
				Matrix.CreateScale(0.59486216f, 0.46973562f, 0.53222954f) * Matrix.CreateRotationX(-0.5869491f) * Matrix.CreateRotationY(-0.2135827f) * Matrix.CreateRotationZ(2.6965902f) * Matrix.CreateTranslation(-20.294147f, 33.225086f, 171.3266f),
				Matrix.CreateScale(0.45985523f, 0.43392494f, 0.38299063f) * Matrix.CreateRotationX(0.097626336f) * Matrix.CreateRotationY(-0.95469826f) * Matrix.CreateRotationZ(1.6976305f) * Matrix.CreateTranslation(-33.855854f, 35.05395f, 184.65251f),
				Matrix.CreateScale(0.4172351f, 0.38967186f, 0.34858924f) * Matrix.CreateRotationX(-0.057762213f) * Matrix.CreateRotationY(-1.0527545f) * Matrix.CreateRotationZ(1.937457f) * Matrix.CreateTranslation(-33.29405f, 28.039743f, 189.09615f),
				Matrix.CreateScale(0.6760227f, 0.56769145f, 0.6267637f) * Matrix.CreateRotationX(-2.3205712f) * Matrix.CreateRotationY(0.49035498f) * Matrix.CreateRotationZ(-0.35396165f) * Matrix.CreateTranslation(-23.472673f, 24.551958f, 183.62451f),
				Matrix.CreateScale(0.6645037f, 0.57837385f, 0.6258517f) * Matrix.CreateRotationX(-2.151846f) * Matrix.CreateRotationY(-0.39615455f) * Matrix.CreateRotationZ(0.25657463f) * Matrix.CreateTranslation(16.312057f, 22.969252f, 185.87996f),
				Matrix.CreateScale(0.4600257f, 0.43416417f, 0.3826378f) * Matrix.CreateRotationX(3.0145113f) * Matrix.CreateRotationY(-1.1699647f) * Matrix.CreateRotationZ(1.4829642f) * Matrix.CreateTranslation(26.360117f, 26.65583f, 189.71805f),
				Matrix.CreateScale(0.3370805f, 0.43609604f, 0.3361327f) * Matrix.CreateRotationX(-3.1080396f) * Matrix.CreateRotationY(-1.1326616f) * Matrix.CreateRotationZ(1.4128113f) * Matrix.CreateTranslation(25.310486f, 19.12759f, 194.45319f),
				Matrix.CreateScale(0.6479885f, 0.5938663f, 0.6250597f) * Matrix.CreateRotationX(-2.4907336f) * Matrix.CreateRotationY(-0.45268938f) * Matrix.CreateRotationZ(0.50575256f) * Matrix.CreateTranslation(15.564279f, 11.352174f, 192.28279f),
				Matrix.CreateScale(0.81244624f, 0.5033684f, 0.7366666f) * Matrix.CreateRotationX(-2.0988781f) * Matrix.CreateRotationY(-0.015331961f) * Matrix.CreateRotationZ(0.041481502f) * Matrix.CreateTranslation(-2.3648531f, 8.460554f, 189.97736f),
				Matrix.CreateScale(0.6776758f, 0.5678505f, 0.6250597f) * Matrix.CreateRotationX(-2.4131935f) * Matrix.CreateRotationY(0.3678197f) * Matrix.CreateRotationZ(-0.36999974f) * Matrix.CreateTranslation(-19.436186f, 9.257215f, 191.96564f),
				Matrix.CreateScale(0.4625572f, 0.4282468f, 0.38580188f) * Matrix.CreateRotationX(-0.27252948f) * Matrix.CreateRotationY(-1.1287537f) * Matrix.CreateRotationZ(2.1779637f) * Matrix.CreateTranslation(-29.20887f, 19.296638f, 194.21648f),
				Matrix.CreateScale(0.3375218f, 0.31248593f, 0.28151447f) * Matrix.CreateRotationX(-0.27252948f) * Matrix.CreateRotationY(-1.1287537f) * Matrix.CreateRotationZ(2.1779637f) * Matrix.CreateTranslation(-28.370136f, 11.101428f, 198.82176f),
				Matrix.CreateScale(0.20863342f, 0.34250817f, 0.5465368f) * Matrix.CreateRotationX(2.930468f) * Matrix.CreateRotationY(-1.2081566f) * Matrix.CreateRotationZ(1.5919533f) * Matrix.CreateTranslation(18.28996f, 3.7440884f, 202.22696f),
				Matrix.CreateScale(0.5091167f, 0.47436625f, 0.48807898f) * Matrix.CreateRotationX(-2.8490286f) * Matrix.CreateRotationY(0.6030164f) * Matrix.CreateRotationZ(1.383574f) * Matrix.CreateTranslation(12.835738f, -1.3739192f, 201.9446f),
				Matrix.CreateScale(0.77957875f, 0.5070165f, 0.8054802f) * Matrix.CreateRotationX(3.9071648f) * Matrix.CreateRotationY(-0.04256754f) * Matrix.CreateRotationZ(0.019824155f) * Matrix.CreateTranslation(-2.6324022f, -7.928337f, 204.62631f),
				Matrix.CreateScale(0.27479064f, 0.09693247f, 0.100961804f) * Matrix.CreateRotationX(-1.1913348f) * Matrix.CreateRotationY(-0.09895785f) * Matrix.CreateRotationZ(-3.137025f) * Matrix.CreateTranslation(-5.6169696f, -1.3717237f, 191.10356f),
				Matrix.CreateScale(0.44286397f, 0.49504584f, 0.554546f) * Matrix.CreateRotationX(-0.43975782f) * Matrix.CreateRotationY(0.7053175f) * Matrix.CreateRotationZ(1.652292f) * Matrix.CreateTranslation(-19.775192f, -1.881347f, 200.49481f),
				Matrix.CreateScale(0.3353942f, 0.3181485f, 0.27825797f) * Matrix.CreateRotationX(0.2943784f) * Matrix.CreateRotationY(-1.1949463f) * Matrix.CreateRotationZ(1.5984483f) * Matrix.CreateTranslation(-26.695461f, 4.2505684f, 202.01718f),
				Matrix.CreateScale(0.27465674f, 0.09697973f, 0.100961804f) * Matrix.CreateRotationX(-1.1885699f) * Matrix.CreateRotationY(0.15338476f) * Matrix.CreateRotationZ(-3.0363052f) * Matrix.CreateTranslation(1.0029396f, -1.5677773f, 199.13138f),
				Matrix.CreateScale(0.23173147f, 0.21981603f, 0.19225477f) * Matrix.CreateRotationX(0.11927667f) * Matrix.CreateRotationY(-1.1949463f) * Matrix.CreateRotationZ(1.5984483f) * Matrix.CreateTranslation(-25.412241f, -1.9873122f, 207.7467f),
				Matrix.CreateScale(0.2220721f, 0.12732933f, 0.2723526f) * Matrix.CreateRotationX(0.46056682f) * Matrix.CreateRotationY(0.9249648f) * Matrix.CreateRotationZ(2.7487767f) * Matrix.CreateTranslation(-23.045294f, -6.083092f, 208.46718f),
				Matrix.CreateScale(0.61652476f, 0.30601633f, 0.44513565f) * Matrix.CreateRotationX(-0.07961134f) * Matrix.CreateRotationY(0.48284814f) * Matrix.CreateRotationZ(-3.9187627f) * Matrix.CreateTranslation(-14.140168f, -10.528718f, 205.50098f),
				Matrix.CreateScale(0.20869513f, 0.34238395f, 0.23998275f) * Matrix.CreateRotationX(2.9829187f) * Matrix.CreateRotationY(-1.1853235f) * Matrix.CreateRotationZ(1.5334039f) * Matrix.CreateTranslation(14.655433f, -8.232341f, 209.46234f),
				Matrix.CreateScale(0.61652476f, 0.3060191f, 0.5121602f) * Matrix.CreateRotationX(-0.17695011f) * Matrix.CreateRotationY(0.13759814f) * Matrix.CreateRotationZ(-2.8261375f) * Matrix.CreateTranslation(7.971637f, -10.152804f, 206.48434f)
			};
			for (int num9 = 0; num9 < 36; num9++)
			{
				this.dropChunk(ref this.faceChunk, array3[num9], 7);
				this.dropChunk(ref this.faceChunk, array4[num9], 9);
			}
			this.dropChunk(ref this.faceChunk, array3[36], 7);
			this.dropChunk(ref this.faceChunk, array3[37], 7);
			Matrix[] array5 = new Matrix[]
			{
				Matrix.CreateScale(0.6249726f, 0.6249726f, 0.6249726f) * Matrix.CreateRotationX(-0.6496698f) * Matrix.CreateRotationY(0.10908994f) * Matrix.CreateRotationZ(-2.204136f) * Matrix.CreateTranslation(31.558002f, 43.63091f, -117.8855f),
				Matrix.CreateScale(0.6249726f, 0.6249726f, 0.4483838f) * Matrix.CreateRotationX(-0.96178645f) * Matrix.CreateRotationY(-0.54967594f) * Matrix.CreateRotationZ(-2.9896562f) * Matrix.CreateTranslation(19.322937f, 36.14469f, -113.48896f),
				Matrix.CreateScale(0.7477622f, 0.7477622f, 0.7477622f) * Matrix.CreateRotationX(-2.4504223f) * Matrix.CreateRotationY(-0.05025345f) * Matrix.CreateRotationZ(-0.009903382f) * Matrix.CreateTranslation(-1.2861779f, 42.109234f, -106.83503f),
				Matrix.CreateScale(0.59793127f, 0.44197336f, 0.59793127f) * Matrix.CreateRotationX(-0.85920656f) * Matrix.CreateRotationY(0.00016024953f) * Matrix.CreateRotationZ(3.0280492f) * Matrix.CreateTranslation(-27.801949f, 36.424904f, -117.55912f),
				Matrix.CreateScale(0.76298636f, 0.76298636f, 0.76298636f) * Matrix.CreateRotationX(-2.267468f) * Matrix.CreateRotationY(0.106266044f) * Matrix.CreateRotationZ(-0.20471469f) * Matrix.CreateTranslation(-23.839731f, 50.02435f, -123.05215f),
				Matrix.CreateScale(0.76298636f, 0.76298636f, 0.76298636f) * Matrix.CreateRotationX(-2.4504223f) * Matrix.CreateRotationY(-0.05025345f) * Matrix.CreateRotationZ(-0.009903382f) * Matrix.CreateTranslation(-0.7291206f, 49.992393f, -116.35792f),
				Matrix.CreateScale(0.76298636f, 0.76298636f, 0.76298636f) * Matrix.CreateRotationX(-2.2842891f) * Matrix.CreateRotationY(-0.06817425f) * Matrix.CreateRotationZ(-0.07318518f) * Matrix.CreateTranslation(18.115206f, 48.633926f, -119.08546f),
				Matrix.CreateScale(0.59793127f, 0.44197336f, 0.59793127f) * Matrix.CreateRotationX(-1.4547899f) * Matrix.CreateRotationY(-1.0708479f) * Matrix.CreateRotationZ(3.3181472f) * Matrix.CreateTranslation(-38.25945f, 49.251583f, -118.73936f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.6803323f) * Matrix.CreateRotationY(0.6807442f) * Matrix.CreateRotationZ(-0.045894645f) * Matrix.CreateTranslation(-37.77458f, 64.05266f, -119.89065f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.8097754f) * Matrix.CreateRotationY(0.18824755f) * Matrix.CreateRotationZ(0.005669557f) * Matrix.CreateTranslation(-22.234709f, 64.22835f, -128.55312f),
				Matrix.CreateScale(0.8729049f, 0.8729049f, 0.8729049f) * Matrix.CreateRotationX(-1.8536123f) * Matrix.CreateRotationY(-0.05025345f) * Matrix.CreateRotationZ(-0.009903382f) * Matrix.CreateTranslation(-0.19121866f, 63.247982f, -124.44214f),
				Matrix.CreateScale(0.6249726f, 0.6249726f, 0.6249726f) * Matrix.CreateRotationX(-1.6459138f) * Matrix.CreateRotationY(-0.9861076f) * Matrix.CreateRotationZ(0.063855976f) * Matrix.CreateTranslation(33.743656f, 60.577694f, -116.96059f),
				Matrix.CreateScale(0.6249726f, 0.6249726f, 0.6249726f) * Matrix.CreateRotationX(-1.7233521f) * Matrix.CreateRotationY(-0.4229281f) * Matrix.CreateRotationZ(-0.057023462f) * Matrix.CreateTranslation(21.4265f, 61.10464f, -127.95314f),
				Matrix.CreateScale(0.6249726f, 0.6249726f, 0.6249726f) * Matrix.CreateRotationX(-1.7233521f) * Matrix.CreateRotationY(-0.4229281f) * Matrix.CreateRotationZ(-0.057023462f) * Matrix.CreateTranslation(23.290537f, 76.55791f, -130.13112f),
				Matrix.CreateScale(0.6249726f, 0.6249726f, 0.6249726f) * Matrix.CreateRotationX(-1.6459138f) * Matrix.CreateRotationY(-0.9861076f) * Matrix.CreateRotationZ(0.063855976f) * Matrix.CreateTranslation(33.725407f, 76.033485f, -117.60134f),
				Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationX(-1.3761346f) * Matrix.CreateRotationY(-0.05025345f) * Matrix.CreateRotationZ(-0.009903382f) * Matrix.CreateTranslation(0f, 80.78291f, -124.791214f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.4753562f) * Matrix.CreateRotationY(0.17653093f) * Matrix.CreateRotationZ(-0.19675669f) * Matrix.CreateTranslation(-22.332651f, 81.78893f, -129.62746f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.6803323f) * Matrix.CreateRotationY(0.6807442f) * Matrix.CreateRotationZ(-0.045894645f) * Matrix.CreateTranslation(-38.181618f, 81.58589f, -121.385925f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-0.9789075f) * Matrix.CreateRotationY(0.6996845f) * Matrix.CreateRotationZ(0.2695538f) * Matrix.CreateTranslation(-37.792046f, 91.08124f, -119.16617f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.0960026f) * Matrix.CreateRotationY(0.18824755f) * Matrix.CreateRotationZ(0.005669557f) * Matrix.CreateTranslation(-21.740582f, 93.62254f, -126.8969f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.0960026f) * Matrix.CreateRotationY(0.18824755f) * Matrix.CreateRotationZ(0.005669557f) * Matrix.CreateTranslation(-0.18245436f, 98.38738f, -128.57964f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.2608281f) * Matrix.CreateRotationY(-0.08839668f) * Matrix.CreateRotationZ(-0.0843975f) * Matrix.CreateTranslation(15.713474f, 97.22399f, -127.68208f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-1.379857f) * Matrix.CreateRotationY(-0.5223218f) * Matrix.CreateRotationZ(-0.17590432f) * Matrix.CreateTranslation(26.163097f, 88.17002f, -126.92717f),
				Matrix.CreateScale(0.68988013f, 0.5796665f, 0.68988013f) * Matrix.CreateRotationX(-0.8142616f) * Matrix.CreateRotationY(-0.8590925f) * Matrix.CreateRotationZ(-0.6278064f) * Matrix.CreateTranslation(32.32895f, 93.55564f, -116.80268f),
				Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationX(-0.512174f) * Matrix.CreateRotationY(-0.05219151f) * Matrix.CreateRotationZ(-0.7852635f) * Matrix.CreateTranslation(20.577557f, 101.301186f, -113.12041f),
				Matrix.CreateScale(0.9988255f, 0.94390815f, 0.94390815f) * Matrix.CreateRotationX(-0.8706422f) * Matrix.CreateRotationY(-0.05025345f) * Matrix.CreateRotationZ(-0.009903382f) * Matrix.CreateTranslation(-2.642398f, 108.05068f, -114.00987f),
				Matrix.CreateScale(0.939259f, 0.939259f, 0.939259f) * Matrix.CreateRotationX(-0.63442296f) * Matrix.CreateRotationY(0.24199164f) * Matrix.CreateRotationZ(0.54448134f) * Matrix.CreateTranslation(-24.607763f, 100.64297f, -112.88944f)
			};
			for (int num10 = 0; num10 < 27; num10++)
			{
				this.dropChunk(ref this.assChunk, array5[num10], 3);
			}
			this.cuttyBone = new Matrix[this.a.skinTransforms.Length];
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00262D68 File Offset: 0x00260F68
		public void setCuttyTargets()
		{
			this.screenTarget1 = new RenderTarget2D(this.sc.GraphicsDevice, 600, 600, true, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.screenTarget2 = new RenderTarget2D(this.sc.GraphicsDevice, 600, 600, true, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.sc.isLoading = false;
			this.initTarget();
			this.sc.isLoading = true;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00262DE0 File Offset: 0x00260FE0
		public void disposeCuttyTargets()
		{
			if (!this.screenTarget1.IsDisposed)
			{
				this.screenTarget1.Dispose();
				this.screenTarget1 = new RenderTarget2D(this.sc.GraphicsDevice, 4, 4, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			}
			if (!this.screenTarget2.IsDisposed)
			{
				this.screenTarget2.Dispose();
				this.screenTarget2 = new RenderTarget2D(this.sc.GraphicsDevice, 4, 4, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00262E57 File Offset: 0x00261057
		public void UnloadContent()
		{
			this.sparks.unloadContent();
			this.dots.unloadContent();
			this.content.Unload();
			this.contentB.Unload();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00262E88 File Offset: 0x00261088
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
				Cutty.SetCurveKeyTangent(ref curveKey, ref curveKey3, ref curveKey2);
				x.Keys[i] = curveKey3;
				curveKey = y.Keys[num];
				curveKey2 = y.Keys[num2];
				curveKey3 = y.Keys[i];
				Cutty.SetCurveKeyTangent(ref curveKey, ref curveKey3, ref curveKey2);
				y.Keys[i] = curveKey3;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00262F68 File Offset: 0x00261168
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

		// Token: 0x060009B3 RID: 2483 RVA: 0x00262FEF File Offset: 0x002611EF
		public void damHealth(ushort amt)
		{
			if (this.cuttyHeal || !Cutty.cuttyDoneSpeech)
			{
				return;
			}
			this.health -= amt;
			if (this.health < 0 || this.health >= 15000)
			{
				this.health = 0;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00263030 File Offset: 0x00261230
		public void checkTargets(Vector3 gunpos, Vector3 gunlook, ref Cursor genCursor, float wall)
		{
			if (!Cutty.cuttyDoneSpeech)
			{
				this.health = this.startHealth;
				return;
			}
			this.cuttyDistance = 20000f;
			float num = 20000f;
			this.boneHit = -1;
			Matrix matrix = Matrix.Identity;
			this.cuttyisHit = false;
			for (int i = 0; i < this.col_Bone.Length; i++)
			{
				matrix = Matrix.CreateTranslation(this.col_Pos[i]) * this.cuttyBone[this.col_Bone[i]];
				Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
				float? num2 = genCursor.hitSphere2(gunpos, gunlook, vector, this.col_Scale[i] * this.cuttyScale);
				if (num2 != null && num2.Value < num && num2.Value < wall)
				{
					this.cuttyisHit = true;
					this.hitCenter = Vector3.Transform(Vector3.Zero, matrix);
					this.hitEdge = num2.Value * genCursor.rayDir + genCursor.rayPos;
					num = num2.Value;
					this.cuttyDistance = num;
					this.boneHit = i;
				}
			}
			if (this.cuttyisHit && this.boneHit != -1)
			{
				if (this.boneHit == 0)
				{
					Vector3 vector2 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					if (vector2.X < 0f)
					{
						Vector2 vector3 = new Vector2(222f, 183f);
						Vector2 vector4 = new Vector2(500f, 268f);
						Vector2 vector5 = new Vector2(300f, 530f);
						Vector2 vector6 = new Vector2(504f, 472f);
						this.addBulletHole(vector3, vector4, vector5, vector6, vector2.Z, vector2.Y, this.boneHit);
					}
					else
					{
						Vector2 vector7 = new Vector2(790f, 183f);
						Vector2 vector8 = new Vector2(515f, 268f);
						Vector2 vector9 = new Vector2(734f, 550f);
						Vector2 vector10 = new Vector2(517f, 470f);
						this.addBulletHole(vector7, vector8, vector9, vector10, vector2.Z, vector2.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 1)
				{
					Vector3 vector11 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					if (vector11.X < 0f)
					{
						Vector2 vector12 = new Vector2(320f, 104f);
						Vector2 vector13 = new Vector2(522f, 125f);
						Vector2 vector14 = new Vector2(267f, 300f);
						Vector2 vector15 = new Vector2(522f, 309f);
						this.addBulletHole(vector12, vector13, vector14, vector15, vector11.Z, vector11.Y, this.boneHit);
					}
					else
					{
						Vector2 vector16 = new Vector2(722f, 117f);
						Vector2 vector17 = new Vector2(502f, 125f);
						Vector2 vector18 = new Vector2(774f, 300f);
						Vector2 vector19 = new Vector2(502f, 309f);
						this.addBulletHole(vector16, vector17, vector18, vector19, vector11.Z, vector11.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 2)
				{
					Vector3 vector20 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					if (vector20.X < 0f)
					{
						Vector2 vector21 = new Vector2(368f, 37f);
						Vector2 vector22 = new Vector2(515f, 23f);
						Vector2 vector23 = new Vector2(334f, 150f);
						Vector2 vector24 = new Vector2(515f, 178f);
						this.addBulletHole(vector21, vector22, vector23, vector24, vector20.Z, vector20.Y, this.boneHit);
					}
					else
					{
						Vector2 vector25 = new Vector2(658f, 33f);
						Vector2 vector26 = new Vector2(509f, 22f);
						Vector2 vector27 = new Vector2(692f, 158f);
						Vector2 vector28 = new Vector2(509f, 176f);
						this.addBulletHole(vector25, vector26, vector27, vector28, vector20.Z, vector20.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 3)
				{
					Vector3 vector29 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					if (vector29.X < 0f)
					{
						Vector2 vector30 = new Vector2(505f, 434f);
						Vector2 vector31 = new Vector2(514f, 591f);
						Vector2 vector32 = new Vector2(227f, 477f);
						Vector2 vector33 = new Vector2(460f, 714f);
						this.addBulletHole(vector30, vector31, vector32, vector33, vector29.Y, -vector29.Z, this.boneHit);
					}
					else
					{
						Vector2 vector34 = new Vector2(512f, 429f);
						Vector2 vector35 = new Vector2(511f, 591f);
						Vector2 vector36 = new Vector2(755f, 480f);
						Vector2 vector37 = new Vector2(559f, 713f);
						this.addBulletHole(vector34, vector35, vector36, vector37, vector29.Y, -vector29.Z, -this.boneHit);
					}
				}
				else if (this.boneHit == 4)
				{
					Vector3 vector38 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					Vector2 vector39 = new Vector2(862f, 140f);
					Vector2 vector40 = new Vector2(687f, 182f);
					Vector2 vector41 = new Vector2(888f, 347f);
					Vector2 vector42 = new Vector2(723f, 409f);
					this.addBulletHole(vector39, vector40, vector41, vector42, vector38.Z, vector38.Y, this.boneHit);
				}
				else if (this.boneHit == 5)
				{
					Vector3 vector43 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					Vector2 vector44 = new Vector2(161f, 135f);
					Vector2 vector45 = new Vector2(332f, 162f);
					Vector2 vector46 = new Vector2(120f, 334f);
					Vector2 vector47 = new Vector2(269f, 430f);
					this.addBulletHole(vector44, vector45, vector46, vector47, vector43.Z, vector43.Y, this.boneHit);
				}
				else if (this.boneHit == 6)
				{
					Vector3 vector48 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					Vector2 vector49 = new Vector2(758f, 631f);
					Vector2 vector50 = new Vector2(704f, 522f);
					Vector2 vector51 = new Vector2(664f, 698f);
					Vector2 vector52 = new Vector2(594f, 603f);
					this.addBulletHole(vector49, vector50, vector51, vector52, vector48.Z, vector48.Y, this.boneHit);
				}
				else if (this.boneHit == 7)
				{
					Vector3 vector53 = Vector3.Transform(this.hitEdge - this.hitCenter, Matrix.CreateRotationY(-this.cuttyRot));
					Vector2 vector54 = new Vector2(272f, 629f);
					Vector2 vector55 = new Vector2(307f, 516f);
					Vector2 vector56 = new Vector2(359f, 702f);
					Vector2 vector57 = new Vector2(426f, 599f);
					this.addBulletHole(vector54, vector55, vector56, vector57, vector53.Z, vector53.Y, this.boneHit);
				}
				ushort num3 = 0;
				if (this.distanceCutty < 4000f)
				{
					this.hitcounter++;
					if (this.df == 5 && this.hitcounter % 2 == 0)
					{
						num3 += 1;
					}
					if (this.df == 4)
					{
						num3 += 1;
					}
					if (this.df == 3)
					{
						num3 += 2;
					}
					if (this.df > 2 && Cutty.cuttyCount > 1)
					{
						num3 += 1;
					}
					if (this.df == 2)
					{
						num3 += 1;
					}
					if (this.df == 1)
					{
						num3 += 2;
					}
					if (this.df == 0)
					{
						num3 += 4;
					}
				}
				if (this.distanceCutty < 1200f)
				{
					if (this.df == 5)
					{
						num3 += 1;
					}
					if (this.df == 4)
					{
						num3 += 1;
					}
					if (this.df == 3)
					{
						num3 += 2;
					}
					if (this.df > 2 && Cutty.cuttyCount > 1)
					{
						num3 += 1;
					}
					if (this.df == 2)
					{
						num3 += 2;
					}
					if (this.df == 1)
					{
						num3 += 3;
					}
					if (this.df == 0)
					{
						num3 += 4;
					}
				}
				this.damHealth(num3);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00263964 File Offset: 0x00261B64
		private void addBulletHole(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float sendY, float sendX, int bonehit)
		{
			float num = this.col_Scale[this.boneHit] * this.cuttyScale;
			float num2 = (sendY + num) / (num * 2f);
			float num3 = (sendX + num) / (num * 2f);
			float num4 = MathHelper.Lerp(p3.X, p1.X, num2);
			float num5 = MathHelper.Lerp(p4.X, p2.X, num2);
			float num6 = MathHelper.Lerp(num4, num5, num3);
			float num7 = MathHelper.Lerp(p3.Y, p4.Y, num3);
			float num8 = MathHelper.Lerp(p1.Y, p2.Y, num3);
			float num9 = MathHelper.Lerp(num7, num8, num2);
			Cutty.uvIndex = (byte)this.myIndex;
			Cutty.xcoord = (int)num6;
			Cutty.ycoord = (int)num9;
			this.addTargetBlood(this.sc.paintColor, (int)num6 - 25, (int)num6 + 25, (int)num9 - 25, (int)num9 + 25);
			if (Math.Abs(bonehit) == 1 && num3 > 0.7f)
			{
				this.addTargetBlood(this.sc.paintColor, 275, 417, 736, 836);
			}
			if (bonehit == 0)
			{
				if (num3 > 0.6f)
				{
					this.addTargetBlood(this.sc.paintColor, 592, 945, 755, 880);
					this.addTargetBlood(this.sc.paintColor, 592, 945, 830, 970);
					if (this.distanceCutty < 4000f && !this.cuttyHeal)
					{
						this.spineDamage += 1;
						if (this.spineDamage > 300)
						{
							this.spineDamage = 300;
						}
						if ((float)this.spineDamage > this.spineBreach[this.df])
						{
							if (!this.spineDestroyed)
							{
								this.spineSwitch();
							}
							if ((float)this.spineDamage < this.spineBreach[this.df] + (float)(480 / this.spineDam[this.df]))
							{
								this.damHealth(this.spineDam[this.df]);
								this.buzzSkeleton(8);
								this.boneStrike = true;
							}
						}
					}
				}
				if (this.health <= 0)
				{
					this.buzzSkeleton(8);
					this.seizureTimer = 40;
					this.boneStrike = true;
				}
			}
			if (Math.Abs(bonehit) == 2)
			{
				if (this.distanceCutty < 2500f && !this.cuttyHeal)
				{
					this.faceDamage += 1;
					if (this.faceDamage > 300)
					{
						this.faceDamage = 300;
					}
					if ((float)this.faceDamage > this.faceBreach[this.df])
					{
						if (!this.faceDestroyed)
						{
							this.faceSwitch();
						}
						if ((float)this.faceDamage < this.faceBreach[this.df] + (float)(560 / this.faceDam[this.df]))
						{
							this.damHealth(this.faceDam[this.df]);
							this.buzzSkeleton(4);
							this.boneStrike = true;
						}
					}
				}
				if (this.health <= 0)
				{
					this.buzzSkeleton(8);
					this.seizureTimer = 40;
					this.boneStrike = true;
				}
			}
			if (Math.Abs(bonehit) == 3)
			{
				if (this.distanceCutty < 4000f && !this.cuttyHeal)
				{
					this.assDamage += 1;
					if (this.assDamage > 300)
					{
						this.assDamage = 300;
					}
					if ((float)this.assDamage > this.assBreach[this.df])
					{
						if (!this.assDestroyed)
						{
							this.assSwitch();
						}
						if ((float)this.assDamage < this.assBreach[this.df] + (float)(420 / this.assDam[this.df]))
						{
							this.damHealth(this.assDam[this.df]);
							this.buzzSkeleton(8);
							this.boneStrike = true;
							this.seizureTimer = 40;
						}
					}
				}
				if (this.health <= 0)
				{
					this.buzzSkeleton(8);
					this.seizureTimer = 40;
					this.boneStrike = true;
				}
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00263D7B File Offset: 0x00261F7B
		public void buzzSkeleton(int amt)
		{
			this.showSkelTimer = amt;
			this.sc.buzz.Play(this.sc.ev, (float)this.rr.Next(-50, 20) / 100f, 0f);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00263DBC File Offset: 0x00261FBC
		public void runDestructionChecks(int type)
		{
			if (type < 1)
			{
				return;
			}
			if ((float)this.spineDamage > this.spineBreach[this.df] && !this.spineDestroyed)
			{
				this.spineSwitch();
			}
			if ((float)this.assDamage > this.assBreach[this.df] && !this.assDestroyed)
			{
				this.assSwitch();
			}
			if ((float)this.faceDamage > this.faceBreach[this.df] && !this.faceDestroyed)
			{
				this.faceSwitch();
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00263E3C File Offset: 0x0026203C
		private void spineSwitch()
		{
			this.talkIndex = 8;
			Cutty.whichPigTalks = this.myIndex;
			this.sc.cuttygouge.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, (float)this.rr.Next(-20, 30) / 100f);
			this.chunk.startDrop = true;
			this.spineDestroyed = true;
			if ((float)this.spineDamage < this.spineBreach[this.df])
			{
				this.spineDamage = (ushort)(this.spineBreach[this.df] + 5f);
			}
			if (this.bodyState == 0)
			{
				this.bodyState = 1;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 1;
				return;
			}
			if (this.bodyState == 2)
			{
				this.bodyState = 4;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 4;
				return;
			}
			if (this.bodyState == 3)
			{
				this.bodyState = 5;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 5;
				return;
			}
			if (this.bodyState == 6)
			{
				this.bodyState = 7;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 7;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00263F88 File Offset: 0x00262188
		private void assSwitch()
		{
			this.talkIndex = 8;
			Cutty.whichPigTalks = this.myIndex;
			this.assChunk.startDrop = true;
			this.sc.cuttygouge.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, (float)this.rr.Next(-20, 30) / 100f);
			this.assDestroyed = true;
			if ((float)this.assDamage < this.assBreach[this.df])
			{
				this.assDamage = (ushort)(this.assBreach[this.df] + 5f);
			}
			if (this.bodyState == 0)
			{
				this.bodyState = 2;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 2;
				return;
			}
			if (this.bodyState == 1)
			{
				this.bodyState = 4;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 4;
				return;
			}
			if (this.bodyState == 3)
			{
				this.bodyState = 6;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 6;
				return;
			}
			if (this.bodyState == 5)
			{
				this.bodyState = 7;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 7;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x002640D4 File Offset: 0x002622D4
		private void faceSwitch()
		{
			this.talkIndex = 8;
			Cutty.whichPigTalks = this.myIndex;
			this.faceChunk.startDrop = true;
			this.sc.cuttygouge.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, (float)this.rr.Next(-20, 30) / 100f);
			this.faceDestroyed = true;
			if ((float)this.faceDamage < this.faceBreach[this.df])
			{
				this.faceDamage = (ushort)(this.faceBreach[this.df] + 5f);
			}
			if (this.bodyState == 0)
			{
				this.bodyState = 3;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 3;
				return;
			}
			if (this.bodyState == 2)
			{
				this.bodyState = 6;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 6;
				return;
			}
			if (this.bodyState == 1)
			{
				this.bodyState = 5;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 5;
				return;
			}
			if (this.bodyState == 4)
			{
				this.bodyState = 7;
				this.pigModel = this.sc.pigAll;
				this.pigIndex = 7;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00264220 File Offset: 0x00262420
		public void Update(GameTime gm, NetworkSession net, Vector3 campos, int timeframe, Vector3 lookVec, Vector3 playerPos, Vector3 remotePos, float playerHealth, float remoteHealth, bool isSpectating, ref float[,] heights)
		{
			this.healthPerc = (float)this.health / (float)this.startHealth;
			this.df = this.sc.df;
			if (!this.spectator)
			{
				this.df += 3;
			}
			this.networkSession = net;
			this.playerpos = playerPos;
			this.remotepos = remotePos;
			this.timeframe = timeframe;
			this.spectator = isSpectating;
			this.playerHealth = playerHealth;
			this.remoteHealth = remoteHealth;
			if (this.sc.host && !this.deathsent)
			{
				this.pigLogic();
			}
			this.pigBones(lookVec, ref heights);
			if (this.chunk.startDrop)
			{
				this.updateChunk(ref this.chunk, ref heights);
			}
			if (this.faceChunk.startDrop)
			{
				this.updateChunk(ref this.faceChunk, ref heights);
			}
			if (this.assChunk.startDrop)
			{
				this.updateChunk(ref this.assChunk, ref heights);
			}
			if (this.sc.host)
			{
				this.runScheduler(ref heights);
			}
			if (this.shockTimer > 0f)
			{
				this.shockWave(ref heights);
			}
			if (this.sc.host)
			{
				this.fireTimer--;
				if (this.fireTimer <= 0)
				{
					this.onFire = false;
				}
				else
				{
					ushort num = 1;
					int num2 = 10;
					if (this.spectator)
					{
						num2 = 5;
					}
					if (this.df == 5)
					{
						num = 1;
					}
					if (this.df == 4)
					{
						num = 2;
					}
					if (this.df == 3)
					{
						num = 3;
					}
					if (this.df == 2)
					{
						num = 1;
					}
					if (this.df == 1)
					{
						num = 3;
					}
					if (this.df == 0)
					{
						num = 4;
					}
					if (this.sc.myTimer % (float)num2 == 0f)
					{
						this.damHealth(num);
					}
				}
			}
			if (this.onFire)
			{
				this.fireRamp -= 2;
				if (this.fireRamp < 4)
				{
					this.fireRamp = 4;
				}
			}
			else
			{
				this.fireRamp++;
				if (this.fireRamp > 130)
				{
					this.fireRamp = 130;
				}
			}
			if (this.fireRamp < 130 && this.sc.myTimer % (float)this.fireRamp == 0f)
			{
				this.addExplosion(ref this.fireball, campos);
			}
			this.updateExplosion(ref this.fireball, campos);
			this.sparks.Update(gm);
			this.dots.Update(gm);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00264484 File Offset: 0x00262684
		private void dropChunk(ref Cutty.shell sh, Matrix mm, int bone)
		{
			sh.offset[sh.index] = mm;
			sh.bone[sh.index] = bone;
			sh.dupe[sh.index].init((float)this.rr.Next(20, 60) / 100f, 2f, 1f, mm, new Vector3(2f, 1f, 0f), -(float)this.rr.Next(80, 160) / 1000f, 80, 80f, 220f);
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity - 1)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0026459C File Offset: 0x0026279C
		private void updateChunk(ref Cutty.shell sh, ref float[,] heights)
		{
			sh.tempindex = 0;
			bool flag = true;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move == 1 || sh.dupe[i].move == 2)
				{
					sh.dupe[i].Update(ref heights);
				}
				if (sh.dupe[i].move == 0)
				{
					sh.dupe[i].transform = sh.offset[i] * this.cuttyBone[sh.bone[i]];
				}
				if (sh.startDrop)
				{
					sh.dropTimer += 0.013f;
					if ((float)i < sh.dropTimer && sh.dupe[i].move == 0)
					{
						sh.dupe[i].move = 1;
						sh.dupe[i].createState(sh.dupe[i].transform);
						Vector3 vector = (this.cuttyPos - this.oldcuttyPos) * (float)this.rr.Next(35, 110) / 100f;
						sh.dupe[i].velocity = vector + new Vector3((float)this.rr.Next(-50, 50) / 30f, (float)this.rr.Next(10, 40) / 10f, (float)this.rr.Next(-50, 50) / 30f);
						sh.dupe[i].Update(ref heights);
						this.cuttyChunkRelease = true;
						this.hitCenter2 = sh.dupe[i].mypos;
						this.hitEdge2 = this.hitCenter2 + sh.dupe[i].velocity;
					}
					if (sh.dupe[i].move != 3)
					{
						flag = false;
					}
				}
				sh.stream[i].Trans = sh.dupe[i].transform;
				sh.displayList[sh.tempindex] = sh.stream[i];
				sh.tempindex++;
			}
			if (sh.startDrop && flag)
			{
				sh.startDrop = false;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x002647F0 File Offset: 0x002629F0
		private void addExplosion(ref Cutty.hole hole, Vector3 campos)
		{
			hole.location[hole.stainIndex] = new Vector3(0f, 145.42426f, 0f) + new Vector3((float)this.rr.Next(-90, 90) / 10f, (float)this.rr.Next(-120, -60) / 10f, (float)this.rr.Next(-100, 1150) / 10f);
			hole.bone[hole.stainIndex] = 5;
			Matrix matrix = Matrix.CreateTranslation(hole.location[hole.stainIndex]) * this.cuttyBone[5];
			Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
			Matrix matrix2 = Matrix.CreateBillboard(vector, new Vector3(campos.X, (campos.Y - vector.Y) / 2f, campos.Z), this.view.Up, new Vector3?(this.view.Forward));
			Vector3 vector2 = new Vector3(340f, 600f, 340f) * (float)this.rr.Next(70, 160) / 400f;
			if (this.rr.Next(1, 100) < 50)
			{
				hole.scale[hole.stainIndex] = Matrix.CreateScale(vector2) * Matrix.CreateRotationZ((float)this.rr.Next(-20, 20) / 100f);
			}
			else
			{
				hole.scale[hole.stainIndex] = Matrix.CreateScale(vector2) * Matrix.CreateRotationY(3.14f) * Matrix.CreateRotationZ((float)this.rr.Next(-20, 20) / 100f);
			}
			hole.stainTrans[hole.stainIndex].Trans = hole.scale[hole.stainIndex] * matrix2;
			hole.fade[hole.stainIndex] = (float)this.rr.Next(80, 100) / 100f;
			hole.stainTrans[hole.stainIndex].Fade = hole.fade[hole.stainIndex];
			Vector4 vector3 = hole.stainR[0];
			hole.stainTrans[hole.stainIndex].Coord = new Vector4(1864f / vector3.Z, vector3.X / 1864f, 1200f / vector3.W, vector3.Y / 1200f);
			hole.frame[hole.stainIndex] = 0;
			hole.stainIndex++;
			if (hole.stainIndex > hole.stainCapacity - 1)
			{
				hole.stainIndex = 0;
			}
			hole.stainMax++;
			if (hole.stainMax > hole.stainCapacity - 1)
			{
				hole.stainMax = hole.stainCapacity;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00264B18 File Offset: 0x00262D18
		private void updateExplosion(ref Cutty.hole hole, Vector3 campos)
		{
			if (!this.burning.sound[0].IsDisposed)
			{
				if (hole.stainMax == 0)
				{
					this.burning.sound[0].Volume = 0f;
					return;
				}
				this.burning.sound[0].Volume = this.sc.ev - MathHelper.Clamp((this.distanceCutty - 200f) / 2200f, 0f, this.sc.ev);
			}
			bool flag = false;
			for (int i = 0; i < hole.stainMax; i++)
			{
				hole.frame[i]++;
				if (hole.frame[i] > hole.stainR.Length - 1)
				{
					hole.frame[i] = hole.stainR.Length - 1;
				}
				else
				{
					hole.location[i] += new Vector3(0f, -1f, -1.1f);
					Matrix matrix = Matrix.CreateTranslation(hole.location[i]) * this.cuttyBone[hole.bone[i]];
					Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
					Matrix matrix2 = Matrix.CreateBillboard(vector, new Vector3(campos.X, (campos.Y - vector.Y) / 2f, campos.Z), this.view.Up, new Vector3?(this.view.Forward));
					hole.stainTrans[i].Trans = hole.scale[i] * matrix2;
					Vector4 vector2 = hole.stainR[hole.frame[i]];
					hole.stainTrans[i].Coord = new Vector4(1864f / vector2.Z, vector2.X / 1864f, 1200f / vector2.W, vector2.Y / 1200f);
					hole.stainTrans[i].Fade = hole.fade[i];
					flag = true;
				}
			}
			if (!flag)
			{
				hole.stainIndex = 0;
				hole.stainMax = 0;
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00264D70 File Offset: 0x00262F70
		private void animManager(string type)
		{
			if (this.animClip != 0 && this.animClip == 3 && this.animCount == 5f)
			{
				this.sc.chomp2.Play(this.sc.ev, 0f, 0f);
			}
			if (type == "bite")
			{
				this.animList.Clear();
				this.animList.Add(5);
				this.animList.Add(7);
				this.animList.Add(8);
				this.animList.Add(9);
				this.animList.Add(12);
				this.animList.Add(16);
				this.animClip = 3;
				this.animCount = 0f;
				this.animMin = 0;
				this.animMax = 42;
				this.animTween = 0f;
				this.animLoop = 0;
				this.gonnaBite = false;
				return;
			}
			if (type == "bite2")
			{
				this.animList.Clear();
				this.animList.Add(5);
				this.animList.Add(7);
				this.animList.Add(8);
				this.animList.Add(9);
				this.animList.Add(12);
				this.animList.Add(16);
				this.animClip = 3;
				this.animCount = 0f;
				this.animMin = 0;
				this.animMax = 42;
				this.animTween = 0f;
				this.animLoop = 1;
				this.gonnaBite = false;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00264F04 File Offset: 0x00263104
		private void setbreakLevels()
		{
			this.break1 = 510;
			this.break2 = 790;
			if (this.df == 0)
			{
				if (this.healthPerc <= 1f && this.healthPerc >= 0.75f)
				{
					this.break1 = 400;
					this.break2 = 500;
				}
				if ((double)this.healthPerc < 0.75 && this.healthPerc >= 0.25f)
				{
					this.break1 = 150;
					this.break2 = 250;
				}
				if ((double)this.healthPerc < 0.25 && this.healthPerc >= 0f)
				{
					this.break1 = 200;
					this.break2 = 350;
				}
			}
			else if (this.df == 1)
			{
				if (this.healthPerc <= 1f && this.healthPerc >= 0.75f)
				{
					this.break1 = 650;
					this.break2 = 790;
				}
				if ((double)this.healthPerc < 0.75 && this.healthPerc >= 0.25f)
				{
					this.break1 = 370;
					this.break2 = 510;
				}
				if ((double)this.healthPerc < 0.25 && this.healthPerc >= 0f)
				{
					this.break1 = 510;
					this.break2 = 850;
				}
			}
			else if (this.df == 2)
			{
				if (this.healthPerc <= 1f && this.healthPerc >= 0.75f)
				{
					this.break1 = 650;
					this.break2 = 750;
				}
				if ((double)this.healthPerc < 0.75 && this.healthPerc >= 0.25f)
				{
					this.break1 = 510;
					this.break2 = 790;
				}
				if ((double)this.healthPerc < 0.25 && this.healthPerc >= 0f)
				{
					this.break1 = 600;
					this.break2 = 840;
				}
			}
			else if (this.df == 3)
			{
				if (this.healthPerc <= 1f && this.healthPerc >= 0.75f)
				{
					this.break1 = 400;
					this.break2 = 500;
				}
				if ((double)this.healthPerc < 0.75 && this.healthPerc >= 0.25f)
				{
					this.break1 = 150;
					this.break2 = 250;
				}
				if ((double)this.healthPerc < 0.25 && this.healthPerc >= 0f)
				{
					this.break1 = 200;
					this.break2 = 350;
				}
			}
			else if (this.df == 4)
			{
				if (this.healthPerc <= 1f && this.healthPerc >= 0.75f)
				{
					this.break1 = 550;
					this.break2 = 790;
				}
				if ((double)this.healthPerc < 0.75 && this.healthPerc >= 0.35f)
				{
					this.break1 = 650;
					this.break2 = 710;
				}
				if ((double)this.healthPerc < 0.35 && this.healthPerc >= 0f)
				{
					this.break1 = 610;
					this.break2 = 870;
				}
			}
			else if (this.df == 5)
			{
				if (this.healthPerc <= 1f && this.healthPerc >= 0.75f)
				{
					this.break1 = 650;
					this.break2 = 750;
				}
				if ((double)this.healthPerc < 0.75 && this.healthPerc >= 0.25f)
				{
					this.break1 = 510;
					this.break2 = 790;
				}
				if ((double)this.healthPerc < 0.25 && this.healthPerc >= 0f)
				{
					this.break1 = 600;
					this.break2 = 850;
				}
			}
			this.oldDF = this.df;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00265350 File Offset: 0x00263550
		private void pigLogic()
		{
			if (this.oldDF != this.df)
			{
				this.setbreakLevels();
			}
			this.targetWait--;
			int num = this.curveIndex;
			if (!this.cuttyDoneLocalSpeech)
			{
				if (Cutty.allplayersReady && !Cutty.someoneTalking)
				{
					if (Cutty.speeches < this.speechList.Count && this.speechInclude.Contains(Cutty.speeches))
					{
						Cutty.someoneTalking = true;
						this.cuttyStruct.homing = 0;
						this.cuttyStruct.curveIndex = this.speechCurveindex;
						this.cuttyStruct.targetRate = 0f;
						this.cuttyStruct.loop = 0.1f;
						this.cuttyStruct.animType = 0;
						this.cuttyStruct.talkindex = this.speechList[Cutty.speeches];
						this.scheduleAction(130);
						Cutty.speeches++;
						return;
					}
					if (Cutty.speeches >= this.speechList.Count)
					{
						this.targetWait = this.rr.Next(600, 800);
						this.cuttyStruct.homing = 0;
						this.cuttyStruct.curveIndex = this.startCurveindex;
						this.cuttyStruct.targetRate = this.startRate;
						this.cuttyStruct.loop = this.startLoop;
						this.cuttyStruct.animType = 0;
						this.cuttyStruct.talkindex = -1;
						this.scheduleAction(130);
						this.cuttyDoneLocalSpeech = true;
						Cutty.cuttyDoneSpeech = true;
					}
				}
				return;
			}
			if (!this.newAction && this.health <= 0)
			{
				this.deathsent = true;
				this.targetWait = 500;
				this.cuttyStruct.dur = 40;
				this.cuttyStruct.destx = this.cuttyPos.X + -(float)Math.Cos((double)(this.cuttyRot + 1.57f)) * 600f;
				this.cuttyStruct.destz = this.cuttyPos.Z + (float)Math.Sin((double)(this.cuttyRot + 1.57f)) * 600f;
				this.cuttyStruct.rot = this.cuttyRot;
				this.scheduleAction(132);
				this.newAction = true;
				return;
			}
			this.nextAttack--;
			if (!this.newAction && this.playerHealth != 100f && (this.remoteHealth != 100f || this.spectator) && this.healthPerc < 0.72f && this.clipIndexA == 1 && this.nextAttack < 0 && this.homing == 0)
			{
				this.homing = 0;
				float num2 = 9000f;
				float num3 = 9000f;
				int num4 = 0;
				float num5 = 0f;
				if (this.distanceCutty > 200f && this.distanceCutty < 1500f)
				{
					num2 = MathHelper.Clamp(this.distanceCutty, 300f, 1200f);
				}
				if (!this.spectator && this.distanceCutty2 >= 200f && this.distanceCutty2 <= 1500f)
				{
					num3 = MathHelper.Clamp(this.distanceCutty2, 300f, 1200f);
				}
				if (num2 < num3)
				{
					num4 = 1;
					num5 = num2;
				}
				if (num3 < num2)
				{
					num4 = 2;
					num5 = num3;
				}
				if (num5 >= 200f && num5 <= 1500f)
				{
					this.cuttyStruct.dur = (ushort)num5;
					if (num4 == 1)
					{
						this.cuttyStruct.destx = this.playerpos.X;
						this.cuttyStruct.destz = this.playerpos.Z;
					}
					else
					{
						this.cuttyStruct.destx = this.remotepos.X;
						this.cuttyStruct.destz = this.remotepos.Z;
					}
					this.scheduleAction(131);
					this.newAction = true;
					return;
				}
			}
			if (!this.newAction && Cutty.gonnaHealindex == -1)
			{
				this.gonnaHealDelay -= 1f;
				float num6 = 700f;
				bool flag = this.healCount == 2 && this.healthPerc > 0.4f && this.healthPerc < 0.75f;
				bool flag2 = this.healCount > 0 && this.healthPerc < 0.2f;
				if (this.healCount > 0 && (flag || flag2) && this.gonnaHealDelay <= 0f)
				{
					Vector3 zero = Vector3.Zero;
					float num7 = Vector3.Distance(this.roofTop1, this.cuttyPos);
					float num8 = this.roofTop1.Y;
					float num9 = num7;
					zero = this.roofTop1;
					if (num7 > num6)
					{
						float num10 = Vector3.Distance(this.roofTop2, this.cuttyPos);
						num8 = this.roofTop2.Y;
						num9 = num10;
						zero = this.roofTop2;
						if (num10 > num6)
						{
							num8 = this.roofTop3.Y;
							float num11 = Vector3.Distance(this.roofTop3, this.cuttyPos);
							zero = this.roofTop3;
							if (num11 <= num6)
							{
								num9 = num11;
							}
						}
					}
					if (num9 <= num6)
					{
						this.healCount--;
						this.cuttyStruct.dur = (ushort)num9;
						this.cuttyStruct.destx = zero.X;
						this.cuttyStruct.destz = zero.Z;
						float num12 = -(float)Math.Atan2((double)(this.cuttyPos.Z - zero.Z), (double)(this.cuttyPos.X - zero.X)) - 1.57f;
						float num13 = Cutty.WrapAngle(num8 - num12);
						float num14 = Cutty.WrapAngle(num8 + 3.14f - num12);
						if (Math.Abs(num13) <= Math.Abs(num14))
						{
							this.cuttyStruct.rot = num12 + num13;
						}
						else
						{
							this.cuttyStruct.rot = num12 + num14;
						}
						this.scheduleAction(133);
						this.newAction = true;
						return;
					}
				}
			}
			if (this.targetWait <= 0 && !this.newAction)
			{
				this.curveIndex = this.rr.Next(1, 4);
				if (this.curveIndex != num || this.homing > 10)
				{
					if (Cutty.homingLocal == this.myIndex)
					{
						Cutty.homingLocal = -1;
					}
					if (Cutty.homingRemote == this.myIndex)
					{
						Cutty.homingRemote = -1;
					}
					int num15 = this.rr.Next(0, 1000);
					int num16 = this.rr.Next(1, 3);
					if (this.spectator)
					{
						num16 = 1;
					}
					if (this.homing == 11)
					{
						if (this.distanceCutty < (float)this.rr.Next(100, 350))
						{
							num15 = 10;
						}
						else
						{
							num15 = this.rr.Next(this.break1, 1000);
						}
					}
					if (this.homing == 12)
					{
						if (this.distanceCutty2 < (float)this.rr.Next(100, 350))
						{
							num15 = 10;
						}
						else
						{
							num15 = this.rr.Next(this.break1, 1000);
						}
					}
					if (num15 < this.break1)
					{
						if (num16 == 1 && Cutty.homingLocal != -1 && Cutty.homingRemote == -1 && !this.spectator && this.remoteHealth > 0f)
						{
							num16 = 2;
						}
						if (num16 == 2 && Cutty.homingRemote != -1 && Cutty.homingLocal == -1 && this.playerHealth > 0f)
						{
							num16 = 1;
						}
						bool flag3 = Cutty.homingRemote != -1 && Cutty.homingLocal != -1;
						bool flag4 = num16 == 1 && Cutty.homingLocal != -1;
						bool flag5 = num16 == 2 && Cutty.homingRemote != -1;
						bool flag6 = this.playerHealth < 1f && num16 == 1;
						bool flag7 = this.remoteHealth < 1f && num16 == 2;
						bool flag8 = this.homingCount >= 4;
						bool flag9 = this.homingCount >= 2 && ((num16 == 1 && this.playerHealth < 105f) || (num16 == 2 && this.remoteHealth < 105f));
						if (flag8 || flag9 || flag6 || flag7 || flag3 || flag4 || flag5)
						{
							num15 = this.rr.Next(this.break1, 1000);
						}
					}
					if (num15 >= this.break2)
					{
						this.homingCount = 0;
						int num17 = -1;
						this.targetWait = this.rr.Next(270, 500);
						this.cuttyStruct.homing = 0;
						this.cuttyStruct.curveIndex = this.curveIndex;
						this.cuttyStruct.targetRate = (float)this.rr.Next(90, 130) / 100f * 8f;
						this.cuttyStruct.loop = this.loop;
						this.cuttyStruct.animType = 0;
						if (this.rr.Next(1, 500) < 40 && this.quips.Count > 0 && this.myIndex == 0)
						{
							int num18 = this.rr.Next(0, this.quips.Count);
							num17 = this.quips[num18];
							this.quips.RemoveAt(num18);
						}
						this.cuttyStruct.talkindex = num17;
						this.scheduleAction(130);
						return;
					}
					if (num15 >= this.break1)
					{
						this.targetWait = this.rr.Next(150, 300);
						int num19 = -1;
						if (this.homingCount >= 1)
						{
							num19 = this.rr.Next(9, 11);
						}
						this.homingCount = 0;
						this.cuttyStruct.homing = 0;
						this.cuttyStruct.curveIndex = this.curveIndex;
						this.cuttyStruct.targetRate = (float)this.rr.Next(80, 130) / 100f * 2f;
						this.cuttyStruct.loop = this.loop;
						this.cuttyStruct.animType = 0;
						if (this.rr.Next(1, 500) < 70 && this.quips.Count > 0 && this.myIndex == 0)
						{
							int num20 = this.rr.Next(0, this.quips.Count);
							num19 = this.quips[num20];
							this.quips.RemoveAt(num20);
						}
						this.cuttyStruct.talkindex = num19;
						this.scheduleAction(130);
						return;
					}
					if (num15 < this.break1)
					{
						int num21 = -1;
						this.homingCount++;
						if (num16 == 1 && this.distanceCutty > 1000f)
						{
							num21 = 12;
						}
						if (num16 == 2 && this.distanceCutty2 > 1000f)
						{
							num21 = 12;
						}
						this.targetWait = this.rr.Next(340, 600);
						if (this.homing < 10)
						{
							this.homingCount = 0;
							this.cuttyStruct.homing = num16;
						}
						if (this.homing == 11)
						{
							this.cuttyStruct.homing = 1;
							num21 = -1;
							this.targetWait = this.rr.Next(220, 360);
						}
						if (this.homing == 12)
						{
							this.cuttyStruct.homing = 2;
							num21 = -1;
							this.targetWait = this.rr.Next(220, 360);
						}
						if (this.cuttyStruct.homing == 1)
						{
							Cutty.homingLocal = this.myIndex;
						}
						if (this.cuttyStruct.homing == 2)
						{
							Cutty.homingRemote = this.myIndex;
						}
						this.cuttyStruct.curveIndex = num;
						this.cuttyStruct.targetRate = (float)this.rr.Next(80, 100) / 10f;
						this.cuttyStruct.loop = this.loop;
						this.cuttyStruct.animType = 0;
						this.cuttyStruct.talkindex = num21;
						this.scheduleAction(130);
					}
				}
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00265F7C File Offset: 0x0026417C
		private void volumeGallop()
		{
			if (!this.gallop.sound[0].IsDisposed)
			{
				if (this.clipIndexA == 0)
				{
					this.gallop.sound[0].Volume = this.sc.ev - MathHelper.Clamp(this.distanceCutty / 1900f, 0f, this.sc.ev);
					return;
				}
				this.gallop.sound[0].Volume = 0f;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00265FFC File Offset: 0x002641FC
		private void pigVolume()
		{
			if (!this.pigDialog1.sound[0].IsDisposed)
			{
				this.pigDialog1.sound[0].Volume = this.sc.vv;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00266030 File Offset: 0x00264230
		private void pigBones(Vector3 lookVec, ref float[,] heights)
		{
			this.talkSmooth = 0f;
			this.lookingatPig = false;
			this.distanceCutty = Vector3.Distance(this.playerpos, this.cuttyPos);
			this.distanceCutty2 = Vector3.Distance(this.remotepos, this.cuttyPos);
			if (this.attack1)
			{
				this.pigAttack(ref heights);
			}
			else if (this.cuttyHeal)
			{
				this.pigHealing(ref heights);
			}
			else if (this.death1 && !this.cuttyisDead)
			{
				this.pigDying();
			}
			else if (this.health > 0)
			{
				this.pigClipControl();
			}
			if (this.myClip != this.clipIndexA)
			{
				this.tween = 0f;
				this.clipIndexB = this.clipIndexA;
				this.clipIndexA = this.myClip;
			}
			this.a = this.pig1[this.clipIndexA];
			this.b = this.pig1[this.clipIndexB];
			this.volumeGallop();
			this.isSpeaking = false;
			if (this.pigJawIndex >= 0)
			{
				this.isSpeaking = true;
				this.talkSmooth = this.pigJaw[this.pigJawIndex] * 1f;
				this.pigJawIndex++;
				if (this.pigJaw[this.pigJawIndex] == -1f)
				{
					this.pigJawIndex = -1;
					Cutty.someoneTalking = false;
					this.pigLine = -1;
					this.talkSmooth = 0f;
				}
			}
			double num = (double)(this.pigFrame1 * 0.0417f);
			this.currentTimeValue = TimeSpan.FromSeconds(num % this.a.currentClipValue.Duration.TotalSeconds);
			this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 28;
			this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
			this.a.UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
			if (this.tween < 1f)
			{
				this.tween += 0.05f;
				this.currentTimeValue = TimeSpan.FromSeconds(num % this.b.currentClipValue.Duration.TotalSeconds);
				this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 28;
				this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
				this.b.UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
				for (int i = 0; i < this.b.boneTransforms.Length; i++)
				{
					this.a.boneTransforms[i] = Matrix.Lerp(this.b.boneTransforms[i], this.a.boneTransforms[i], this.tween);
				}
			}
			if (this.animCount > -1f)
			{
				num = (double)(this.animCount * 0.4f * 0.0417f);
				this.currentTimeValue = TimeSpan.FromSeconds(num % this.pig1[this.animClip].currentClipValue.Duration.TotalSeconds);
				this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 28;
				this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
				this.pig1[this.animClip].UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
				this.animTween = MathHelper.Clamp(this.animTween, 0f, 1f);
				for (int j = 0; j < this.animList.Count; j++)
				{
					this.a.boneTransforms[this.animList[j]] = this.a.boneTransforms[this.animList[j]] * (1f - this.animTween) + this.pig1[this.animClip].boneTransforms[this.animList[j]] * this.animTween;
				}
				this.animCount += 1f;
				if (this.animCount < (float)(this.animMin + 7))
				{
					this.animTween += 0.16666667f;
				}
				if (this.animCount > (float)(this.animMax - 7))
				{
					this.animTween -= 0.16666667f;
				}
				if (this.animCount > (float)this.animMax)
				{
					if (this.animLoop == 0)
					{
						this.animCount = -1f;
						this.animTween = 0f;
						this.animClip = 0;
					}
					else
					{
						this.animCount = (float)this.animMin;
						this.animTween = 0f;
						this.animLoop--;
					}
				}
			}
			this.isWatching = false;
			if (this.distanceCutty < 600f && this.health > 0)
			{
				if (this.homing == 0)
				{
					this.isWatching = true;
				}
				if (this.homing == 1)
				{
					this.isWatching = true;
				}
				if (this.homing == 2)
				{
					this.isWatching = true;
				}
			}
			float num2 = 0f;
			if (this.isWatching)
			{
				num2 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.playerpos.Z), (double)(this.cuttyPos.X - this.playerpos.X)) - (1.57f + this.cuttyRot);
			}
			float num3 = Cutty.WrapAngle(num2 - this.piglook);
			num3 = MathHelper.Clamp(num3, -0.013f, 0.013f);
			this.piglook = Cutty.WrapAngle(this.piglook + num3);
			this.piglook = MathHelper.Clamp(this.piglook, -0.8f, 0.8f);
			if (this.sc.myTimer % 250f == 0f)
			{
				this.pigPush = (float)this.rr.Next(-100, 100) / 100f;
			}
			this.a.boneTransforms[7] = Matrix.CreateRotationY(-this.piglook) * this.a.boneTransforms[7];
			if (this.isSpeaking)
			{
				float num4 = -4f;
				this.a.boneTransforms[9] = Matrix.CreateRotationZ(MathHelper.ToRadians(num4 + this.talkSmooth)) * this.a.boneTransforms[9];
				Matrix matrix = Matrix.CreateRotationZ(MathHelper.ToRadians(0f - this.talkSmooth / 7f));
				Matrix matrix2 = Matrix.CreateRotationX(MathHelper.ToRadians(0f - this.talkSmooth / 4f * (float)Math.Sin((double)(this.sc.myTimer / 14f))));
				this.a.boneTransforms[7] = matrix2 * this.a.boneTransforms[7];
				this.a.boneTransforms[7] = matrix * this.a.boneTransforms[7];
			}
			if (!this.cuttyisDead)
			{
				string text = "";
				this.cuttyVeloc = this.cuttyPos - this.oldcuttyPos;
				this.oldcuttyPos = this.cuttyPos;
				this.loop += 1f / this.targetDist[this.curveIndex] * this.targetRate;
				if (this.loop >= 1f)
				{
					this.loop = 0f;
				}
				Matrix matrix3 = Matrix.CreateTranslation(-2.6942043f, 12.643917f, 196.16183f) * this.cuttyBone[9];
				Vector3 vector = Vector3.Transform(Vector3.Zero, matrix3);
				float num5 = Vector3.DistanceSquared(new Vector3(this.playerpos.X, this.playerpos.Y + 80f, this.playerpos.Z), vector);
				float num6 = Vector3.DistanceSquared(new Vector3(this.remotepos.X, this.remotepos.Y + 80f, this.remotepos.Z), vector);
				if (!this.newAction)
				{
					if (this.homing == 0)
					{
						this.tx = this.targetX[this.curveIndex].Evaluate(this.loop);
						this.tz = this.targetZ[this.curveIndex].Evaluate(this.loop);
					}
					if (this.homing == 1 || this.homing == 11)
					{
						if (this.sc.host)
						{
							this.tx = this.playerpos.X;
							this.tz = this.playerpos.Z;
							if (this.homing != 11 && num5 < 22500f && !this.gonnaBite)
							{
								this.targetWait = 90;
								this.gonnaBite = true;
								this.cuttyStruct.homing = 11;
								this.cuttyStruct.curveIndex = this.curveIndex;
								if (this.rr.Next(1, 100) < 70)
								{
									this.cuttyStruct.targetRate = 3f;
								}
								else
								{
									this.cuttyStruct.targetRate = 8f;
								}
								this.cuttyStruct.loop = this.loop;
								this.cuttyStruct.animType = this.rr.Next(1, 3);
								this.cuttyStruct.talkindex = -1;
								this.scheduleAction(130);
							}
							if (this.distanceCutty < 50f && this.playerHealth > 105f)
							{
								this.cuttyAssPush = true;
							}
						}
						else
						{
							this.tx = this.remotepos.X;
							this.tz = this.remotepos.Z;
						}
					}
					if (this.homing == 2 || this.homing == 12)
					{
						if (this.sc.host)
						{
							this.tx = this.remotepos.X;
							this.tz = this.remotepos.Z;
							if (this.homing != 12 && num6 < 22500f && !this.gonnaBite)
							{
								this.targetWait = 90;
								this.gonnaBite = true;
								this.cuttyStruct.homing = 12;
								this.cuttyStruct.curveIndex = this.curveIndex;
								if (this.rr.Next(1, 100) < 70)
								{
									this.cuttyStruct.targetRate = 3f;
								}
								else
								{
									this.cuttyStruct.targetRate = 8f;
								}
								this.cuttyStruct.loop = this.loop;
								this.cuttyStruct.animType = this.rr.Next(1, 3);
								this.cuttyStruct.talkindex = -1;
								this.scheduleAction(130);
							}
						}
						else
						{
							this.tx = this.playerpos.X;
							this.tz = this.playerpos.Z;
							if (this.distanceCutty < 50f && this.playerHealth > 105f)
							{
								this.cuttyAssPush = true;
							}
							if (this.homing != 12 && num5 < 10000f && this.animClip != 3)
							{
								text = "bite";
							}
						}
					}
				}
				this.animManager(text);
				if (num5 < 2500f && this.animClip != 3)
				{
					this.cuttyMouthPos = new Vector2(vector.X, vector.Z) - new Vector2(this.playerpos.X, this.playerpos.Z);
					this.cuttyMouthCollide = true;
				}
				if (num5 < 22500f && this.animClip == 3)
				{
					this.cuttyMouthPos = new Vector2(vector.X, vector.Z) - new Vector2(this.playerpos.X, this.playerpos.Z);
					this.cuttyMouthCollide = true;
				}
				num2 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.tz), (double)(this.cuttyPos.X - this.tx)) - 1.57f;
				float num7 = Cutty.WrapAngle(num2 - this.cuttyRot);
				float num8 = 0.065f;
				if (this.homing > 0)
				{
					num8 = 0.065f;
					num7 = MathHelper.Clamp(num7, -num8, num8);
					this.cuttyRot = Cutty.WrapAngle(this.cuttyRot + num7);
				}
				if (this.attack1)
				{
					num8 = 0.06f;
					if (this.attackLand)
					{
						num8 = 0.004f;
					}
					num7 = MathHelper.Clamp(num7, -num8, num8);
					this.cuttyRot = Cutty.WrapAngle(this.cuttyRot + num7);
				}
				if (this.death1)
				{
					num8 = 0f;
					if (this.cuttyDyingWalk)
					{
						num7 = MathHelper.Clamp(num7, -0.1f, 0.1f);
						this.cuttyRot = Cutty.WrapAngle(this.cuttyRot + num7);
					}
				}
				if (this.cuttyHeal)
				{
					num8 = 0.037f;
					if (this.healWait1 <= 0f)
					{
						num7 = Cutty.WrapAngle(this.cuttyHealrot - this.cuttyRot);
						num8 = 0.01f;
					}
					if (this.healWait2 <= 0f)
					{
						num7 = Cutty.WrapAngle(this.cuttyHealrot - this.cuttyRot);
						num8 = 0.02f;
					}
					num7 = MathHelper.Clamp(num7, -num8, num8);
					this.cuttyRot = Cutty.WrapAngle(this.cuttyRot + num7);
				}
				if (!this.death1 && !this.cuttyHeal && !this.attack1 && this.homing == 0)
				{
					num7 = MathHelper.Clamp(num7, -num8, num8);
					this.cuttyRot = Cutty.WrapAngle(this.cuttyRot + num7);
				}
				this.cuttyPos.X = this.cuttyPos.X + -(float)Math.Cos((double)(this.cuttyRot + 1.57f)) * this.cuttyRate;
				this.cuttyPos.Z = this.cuttyPos.Z + (float)Math.Sin((double)(this.cuttyRot + 1.57f)) * this.cuttyRate;
			}
			else
			{
				this.glowWait--;
				if (this.glowWait < 0)
				{
					this.eyeGlow -= 0.001f;
					if (this.eyeGlow < 0f)
					{
						this.eyeGlow = 0f;
					}
				}
			}
			if (this.faceseizureTimer > 0)
			{
				this.faceseizureTimer--;
				if (this.faceseizureTimer % 2 == 0)
				{
					Math.Cos((double)(this.sc.myTimer / 26f));
					float num9 = 380f + (float)Math.Sin((double)(this.sc.myTimer / 34f)) * 220f;
					float num10 = 480f + (float)Math.Cos((double)(this.sc.myTimer / 24f)) * 200f;
					int num11 = 13;
					int num12 = 15;
					this.a.boneTransforms[7] = Matrix.CreateRotationY((float)this.rr.Next(-num12, num12) / num10) * this.a.boneTransforms[7];
					this.a.boneTransforms[7] = Matrix.CreateRotationZ((float)this.rr.Next(-num11, num11) / num9) * this.a.boneTransforms[7];
				}
			}
			if (this.seizureTimer > 0)
			{
				this.seizureTimer--;
				float num13 = 350f + (float)Math.Cos((double)(this.sc.myTimer / 26f)) * 300f;
				float num14 = 350f + (float)Math.Sin((double)(this.sc.myTimer / 34f)) * 300f;
				float num15 = 480f + (float)Math.Cos((double)(this.sc.myTimer / 24f)) * 200f;
				int num16 = 10;
				int num17 = 20;
				this.a.boneTransforms[5] = Matrix.CreateRotationZ((float)this.rr.Next(-num16, num16) / num15) * this.a.boneTransforms[5];
				this.a.boneTransforms[5] = Matrix.CreateRotationX((float)this.rr.Next(-num16, num16) / num15) * this.a.boneTransforms[5];
				this.a.boneTransforms[12] = Matrix.CreateRotationX((float)this.rr.Next(-num17, num17) / num15) * this.a.boneTransforms[12];
				this.a.boneTransforms[12] = Matrix.CreateRotationY((float)this.rr.Next(-num17, num17) / num13) * this.a.boneTransforms[12];
				this.a.boneTransforms[13] = Matrix.CreateRotationX((float)this.rr.Next(-num16, num16) / num14) * this.a.boneTransforms[13];
				this.a.boneTransforms[13] = Matrix.CreateRotationY((float)this.rr.Next(-num17, num17) / num13) * this.a.boneTransforms[13];
				this.a.boneTransforms[16] = Matrix.CreateRotationX((float)this.rr.Next(-num16, num16) / num13) * this.a.boneTransforms[16];
				this.a.boneTransforms[16] = Matrix.CreateRotationY((float)this.rr.Next(-num16, num16) / num13) * this.a.boneTransforms[16];
				this.a.boneTransforms[17] = Matrix.CreateRotationX((float)this.rr.Next(-num17, num17) / num14) * this.a.boneTransforms[17];
				this.a.boneTransforms[17] = Matrix.CreateRotationY((float)this.rr.Next(-num17, num17) / num13) * this.a.boneTransforms[17];
				this.a.boneTransforms[7] = Matrix.CreateRotationY((float)this.rr.Next(-num17, num17) / num15) * this.a.boneTransforms[7];
				this.a.boneTransforms[20] = Matrix.CreateRotationX((float)this.rr.Next(-num17, num17) / num13) * this.a.boneTransforms[20];
				this.a.boneTransforms[20] = Matrix.CreateRotationY((float)this.rr.Next(-num16, num16) / num14) * this.a.boneTransforms[20];
				this.a.boneTransforms[25] = Matrix.CreateRotationX((float)this.rr.Next(-num17, num17) / num15) * this.a.boneTransforms[25];
				this.a.boneTransforms[25] = Matrix.CreateRotationY((float)this.rr.Next(-num16, num16) / num13) * this.a.boneTransforms[25];
			}
			if (!this.attack1 && !this.cuttyHeal)
			{
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out this.cuttyPos.Y);
			}
			this.cuttyTrans = Matrix.CreateScale(this.cuttyScale) * Matrix.CreateRotationY(this.cuttyRot) * Matrix.CreateTranslation(this.cuttyPos);
			this.a.UpdateWorldTransforms(this.cuttyTrans, this.a.boneTransforms);
			this.a.skinTransforms.CopyTo(this.cuttyBone, 0);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x002675BC File Offset: 0x002657BC
		private void pigClipControl()
		{
			this.myClip = 0;
			float num = Vector2.Distance(new Vector2(this.tx, this.tz), new Vector2(this.cuttyPos.X, this.cuttyPos.Z));
			if (this.homing > 0)
			{
				num += 200f;
			}
			float num2 = 0f;
			if (num >= 220f && num < 450f)
			{
				if (this.targetRate > 0f)
				{
					num2 = this.targetRate;
				}
				else
				{
					num2 = 2f;
				}
			}
			else if (num >= 450f && num < 800f)
			{
				if (this.targetRate > 0f)
				{
					num2 = this.targetRate * 1.2f;
				}
				else
				{
					num2 = 3f;
				}
			}
			else if (num >= 800f)
			{
				if (this.targetRate > 5f)
				{
					num2 = this.targetRate * 1.3f;
				}
				else
				{
					num2 = 10f;
				}
			}
			if (this.cuttyRate > num2)
			{
				this.cuttyRate -= 0.2f;
			}
			else if (this.cuttyRate < num2)
			{
				this.cuttyRate += 0.1f;
			}
			if (this.cuttyRate < 0f)
			{
				this.cuttyRate = 0f;
			}
			if (this.cuttyRate <= 0f)
			{
				this.myClip = 2;
			}
			if (this.cuttyRate > 0f && this.cuttyRate <= this.clipRate[1] * 2f)
			{
				this.myClip = 1;
			}
			if (this.cuttyRate > 5f)
			{
				this.myClip = 0;
			}
			this.fps = (this.cuttyRate + 1f) / (this.clipRate[this.clipIndexA] + 1f);
			this.pigFrame1 += this.fps;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00267780 File Offset: 0x00265980
		private void pigDying()
		{
			this.homing = 0;
			this.attack1 = false;
			if (this.cuttyDyingWalk)
			{
				this.myClip = 0;
				this.cuttyDeathTimer -= 1f;
				if (this.cuttyDeathTimer <= 0f)
				{
					this.cuttyPos.X = this.cuttyDeadx;
					this.cuttyPos.Z = this.cuttyDeadz;
				}
				this.tx = this.cuttyDeadx;
				this.tz = this.cuttyDeadz;
				float num = Vector2.Distance(new Vector2(this.tx, this.tz), new Vector2(this.cuttyPos.X, this.cuttyPos.Z));
				this.targetRate = 0f;
				float num2 = 0f;
				if (num >= 20f && num < 250f)
				{
					num2 = 2f;
				}
				else if (num >= 250f)
				{
					num2 = 8f;
				}
				if (this.cuttyRate > num2)
				{
					this.cuttyRate -= 0.2f;
				}
				else if (this.cuttyRate < num2)
				{
					this.cuttyRate += 0.1f;
				}
				if (this.cuttyRate < 0f)
				{
					this.cuttyRate = 0f;
				}
				if (this.cuttyRate <= 0f)
				{
					this.myClip = 2;
				}
				if (this.cuttyRate > 0f && this.cuttyRate <= this.clipRate[1] * 2f)
				{
					this.myClip = 1;
				}
				if (this.cuttyRate > 5f)
				{
					this.myClip = 0;
				}
				this.fps = (this.cuttyRate + 1f) / (this.clipRate[this.clipIndexA] + 1f);
				this.pigFrame1 += this.fps;
				if (this.myClip == 2 && num <= 20f && this.tween >= 1f)
				{
					this.cuttyDyingWalk = false;
					return;
				}
			}
			else if (!this.cuttyDyingWalk)
			{
				this.myClip = 4;
				this.cuttyRate = 0f;
				if (this.clipIndexA != 4)
				{
					this.sc.dieyell.Play(this.sc.ev, 0f, 0f);
					this.myClip = 4;
					this.pigFrame1 = 0f;
					if (!this.assDestroyed)
					{
						this.assSwitch();
					}
					else if (!this.faceDestroyed)
					{
						this.faceSwitch();
					}
					else if (!this.spineDestroyed)
					{
						this.spineSwitch();
					}
				}
				if (!this.cuttyisDead)
				{
					this.fps = 0.4f;
					this.pigFrame1 += this.fps;
					if (this.pigFrame1 >= 121f)
					{
						this.fireTimer = 1200;
						this.onFire = true;
						this.cuttyisDead = true;
						this.pigFrame1 = 121f;
						if (!this.assDestroyed)
						{
							this.assSwitch();
						}
						else if (!this.faceDestroyed)
						{
							this.faceSwitch();
						}
						else if (!this.spineDestroyed)
						{
							this.spineSwitch();
						}
					}
				}
				float num3 = Cutty.WrapAngle(this.cuttyDeadrot - this.cuttyRot);
				num3 = MathHelper.Clamp(num3, -0.06f, 0.06f);
				this.cuttyRot = Cutty.WrapAngle(this.cuttyRot + num3);
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00267AC4 File Offset: 0x00265CC4
		private void pigHealing(ref float[,] heights)
		{
			this.myClip = 5;
			this.fps = 0f;
			this.healWait1 -= 1f;
			if (this.healWait1 <= 0f)
			{
				this.healWait1 = 0f;
				this.healtimer += 1f;
				if (this.healtimer > this.healduration)
				{
					this.healtimer = this.healduration + 5f;
					this.healWait2 -= 1f;
				}
			}
			if (this.healWait1 > 0f)
			{
				this.fps = 0.4f;
				float num;
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out num);
				this.cuttyPos.Y = num;
			}
			if (this.healWait2 > 0f && this.healWait1 <= 0f && this.healtimer <= this.healduration)
			{
				float num2 = this.healtimer / this.healduration;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				this.cuttyPos.X = MathHelper.Lerp(this.healorigx, this.healdestx, num2);
				this.cuttyPos.Z = MathHelper.Lerp(this.healorigz, this.healdestz, num2);
				this.upForce -= this.grav;
				this.cuttyPos.Y = this.cuttyPos.Y + this.upForce;
				float num3;
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out num3);
				if (this.cuttyPos.Y <= num3)
				{
					this.cuttyPos.Y = num3 - 10f;
				}
				else if (this.healtimer == this.healduration)
				{
					this.healtimer = this.healduration - 1f;
				}
				this.fps = 0.4f;
				if (this.pigFrame1 > 54f)
				{
					this.pigFrame1 = 54f;
				}
			}
			if (this.healWait2 > 0f && this.healtimer > this.healduration)
			{
				if (this.healWait2 == 55f)
				{
					this.sc.piledriver.Play(this.sc.ev, (float)this.rr.Next(-20, 20) / 100f, 0f);
				}
				if (this.pigFrame1 < 54f)
				{
					this.pigFrame1 = 54f;
				}
				float num4;
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out num4);
				this.cuttyPos.Y = num4 - 10f;
				this.fps = 0.4f;
			}
			this.tx = this.healdestx;
			this.tz = this.healdestz;
			this.homing = 0;
			this.cuttyRate = 0f;
			this.targetRate = 0f;
			if (this.healWait2 <= 0f)
			{
				this.healChant -= 1f;
				if (this.spectator && this.healChant % 4f == 0f)
				{
					this.health += 1;
				}
				if (!this.spectator && this.healChant % 2f == 0f)
				{
					this.health += 1;
				}
				if (this.health > this.startHealth)
				{
					this.health = this.startHealth;
				}
				if (this.healChant == 700f)
				{
					this.talkIndex = 3;
					Cutty.whichPigTalks = this.myIndex;
				}
				if (this.healChant == 400f)
				{
					this.talkIndex = 4;
					Cutty.whichPigTalks = this.myIndex;
				}
				this.energize(this.cuttyPos);
				this.myClip = 6;
				this.fps = 0.4f;
				if (this.healChant <= 0f)
				{
					this.cuttyHeal = false;
					this.glowTimer = 0f;
					this.talkIndex = 11;
					if (this.quips.Count > 0 && this.myIndex == 0)
					{
						int num5 = this.rr.Next(0, this.quips.Count);
						this.talkIndex = this.quips[num5];
						this.quips.RemoveAt(num5);
					}
					Cutty.whichPigTalks = this.myIndex;
					Cutty.gonnaHealindex = -1;
					this.gonnaHealDelay = 600f;
					this.newAction = false;
				}
			}
			this.pigFrame1 += this.fps;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00267F60 File Offset: 0x00266160
		private void pigAttack(ref float[,] heights)
		{
			this.myClip = 5;
			this.attackLand = false;
			this.fps = 0f;
			this.attackWait1 -= 1f;
			if (this.attackWait1 <= 0f)
			{
				this.attackWait1 = 0f;
				this.attacktimer += 1f;
				if (this.attacktimer > this.attackduration)
				{
					this.attackLand = true;
					this.attacktimer = this.attackduration + 5f;
					this.attackWait2 -= 1f;
				}
			}
			if (this.attackWait1 > 0f)
			{
				this.fps = 0.4f;
				float num;
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out num);
				this.cuttyPos.Y = num;
			}
			if (this.attackWait2 > 0f && this.attackWait1 <= 0f && this.attacktimer <= this.attackduration)
			{
				float num2 = this.attacktimer / this.attackduration;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				this.cuttyPos.X = MathHelper.Lerp(this.origx, this.destx, num2);
				this.cuttyPos.Z = MathHelper.Lerp(this.origz, this.destz, num2);
				this.upForce -= this.grav;
				this.cuttyPos.Y = this.cuttyPos.Y + this.upForce;
				float num3;
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out num3);
				if (this.cuttyPos.Y <= num3)
				{
					this.cuttyPos.Y = num3;
				}
				else if (this.attacktimer >= this.attackduration - 1f)
				{
					this.attacktimer -= 2f;
				}
				this.fps = 0.4f;
				if (this.pigFrame1 > 54f)
				{
					this.pigFrame1 = 54f;
				}
			}
			if (this.attackWait2 > 0f && this.attacktimer > this.attackduration)
			{
				if (this.attackWait2 == 55f)
				{
					this.sc.cuttyWave.Play(this.sc.ev, (float)this.rr.Next(-10, 10) / 100f, 0f);
					this.sc.piledriver.Play(this.sc.ev, (float)this.rr.Next(-20, 20) / 100f, 0f);
					this.shockTimer = 65f;
					this.shockRadius = 80f;
					this.shockHit = false;
					this.shockHasHit = false;
				}
				if (this.pigFrame1 < 54f)
				{
					this.pigFrame1 = 54f;
				}
				float num4;
				Cutty.GetHeightFast(ref heights, new Vector2(this.cuttyPos.X, this.cuttyPos.Z), out num4);
				this.cuttyPos.Y = num4;
				this.fps = 0.4f;
			}
			if (this.attackWait2 <= 0f)
			{
				this.attack1 = false;
				int num5 = 100 + Cutty.cuttyCount * 150;
				int num6 = Cutty.cuttyCount * 320;
				this.nextAttack = this.rr.Next(num5, num6);
				this.newAction = false;
			}
			this.tx = this.destx;
			this.tz = this.destz;
			this.homing = 0;
			this.targetWait = 100;
			this.cuttyRate = 0f;
			this.targetRate = 0f;
			this.pigFrame1 += this.fps;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00268330 File Offset: 0x00266530
		public void endHealing()
		{
			if (this.cuttyHeal)
			{
				this.homingCount = 0;
				this.targetWait = this.rr.Next(270, 500);
				this.cuttyStruct.homing = 0;
				this.cuttyStruct.curveIndex = this.curveIndex;
				this.cuttyStruct.targetRate = (float)this.rr.Next(90, 130) / 100f * 8f;
				this.cuttyStruct.loop = this.loop;
				this.cuttyStruct.animType = 0;
				this.cuttyStruct.talkindex = this.rr.Next(9, 12);
				this.scheduleAction(130);
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x002683F4 File Offset: 0x002665F4
		public void host_SendData(ref packetSender packetWriter, int timeFrame)
		{
			if (this.cuttyStruct_send.flag == 130)
			{
				packetWriter.Write(130);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write((byte)this.cuttyStruct_send.homing);
				packetWriter.Write(this.cuttyStruct_send.targetRate);
				packetWriter.Write((byte)this.cuttyStruct_send.curveIndex);
				packetWriter.Write(this.cuttyStruct_send.loop);
				packetWriter.Write((byte)this.cuttyStruct_send.animType);
				if (this.cuttyStruct_send.talkindex > -1)
				{
					packetWriter.Write((byte)this.cuttyStruct_send.talkindex);
				}
				else
				{
					packetWriter.Write(150);
				}
			}
			if (this.cuttyStruct_send.flag == 131)
			{
				packetWriter.Write(131);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.destx);
				packetWriter.Write(this.cuttyStruct_send.destz);
			}
			if (this.cuttyStruct_send.flag == 132)
			{
				packetWriter.Write(132);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.destx);
				packetWriter.Write(this.cuttyStruct_send.destz);
				packetWriter.Write(this.cuttyStruct_send.rot);
			}
			if (this.cuttyStruct_send.flag == 133)
			{
				packetWriter.Write(133);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.destx);
				packetWriter.Write(this.cuttyStruct_send.destz);
				packetWriter.Write(this.cuttyStruct_send.rot);
			}
			this.actionScheduled = false;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00268610 File Offset: 0x00266810
		private void scheduleAction(int flag)
		{
			this.delay = 0f;
			this.delay = (float)Math.Round((double)(this.delay * 0.06f));
			this.timeDelay = this.timeframe + (int)this.delay;
			if (this.timeDelay <= this.lasttimeDelay && this.lasttimeDelay - this.timeDelay < 60)
			{
				this.timeDelay = this.lasttimeDelay + 1;
			}
			this.lasttimeDelay = this.timeDelay;
			this.timeDelay = this.timeframe;
			if (flag == 130)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.homing = this.cuttyStruct.homing;
				this.cuttyStruct_send.curveIndex = this.cuttyStruct.curveIndex;
				this.cuttyStruct_send.targetRate = this.cuttyStruct.targetRate;
				this.cuttyStruct_send.loop = this.cuttyStruct.loop;
				this.cuttyStruct_send.animType = this.cuttyStruct.animType;
				this.cuttyStruct_send.talkindex = this.cuttyStruct.talkindex;
			}
			if (flag == 131)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.destx = this.cuttyStruct.destx;
				this.cuttyStruct_send.destz = this.cuttyStruct.destz;
			}
			if (flag == 132)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.destx = this.cuttyStruct.destx;
				this.cuttyStruct_send.destz = this.cuttyStruct.destz;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
			}
			if (flag == 133)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.destx = this.cuttyStruct.destx;
				this.cuttyStruct_send.destz = this.cuttyStruct.destz;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
			}
			this.scheduleList.Add(this.cuttyStruct_send);
			this.actionScheduled = true;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x002688C0 File Offset: 0x00266AC0
		private void runScheduler(ref float[,] heights)
		{
			for (int i = 0; i < this.scheduleList.Count; i++)
			{
				this.tempConduct = this.scheduleList[i];
				if (this.timeframe >= this.tempConduct.time || Math.Abs(this.tempConduct.time - this.timeframe) > 120)
				{
					this.cuttyAction(ref this.tempConduct, true, ref heights);
					this.scheduleList.RemoveAt(i);
				}
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00268940 File Offset: 0x00266B40
		public void cuttyAction(ref Cutty.conductor tempy, bool isLocal, ref float[,] heights)
		{
			if (tempy.flag == 130)
			{
				if (this.cuttyHeal)
				{
					this.cuttyHeal = false;
					this.glowTimer = 0f;
					Cutty.gonnaHealindex = -1;
					this.gonnaHealDelay = 600f;
					this.newAction = false;
				}
				this.homing = tempy.homing;
				this.curveIndex = tempy.curveIndex;
				this.targetRate = tempy.targetRate;
				this.loop = tempy.loop;
				if (tempy.animType != 0)
				{
					if (tempy.animType == 1)
					{
						this.animManager("bite");
					}
					if (tempy.animType == 2)
					{
						this.animManager("bite2");
					}
				}
				if (tempy.talkindex > -1)
				{
					this.talkIndex = tempy.talkindex;
					Cutty.whichPigTalks = this.myIndex;
				}
				else
				{
					Cutty.cuttyDoneSpeech = true;
					this.cuttyDoneLocalSpeech = true;
				}
			}
			if (tempy.flag == 131)
			{
				this.newAction = true;
				this.attack1 = true;
				this.homing = 0;
				this.pigFrame1 = 0f;
				this.attacktimer = 0f;
				this.attackWait1 = 42.5f;
				this.attackWait2 = 62f;
				this.origx = this.cuttyPos.X;
				this.origy = this.cuttyPos.Y;
				this.origz = this.cuttyPos.Z;
				this.destx = tempy.destx;
				this.destz = tempy.destz;
				Cutty.GetHeightFast(ref heights, new Vector2(this.destx, this.destz), out this.desty);
				this.attackduration = (float)tempy.dur;
				this.attackduration = MathHelper.Lerp(70f, 90f, (this.attackduration - 300f) / 900f);
				this.grav = 0.55f;
				if (this.desty > this.origy && this.desty > 50f)
				{
					this.grav += MathHelper.Lerp(0f, 0.7f, (this.desty - this.origy) / 500f);
				}
				this.upForce = this.grav * (this.attackduration / 2f);
				if (this.desty > this.origy && this.desty > 50f)
				{
					this.attackduration -= MathHelper.Lerp(0f, 15f, (this.desty - this.origy) / 500f);
				}
			}
			if (tempy.flag == 132)
			{
				this.newAction = true;
				this.death1 = true;
				this.cuttyisDead = false;
				this.cuttyDeathTimer = 500f;
				this.health = 0;
				this.homing = 0;
				if (Cutty.homingLocal == this.myIndex)
				{
					Cutty.homingLocal = -1;
				}
				if (Cutty.homingRemote == this.myIndex)
				{
					Cutty.homingRemote = -1;
				}
				this.cuttyDyingWalk = true;
				this.cuttyDeadrot = tempy.rot;
				this.cuttyDeadx = tempy.destx;
				this.cuttyDeadz = tempy.destz;
			}
			if (tempy.flag == 133)
			{
				this.newAction = true;
				this.pigFrame1 = 0f;
				this.homing = 0;
				this.cuttyHeal = true;
				Cutty.gonnaHealindex = this.myIndex;
				this.glowTimer = 1f;
				this.healtimer = 0f;
				this.healWait1 = 42.5f;
				this.healWait2 = 62f;
				this.healChant = 800f;
				this.healorigx = this.cuttyPos.X;
				this.healorigy = this.cuttyPos.Y;
				this.healorigz = this.cuttyPos.Z;
				this.healdestx = tempy.destx;
				this.healdestz = tempy.destz;
				this.cuttyHealrot = tempy.rot;
				Cutty.GetHeightFast(ref heights, new Vector2(this.healdestx, this.healdestz), out this.healdesty);
				this.healduration = (float)tempy.dur;
				this.healduration = MathHelper.Clamp(this.healduration / 10f, 70f, 110f);
				this.grav = 0.55f;
				if (this.healdesty > this.healorigy && this.healdesty > 50f)
				{
					this.grav += MathHelper.Lerp(0f, 0.7f, (this.healdesty - this.healorigy) / 500f);
				}
				this.upForce = this.grav * (this.healduration / 2f);
				if (this.healdesty > this.healorigy && this.healdesty > 50f)
				{
					this.healduration -= MathHelper.Lerp(0f, 15f, (this.healdesty - this.healorigy) / 500f);
				}
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00268E1B File Offset: 0x0026701B
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

		// Token: 0x060009D0 RID: 2512 RVA: 0x00268E44 File Offset: 0x00267044
		private void initTarget()
		{
			this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget1);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget2);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.pigSkin.Parameters["BloodTexture"].SetValue(this.screenTarget1);
			this.targetChoice = 1;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00268EF0 File Offset: 0x002670F0
		public void addTargetBlood(int index, int xmin, int xmax, int ymin, int ymax)
		{
			int num = (int)((float)xmin * 0.585f);
			int num2 = (int)((float)ymin * 0.585f);
			int num3 = (int)((float)xmax * 0.585f);
			int num4 = (int)((float)ymax * 0.585f);
			Rectangle rectangle = this.sc.woundRect[index];
			Color color = new Color(255, 255, 255, 255) * ((float)this.rr.Next(40, 100) / 100f);
			color.A = byte.MaxValue;
			if (this.targetChoice == 2)
			{
				this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget1);
				this.sc.GraphicsDevice.Clear(Color.Transparent);
				this.spriteBatch.Begin();
				this.spriteBatch.Draw(this.screenTarget2, Vector2.Zero, Color.White);
				this.spriteBatch.Draw(this.sc.wound, new Vector2((float)this.rr.Next(num, num3), (float)this.rr.Next(num2, num4)), new Rectangle?(rectangle), color, (float)this.rr.Next(-800, 800) / 100f, new Vector2(32f, 32f), (float)this.rr.Next(80, 100) / 100f, SpriteEffects.None, 0f);
				this.spriteBatch.End();
				this.sc.GraphicsDevice.SetRenderTarget(null);
				this.pigSkin.Parameters["BloodTexture"].SetValue(this.screenTarget1);
				this.targetChoice = 1;
				return;
			}
			this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget2);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.spriteBatch.Begin();
			this.spriteBatch.Draw(this.screenTarget1, Vector2.Zero, Color.White);
			this.spriteBatch.Draw(this.sc.wound, new Vector2((float)this.rr.Next(num, num3), (float)this.rr.Next(num2, num4)), new Rectangle?(rectangle), color, (float)this.rr.Next(-800, 800) / 100f, new Vector2(32f, 32f), (float)this.rr.Next(80, 100) / 100f, SpriteEffects.None, 0f);
			this.spriteBatch.End();
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.pigSkin.Parameters["BloodTexture"].SetValue(this.screenTarget2);
			this.targetChoice = 2;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x002691C4 File Offset: 0x002673C4
		public void Draw(Vector3 campos, Matrix view, Matrix proj, bool showSphere, ref Texture2D ttworld, Matrix viewWorld, int tech)
		{
			this.view = view;
			this.proj = proj;
			if (this.chunk.startDrop)
			{
				this.DrawInstance(this.chunk, "fastShader");
			}
			if (this.faceChunk.startDrop)
			{
				this.DrawInstance(this.faceChunk, "fastShader");
			}
			if (this.assChunk.startDrop)
			{
				this.DrawInstance(this.assChunk, "fastShader");
			}
			this.drawCutty(ref ttworld, viewWorld, tech);
			if (this.showSkelTimer > 0)
			{
				this.showSkelTimer--;
				if (this.showSkelTimer < 0)
				{
					this.showSkelTimer = 0;
				}
				if (this.showSkelTimer % 5 == 0)
				{
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.None;
					this.sc.GraphicsDevice.BlendState = BlendState.Additive;
					this.drawCuttySkel();
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
					this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x002692D3 File Offset: 0x002674D3
		public void DrawFireBall()
		{
			this.DrawGrenadeExplosion(ref this.fireball, Matrix.Identity);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x002692E8 File Offset: 0x002674E8
		public void DrawEyes(Vector3 campos)
		{
			float num = 0.9f;
			if (this.cuttyHeal)
			{
				num = 0.4f + Math.Abs((float)(Math.Sin((double)(this.sc.myTimer / 14f)) * 1.600000023841858));
			}
			Matrix matrix = Matrix.CreateTranslation(26.98f, 101f, 167.34f) * this.cuttyBone[7];
			Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
			vector += Vector3.Normalize(campos - vector) * 12f;
			Matrix matrix2 = Matrix.CreateScale(num * this.eyeGlow) * Matrix.CreateBillboard(vector, campos, this.view.Up, new Vector3?(this.view.Forward));
			this.drawEye(matrix2);
			matrix = Matrix.CreateTranslation(-35.36717f, 101f, 167.34f) * this.cuttyBone[7];
			vector = Vector3.Transform(Vector3.Zero, matrix);
			vector += Vector3.Normalize(campos - vector) * 12f;
			matrix2 = Matrix.CreateScale(num * this.eyeGlow) * Matrix.CreateBillboard(vector, campos, this.view.Up, new Vector3?(this.view.Forward));
			this.drawEye(matrix2);
			this.sparks.SetCamera(this.view, this.proj);
			this.sparks.Draw(0);
			this.dots.SetCamera(this.view, this.proj);
			this.dots.Draw(0);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00269498 File Offset: 0x00267698
		private void DrawModel(Model model, Matrix world)
		{
			Vector3 vector = new Vector3(0.52f, 0.52f, 0.52f);
			Vector3 vector2 = new Vector3(0.85f, 0.85f, 0.85f);
			foreach (Effect effect in model.Meshes[0].Effects)
			{
				BasicEffect basicEffect = (BasicEffect)effect;
				basicEffect.World = world;
				basicEffect.View = this.view;
				basicEffect.Projection = this.proj;
				basicEffect.LightingEnabled = true;
				basicEffect.DirectionalLight0.Enabled = true;
				basicEffect.DirectionalLight0.Direction = new Vector3(0.2f, -0.5f, 0.1f);
				basicEffect.AmbientLightColor = vector2;
				basicEffect.DirectionalLight0.DiffuseColor = vector;
				basicEffect.PreferPerPixelLighting = false;
			}
			model.Meshes[0].Draw();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x002695A0 File Offset: 0x002677A0
		private void drawCutty(ref Texture2D ttWorld, Matrix viewWorld, int tech)
		{
			float num = 0f;
			this.pigModel.Meshes[this.pigIndex].MeshParts[0].Effect = this.pigSkin;
			this.pigSkin.Parameters["darkness"].SetValue(MathHelper.Lerp(0f, 1.2f, this.sc.darkness));
			this.pigSkin.Parameters["gDiffuse"].SetValue(ttWorld);
			this.pigSkin.Parameters["projectorView"].SetValue(viewWorld);
			if (this.glowTimer > 0f)
			{
				float num2 = (float)Math.Abs(Math.Sin((double)(this.sc.myTimer / 24f)) * 1.5);
				num = (float)Math.Sin((double)(this.sc.myTimer / 8f)) / 4f;
				this.pigSkin.Parameters["glow"].SetValue(0.2f + num2);
				this.pigSkin.Parameters["slide"].SetValue(this.sc.myTimer * 0.003f);
				this.pigSkin.Parameters["darkness"].SetValue(MathHelper.Lerp(0.4f, 1.2f, this.sc.darkness));
				tech = 5;
			}
			this.pigSkin.Parameters["eyeglow"].SetValue(this.eyeGlow + num);
			this.pigSkin.Parameters["amb"].SetValue(new Vector3(0.8f, 0.8f, 0.8f));
			this.pigSkin.Parameters["diff"].SetValue(new Vector3(0.7f, 0.7f, 0.7f));
			this.pigSkin.Parameters["LightDirection"].SetValue(new Vector3(0.7f, -0.7f, 0f));
			this.pigSkin.Parameters["View"].SetValue(this.view);
			this.pigSkin.Parameters["Projection"].SetValue(this.proj);
			this.pigSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
			this.pigSkin.CurrentTechnique = this.pigSkin.Techniques[tech];
			this.pigModel.Meshes[this.pigIndex].Draw();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0026986C File Offset: 0x00267A6C
		public void drawCutty_Glow()
		{
			Vector3 vector = new Vector3(0.3f, 0.3f, 0.6f) * (float)(Math.Sin((double)(this.sc.myTimer / 50f)) / 2.0 + 0.5);
			this.pigModel.Meshes[this.pigIndex].MeshParts[0].Effect = this.solidSkin;
			this.solidSkin.Parameters["View"].SetValue(this.view);
			this.solidSkin.Parameters["Projection"].SetValue(this.proj);
			this.solidSkin.Parameters["color"].SetValue(vector);
			this.solidSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
			this.solidSkin.CurrentTechnique = this.solidSkin.Techniques["SkinnedEffect"];
			this.pigModel.Meshes[this.pigIndex].Draw();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x002699A8 File Offset: 0x00267BA8
		public void spiralGlow()
		{
			float num = 15f;
			Vector2[] array = new Vector2[24];
			for (int i = 0; i < 24; i++)
			{
				float num2 = (float)(-(float)i);
				array[i] = new Vector2((float)Math.Sin((double)MathHelper.ToRadians(num2 * num)), -1.4f * (float)Math.Cos((double)MathHelper.ToRadians(180f + num2 * num)));
			}
			this.glowEffect.Parameters["offsets"].SetValue(array);
			this.glowEffect.Parameters["glowdist"].SetValue(0.005f);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00269A4C File Offset: 0x00267C4C
		private void drawCuttySkel()
		{
			this.sc.pigAll.Meshes[0].MeshParts[0].Effect = this.pigSkin;
			this.pigSkin.Parameters["View"].SetValue(this.view);
			this.pigSkin.Parameters["Projection"].SetValue(this.proj);
			this.pigSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
			this.pigSkin.CurrentTechnique = this.pigSkin.Techniques[3];
			this.sc.pigAll.Meshes[0].Draw();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00269B24 File Offset: 0x00267D24
		private void drawEye(Matrix world)
		{
			float num = this.eyeGlow;
			if (this.eyeGlow < 0.7f)
			{
				if ((float)this.rr.Next(1, 1000) < 1000f / this.eyeGlow)
				{
					num += (float)this.rr.Next(-100, 100) / 500f;
				}
				if (num < 0f)
				{
					num = 0f;
				}
			}
			this.eyeglow.Meshes[0].MeshParts[0].Effect = this.eyeEffect;
			this.eyeEffect.Parameters["darkness"].SetValue(num);
			this.eyeEffect.Parameters["View"].SetValue(this.view);
			this.eyeEffect.Parameters["Projection"].SetValue(this.proj);
			this.eyeEffect.Parameters["world"].SetValue(world);
			this.eyeEffect.CurrentTechnique = this.eyeEffect.Techniques[0];
			this.eyeglow.Meshes[0].Draw();
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00269C5C File Offset: 0x00267E5C
		private void shockWave(ref float[,] heights)
		{
			if (this.shockTimer > 10f)
			{
				if (this.rr.Next(1, 100) < 40)
				{
					this.sc.darkness = (float)this.rr.Next(110, 170) / 100f;
				}
				else
				{
					this.sc.darkness = (float)this.rr.Next(5, 50) / 100f;
				}
			}
			if (this.shockTimer == 5f)
			{
				this.sc.darkness = this.olderDarkness;
			}
			this.shockTimer -= 1f;
			this.shockRadius += (float)this.rr.Next(7, 12);
			if (!this.shockHasHit && this.shockRadius < 1000f && Vector3.Distance(this.playerpos, this.cuttyPos) < this.shockRadius)
			{
				this.shockHasHit = true;
				this.shockHit = true;
			}
			Vector3 up = Vector3.Up;
			float num = 2f * this.shockRadius * 3.14f;
			float num2 = 70f / num;
			if (num2 < 0.02f)
			{
				num2 = 0.02f;
			}
			float num3 = (float)(this.rr.Next(1700, 2700) / 100) * ((this.shockTimer + 1f) * 2f);
			for (float num4 = 0f; num4 < 6.28f; num4 += num2)
			{
				Vector3 vector = Vector3.Transform(new Vector3(this.shockRadius, 0f, 0f), Matrix.CreateRotationY(num4));
				vector.X += this.cuttyPos.X + (float)this.rr.Next(-12, 12);
				vector.Z += this.cuttyPos.Z + (float)this.rr.Next(-12, 12);
				Cutty.GetHeightFast(ref heights, new Vector2(vector.X, vector.Z), out vector.Y);
				int num5 = this.rr.Next(1, 3);
				for (int i = 0; i < num5; i++)
				{
					int num6 = this.rr.Next(0, 50);
					Vector3 vector2 = new Vector3((float)this.rr.Next(-num6, num6) / 100f, 0f, (float)this.rr.Next(-num6, num6) / 100f);
					this.sparks.AddParticle(vector + vector2, new Vector3(0f, 1f, 0f) * (float)this.rr.Next(0, (int)num3) / 10f);
				}
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00269F18 File Offset: 0x00268118
		private void energize(Vector3 pos)
		{
			Vector3 vector = new Vector3((float)this.rr.Next(-1000, 1000), (float)this.rr.Next(1000, 2000), (float)this.rr.Next(-1000, 1000));
			Vector3 vector2 = new Vector3((float)this.rr.Next(-100, 100), 0f, (float)this.rr.Next(-100, 100));
			Vector3 vector3 = vector + (pos + vector2);
			this.dots.AddParticle(vector3, -vector / 4f);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00269FC4 File Offset: 0x002681C4
		private void DrawGrenadeExplosion(ref Cutty.hole hole, Matrix world)
		{
			int stainMax = hole.stainMax;
			if (stainMax < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = this.sc.fireballDecal.Meshes[0].MeshParts[0];
			hole.stainBuffer.SetData<Cutty.hitStream>(hole.stainTrans, 0, stainMax, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["premultiply"];
			effect.Parameters["World1"].SetValue(world);
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(hole.stainBuffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, stainMax);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0026A128 File Offset: 0x00268328
		private void DrawInstance(Cutty.shell shell, string tech)
		{
			int tempindex = shell.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = shell.model.Meshes[0].MeshParts[0];
			shell.buffer.SetData<Cutty.instancedObject>(shell.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.Parameters["amb"].SetValue(new Vector3(0.8f, 0.8f, 0.8f));
			effect.Parameters["diff"].SetValue(new Vector3(0.7f, 0.7f, 0.7f));
			effect.Parameters["LightDirection"].SetValue(new Vector3(0.7f, -0.7f, 0f));
			effect.CurrentTechnique = effect.Techniques[tech];
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(shell.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0026A2EC File Offset: 0x002684EC
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height)
		{
			int num = (int)MathHelper.Clamp(position.X / Cutty.unit, 0f, (float)(Cutty.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / Cutty.unit, 0f, (float)(Cutty.bitmap - 2));
			float num3 = position.X % Cutty.unit / Cutty.unit;
			float num4 = position.Y % Cutty.unit / Cutty.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x0400285B RID: 10331
		private int hitcounter;

		// Token: 0x0400285C RID: 10332
		public static int bitmap;

		// Token: 0x0400285D RID: 10333
		public static float unit;

		// Token: 0x0400285E RID: 10334
		public static float Grid;

		// Token: 0x0400285F RID: 10335
		public static float shortestDistance = 20000f;

		// Token: 0x04002860 RID: 10336
		public static byte bit = 0;

		// Token: 0x04002861 RID: 10337
		public static byte uvIndex = 0;

		// Token: 0x04002862 RID: 10338
		public static int xcoord = 0;

		// Token: 0x04002863 RID: 10339
		public static int ycoord = 0;

		// Token: 0x04002864 RID: 10340
		public static int cuttyCount = 0;

		// Token: 0x04002865 RID: 10341
		public static bool allplayersReady = false;

		// Token: 0x04002866 RID: 10342
		public static bool cuttyDoneSpeech = false;

		// Token: 0x04002867 RID: 10343
		public static int homingLocal = -1;

		// Token: 0x04002868 RID: 10344
		public static int homingRemote = -1;

		// Token: 0x04002869 RID: 10345
		public static int speeches = 0;

		// Token: 0x0400286A RID: 10346
		public static int whichPigTalks = 0;

		// Token: 0x0400286B RID: 10347
		public static bool someoneTalking = false;

		// Token: 0x0400286C RID: 10348
		public static int gonnaHealindex = -1;

		// Token: 0x0400286D RID: 10349
		public int df;

		// Token: 0x0400286E RID: 10350
		public int oldDF;

		// Token: 0x0400286F RID: 10351
		private AnimationPlayer a;

		// Token: 0x04002870 RID: 10352
		private AnimationPlayer b;

		// Token: 0x04002871 RID: 10353
		private Matrix[] cuttyBone;

		// Token: 0x04002872 RID: 10354
		private int myClip;

		// Token: 0x04002873 RID: 10355
		private NetworkSession networkSession;

		// Token: 0x04002874 RID: 10356
		public int break1 = 510;

		// Token: 0x04002875 RID: 10357
		public int break2 = 790;

		// Token: 0x04002876 RID: 10358
		public float olderDarkness;

		// Token: 0x04002877 RID: 10359
		private int timeframe;

		// Token: 0x04002878 RID: 10360
		public bool spectator;

		// Token: 0x04002879 RID: 10361
		private float playerHealth;

		// Token: 0x0400287A RID: 10362
		private float remoteHealth;

		// Token: 0x0400287B RID: 10363
		private SoundEffects gallop;

		// Token: 0x0400287C RID: 10364
		private SoundEffects burning;

		// Token: 0x0400287D RID: 10365
		private ParticleSystem sparks;

		// Token: 0x0400287E RID: 10366
		private ParticleSystem dots;

		// Token: 0x0400287F RID: 10367
		public float shockRadius;

		// Token: 0x04002880 RID: 10368
		public float shockTimer;

		// Token: 0x04002881 RID: 10369
		public bool shockHit;

		// Token: 0x04002882 RID: 10370
		public bool shockHasHit;

		// Token: 0x04002883 RID: 10371
		public static StringBuilder nameBuild = new StringBuilder(32, 32);

		// Token: 0x04002884 RID: 10372
		public int myIndex;

		// Token: 0x04002885 RID: 10373
		public int myType;

		// Token: 0x04002886 RID: 10374
		private int bodyState;

		// Token: 0x04002887 RID: 10375
		public bool boneStrike;

		// Token: 0x04002888 RID: 10376
		private int showSkelTimer;

		// Token: 0x04002889 RID: 10377
		public int seizureTimer;

		// Token: 0x0400288A RID: 10378
		private int faceseizureTimer;

		// Token: 0x0400288B RID: 10379
		public float glowTimer;

		// Token: 0x0400288C RID: 10380
		private Vector3 playerpos;

		// Token: 0x0400288D RID: 10381
		private Vector3 remotepos;

		// Token: 0x0400288E RID: 10382
		public bool cuttyMouthCollide;

		// Token: 0x0400288F RID: 10383
		public bool cuttyAssPush;

		// Token: 0x04002890 RID: 10384
		public int cuttyWait;

		// Token: 0x04002891 RID: 10385
		public Vector2 cuttyMouthPos;

		// Token: 0x04002892 RID: 10386
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x04002893 RID: 10387
		private Cutty.hitStream hitstreamTemp = default(Cutty.hitStream);

		// Token: 0x04002894 RID: 10388
		private static VertexDeclaration vd2 = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
			new VertexElement(68, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4)
		});

		// Token: 0x04002895 RID: 10389
		public int fireRamp = 130;

		// Token: 0x04002896 RID: 10390
		public int fireTimer = 500;

		// Token: 0x04002897 RID: 10391
		public bool onFire;

		// Token: 0x04002898 RID: 10392
		private Cutty.hole fireball;

		// Token: 0x04002899 RID: 10393
		private static VertexDeclaration instanceDec = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3)
		});

		// Token: 0x0400289A RID: 10394
		private Cutty.instancedObject[] tempInstance = new Cutty.instancedObject[1];

		// Token: 0x0400289B RID: 10395
		private Cutty.shell chunk;

		// Token: 0x0400289C RID: 10396
		private Cutty.shell faceChunk;

		// Token: 0x0400289D RID: 10397
		private Cutty.shell assChunk;

		// Token: 0x0400289E RID: 10398
		public static RasterizerState wiredOn = new RasterizerState
		{
			FillMode = FillMode.WireFrame
		};

		// Token: 0x0400289F RID: 10399
		public static RasterizerState wiredOff = new RasterizerState
		{
			FillMode = FillMode.Solid
		};

		// Token: 0x040028A0 RID: 10400
		private Curve[] targetX;

		// Token: 0x040028A1 RID: 10401
		private Curve[] targetZ;

		// Token: 0x040028A2 RID: 10402
		private float tx;

		// Token: 0x040028A3 RID: 10403
		private float tz;

		// Token: 0x040028A4 RID: 10404
		public float targetRate;

		// Token: 0x040028A5 RID: 10405
		public int targetWait;

		// Token: 0x040028A6 RID: 10406
		public bool newAction;

		// Token: 0x040028A7 RID: 10407
		public float[] targetDist;

		// Token: 0x040028A8 RID: 10408
		public int curveIndex = 1;

		// Token: 0x040028A9 RID: 10409
		public float loop;

		// Token: 0x040028AA RID: 10410
		public float cuttyDistance = 20000f;

		// Token: 0x040028AB RID: 10411
		public float fps;

		// Token: 0x040028AC RID: 10412
		public ushort assDamage;

		// Token: 0x040028AD RID: 10413
		public ushort faceDamage;

		// Token: 0x040028AE RID: 10414
		public ushort spineDamage;

		// Token: 0x040028AF RID: 10415
		private bool assDestroyed;

		// Token: 0x040028B0 RID: 10416
		private bool spineDestroyed;

		// Token: 0x040028B1 RID: 10417
		private bool faceDestroyed;

		// Token: 0x040028B2 RID: 10418
		public float[] assBreach = new float[] { 40f, 50f, 70f, 60f, 70f, 80f };

		// Token: 0x040028B3 RID: 10419
		public ushort[] assDam = new ushort[] { 12, 10, 8, 9, 5, 5 };

		// Token: 0x040028B4 RID: 10420
		public float[] faceBreach = new float[] { 40f, 50f, 70f, 80f, 100f, 100f };

		// Token: 0x040028B5 RID: 10421
		public ushort[] faceDam = new ushort[] { 14, 10, 8, 8, 5, 5 };

		// Token: 0x040028B6 RID: 10422
		public float[] spineBreach = new float[] { 30f, 40f, 50f, 50f, 60f, 70f };

		// Token: 0x040028B7 RID: 10423
		public ushort[] spineDam = new ushort[] { 14, 10, 8, 8, 5, 5 };

		// Token: 0x040028B8 RID: 10424
		public float delay;

		// Token: 0x040028B9 RID: 10425
		private int timeDelay;

		// Token: 0x040028BA RID: 10426
		private int lasttimeDelay;

		// Token: 0x040028BB RID: 10427
		public bool actionScheduled;

		// Token: 0x040028BC RID: 10428
		public Cutty.conductor tempConduct = default(Cutty.conductor);

		// Token: 0x040028BD RID: 10429
		private Cutty.conductor cuttyStruct = default(Cutty.conductor);

		// Token: 0x040028BE RID: 10430
		private Cutty.conductor cuttyStruct_send = default(Cutty.conductor);

		// Token: 0x040028BF RID: 10431
		private List<Cutty.conductor> scheduleList = new List<Cutty.conductor>();

		// Token: 0x040028C0 RID: 10432
		private int currentKeyframe;

		// Token: 0x040028C1 RID: 10433
		private TimeSpan currentTimeValue;

		// Token: 0x040028C2 RID: 10434
		private ScreenManager sc;

		// Token: 0x040028C3 RID: 10435
		private ContentManager content;

		// Token: 0x040028C4 RID: 10436
		private ContentManager contentB;

		// Token: 0x040028C5 RID: 10437
		private RenderTarget2D screenTarget1;

		// Token: 0x040028C6 RID: 10438
		private RenderTarget2D screenTarget2;

		// Token: 0x040028C7 RID: 10439
		private int targetChoice;

		// Token: 0x040028C8 RID: 10440
		private SpriteBatch spriteBatch;

		// Token: 0x040028C9 RID: 10441
		private int boneHit = -1;

		// Token: 0x040028CA RID: 10442
		private int[] col_Bone = new int[] { 5, 7, 8, 2, 13, 17, 21, 26 };

		// Token: 0x040028CB RID: 10443
		private float[] col_Scale = new float[8];

		// Token: 0x040028CC RID: 10444
		private Vector3[] col_Pos = new Vector3[8];

		// Token: 0x040028CD RID: 10445
		public bool cuttyisHit;

		// Token: 0x040028CE RID: 10446
		public bool cuttyChunkRelease;

		// Token: 0x040028CF RID: 10447
		public Vector3 hitCenter;

		// Token: 0x040028D0 RID: 10448
		public Vector3 hitCenter2;

		// Token: 0x040028D1 RID: 10449
		public Vector3 hitEdge;

		// Token: 0x040028D2 RID: 10450
		public Vector3 hitEdge2;

		// Token: 0x040028D3 RID: 10451
		public string[] pigdialogueName = new string[]
		{
			"entrails", "vomit", "introduction2", "latin1", "latin2", "nohope", "seensmell", "timetodie", "yell1", "roar1",
			"roar2", "roar3", "roar4", "trample1", "trample2", "brethren1", "brethren2", "peace1", "peace2", "spine1",
			"spine2", "sport1", "sport2"
		};

		// Token: 0x040028D4 RID: 10452
		private List<int> quips = new List<int> { 0, 1, 5, 6, 7, 5, 1, 0, 6 };

		// Token: 0x040028D5 RID: 10453
		public int pigLine = -1;

		// Token: 0x040028D6 RID: 10454
		public int pigJawIndex = -1;

		// Token: 0x040028D7 RID: 10455
		public int talkIndex = -1;

		// Token: 0x040028D8 RID: 10456
		public SoundEffects pigDialog1;

		// Token: 0x040028D9 RID: 10457
		private float distanceCutty = 1000f;

		// Token: 0x040028DA RID: 10458
		private float distanceCutty2 = 1000f;

		// Token: 0x040028DB RID: 10459
		private int glowWait = 500;

		// Token: 0x040028DC RID: 10460
		public float eyeGlow = 1f;

		// Token: 0x040028DD RID: 10461
		public bool isSpeaking;

		// Token: 0x040028DE RID: 10462
		public bool isWatching;

		// Token: 0x040028DF RID: 10463
		public bool pigDialog1Loaded;

		// Token: 0x040028E0 RID: 10464
		public bool lookingatPig = true;

		// Token: 0x040028E1 RID: 10465
		public float[] pigJaw;

		// Token: 0x040028E2 RID: 10466
		public float talkSmooth;

		// Token: 0x040028E3 RID: 10467
		public float piglook;

		// Token: 0x040028E4 RID: 10468
		public float pigPush;

		// Token: 0x040028E5 RID: 10469
		public float pigFrame1;

		// Token: 0x040028E6 RID: 10470
		private float animCount = -1f;

		// Token: 0x040028E7 RID: 10471
		public int animClip;

		// Token: 0x040028E8 RID: 10472
		private List<int> animList = new List<int>();

		// Token: 0x040028E9 RID: 10473
		private float animTween;

		// Token: 0x040028EA RID: 10474
		private int animLoop;

		// Token: 0x040028EB RID: 10475
		private int animMin;

		// Token: 0x040028EC RID: 10476
		private int animMax;

		// Token: 0x040028ED RID: 10477
		private bool gonnaBite;

		// Token: 0x040028EE RID: 10478
		private AnimationPlayer[] pig1;

		// Token: 0x040028EF RID: 10479
		private int clipIndexA;

		// Token: 0x040028F0 RID: 10480
		private int clipIndexB;

		// Token: 0x040028F1 RID: 10481
		private float tween = 1f;

		// Token: 0x040028F2 RID: 10482
		private float[] clipRate;

		// Token: 0x040028F3 RID: 10483
		private float cuttyRate;

		// Token: 0x040028F4 RID: 10484
		public Vector3 cuttyPos;

		// Token: 0x040028F5 RID: 10485
		public Vector3 oldcuttyPos;

		// Token: 0x040028F6 RID: 10486
		public Vector3 cuttyVeloc;

		// Token: 0x040028F7 RID: 10487
		public float cuttyScale;

		// Token: 0x040028F8 RID: 10488
		public float cuttyRot;

		// Token: 0x040028F9 RID: 10489
		private Matrix cuttyTrans;

		// Token: 0x040028FA RID: 10490
		private Random rr;

		// Token: 0x040028FB RID: 10491
		private Model pigModel;

		// Token: 0x040028FC RID: 10492
		private Model sphere;

		// Token: 0x040028FD RID: 10493
		private Model eyeglow;

		// Token: 0x040028FE RID: 10494
		private int pigIndex;

		// Token: 0x040028FF RID: 10495
		private Effect pigSkin;

		// Token: 0x04002900 RID: 10496
		private Effect eyeEffect;

		// Token: 0x04002901 RID: 10497
		private Effect solidSkin;

		// Token: 0x04002902 RID: 10498
		public Effect glowEffect;

		// Token: 0x04002903 RID: 10499
		private Texture2D pigTexture;

		// Token: 0x04002904 RID: 10500
		private Texture2D eyeTexture1;

		// Token: 0x04002905 RID: 10501
		private Texture2D eyeTexture2;

		// Token: 0x04002906 RID: 10502
		private Matrix view;

		// Token: 0x04002907 RID: 10503
		private Matrix proj;

		// Token: 0x04002908 RID: 10504
		public bool attack1;

		// Token: 0x04002909 RID: 10505
		private bool attackLand;

		// Token: 0x0400290A RID: 10506
		private int nextAttack = 100;

		// Token: 0x0400290B RID: 10507
		public bool death1;

		// Token: 0x0400290C RID: 10508
		public bool deathsent;

		// Token: 0x0400290D RID: 10509
		public bool cuttyisDead;

		// Token: 0x0400290E RID: 10510
		public bool cuttyHeal;

		// Token: 0x0400290F RID: 10511
		private float gonnaHealDelay;

		// Token: 0x04002910 RID: 10512
		private Vector3 roofTop1 = new Vector3(3364f, -1.57f, 4625f);

		// Token: 0x04002911 RID: 10513
		private Vector3 roofTop2 = new Vector3(1244f, 0f, 2868f);

		// Token: 0x04002912 RID: 10514
		private Vector3 roofTop3 = new Vector3(4639f, 0f, 2942f);

		// Token: 0x04002913 RID: 10515
		private float cuttyDeadx;

		// Token: 0x04002914 RID: 10516
		private float cuttyDeadz;

		// Token: 0x04002915 RID: 10517
		private float cuttyDeadrot;

		// Token: 0x04002916 RID: 10518
		private bool cuttyDyingWalk;

		// Token: 0x04002917 RID: 10519
		private float cuttyDeathTimer = 500f;

		// Token: 0x04002918 RID: 10520
		private float cuttyHealrot;

		// Token: 0x04002919 RID: 10521
		private float origx;

		// Token: 0x0400291A RID: 10522
		private float origy;

		// Token: 0x0400291B RID: 10523
		private float origz;

		// Token: 0x0400291C RID: 10524
		private float destx;

		// Token: 0x0400291D RID: 10525
		private float desty;

		// Token: 0x0400291E RID: 10526
		private float destz;

		// Token: 0x0400291F RID: 10527
		private float upForce;

		// Token: 0x04002920 RID: 10528
		private float grav;

		// Token: 0x04002921 RID: 10529
		private float attackduration;

		// Token: 0x04002922 RID: 10530
		private float attacktimer;

		// Token: 0x04002923 RID: 10531
		private float attackWait1;

		// Token: 0x04002924 RID: 10532
		private float attackWait2;

		// Token: 0x04002925 RID: 10533
		private float healorigx;

		// Token: 0x04002926 RID: 10534
		private float healorigy;

		// Token: 0x04002927 RID: 10535
		private float healorigz;

		// Token: 0x04002928 RID: 10536
		private float healdestx;

		// Token: 0x04002929 RID: 10537
		private float healdesty;

		// Token: 0x0400292A RID: 10538
		private float healdestz;

		// Token: 0x0400292B RID: 10539
		private float healduration;

		// Token: 0x0400292C RID: 10540
		private float healtimer;

		// Token: 0x0400292D RID: 10541
		private float healWait1;

		// Token: 0x0400292E RID: 10542
		private float healWait2;

		// Token: 0x0400292F RID: 10543
		private float healChant;

		// Token: 0x04002930 RID: 10544
		private bool onGround;

		// Token: 0x04002931 RID: 10545
		public ushort startHealth = 1500;

		// Token: 0x04002932 RID: 10546
		public ushort health = 1500;

		// Token: 0x04002933 RID: 10547
		private float healthPerc;

		// Token: 0x04002934 RID: 10548
		private int healCount = 2;

		// Token: 0x04002935 RID: 10549
		public byte damagebit = 1;

		// Token: 0x04002936 RID: 10550
		public int homing;

		// Token: 0x04002937 RID: 10551
		public int homingCount;

		// Token: 0x04002938 RID: 10552
		public bool cuttyDoneLocalSpeech;

		// Token: 0x04002939 RID: 10553
		public int speechCurveindex = 4;

		// Token: 0x0400293A RID: 10554
		public int startCurveindex = 1;

		// Token: 0x0400293B RID: 10555
		public float startLoop = 0.2f;

		// Token: 0x0400293C RID: 10556
		public float startRate = 9f;

		// Token: 0x0400293D RID: 10557
		private List<int> speechList = new List<int>();

		// Token: 0x0400293E RID: 10558
		private List<int> speechInclude = new List<int>();

		// Token: 0x02000116 RID: 278
		public struct hitStream : IVertexType
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0026A554 File Offset: 0x00268754
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return Cutty.hitStream.VertexDeclaration;
				}
			}

			// Token: 0x0400293F RID: 10559
			public Matrix Trans;

			// Token: 0x04002940 RID: 10560
			public float Fade;

			// Token: 0x04002941 RID: 10561
			public Vector4 Coord;

			// Token: 0x04002942 RID: 10562
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
				new VertexElement(68, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4)
			});
		}

		// Token: 0x02000117 RID: 279
		public struct hole
		{
			// Token: 0x04002943 RID: 10563
			public float[] fade;

			// Token: 0x04002944 RID: 10564
			public Vector3[] location;

			// Token: 0x04002945 RID: 10565
			public int[] bone;

			// Token: 0x04002946 RID: 10566
			public Cutty.hitStream[] stainTrans;

			// Token: 0x04002947 RID: 10567
			public int[] frame;

			// Token: 0x04002948 RID: 10568
			public Matrix[] scale;

			// Token: 0x04002949 RID: 10569
			public int stainIndex;

			// Token: 0x0400294A RID: 10570
			public int stainMax;

			// Token: 0x0400294B RID: 10571
			public int stainCapacity;

			// Token: 0x0400294C RID: 10572
			public DynamicVertexBuffer stainBuffer;

			// Token: 0x0400294D RID: 10573
			public Vector4[] stainR;
		}

		// Token: 0x02000118 RID: 280
		public struct instancedObject : IVertexType
		{
			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0026A5FF File Offset: 0x002687FF
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return Cutty.instancedObject.InstanceVertexDeclaration;
				}
			}

			// Token: 0x0400294E RID: 10574
			public Matrix Trans;

			// Token: 0x0400294F RID: 10575
			private static readonly VertexDeclaration InstanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3)
			});
		}

		// Token: 0x02000119 RID: 281
		public struct shell
		{
			// Token: 0x04002950 RID: 10576
			public int type;

			// Token: 0x04002951 RID: 10577
			public int max;

			// Token: 0x04002952 RID: 10578
			public int tempindex;

			// Token: 0x04002953 RID: 10579
			public int index;

			// Token: 0x04002954 RID: 10580
			public int maxCapacity;

			// Token: 0x04002955 RID: 10581
			public Matrix[] offset;

			// Token: 0x04002956 RID: 10582
			public bool startDrop;

			// Token: 0x04002957 RID: 10583
			public float dropTimer;

			// Token: 0x04002958 RID: 10584
			public int[] bone;

			// Token: 0x04002959 RID: 10585
			public Cutty.instancedObject[] stream;

			// Token: 0x0400295A RID: 10586
			public DynamicVertexBuffer buffer;

			// Token: 0x0400295B RID: 10587
			public Cutty.instancedObject[] displayList;

			// Token: 0x0400295C RID: 10588
			public chunkDupe[] dupe;

			// Token: 0x0400295D RID: 10589
			public Model model;
		}

		// Token: 0x0200011A RID: 282
		public struct conductor
		{
			// Token: 0x0400295E RID: 10590
			public int flag;

			// Token: 0x0400295F RID: 10591
			public int time;

			// Token: 0x04002960 RID: 10592
			public int homing;

			// Token: 0x04002961 RID: 10593
			public float targetRate;

			// Token: 0x04002962 RID: 10594
			public int curveIndex;

			// Token: 0x04002963 RID: 10595
			public float loop;

			// Token: 0x04002964 RID: 10596
			public int animType;

			// Token: 0x04002965 RID: 10597
			public int talkindex;

			// Token: 0x04002966 RID: 10598
			public ushort dur;

			// Token: 0x04002967 RID: 10599
			public float destx;

			// Token: 0x04002968 RID: 10600
			public float destz;

			// Token: 0x04002969 RID: 10601
			public float rot;
		}
	}
}
