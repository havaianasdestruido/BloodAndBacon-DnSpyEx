using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x0200018A RID: 394
	internal class GameplayScreen : GameScreen
	{
		// Token: 0x06000E65 RID: 3685 RVA: 0x004050FC File Offset: 0x004032FC
		public GameplayScreen(float divider)
		{
			base.TransitionOnTime = TimeSpan.FromSeconds(0.5);
			base.TransitionOffTime = TimeSpan.FromSeconds(0.5);
			this.backgroundThread = new Thread(new ThreadStart(this.BackgroundWorkerThread));
			this.backgroundThread.Name = "space";
			this.backgroundThreadExit = new ManualResetEvent(false);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00405E70 File Offset: 0x00404070
		public override void LoadContent()
		{
			this.sc = base.ScreenManager;
			this.sc.loadflag = 0;
			this.sc.lockVideosetup = true;
			this.sc.LoadSpaceKeys();
			this.random = new Random();
			this.sc.Game.ResetElapsedTime();
			if (this.content == null)
			{
				this.content = new ContentManager(this.sc.Game.Services, "Content");
			}
			this.spriteBatch = new SpriteBatch(this.sc.GraphicsDevice);
			this.sc.loadSpaceFarm();
			this.sc.myTimer = 0f;
			if (this.sc.usingMouse)
			{
				this.sc.getXkey();
			}
			else
			{
				this.sc.myXkey = "X";
			}
			this.emo = GameplayScreen.act.chill;
			this.typer = this.content.Load<SpriteFont>("astro\\fonts\\typer");
			this.pop1 = this.content.Load<SoundEffect>("astro\\Audio\\pop");
			this.objectiveData.Add("OBJECTIVE 1.01 : \nLook For A Safe Place\nYou Are Running Out of Oxygen\nYou Will Soon Die");
			this.objectiveData.Add("OBJECTIVE 2.F5 : \nFind A New Vehicle\nLearn How To Fly\nPark The Rover Inside");
			this.objectiveData.Add("OBJECTIVE 3.06 : \nRescue A Stranded Astronaut\nWalk Up To The Astronaut\nBring Him Back To The Farm\nHonk To Make Him Leave");
			this.objectiveData.Add("OBJECTIVE 4.BB : \nMine Some Ore Now\nUsing Your Rover\nSmash Rocks With The Bumper");
			this.objectiveData.Add("OBJECTIVE 5.E9 : \nRefining Ore Make Fuel\nFill The Fuel Tank\nBring Ore Into The Lander");
			this.objectiveData.Add("OBJECTIVE 6.01 : \nKill An Astronaut\nTry Everything\nDo It Now , Do It");
			this.objectiveData.Add("OBJECTIVE 7.XY : \nExploration And Discovery\nThere Is A Secret Location\nUse Your Resources");
			this.objectiveData.Add("OBJECTIVE 8.EA : \nCreate A Worker\nBuild And Build\nPress The 2nd Switch");
			this.objectiveData.Add("OBJECTIVE 9.7I :  \nSearching For Mission\nPlease Wait A Minute\nSearching . . . .");
			this.objectiveData.Add("OBJECTIVE 10.0E : \nSomeone Needs To Be Rescued\nUse Your Compass\nBring Him Home Safely");
			this.objectiveData.Add("OBJECTIVE 11.3G : \nTake A Ride On A Dropship\nLook At The Terrain\nFollow The Groove");
			this.objectiveData.Add("OBJECTIVE 12.TT : \nSomeone Is Lost\nPlease Find Them\nBring That Back Home");
			this.objectiveData.Add("OBJECTIVE 13.RD : \nThis Really Is Enough\nThanks For Playing\nStart Transmission");
			this.objectiveData.Add("OBJECTIVE 13.XC : \nThis Really Is Enough\nThanks For Playing\nEnd Transmission");
			this.objectiveData.Add("OBJECTIVE 13.XC : \nThis Really Is Enough\nThanks For Playing\nEnd Transmission");
			this.text = this.objectiveData[this.objIndex];
			this.grassTexture = this.sc.grassTexture;
			this.buildingRGB = this.sc.buildingRGB;
			this.buildingShadow = this.sc.buildingshadspace;
			this.buildingEffect = this.content.Load<Effect>("effects\\buildingsPMspace");
			this.buildingEffect.Parameters["rgbTexture"].SetValue(this.buildingRGB);
			this.buildingEffect.Parameters["shadTexture"].SetValue(this.buildingShadow);
			this.buildingEffect.Parameters["moon"].SetValue(this.sc.moontype);
			this.heightmodel = this.sc.farmspacecollide;
			Dictionary<string, object> dictionary = (Dictionary<string, object>)this.heightmodel.Tag;
			float[] array = (float[])dictionary["Heights"];
			int num = 0;
			this.heights = new int[200, 200];
			for (int i = 0; i < 200; i++)
			{
				for (int j = 0; j < 200; j++)
				{
					this.heights[i, j] = (int)array[num];
					num++;
				}
			}
			this.farmLocation = new Vector3(0f, 5f, 0f);
			Lander.farmLocation = this.farmLocation;
			Rover.farmLocation = this.farmLocation;
			Rover.nearfarm = this.nearfarm;
			Lander.nearfarm = this.nearfarm;
			this.afont4 = this.content.Load<SpriteFont>("font\\afont4");
			this.ammoMedium = new SpriteFont[] { this.afont4 };
			this.ammoMedium2 = this.content.Load<SpriteFont>("font\\ammomedium2");
			int num2 = 0;
			foreach (Vector3 vector in this.origRegion1)
			{
				this.origRegion1[num2] = Vector3.Transform(this.origRegion1[num2], Matrix.CreateScale(1.8f));
				num2++;
			}
			this.tinting = new Vector3[]
			{
				this.mercury, this.mercury, this.venus, this.earth, this.mars, this.jupiter, this.saturn, this.uranus, this.neptune, this.pluto,
				this.c10
			};
			for (int l = 0; l < this.tinting.Length; l++)
			{
				this.tinting[l] = (this.tinting[l] + Vector3.One * 2f) / 3f;
			}
			this.sc.planet = this.sc.bgindex;
			this.lander = new Lander();
			this.rover = new Rover();
			this.overlay = new Overlay();
			this.tmake = new tmake();
			this.surface = new MoonSurface();
			this.landerEngineI = this.sc.landerEngine.CreateInstance();
			this.roverEngineI = this.sc.roverEngine.CreateInstance();
			this.dropEngineI = this.sc.dropEngine.CreateInstance();
			this.gravelI = this.sc.gravel.CreateInstance();
			this.overtureInstance = this.sc.overture.CreateInstance();
			this.flowerradioInstance = this.content.Load<SoundEffect>("astro\\audio\\radioshit").CreateInstance();
			this.breathing = this.sc.breath.CreateInstance();
			this.blurEffect = this.content.Load<Effect>("astro\\shaders\\postShader");
			float num3 = 15f;
			Vector2[] array3 = new Vector2[24];
			for (int m = 0; m < 24; m++)
			{
				float num4 = (float)(-(float)m);
				array3[m] = new Vector2((float)Math.Sin((double)MathHelper.ToRadians(num4 * num3)), -1.4f * (float)Math.Cos((double)MathHelper.ToRadians(180f + num4 * num3)));
			}
			this.blurEffect.Parameters["offsets"].SetValue(array3);
			this.blurdot = this.content.Load<Texture2D>("astro\\textures\\blurdot");
			this.blurEffect.Parameters["flatTexture"].SetValue(this.blurdot);
			this.mybasic = this.content.Load<Effect>("astro\\shaders\\basic");
			this.dropshipTexture = this.content.Load<Texture2D>("astro\\textures\\dropShipPNGnewlow");
			this.dropshipLights = this.content.Load<Texture2D>("astro\\textures\\dropshipLights");
			this.mybasic.Parameters["modelTexture"].SetValue(this.dropshipTexture);
			this.mybasic.Parameters["modelTexture2"].SetValue(this.dropshipLights);
			this.distortion = this.content.Load<Texture2D>("astro\\textures\\dist");
			this.shieldTexture = this.content.Load<Texture2D>("astro\\textures\\shield");
			this.terrainEffect = this.content.Load<Effect>("astro\\shaders\\terrainShader");
			this.starEffect = this.content.Load<Effect>("astro\\shaders\\starShader");
			this.shadowEffect = this.content.Load<Effect>("astro\\shaders\\shadowShader");
			this.simpEffect = this.content.Load<Effect>("astro\\shaders\\simple");
			this.wallPlane = new Plane(new Vector3(-15000f, 0f, -15000f), new Vector3(15000f, 0f, -15000f), new Vector3(-15000f, 0f, 15000f));
			this.texdata = new MoonSurface.tex[4000000];
			this.heightData = new int[2000, 2000];
			this.normalData = new Vector3[2000, 2000];
			this.objectData = new int[500, 500];
			this.sphere = this.content.Load<Model>("astro\\models//sphere");
			this.prints = default(GameplayScreen.hole);
			this.prints.stainMax = 0;
			this.prints.stainIndex = 0;
			this.prints.drift = new float[150];
			this.prints.stainCapacity = 150;
			this.prints.stainTrans = new GameplayScreen.hitStream[this.prints.stainCapacity];
			this.prints.stainBuffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, GameplayScreen.vd2, this.prints.stainCapacity, BufferUsage.WriteOnly);
			this.footprint = this.content.Load<Model>("astro\\models//footprint1");
			this.normalmap = this.content.Load<Texture2D>("astro\\textures//normalmap");
			this.tmake.Load(this.content, this.sc);
			this.bitmap = this.tmake.Tw;
			this.rover.bitmap = this.bitmap;
			this.lander.bitmap = this.bitmap;
			this.gridscale = this.tmake.gridScale;
			this.rover.gridscale = this.gridscale;
			this.lander.gridScale = (float)this.gridscale;
			this.unit = this.bitmap - 1;
			this.rover.LoadContent(this.content, this.sc);
			this.lander.LoadContent(this.content, this.sc);
			this.overlay.Load(this.content, this.sc);
			this.overlay.farm = this.farmLocation;
			this.shale1.LoadContent(this.content, this.sc);
			this.shale2.LoadContent(this.content, this.sc);
			this.shale3.LoadContent(this.content, this.sc);
			this.dropship = this.content.Load<Model>("astro\\models\\dropship");
			this.dropshipShade = this.content.Load<Model>("astro\\models\\dropship");
			this.beacon = this.content.Load<Model>("astro\\models\\beacon");
			this.stardome = this.content.Load<Model>("astro\\models\\dome3");
			this.galaxy = this.content.Load<Model>("astro\\models\\galaxy");
			this.darkFog = this.content.Load<Model>("astro\\models\\darkFog");
			this.LanderShadow = this.content.Load<Model>("astro\\models\\LanderShade");
			this.tankShadow = this.content.Load<Model>("astro\\models\\tankShade");
			this.bigquad = this.content.Load<Model>("astro\\models\\bigquad");
			this.rock.flower = this.content.Load<Model>("astro\\models\\flower");
			this.rock.rock = this.content.Load<Model>("astro\\models\\rockhigh");
			this.rock.rock1 = this.content.Load<Model>("astro\\models\\rockpart1");
			this.rock.rock2 = this.content.Load<Model>("astro\\models\\rockpart2");
			this.rock.slab = this.content.Load<Model>("astro\\models\\slab3");
			this.rock.slablet = this.content.Load<Model>("astro\\models\\slab4");
			this.rock.reflection = this.content.Load<Texture2D>("astro\\textures\\reflectBW");
			this.shale1.model = this.content.Load<Model>("astro\\models\\shale1");
			this.shale1.gtype = gemstruct.gemtype.shale;
			this.shale2.model = this.content.Load<Model>("astro\\models\\shale2");
			this.shale2.gtype = gemstruct.gemtype.ruby;
			this.shale3.model = this.content.Load<Model>("astro\\models\\sapphire");
			this.shale3.gtype = gemstruct.gemtype.sapphire;
			this.starSheet = this.content.Load<Texture2D>("astro\\sprites\\stars\\starSheet");
			this.skids = new skidsSystem(this.sc.Game, this.content);
			this.skids.Initialize();
			this.skids.LoadContent(this.sc.GraphicsDevice);
			this.streaks = new streaksSystem(this.sc.Game, this.content);
			this.streaks.Initialize();
			this.streaks.LoadContent(this.sc.GraphicsDevice);
			this.dust = new dustSystem(this.sc.Game, this.content);
			this.dust.Initialize();
			this.dust.LoadContent(this.sc.GraphicsDevice);
			this.lastAlias = this.sc.aliasSetting;
			this.aspectratio = this.sc.aspectratio2;
			this.longProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(this.lens), this.aspectratio, 14f, 195000f);
			this.sc.loadflag = 0;
			this.surface.LoadContent(this.content, this.sc, ref this.heightData, ref this.normalData, ref this.objectData, ref this.texdata);
			this.sc.loadflag = 0;
			this.tmake.LoadVertices();
			this.rock.LoadContent(this.content, this.sc, this.gridscale, this.bitmap, 550);
			this.pp = this.sc.GraphicsDevice.PresentationParameters;
			this.resolveTarget1 = new RenderTarget2D(this.sc.GraphicsDevice, 1280, 720, false, this.pp.BackBufferFormat, this.pp.DepthStencilFormat, 2, RenderTargetUsage.DiscardContents);
			this.resolveTarget2 = new RenderTarget2D(this.sc.GraphicsDevice, 1280, 720, false, this.pp.BackBufferFormat, this.pp.DepthStencilFormat, 0, RenderTargetUsage.DiscardContents);
			this.shadowTarget = new RenderTarget2D(this.sc.GraphicsDevice, 800, 800, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.buildTextures(0);
			this.terrainGoto(1000, 1000);
			this.facility = new Facility();
			Facility.inFacility = false;
			Facility.outsideCastle = true;
			this.facility.scaler = 4f;
			this.facility.facilityLocate.X = (float)((int)((this.surface.facility1.X + 75f) / 150f) * 150 - 75);
			this.facility.facilityLocate.Y = (float)((int)((this.surface.facility1.Y + 75f) / 150f) * 150 - 75);
			this.overlay.facility.X = this.facility.facilityLocate.X;
			this.overlay.facility.Z = this.facility.facilityLocate.Y;
			this.GetHeightOnly(ref this.tmake.heightData, new Vector3(this.facility.facilityLocate.X, 0f, this.facility.facilityLocate.Y), out this.groundHeight);
			Facility.offset = new Vector3(-2250f + this.facility.facilityLocate.X, this.groundHeight - 1441f / this.facility.scaler, -4337.5f + this.facility.facilityLocate.Y);
			this.facility.LoadContent(this.content, this.sc);
			this.sc.loadflag = 2;
			this.makeFaciltyDirt();
			this.starmap.LoadContent(this.sc, this.content, this.sc.GraphicsDevice, 1200, 20000, true, this.sc.planet - 1);
			this.starEffect.Parameters["modelTexture"].SetValue(this.starmap.manmadestars);
			this.sc.loadflag = 0;
			this.sc.astronaut.man.dupe.Clear();
			for (int n = 0; n < this.surface.astroLoc.Count; n++)
			{
				this.sc.astronaut.dropAstronaut(this.myframe, this.surface.astroLoc[n].Loc, 1, this.surface.astroLoc[n].emo);
				this.myframe++;
			}
			this.lens = 70f;
			this.setLens(70f);
			this.buildstrings();
			while (!LoadingScreen2.done)
			{
				Thread.Sleep(5);
			}
			this.backgroundThread.Start();
			this.sc.LoadSpacePrefs();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00407048 File Offset: 0x00405248
		public void buildstrings()
		{
			GameplayScreen.nearastroBuild.Length = 0;
			GameplayScreen.nearastroBuild.Append("  press to rescue astronaut");
			GameplayScreen.rover1Build.Length = 0;
			GameplayScreen.rover1Build.Append("  press to drive rover");
			GameplayScreen.liftingFriendBuild.Length = 0;
			GameplayScreen.liftingFriendBuild.Append("  lifting your friend !!");
			GameplayScreen.atDoorBuild.Length = 0;
			GameplayScreen.atDoorBuild.Append("  press to move Barn Door");
			GameplayScreen.atPump1Build.Length = 0;
			GameplayScreen.atPump1Build.Append("  press to drink water");
			GameplayScreen.atPump2Build.Length = 0;
			GameplayScreen.atPump2Build.Append("  this Well is empty");
			GameplayScreen.atLever1Build.Length = 0;
			GameplayScreen.atLever1Build.Append("  press to activate Electric Fence");
			GameplayScreen.atLever2Build.Length = 0;
			GameplayScreen.atLever2Build.Append("  Out of Order, try again tomorrow");
			GameplayScreen.atLever3Build.Length = 0;
			GameplayScreen.atLever3Build.Append("  electric fence is in operation");
			GameplayScreen.atPumpBusyBuild.Length = 0;
			GameplayScreen.atPumpBusyBuild.Append("  Pump is being used");
			GameplayScreen.atKissingBuild.Length = 0;
			GameplayScreen.atKissingBuild.Append("  to Wear Secret Item");
			GameplayScreen.atgrinderBuild.Length = 0;
			GameplayScreen.atgrinderBuild.Append("  Shoot the Glowing Blue Buttons !!");
			GameplayScreen.needbloodBuild.Length = 0;
			GameplayScreen.needbloodBuild.Append("  Kick parts into Front-Blade");
			GameplayScreen.reloadBuild.Length = 0;
			GameplayScreen.reloadBuild.Append("  press to Reload Weapon");
			GameplayScreen.atDoorLockedBuild.Length = 0;
			GameplayScreen.atDoorLockedBuild.Append("  Barn Door is locked");
			GameplayScreen.atFarmerBuild.Length = 0;
			GameplayScreen.atFarmerBuild.Append("  press to talk to Farmer");
			GameplayScreen.pickupMilkBuild.Length = 0;
			GameplayScreen.pickupMilkBuild.Append("  press to pickup Boars Milk");
			GameplayScreen.pickupAmmoBuild.Length = 0;
			GameplayScreen.pickupAmmoBuild.Append("  press to pickup Bullet Box");
			GameplayScreen.pickupHulkBuild.Length = 0;
			GameplayScreen.pickupHulkBuild.Append("  press to pickup Bulkify");
			GameplayScreen.pickupGrenBuild.Length = 0;
			GameplayScreen.pickupGrenBuild.Append("  press to pickup Grenade");
			GameplayScreen.pickupPillBuild.Length = 0;
			GameplayScreen.pickupPillBuild.Append("  press to pickup BounceBack");
			GameplayScreen.pickupRocketBuild.Length = 0;
			GameplayScreen.pickupRocketBuild.Append("  press to pickup Rocket");
			GameplayScreen.pickupFullBuild.Length = 0;
			GameplayScreen.pickupFullBuild.Append("  cant carry more of that");
			GameplayScreen.pickWeaponBuild.Length = 0;
			GameplayScreen.pickWeaponBuild.Append("  press to take Weapon");
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x004072E0 File Offset: 0x004054E0
		private void BackgroundWorkerThread()
		{
			while (!this.backgroundThreadExit.WaitOne(10))
			{
				if (!this.isBusy && this.loads.Count > 0 && !this.isDrawing)
				{
					this.isBusy = true;
					switch (this.loads[0])
					{
					case 1:
						this.terrainhester();
						break;
					}
					this.loads.RemoveAt(0);
					this.isBusy = false;
				}
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0040737C File Offset: 0x0040557C
		public override void UnloadContent()
		{
			try
			{
				GamePad.SetVibration(this.myplayer, 0f, 0f);
				this.dropEngineI.Dispose();
				this.landerEngineI.Dispose();
				this.roverEngineI.Dispose();
				this.gravelI.Dispose();
				this.overtureInstance.Dispose();
				this.flowerradioInstance.Dispose();
				this.breathing.Dispose();
			}
			catch
			{
			}
			try
			{
				this.resolveTarget1.Dispose();
				this.resolveTarget1 = null;
				this.resolveTarget2.Dispose();
				this.resolveTarget2 = null;
				this.shadowTarget.Dispose();
				this.shadowTarget = null;
				this.starmap.manmadestars.Dispose();
				this.starmap.tt.Dispose();
				this.starmap.starSheet.Dispose();
				this.starmap.colorArray1 = new Color[0];
				this.sc.astronaut.man.dupe.Clear();
				this.texdata = new MoonSurface.tex[0];
			}
			catch
			{
			}
			try
			{
				if (this.backgroundThread != null)
				{
					this.backgroundThreadExit.Set();
					this.backgroundThread.Join();
				}
			}
			catch
			{
			}
			this.content.Unload();
			this.content.Dispose();
			this.content = null;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00407514 File Offset: 0x00405714
		private string botPath(ref float[] floorplot, ref List<Vector2> botPath, Vector2 startpos, Vector2 destiny)
		{
			List<Vector2> list = new List<Vector2>();
			float num = 700f;
			int num2 = -1;
			for (int i = 0; i < floorplot.Length; i += 2)
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
				return "lost";
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
			botPath.Insert(0, startpos);
			botPath.Add(destiny);
			if (!flag)
			{
				return "lost";
			}
			return "good";
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00407648 File Offset: 0x00405848
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

		// Token: 0x06000E6C RID: 3692 RVA: 0x00407760 File Offset: 0x00405960
		private void buildTextures(int x)
		{
			if (x == 1)
			{
				this.myVar2[0]++;
			}
			if (this.myVar2[0] > 100)
			{
				this.myVar2[0] = 1;
			}
			if (x == 2)
			{
				this.myVar2[1]++;
			}
			if (this.myVar2[1] > 100)
			{
				this.myVar2[1] = 1;
			}
			if (x == 3)
			{
				this.myVar2[2]++;
			}
			if (this.myVar2[2] > 100)
			{
				this.myVar2[2] = 1;
			}
			if (x == 4)
			{
				this.myVar2[3]++;
			}
			if (this.myVar2[3] > 100)
			{
				this.myVar2[3] = 1;
			}
			if (this.sc.planetName[this.sc.planet] == "Mercury")
			{
				this.landNum1 = 12;
				this.landNum2 = 95;
				this.landNum4 = 69;
			}
			else if (this.sc.planetName[this.sc.planet] == "Venus")
			{
				this.landNum1 = 1;
				this.landNum2 = 93;
				this.landNum4 = 87;
			}
			else if (this.sc.planetName[this.sc.planet] == "Earth")
			{
				this.landNum1 = 97;
				this.landNum2 = 101;
				this.landNum4 = 51;
			}
			else if (this.sc.planetName[this.sc.planet] == "Mars")
			{
				this.landNum1 = 16;
				this.landNum2 = 22;
				this.landNum4 = 31;
			}
			else if (this.sc.planetName[this.sc.planet] == "Neptune")
			{
				this.landNum1 = 10;
				this.landNum2 = 9;
				this.landNum4 = 11;
			}
			else
			{
				this.landNum1 = this.myVar2[0];
				this.landNum2 = this.myVar2[1];
				this.landNum4 = this.myVar2[3];
			}
			this.layer1 = this.content.Load<Texture2D>("astro\\sprites\\land\\text" + this.landNum1);
			this.layer2 = this.content.Load<Texture2D>("astro\\sprites\\land\\text" + this.landNum2);
			this.layer4 = this.content.Load<Texture2D>("astro\\sprites\\land\\text" + this.landNum4);
			this.terrainEffect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadows3"];
			this.terrainEffect.Parameters["xTexture1"].SetValue(this.layer1);
			this.terrainEffect.Parameters["xTexture2"].SetValue(this.layer2);
			this.terrainEffect.Parameters["xTexture4"].SetValue(this.layer4);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00407A8C File Offset: 0x00405C8C
		private void bucketCollide()
		{
			Matrix matrix = Matrix.CreateRotationX(-0.44f) * Matrix.CreateTranslation(0f, 25.98f, -26.1f) * this.sc.scooperTrans;
			gemstruct.setitRight = this.rover.orientation * Matrix.CreateTranslation(this.rover.position);
			matrix = Matrix.CreateRotationX(0f) * Matrix.CreateTranslation(0f, 0f, -26.1f) * this.sc.scooperTrans;
			gemstruct.setVacuum = matrix * this.rover.orientation * Matrix.CreateTranslation(this.rover.position);
			gemstruct.inDumper = this.rover.orientation * Matrix.CreateFromAxisAngle(this.rover.orientation.Forward, this.rover.leanAmt) * Matrix.CreateTranslation(this.rover.pp);
			this.bucketRegion1[0] = Vector3.Transform(this.origRegion1[0] * 30f, gemstruct.setitRight);
			this.bucketRegion1[1] = Vector3.Transform(this.origRegion1[1] * 30f, gemstruct.setitRight);
			this.bucketRegion1[2] = Vector3.Transform(this.origRegion1[2] * 30f, gemstruct.setitRight);
			this.bucketRegion1[3] = Vector3.Transform(this.origRegion1[3] * 30f, gemstruct.setitRight);
			this.bucketRegion1[4] = Vector3.Transform(this.origRegion1[4] * 30f, this.rover.orientation * Matrix.CreateTranslation(this.rover.position));
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00407CC4 File Offset: 0x00405EC4
		private void terrainGoto(int x, int z)
		{
			this.trnLoctn.X = this.trnLoctn.X + (float)(300 * this.gridscale);
			this.trnLoctn.Z = this.trnLoctn.Z + (float)(300 * this.gridscale);
			this.myposx = x + 298;
			this.myposz = z + 298;
			if (this.myposx > 1999)
			{
				this.myposx -= 2000;
			}
			if (this.myposz > 1999)
			{
				this.myposz -= 2000;
			}
			for (int i = 299; i >= 0; i--)
			{
				this.trnLoctn -= new Vector3((float)this.gridscale, 0f, (float)this.gridscale);
				this.whatz = (int)Math.Round((double)(this.trnLoctn.Z / (float)this.gridscale)) * this.gridscale;
				this.zgrid = (int)((this.trnLoctn.Z + (float)(this.gridscale / 2)) / (float)this.gridscale % (float)this.unit + (float)this.unit) % this.unit;
				this.whatx = (int)Math.Round((double)(this.trnLoctn.X / (float)this.gridscale)) * this.gridscale;
				this.xgrid = (int)((this.trnLoctn.X + (float)(this.gridscale / 2)) / (float)this.gridscale % (float)this.unit + (float)this.unit) % this.unit;
				if ((this.lastxgrid != this.xgrid && this.whatx != this.diffix) || (this.lastzgrid != this.zgrid && this.whatz != this.diffiz))
				{
					this.UpdateTerrain(150000, true);
					if (Vector2.Distance(new Vector2(this.trnLoctn.X, this.trnLoctn.Z), new Vector2(this.trnLoctnLast.X, this.trnLoctnLast.Z)) > (float)this.cutoff)
					{
						this.trnLoctnLast = this.trnLoctn;
					}
				}
			}
			this.rover.velocity = Vector3.Zero;
			this.GetHeightOnly(ref this.tmake.heightData, this.trnLoctn, out this.rover.position.Y);
			this.rover.HandleInput(ref this.mouseState, ref this.prevMouse, ref this.keyState, ref this.prevkeyState, ref this.gamePadState, ref this.heights, ref this.tmake.normals);
			this.rover.position = new Vector3(this.rover.position.X, this.rover.position.Y, this.rover.position.Z);
			this.oldtrnLoctn = this.trnLoctn;
			this.oldtrnLoctn2 = this.trnLoctn;
			this.rock.myPOS = new Vector2((float)((int)Math.Round((double)(this.trnLoctn.X / 1000f)) * 1000), (float)((int)Math.Round((double)(this.trnLoctn.Z / 1000f)) * 1000));
			this.rock.oldmyPOS = this.rock.myPOS;
			this.rock.slabPOS = this.rock.myPOS;
			this.rock.oldslabPOS = this.rock.myPOS;
			this.trnLoctnLast = this.trnLoctn;
			this.tmake.moveall();
			this.rock.placeOBJECTS(this.vehicleindex, ref this.rover, ref this.lander, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData, (float)this.sc.planet);
			this.rock.placeSLAB(this.vehicleindex, ref this.rover, ref this.lander, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
			this.lander.velocity = Vector3.Zero;
			Lander.nearfarm = false;
			this.lander.position = new Vector3(-4500f, 0f, 3900f);
			this.GetHeightOnly(ref this.tmake.heightData, this.lander.position, out this.lander.position.Y);
			this.lander.position = new Vector3(this.lander.position.X, this.lander.position.Y, this.lander.position.Z);
			this.lander.impact = 1;
			this.lander.diff = 2f;
			this.lander.door1 = 1;
			this.rover.onramp = 0;
			this.lander.HandleInput(ref this.mouseState, ref this.prevMouse, ref this.keyState, ref this.prevkeyState, ref this.gamePadState, ref this.tmake.heightData, ref this.tmake.normals);
			this.lander.orientation = Matrix.CreateRotationY(this.lander.directme);
			this.lander.orientation.Up = this.lander.normal;
			this.lander.orientation.Right = Vector3.Cross(this.lander.orientation.Forward, this.lander.orientation.Up);
			this.lander.orientation.Right = Vector3.Normalize(this.lander.orientation.Right);
			this.lander.orientation.Forward = Vector3.Cross(this.lander.orientation.Up, this.lander.orientation.Right);
			this.lander.orientation.Forward = Vector3.Normalize(this.lander.orientation.Forward);
			this.dropshipPOS = new Vector3(-110250f, this.lander.position.Y + 4500f, 0f);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00408318 File Offset: 0x00406518
		private void updateMusic()
		{
			this.flowervol = 1f - MathHelper.Clamp(this.rock.flowerDistance / 35000f, 0f, 1f);
			this.overlay.flowerScale = this.flowervol;
			this.flowervolumeTimer--;
			if (this.flowervolumeTimer <= 0)
			{
				this.flowervol = 0f;
			}
			if (this.flowervolumeTimer == 0)
			{
				this.sc.radio1.Play(this.sc.ev, 0.3f, 0f);
			}
			if (this.flowerradioInstance.State == SoundState.Stopped)
			{
				this.flowerradioInstance.IsLooped = true;
				this.flowerradioInstance.Volume = this.flowervol * this.sc.voiceVolume;
				this.flowerradioInstance.Play();
			}
			else
			{
				this.flowerradioInstance.Resume();
				this.flowerradioInstance.Volume = this.flowervol * this.sc.voiceVolume;
			}
			if (this.overtureInstance.State == SoundState.Stopped)
			{
				this.overtureInstance.IsLooped = true;
				this.overtureInstance.Volume = 0.3f * this.sc.mv;
				this.overtureInstance.Play();
			}
			else
			{
				this.overtureInstance.Resume();
				this.overtureInstance.Volume = 0.3f * this.sc.mv;
			}
			if (this.musictick == 0)
			{
				this.musictick = this.random.Next(100, 360) + 180;
			}
			this.musictick--;
			if (this.musictick < 1)
			{
				if (this.random.Next(1, 11) < 6)
				{
					this.sc.steeldrum1.Play(0.3f * this.sc.mv, 0f, 0f);
					this.musictick = 1620 + this.random.Next(100, 540) + 230;
					return;
				}
				this.sc.steeldrum2.Play(0.3f * this.sc.mv, 0f, 0f);
				this.musictick = 1620 + this.random.Next(100, 540) + 260;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0040857A File Offset: 0x0040677A
		private int range(int val, int max)
		{
			val = (val % max + max) % max;
			return val;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00408588 File Offset: 0x00406788
		private void textures(int x, int y, int xx, int yy)
		{
			this.tmake.Tv[x + y * this.bitmap].TextureCoordinate.X = (float)x / 36.125f + 0.11072665f;
			this.tmake.Tv[x + y * this.bitmap].TextureCoordinate.Y = (float)y / 36.125f + 0.11072665f;
			this.tmake.Tv[x + y * this.bitmap].TexWeights.X = this.texdata[xx + yy * 2000].TexWeights.X;
			this.tmake.Tv[x + y * this.bitmap].TexWeights.Y = this.texdata[xx + yy * 2000].TexWeights.Y;
			this.tmake.Tv[x + y * this.bitmap].TexWeights.W = this.texdata[xx + yy * 2000].TexWeights.Z;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x004086C4 File Offset: 0x004068C4
		private void makeFaciltyDirt()
		{
			float facility1hite = this.surface.facility1hite;
			float x = this.facility.facilityLocate.X;
			float y = this.facility.facilityLocate.Y;
			this.X = MathHelper.Clamp(1f - Math.Abs(facility1hite - 0f) / 2000f, 0f, 1f);
			this.Y = MathHelper.Clamp(1f - Math.Abs(facility1hite - 4000f) / 3980f, 0f, 1f);
			this.W = MathHelper.Clamp(1f - Math.Abs(facility1hite - 8000f) / 6000f, 0f, 1f);
			if (facility1hite >= 7000f)
			{
				this.W = 1f;
			}
			if (facility1hite <= 20f)
			{
				this.Y = 0.01f;
				this.X = 1f;
			}
			float num = this.X;
			num += this.Y;
			num += this.W;
			MathHelper.Clamp(num, 1f, 10f);
			this.X /= num;
			this.Y /= num;
			this.W /= num;
			this.pp = this.sc.GraphicsDevice.PresentationParameters;
			RenderTarget2D renderTarget2D = new RenderTarget2D(this.sc.GraphicsDevice, 500, 500, true, this.pp.BackBufferFormat, this.pp.DepthStencilFormat, 0, RenderTargetUsage.DiscardContents);
			this.sc.GraphicsDevice.SetRenderTarget(renderTarget2D);
			this.sc.GraphicsDevice.Clear(Color.Black);
			Color color = new Color(this.X, this.X, this.X, 1f);
			Color color2 = new Color(this.Y, this.Y, this.Y, 1f);
			Color color3 = new Color(this.W, this.W, this.W, 1f);
			int num2 = 250;
			int num3 = 2;
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			for (int i = 0; i < num3; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					this.spriteBatch.Draw(this.layer1, new Rectangle(i * num2, j * num2, num2, num2), color);
				}
			}
			for (int k = 0; k < num3; k++)
			{
				for (int l = 0; l < num3; l++)
				{
					this.spriteBatch.Draw(this.layer2, new Rectangle(k * num2, l * num2, num2, num2), color2);
				}
			}
			for (int m = 0; m < num3; m++)
			{
				for (int n = 0; n < num3; n++)
				{
					this.spriteBatch.Draw(this.layer4, new Rectangle(m * num2, n * num2, num2, num2), color3);
				}
			}
			this.spriteBatch.End();
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.facility.dirtrgb = renderTarget2D;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x004089F0 File Offset: 0x00406BF0
		private void UpdateTerrain(int cutoff, bool ignore)
		{
			int num = 0;
			int num2 = this.bitmap - 2;
			int num3 = 0;
			int num4 = 0;
			if (this.zgrid != this.lastzgrid && this.whatz != this.diffiz)
			{
				int num5 = this.range(this.zgrid + 1, this.bitmap - 1);
				int num6 = this.range(this.zgrid + 2, this.bitmap - 1);
				int num7 = this.range(this.zgrid - 1, this.bitmap - 1);
				int num8 = this.range(this.zgrid - 2, this.bitmap - 1);
				if ((this.zgrid > this.lastzgrid || (this.zgrid == 0 && this.lastzgrid == num2)) && (this.zgrid != num2 || this.lastzgrid != 0))
				{
					this.myposz++;
					if (this.myposz > 1999)
					{
						this.myposz = 0;
					}
				}
				else if ((this.zgrid < this.lastzgrid || (this.zgrid == num2 && this.lastzgrid == 0)) && (this.zgrid != 0 || this.lastzgrid != num2))
				{
					this.myposz--;
					if (this.myposz < 0)
					{
						this.myposz = 1999;
					}
				}
				bool flag = (this.zgrid > this.lastzgrid || (this.zgrid == 0 && this.lastzgrid == num2)) && (this.zgrid != num2 || this.lastzgrid != 0);
				bool flag2 = (this.zgrid < this.lastzgrid || (this.zgrid == num2 && this.lastzgrid == 0)) && (this.zgrid != 0 || this.lastzgrid != num2);
				if (flag || flag2)
				{
					if (flag)
					{
						num4 = this.myposz + (this.bitmap - 2) / 2 + 1;
						if (num4 > 1999)
						{
							num4 -= 2000;
						}
						if (num4 < 0)
						{
							num4 += 2000;
						}
					}
					if (flag2)
					{
						num4 = this.myposz - (this.bitmap - 2) / 2 + 2;
						if (num4 < 0)
						{
							num4 += 2000;
						}
						if (num4 < 0)
						{
							num4 += 2000;
						}
					}
					for (int i = 0; i < this.bitmap; i++)
					{
						if (flag)
						{
							this.tmake.Tv[i + this.zgrid * this.bitmap].Position = new Vector3(this.tmake.Tv[i + this.zgrid * this.bitmap].Position.X, -100000f, this.trnLoctn.Z);
							this.index = i + num7 * this.bitmap;
							num3 = this.myposx - (this.bitmap - 2) / 2 + this.range(i - this.lastxgrid, this.bitmap - 1) + 1;
							if (num3 > 1999)
							{
								num3 -= 2000;
							}
							if (num3 < 0)
							{
								num3 += 2000;
							}
							this.tmake.heightData[i, num7] = this.heightData[num3, num4];
							if (num7 == 0)
							{
								num = 1;
								this.tmake.heightData[i, this.bitmap - 1] = this.heightData[num3, num4];
							}
							float num9 = (float)this.heightData[num3, num4];
							if (i == this.lastxgrid)
							{
								num9 = -100000f;
							}
							this.tmake.Tv[this.index].Position = new Vector3(this.tmake.Tv[this.index].Position.X, num9, (float)(this.whatz + ((this.bitmap - 1) * this.gridscale / 2 - this.gridscale)));
							if (num8 == 1)
							{
								this.tmake.Tv[i + (this.bitmap - 1) * this.bitmap].Normal = this.tmake.Tv[i].Normal;
							}
							this.tmake.normals[i, num7] = this.normalData[num3, num4];
							this.tmake.Tv[i + num7 * this.bitmap].Normal = this.normalData[num3, num4];
							this.textures(i, num7, num3, num4);
							if (!ignore && i % 2 == 0)
							{
								this.rock.simpleObjectAdd(this.tmake.Tv[this.index].Position.X, -160f + this.tmake.Tv[this.index].Position.Z, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
							}
						}
						else if (flag2)
						{
							this.tmake.Tv[i + this.zgrid * this.bitmap].Position = new Vector3(this.tmake.Tv[i + this.zgrid * this.bitmap].Position.X, -100000f, this.trnLoctn.Z);
							this.index = i + num5 * this.bitmap;
							num3 = this.myposx - (this.bitmap - 2) / 2 + this.range(i - this.lastxgrid, this.bitmap - 1) + 1;
							if (num3 > 1999)
							{
								num3 -= 2000;
							}
							if (num3 < 0)
							{
								num3 += 2000;
							}
							this.tmake.heightData[i, num5] = this.heightData[num3, num4];
							if (num5 == 0)
							{
								num = 2;
								this.tmake.heightData[i, this.bitmap - 1] = this.heightData[num3, num4];
							}
							float num10 = (float)this.heightData[num3, num4];
							if (i == this.lastxgrid)
							{
								num10 = -100000f;
							}
							this.tmake.Tv[this.index].Position = new Vector3(this.tmake.Tv[this.index].Position.X, num10, (float)(this.whatz - ((this.bitmap - 1) * this.gridscale / 2 - this.gridscale)));
							if (num6 == num2)
							{
								this.tmake.Tv[i + (this.bitmap - 1) * this.bitmap].Normal = this.tmake.Tv[i].Normal;
							}
							this.tmake.normals[i, num5] = this.normalData[num3, num4];
							this.tmake.Tv[this.index].Normal = this.normalData[num3, num4];
							this.textures(i, num5, num3, num4);
							if (!ignore && i % 2 == 0)
							{
								this.rock.simpleObjectAdd(this.tmake.Tv[this.index].Position.X, 160f + this.tmake.Tv[this.index].Position.Z, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
							}
						}
						if (num > 0)
						{
							this.tmake.Tv[i + (this.bitmap - 1) * this.bitmap].Position = this.tmake.Tv[i].Position;
						}
						if (this.zgrid == 0)
						{
							this.tmake.Tv[i + (this.bitmap - 1) * this.bitmap].Position = this.tmake.Tv[i].Position;
						}
					}
				}
				this.diffiz = this.whatz;
				this.lastzgrid = this.zgrid;
			}
			num = 0;
			if (this.xgrid != this.lastxgrid && this.whatx != this.diffix)
			{
				int num11 = this.range(this.xgrid + 1, this.bitmap - 1);
				int num12 = this.range(this.xgrid + 2, this.bitmap - 1);
				int num13 = this.range(this.xgrid - 1, this.bitmap - 1);
				int num14 = this.range(this.xgrid - 2, this.bitmap - 1);
				if ((this.xgrid > this.lastxgrid || (this.xgrid == 0 && this.lastxgrid == num2)) && (this.xgrid != num2 || this.lastxgrid != 0))
				{
					this.myposx++;
					if (this.myposx > 1999)
					{
						this.myposx = 0;
					}
				}
				else if ((this.xgrid < this.lastxgrid || (this.xgrid == num2 && this.lastxgrid == 0)) && (this.xgrid != 0 || this.lastxgrid != num2))
				{
					this.myposx--;
					if (this.myposx < 0)
					{
						this.myposx = 1999;
					}
				}
				bool flag3 = (this.xgrid > this.lastxgrid || (this.xgrid == 0 && this.lastxgrid == num2)) && (this.xgrid != num2 || this.lastxgrid != 0);
				bool flag4 = (this.xgrid < this.lastxgrid || (this.xgrid == num2 && this.lastxgrid == 0)) && (this.xgrid != 0 || this.lastxgrid != num2);
				if (flag3 || flag4)
				{
					if (flag3)
					{
						num3 = this.myposx + (this.bitmap - 2) / 2 + 1;
						if (num3 > 1999)
						{
							num3 -= 2000;
						}
						if (num3 < 0)
						{
							num3 += 2000;
						}
					}
					if (flag4)
					{
						num3 = this.myposx - (this.bitmap - 2) / 2 + 2;
						if (num3 > 1999)
						{
							num3 -= 2000;
						}
						if (num3 < 0)
						{
							num3 += 2000;
						}
					}
					for (int j = 0; j < this.bitmap; j++)
					{
						if (flag3)
						{
							this.tmake.Tv[this.xgrid + j * this.bitmap].Position = new Vector3(this.trnLoctn.X, -100000f, this.tmake.Tv[this.xgrid + j * this.bitmap].Position.Z);
							this.index = num13 + j * this.bitmap;
							num4 = this.myposz - (this.bitmap - 2) / 2 + this.range(j - this.lastzgrid, this.bitmap - 1) + 1;
							if (num4 > 1999)
							{
								num4 -= 2000;
							}
							if (num4 < 0)
							{
								num4 += 2000;
							}
							this.tmake.heightData[num13, j] = this.heightData[num3, num4];
							if (num13 == 0)
							{
								num = 1;
								this.tmake.heightData[this.bitmap - 1, j] = this.heightData[num3, num4];
							}
							float num15 = (float)this.heightData[num3, num4];
							if (j == this.lastzgrid)
							{
								num15 = -100000f;
							}
							this.tmake.Tv[this.index].Position = new Vector3((float)(this.whatx + ((this.bitmap - 1) * this.gridscale / 2 - this.gridscale)), num15, this.tmake.Tv[this.index].Position.Z);
							if (num14 == 1)
							{
								this.tmake.Tv[this.bitmap - 1 + j * this.bitmap].Normal = this.tmake.Tv[j * this.bitmap].Normal;
							}
							this.tmake.normals[num13, j] = this.normalData[num3, num4];
							this.tmake.Tv[this.index].Normal = this.normalData[num3, num4];
							this.textures(num13, j, num3, num4);
							if (!ignore && j % 2 == 0)
							{
								this.rock.simpleObjectAdd(-160f + this.tmake.Tv[this.index].Position.X, this.tmake.Tv[this.index].Position.Z, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
							}
						}
						else if (flag4)
						{
							this.tmake.Tv[this.xgrid + j * this.bitmap].Position = new Vector3(this.trnLoctn.X, -100000f, this.tmake.Tv[this.xgrid + j * this.bitmap].Position.Z);
							this.index = num11 + j * this.bitmap;
							num4 = this.myposz - (this.bitmap - 2) / 2 + this.range(j - this.lastzgrid, this.bitmap - 1) + 1;
							if (num4 > 1999)
							{
								num4 -= 2000;
							}
							if (num4 < 0)
							{
								num4 += 2000;
							}
							this.tmake.heightData[num11, j] = this.heightData[num3, num4];
							if (num11 == 0)
							{
								num = 2;
								this.tmake.heightData[this.bitmap - 1, j] = this.heightData[num3, num4];
							}
							float num16 = (float)this.heightData[num3, num4];
							if (j == this.lastzgrid)
							{
								num16 = -100000f;
							}
							this.tmake.Tv[this.index].Position = new Vector3((float)(this.whatx - ((this.bitmap - 1) * this.gridscale / 2 - this.gridscale)), num16, this.tmake.Tv[this.index].Position.Z);
							if (num12 == num2)
							{
								this.tmake.Tv[this.bitmap - 1 + j * this.bitmap].Normal = this.tmake.Tv[j * this.bitmap].Normal;
							}
							this.tmake.normals[num11, j] = this.normalData[num3, num4];
							this.tmake.Tv[this.index].Normal = this.normalData[num3, num4];
							this.textures(num11, j, num3, num4);
							if (!ignore && j % 2 == 0)
							{
								this.rock.simpleObjectAdd(160f + this.tmake.Tv[this.index].Position.X, this.tmake.Tv[this.index].Position.Z, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
							}
						}
						if (num > 0)
						{
							this.tmake.Tv[this.bitmap - 1 + j * this.bitmap].Position = this.tmake.Tv[j * this.bitmap].Position;
						}
						if (this.xgrid == 0)
						{
							this.tmake.Tv[this.bitmap - 1 + j * this.bitmap].Position = this.tmake.Tv[j * this.bitmap].Position;
						}
					}
				}
				this.diffix = this.whatx;
				this.lastxgrid = this.xgrid;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00409AA8 File Offset: 0x00407CA8
		private void dropGems(ref gemstruct gem, int amt)
		{
			if (this.objIndex == 3)
			{
				this.emo = GameplayScreen.act.rocked;
			}
			float num = this.sc.gemDropScale / 2f * (float)amt;
			if (this.sc.gemDropScale > 10f)
			{
				num = 120f;
			}
			amt = (int)MathHelper.Clamp((float)((int)num), 5f, 120f);
			float gemDropScale = this.sc.gemDropScale;
			Vector3 gemDropPosition = this.sc.gemDropPosition;
			Vector3 vector = this.sc.gemDropVelocity;
			if (gem.Inst.Count >= gem.max - amt)
			{
				gem.Inst.RemoveRange(0, amt);
			}
			for (int i = 0; i < amt; i++)
			{
				float num2 = gemDropPosition.X + (float)this.random.Next(-900, 900) / 100f * gemDropScale;
				float num3 = gemDropPosition.Z + (float)this.random.Next(-900, 900) / 100f * gemDropScale;
				vector = Vector3.Normalize(vector);
				vector.X += (float)this.random.Next(-300, 300) / 100f;
				vector.Y += (float)this.random.Next(-300, 1200) / 100f;
				vector.Z += (float)this.random.Next(-300, 300) / 100f;
				float num4 = (float)this.random.Next(-900, 900) / 100f * gemDropScale;
				float num5;
				this.GetHeightOnly(ref this.tmake.heightData, new Vector3(num2, 0f, num3), out num5);
				gem.Inst.Add(new gemDupe(this.sc, new Vector3(num2, num5 + gemDropScale * 20f + num4, num3), vector, true, this.random.Next(2, 200)));
				int num6 = gem.Inst.Count - 1;
				gem.Inst[num6].onramp = 5;
				gem.Trans[num6].Trans = gem.Inst[num6].transform;
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00409D14 File Offset: 0x00407F14
		private void updateGems(ref gemstruct gem)
		{
			Vector2 vector = new Vector2(this.trnLoctn.X, this.trnLoctn.Z);
			for (int i = 0; i < gem.Inst.Count; i++)
			{
				int num = i;
				gem.uptheRamp(this.sc.equip[3], ref this.rover, gem.Inst[num], ref gem.xInst, ref gem.xCount, ref gem.xTrans, ref gem.xTrack);
				if (gem.Inst[num].move)
				{
					gem.Inst[num].Update(ref this.tmake.heightData);
					gem.Trans[num].Trans = gem.Inst[num].transform;
					Vector3 vector2 = Vector3.Transform(Vector3.Zero, gem.Inst[num].transform);
					if (Vector2.DistanceSquared(vector, new Vector2(vector2.X, vector2.Z)) > 1.225E+09f)
					{
						gem.Inst[num].move = false;
					}
				}
				else
				{
					gem.Inst.RemoveAt(num);
					i--;
				}
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00409E5C File Offset: 0x0040805C
		private void nearScooper(ref gemstruct gem, SoundEffect soundy)
		{
			if (gem.Inst.Count > gem.max - 1)
			{
				return;
			}
			for (int i = 0; i < gem.Inst.Count; i++)
			{
				Vector3 mypos = gem.Inst[i].mypos;
				if (this.sc.equip[3] == 1 && Vector3.Distance(mypos, this.bucketRegion1[1]) < 60f)
				{
					int num = 0;
					float num2 = 0f;
					float num3 = 0f;
					Vector2 vector = new Vector2(this.bucketRegion1[3].X, this.bucketRegion1[3].Z);
					Vector2 vector2 = Vector2.Zero;
					Vector2 vector3 = new Vector2(mypos.X, mypos.Z);
					for (int j = 0; j < 4; j++)
					{
						vector2 = vector;
						vector = new Vector2(this.bucketRegion1[j].X, this.bucketRegion1[j].Z);
						Vector2 vector4 = vector3 - vector2;
						Vector2 vector5 = vector3 - vector;
						float num4 = vector4.X * vector5.Y - vector5.X * vector4.Y;
						float num5 = (float)Math.Acos((double)(Vector2.Dot(vector4, vector5) / (vector4.Length() * vector5.Length())));
						num5 = ((num4 > 0f) ? num5 : (-num5));
						if (Math.Abs(num5) > num3)
						{
							num3 = Math.Abs(num5);
							num = j;
						}
						if (j > 0)
						{
							num2 += num5;
						}
					}
					if (Math.Abs(num2) >= 3.1415927f && num != 3 && gem.Inst.Count < gem.max)
					{
						float scaler = gem.Inst[i].scaler;
						Matrix myRot = gem.Inst[i].myRot;
						gem.Inst.Add(new gemDupe(this.sc, mypos, Vector3.Zero, false, this.random.Next(5, 300)));
						int num6 = gem.Inst.Count - 1;
						gem.Inst[num6].onramp = 1;
						gem.Inst[num6].move = true;
						gem.Inst[num6].myRot = myRot;
						gem.Inst[num6].scaler = scaler;
						Vector3 vector6 = this.bucketRegion1[0];
						Vector3 vector7 = this.bucketRegion1[1];
						Vector3 vector8 = this.bucketRegion1[2];
						Vector3 vector9 = this.bucketRegion1[3];
						Vector3 vector10 = this.origRegion1[0] * 30f;
						Vector3 vector11 = this.origRegion1[1] * 30f;
						Vector3 vector12 = this.origRegion1[2] * 30f;
						Vector3 vector13 = this.origRegion1[3] * 30f;
						Vector3 mypos2 = gem.Inst[num6].mypos;
						float num7 = Vector3.Distance(vector6, vector7);
						float num8 = Vector3.Distance(vector7, mypos2);
						float num9 = Vector3.Distance(vector6, mypos2);
						float num10 = Vector3.Distance(vector7, vector8);
						float num11 = Vector3.Distance(vector6, vector9);
						float num12 = Vector3.Distance(vector8, mypos2);
						Vector3.Distance(vector9, mypos2);
						Vector3.Distance(vector8, vector9);
						float num13 = (num7 * num7 + num8 * num8 - num9 * num9) / (2f * num7 * num8);
						float num14 = (float)Math.Acos((double)num13);
						float num15 = (float)Math.Sin((double)num14) * num8;
						float num16 = num15 / num10;
						float num17 = num15 / num11;
						Vector3 vector14 = Vector3.Lerp(vector7, vector8, num16);
						Vector3 vector15 = Vector3.Lerp(vector11, vector12, num16);
						Vector3 vector16 = Vector3.Lerp(vector6, vector9, num17);
						Vector3 vector17 = Vector3.Lerp(vector10, vector13, num17);
						num13 = (num10 * num10 + num12 * num12 - num8 * num8) / (2f * num10 * num12);
						float num18 = (float)Math.Acos((double)num13);
						float num19 = (float)Math.Sin((double)num18) * num12;
						float num20 = num19 / Vector3.Distance(vector14, vector16);
						gem.Inst[num6].mypos = Vector3.Lerp(vector15, vector17, num20);
						gem.Inst[num6].oldpos = gem.Inst[num6].mypos;
						gem.Inst[num6].myRot *= Matrix.CreateRotationY(-this.rover.facingDirection) * Matrix.CreateRotationX(0.44f);
						gem.Inst[num6].velocity += new Vector3((float)this.random.Next(-100, 100) / 300f, (float)this.random.Next(80, 200) / 300f, (float)this.random.Next(-100, 100) / 300f);
						soundy.Play(this.sc.ev, (float)this.random.Next(-30, 70) / 100f, 0f);
						gem.Inst.RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0040A404 File Offset: 0x00408604
		public void clearDumper()
		{
			if (gemstruct.totalGems > 0)
			{
				gemstruct.totalGems = 0;
				this.refineShale = this.shale1.xCount;
				this.refineRuby = this.shale2.xCount;
				this.refineBlue = this.shale3.xCount;
				this.shale2.xTrack = 0;
				this.shale2.xCount = 0;
				Array.Clear(this.shale2.xInst, 0, this.shale2.xInst.Length);
				Array.Clear(this.shale2.xTrans, 0, this.shale2.xInst.Length);
				this.shale1.xTrack = 0;
				this.shale1.xCount = 0;
				Array.Clear(this.shale1.xInst, 0, this.shale1.xInst.Length);
				Array.Clear(this.shale1.xTrans, 0, this.shale1.xInst.Length);
				this.shale3.xTrack = 0;
				this.shale3.xCount = 0;
				Array.Clear(this.shale3.xInst, 0, this.shale3.xInst.Length);
				Array.Clear(this.shale3.xTrans, 0, this.shale3.xInst.Length);
				return;
			}
			this.sc.confirm.Play(this.sc.ev, 0f, 0f);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0040A578 File Offset: 0x00408778
		public void openHatch(bool force)
		{
			if (force || (!this.lander.onDropship && this.lander.impact == 1 && this.lander.diff <= 40f && this.lander.door1 == 1 && this.lander.normal.Y > 0.9f))
			{
				this.lander.commandFlag = 1;
				this.sc.ramp.Play(this.sc.ev, 0f, 0f);
				this.rover.directme = this.lander.directme;
				if (this.rover.directme < 0f)
				{
					this.rover.directme += 6.28f;
				}
				this.rover.directme = this.lander.directme - MathHelper.ToRadians(-135f);
				if (this.rover.directme < 0f)
				{
					this.rover.directme += 6.28f;
				}
				if (!force)
				{
					this.rover.position.X = this.lander.position.X;
					this.rover.position.Y = this.lander.position.Y;
					this.rover.position.Z = this.lander.position.Z;
					this.rover.facingDirection = this.rover.directme;
					this.rover.orientation = Matrix.CreateRotationY(this.rover.directme);
					this.rover.orientation.Up = this.lander.orientation.Up;
					this.rover.orientation.Right = Vector3.Cross(this.rover.orientation.Forward, this.rover.orientation.Up);
					this.rover.orientation.Right = Vector3.Normalize(this.rover.orientation.Right);
					this.rover.orientation.Forward = Vector3.Cross(this.rover.orientation.Up, this.rover.orientation.Right);
					this.rover.orientation.Forward = Vector3.Normalize(this.rover.orientation.Forward);
					this.rover.onramp = 3;
					this.lastpos = this.rover.position;
					this.rover.grav = Vector3.Zero;
					this.rover.movement = Vector3.Zero;
					this.rover.thumb = 0f;
					this.rover.righttrig = 0f;
					return;
				}
			}
			else
			{
				this.sc.warning.Play(this.sc.ev, -0.3f, 0f);
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0040A894 File Offset: 0x00408A94
		private void addPrint(Matrix mm)
		{
			this.prints.drift[this.prints.stainIndex] = 400f;
			this.hitstreamTemp.Trans = mm;
			this.hitstreamTemp.Fade = 1f;
			this.prints.stainTrans[this.prints.stainIndex] = this.hitstreamTemp;
			this.prints.stainIndex = this.prints.stainIndex + 1;
			if (this.prints.stainIndex > this.prints.stainCapacity - 1)
			{
				this.prints.stainIndex = 0;
			}
			this.prints.stainMax = this.prints.stainMax + 1;
			if (this.prints.stainMax > this.prints.stainCapacity - 1)
			{
				this.prints.stainMax = this.prints.stainCapacity;
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0040A97C File Offset: 0x00408B7C
		private void updatePrints()
		{
			for (int i = 0; i < this.prints.stainMax; i++)
			{
				this.prints.drift[i] -= 1f;
				if (this.prints.drift[i] < 1f)
				{
					GameplayScreen.hitStream[] stainTrans = this.prints.stainTrans;
					int num = i;
					stainTrans[num].Fade = stainTrans[num].Fade - 0.006f;
				}
				this.prints.stainTrans[i].Trans = this.prints.stainTrans[i].Trans;
				if (this.prints.stainTrans[i].Fade <= 0f)
				{
					this.prints.drift[i] = this.prints.drift[this.prints.stainMax - 1];
					this.prints.stainTrans[i].Fade = this.prints.stainTrans[this.prints.stainMax - 1].Fade;
					this.prints.stainTrans[i].Trans = this.prints.stainTrans[this.prints.stainMax - 1].Trans;
					this.prints.stainMax = this.prints.stainMax - 1;
					if (this.prints.stainMax < 0)
					{
						this.prints.stainMax = 0;
					}
					if (this.prints.stainIndex > this.prints.stainMax - 1)
					{
						this.prints.stainIndex = this.prints.stainIndex - 1;
					}
					if (this.prints.stainIndex < 0)
					{
						this.prints.stainIndex = 0;
					}
				}
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0040AB54 File Offset: 0x00408D54
		private void removePoly(int x, int z)
		{
			int num = (int)((float)((z + this.gridscale / 2) / this.gridscale % this.unit) + 1.5f * (float)this.unit) % this.unit;
			int num2 = (int)((float)((x + this.gridscale / 2) / this.gridscale % this.unit) + 1.5f * (float)this.unit) % this.unit;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.X = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.Y = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.Z = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.W = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].Position.Y = -5000f;
			num--;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.X = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.Y = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.Z = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.W = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].Position.Y = -5000f;
			num--;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.X = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.Y = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.Z = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].TexWeights.W = -5000f;
			this.tmake.Tv[num2 + num * this.bitmap].Position.Y = -5000f;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0040AE30 File Offset: 0x00409030
		private void rebuildFacility()
		{
			this.GetHeightOnly(ref this.tmake.heightData, new Vector3(this.facility.facilityLocate.X, 0f, this.facility.facilityLocate.Y), out this.groundHeight);
			Facility.offset = new Vector3(-2250f + this.facility.facilityLocate.X, this.groundHeight - 1441f / this.facility.scaler, -4337.5f + this.facility.facilityLocate.Y);
			this.facility.buildFloor(3);
			this.facility.updateGateCollision();
			int num = (int)this.facility.facilityLocate.X;
			int num2 = (int)this.facility.facilityLocate.Y;
			this.removePoly(num, num2);
			this.facility.rebuild = false;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0040AF1C File Offset: 0x0040911C
		public bool KMdown(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.mouseState.LeftButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.mouseState.MiddleButton == ButtonState.Pressed;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.mouseState.RightButton == ButtonState.Pressed;
			}
			else if (k == Keys.Print)
			{
				flag = this.mouseState.XButton1 == ButtonState.Pressed;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.mouseState.XButton2 == ButtonState.Pressed;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k);
			}
			return flag;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0040AFB0 File Offset: 0x004091B0
		public bool KMreleased(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = this.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0040B044 File Offset: 0x00409244
		public bool KMtoggle(Keys k)
		{
			bool flag;
			if (k == Keys.VolumeDown)
			{
				flag = this.mouseState.LeftButton == ButtonState.Pressed && this.prevMouse.LeftButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeMute)
			{
				flag = this.mouseState.MiddleButton == ButtonState.Pressed && this.prevMouse.MiddleButton == ButtonState.Released;
			}
			else if (k == Keys.VolumeUp)
			{
				flag = this.mouseState.RightButton == ButtonState.Pressed && this.prevMouse.RightButton == ButtonState.Released;
			}
			else if (k == Keys.Print)
			{
				flag = this.mouseState.XButton1 == ButtonState.Pressed && this.prevMouse.XButton1 == ButtonState.Released;
			}
			else if (k == Keys.PrintScreen)
			{
				flag = this.mouseState.XButton2 == ButtonState.Pressed && this.prevMouse.XButton2 == ButtonState.Released;
			}
			else
			{
				flag = this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
			}
			return flag;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0040B144 File Offset: 0x00409344
		public bool Ktoggle(Keys k)
		{
			return this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0040B170 File Offset: 0x00409370
		public bool Kdown(Keys k)
		{
			return this.keyState.IsKeyDown(k);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0040B330 File Offset: 0x00409530
		public override void HandleInput(InputState input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			base.ControllingPlayer = new PlayerIndex?(this.sc.playerindex);
			this.myplayer = this.sc.playerindex;
			this.gamePadState = input.CurrentGamePadStates[(int)this.sc.playerindex];
			bool flag = !this.gamePadState.IsConnected && input.GamePadWasConnected[(int)this.sc.playerindex];
			this.prevkeyState = input.lastKeyState;
			this.keyState = input.currentKeyState;
			this.prevMouse = this.mouseState;
			this.mouseState = Mouse.GetState();
			if (this.myframe % 120 == 0)
			{
				this.sc.centerWindow();
			}
			if (!this.editcam)
			{
				Mouse.SetPosition((int)this.sc.winCorner.X, (int)this.sc.winCorner.Y);
			}
			this.sc.mymouse.X = (float)this.mouseState.X;
			this.sc.mymouse.Y = (float)this.mouseState.Y;
			this.mouseX = (float)(this.mouseState.X - (int)this.sc.winCorner.X);
			this.mouseY = (float)(this.mouseState.Y - (int)this.sc.winCorner.Y);
			if (this.delayinput)
			{
				this.prevMouse = this.mouseState;
				this.prevkeyState = this.keyState;
				this.delayinput = false;
				this.sc.centerWindow();
			}
			if (Facility.openConstruction)
			{
				this.editcam = false;
				GamePad.SetVibration(this.myplayer, 0f, 0f);
				Facility.openConstruction = false;
				this.sc.AddScreen(new Construction(ref this.facility), base.ControllingPlayer);
			}
			if (!this.Ktoggle(this.sc.escape_key) && !input.IsPauseGame(base.ControllingPlayer) && !flag && !input.IsMenuGame(base.ControllingPlayer))
			{
				if (this.vehicleindex != 3 && this.Ktoggle(this.sc.f1_key))
				{
					this.editcam = !this.editcam;
					this.camadjust = false;
					this.sc.tick.Play(this.sc.ev, 0.2f, 0f);
					if (!this.editcam)
					{
						this.sc.SaveSpacePrefs();
					}
				}
				if (this.editcam)
				{
					if (this.vehicleindex == 3)
					{
						this.editcam = false;
					}
					this.camadjust = false;
					this.sc.Game.IsMouseVisible = true;
					if (this.Ktoggle(Keys.Tab) || this.KMtoggle(this.sc.tab_key))
					{
						int num = 0;
						if (this.vehicleindex == 1)
						{
							this.sc.roverindex++;
							if (this.sc.roverindex > 2)
							{
								this.sc.roverindex = 0;
							}
							this.yy = this.sc.roverheight[this.sc.roverindex];
							this.rot = this.sc.roverradian[this.sc.roverindex];
							num = this.sc.roverindex + 1;
						}
						if (this.vehicleindex == 2)
						{
							this.sc.landerindex++;
							if (this.sc.landerindex > 2)
							{
								this.sc.landerindex = 0;
							}
							this.yy = this.sc.landerheight[this.sc.landerindex];
							this.rot = this.sc.landerheight[this.sc.landerindex];
							num = this.sc.landerindex + 1;
						}
						if (this.memoTimer <= 0 || this.memoIcon == 7)
						{
							this.memoTimer = 150;
							this.memoIcon = 7;
							GameplayScreen.memo.Length = 0;
							GameplayScreen.memo.Append("Camera " + num.ToString());
						}
					}
					if (this.vehicleindex == 1)
					{
						Rectangle rectangle = new Rectangle((int)this.menuposition.X, (int)this.menuposition.Y, this.rovercamedit.Width, this.rovercamedit.Height);
						if (!this.queryButton2(rectangle))
						{
							this.camadjust = true;
							this.sc.Game.IsMouseVisible = false;
							this.buttonchoice = this.buttonhand;
						}
						rectangle = new Rectangle((int)(this.pointbox1[0].X + this.menuposition.X), (int)(this.pointbox1[0].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle) && this.KMtoggle(this.sc.lmb_key))
						{
							if (this.sc.roverrotlock == 0)
							{
								this.sc.roverradian[0] = this.sc.roverradian[0] + this.rover.facingDirection;
								this.sc.roverradian[1] = this.sc.roverradian[1] + this.rover.facingDirection;
								this.sc.roverradian[2] = this.sc.roverradian[2] + this.rover.facingDirection;
							}
							if (this.sc.roverrotlock == 2)
							{
								this.sc.roverradian[0] = this.sc.roverradian[0] - this.rover.facingDirection;
								this.sc.roverradian[1] = this.sc.roverradian[1] - this.rover.facingDirection;
								this.sc.roverradian[2] = this.sc.roverradian[2] - this.rover.facingDirection;
							}
							if (this.sc.roverradian[0] > 6.283185f)
							{
								this.sc.roverradian[0] -= 6.283185f;
							}
							if (this.sc.roverradian[0] < 0f)
							{
								this.sc.roverradian[0] += 6.283185f;
							}
							if (this.sc.roverradian[1] > 6.283185f)
							{
								this.sc.roverradian[1] -= 6.283185f;
							}
							if (this.sc.roverradian[1] < 0f)
							{
								this.sc.roverradian[1] += 6.283185f;
							}
							if (this.sc.roverradian[2] > 6.283185f)
							{
								this.sc.roverradian[2] -= 6.283185f;
							}
							if (this.sc.roverradian[2] < 0f)
							{
								this.sc.roverradian[2] += 6.283185f;
							}
							this.rot = this.sc.roverradian[this.sc.roverindex];
							this.sc.roverrotlock++;
							if (this.sc.roverrotlock == 1 && this.softlockMess1)
							{
								messagebox messagebox = new messagebox("Camera Softlock Selected\nHold Spacebar While Driving", 0, 0);
								this.sc.AddScreen(messagebox, null);
								this.softlockMess1 = false;
							}
							if (this.sc.roverrotlock > 2)
							{
								this.sc.roverrotlock = 0;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						rectangle = new Rectangle((int)(this.pointbox1[1].X + this.menuposition.X), (int)(this.pointbox1[1].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle) && this.KMtoggle(this.sc.lmb_key))
						{
							this.sc.roverhitelock++;
							if (this.sc.roverhitelock == 1 && this.softlockMess1)
							{
								messagebox messagebox2 = new messagebox("Camera Softlock Selected\nHold Spacebar While Driving", 0, 0);
								this.sc.AddScreen(messagebox2, null);
								this.softlockMess1 = false;
							}
							if (this.sc.roverhitelock > 2)
							{
								this.sc.roverhitelock = 0;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						rectangle = new Rectangle((int)(this.pointbox1[2].X + this.menuposition.X), (int)(this.pointbox1[2].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle) && this.KMtoggle(this.sc.lmb_key))
						{
							if (this.sc.space_rinvertX == -1)
							{
								this.sc.space_rinvertX = 1;
							}
							else
							{
								this.sc.space_rinvertX = -1;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						rectangle = new Rectangle((int)(this.pointbox1[3].X + this.menuposition.X), (int)(this.pointbox1[3].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle) && this.KMtoggle(this.sc.lmb_key))
						{
							if (this.sc.space_rinvertY == -1)
							{
								this.sc.space_rinvertY = 1;
							}
							else
							{
								this.sc.space_rinvertY = -1;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						float num2 = (float)((this.mouseState.X - this.prevMouse.X) * 4);
						float num3 = (float)((this.mouseState.Y - this.prevMouse.Y) * 4);
						float num4 = 0f;
						if (this.camadjust && this.KMdown(this.sc.lmb_key))
						{
							this.buttonchoice = this.buttonhand;
							num4 = -num2 * (0.0009f * this.sc.space_rsentivityX) * (float)this.sc.space_rinvertX;
						}
						this.sc.roverradian[this.sc.roverindex] = this.rot;
						this.rot += num4;
						if (this.rot > 6.283185f)
						{
							this.rot = 0f;
						}
						if (this.rot < 0f)
						{
							this.rot = 6.283185f;
						}
						this.aUppy = Vector3.Up;
						if (this.sc.roverrotlock > 0)
						{
							this.myrotter = this.rot - this.rover.facingDirection;
							if (this.myrotter > 6.283185f)
							{
								this.myrotter -= 6.283185f;
							}
							if (this.myrotter < 0f)
							{
								this.myrotter += 6.283185f;
							}
						}
						else
						{
							this.myrotter = this.rot;
						}
						if (this.camadjust && this.KMdown(this.sc.rmb_key))
						{
							this.buttonchoice = this.buttonzoom;
							this.sc.roverdist[this.sc.roverindex] += num3 * 2f;
							this.sc.roverdist[this.sc.roverindex] = MathHelper.Clamp(this.sc.roverdist[this.sc.roverindex], 50f, 2200f);
						}
						this.xx = (float)Math.Sin((double)this.myrotter) * this.sc.roverdist[this.sc.roverindex];
						this.zz = (float)(-(float)Math.Cos((double)this.myrotter)) * this.sc.roverdist[this.sc.roverindex];
						this.aCampos.X = this.rover.position.X + this.xx;
						this.aCampos.Y = this.rover.position.Y + this.yy;
						this.aCampos.Z = this.rover.position.Z + this.zz;
						float num5;
						this.GetHeightOnly(ref this.tmake.heightData, this.aCampos, out num5);
						if (this.aCampos.Y < num5 + (float)this.roverY)
						{
							this.aCampos.Y = num5 + (float)this.roverY;
							this.yy = this.aCampos.Y - this.rover.position.Y;
						}
						if (this.camadjust && this.KMdown(this.sc.lmb_key))
						{
							this.yy += -num3 * 0.4f * this.sc.space_rsentivityY * (float)this.sc.space_rinvertY;
						}
						this.yy = MathHelper.Clamp(this.yy, -300f, 2f * this.sc.roverdist[this.sc.roverindex]);
						this.sc.roverheight[this.sc.roverindex] = this.yy;
						if (this.yy != this.sc.roverheight[this.sc.roverindex])
						{
							if (this.yy > this.sc.roverheight[this.sc.roverindex])
							{
								this.yy -= 5f;
							}
							if (this.yy < this.sc.roverheight[this.sc.roverindex])
							{
								this.yy += 5f;
							}
							if (Math.Abs(this.sc.roverheight[this.sc.roverindex] - this.yy) <= 6f)
							{
								this.yy = this.sc.roverheight[this.sc.roverindex];
							}
						}
						this.aCamtarget.X = this.rover.position.X;
						this.aCamtarget.Y = this.rover.position.Y + 30f;
						this.aCamtarget.Z = this.rover.position.Z;
						if (this.sc.roverdist[this.sc.roverindex] < 110f)
						{
							this.rover.dashcam = 1;
							this.myrotter = 3.1415925f - this.rover.facingDirection;
							if (this.myrotter < 0f)
							{
								this.myrotter += 6.283185f;
							}
							if (this.sc.vehiclelerp >= 1f)
							{
								this.rover.dashcam = 1;
							}
							this.aCampos = this.rover.position + Vector3.Transform(new Vector3(0f, 3f * this.rover.scoopx + 64f, -35f), this.rover.orientation);
							this.aCamtarget = this.rover.position + Vector3.Transform(new Vector3(0f, 0f, -(this.rover.scoopx * 600f) - 350f), this.rover.orientation);
							this.aUppy = Vector3.Normalize(Vector3.Transform(new Vector3(0f, 10000f, 0f), this.rover.orientation));
						}
						else
						{
							this.rover.dashcam = 0;
						}
					}
					if (this.vehicleindex == 2)
					{
						Rectangle rectangle2 = new Rectangle((int)this.menuposition.X, (int)this.menuposition.Y, this.landercamedit.Width, this.landercamedit.Height);
						if (!this.queryButton2(rectangle2))
						{
							this.camadjust = true;
							this.sc.Game.IsMouseVisible = false;
							this.buttonchoice = this.buttonhand;
						}
						rectangle2 = new Rectangle((int)(this.pointbox1[0].X + this.menuposition.X), (int)(this.pointbox1[0].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle2) && this.KMtoggle(this.sc.lmb_key))
						{
							this.sc.landerrotlock++;
							if (this.sc.landerrotlock == 1 && this.softlockMess2)
							{
								messagebox messagebox3 = new messagebox("Camera Softlock Selected\nHold Spacebar While Flying", 0, 0);
								this.sc.AddScreen(messagebox3, null);
								this.softlockMess2 = false;
							}
							if (this.sc.landerrotlock > 2)
							{
								this.sc.landerrotlock = 0;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						rectangle2 = new Rectangle((int)(this.pointbox1[1].X + this.menuposition.X), (int)(this.pointbox1[1].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle2) && this.KMtoggle(this.sc.lmb_key))
						{
							this.sc.landerhitelock++;
							if (this.sc.landerhitelock == 1 && this.softlockMess2)
							{
								messagebox messagebox4 = new messagebox("Camera Softlock Selected\nHold Spacebar While Flying", 0, 0);
								this.sc.AddScreen(messagebox4, null);
								this.softlockMess2 = false;
							}
							if (this.sc.landerhitelock > 2)
							{
								this.sc.landerhitelock = 0;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						rectangle2 = new Rectangle((int)(this.pointbox1[2].X + this.menuposition.X), (int)(this.pointbox1[2].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle2) && this.KMtoggle(this.sc.lmb_key))
						{
							if (this.sc.space_invertX == -1)
							{
								this.sc.space_invertX = 1;
							}
							else
							{
								this.sc.space_invertX = -1;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						rectangle2 = new Rectangle((int)(this.pointbox1[3].X + this.menuposition.X), (int)(this.pointbox1[3].Y + this.menuposition.Y), 23, 24);
						if (this.queryButton2(rectangle2) && this.KMtoggle(this.sc.lmb_key))
						{
							if (this.sc.space_invertY == -1)
							{
								this.sc.space_invertY = 1;
							}
							else
							{
								this.sc.space_invertY = -1;
							}
							this.sc.tick.Play(this.sc.ev, 0f, 0f);
						}
						float num6 = (float)((this.mouseState.X - this.prevMouse.X) * 4);
						float num7 = (float)((this.mouseState.Y - this.prevMouse.Y) * 4);
						float num8 = 0f;
						if (this.camadjust && this.KMdown(this.sc.lmb_key))
						{
							this.buttonchoice = this.buttonhand;
							num8 = -num6 * (0.00045f * this.sc.space_sentivityX) * (float)this.sc.space_invertX;
							this.sc.landerradian[0] = this.rot;
							this.sc.landerradian[1] = this.rot;
							this.sc.landerradian[2] = this.rot;
						}
						this.rot += num8;
						if (this.camadjust && this.KMdown(this.sc.rmb_key))
						{
							this.buttonchoice = this.buttonzoom;
							this.sc.landerdist[this.sc.landerindex] += num7 * 2f;
							this.sc.landerdist[this.sc.landerindex] = MathHelper.Clamp(this.sc.landerdist[this.sc.landerindex], 250f, 3500f);
						}
						this.xx = (float)Math.Sin((double)this.rot) * this.sc.landerdist[this.sc.landerindex];
						this.zz = (float)(-(float)Math.Cos((double)this.rot)) * this.sc.landerdist[this.sc.landerindex];
						this.yy = MathHelper.Clamp(this.yy, -800f, this.sc.landerdist[this.sc.landerindex] * 2f);
						Vector3 vector = new Vector3(this.xx, this.yy, this.zz);
						this.aCampos = this.lander.position + vector;
						float num9;
						this.GetHeightOnly(ref this.tmake.heightData, this.aCampos, out num9);
						if (this.aCampos.Y < num9 + (float)this.landerY)
						{
							this.aCampos.Y = num9 + (float)this.landerY;
							this.yy = this.aCampos.Y - this.lander.position.Y;
						}
						if (this.camadjust && this.KMdown(this.sc.lmb_key))
						{
							this.yy += -num7 * 0.35f * this.sc.space_sentivityY * (float)this.sc.space_invertY;
						}
						this.yy = MathHelper.Clamp(this.yy, -800f, this.sc.landerdist[this.sc.landerindex] * 2f);
						this.sc.landerheight[this.sc.landerindex] = this.yy;
						if (this.yy != this.sc.landerheight[this.sc.landerindex])
						{
							if (this.yy > this.sc.landerheight[this.sc.landerindex])
							{
								this.yy -= 5f;
							}
							if (this.yy < this.sc.landerheight[this.sc.landerindex])
							{
								this.yy += 5f;
							}
							if (Math.Abs(this.sc.landerheight[this.sc.landerindex] - this.yy) <= 6f)
							{
								this.yy = this.sc.landerheight[this.sc.landerindex];
							}
						}
						this.aCamtarget.X = this.lander.position.X;
						this.aCamtarget.Y = this.lander.position.Y;
						this.aCamtarget.Z = this.lander.position.Z;
					}
					this.viewMatrix = Matrix.CreateLookAt(this.aCampos, this.aCamtarget, this.aUppy);
				}
				else
				{
					this.sc.Game.IsMouseVisible = false;
					if (this.sc.developer && this.KMtoggle(Keys.F7))
					{
						this.sc.cheat_astro = !this.sc.cheat_astro;
					}
					if (this.sc.cheat_astro && this.Ktoggle(Keys.F5))
					{
						Facility.openConstruction = true;
					}
					if (this.sc.cheat_astro && !this.nearAstro && (this.Ktoggle(this.sc.space_key) || (this.gamePadState.Buttons.X == ButtonState.Pressed && this.prevstate.Buttons.X == ButtonState.Released)))
					{
						if (!Facility.inFacility)
						{
							this.sc.astronaut.dropAstronaut(this.myframe, new Vector2(this.trnLoctn.X + 150f, this.trnLoctn.Z), 1, astroDupe.emotion.safe);
						}
						else
						{
							float num10 = (float)this.random.Next(-300, 300) / 50f;
							this.sc.astronaut.dropFacilityAstronaut(this.myframe, new Vector2(this.trnLoctn.X, this.trnLoctn.Z), astroDupe.emotion.underground, num10);
						}
					}
					bool flag2 = false;
					if (this.vehicleindex == 2)
					{
						this.rover.dashcam = 0;
						float num11 = 0.96f;
						if (this.sc.usingMouse)
						{
							num11 = 1f;
							this.overlay.fuelDec -= this.lander.rightTrigger / 100f;
						}
						else if (this.lander.rightTrigger >= num11)
						{
							this.overlay.fuelDec -= this.lander.speed / 60f;
						}
						else
						{
							this.overlay.fuelDec -= this.lander.rightTrigger / 140f;
						}
						if (this.overlay.fuelDec < 0f)
						{
							this.overlay.fuelDec = 1f;
							this.overlay.fuelAMT--;
						}
						this.lander.thrust = (float)Math.Pow((double)((float)this.overlay.fuelAMT / 100f), 0.20000000298023224) * 1.4f;
						if (this.lander.rightTrigger >= num11)
						{
							this.landerDrips();
							if (!this.sc.usingMouse)
							{
								this.lander.thrust = this.lander.maxthrust;
							}
						}
						else
						{
							this.lastleg1 = Vector3.Zero;
							this.lastleg2 = Vector3.Zero;
							this.lastleg3 = Vector3.Zero;
							this.lastleg4 = Vector3.Zero;
						}
						if (this.overlay.fuelAMT <= 4)
						{
							this.lander.thrust = 0.3f;
						}
						if (this.myframe % 2 == 0 && this.refineShale > 0)
						{
							if (this.objIndex == 4)
							{
								this.emo = GameplayScreen.act.refined;
							}
							this.refineShale--;
							this.overlay.fuelTick += 0.4f;
						}
						if (this.myframe % 5 == 0 && this.refineShale <= 0 && this.refineRuby > 0)
						{
							if (this.objIndex == 4)
							{
								this.emo = GameplayScreen.act.refined;
							}
							this.refineRuby--;
							this.overlay.fuelTick += 1.5f;
						}
						if (this.myframe % 6 == 0 && this.refineShale <= 0 && this.refineRuby <= 0 && this.refineBlue > 0)
						{
							if (this.objIndex == 4)
							{
								this.emo = GameplayScreen.act.refined;
							}
							this.refineBlue--;
							this.overlay.fuelTick += 2.5f;
						}
						if (this.overlay.fuelTick > 1f)
						{
							this.overlay.fuelAMT++;
							float num12 = (float)this.overlay.fuelAMT / 80f - 0.3f;
							this.overlay.fuelTick = 0f;
							if (this.refineShale > 0)
							{
								this.sc.tick.Play(this.sc.ev * 0.6f, num12, 0f);
							}
							else if (this.refineRuby > 0)
							{
								this.sc.boulderhit2.Play(this.sc.ev, num12, 0f);
							}
							else
							{
								this.sc.boulderhit4.Play(this.sc.ev, num12, 0f);
							}
						}
						if (this.nearfarm)
						{
							this.lander.HandleInput(ref this.mouseState, ref this.prevMouse, ref this.keyState, ref this.prevkeyState, ref this.gamePadState, ref this.heights, ref this.tmake.normals);
						}
						else
						{
							this.lander.HandleInput(ref this.mouseState, ref this.prevMouse, ref this.keyState, ref this.prevkeyState, ref this.gamePadState, ref this.tmake.heightData, ref this.tmake.normals);
						}
						if (this.lander.normalizedSpeed > 0f)
						{
							float normalizedSpeed = this.lander.normalizedSpeed;
							if (this.lander.lander2ground < 2000f || this.lander.onDropship)
							{
								this.ejecta((int)(normalizedSpeed * 250f));
							}
							if (this.landerEngineI.State == SoundState.Stopped)
							{
								this.landerEngineI.IsLooped = true;
								this.landerEngineI.Pitch = normalizedSpeed * 2f - 1f;
								this.landerEngineI.Volume = normalizedSpeed * this.sc.ev;
								this.landerEngineI.Play();
							}
							else
							{
								this.landerEngineI.Resume();
								this.landerEngineI.Pitch = normalizedSpeed * 2f - 1f;
								this.landerEngineI.Volume = normalizedSpeed * this.sc.ev;
							}
							float num13 = 1f - MathHelper.Clamp(this.lander.lander2ground / 1500f, 0f, 1f);
							if (num13 > 0f && this.sc.vibroSetting == 1)
							{
								GamePad.SetVibration(this.myplayer, (normalizedSpeed / 2.5f + 0.1f) * num13, (normalizedSpeed / 2.5f + 0.12f) * num13);
							}
						}
						else
						{
							if (this.vibrateLander == 0f && this.vibrateControl < 3)
							{
								GamePad.SetVibration(this.myplayer, 0f, 0f);
							}
							if (this.landerEngineI.State == SoundState.Playing)
							{
								this.landerEngineI.Pause();
							}
						}
						if (this.lander.rampSwitch == 1)
						{
							this.lander.rampSwitch = 0;
							this.rover.region1 = this.lander.region1;
							this.rover.region3 = this.lander.region2;
							this.rover.region2[0] = this.rover.region1[9];
							this.rover.region2[1] = this.rover.region1[10];
							this.rover.region2[2] = this.rover.region1[11];
							this.rover.region2[3] = this.rover.region1[6];
							this.rover.region2[4] = this.rover.region1[7];
							this.rover.region2[5] = this.rover.region1[8];
							this.rover.region2[6] = this.rover.region3[3];
							this.rover.region2[7] = this.rover.region3[4];
							this.rover.region2[8] = this.rover.region3[5];
							this.rover.region2[9] = this.rover.region3[0];
							this.rover.region2[10] = this.rover.region3[1];
							this.rover.region2[11] = this.rover.region3[2];
							Vector3 vector2 = new Vector3(this.rover.region1[12], this.rover.region1[13], this.rover.region1[14]);
							Vector3 vector3 = new Vector3(this.rover.region3[12], this.rover.region3[13], this.rover.region3[14]);
							Vector3 vector4 = Vector3.Normalize(Vector3.Lerp(vector2, vector3, 0.5f));
							this.rover.region2[12] = vector4.X;
							this.rover.region2[13] = vector4.Y;
							this.rover.region2[14] = vector4.Z;
							this.vehicleindex = 1;
							this.overlay.buttonindex = this.roverbuttonindex;
							this.sc.vehiclelerp = 0f;
							GamePad.SetVibration(this.myplayer, 0f, 0f);
							if (this.landerEngineI.State == SoundState.Playing)
							{
								this.landerEngineI.Pause();
							}
							this.bCampos = this.aCampos;
							this.bCamtarget = this.aCamtarget;
							this.bUppy = this.aUppy;
						}
						if (this.Ktoggle(this.sc.right_key) || (this.gamePadState.DPad.Right == ButtonState.Pressed && this.prevstate.DPad.Right == ButtonState.Released))
						{
							this.landerbuttonindex++;
							if (this.landerbuttonindex > 4)
							{
								this.landerbuttonindex = 1;
							}
							this.overlay.buttonindex = this.landerbuttonindex;
							this.sc.click.Play(this.sc.ev, 0f, 0f);
						}
						if (this.Ktoggle(this.sc.left_key) || (this.gamePadState.DPad.Left == ButtonState.Pressed && this.prevstate.DPad.Left == ButtonState.Released))
						{
							this.landerbuttonindex--;
							if (this.landerbuttonindex < 1)
							{
								this.landerbuttonindex = 4;
							}
							this.sc.click.Play(this.sc.ev, 0f, 0f);
							this.overlay.buttonindex = this.landerbuttonindex;
						}
						bool flag3 = this.Ktoggle(this.sc.one_key) || this.Ktoggle(this.sc.two_key) || this.Ktoggle(this.sc.three_key) || this.Ktoggle(this.sc.four_key);
						if (this.Ktoggle(this.sc.up_key) || flag3 || (this.gamePadState.Buttons.A == ButtonState.Pressed && this.prevstate.Buttons.A == ButtonState.Released))
						{
							int num14 = this.landerbuttonindex;
							if (this.sc.usingMouse)
							{
								if (this.Ktoggle(this.sc.one_key))
								{
									num14 = 1;
								}
								if (this.Ktoggle(this.sc.two_key))
								{
									num14 = 2;
								}
								if (this.Ktoggle(this.sc.three_key))
								{
									num14 = 3;
								}
								if (this.Ktoggle(this.sc.four_key))
								{
									num14 = 4;
								}
							}
							switch (num14)
							{
							case 1:
								this.overlay.landerbutton1 = 50;
								if (this.lander.door1 == 1)
								{
									this.openHatch(false);
								}
								else if (this.lander.door1 == 10)
								{
									this.sc.ramp.Play(this.sc.ev, 0f, 0f);
									this.lander.commandFlag = 2;
								}
								break;
							case 2:
								this.clearDumper();
								this.overlay.landerbutton2 = 50;
								break;
							case 3:
								this.overlay.landerbutton3 = 50;
								if (this.lander.radioFixed)
								{
									if (this.flowervol < 0.3f)
									{
										this.radiocount++;
										this.radiotimer = 20;
									}
									if (this.rock.chosenfar == -1 && this.flowervolumeTimer <= 0)
									{
										if (this.radiocount != 13)
										{
											this.sc.radio1.Play(this.sc.ev, (float)this.random.Next(-40, 40) / 100f, (float)this.random.Next(-60, 80) / 100f);
										}
										if (this.radiocount == 13 && this.flowervol < 0.1f)
										{
											this.sc.abort.Play(this.sc.ev, 1f, 0f);
										}
									}
									this.landerbuttonindex = 3;
									this.flowervolumeTimer = 900;
									this.overlay.landerbutton3 = 1800;
								}
								else
								{
									this.sc.abort.Play(this.sc.ev, -0.4f, 0.3f);
								}
								break;
							case 4:
							{
								this.overlay.landerbutton4 = 50;
								Vector3 vector5 = this.lander.position + Vector3.Transform(new Vector3(-200f, 0f, -200f), this.lander.orientation);
								float num15;
								this.GetHeightOnly(ref this.tmake.heightData, new Vector3(vector5.X, vector5.Y, vector5.Z), out num15);
								if (this.overlay.nextBouy == 1)
								{
									this.overlay.bouy1.X = vector5.X;
									this.overlay.bouy1.Y = num15;
									this.overlay.bouy1.Z = vector5.Z;
									this.sc.dropBeacon.Play(this.sc.ev, 0f, 0f);
								}
								else if (this.overlay.nextBouy == 2)
								{
									this.overlay.bouy2.X = vector5.X;
									this.overlay.bouy2.Y = num15;
									this.overlay.bouy2.Z = vector5.Z;
									this.sc.dropBeacon.Play(this.sc.ev, 0f, 0f);
								}
								else
								{
									this.sc.rejected.Play(this.sc.ev, 0f, 0f);
								}
								this.overlay.nextBouy = 0;
								if (this.overlay.bouy1 == Vector3.Zero)
								{
									this.overlay.nextBouy = 1;
								}
								else if (this.overlay.bouy2 == Vector3.Zero)
								{
									this.overlay.nextBouy = 2;
								}
								break;
							}
							}
						}
						if (this.lander.leftTrigger > 0f)
						{
							this.openHatch(false);
						}
						if (this.gamePadState.Buttons.Y == ButtonState.Pressed && this.prevstate.Buttons.Y == ButtonState.Released)
						{
							Vector3 vector6;
							if (this.vehicleindex == 1)
							{
								vector6 = this.rover.position + Vector3.Transform(new Vector3((float)this.random.Next(-10, 10), 0f, 200f), this.rover.orientation);
							}
							else
							{
								vector6 = this.lander.position + Vector3.Transform(new Vector3(-200f, 0f, -200f), this.lander.orientation);
							}
							float num16;
							this.GetHeightOnly(ref this.tmake.heightData, new Vector3(vector6.X, vector6.Y, vector6.Z), out num16);
							if (this.overlay.nextBouy == 1)
							{
								this.overlay.bouy1.X = vector6.X;
								this.overlay.bouy1.Y = num16;
								this.overlay.bouy1.Z = vector6.Z;
								this.sc.dropBeacon.Play(this.sc.ev, 0f, 0f);
							}
							if (this.overlay.nextBouy == 2)
							{
								this.overlay.bouy2.X = vector6.X;
								this.overlay.bouy2.Y = num16;
								this.overlay.bouy2.Z = vector6.Z;
								this.sc.dropBeacon.Play(this.sc.ev, 0f, 0f);
							}
							this.overlay.nextBouy = 0;
							if (this.overlay.bouy1 == Vector3.Zero)
							{
								this.overlay.nextBouy = 1;
							}
							else if (this.overlay.bouy2 == Vector3.Zero)
							{
								this.overlay.nextBouy = 2;
							}
						}
					}
					if (this.vehicleindex == 1)
					{
						if (this.rover.solar1 == 100 && this.myframe % 5 == 0)
						{
							if (this.sunDir.Y < 0f)
							{
								float num17 = -Vector3.Dot(Vector3.Transform(new Vector3(1f, -0.6f, 0f), this.rover.orientation), this.sunDir);
								if (num17 > 0f)
								{
									if (this.rover.movement.Z == 0f)
									{
										num17 *= 3.5f;
									}
									else
									{
										num17 *= 1.8f;
									}
									this.overlay.cellTick += num17;
								}
								else
								{
									this.overlay.cellTick += 0.25f;
									if (this.rover.movement.Z == 0f)
									{
										this.overlay.cellTick += 0.1f;
									}
								}
							}
							else
							{
								this.overlay.cellTick += 0.25f;
								if (this.rover.movement.Z == 0f)
								{
									this.overlay.cellTick += 0.1f;
								}
							}
							if (this.overlay.cellTick > 1f)
							{
								this.overlay.cellAMT++;
								this.overlay.cellTick = 0f;
								float num18 = (float)this.overlay.cellAMT / 101.1f;
								if (this.overlay.cellAMT < 100 && (this.overlay.cellAMT % 5 == 0 || this.overlay.cellAMT > 95))
								{
									if (this.overlay.cellAMT > 95)
									{
										this.sc.fuelfull.Play(this.sc.ev * 0.7f, num18, 0f);
									}
									else
									{
										this.sc.fuellow.Play(this.sc.ev * 0.6f, num18, 0f);
									}
								}
							}
						}
						this.overlay.cellDec += this.rover.movement.Z / 1700f;
						if (this.overlay.cellDec < 0f)
						{
							this.overlay.cellDec = 1f;
							this.overlay.cellAMT--;
						}
						Rover.max = -5f;
						if (this.overlay.cellAMT > 10)
						{
							Rover.max = -10f;
						}
						if (this.overlay.cellAMT > 25)
						{
							Rover.max = -20f;
						}
						if (this.overlay.cellAMT > 35)
						{
							Rover.max = -30f;
						}
						if (this.overlay.cellAMT > 45)
						{
							Rover.max = -45f;
						}
						if (this.overlay.cellAMT > 85)
						{
							Rover.max = -65f;
						}
						if (this.rover.solar1 == 100)
						{
							Rover.max /= 2.5f;
						}
						if (this.rover.scoop1 == 100)
						{
							Rover.max = Math.Max(Rover.max, -6f);
						}
						if (this.nearfarm)
						{
							this.rover.HandleInput(ref this.mouseState, ref this.prevMouse, ref this.keyState, ref this.prevkeyState, ref this.gamePadState, ref this.heights, ref this.tmake.normals);
						}
						else
						{
							this.rover.HandleInput(ref this.mouseState, ref this.prevMouse, ref this.keyState, ref this.prevkeyState, ref this.gamePadState, ref this.tmake.heightData, ref this.tmake.normals);
						}
						float num19 = MathHelper.Clamp(Math.Abs(this.rover.speedx), 0f, 43f);
						float num20 = MathHelper.Clamp(Math.Abs(this.rover.speedx), 0f, 43f);
						if (this.roverEngineI.State == SoundState.Stopped)
						{
							this.roverEngineI.IsLooped = true;
							this.roverEngineI.Volume = num19 / 48f * this.sc.ev;
							this.roverEngineI.Play();
						}
						else
						{
							this.roverEngineI.Resume();
							this.roverEngineI.Pitch = num20 / 43f - 1f;
							this.roverEngineI.Volume = num19 / 48f * this.sc.ev;
						}
						if (Math.Abs(this.rover.grav.X) + Math.Abs(this.rover.grav.Z) > 0f && this.rover.speedx > -1f && this.rover.groundflag == 1)
						{
							if (this.gravelI.State == SoundState.Stopped)
							{
								this.gravelI.IsLooped = true;
								this.gravelI.Volume = MathHelper.Clamp((Math.Abs(this.rover.grav.X) + Math.Abs(this.rover.grav.Z)) / 5f, 0f, 1f) * this.sc.ev;
								this.gravelI.Play();
							}
							else
							{
								this.gravelI.Resume();
								this.gravelI.Volume = MathHelper.Clamp((Math.Abs(this.rover.grav.X) + Math.Abs(this.rover.grav.Z)) / 5f, 0f, 1f) * this.sc.ev;
							}
						}
						else if (this.gravelI.State == SoundState.Playing)
						{
							this.gravelI.Pause();
						}
						if (this.rover.camSwitch == 1)
						{
							this.rover.camSwitch = 0;
							this.overlay.landerbutton4 = 0;
							this.lander.commandFlag = 2;
							this.sc.ramp.Play(this.sc.ev, 0f, 0f);
							this.vehicleindex = 2;
							this.setLens(70f);
							this.overlay.buttonindex = this.landerbuttonindex;
							if (this.roverEngineI.State == SoundState.Playing)
							{
								this.roverEngineI.Pause();
							}
							if (this.gravelI.State == SoundState.Playing)
							{
								this.gravelI.Pause();
							}
							if (this.vibrateControl > 0)
							{
								GamePad.SetVibration(this.myplayer, 0f, 0f);
							}
							this.sc.vehiclelerp = 0f;
							this.bCampos = this.aCampos;
							this.bCamtarget = this.aCamtarget;
							this.bUppy = this.aUppy;
							this.rot -= this.rover.facingDirection;
							if (this.rot > 6.283185f)
							{
								this.rot -= 6.283185f;
							}
							if (this.rot < 0f)
							{
								this.rot += 6.283185f;
							}
						}
						if (this.Ktoggle(this.sc.right_key) || (this.gamePadState.DPad.Right == ButtonState.Pressed && this.prevstate.DPad.Right == ButtonState.Released))
						{
							this.roverbuttonindex++;
							if (this.roverbuttonindex > 4)
							{
								this.roverbuttonindex = 1;
							}
							this.sc.click.Play(this.sc.ev, 0f, 0f);
							this.overlay.buttonindex = this.roverbuttonindex;
						}
						if (this.Ktoggle(this.sc.left_key) || (this.gamePadState.DPad.Left == ButtonState.Pressed && this.prevstate.DPad.Left == ButtonState.Released))
						{
							this.roverbuttonindex--;
							if (this.roverbuttonindex < 1)
							{
								this.roverbuttonindex = 4;
							}
							this.sc.click.Play(this.sc.ev, 0f, 0f);
							this.overlay.buttonindex = this.roverbuttonindex;
						}
						bool flag4 = this.Ktoggle(this.sc.x_key) || this.Ktoggle(this.sc.one_key) || this.Ktoggle(this.sc.two_key) || this.Ktoggle(this.sc.three_key) || this.Ktoggle(this.sc.four_key);
						if (!flag2 && (this.Ktoggle(this.sc.up_key) || flag4 || (this.gamePadState.Buttons.A == ButtonState.Pressed && this.prevstate.Buttons.A == ButtonState.Released)))
						{
							int num21 = this.roverbuttonindex;
							if (this.sc.usingMouse)
							{
								if (this.Ktoggle(this.sc.one_key) || this.Ktoggle(this.sc.x_key))
								{
									num21 = 1;
								}
								if (this.Ktoggle(this.sc.two_key))
								{
									num21 = 2;
								}
								if (this.Ktoggle(this.sc.three_key))
								{
									num21 = 3;
								}
								if (this.Ktoggle(this.sc.four_key))
								{
									num21 = 4;
								}
							}
							flag2 = true;
							switch (num21)
							{
							case 1:
								if (this.rover.onramp == 0 && this.rover.grav.Length() < 4f && this.rover.velocity.Length() < 4f)
								{
									this.overlay.roverbutton1 = 50;
									this.vehicleindex = 3;
									this.setLens(80f);
									this.sc.vehiclelerp = 0f;
									if (this.sc.roverdist[this.sc.roverindex] == 0f)
									{
										this.sc.vehiclelerp = 1f;
									}
									this.bCampos = this.aCampos;
									this.bCamtarget = this.aCamtarget;
									this.bUppy = this.aUppy;
									this.rover.dashcam = 0;
									this.rover.speedx = 0f;
									this.humanonramp = 0;
									this.camheight = 3.4f;
									this.camhite = 80f;
									Vector3 vector7 = Vector3.Normalize(this.aCampos - this.rover.position);
									if (this.sc.vehiclelerp == 1f)
									{
										vector7 = Vector3.Transform(Vector3.Right, Matrix.CreateRotationY(this.rover.facingDirection));
									}
									this.aCampos = new Vector3(this.rover.position.X + vector7.X * 140f, this.rover.position.Y, this.rover.position.Z + vector7.Z * 140f);
									this.campos = this.aCampos;
									this.camradian = 0f - (float)Math.Atan2((double)(this.aCampos.X - this.rover.position.X), (double)(this.aCampos.Z - this.rover.position.Z));
									this.camlookpos.X = (float)(-(float)Math.Cos((double)this.camheight) * Math.Sin((double)this.camradian) * 200.0) + this.campos.X;
									this.camlookpos.Z = (float)(-(float)Math.Cos((double)this.camheight) * -(float)Math.Cos((double)this.camradian) * 200.0) + this.campos.Z;
									this.camlookpos.Y = (float)Math.Sin((double)this.camheight) * 200f + this.campos.Y;
									this.aCamtarget = new Vector3(this.camlookpos.X, this.camlookpos.Y + this.camhite, this.camlookpos.Z);
								}
								else
								{
									this.sc.abort.Play(this.sc.ev, 0f, 0f);
									this.vibrateControl = 20;
								}
								break;
							case 2:
								this.overlay.roverbutton2 = 50;
								if (this.rover.solar1 == 1)
								{
									this.rover.solarflag = 1;
									this.sc.opensolar.Play(this.sc.ev * 0.3f, 0.1f, 0.2f);
								}
								else if (this.rover.solar1 == 100)
								{
									this.rover.solarflag = 2;
									this.sc.opensolar.Play(this.sc.ev * 0.3f, -0.1f, -0.2f);
								}
								break;
							case 3:
								if (this.rover.movement.Z >= -10f)
								{
									this.rover.scoopflag = 1;
									Rover.max = -6f;
									this.rover.min = 5f;
									if (this.rover.scoop1 == 1)
									{
										this.sc.scoopx.Play(this.sc.ev, 0f, 0f);
									}
								}
								if (this.rover.scoop1 != 1)
								{
									this.rover.scoopflag = 2;
									this.rover.min = 7f;
									if (this.rover.scoop1 == 100)
									{
										this.sc.scoopx.Play(this.sc.ev, -0.12f, 0f);
									}
								}
								this.overlay.roverbutton3 = 50;
								break;
							case 4:
								this.sc.horn.Play(this.sc.ev, 0f, 0f);
								this.horntimer = 20;
								this.horncount++;
								this.overlay.roverbutton4 = 50;
								break;
							}
						}
						if (this.sc.equip[3] == 1)
						{
							if (this.gamePadState.Buttons.RightShoulder == ButtonState.Pressed && this.prevstate.Buttons.RightShoulder == ButtonState.Released && this.rover.movement.Z >= -10f)
							{
								if (this.rover.scoop1 == 1)
								{
									this.sc.scoopx.Play(this.sc.ev, 0f, 0f);
								}
								this.rover.scoopflag = 1;
								Rover.max = -6f;
								this.rover.min = 3f;
							}
							if (this.gamePadState.Buttons.LeftShoulder == ButtonState.Pressed && this.prevstate.Buttons.LeftShoulder == ButtonState.Released)
							{
								if (this.rover.scoop1 == 100)
								{
									this.sc.scoopx.Play(this.sc.ev, -0.2f, 0f);
								}
								this.rover.scoopflag = 2;
								this.rover.min = 7f;
							}
						}
						if (!flag2 && (this.Ktoggle(this.sc.t_key) || (this.gamePadState.Buttons.Y == ButtonState.Pressed && this.prevstate.Buttons.Y == ButtonState.Released)))
						{
							this.sc.horn.Play(this.sc.ev, 0f, 0f);
							this.horntimer = 20;
							this.horncount++;
						}
					}
					if (this.vehicleindex == 3)
					{
						if (this.rover.solar1 == 100 && this.myframe % 5 == 0)
						{
							if (this.sunDir.Y < 0f)
							{
								float num22 = -Vector3.Dot(Vector3.Transform(Vector3.Right, this.rover.orientation), this.sunDir);
								if (num22 > 0f)
								{
									this.overlay.cellTick += num22 * 0.2f;
								}
								else
								{
									this.overlay.cellTick += 0.08f;
								}
							}
							else
							{
								this.overlay.cellTick += 0.03f;
							}
							if (this.overlay.cellTick > 1f)
							{
								this.overlay.cellAMT++;
								this.overlay.cellTick = 0f;
								if (Vector3.Distance(this.campos, this.rover.position) < 1000f)
								{
									float num23 = (float)this.overlay.cellAMT / 101.1f;
									if (this.overlay.cellAMT < 100 && (this.overlay.cellAMT % 5 == 0 || this.overlay.cellAMT > 95))
									{
										if (this.overlay.cellAMT > 95)
										{
											this.sc.fuelfull.Play(this.sc.ev * 0.7f, num23, 0f);
										}
										else
										{
											this.sc.fuellow.Play(this.sc.ev * 0.7f, num23, 0f);
										}
									}
								}
							}
						}
						if (Facility.inFacility && this.objIndex == 6)
						{
							this.emo = GameplayScreen.act.facilityfound;
						}
						if (Facility.createWorkerLoc != Vector4.Zero && (this.sc.astronaut.man.dupe.Count < 10 || this.objIndex == 7))
						{
							this.sc.enginex.Play(this.sc.ev, 0.6f, 0f);
							Vector2 vector8 = new Vector2(Facility.createWorkerLoc.X, Facility.createWorkerLoc.Z);
							this.sc.astronaut.dropFacilityAstronaut(this.myframe, vector8, astroDupe.emotion.underground, 4.71f + Facility.createWorkerLoc.W);
							if (this.objIndex == 7)
							{
								this.emo = GameplayScreen.act.cloned;
							}
							Facility.createWorkerLoc = Vector4.Zero;
						}
						if ((this.gamePadState.Buttons.A == ButtonState.Pressed || (this.sc.usingMouse && this.KMdown(this.sc.space_key))) && Facility.outsideCastle && !Facility.inFacility)
						{
							if (this.rejump && this.jumping && this.doubleJump < 2f)
							{
								this.doubleJump += 1f;
								this.jumpCount = 30;
								this.sc.jump.Play(this.sc.ev, 0.1f + this.doubleJump / 5f, 0f);
								this.rejump = false;
								if (this.fallGrav < 0f)
								{
									if (this.fallGrav > this.gravLimdown)
									{
										this.fallGrav = 0f;
									}
									else
									{
										this.fallGrav /= 2f;
									}
								}
							}
							if (this.jumpCount > 0 && this.jumping)
							{
								this.fallGrav += 0.15f;
								if (this.fallGrav > this.gravLim)
								{
									this.fallGrav = this.gravLim;
								}
							}
						}
						if (((!this.sc.usingMouse && this.prevstate.Buttons.A == ButtonState.Released) || (this.sc.usingMouse && this.KMreleased(this.sc.space_key))) && Facility.outsideCastle && !Facility.inFacility && this.jumping && !this.rejump)
						{
							this.rejump = true;
						}
						if (((this.sc.usingMouse && this.Ktoggle(this.sc.space_key)) || (!this.atRover && !flag2 && this.gamePadState.Buttons.A == ButtonState.Pressed && this.prevstate.Buttons.A == ButtonState.Released)) && !this.jumpCalled && !this.jumping && !Facility.inFacility)
						{
							this.jumpCalled = true;
							this.jumpCount = 30;
							this.sc.jump.Play(this.sc.ev, 0f, 0f);
							this.doubleJump = 0f;
							this.rejump = false;
							Vector2 left = this.gamePadState.ThumbSticks.Left;
							if (this.sc.usingMouse)
							{
								if (this.KMdown(this.sc.a_key))
								{
									left.X -= 1f;
								}
								if (this.KMdown(this.sc.d_key))
								{
									left.X += 1f;
								}
								if (this.KMdown(this.sc.w_key))
								{
									left.Y += 1f;
								}
								if (this.KMdown(this.sc.s_key))
								{
									left.Y -= 1f;
								}
							}
							float num24 = 1.5f;
							float num25 = 0.3f;
							this.fallLim = 5f;
							if (this.Ktoggle(this.sc.leftshift_key) || this.gamePadState.Buttons.LeftStick == ButtonState.Pressed)
							{
								num25 = 0.9f;
								this.fallLim = 8f;
							}
							if (Facility.outsideCastle)
							{
								this.walkspeed = num24 + num25 * 5f;
								this.vec = Vector3.Transform(new Vector3(-left.X / 1.3f, 0f, left.Y), Matrix.CreateRotationY(-3.14f - this.camradian)) * this.walkspeed;
								this.vec /= this.slopereducer;
								this.fallLim /= this.slopereducer;
								this.fallVec = this.vec;
								this.gravLim = 5f;
								this.fallAcc = -0.06f;
								this.fallGrav = 1f;
							}
							else
							{
								this.fallLim = 2f;
								this.walkspeed = num24 + 0.33f;
								this.vec = Vector3.Transform(new Vector3(-left.X / 1.3f, 0f, left.Y), Matrix.CreateRotationY(-3.14f - this.camradian)) * this.walkspeed;
								this.fallVec = this.vec;
								this.fallAcc = -0.05f;
								this.fallGrav = 1f;
							}
						}
						if (this.humanonramp == 3 && !this.lander.radioFixed && (this.Ktoggle(this.sc.x_key) || (this.gamePadState.Buttons.X == ButtonState.Pressed && this.prevstate.Buttons.X == ButtonState.Released)))
						{
							this.sc.radio1.Play(this.sc.ev, 0f, 0f);
							this.lander.radioFixed = true;
						}
						if (this.atRover && (this.Ktoggle(this.sc.x_key) || (this.gamePadState.Buttons.A == ButtonState.Pressed && this.prevstate.Buttons.A == ButtonState.Released)))
						{
							this.vehicleindex = 1;
							this.setLens(70f);
							this.sc.vehiclelerp = 1f;
							if (this.sc.roverdist[this.sc.roverindex] == 0f)
							{
								this.sc.vehiclelerp = 1f;
							}
							this.bCampos = this.aCampos;
							this.bCamtarget = this.aCamtarget;
							this.bUppy = this.aUppy;
							if (this.cameditMess1 && this.sc.usingMouse)
							{
								messagebox messagebox5 = new messagebox("Press F1 to Edit Cameras", 0, 0);
								base.ScreenManager.AddScreen(messagebox5, null);
								this.cameditMess1 = false;
							}
							this.rot = this.camradian + 3.14f;
							if (this.rot > 6.283185f)
							{
								this.rot -= 6.283185f;
							}
							if (this.rot < 0f)
							{
								this.rot += 6.283185f;
							}
						}
						if ((this.Ktoggle(this.sc.t_key) || (this.gamePadState.Buttons.Y == ButtonState.Pressed && this.prevstate.Buttons.Y == ButtonState.Released)) && !this.nearAstro && this.rover.velocity.Length() < 3f && this.rover.onramp == 0)
						{
							this.sc.astronaut.everyoneOut = true;
						}
						if (this.nearAstro && (this.Ktoggle(this.sc.x_key) || (this.gamePadState.Buttons.X == ButtonState.Pressed && this.prevstate.Buttons.X == ButtonState.Released)))
						{
							this.sc.astronaut.saluteRequest = true;
							this.sc.astronaut.facingRover = this.rover.facingDirection;
						}
						if (this.nearFlower && (this.Ktoggle(this.sc.x_key) || (this.gamePadState.Buttons.X == ButtonState.Pressed && this.prevstate.Buttons.X == ButtonState.Released)) && this.rock.chosen != -1 && this.rock.flowerInst[this.rock.chosen].stateFlag == 15)
						{
							this.sc.grow.Play(this.sc.ev, 0f, (float)this.random.Next(-40, 40) / 100f);
							this.rock.flowerInst[this.rock.chosen].stateFlag = 5;
						}
						if (Facility.inFacility)
						{
							this.facility.HandleInput(input, this.gamePadState, this.prevstate, this.aCampos, this.aCamtarget);
						}
					}
					if (this.Ktoggle(this.sc.enter_key) || (this.gamePadState.Buttons.B == ButtonState.Pressed && this.prevstate.Buttons.B == ButtonState.Released))
					{
						this.typewriterblank = 500f;
						this.textflag = 0;
						this.typewriterwait = 480f;
						this.typeposition = 0f;
						this.typevertical = (float)this.random.Next(100, 150);
						this.typewriterdelay = this.random.Next(2, 7);
					}
					if (this.lander.door1 == 1 && this.vehicleDistance < 2000f && this.rover.onramp == 0 && this.vehicleindex == 1)
					{
						this.openHatch(true);
					}
					if (this.lander.rampSwitch == 1)
					{
						this.lander.rampSwitch = 0;
						Array.Copy(this.lander.region1, this.rover.region1, this.rover.region1.Length);
						Array.Copy(this.lander.region2, this.rover.region3, this.rover.region3.Length);
						this.rover.region2[0] = this.rover.region1[9];
						this.rover.region2[1] = this.rover.region1[10];
						this.rover.region2[2] = this.rover.region1[11];
						this.rover.region2[3] = this.rover.region1[6];
						this.rover.region2[4] = this.rover.region1[7];
						this.rover.region2[5] = this.rover.region1[8];
						this.rover.region2[6] = this.rover.region3[3];
						this.rover.region2[7] = this.rover.region3[4];
						this.rover.region2[8] = this.rover.region3[5];
						this.rover.region2[9] = this.rover.region3[0];
						this.rover.region2[10] = this.rover.region3[1];
						this.rover.region2[11] = this.rover.region3[2];
						Vector3 vector9 = new Vector3(this.rover.region1[12], this.rover.region1[13], this.rover.region1[14]);
						Vector3 vector10 = new Vector3(this.rover.region3[12], this.rover.region3[13], this.rover.region3[14]);
						Vector3 vector11 = Vector3.Normalize(Vector3.Lerp(vector9, vector10, 0.5f));
						this.rover.region2[12] = vector11.X;
						this.rover.region2[13] = vector11.Y;
						this.rover.region2[14] = vector11.Z;
					}
					if (this.lander.shockhit > 0)
					{
						if (this.lander.shockhit > 20)
						{
							this.sc.groundhit.Play(this.sc.ev, 0.3f - (float)this.random.Next(1, 1000) / 1000f, 0f);
							this.vibrateControl = this.lander.shockhit;
						}
						this.lander.shockhit = 0;
					}
					if (this.rover.shockhit > 0)
					{
						this.vibrateControl = this.rover.shockhit / 3;
						if (this.rover.shockhit > 15)
						{
							this.sc.shocks.Play(0.7f * this.sc.ev, (float)this.random.Next(-90, 90) / 100f, 0f);
						}
						this.rover.shockhit = 0;
					}
					if (this.vibrateControl > 0)
					{
						this.vibrateControl--;
						if (this.sc.vibroSetting == 1 && this.vibrateControl > 2)
						{
							GamePad.SetVibration(this.myplayer, 0.5f, 1f);
						}
						if (this.vibrateControl < 2)
						{
							GamePad.SetVibration(this.myplayer, 0f, 0f);
						}
					}
					if (this.myframe % 25 == 0 || this.jumping)
					{
						if (this.dropEngineI.State == SoundState.Stopped)
						{
							this.dropEngineI.IsLooped = true;
							this.dropEngineI.Volume = this.sc.ev * (1f - MathHelper.Clamp((this.dropshipDistance - 3000f) / 12000f, 0f, 1f));
							this.dropEngineI.Play();
						}
						else
						{
							float num26 = 1f - MathHelper.Clamp((this.dropshipDistance - 3000f) / 12000f, 0f, 1f);
							this.dropEngineI.Resume();
							this.dropEngineI.Volume = num26 * this.sc.ev;
						}
						if (this.breathing.State == SoundState.Stopped)
						{
							this.breathing.IsLooped = true;
							if (this.vehicleindex == 3)
							{
								if (!this.jumping)
								{
									this.breathvol += 0.1f;
									if (this.breathvol > 0.4f)
									{
										this.breathvol = 0.4f;
									}
								}
								else
								{
									this.breathvol -= 0.01f;
									if (this.breathvol < 0.05f)
									{
										this.breathvol = 0.05f;
									}
								}
								this.breathing.Volume = this.breathvol * this.sc.ev;
							}
							else
							{
								this.breathing.Volume = 0f;
							}
							this.breathing.Play();
						}
						else
						{
							this.breathing.Resume();
							if (this.vehicleindex == 3)
							{
								if (!this.jumping)
								{
									this.breathvol += 0.1f;
									if (this.breathvol > 0.5f)
									{
										this.breathvol = 0.5f;
									}
								}
								else
								{
									this.breathvol -= 0.01f;
									if (this.breathvol < 0.05f)
									{
										this.breathvol = 0.05f;
									}
								}
								this.breathing.Volume = this.breathvol * this.sc.ev;
							}
							else
							{
								this.breathing.Volume = 0f;
							}
						}
					}
					if (this.vehicleindex != 3 && (this.KMtoggle(this.sc.mmb_key) || this.Ktoggle(this.sc.tab_key) || (this.gamePadState.Buttons.RightStick == ButtonState.Pressed && this.prevstate.Buttons.RightStick == ButtonState.Released)))
					{
						if (this.vehicleindex == 1)
						{
							this.sc.roverindex++;
							if (this.sc.roverindex > 2)
							{
								this.sc.roverindex = 0;
							}
							this.yy = this.sc.roverheight[this.sc.roverindex];
							this.rot = this.sc.roverradian[this.sc.roverindex];
							this.setLens(70f);
							if (this.memoTimer <= 0 || this.memoIcon == 7)
							{
								int num27 = this.sc.roverindex + 1;
								this.memoTimer = 150;
								this.memoIcon = 7;
								GameplayScreen.memo.Length = 0;
								GameplayScreen.memo.Append("Camera " + num27.ToString());
							}
						}
						else if (this.vehicleindex == 2)
						{
							this.sc.landerindex++;
							if (this.sc.landerindex > 2)
							{
								this.sc.landerindex = 0;
							}
							this.yy = this.sc.landerheight[this.sc.landerindex];
							this.rot = this.sc.landerradian[this.sc.landerindex];
							if (this.lander.impact == 1)
							{
								this.sc.vehiclelerp = 0f;
								this.bCampos = this.aCampos;
								this.bCamtarget = this.aCamtarget;
								this.bUppy = this.aUppy;
							}
							this.setLens(70f);
							if (this.memoTimer <= 0 || this.memoIcon == 7)
							{
								int num28 = this.sc.landerindex + 1;
								this.memoTimer = 150;
								this.memoIcon = 7;
								GameplayScreen.memo.Length = 0;
								GameplayScreen.memo.Append("Camera " + num28.ToString());
							}
						}
					}
					float num29 = 0f;
					if (this.sc.usingMouse)
					{
						if (this.vehicleindex == 1)
						{
							if (this.sc.roverrotlock == 1 && this.Kdown(this.sc.u_key))
							{
								num29 = -this.mouseX * (0.00045f * this.sc.space_rsentivityX) * (float)this.sc.space_rinvertX;
								this.sc.roverradian[this.sc.roverindex] = this.rot;
							}
							if (this.sc.roverrotlock == 0)
							{
								num29 = -this.mouseX * (0.00045f * this.sc.space_rsentivityX) * (float)this.sc.space_rinvertX;
								this.sc.roverradian[this.sc.roverindex] = this.rot;
							}
						}
						if (this.vehicleindex == 2)
						{
							if (this.sc.landerrotlock == 1 && this.Kdown(this.sc.u_key))
							{
								num29 = -this.mouseX * (0.00045f * this.sc.space_sentivityX) * (float)this.sc.space_invertX;
								this.sc.landerradian[0] = this.rot;
								this.sc.landerradian[1] = this.rot;
								this.sc.landerradian[2] = this.rot;
							}
							if (this.sc.landerrotlock == 0)
							{
								num29 = -this.mouseX * (0.00045f * this.sc.space_sentivityX) * (float)this.sc.space_invertX;
								this.sc.landerradian[0] = this.rot;
								this.sc.landerradian[1] = this.rot;
								this.sc.landerradian[2] = this.rot;
							}
						}
					}
					else
					{
						if (this.vehicleindex == 1)
						{
							if (this.sc.roverrotlock == 0)
							{
								num29 = -this.gamePadState.ThumbSticks.Right.X * (0.1f * this.sc.space_rsentivityX) * (float)this.sc.space_rinvertX;
							}
							this.sc.roverradian[this.sc.roverindex] = this.rot;
						}
						if (this.vehicleindex == 2)
						{
							num29 = -this.gamePadState.ThumbSticks.Right.X * (0.1f * this.sc.space_sentivityX) * (float)this.sc.space_invertX;
							this.sc.landerradian[0] = this.rot;
							this.sc.landerradian[1] = this.rot;
							this.sc.landerradian[2] = this.rot;
						}
					}
					this.rot += num29;
					if (this.rot > 6.283185f)
					{
						this.rot = 0f;
					}
					if (this.rot < 0f)
					{
						this.rot = 6.283185f;
					}
					this.aUppy = Vector3.Up;
					this.lander.camrot = this.rot;
					if (this.vehicleindex == 1)
					{
						if (this.objIndex == 0)
						{
							this.emo = GameplayScreen.act.rovered;
						}
						this.sc.vehicleindex = this.vehicleindex;
						this.overlay.damaged1 = false;
						if (this.sc.roverdist[this.sc.roverindex] > 110f)
						{
							if (this.sc.roverrotlock > 0)
							{
								this.myrotter = this.rot - this.rover.facingDirection;
								if (this.myrotter > 6.283185f)
								{
									this.myrotter -= 6.283185f;
								}
								if (this.myrotter < 0f)
								{
									this.myrotter += 6.283185f;
								}
							}
							else
							{
								this.myrotter = this.rot;
							}
							this.xx = (float)Math.Sin((double)this.myrotter) * this.sc.roverdist[this.sc.roverindex];
							this.zz = (float)(-(float)Math.Cos((double)this.myrotter)) * this.sc.roverdist[this.sc.roverindex];
							if (this.sc.usingMouse)
							{
								this.yy = MathHelper.Clamp(this.yy, -300f, this.sc.roverdist[this.sc.roverindex] * 2f);
							}
							else
							{
								this.yy = MathHelper.Clamp(this.yy, -300f, this.sc.roverdist[this.sc.roverindex] * 2f);
							}
							this.aCampos.X = this.rover.position.X + this.xx;
							this.aCampos.Y = this.rover.position.Y + this.yy;
							this.aCampos.Z = this.rover.position.Z + this.zz;
							float num30;
							this.GetHeightOnly(ref this.tmake.heightData, this.aCampos, out num30);
							if (this.aCampos.Y < num30 + (float)this.roverY)
							{
								this.aCampos.Y = num30 + (float)this.roverY;
								this.yy = this.aCampos.Y - this.rover.position.Y;
							}
							if (this.sc.usingMouse)
							{
								if (this.sc.roverhitelock == 1 && this.Kdown(this.sc.u_key))
								{
									this.yy += -this.mouseY * 0.2f * this.sc.space_rsentivityY * (float)this.sc.space_rinvertY;
									this.sc.roverheight[this.sc.roverindex] = this.yy;
								}
								if (this.sc.roverhitelock == 0)
								{
									this.yy += -this.mouseY * 0.2f * this.sc.space_rsentivityY * (float)this.sc.space_rinvertY;
									this.sc.roverheight[this.sc.roverindex] = this.yy;
								}
							}
							else
							{
								if (this.sc.roverhitelock == 0)
								{
									this.yy += this.gamePadState.ThumbSticks.Right.Y * (20f * this.sc.space_rsentivityY) * (float)this.sc.space_rinvertY;
								}
								this.sc.roverheight[this.sc.roverindex] = this.yy;
							}
							if (this.yy != this.sc.roverheight[this.sc.roverindex])
							{
								if (this.yy > this.sc.roverheight[this.sc.roverindex])
								{
									this.yy -= 3f;
								}
								if (this.yy < this.sc.roverheight[this.sc.roverindex])
								{
									this.yy += 3f;
								}
								if (Math.Abs(this.sc.roverheight[this.sc.roverindex] - this.yy) <= 4f)
								{
									this.yy = this.sc.roverheight[this.sc.roverindex];
								}
							}
							this.aCamtarget.X = this.rover.position.X;
							this.aCamtarget.Y = this.rover.position.Y + 30f;
							this.aCamtarget.Z = this.rover.position.Z;
							this.rover.dashcam = 0;
							this.setLens(70f);
						}
						else
						{
							this.myrotter = 3.1415925f - this.rover.facingDirection;
							if (this.myrotter < 0f)
							{
								this.myrotter += 6.283185f;
							}
							if (this.sc.vehiclelerp >= 1f)
							{
								this.rover.dashcam = 1;
							}
							this.setLens(this.lens);
							this.aCampos = this.rover.position + Vector3.Transform(new Vector3(0f, 3f * this.rover.scoopx + 64f, -35f), this.rover.orientation);
							this.aCamtarget = this.rover.position + Vector3.Transform(new Vector3(0f, 0f, -(this.rover.scoopx * 600f) - 350f), this.rover.orientation);
							this.aUppy = Vector3.Normalize(Vector3.Transform(new Vector3(0f, 10000f, 0f), this.rover.orientation));
						}
					}
					if (this.vehicleindex == 2)
					{
						if (this.objIndex == 1)
						{
							this.emo = GameplayScreen.act.landered;
						}
						this.sc.vehicleindex = this.vehicleindex;
						this.xx = (float)Math.Sin((double)this.rot) * this.sc.landerdist[this.sc.landerindex];
						this.zz = (float)(-(float)Math.Cos((double)this.rot)) * this.sc.landerdist[this.sc.landerindex];
						if (this.sc.usingMouse)
						{
							this.yy = MathHelper.Clamp(this.yy, -800f, this.sc.landerheight[this.sc.landerindex]);
						}
						else
						{
							this.yy = MathHelper.Clamp(this.yy, -300f, this.sc.landerdist[this.sc.landerindex]);
						}
						Vector3 vector12 = new Vector3(this.xx, this.yy, this.zz);
						this.aCampos = this.lander.position + vector12;
						float num31;
						this.GetHeightOnly(ref this.tmake.heightData, this.aCampos, out num31);
						if (this.aCampos.Y < num31 + (float)this.landerY)
						{
							this.aCampos.Y = num31 + (float)this.landerY;
							this.yy = this.aCampos.Y - this.lander.position.Y;
						}
						float num32 = 0f;
						if (this.sc.usingMouse)
						{
							if (this.sc.landerhitelock == 0)
							{
								this.yy += -this.mouseY * 0.35f * this.sc.space_sentivityY * (float)this.sc.space_invertY;
								this.sc.landerheight[this.sc.landerindex] = this.yy;
							}
							if (this.sc.landerhitelock == 1 && this.Kdown(this.sc.u_key))
							{
								this.yy += -this.mouseY * 0.35f * this.sc.space_sentivityY * (float)this.sc.space_invertY;
								this.sc.landerheight[this.sc.landerindex] = this.yy;
							}
							if (this.Ktoggle(this.sc.w_key))
							{
								num32 = 1f;
							}
						}
						else
						{
							if (this.sc.landerhitelock == 0)
							{
								this.yy += this.gamePadState.ThumbSticks.Right.Y * (45f * this.sc.space_sentivityY) * (float)this.sc.space_invertY;
							}
							num32 = this.lander.rightTrigger;
						}
						if (this.yy != this.sc.landerheight[this.sc.landerindex])
						{
							if (this.yy > this.sc.landerheight[this.sc.landerindex])
							{
								this.yy -= 5f;
							}
							if (this.yy < this.sc.landerheight[this.sc.landerindex])
							{
								this.yy += 5f;
							}
							if (Math.Abs(this.sc.landerheight[this.sc.landerindex] - this.yy) <= 6f)
							{
								this.yy = this.sc.landerheight[this.sc.landerindex];
							}
						}
						this.aCamtarget.X = this.lander.position.X;
						this.aCamtarget.Y = this.lander.position.Y;
						this.aCamtarget.Z = this.lander.position.Z;
						if (this.lander.lander2ground >= 100f && (this.Ktoggle(this.sc.w_key) || this.lander.rightTrigger > 0f))
						{
							float num33 = (1f - MathHelper.Clamp((this.lander.lander2ground - 100f) / 1800f, 0f, 1f)) * num32;
							this.aCamtarget.X = this.aCamtarget.X + num33 * (float)this.random.Next(-850, 850) / 90f;
							this.aCamtarget.Y = this.aCamtarget.Y + num33 * (float)this.random.Next(-850, 850) / 90f;
							this.aCamtarget.Z = this.aCamtarget.Z + num33 * (float)this.random.Next(-850, 850) / 90f;
						}
					}
					if (this.vehicleindex == 3)
					{
						this.sc.vehicleindex = this.vehicleindex;
						Vector2 vector13 = Vector2.Zero;
						this.vec = Vector3.Zero;
						this.facility.jumping = this.jumping;
						if (this.sc.vehiclelerp >= 1f)
						{
							float num34;
							float num35;
							if (this.mouseX == 0f && this.mouseY == 0f)
							{
								num34 = this.gamePadState.ThumbSticks.Right.X * (0.0286f * this.sc.space_wsentivityX) * (float)this.sc.space_winvertX;
								num35 = this.gamePadState.ThumbSticks.Right.Y * (0.017f * this.sc.space_wsentivityY) * (float)this.sc.space_winvertY;
							}
							else
							{
								num34 = this.mouseX * (0.0012f * this.sc.space_wsentivityX) * (float)this.sc.space_winvertX;
								num35 = -this.mouseY * (0.0012f * this.sc.space_wsentivityY) * (float)this.sc.space_winvertY;
							}
							this.camradian += num34;
							this.camheight -= num35;
							this.camheight = MathHelper.Clamp(this.camheight, 1.7f, 4.5f);
							vector13 = this.gamePadState.ThumbSticks.Left;
							if (this.sc.usingMouse)
							{
								if (this.KMdown(this.sc.a_key))
								{
									vector13.X -= 1f;
								}
								if (this.KMdown(this.sc.d_key))
								{
									vector13.X += 1f;
								}
								if (this.KMdown(this.sc.w_key))
								{
									vector13.Y += 1f;
								}
								if (this.KMdown(this.sc.s_key))
								{
									vector13.Y -= 1f;
								}
							}
							float num36 = 1.5f;
							float num37 = 0.3f;
							if (this.KMdown(this.sc.leftshift_key) || this.gamePadState.Buttons.LeftStick == ButtonState.Pressed)
							{
								num37 = 0.9f;
							}
							this.walkspeed = num36 + num37 * 5f;
							this.vec = Vector3.Transform(new Vector3(-vector13.X / 1.3f, 0f, vector13.Y), Matrix.CreateRotationY(-3.14f - this.camradian)) * this.walkspeed;
							Vector3 vector14 = Vector3.Normalize(this.vec);
							if (this.vec.Length() > this.walkspeed)
							{
								this.vec = vector14 * this.walkspeed;
							}
							if (this.jumping)
							{
								Vector3 vector15 = Vector3.Transform(new Vector3(-vector13.X / 1.2f, 0f, vector13.Y), Matrix.CreateRotationY(-3.14f - this.camradian)) * 1f * num37;
								if ((this.fallVec + vector15).Length() < this.fallLim)
								{
									this.fallVec += vector15;
								}
								if ((this.fallVec + vector15).Length() >= this.fallLim && (this.fallVec + vector15).Length() < this.fallVec.Length())
								{
									this.fallVec += vector15;
								}
								this.vec = this.fallVec;
							}
							this.vec += this.hitVec;
							this.hitVec *= 0.9f;
						}
						float num38 = 13f;
						float num39 = this.campos.Y;
						if (this.nearfarm)
						{
							this.GetHeightFarm2(this.campos + this.vec, out this.groundHeight);
						}
						else
						{
							this.GetHeightOnly(ref this.tmake.heightData, this.campos + this.vec, out this.groundHeight);
						}
						float num40 = this.groundHeight;
						this.slopereducer = 1f;
						bool flag5 = this.campos.X < this.facility.facilityLocate.X + 865f && this.campos.X > this.facility.facilityLocate.X - 865f && this.campos.Z < this.facility.facilityLocate.Y + 200f && this.campos.Z > this.facility.facilityLocate.Y - 945f;
						if (flag5 && Facility.outsideCastle)
						{
							float num41 = 2f;
							bool flag6 = false;
							float num42;
							this.facility.GetHeightEntry(this.campos, out num42, num40);
							if (!this.facility.steep)
							{
								flag6 = true;
							}
							this.facility.GetHeightEntry(this.campos + this.vec, out this.groundHeight, num40);
							if (!this.facility.steep)
							{
								flag6 = true;
							}
							if (this.vec.Length() > 0f)
							{
								float num43 = (num42 - this.groundHeight) / this.vec.Length();
								if (Math.Abs(num43) < num41 || flag6 || this.jumping)
								{
									this.campos += this.vec;
								}
								else
								{
									Vector3 vector16 = new Vector3(0f, 0f, this.vec.Z);
									this.facility.GetHeightEntry(this.campos + vector16, out this.groundHeight, num40);
									num43 = (num42 - this.groundHeight) / this.vec.Length();
									if (Math.Abs(num43) < num41)
									{
										this.campos.X = this.campos.X + vector16.X;
										this.campos.Y = this.campos.Y + this.vec.Y;
										this.campos.Z = this.campos.Z + vector16.Z;
									}
									else
									{
										vector16 = new Vector3(this.vec.X, 0f, 0f);
										this.facility.GetHeightEntry(this.campos + vector16, out this.groundHeight, num40);
										num43 = (num42 - this.groundHeight) / this.vec.Length();
										if (Math.Abs(num43) < num41)
										{
											this.campos.X = this.campos.X + vector16.X;
											this.campos.Y = this.campos.Y + this.vec.Y;
											this.campos.Z = this.campos.Z + vector16.Z;
										}
										else
										{
											this.groundHeight = num42;
										}
									}
								}
							}
						}
						else if (!Facility.outsideCastle)
						{
							float num41 = 5.5f;
							float num44;
							this.facility.GetHeight(this.campos, out num44, num40);
							this.facility.GetHeight(this.campos + this.vec, out this.groundHeight, num40);
							if (this.vec.Length() > 0f)
							{
								float num45 = (num44 - this.groundHeight) / this.vec.Length();
								if (Math.Abs(num45) < num41)
								{
									this.campos += this.vec;
								}
								else
								{
									Vector3 vector17 = new Vector3(0f, 0f, this.vec.Z);
									this.facility.GetHeight(this.campos + vector17, out this.groundHeight, num40);
									num45 = (num44 - this.groundHeight) / this.vec.Length();
									if (Math.Abs(num45) < num41)
									{
										this.campos.X = this.campos.X + vector17.X;
										this.campos.Y = this.campos.Y + this.vec.Y;
										this.campos.Z = this.campos.Z + vector17.Z;
									}
									else
									{
										vector17 = new Vector3(this.vec.X, 0f, 0f);
										this.facility.GetHeight(this.campos + vector17, out this.groundHeight, num40);
										num45 = (num44 - this.groundHeight) / this.vec.Length();
										if (Math.Abs(num45) < num41)
										{
											this.campos.X = this.campos.X + vector17.X;
											this.campos.Y = this.campos.Y + this.vec.Y;
											this.campos.Z = this.campos.Z + vector17.Z;
										}
										else
										{
											this.groundHeight = num44;
										}
									}
								}
							}
						}
						if (Facility.outsideCastle && !flag5)
						{
							float num46;
							if (this.nearfarm)
							{
								this.GetHeightFarm2(this.campos, out num46);
								this.GetHeightFarm2(this.campos + this.vec, out this.groundHeight);
							}
							else
							{
								this.GetHeightOnly(ref this.tmake.heightData, this.campos, out num46);
								this.GetHeightOnly(ref this.tmake.heightData, this.campos + this.vec, out this.groundHeight);
							}
							this.dumpos = this.campos;
							if (this.vec.Length() > 0f)
							{
								float num47 = (num46 - this.groundHeight) / this.vec.Length();
								if (num47 > 0f)
								{
									this.campos += this.vec;
								}
								else if (this.nearfarm)
								{
									if (num47 >= 0f)
									{
										this.campos += this.vec;
									}
									else
									{
										this.vec.Y = 0f;
										this.vec.X = 0f;
										this.vec.Z = 0f;
									}
									this.GetHeightFarm2(this.campos, out this.groundHeight);
								}
								else
								{
									this.slopereducer = MathHelper.Clamp(Math.Abs((num47 + 0.5f) * (num47 + 0.5f)), 1f, 500f);
									this.vec.X = this.vec.X / this.slopereducer;
									this.vec.Z = this.vec.Z / this.slopereducer;
									this.campos += this.vec;
									this.GetHeightOnly(ref this.tmake.heightData, this.campos, out this.groundHeight);
								}
							}
							BoundingBox boundingBox = default(BoundingBox);
							Vector3 vector18 = Vector3.Transform(this.campos - this.rover.position, Matrix.Invert(this.rover.orientation));
							boundingBox.Min = new Vector3(-80f, -100f, -100f);
							boundingBox.Max = new Vector3(80f, 200f, 140f);
							if (boundingBox.Contains(vector18) == ContainmentType.Contains)
							{
								this.campos += Vector3.Normalize(this.aCampos - this.rover.position) * 4f;
							}
							if (this.lander.door1 == 10)
							{
								if (this.nearfarm)
								{
									this.rover.hitramp2(this.heights, this.tmake.normals, ref this.humanonramp, ref this.dumpos, ref this.campos, ref this.groundy, ref this.normalxxx, ref this.vec);
								}
								else
								{
									this.rover.hitramp2(this.tmake.heightData, this.tmake.normals, ref this.humanonramp, ref this.dumpos, ref this.campos, ref this.groundy, ref this.normalxxx, ref this.vec);
								}
								this.groundHeight = this.groundy.Y;
							}
						}
						float num48 = Vector3.Distance(this.oldcampos, this.campos);
						if (!this.jumping)
						{
							this.dist += num48;
						}
						if (this.dist > 500000f)
						{
							this.dist = 0f;
							this.olddist = 0f;
						}
						this.bobber = ((float)Math.Sin((double)(this.dist / num38)) + 1f) * 2f;
						float num49 = Math.Abs(this.dist - this.olddist);
						if (num49 > 30f)
						{
							this.olddist = this.dist;
						}
						this.camhite = 70f;
						this.campos.Y = this.groundHeight;
						this.updatePrints();
						if (num49 > 30f && !this.jumping && !Facility.inFacility)
						{
							this.print++;
							if (this.print > 1)
							{
								this.print = 0;
							}
							Vector3 vector19 = this.campos + Vector3.Transform(new Vector3(-7f, 0f, 0f), Matrix.CreateRotationY(-this.camradian));
							if (this.print == 1)
							{
								vector19 = this.campos + Vector3.Transform(new Vector3(7f, 0f, 0f), Matrix.CreateRotationY(-this.camradian));
							}
							if (this.print == 1)
							{
								this.sc.step.Play(this.sc.ev * 0.7f, (float)this.random.Next(-10, 10) / 100f, 0f);
							}
							float num50;
							Vector3 vector20;
							this.GetHeightAndNormal(ref this.tmake.heightData, ref this.tmake.normals, new Vector3(vector19.X, 0f, vector19.Z), out num50, out vector20);
							vector19.Y = num50 + 2f;
							Matrix matrix = Matrix.CreateRotationY(-this.camradian);
							matrix.Up = vector20;
							matrix.Right = Vector3.Cross(matrix.Forward, matrix.Up);
							matrix.Right = Vector3.Normalize(matrix.Right);
							matrix.Forward = Vector3.Cross(matrix.Up, matrix.Right);
							matrix.Forward = Vector3.Normalize(matrix.Forward);
							Matrix matrix2 = Matrix.CreateScale(0.8f) * matrix * Matrix.CreateTranslation(vector19);
							this.addPrint(matrix2);
						}
						if (this.jumping)
						{
							num39 += this.fallGrav;
							this.campos.Y = MathHelper.Max(this.groundHeight, num39);
						}
						if (this.campos.Y > this.groundHeight || this.jumpCalled || this.jumpCount > 0)
						{
							this.jumpCount--;
							this.jumping = true;
							this.jumpCalled = false;
							if (this.hitVec.Length() > 0f)
							{
								this.fallGrav = MathHelper.Min(this.hitVec.Y / 10f, 6f);
								this.hitVec.Y = 0f;
								this.fallVec = this.hitVec / 5f;
								this.hitVec = Vector3.Zero;
							}
							if (this.fallGrav >= this.gravLimdown)
							{
								this.fallGrav += this.fallAcc;
							}
							if (this.fallGrav < this.gravLimdown)
							{
								if (this.fallAcc != -0.15f)
								{
									this.sc.alarm1.Play(this.sc.ev, 0f, 0f);
								}
								this.fallAcc = -0.15f;
								this.fallGrav += this.fallAcc;
							}
						}
						else
						{
							if (this.jumping)
							{
								this.sc.land.Play(this.sc.ev, 0f, 0f);
							}
							if (this.fallGrav < -12f)
							{
								this.sc.bone.Play(this.sc.ev, 0f, 0f);
								this.overlay.damaged1 = true;
								this.vibrateControl = 60;
								this.iscamShake = true;
								this.camShakeTimer = 130;
							}
							this.jumpCalled = false;
							this.jumping = false;
							this.jumpCount = 0;
							this.fallVec = Vector3.Zero;
							this.fallGrav = 0f;
							this.fallAcc = 0f;
						}
						this.camlookpos.X = (float)(-(float)Math.Cos((double)this.camheight) * Math.Sin((double)this.camradian) * 200.0) + this.campos.X;
						this.camlookpos.Z = (float)(-(float)Math.Cos((double)this.camheight) * -(float)Math.Cos((double)this.camradian) * 200.0) + this.campos.Z;
						this.camlookpos.Y = (float)Math.Sin((double)this.camheight) * 200f + this.campos.Y;
						this.oldcampos = this.campos;
						if (this.iscamShake)
						{
							this.camShakeTimer--;
							this.camShake = (float)this.camShakeTimer / 100f * new Vector3((float)this.random.Next(-500, 500) / 100f, (float)this.random.Next(-500, 500) / 100f, (float)this.random.Next(-500, 500) / 100f);
							if (this.camShakeTimer <= 0)
							{
								this.iscamShake = false;
								this.camShakeTimer = 0;
								this.camShake = Vector3.Zero;
							}
						}
						this.aCampos = new Vector3(this.campos.X, this.campos.Y + this.bobber + this.camhite, this.campos.Z);
						this.aCamtarget = this.camShake + new Vector3(this.camlookpos.X, this.camlookpos.Y + this.bobber + this.camhite, this.camlookpos.Z);
						this.aUppy = Vector3.Up;
						this.lens = 70f;
					}
					if (this.sc.vehiclelerp < 1f)
					{
						this.sc.vehiclelerp += 0.025f;
						if (this.sc.vehiclelerp > 1f)
						{
							this.sc.vehiclelerp = 1f;
						}
						if (this.vehicleindex == 3)
						{
							this.lens = MathHelper.Lerp(70f, 80f, this.sc.vehiclelerp);
						}
						if (this.vehicleindex == 1)
						{
							this.lens = MathHelper.Lerp(80f, 70f, this.sc.vehiclelerp);
						}
						this.setLens(this.lens);
						this.aCampos = Vector3.Hermite(this.bCampos, Vector3.Zero, this.aCampos, Vector3.Zero, this.sc.vehiclelerp);
						this.aCamtarget = Vector3.Hermite(this.bCamtarget, Vector3.Zero, this.aCamtarget, Vector3.Zero, this.sc.vehiclelerp);
						this.aUppy = Vector3.Hermite(this.bUppy, Vector3.Zero, this.aUppy, Vector3.Zero, this.sc.vehiclelerp);
					}
					this.viewMatrix = Matrix.CreateLookAt(this.aCampos, this.aCamtarget, this.aUppy);
				}
				this.prevstate = this.gamePadState;
				if (this.myframe % 40 == 0)
				{
					this.atRover = false;
					if (this.vehicleindex == 3 && !Facility.inFacility)
					{
						float num51 = Vector3.DistanceSquared(this.aCampos, this.rover.position);
						if (num51 < 22500f)
						{
							Vector3 vector21 = Vector3.Normalize(this.aCamtarget - this.aCampos);
							Vector3 vector22 = Vector3.Normalize(this.rover.position - this.aCampos);
							if (Vector3.Dot(vector21, vector22) > 0.65f)
							{
								this.atRover = true;
							}
						}
					}
				}
				this.gemPOS = new Vector2((float)((int)Math.Round((double)(this.trnLoctn.X / 1000f)) * 1000), (float)((int)Math.Round((double)(this.trnLoctn.Z / 1000f)) * 1000));
				this.oldgemPOS = this.gemPOS;
				if (this.vehicleindex == 1 || this.lander.door1 != 1)
				{
					this.bucketCollide();
					if (this.rover.scooperON || this.sc.equip[3] > 1)
					{
						this.nearScooper(ref this.shale1, this.sc.shalehit1);
						this.nearScooper(ref this.shale2, this.sc.boulderhit2);
						this.nearScooper(ref this.shale3, this.sc.boulderhit3);
					}
					this.updateGems(ref this.shale1);
					this.updateGems(ref this.shale2);
					this.updateGems(ref this.shale3);
					for (int i = 0; i < this.shale1.xCount; i++)
					{
						this.shale1.hitDumper(this.shale1.xInst[i]);
						this.shale1.xInst[i].Update(ref this.tmake.heightData);
						this.shale1.xTrans[i].Trans = this.shale1.xInst[i].transform;
					}
					for (int j = 0; j < this.shale2.xCount; j++)
					{
						this.shale2.hitDumper(this.shale2.xInst[j]);
						this.shale2.xInst[j].Update(ref this.tmake.heightData);
						this.shale2.xTrans[j].Trans = this.shale2.xInst[j].transform;
					}
					for (int k = 0; k < this.shale3.xCount; k++)
					{
						this.shale3.hitDumper(this.shale3.xInst[k]);
						this.shale3.xInst[k].Update(ref this.tmake.heightData);
						this.shale3.xTrans[k].Trans = this.shale3.xInst[k].transform;
					}
				}
				else
				{
					this.updateGems(ref this.shale1);
					this.updateGems(ref this.shale2);
					this.updateGems(ref this.shale3);
				}
				astro.inDumper = this.rover.orientation * Matrix.CreateFromAxisAngle(this.rover.orientation.Forward, this.rover.leanAmt) * Matrix.CreateTranslation(this.rover.pp);
				astro.rovpos = this.rover.position;
				astroDupe.constantRoverPosition = this.rover.position;
				astro.rovveloc = Vector3.Zero;
				if (this.vehicleindex == 1)
				{
					astro.rovveloc = this.rover.velocity;
					this.sc.astronaut.Update(ref this.tmake.heightData, ref this.tmake.normals, 1, this.aCampos, this.aCamtarget, false);
					if (astro.someonedied)
					{
						if (this.objIndex == 5)
						{
							this.emo = GameplayScreen.act.killed;
						}
						this.sc.trophy.win(this.sc.trophy.spacedeath);
					}
					this.sc.astronaut.facingRover = this.rover.facingDirection;
					if (this.sc.astronaut.manhit)
					{
						this.sc.astronaut.manhit = false;
						this.vibrateControl = 30;
						return;
					}
				}
				else
				{
					bool flag7 = this.vehicleindex == 2 && this.lander.door1 == 1;
					this.sc.astronaut.Update(ref this.tmake.heightData, ref this.tmake.normals, 0, this.aCampos, this.aCamtarget, flag7);
					if (this.vehicleindex == 3)
					{
						if (this.sc.astronaut.chosen != -1)
						{
							this.nearAstro = true;
						}
						else
						{
							this.nearAstro = false;
						}
						if (this.rock.chosen != -1)
						{
							this.nearFlower = true;
							return;
						}
						this.nearFlower = false;
					}
				}
				return;
			}
			if (this.editcam)
			{
				this.editcam = false;
				this.sc.SaveSpacePrefs();
				return;
			}
			this.sc.back.Play(this.sc.ev, 0f, 0f);
			GamePad.SetVibration(this.myplayer, 0f, 0f);
			if (this.gravelI.State == SoundState.Playing)
			{
				this.gravelI.Pause();
			}
			if (this.roverEngineI.State == SoundState.Playing)
			{
				this.roverEngineI.Pause();
			}
			if (this.dropEngineI.State == SoundState.Playing)
			{
				this.dropEngineI.Pause();
			}
			if (this.breathing.State == SoundState.Playing)
			{
				this.breathing.Pause();
			}
			if (this.landerEngineI.State == SoundState.Playing)
			{
				this.landerEngineI.Pause();
			}
			if (this.overtureInstance.State == SoundState.Playing)
			{
				this.overtureInstance.Pause();
			}
			if (this.flowerradioInstance.State == SoundState.Playing)
			{
				this.flowerradioInstance.Pause();
			}
			this.sc.menutype = 1;
			string text = "Controls";
			if (this.sc.usingMouse)
			{
				text = "Keyboard";
				this.sc.Game.IsMouseVisible = true;
			}
			PauseMenuScreen pauseMenuScreen = new PauseMenuScreen(text);
			pauseMenuScreen.Accepted += delegate
			{
				try
				{
					GamePad.SetVibration(this.myplayer, 0f, 0f);
					this.dropEngineI.Dispose();
					this.landerEngineI.Dispose();
					this.roverEngineI.Dispose();
					this.gravelI.Dispose();
					this.overtureInstance.Dispose();
					this.flowerradioInstance.Dispose();
					this.breathing.Dispose();
				}
				catch
				{
				}
				this.resolveTarget1.Dispose();
				this.resolveTarget1 = null;
				this.resolveTarget2.Dispose();
				this.resolveTarget2 = null;
				this.shadowTarget.Dispose();
				this.shadowTarget = null;
				this.starmap.manmadestars.Dispose();
				this.starmap.tt.Dispose();
				this.starmap.starSheet.Dispose();
				this.starmap.colorArray1 = new Color[0];
				this.sc.astronaut.man.dupe.Clear();
				this.texdata = new MoonSurface.tex[0];
				try
				{
					if (this.backgroundThread != null)
					{
						this.backgroundThreadExit.Set();
						this.backgroundThread.Join();
					}
				}
				catch
				{
				}
				this.sc.SaveSpacePrefs();
				this.sc.LoadPrefs();
				this.sc.LoadSavedKeys();
				LoadingScreen2.Load(base.ScreenManager, false, null, new GameScreen[]
				{
					null,
					new MainMenu(false)
				});
				GC.Collect();
			};
			pauseMenuScreen.Cancelled += delegate
			{
			};
			this.sc.AddScreen(pauseMenuScreen, base.ControllingPlayer);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00412BCC File Offset: 0x00410DCC
		private void setLens(float lens)
		{
			if (this.vehicleindex == 1)
			{
				this.shortclip = 25f;
				this.farclip = 55000f;
				this.shortclip2 = 10f;
				this.farclip2 = 90000f;
				if (this.rover.dashcam == 1)
				{
					this.shortclip = 1f;
					this.shortclip2 = 0.5f;
				}
				this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip, this.farclip);
				this.longProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip2, this.farclip2);
				return;
			}
			if (this.vehicleindex == 2)
			{
				this.shortclip = 100f;
				this.farclip = 65000f;
				this.shortclip2 = 10f;
				this.farclip2 = 90000f;
				this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip, this.farclip);
				this.longProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip2, this.farclip2);
				return;
			}
			if (this.vehicleindex == 3)
			{
				this.shortclip = 10f;
				this.farclip = 55000f;
				this.shortclip2 = 10f;
				this.farclip2 = 90000f;
				this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip, this.farclip);
				this.longProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip2, this.farclip2);
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00412D74 File Offset: 0x00410F74
		private void setLens2(float lens)
		{
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, 70f, 250000f);
			this.longProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(lens), this.aspectratio, this.shortclip2, 260000f);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00412DC4 File Offset: 0x00410FC4
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
			this.frameCounter++;
			if (base.IsActive)
			{
				this.myframe++;
				this.displayObjective--;
				if (this.displayObjective > 450 && this.displayObjective < 455)
				{
					this.memoTimer = 300;
					GameplayScreen.memo.Length = 0;
					if (this.firstMess)
					{
						if (this.sc.usingMouse)
						{
							GameplayScreen.memo.Append("Welcome Back : Press Esc For Menu");
						}
						else
						{
							GameplayScreen.memo.Append("Welcome Back : Press Back For Menu");
						}
						this.memoIcon = 2;
						this.sc.tada2.Play(this.sc.ev * 0.6f, 0f, 0f);
						this.firstMess = false;
						this.displayObjective = 240;
					}
					else
					{
						if (this.objIndex == 0)
						{
							GameplayScreen.memo.Append("Good Job You Can Breathe");
							this.sc.tada2.Play(this.sc.ev * 0.6f, 0f, 0f);
						}
						else if (this.objIndex == 1)
						{
							GameplayScreen.memo.Append("Impressive Flying");
							this.sc.tada3.Play(this.sc.ev * 0.6f, 0f, 0f);
						}
						else if (this.objIndex == 2)
						{
							if (!this.sc.man4)
							{
								this.sc.fanfare.Play(this.sc.ev, 0f, 0f);
								this.sc.man4 = true;
								this.sc.SaveEquipables();
								GameplayScreen.memo.Append("New Character Unlocked");
							}
							else
							{
								this.sc.tada4.Play(this.sc.ev * 0.6f, 0f, 0f);
								GameplayScreen.memo.Append("Epic Rescue Strategy");
							}
						}
						else if (this.objIndex == 3)
						{
							this.sc.tada5.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("Congrats You Rock");
						}
						else if (this.objIndex == 4)
						{
							this.sc.tada6.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("Elon Musk Is Jealous");
						}
						else if (this.objIndex == 5)
						{
							this.sc.tada7.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("You Murderer You");
							this.overlay.bigfacilitymarker = true;
						}
						else if (this.objIndex == 6)
						{
							this.sc.tada3.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("You Are A Clever One");
						}
						else if (this.objIndex == 7)
						{
							this.sc.tada4.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("Now You Learned The Secret");
						}
						else if (this.objIndex == 8)
						{
							this.sc.tada5.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("Found New Mission");
						}
						else if (this.objIndex == 9)
						{
							this.sc.tada6.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("That Was A Great Rescue");
						}
						else if (this.objIndex == 10)
						{
							this.sc.tada2.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("You Could Leave The Planet");
						}
						else if (this.objIndex == 11)
						{
							this.sc.tada7.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("Another Hero in the Making");
						}
						else if (this.objIndex == 12)
						{
							this.sc.tada3.Play(this.sc.ev * 0.6f, 0f, 0f);
							GameplayScreen.memo.Append("You Are The Best Player");
						}
						this.memoIcon = 2;
						this.displayObjective = 450;
						this.objIndex++;
						if (this.objIndex > this.objectiveData.Count - 1)
						{
							this.displayObjective = 0;
							this.objIndex = this.objectiveData.Count - 1;
						}
					}
				}
				if (this.displayObjective > 1 && this.displayObjective < 10)
				{
					this.typewriterblank = 500f;
					this.textflag = 0;
					this.typewriterwait = 480f;
					this.typeposition = 0f;
					this.typevertical = (float)this.random.Next(100, 150);
					this.typewriterdelay = this.random.Next(2, 7);
					this.displayObjective = 0;
				}
				if (this.objIndex == 8 || this.objIndex == 12)
				{
					this.waiting++;
					if (this.waiting > 600)
					{
						this.emo = GameplayScreen.act.waited;
					}
				}
				if (this.myframe % 180 == 0 && this.displayObjective <= 0)
				{
					switch (this.objIndex)
					{
					case 0:
						if (this.emo == GameplayScreen.act.rovered)
						{
							this.displayObjective = 850;
						}
						break;
					case 1:
						if (this.emo == GameplayScreen.act.landered)
						{
							this.displayObjective = 850;
							Vector3 vector = Vector3.Zero;
							for (int i = 0; i < this.sc.astronaut.man.dupe.Count; i++)
							{
								if (this.sc.astronaut.man.dupe[i].headType == 2 && this.sc.astronaut.man.dupe[i].emo == astroDupe.emotion.stranded)
								{
									vector = this.sc.astronaut.man.dupe[i].mypos;
								}
							}
							if (vector != Vector3.Zero)
							{
								Overlay.manbouy = vector;
							}
						}
						break;
					case 2:
					{
						for (int j = 0; j < this.sc.astronaut.man.dupe.Count; j++)
						{
							if (astro.teamCount > astro.oldteamCount)
							{
								astro.oldteamCount = astro.teamCount;
								this.displayObjective = 850;
							}
						}
						break;
					}
					case 3:
						if (this.emo == GameplayScreen.act.rocked)
						{
							this.displayObjective = 850;
						}
						break;
					case 4:
						if (this.emo == GameplayScreen.act.refined)
						{
							this.displayObjective = 850;
						}
						break;
					case 5:
						if (this.emo == GameplayScreen.act.killed)
						{
							this.displayObjective = 850;
						}
						break;
					case 6:
						if (this.emo == GameplayScreen.act.facilityfound)
						{
							this.displayObjective = 850;
							this.overlay.bigfacilitymarker = true;
						}
						break;
					case 7:
						if (this.emo == GameplayScreen.act.cloned)
						{
							this.displayObjective = 850;
						}
						break;
					case 8:
						if (this.emo == GameplayScreen.act.waited)
						{
							this.displayObjective = 850;
							this.waiting = 0;
							this.emo = GameplayScreen.act.nothing;
							Vector3 vector2 = Vector3.Zero;
							for (int k = 0; k < this.sc.astronaut.man.dupe.Count; k++)
							{
								if (this.sc.astronaut.man.dupe[k].emo == astroDupe.emotion.stranded)
								{
									vector2 = this.sc.astronaut.man.dupe[k].mypos;
								}
							}
							if (vector2 != Vector3.Zero)
							{
								Overlay.manbouy = vector2;
							}
						}
						break;
					case 9:
					{
						for (int l = 0; l < this.sc.astronaut.man.dupe.Count; l++)
						{
							if (astro.teamCount > astro.oldteamCount)
							{
								astro.oldteamCount = astro.teamCount;
								this.displayObjective = 850;
							}
						}
						break;
					}
					case 10:
						if (this.emo == GameplayScreen.act.dropship)
						{
							this.displayObjective = 850;
							Vector3 vector3 = Vector3.Zero;
							for (int m = 0; m < this.sc.astronaut.man.dupe.Count; m++)
							{
								if (this.sc.astronaut.man.dupe[m].emo == astroDupe.emotion.stranded)
								{
									vector3 = this.sc.astronaut.man.dupe[m].mypos;
								}
							}
							if (vector3 != Vector3.Zero)
							{
								Overlay.manbouy = vector3;
							}
						}
						break;
					case 11:
					{
						for (int n = 0; n < this.sc.astronaut.man.dupe.Count; n++)
						{
							if (astro.teamCount > astro.oldteamCount)
							{
								astro.oldteamCount = astro.teamCount;
								this.displayObjective = 850;
							}
						}
						break;
					}
					case 12:
						if (this.emo == GameplayScreen.act.waited)
						{
							this.displayObjective = 850;
							this.waiting = 0;
						}
						break;
					}
				}
				this.radiotimer--;
				if (this.radiotimer <= 0)
				{
					if (this.radiocount == 13)
					{
						float num = Vector2.Distance(new Vector2(this.lander.position.X, this.lander.position.Z), this.facility.facilityLocate);
						num = 1f - MathHelper.Clamp(num / 12000f, 0f, 0.93f);
						int num2 = this.random.Next(1, 100);
						if (num2 < 40)
						{
							this.sc.radio3.Play(this.sc.voiceVolume * num, 0.1f, 0f);
						}
						if (num2 >= 40)
						{
							this.sc.radio2.Play(this.sc.voiceVolume * num, 0f, 0f);
						}
					}
					this.radiocount = 0;
				}
				this.horntimer--;
				if (this.horntimer <= 0)
				{
					if (this.horncount >= 1)
					{
						this.sc.astronaut.everyoneOut = true;
						if (this.horncount >= 5)
						{
							messagebox messagebox = new messagebox("Really?  10 Honks?", 0, 0);
							this.sc.trophy.win(this.sc.trophy.walkinghere);
							this.sc.AddScreen(messagebox, null);
						}
						if (this.horncount == 20)
						{
							messagebox messagebox2 = new messagebox("You Are Crazy!", 0, 0);
							this.sc.AddScreen(messagebox2, null);
						}
						if (this.horncount >= 80 && this.horncount <= 100)
						{
							messagebox messagebox3 = new messagebox("Use The Radio", 0, 0);
							this.sc.AddScreen(messagebox3, null);
						}
					}
					this.horncount = 0;
				}
				this.sc.myTimer += 1f;
				if (this.vehicleindex == 1 || this.vehicleindex == 3)
				{
					this.TireTreads();
					this.skids.Update(gameTime);
				}
				else
				{
					this.dust.Update(gameTime);
					this.streaks.Update(gameTime);
				}
				this.terrainhester();
				if (this.frameCounter % 120 == 0)
				{
					if (this.rock.chosenfar != -1)
					{
						this.overlay.flower = this.rock.flowerInst[this.rock.chosenfar].mypos;
					}
					else
					{
						this.overlay.flower = Vector3.Zero;
					}
					if (this.overlay.bouy1 != Vector3.Zero)
					{
						if (Math.Abs(this.trnLoctn.X - this.overlay.bouy1.X) > 150000f)
						{
							int num3 = Math.Sign(this.trnLoctn.X - this.overlay.bouy1.X);
							Overlay overlay = this.overlay;
							overlay.bouy1.X = overlay.bouy1.X + (float)(num3 * 300000);
						}
						if (Math.Abs(this.trnLoctn.Z - this.overlay.bouy1.Z) > 150000f)
						{
							int num4 = Math.Sign(this.trnLoctn.Z - this.overlay.bouy1.Z);
							Overlay overlay2 = this.overlay;
							overlay2.bouy1.Z = overlay2.bouy1.Z + (float)(num4 * 300000);
						}
					}
					if (this.overlay.bouy2 != Vector3.Zero)
					{
						if (Math.Abs(this.trnLoctn.X - this.overlay.bouy2.X) > 150000f)
						{
							int num5 = Math.Sign(this.trnLoctn.X - this.overlay.bouy2.X);
							Overlay overlay3 = this.overlay;
							overlay3.bouy2.X = overlay3.bouy2.X + (float)(num5 * 300000);
						}
						if (Math.Abs(this.trnLoctn.Z - this.overlay.bouy2.Z) > 150000f)
						{
							int num6 = Math.Sign(this.trnLoctn.Z - this.overlay.bouy2.Z);
							Overlay overlay4 = this.overlay;
							overlay4.bouy2.Z = overlay4.bouy2.Z + (float)(num6 * 300000);
						}
					}
					if (Overlay.manbouy != Vector3.Zero)
					{
						if (Math.Abs(this.trnLoctn.X - Overlay.manbouy.X) > 150000f)
						{
							int num7 = Math.Sign(this.trnLoctn.X - Overlay.manbouy.X);
							Overlay.manbouy.X = Overlay.manbouy.X + (float)(num7 * 300000);
						}
						if (Math.Abs(this.trnLoctn.Z - Overlay.manbouy.Z) > 150000f)
						{
							int num8 = Math.Sign(this.trnLoctn.Z - Overlay.manbouy.Z);
							Overlay.manbouy.Z = Overlay.manbouy.Z + (float)(num8 * 300000);
						}
					}
					for (int num9 = 0; num9 < this.sc.astronaut.man.dupe.Count; num9++)
					{
						if (Math.Abs(this.trnLoctn.X - this.sc.astronaut.man.dupe[num9].mypos.X) > 150000f)
						{
							int num10 = Math.Sign(this.trnLoctn.X - this.sc.astronaut.man.dupe[num9].mypos.X);
							astroDupe astroDupe = this.sc.astronaut.man.dupe[num9];
							astroDupe.mypos.X = astroDupe.mypos.X + (float)(num10 * 300000);
						}
						if (Math.Abs(this.trnLoctn.Z - this.sc.astronaut.man.dupe[num9].mypos.Z) > 150000f)
						{
							int num11 = Math.Sign(this.trnLoctn.Z - this.sc.astronaut.man.dupe[num9].mypos.Z);
							astroDupe astroDupe2 = this.sc.astronaut.man.dupe[num9];
							astroDupe2.mypos.Z = astroDupe2.mypos.Z + (float)(num11 * 300000);
						}
					}
					if (Math.Abs(this.trnLoctn.X - this.dropshipPOS.X) > 150000f)
					{
						int num12 = Math.Sign(this.trnLoctn.X - this.dropshipPOS.X);
						this.dropshipPOS.X = this.dropshipPOS.X + (float)(num12 * 300000);
					}
					if (Math.Abs(this.trnLoctn.Z - this.dropshipPOS.Z) > 150000f)
					{
						int num13 = Math.Sign(this.trnLoctn.Z - this.dropshipPOS.Z);
						this.dropshipPOS.Z = this.dropshipPOS.Z + (float)(num13 * 300000);
					}
					if (Math.Abs(this.trnLoctn.X - this.farmLocation.X) > 150000f)
					{
						int num14 = Math.Sign(this.trnLoctn.X - this.farmLocation.X);
						this.farmLocation.X = this.farmLocation.X + (float)(num14 * 300000);
						Lander.farmLocation = this.farmLocation;
						Rover.farmLocation = this.farmLocation;
						astroDupe.farmLocation = this.farmLocation;
						this.overlay.farm.X = this.farmLocation.X;
						this.overlay.farm.Z = this.farmLocation.Z;
					}
					if (Math.Abs(this.trnLoctn.Z - this.farmLocation.Z) > 150000f)
					{
						int num15 = Math.Sign(this.trnLoctn.Z - this.farmLocation.Z);
						this.farmLocation.Z = this.farmLocation.Z + (float)(num15 * 300000);
						Lander.farmLocation = this.farmLocation;
						Rover.farmLocation = this.farmLocation;
						astroDupe.farmLocation = this.farmLocation;
						this.overlay.farm.X = this.farmLocation.X;
						this.overlay.farm.Z = this.farmLocation.Z;
					}
					if (Math.Abs(this.trnLoctn.X - this.facility.facilityLocate.X) > 150000f)
					{
						int num16 = Math.Sign(this.trnLoctn.X - this.facility.facilityLocate.X);
						Facility facility = this.facility;
						facility.facilityLocate.X = facility.facilityLocate.X + (float)(num16 * 300000);
						this.overlay.facility.X = this.facility.facilityLocate.X;
						this.overlay.facility.Z = this.facility.facilityLocate.Y;
					}
					if (Math.Abs(this.trnLoctn.Z - this.facility.facilityLocate.Y) > 150000f)
					{
						int num17 = Math.Sign(this.trnLoctn.Z - this.facility.facilityLocate.Y);
						Facility facility2 = this.facility;
						facility2.facilityLocate.Y = facility2.facilityLocate.Y + (float)(num17 * 300000);
						this.overlay.facility.X = this.facility.facilityLocate.X;
						this.overlay.facility.Z = this.facility.facilityLocate.Y;
					}
					if (this.vehicleindex == 1)
					{
						if (Math.Abs(this.rover.position.X - this.lander.position.X) > 150000f)
						{
							int num18 = Math.Sign(this.rover.position.X - this.lander.position.X);
							Lander lander = this.lander;
							lander.position.X = lander.position.X + (float)(num18 * 300000);
							for (int num19 = 0; num19 < 12; num19 += 3)
							{
								this.rover.region1[num19] += (float)(num18 * 300000);
								this.rover.region2[num19] += (float)(num18 * 300000);
								this.rover.region3[num19] += (float)(num18 * 300000);
							}
						}
						if (Math.Abs(this.rover.position.Z - this.lander.position.Z) > 150000f)
						{
							int num20 = Math.Sign(this.rover.position.Z - this.lander.position.Z);
							Lander lander2 = this.lander;
							lander2.position.Z = lander2.position.Z + (float)(num20 * 300000);
							for (int num21 = 0; num21 < 12; num21 += 3)
							{
								this.rover.region1[num21 + 2] += (float)(num20 * 300000);
								this.rover.region2[num21 + 2] += (float)(num20 * 300000);
								this.rover.region3[num21 + 2] += (float)(num20 * 300000);
							}
						}
					}
				}
				if (this.frameCounter % 2 == 0)
				{
					float num22 = Vector2.Distance(new Vector2(this.trnLoctn.X, this.trnLoctn.Z), new Vector2(this.oldtrnLoctn.X, this.oldtrnLoctn.Z));
					this.delta.X = this.trnLoctn.X - this.oldtrnLoctn.X;
					this.delta.Z = this.trnLoctn.Z - this.oldtrnLoctn.Z;
					this.delta.Y = 0f;
					if (num22 >= 5f && this.delta.LengthSquared() > 0f)
					{
						this.starRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.Normalize(this.delta), Vector3.Up), num22 / 110000f);
					}
					this.oldtrnLoctn.X = this.trnLoctn.X;
					this.oldtrnLoctn.Y = this.trnLoctn.Y;
					this.oldtrnLoctn.Z = this.trnLoctn.Z;
					if (this.vehicleindex == 3)
					{
						this.starRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(new Vector3(-1f, 0f, 0.4f), Vector3.Up), 6E-05f);
					}
					else
					{
						this.starRot *= Matrix.CreateFromAxisAngle(Vector3.Cross(new Vector3(-1f, 0f, 0.1f), Vector3.Up), 2E-05f);
					}
					this.sunDir = -Vector3.Normalize(Vector3.Transform(new Vector3(1f, 0f, 0f), this.starRot));
					this.updateMusic();
				}
				if (this.frameCounter % 3 == 0)
				{
					if (this.vehicleindex == 1)
					{
						Vector2 vector4 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.lander.position.X, this.lander.position.Z);
						Vector2 vector5 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.landerNeedle = (float)Math.Acos((double)(Vector2.Dot(vector4, vector5) / (vector4.Length() * vector5.Length())));
						this.overlay.landerNeedle = ((vector4.X * vector5.Y - vector5.X * vector4.Y > 0f) ? this.overlay.landerNeedle : (-this.overlay.landerNeedle));
					}
					if (this.overlay.flower != Vector3.Zero)
					{
						Vector2 vector6 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.overlay.flower.X, this.overlay.flower.Z);
						Vector2 vector7 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.flowerNeedle = (float)Math.Acos((double)(Vector2.Dot(vector6, vector7) / (vector6.Length() * vector7.Length())));
						this.overlay.flowerNeedle = ((vector6.X * vector7.Y - vector7.X * vector6.Y > 0f) ? this.overlay.flowerNeedle : (-this.overlay.flowerNeedle));
					}
					if (this.overlay.bouy1 != Vector3.Zero)
					{
						Vector2 vector8 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.overlay.bouy1.X, this.overlay.bouy1.Z);
						Vector2 vector9 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.bouy1Needle = (float)Math.Acos((double)(Vector2.Dot(vector8, vector9) / (vector8.Length() * vector9.Length())));
						this.overlay.bouy1Needle = ((vector8.X * vector9.Y - vector9.X * vector8.Y > 0f) ? this.overlay.bouy1Needle : (-this.overlay.bouy1Needle));
					}
					if (this.overlay.bouy2 != Vector3.Zero)
					{
						Vector2 vector10 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.overlay.bouy2.X, this.overlay.bouy2.Z);
						Vector2 vector11 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.bouy2Needle = (float)Math.Acos((double)(Vector2.Dot(vector10, vector11) / (vector10.Length() * vector11.Length())));
						this.overlay.bouy2Needle = ((vector10.X * vector11.Y - vector11.X * vector10.Y > 0f) ? this.overlay.bouy2Needle : (-this.overlay.bouy2Needle));
					}
					if (Overlay.manbouy != Vector3.Zero)
					{
						Vector2 vector12 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(Overlay.manbouy.X, Overlay.manbouy.Z);
						Vector2 vector13 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.bouymanNeedle = (float)Math.Acos((double)(Vector2.Dot(vector12, vector13) / (vector12.Length() * vector13.Length())));
						this.overlay.bouymanNeedle = ((vector12.X * vector13.Y - vector13.X * vector12.Y > 0f) ? this.overlay.bouymanNeedle : (-this.overlay.bouymanNeedle));
					}
					if (this.overlay.facility != Vector3.Zero)
					{
						Vector2 vector14 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.overlay.facility.X, this.overlay.facility.Z);
						Vector2 vector15 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.facilityNeedle = (float)Math.Acos((double)(Vector2.Dot(vector14, vector15) / (vector14.Length() * vector15.Length())));
						this.overlay.facilityNeedle = ((vector14.X * vector15.Y - vector15.X * vector14.Y > 0f) ? this.overlay.facilityNeedle : (-this.overlay.facilityNeedle));
					}
					if (this.overlay.farm != Vector3.Zero)
					{
						Vector2 vector16 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.overlay.farm.X, this.overlay.farm.Z);
						Vector2 vector17 = new Vector2(this.aCampos.X, this.aCampos.Z) - new Vector2(this.aCamtarget.X, this.aCamtarget.Z);
						this.overlay.farmNeedle = (float)Math.Acos((double)(Vector2.Dot(vector16, vector17) / (vector16.Length() * vector17.Length())));
						this.overlay.farmNeedle = ((vector16.X * vector17.Y - vector17.X * vector16.Y > 0f) ? this.overlay.farmNeedle : (-this.overlay.farmNeedle));
					}
				}
				Vector3 vector18 = Vector3.Zero;
				if (this.vehicleindex == 1)
				{
					vector18 = this.rover.position;
				}
				if (this.vehicleindex == 2)
				{
					vector18 = this.lander.position;
				}
				if (this.vehicleindex == 3)
				{
					vector18 = this.aCampos;
				}
				this.rock.updateSLAB(this.myframe, this.vehicleindex, vector18, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
				if (this.myframe % 3 == 0)
				{
					this.rock.removeOBJECTS(this.aCampos, this.aCamtarget, this.vehicleindex, vector18, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData);
				}
				this.rock.collideROCKS(this.vehicleindex, ref this.lander, ref this.rover, this.aCampos, ref this.sc.boulderhit, ref this.sc.crack, ref this.tmake.heightData, ref this.tmake.normals, ref this.objectData, ref this.vec, ref this.hitVec);
				if (this.vehicleindex == 1 && this.nearfarm && this.myframe % 2 == 0)
				{
					this.tragetCount++;
					if (this.tragetCount > 4)
					{
						this.tragetCount = 0;
					}
					if (this.tragetCount == 0)
					{
						this.barnloc1 = new Vector3(391f, 0f, 1636f) + this.farmLocation;
						this.xSpace = new Vector2(41f, 742f) + new Vector2(this.farmLocation.X, this.farmLocation.X);
						this.zSpace = new Vector2(1388f, 1859f) + new Vector2(this.farmLocation.Z, this.farmLocation.Z);
					}
					if (this.tragetCount == 1)
					{
						this.barnloc1 = new Vector3(1599f, 0f, -59f) + this.farmLocation;
						this.xSpace = new Vector2(1431f, 1768f) + new Vector2(this.farmLocation.X, this.farmLocation.X);
						this.zSpace = new Vector2(-209f, 91f) + new Vector2(this.farmLocation.Z, this.farmLocation.Z);
					}
					if (this.tragetCount == 2)
					{
						this.barnloc1 = new Vector3(-1646f, 0f, -188f) + this.farmLocation;
						this.xSpace = new Vector2(-2075f, -1217f) + new Vector2(this.farmLocation.X, this.farmLocation.X);
						this.zSpace = new Vector2(-689f, 314f) + new Vector2(this.farmLocation.Z, this.farmLocation.Z);
					}
					if (this.tragetCount == 3)
					{
						this.barnloc1 = new Vector3(1879f, 0f, -1732f) + this.farmLocation;
						this.xSpace = new Vector2(1670f, 2088f) + new Vector2(this.farmLocation.X, this.farmLocation.X);
						this.zSpace = new Vector2(-1928f, -1536f) + new Vector2(this.farmLocation.Z, this.farmLocation.Z);
					}
					if (this.tragetCount == 4)
					{
						this.barnloc1 = new Vector3(-1109f, 0f, -1645f) + this.farmLocation;
						this.xSpace = new Vector2(-1550f, -668f) + new Vector2(this.farmLocation.X, this.farmLocation.X);
						this.zSpace = new Vector2(-1819f, -1472f) + new Vector2(this.farmLocation.Z, this.farmLocation.Z);
					}
					if (this.rover.position.X >= this.xSpace.X && this.rover.position.X <= this.xSpace.Y && this.rover.position.Z >= this.zSpace.X && this.rover.position.Z <= this.zSpace.Y)
					{
						this.sc.boulderhit.Play(this.sc.ev, 0f, 0f);
						Vector2 vector19 = Vector2.Zero;
						Vector2 vector20 = new Vector2(this.rover.position.X - this.barnloc1.X, this.rover.position.Z - this.barnloc1.Z);
						if (vector20.LengthSquared() > 0f)
						{
							vector19 = Vector2.Normalize(vector20);
						}
						this.rover.grav.X = vector19.X * 20f;
						this.rover.grav.Z = vector19.Y * 20f;
						Rover.fric = 0.99f;
						Rover.rockhitCount = 140;
						this.rover.velocity = Vector3.Zero;
						this.rover.movement *= 0f;
					}
				}
				if (this.sc.gemDropType == 1)
				{
					if (this.firstshale < 4)
					{
						this.memoTimer = 190;
						GameplayScreen.memo.Length = 0;
						GameplayScreen.memo.Append("common shale rock");
						this.memoIcon = 2;
						this.sc.gemfound.Play(this.sc.ev, -0.2f, 0f);
						this.firstshale++;
					}
					this.dropGems(ref this.shale1, this.random.Next(30, 60));
				}
				if (this.sc.gemDropType == 2)
				{
					if (this.firstruby < 6)
					{
						this.memoTimer = 190;
						GameplayScreen.memo.Length = 0;
						GameplayScreen.memo.Append("rare rubies found");
						this.memoIcon = 2;
						this.sc.gemfound.Play(this.sc.ev, 0f, 0f);
						this.firstruby++;
					}
					this.dropGems(ref this.shale2, this.random.Next(20, 50));
				}
				if (this.sc.gemDropType == 3)
				{
					if (this.firstsapphire < 15)
					{
						this.memoTimer = 190;
						GameplayScreen.memo.Length = 0;
						GameplayScreen.memo.Append("explosive ornite found");
						this.memoIcon = 2;
						this.sc.gemfound.Play(this.sc.ev, 0.2f, 0f);
						this.firstsapphire++;
					}
					this.dropGems(ref this.shale3, this.random.Next(20, 40));
				}
				this.sc.gemDropType = 0;
				if (this.frameCounter % 100 == 0 && this.vehicleindex == 1)
				{
					Vector2 vector21 = new Vector2(this.rover.position.X, this.rover.position.Z);
					if (this.overlay.bouy1 != Vector3.Zero && Vector2.Distance(new Vector2(this.overlay.bouy1.X, this.overlay.bouy1.Z), vector21) < 90f)
					{
						this.overlay.bouy1 = Vector3.Zero;
						this.sc.eraseBeacon.Play(this.sc.ev, 0f, 0f);
						this.overlay.nextBouy = 1;
					}
					if (this.overlay.bouy2 != Vector3.Zero && Vector2.Distance(new Vector2(this.overlay.bouy2.X, this.overlay.bouy2.Z), vector21) < 90f)
					{
						this.overlay.bouy2 = Vector3.Zero;
						this.sc.eraseBeacon.Play(this.sc.ev, 0f, 0f);
						this.overlay.nextBouy = 2;
					}
				}
				float num23 = MathHelper.Clamp((this.dropshipDistance - 17000f) / 70000f, 0f, 1f);
				float num24 = MathHelper.Hermite(4500f, 0f, -7500f, 0f, num23);
				if (num23 <= 0f)
				{
					float num25 = 9999f;
					this.GetHeightOnly(ref this.tmake.heightData, new Vector3(this.dropshipPOS.X, this.dropshipPOS.Y, this.dropshipPOS.Z + 3000f), out num25);
					this.GetHeightOnly(ref this.tmake.heightData, new Vector3(this.dropshipPOS.X, this.dropshipPOS.Y, this.dropshipPOS.Z + 9000f), out this.surf);
					if (this.surf <= num25)
					{
						this.surf = num25;
					}
				}
				float num26 = MathHelper.Hermite(0.002f, 0f, 1f, 0f, num23);
				if (!this.lander.onDropship)
				{
					this.dropshipAcc -= 0.5f;
					if (this.dropshipAcc < 20f)
					{
						this.dropshipAcc = 20f;
					}
				}
				else
				{
					if (this.objIndex == 10)
					{
						this.emo = GameplayScreen.act.dropship;
					}
					this.dropshipAcc += 1f;
					if (this.dropshipAcc > 250f)
					{
						this.dropshipAcc = 250f;
					}
				}
				this.dropshipPOS.Z = this.dropshipPOS.Z + this.dropshipAcc;
				this.lander.dropshipVeloc.Z = this.dropshipAcc;
				this.dropshipPOS.Y = MathHelper.Lerp(this.dropshipPOS.Y, this.surf + num24, num26);
				this.lander.dropship = this.dropshipPOS;
				this.dropshipBox = new BoundingBox(this.DSmin * 5f + this.dropshipPOS, this.DSmax * 5f + this.dropshipPOS);
				this.lander.insideDropship = this.dropshipBox.Intersects(new BoundingSphere(new Vector3(this.lander.position.X, this.lander.position.Y, this.lander.position.Z + this.dropshipAcc), 230f));
				if (this.vehicleindex == 3 && Facility.inFacility)
				{
					this.facility.Update();
				}
				if (this.vehicleindex == 3)
				{
					this.vehicleDistance = Vector3.Distance(this.aCampos, this.lander.position);
				}
				else
				{
					this.vehicleDistance = Vector3.Distance(this.rover.position, this.lander.position);
				}
				this.dropshipDistance = Vector2.Distance(new Vector2(this.dropshipPOS.X, this.dropshipPOS.Z), new Vector2(this.aCampos.X, this.aCampos.Z));
				this.faciltyDistance = Vector2.Distance(new Vector2(this.facility.facilityLocate.X, this.facility.facilityLocate.Y), new Vector2(this.trnLoctn.X, this.trnLoctn.Z));
				this.overlay.Update(this.vehicleindex);
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00415A58 File Offset: 0x00413C58
		private void terrainhester()
		{
			if (this.vehicleindex == 1)
			{
				this.trnLoctn.X = this.rover.position.X;
				this.trnLoctn.Y = this.rover.position.Y;
				this.trnLoctn.Z = this.rover.position.Z;
				this.cutoff = 300;
			}
			else if (this.vehicleindex == 2)
			{
				this.trnLoctn.X = this.lander.position.X;
				this.trnLoctn.Y = this.lander.position.Y;
				this.trnLoctn.Z = this.lander.position.Z;
				this.cutoff = 300;
			}
			else if (this.vehicleindex == 3)
			{
				this.trnLoctn.X = this.aCampos.X;
				this.trnLoctn.Y = this.aCampos.Y;
				this.trnLoctn.Z = this.aCampos.Z;
				this.cutoff = 300;
			}
			this.nearfarm = this.trnLoctn.X < this.farmLocation.X + 2000f && this.trnLoctn.X > this.farmLocation.X - 2000f && this.trnLoctn.Z < this.farmLocation.Z + 2000f && this.trnLoctn.Z > this.farmLocation.Z - 2000f;
			Lander.nearfarm = this.nearfarm;
			Rover.nearfarm = this.nearfarm;
			Vector3 vector = this.trnLoctn;
			bool flag = Vector2.Distance(new Vector2(this.trnLoctn.X, this.trnLoctn.Z), new Vector2(this.oldtrnLoctn2.X, this.oldtrnLoctn2.Z)) > (float)this.cutoff;
			if (Vector2.Distance(new Vector2(vector.X, vector.Z), new Vector2(this.trnLoctnLast.X, this.trnLoctnLast.Z)) > 149f)
			{
				while (this.trnLoctnLast.X != vector.X || this.trnLoctnLast.Z != vector.Z)
				{
					int num = 140;
					if (this.trnLoctnLast.X < vector.X)
					{
						this.trnLoctnLast.X = this.trnLoctnLast.X + (float)num;
						if (this.trnLoctnLast.X > vector.X)
						{
							this.trnLoctnLast.X = vector.X;
						}
					}
					if (this.trnLoctnLast.X > vector.X)
					{
						this.trnLoctnLast.X = this.trnLoctnLast.X - (float)num;
						if (this.trnLoctnLast.X < vector.X)
						{
							this.trnLoctnLast.X = vector.X;
						}
					}
					if (this.trnLoctnLast.Z < vector.Z)
					{
						this.trnLoctnLast.Z = this.trnLoctnLast.Z + (float)num;
						if (this.trnLoctnLast.Z > vector.Z)
						{
							this.trnLoctnLast.Z = vector.Z;
						}
					}
					if (this.trnLoctnLast.Z > vector.Z)
					{
						this.trnLoctnLast.Z = this.trnLoctnLast.Z - (float)num;
						if (this.trnLoctnLast.Z < vector.Z)
						{
							this.trnLoctnLast.Z = vector.Z;
						}
					}
					this.trnLoctn = this.trnLoctnLast;
					this.whatz = (int)Math.Round((double)(this.trnLoctn.Z / (float)this.gridscale)) * this.gridscale;
					this.zgrid = (int)((this.trnLoctn.Z + (float)(this.gridscale / 2)) / (float)this.gridscale % (float)this.unit + (float)this.unit) % this.unit;
					this.whatx = (int)Math.Round((double)(this.trnLoctn.X / (float)this.gridscale)) * this.gridscale;
					this.xgrid = (int)((this.trnLoctn.X + (float)(this.gridscale / 2)) / (float)this.gridscale % (float)this.unit + (float)this.unit) % this.unit;
					if ((this.lastxgrid != this.xgrid && this.whatx != this.diffix) || (this.lastzgrid != this.zgrid && this.whatz != this.diffiz))
					{
						this.UpdateTerrain(this.cutoff, false);
					}
				}
				if (flag)
				{
					if (!this.isDrawing)
					{
						this.tmake.moveall();
					}
					this.trnLoctnLast = this.trnLoctn;
					this.oldtrnLoctn2 = this.trnLoctn;
				}
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00415F60 File Offset: 0x00414160
		public override void Draw(GameTime gameTime)
		{
			this.isDrawing = true;
			if (base.IsActive || MenuScreen.title == "Video Settings")
			{
				if (this.vehicleindex == 3 && Facility.inFacility)
				{
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
					this.sc.GraphicsDevice.BlendState = BlendState.Opaque;
					this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
					this.sc.GraphicsDevice.SetRenderTarget(this.resolveTarget2);
					this.sc.GraphicsDevice.Clear(Color.Transparent);
					this.bigquad.Meshes[0].MeshParts[0].Effect = this.simpEffect;
					Matrix matrix = Matrix.CreateTranslation(Facility.offset);
					this.simpEffect.CurrentTechnique = this.simpEffect.Techniques["straight"];
					this.simpEffect.Parameters["val"].SetValue(this.facility.specRot);
					this.simpEffect.Parameters["campos"].SetValue(this.aCampos);
					this.simpEffect.Parameters["modelTexture"].SetValue(this.distortion);
					this.simpEffect.Parameters["world"].SetValue(matrix);
					this.simpEffect.Parameters["view"].SetValue(this.viewMatrix);
					this.simpEffect.Parameters["projection"].SetValue(this.projectionMatrix);
					this.simpEffect.CurrentTechnique.Passes[0].Apply();
					this.bigquad.Meshes[0].Draw();
					this.facility.DrawBlack(this.viewMatrix, this.projectionMatrix, this.aCampos);
					this.sc.GraphicsDevice.SetRenderTarget(null);
				}
				this.RestoreRenderStates();
				this.sc.GraphicsDevice.Clear(Color.Black);
				if (!Facility.inFacility && base.IsActive && MathHelper.Clamp(-(this.sunDir.Y - 0.2f) / 0.1f, 0f, 1f) > 0f)
				{
					this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
					this.sc.GraphicsDevice.SetRenderTarget(this.shadowTarget);
					this.sc.GraphicsDevice.BlendState = BlendState.Additive;
					this.sc.GraphicsDevice.Clear(Color.Black);
					Matrix matrix2 = Matrix.Identity;
					Vector3 vector = Vector3.One;
					Matrix matrix3 = Matrix.Identity;
					if (this.vehicleindex == 2 || this.vehicleDistance <= 5000f)
					{
						float num = 0.8f;
						if (this.vehicleindex != 2)
						{
							num = 1f - MathHelper.Clamp((this.vehicleDistance - 4000f) / 1000f, 0.2f, 1f);
						}
						this.terrainEffect.Parameters["landerfade"].SetValue(num);
						this.LanderShadow.Meshes[0].MeshParts[0].Effect = this.shadowEffect;
						matrix2 = this.lander.orientation * Matrix.CreateTranslation(new Vector3(0f, 152f, 0f));
						vector = Vector3.Normalize(new Vector3(this.sunDir.X / 3f, -Math.Abs(this.sunDir.Y) - 0.15f, this.sunDir.Z / 3f));
						matrix3 = Matrix.CreateShadow(vector, this.wallPlane) * Matrix.CreateTranslation(new Vector3(0f, 1f, 0f) / 100f);
						this.shadowEffect.Parameters["colorme"].SetValue(new Vector3(1f, 0f, 0f));
						this.shadowEffect.Parameters["world"].SetValue(matrix2 * matrix3);
						this.shadowEffect.Parameters["view"].SetValue(Matrix.CreateLookAt(new Vector3(0f, 3300f, 0f), Vector3.Zero, Vector3.Forward));
						this.shadowEffect.Parameters["projection"].SetValue(Matrix.CreatePerspectiveFieldOfView(0.785f, 1f, 5f, 5000f));
						this.LanderShadow.Meshes[0].Draw();
					}
					if (this.vehicleindex == 1 || (this.vehicleindex == 3 && Vector3.Distance(this.aCampos, this.rover.position) <= 2000f))
					{
						float num2 = 0.8f;
						if (this.vehicleindex == 3)
						{
							float num3 = Vector3.Distance(this.aCampos, this.rover.position);
							num2 = 1f - MathHelper.Clamp((num3 - 1000f) / 1000f, 0.2f, 1f);
						}
						this.terrainEffect.Parameters["roverfade"].SetValue(num2);
						this.tankShadow.Meshes[0].MeshParts[0].Effect = this.shadowEffect;
						matrix2 = Matrix.CreateScale(1.5f) * this.rover.orientation * Matrix.CreateTranslation(new Vector3(0f, 0f, 0f));
						vector = Vector3.Normalize(new Vector3(this.sunDir.X / 3f, -Math.Abs(this.sunDir.Y) - 0.15f, this.sunDir.Z / 3f));
						matrix3 = Matrix.CreateShadow(vector, this.wallPlane) * Matrix.CreateTranslation(new Vector3(0f, 1f, 0f) / 100f);
						this.shadowEffect.Parameters["colorme"].SetValue(new Vector3(0f, 0f, 1f));
						this.shadowEffect.Parameters["view"].SetValue(Matrix.CreateLookAt(new Vector3(0f, 3500f, 0f), Vector3.Zero, Vector3.Forward));
						this.shadowEffect.Parameters["projection"].SetValue(Matrix.CreatePerspectiveFieldOfView(0.33f, 1f, 5f, 5000f));
						this.shadowEffect.Parameters["world"].SetValue(matrix2 * matrix3);
						this.tankShadow.Meshes[0].Draw();
					}
					if (this.dropshipDistance <= 28000f)
					{
						float num4 = 1f - MathHelper.Clamp((this.dropshipDistance - 24000f) / 4000f, 0.2f, 1f);
						this.terrainEffect.Parameters["shipfade"].SetValue(num4);
						this.dropshipShade.Meshes[0].MeshParts[0].Effect = this.shadowEffect;
						matrix2 = Matrix.CreateTranslation(new Vector3(0f, 20f, 0f));
						vector = Vector3.Normalize(new Vector3(this.sunDir.X / 4f, -Math.Abs(this.sunDir.Y) - 0.15f, this.sunDir.Z / 4f));
						matrix3 = Matrix.CreateShadow(vector, this.wallPlane) * Matrix.CreateTranslation(new Vector3(0f, 1f, 0f) / 100f);
						this.shadowEffect.Parameters["colorme"].SetValue(new Vector3(0f, 1f, 0f));
						this.shadowEffect.Parameters["world"].SetValue(matrix2 * matrix3);
						this.shadowEffect.Parameters["view"].SetValue(Matrix.CreateLookAt(new Vector3(0f, 2100f, 0f), Vector3.Zero, Vector3.Forward));
						this.shadowEffect.Parameters["projection"].SetValue(Matrix.CreatePerspectiveFieldOfView(0.785f, 1f, 5f, 5000f));
						this.dropshipShade.Meshes[0].Draw();
					}
					this.sc.GraphicsDevice.SetRenderTarget(null);
					this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
					this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
				}
				this.RestoreRenderStates();
				if (this.vehicleindex == 1 || this.vehicleindex == 3)
				{
					bool flag = false;
					if (this.vehicleindex == 3 && this.faciltyDistance < 20000f)
					{
						this.sc.GraphicsDevice.SetRenderTarget(this.resolveTarget1);
						flag = true;
					}
					this.sc.GraphicsDevice.Clear(Color.Black);
					if (this.faciltyDistance < 20000f)
					{
						if (this.facility.rebuild)
						{
							this.rebuildFacility();
						}
						this.facility.Draw(this.viewMatrix, this.projectionMatrix, this.aCampos, this.sunDir, this.ambient * 0.8f, this.diffuse, new Vector3(0.8f, 0.8f, 0.8f), this.resolveTarget1, this.resolveTarget2, this.spriteBatch, this.faciltyDistance, flag);
					}
					else if (this.faciltyDistance > 21000f)
					{
						this.facility.rebuild = true;
					}
					if (Facility.inFacility)
					{
						this.sc.astronaut.Draw(this.viewMatrix, this.projectionMatrix, new Vector3(0f, 1f, 0f), Vector3.Zero, new Vector3(1f, 1f, 1f), 1f, this.camradian);
					}
					if (!Facility.inFacility)
					{
						if (this.dropshipDistance < 28000f)
						{
							this.DrawDropship(this.dropship, this.viewMatrix, this.projectionMatrix, this.sunDir);
						}
						this.DrawTerrain(1);
						this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
						this.DrawPrints(ref this.prints);
						this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
						this.rover.Draw(this.viewMatrix, this.projectionMatrix, this.sunDir);
						this.sc.astronaut.Draw(this.viewMatrix, this.projectionMatrix, this.sunDir, this.diffuse, this.ambient * 0.9f, 1f, this.camradian);
						this.sc.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
						if (this.vehicleDistance < 25000f)
						{
							this.lander.Draw(this.viewMatrix, this.projectionMatrix, this.sunDir);
						}
						this.shale1.drawShale(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale2.drawShale(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale3.drawShale(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale1.drawShaleInDumper(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale2.drawShaleInDumper(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale3.drawShaleInDumper(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.rock.drawObjects(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.rock.drawSlabs(this.sunDir, this.ambient, this.diffuse * 1.1f, this.viewMatrix, this.projectionMatrix, this.aCampos);
						this.rock.drawBrokenSides(this.sunDir, this.ambient * 0.7f, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.rock.drawRockRubble(this.sunDir, this.ambient * 0.7f, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.DrawEnv(this.darkFog, this.longProjection);
						this.DrawStars(this.stardome, this.longProjection, "straightStars");
						this.DrawBuilding(this.sc.farmBuildingspace, this.farmLocation);
						this.DrawBeacon(gameTime);
						this.skids.SetCamera(this.viewMatrix, this.projectionMatrix);
						this.skids.Draw(0);
						this.overlay.needle = this.myrotter;
						this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
						this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
						this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
						this.rock.drawFlowers(this.sunDir, new Vector3(0.5f, 0.5f, 0.5f), this.diffuse, this.viewMatrix, this.projectionMatrix);
					}
					if (Facility.inFacility)
					{
						this.DrawStars(this.stardome, this.longProjection, "straightStars");
					}
					if (this.vehicleindex == 1)
					{
						this.overlay.Draw(1);
					}
					else
					{
						if (this.faciltyDistance < 20000f)
						{
							this.sc.GraphicsDevice.SetRenderTarget(null);
							this.blurEffect.CurrentTechnique = this.blurEffect.Techniques["spacewalk"];
							this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, null, null, null, this.blurEffect);
							this.spriteBatch.Draw(this.resolveTarget1, new Rectangle(0, 0, this.sc.GraphicsDevice.Viewport.Width, this.sc.GraphicsDevice.Viewport.Height), Color.White);
							this.spriteBatch.End();
						}
						if (this.sc.vehiclelerp == 1f)
						{
							this.overlay.Draw(3);
						}
					}
				}
				else
				{
					this.sc.GraphicsDevice.Clear(Color.Black);
					if (this.faciltyDistance < 20000f)
					{
						if (this.facility.rebuild)
						{
							this.rebuildFacility();
						}
						this.facility.Draw(this.viewMatrix, this.projectionMatrix, this.aCampos, this.sunDir, this.ambient * 0.8f, this.diffuse, new Vector3(0.8f, 0.8f, 0.8f), null, this.resolveTarget2, this.spriteBatch, this.faciltyDistance, false);
					}
					else if (this.faciltyDistance > 21000f)
					{
						this.facility.rebuild = true;
					}
					this.DrawTerrain(2);
					if (this.lander.door1 != 1)
					{
						this.sc.solar1aMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
						this.sc.solar1aBone.Transform = this.sc.solar1aMatrix * this.sc.solar1aTrans;
						this.rover.solar1 = 1;
						this.rover.directme = this.lander.directme;
						if (this.rover.directme < 0f)
						{
							this.rover.directme += 6.28f;
						}
						this.rover.directme = this.lander.directme - MathHelper.ToRadians(-135f);
						if (this.rover.directme < 0f)
						{
							this.rover.directme += 6.28f;
						}
						this.rover.position.X = this.lander.position.X;
						this.rover.position.Y = this.lander.position.Y;
						this.rover.position.Z = this.lander.position.Z;
						this.rover.facingDirection = this.rover.directme;
						this.rover.orientation = Matrix.CreateRotationY(this.rover.directme);
						this.rover.orientation.Up = this.lander.orientation.Up;
						this.rover.orientation.Right = Vector3.Cross(this.rover.orientation.Forward, this.rover.orientation.Up);
						this.rover.orientation.Right = Vector3.Normalize(this.rover.orientation.Right);
						this.rover.orientation.Forward = Vector3.Cross(this.rover.orientation.Up, this.rover.orientation.Right);
						this.rover.orientation.Forward = Vector3.Normalize(this.rover.orientation.Forward);
						this.rover.scooperON = false;
						this.rover.scoopx = 1f;
						this.rover.scoop1 = 1;
						this.rover.scooperON = false;
						this.sc.RoverEquip();
						this.rover.Draw(this.viewMatrix, this.projectionMatrix, this.sunDir);
						this.shale1.drawShaleInDumper(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale2.drawShaleInDumper(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
						this.shale3.drawShaleInDumper(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
					}
					this.lander.Draw(this.viewMatrix, this.projectionMatrix, this.sunDir);
					if (this.dropshipDistance < 28000f)
					{
						this.DrawDropship(this.dropship, this.viewMatrix, this.projectionMatrix, this.sunDir);
					}
					this.sc.astronaut.Draw(this.viewMatrix, this.projectionMatrix, this.sunDir, this.diffuse, this.ambient * 0.9f, 1f, this.camradian);
					this.sc.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
					this.shale1.drawShale(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
					this.shale2.drawShale(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
					this.rock.drawObjects(this.sunDir, this.ambient, this.diffuse, this.viewMatrix, this.projectionMatrix);
					this.rock.drawSlabs(this.sunDir, this.ambient, this.diffuse * 1.1f, this.viewMatrix, this.projectionMatrix, this.aCampos);
					this.DrawEnv(this.darkFog, this.longProjection);
					this.DrawStars(this.stardome, this.longProjection, "straightStars");
					this.DrawBuilding(this.sc.farmBuildingspace, this.farmLocation);
					this.DrawBeacon(gameTime);
					this.streaks.SetCamera(this.viewMatrix, this.projectionMatrix);
					this.streaks.Draw(0);
					this.dust.SetCamera(this.viewMatrix, this.projectionMatrix);
					this.dust.Draw(0);
					if (this.lander.shieldhit > 0f)
					{
						this.drawSphere();
					}
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
					this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
					this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
					this.rock.drawFlowers(this.sunDir, new Vector3(0.5f, 0.5f, 0.5f), this.diffuse, this.viewMatrix, this.projectionMatrix);
					this.overlay.needle = this.rot;
					this.overlay.Draw(2);
				}
				this.overlay.View = this.viewMatrix;
				this.overlay.Projection = this.projectionMatrix;
				this.overlay.LightDirection = this.sunDir;
			}
			this.DrawOptions();
			if (!base.IsActive)
			{
				this.sc.FadeBackBufferToBlack(160);
			}
			this.isDrawing = false;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x004175D0 File Offset: 0x004157D0
		private void DrawOptions()
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.sc.ScaleMatrix1);
			bool flag = true;
			if (this.editcam)
			{
				flag = false;
				if (this.vehicleindex == 1)
				{
					this.spriteBatch.Draw(this.sc.camedit, this.menuposition, new Rectangle?(this.rovercamedit), Color.White);
					if (this.sc.roverrotlock == 1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[0], new Rectangle?(this.buttonhalf), Color.White);
					}
					if (this.sc.roverrotlock == 2)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[0], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.sc.roverhitelock == 1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[1], new Rectangle?(this.buttonhalf), Color.White);
					}
					if (this.sc.roverhitelock == 2)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[1], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.sc.space_rinvertX == -1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[2], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.sc.space_rinvertY == -1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[3], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.camadjust)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.adj + new Vector2(-9f, -9f), new Rectangle?(this.buttonchoice), Color.White);
					}
				}
				if (this.vehicleindex == 2)
				{
					this.spriteBatch.Draw(this.sc.camedit, this.menuposition, new Rectangle?(this.landercamedit), Color.White);
					if (this.sc.landerrotlock == 1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[0], new Rectangle?(this.buttonhalf), Color.White);
					}
					if (this.sc.landerrotlock == 2)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[0], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.sc.landerhitelock == 1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[1], new Rectangle?(this.buttonhalf), Color.White);
					}
					if (this.sc.landerhitelock == 2)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[1], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.sc.space_invertX == -1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[2], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.sc.space_invertY == -1)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.menuposition + this.pointbox1[3], new Rectangle?(this.buttonfill), Color.White);
					}
					if (this.camadjust)
					{
						this.spriteBatch.Draw(this.sc.camedit, this.adj + new Vector2(-9f, -9f), new Rectangle?(this.buttonchoice), Color.White);
					}
				}
			}
			if (this.refineShale > 0 || this.refineRuby > 0 || this.refineBlue > 0)
			{
				flag = false;
				float y = this.typer.MeasureString("REFINE").Y;
				this.spriteBatch.DrawString(this.typer, "* REFINING *", new Vector2(90f, 180f), new Color(255, 255, 255, 255));
				this.spriteBatch.DrawString(this.typer, "____________", new Vector2(90f, 185f), new Color(255, 255, 255, 255));
				this.spriteBatch.DrawString(this.typer, "SHALE----> " + this.refineShale.ToString(), new Vector2(90f, 190f + y), new Color(255, 255, 255, 255));
				if (this.refineShale <= 0)
				{
					this.spriteBatch.DrawString(this.typer, "RUBIES---> " + this.refineRuby.ToString(), new Vector2(90f, 190f + y + y), new Color(255, 255, 255, 255));
				}
				if (this.refineShale <= 0 && this.refineRuby <= 0)
				{
					this.spriteBatch.DrawString(this.typer, "ORNITE---> " + this.refineBlue.ToString(), new Vector2(90f, 190f + y + y + y), new Color(255, 255, 255, 255));
				}
			}
			if (flag)
			{
				this.timer += 0.0166f;
				this.typewritercount -= 1f;
				if (this.typewriterblank == 500f)
				{
					if (this.textflag == 0)
					{
						this.text = this.objectiveData[this.objIndex];
						this.textflag = 1;
					}
					if (this.typewritercount <= 0f)
					{
						this.typeposition += 1f;
					}
					if (this.typeposition < (float)this.text.Length && this.typewritercount <= 0f)
					{
						float num = (float)this.random.Next(-70, 90) / 100f;
						this.pop1.Play(0.8f * this.sc.mv, num, 0f);
						this.typewritercount = (float)this.typewriterdelay;
					}
					if (this.typeposition > (float)this.text.Length)
					{
						this.typeposition = (float)this.text.Length;
						this.typewriterwait -= 1f;
					}
					float num2 = (float)(Math.Sin((double)(this.timer * 30f)) * 15.0) + 240f;
					if (this.typewriterwait < 240f)
					{
						num2 = this.typewriterwait + (float)(Math.Sin((double)(this.timer * 30f)) * 15.0);
					}
					num2 /= 245f;
					string text = "";
					if (this.typeposition < (float)this.text.Length)
					{
						text = "|";
					}
					this.spriteBatch.DrawString(this.typer, this.text.Substring(0, (int)this.typeposition) + text, new Vector2(90f, this.typevertical), new Color(255, 255, 255, 255) * num2);
					if ((int)this.typeposition == this.text.Length)
					{
						this.typevertical -= 0.01f + (float)this.random.Next(1, 200) / 1000f;
					}
					if (this.typewriterwait <= 0f)
					{
						this.textflag = 0;
						this.typewriterblank = 0f;
						this.typewriterwait = 480f;
						this.typeposition = 0f;
						this.typevertical = (float)this.random.Next(100, 150);
						this.typewriterdelay = this.random.Next(2, 7);
					}
				}
			}
			if (this.memoTimer > 0)
			{
				float num3 = MathHelper.Clamp((float)this.memoTimer / 30f, 0f, 1f);
				Vector2 vector = new Vector2((float)Math.Sin((double)(this.sc.myTimer / 15f)) * 26f, (float)Math.Cos((double)(this.sc.myTimer / 15f)) * 23f);
				vector += new Vector2(135f, 390f);
				if (this.memoIcon > 0)
				{
					if (this.memoIcon == 1)
					{
						vector.X += 10f;
						this.spriteBatch.Draw(this.sc.overlay, vector + new Vector2(-50f, -22f), new Rectangle?(this.rect_choice[this.memoIcon]), Color.White);
					}
					else if (this.memoIcon == 6)
					{
						vector.X += 10f;
						this.spriteBatch.Draw(this.sc.overlay, vector + new Vector2(-50f, 10f), new Rectangle?(this.rect_choice[this.memoIcon]), Color.White);
					}
					else if (this.memoIcon == 7)
					{
						vector.X += 420f;
						vector.Y += 170f;
					}
					else
					{
						this.spriteBatch.Draw(this.sc.overlay, vector + new Vector2(-45f, 5f), new Rectangle?(this.rect_choice[this.memoIcon]), Color.White);
					}
				}
				this.spriteBatch.DrawString(this.ammoMedium2, GameplayScreen.memo, new Vector2(-4f, 4f) + vector, Color.Black * num3);
				this.spriteBatch.DrawString(this.ammoMedium2, GameplayScreen.memo, vector, Color.White * num3);
				this.memoTimer--;
			}
			float num4 = 570f;
			Vector2 vector2 = new Vector2((float)Math.Sin((double)(this.sc.myTimer / 20f)) * 10f, (float)Math.Cos((double)(this.sc.myTimer / 20f)) * 10f) + new Vector2(660f, num4) - this.ammoMedium[this.fontindex].MeasureString(GameplayScreen.atDoorBuild) / 2f;
			if (this.nearAstro && flag && this.sc.astronaut.chosen != -1 && this.sc.astronaut.man.dupe[this.sc.astronaut.chosen].emo != astroDupe.emotion.trucking && this.sc.astronaut.man.dupe[this.sc.astronaut.chosen].emo != astroDupe.emotion.scaredintruck)
			{
				if (this.sc.astronaut.seats.Count > 0)
				{
					if (this.sc.usingMouse)
					{
						Vector2 vector3 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
						Rectangle rectangle = new Rectangle((int)vector3.X - 8, (int)vector3.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
						this.spriteBatch.Draw(this.sc.overlay, rectangle, new Rectangle?(this.rect_Blue), Color.White);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector3, Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Xbutton), Color.White);
					}
				}
				else
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 8f), new Rectangle?(this.rect_Lock), Color.White);
				}
				if (this.sc.astronaut.man.dupe[this.sc.astronaut.chosen].emo == astroDupe.emotion.stranded)
				{
					if (this.sc.astronaut.seats.Count > 0)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to rescue astronaut", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to rescue astronaut", vector2, Color.White);
					}
					else
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " rover full can't rescue", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " rover full can't rescue", vector2, Color.White);
					}
				}
				else if (this.sc.astronaut.man.dupe[this.sc.astronaut.chosen].emo == astroDupe.emotion.safe)
				{
					if (this.sc.astronaut.seats.Count > 0)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to recruit !", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to recruit !", vector2, Color.White);
					}
					else
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " rover full can't recruit", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " rover full can't recruit", vector2, Color.White);
					}
				}
				else if (this.sc.astronaut.man.dupe[this.sc.astronaut.chosen].emo == astroDupe.emotion.underground)
				{
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " this astronaut is upset", vector2 + new Vector2(-2f, 2f), Color.Black);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " this astronaut is upset", vector2, Color.White);
				}
				flag = false;
			}
			if (this.nearFlower && flag && this.rock.chosen != -1)
			{
				if (this.rock.flowerInst[this.rock.chosen].stateFlag == 15)
				{
					if (this.sc.usingMouse)
					{
						Vector2 vector4 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
						Rectangle rectangle2 = new Rectangle((int)vector4.X - 8, (int)vector4.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
						this.spriteBatch.Draw(this.sc.overlay, rectangle2, new Rectangle?(this.rect_Blue), Color.White);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector4, Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Xbutton), Color.White);
					}
					if (objDupe.flowersaved == 0)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " what is this thing?", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " what is this thing?", vector2, Color.White);
					}
					if (objDupe.flowersaved == 1)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to love this flower", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to love this flower", vector2, Color.White);
					}
					if (objDupe.flowersaved >= 2)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " wait, do you want this?", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " wait, do you want this?", vector2, Color.White);
					}
				}
				if (this.rock.flowerInst[this.rock.chosen].stateFlag == 10)
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Heart), Color.White);
					if (objDupe.flowersaved <= 1)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "   Love is Love " + objDupe.flowersaved.ToString() + " of 3", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "   Love is Love " + objDupe.flowersaved.ToString() + " of 3", vector2, Color.White);
					}
					if (objDupe.flowersaved == 2)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "   You Have Two Loves " + objDupe.flowersaved.ToString() + " of 3", vector2 + new Vector2(-2f, 2f), Color.Black);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "   You Have Two Loves " + objDupe.flowersaved.ToString() + " of 3", vector2, Color.White);
					}
					if (objDupe.flowersaved > 2)
					{
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "   The Event Has Begun", vector2 + new Vector2(-2f, 2f), Color.DarkRed);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "   The Event Has Begun", vector2, Color.White);
					}
				}
				flag = false;
			}
			if (this.humanonramp > 0 && flag)
			{
				if (this.humanonramp < 3 || this.lander.radioFixed)
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 5f), new Rectangle?(this.rect_Exclaim2), Color.White);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "  drive rover up ramp", vector2 + new Vector2(-2f, 2f), Color.Black);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], "  drive rover up ramp", vector2, Color.White);
				}
				else
				{
					if (this.sc.usingMouse)
					{
						Vector2 vector5 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
						Rectangle rectangle3 = new Rectangle((int)vector5.X - 8, (int)vector5.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
						this.spriteBatch.Draw(this.sc.overlay, rectangle3, new Rectangle?(this.rect_Blue), Color.White);
						this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector5, Color.White);
					}
					else
					{
						this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Xbutton), Color.White);
					}
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to repair radio", vector2 + new Vector2(-2f, 2f), Color.Black);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to repair radio", vector2, Color.White);
				}
				flag = false;
			}
			if (this.atRover && flag)
			{
				if (this.sc.usingMouse)
				{
					Vector2 vector6 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
					Rectangle rectangle4 = new Rectangle((int)vector6.X - 8, (int)vector6.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
					this.spriteBatch.Draw(this.sc.overlay, rectangle4, new Rectangle?(this.rect_Blue), Color.White);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector6, Color.White);
				}
				else
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Abutton), Color.White);
				}
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to enter rover", vector2 + new Vector2(-2f, 2f), Color.Black);
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to enter rover", vector2, Color.White);
				flag = false;
			}
			if (Facility.atSwitch && flag)
			{
				if (this.sc.usingMouse)
				{
					Vector2 vector7 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
					Rectangle rectangle5 = new Rectangle((int)vector7.X - 8, (int)vector7.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
					this.spriteBatch.Draw(this.sc.overlay, rectangle5, new Rectangle?(this.rect_Blue), Color.White);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector7, Color.White);
				}
				else
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Abutton), Color.White);
				}
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press for c.r.a.n.e", vector2 + new Vector2(-2f, 2f), Color.Black);
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press for c.r.a.n.e", vector2, Color.White);
				flag = false;
			}
			if (Facility.atSwitch2 && flag)
			{
				if (this.sc.usingMouse)
				{
					Vector2 vector8 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
					Rectangle rectangle6 = new Rectangle((int)vector8.X - 8, (int)vector8.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
					this.spriteBatch.Draw(this.sc.overlay, rectangle6, new Rectangle?(this.rect_Blue), Color.White);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector8, Color.White);
				}
				else
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Abutton), Color.White);
				}
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to create worker", vector2 + new Vector2(-2f, 2f), Color.Black);
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to create worker", vector2, Color.White);
				flag = false;
			}
			if (Facility.atMain && flag)
			{
				if (this.sc.usingMouse)
				{
					Vector2 vector9 = new Vector2(vector2.X - this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, vector2.Y);
					Rectangle rectangle7 = new Rectangle((int)vector9.X - 8, (int)vector9.Y + 2, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.myXkey + "  ").X, (int)this.ammoMedium[this.fontindex].MeasureString(this.sc.x_key.ToString()).Y);
					this.spriteBatch.Draw(this.sc.overlay, rectangle7, new Rectangle?(this.rect_Blue), Color.White);
					this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], this.sc.myXkey, vector9, Color.White);
				}
				else
				{
					this.spriteBatch.Draw(this.sc.overlay, vector2 + new Vector2(-30f, 7f), new Rectangle?(this.rect_Abutton), Color.White);
				}
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to open", vector2 + new Vector2(-2f, 2f), Color.Black);
				this.spriteBatch.DrawString(this.ammoMedium[this.fontindex], " press to open", vector2, Color.White);
			}
			this.spriteBatch.End();
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0041953C File Offset: 0x0041773C
		private void DrawBuilding(Model model, Vector3 pos)
		{
			float num = Vector3.Distance(this.aCampos, this.farmLocation);
			if (num > 29000f)
			{
				return;
			}
			Matrix matrix = Matrix.CreateTranslation(pos);
			Vector3 zero = Vector3.Zero;
			Matrix identity = Matrix.Identity;
			model.Meshes[0].MeshParts[0].Effect = this.buildingEffect;
			this.sc.dayTime = "pm";
			if (this.sc.dayTime == "pm")
			{
				this.buildingEffect.Parameters["darkness"].SetValue(this.ambient);
				this.buildingEffect.Parameters["mydiffuse"].SetValue(this.diffuse);
				this.buildingEffect.Parameters["proj"].SetValue(this.projectionMatrix);
			}
			this.buildingEffect.Parameters["moon"].SetValue(this.sunDir);
			this.buildingEffect.Parameters["depth"].SetValue(5000);
			this.buildingEffect.Parameters["pulse"].SetValue(1);
			this.buildingEffect.Parameters["gDiffuse"].SetValue(this.sc.crosshair);
			this.buildingEffect.Parameters["lightPos1"].SetValue(this.aCampos);
			this.buildingEffect.Parameters["world"].SetValue(matrix);
			this.buildingEffect.Parameters["projectorView"].SetValue(matrix * identity);
			this.buildingEffect.Parameters["WorldViewProj"].SetValue(matrix * this.viewMatrix * this.projectionMatrix);
			this.buildingEffect.CurrentTechnique = this.buildingEffect.Techniques[1];
			model.Meshes[0].Draw();
			model.Meshes[1].MeshParts[0].Effect = this.buildingEffect;
			Matrix matrix2 = Matrix.CreateTranslation(688.413f, 86.649f, 1619.653f + zero.Z);
			this.buildingEffect.Parameters["world"].SetValue(matrix * matrix2);
			this.buildingEffect.Parameters["projectorView"].SetValue(matrix * matrix2 * identity);
			this.buildingEffect.Parameters["WorldViewProj"].SetValue(matrix * matrix2 * this.viewMatrix * this.projectionMatrix);
			if (this.sc.dayTime == "pm")
			{
				this.buildingEffect.CurrentTechnique = this.buildingEffect.Techniques[0];
			}
			model.Meshes[1].Draw();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00419860 File Offset: 0x00417A60
		public void DrawStars(Model model, Matrix proj, string tech)
		{
			if (this.overlay.scopeMode)
			{
				return;
			}
			float num = MathHelper.Clamp((this.sunDir.Y + 0f) / 0.4f, 0f, 1f);
			Vector3 vector = new Vector3(1f, 1f, 1f) * (1f - num) + new Vector3(0.9f, 1.1f, 1.5f) * num;
			model.Meshes[0].MeshParts[0].Effect = this.starEffect;
			Matrix matrix = Matrix.CreateRotationX(1.335f) * Matrix.CreateRotationZ(-1.735f) * this.starRot * Matrix.CreateScale(2f) * Matrix.CreateTranslation(this.aCampos);
			this.starEffect.Parameters["bright"].SetValue(vector);
			this.starEffect.Parameters["world"].SetValue(matrix);
			this.starEffect.Parameters["view"].SetValue(this.viewMatrix);
			this.starEffect.Parameters["projection"].SetValue(proj);
			this.starEffect.CurrentTechnique = this.starEffect.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x004199E8 File Offset: 0x00417BE8
		private void DrawEnv(Model model, Matrix proj)
		{
			if (this.overlay.scopeMode)
			{
				return;
			}
			foreach (Effect effect in model.Meshes[0].Effects)
			{
				BasicEffect basicEffect = (BasicEffect)effect;
				basicEffect.Alpha = 1f;
				basicEffect.LightingEnabled = false;
				basicEffect.View = this.viewMatrix;
				basicEffect.Projection = proj;
				basicEffect.PreferPerPixelLighting = false;
				basicEffect.World = Matrix.CreateScale(1.5f) * Matrix.CreateTranslation(this.aCampos.X, this.aCampos.Y + 1000f, this.aCampos.Z);
			}
			model.Meshes[0].Draw();
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00419AD4 File Offset: 0x00417CD4
		private void DrawBeacon(GameTime gm)
		{
			Vector3 vector = new Vector3(0.5f, 0.5f, 0.5f);
			Vector3 vector2 = new Vector3(1f, 1f, 1f);
			this.chosenBouy[0] = this.overlay.bouy1;
			this.chosenBouy[1] = this.overlay.bouy2;
			this.chosenBouy[2] = this.overlay.bouy3;
			for (int i = 0; i < 3; i++)
			{
				int num = this.pulseBouy[i];
				float num2 = Vector3.Distance(this.aCampos, this.chosenBouy[i]) / 3300f;
				Vector3 vector3 = this.colorBouy[i];
				Matrix matrix = Matrix.CreateScale(1.2f) * Matrix.CreateTranslation(this.chosenBouy[i]);
				if (this.chosenBouy[i] != Vector3.Zero)
				{
					foreach (Effect effect in this.beacon.Meshes[1].Effects)
					{
						BasicEffect basicEffect = (BasicEffect)effect;
						basicEffect.LightingEnabled = true;
						basicEffect.AmbientLightColor = vector;
						basicEffect.DiffuseColor = vector2;
						basicEffect.View = this.viewMatrix;
						basicEffect.Projection = this.projectionMatrix;
						basicEffect.World = matrix;
					}
					this.beacon.Meshes[1].Draw();
					foreach (Effect effect2 in this.beacon.Meshes[2].Effects)
					{
						BasicEffect basicEffect2 = (BasicEffect)effect2;
						Vector3 vector4 = vector3 / (1.2f + (float)Math.Sin(gm.TotalGameTime.TotalMilliseconds / (double)num) * 0.5f);
						basicEffect2.LightingEnabled = true;
						basicEffect2.AmbientLightColor = vector4;
						basicEffect2.DiffuseColor = vector4;
						basicEffect2.View = this.viewMatrix;
						basicEffect2.Projection = this.projectionMatrix;
						basicEffect2.World = matrix;
					}
					this.beacon.Meshes[2].Draw();
				}
			}
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			this.sc.GraphicsDevice.BlendState = BlendState.Additive;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
			for (int j = 0; j < 3; j++)
			{
				int num3 = this.pulseBouy[j];
				Vector3 vector5 = new Vector3(this.chosenBouy[j].X, this.chosenBouy[j].Y + 57f, this.chosenBouy[j].Z);
				float num4 = Vector3.Distance(this.aCampos, this.chosenBouy[j]) / 3300f;
				Vector3 vector6 = this.colorBouy[j];
				if (this.chosenBouy[j] != Vector3.Zero)
				{
					foreach (Effect effect3 in this.beacon.Meshes[0].Effects)
					{
						BasicEffect basicEffect3 = (BasicEffect)effect3;
						Vector3 vector7 = vector6 / (5.7f + (float)Math.Sin(gm.TotalGameTime.TotalMilliseconds / (double)num3) * 5f);
						vector = vector7;
						vector2 = vector7;
						Matrix matrix2 = Matrix.CreateBillboard(vector5, this.aCampos, this.projectionMatrix.Up, new Vector3?(this.projectionMatrix.Forward));
						Matrix matrix3 = Matrix.CreateScale(0.6f + MathHelper.Clamp(num4, 0f, 4f)) * matrix2;
						basicEffect3.LightingEnabled = true;
						basicEffect3.AmbientLightColor = vector;
						basicEffect3.DiffuseColor = vector2;
						basicEffect3.View = this.viewMatrix;
						basicEffect3.Projection = this.projectionMatrix;
						basicEffect3.World = matrix3;
					}
					this.beacon.Meshes[0].Draw();
				}
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00419FA0 File Offset: 0x004181A0
		private void DrawGalaxy(Matrix proj)
		{
			foreach (Effect effect in this.galaxy.Meshes[0].Effects)
			{
				BasicEffect basicEffect = (BasicEffect)effect;
				Matrix matrix = Matrix.CreateFromYawPitchRoll(-1.77f, -0.2f, 0f) * this.starRot * Matrix.CreateScale(4.8f) * Matrix.CreateTranslation(this.aCampos);
				basicEffect.LightingEnabled = true;
				basicEffect.AmbientLightColor = new Vector3(0.7f, 0.7f, 0.7f);
				basicEffect.DiffuseColor = new Vector3(0.7f, 0.7f, 0.7f);
				basicEffect.View = this.viewMatrix;
				basicEffect.Projection = proj;
				basicEffect.World = matrix;
			}
			this.galaxy.Meshes[0].Draw();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0041A0B4 File Offset: 0x004182B4
		private void DrawTerrain(int ch)
		{
			float num = MathHelper.Clamp(-(this.sunDir.Y - 0.1f) / 0.1f, 0f, 0.99f);
			float num2 = MathHelper.Clamp(-(this.sunDir.Y - 0.2f) / 0.1f, 0f, 0.99f);
			float num3 = MathHelper.Clamp((this.sunDir.Y - 0.25f) / 0.1f, 0f, 0.99f);
			this.ambient = this.ambDay * num + this.ambSol * (1f - num) * num2 + this.ambSet * (1f - num2) * (1f - num3) + this.ambNite * num3;
			this.diffuse = this.difDay * num + this.difSol * (1f - num) * num2 + this.difSet * (1f - num2) * (1f - num3) + this.difNite * num3;
			this.lander.amb = this.ambient;
			this.lander.diffu = this.diffuse;
			this.rover.amb = this.ambient;
			this.rover.diffu = this.diffuse;
			Effect effect = this.terrainEffect;
			if (this.vehicleindex == 3)
			{
				effect.Parameters["far"].SetValue(1f);
				effect.Parameters["near"].SetValue(5f);
			}
			else
			{
				effect.Parameters["far"].SetValue(1f);
				effect.Parameters["near"].SetValue(5f);
			}
			if (ch == 2)
			{
				if (this.lander.lscale > 0f)
				{
					effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadowsLanderDropship"];
					if (this.dropshipDistance > 28000f)
					{
						effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadowsLander"];
					}
				}
				else
				{
					effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadowsLanderDropshipnoflame"];
					if (this.dropshipDistance > 28000f)
					{
						effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadowsLandernoflame"];
					}
				}
				effect.Parameters["center"].SetValue(this.lander.position);
				if (this.lander.lander2ground > 2500f)
				{
					effect.Parameters["xLanderFlame"].SetValue(0);
				}
				else if (this.overlay.fuelAMT <= 4)
				{
					effect.Parameters["xLanderFlame"].SetValue(0.2f);
				}
				else
				{
					effect.Parameters["xLanderFlame"].SetValue(this.lander.lscale);
				}
				effect.Parameters["xLander2ground"].SetValue(this.lander.lander2ground);
				effect.Parameters["hitefade"].SetValue(1f - MathHelper.Clamp((this.lander.lander2ground - 700f) / 2000f, 0f, 1f));
				effect.Parameters["xGround"].SetValue(this.lander.ground);
			}
			else
			{
				effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadows3"];
				if (this.dropshipDistance > 28000f)
				{
					if (this.vehicleDistance > 5000f)
					{
						effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadowsRover"];
					}
					else
					{
						effect.CurrentTechnique = this.terrainEffect.Techniques["terrainShadowsRoverLander"];
					}
				}
				effect.Parameters["center"].SetValue(this.rover.position);
				effect.Parameters["xLanderFlame"].SetValue(0);
			}
			int num4 = this.unit * this.gridscale;
			float num5 = 36.125f * (float)this.gridscale;
			float num6 = (float)(this.lander.altitude - 155);
			Vector2 vector = new Vector2(this.lander.position.X - 765f + num6 * this.sunDir.X / 2f, this.lander.position.Z - 754f + num6 * this.sunDir.Z / 2f);
			effect.Parameters["shadoffset1"].SetValue(new Vector2(vector.X + 765f, vector.Y + 754f));
			vector.X = (float)(((double)(vector.X % 43350f) + 1.5 * (double)num4) % (double)num4);
			vector.Y = (float)(((double)(vector.Y % 43350f) + 1.5 * (double)num4) % (double)num4);
			effect.Parameters["landerCoord"].SetValue(new Vector2(vector.X / num5, vector.Y / num5));
			vector = new Vector2(this.rover.position.X + 62f + (this.rover.position.Y - this.rover.gndPosition.Y) * this.sunDir.X / 2f, this.rover.position.Z + 55f + (this.rover.position.Y - this.rover.gndPosition.Y) * this.sunDir.Z / 2f);
			effect.Parameters["shadoffset2"].SetValue(new Vector2(vector.X + 62f, vector.Y + 62f));
			vector.X = (float)(((double)(vector.X % 43350f) + 1.5 * (double)num4) % (double)num4);
			vector.Y = (float)(((double)(vector.Y % 43350f) + 1.5 * (double)num4) % (double)num4);
			effect.Parameters["roverCoord"].SetValue(new Vector2(vector.X / num5, vector.Y / num5));
			vector = new Vector2(this.dropshipPOS.X - 5670f, this.dropshipPOS.Z - 5140f);
			vector.X = (float)(((double)(vector.X % 43350f) + 1.5 * (double)num4) % (double)num4);
			vector.Y = (float)(((double)(vector.Y % 43350f) + 1.5 * (double)num4) % (double)num4);
			effect.Parameters["dropCoord"].SetValue(new Vector2(vector.X / num5, vector.Y / num5));
			effect.Parameters["dropship"].SetValue(new Vector2(this.dropshipPOS.X, this.dropshipPOS.Z));
			effect.Parameters["fade"].SetValue(num2);
			effect.Parameters["planet"].SetValue(this.tinting[this.sc.planet]);
			effect.Parameters["xAmbient"].SetValue(this.ambient * 0.4f);
			effect.Parameters["xDiffuse"].SetValue(this.diffuse * 1f);
			effect.Parameters["xView"].SetValue(this.viewMatrix);
			effect.Parameters["xProjection"].SetValue(this.projectionMatrix);
			effect.Parameters["xCamPos"].SetValue(this.aCampos);
			effect.Parameters["xLightDirection"].SetValue(this.sunDir);
			effect.Parameters["xTextureX"].SetValue(this.shadowTarget);
			effect.CurrentTechnique.Passes[0].Apply();
			this.sc.GraphicsDevice.SetVertexBuffer(this.tmake.terrainVertexBuffer);
			this.sc.GraphicsDevice.Indices = this.tmake.terrainIndexBuffer;
			int vertexCount = this.tmake.terrainVertexBuffer.VertexCount;
			int indexCount = this.tmake.terrainIndexBuffer.IndexCount;
			this.sc.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount, 0, indexCount / 3);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0041AA20 File Offset: 0x00418C20
		private void drawSphere()
		{
			this.sphere.Meshes[0].MeshParts[0].Effect = this.simpEffect;
			Matrix matrix = Matrix.CreateTranslation(new Vector3(Facility.offset.X + 2200f, Facility.offset.Y + 360f, Facility.offset.Z + 4050f));
			this.simpEffect.CurrentTechnique = this.simpEffect.Techniques["shield"];
			this.simpEffect.Parameters["val"].SetValue(this.lander.shieldhit / 60f);
			this.simpEffect.Parameters["campos"].SetValue(this.aCampos);
			this.simpEffect.Parameters["modelTexture"].SetValue(this.shieldTexture);
			this.simpEffect.Parameters["world"].SetValue(matrix);
			this.simpEffect.Parameters["view"].SetValue(this.viewMatrix);
			this.simpEffect.Parameters["projection"].SetValue(this.projectionMatrix);
			this.simpEffect.CurrentTechnique.Passes[0].Apply();
			this.sphere.Meshes[0].Draw();
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0041ABA8 File Offset: 0x00418DA8
		public void DrawDropship(Model model, Matrix viewMatrix, Matrix projectionMatrix, Vector3 sundir)
		{
			float num = 1f - MathHelper.Clamp((this.dropshipDistance - 24000f) / 4000f, 0f, 1f);
			if (num <= 0f)
			{
				return;
			}
			Matrix matrix = Matrix.CreateScale(5f) * Matrix.CreateTranslation(this.dropshipPOS);
			model.Meshes[0].MeshParts[0].Effect = this.mybasic;
			this.mybasic.Parameters["inc"].SetValue((float)(this.myframe / 20 % 4) * 0.25f);
			this.mybasic.Parameters["diffuse"].SetValue(1.2f * this.diffuse * num);
			this.mybasic.Parameters["amb"].SetValue(0.5f * this.ambient * num);
			this.mybasic.Parameters["LightDirection"].SetValue(sundir);
			this.mybasic.Parameters["world"].SetValue(matrix);
			this.mybasic.Parameters["view"].SetValue(viewMatrix);
			this.mybasic.Parameters["projection"].SetValue(projectionMatrix);
			model.Meshes[0].Draw();
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0041AD30 File Offset: 0x00418F30
		private void TireTreads()
		{
			if (this.lastpos == new Vector3(0f, 0f, 0f))
			{
				this.lastpos = this.rover.position;
			}
			int num = (int)Vector3.Distance(this.rover.position, this.lastpos);
			if (num > 2)
			{
				Vector3 vector = Vector3.Normalize(this.rover.position - this.lastpos);
				float num2;
				this.GetHeightOnly(ref this.tmake.heightData, this.rover.position, out num2);
				Vector3 vector2 = this.rover.position + Vector3.Transform(new Vector3(37f, 0f, 61f), this.rover.orientation);
				Vector3 vector3 = this.rover.position + Vector3.Transform(new Vector3(-37f, 0f, 61f), this.rover.orientation);
				if (num2 >= this.rover.position.Y - 20f)
				{
					for (int i = 0; i < num; i += 4)
					{
						Vector3 vector4 = new Vector3(vector2.X + (float)i * vector.X, 0f, vector2.Z + (float)i * vector.Z);
						this.GetHeightOnly(ref this.tmake.heightData, vector4, out num2);
						vector4.Y = num2 + 4f;
						if (this.rover.leanAmt >= -0.2f)
						{
							this.skids.AddParticle(vector4, new Vector3(0f, 0f, 0f));
						}
						vector4 = new Vector3(vector3.X + (float)i * vector.X, 0f, vector3.Z + (float)i * vector.Z);
						this.GetHeightOnly(ref this.tmake.heightData, vector4, out num2);
						vector4.Y = num2 + 4f;
						if (this.rover.leanAmt <= 0.2f)
						{
							this.skids.AddParticle(vector4, new Vector3(0f, 0f, 0f));
						}
					}
				}
				this.lastpos = this.rover.position;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0041AF88 File Offset: 0x00419188
		private void landerDrips()
		{
			this.leg1 = this.lander.position + Vector3.Transform(new Vector3(-13f, -152f, 151f), this.lander.orientation);
			this.leg2 = this.lander.position + Vector3.Transform(new Vector3(-151f, -152f, 11f), this.lander.orientation);
			this.leg3 = this.lander.position + Vector3.Transform(new Vector3(13f, -152f, -151f), this.lander.orientation);
			this.leg4 = this.lander.position + Vector3.Transform(new Vector3(151f, -152f, 11f), this.lander.orientation);
			if (this.lastleg1 == new Vector3(0f, 0f, 0f))
			{
				this.lastleg1 = this.leg1;
			}
			if (this.lastleg2 == new Vector3(0f, 0f, 0f))
			{
				this.lastleg2 = this.leg2;
			}
			if (this.lastleg3 == new Vector3(0f, 0f, 0f))
			{
				this.lastleg3 = this.leg3;
			}
			if (this.lastleg4 == new Vector3(0f, 0f, 0f))
			{
				this.lastleg4 = this.leg4;
			}
			int num = (int)Vector3.Distance(this.leg1, this.lastleg1);
			if (num > 2)
			{
				Vector3 vector = Vector3.Normalize(this.leg1 - this.lastleg1);
				for (int i = 0; i < num; i += 4)
				{
					Vector3 vector2 = new Vector3(this.leg1.X + (float)i * vector.X, this.leg1.Y + (float)i * vector.Y, this.leg1.Z + (float)i * vector.Z);
					this.streaks.AddParticle(vector2, new Vector3(0f, 0f, 0f));
				}
			}
			num = (int)Vector3.Distance(this.leg2, this.lastleg2);
			if (num > 2)
			{
				Vector3 vector3 = Vector3.Normalize(this.leg2 - this.lastleg2);
				for (int j = 0; j < num; j += 4)
				{
					Vector3 vector4 = new Vector3(this.leg2.X + (float)j * vector3.X, this.leg2.Y + (float)j * vector3.Y, this.leg2.Z + (float)j * vector3.Z);
					this.streaks.AddParticle(vector4, new Vector3(0f, 0f, 0f));
				}
			}
			num = (int)Vector3.Distance(this.leg3, this.lastleg3);
			if (num > 2)
			{
				Vector3 vector5 = Vector3.Normalize(this.leg3 - this.lastleg3);
				for (int k = 0; k < num; k += 4)
				{
					Vector3 vector6 = new Vector3(this.leg3.X + (float)k * vector5.X, this.leg3.Y + (float)k * vector5.Y, this.leg3.Z + (float)k * vector5.Z);
					this.streaks.AddParticle(vector6, new Vector3(0f, 0f, 0f));
				}
			}
			num = (int)Vector3.Distance(this.leg4, this.lastleg4);
			if (num > 2)
			{
				Vector3 vector7 = Vector3.Normalize(this.leg4 - this.lastleg4);
				for (int l = 0; l < num; l += 4)
				{
					Vector3 vector8 = new Vector3(this.leg4.X + (float)l * vector7.X, this.leg4.Y + (float)l * vector7.Y, this.leg4.Z + (float)l * vector7.Z);
					this.streaks.AddParticle(vector8, new Vector3(0f, 0f, 0f));
				}
			}
			this.lastleg1 = this.leg1;
			this.lastleg2 = this.leg2;
			this.lastleg3 = this.leg3;
			this.lastleg4 = this.leg4;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0041B414 File Offset: 0x00419614
		private void ejecta(int many)
		{
			for (float num = 1f; num < (float)many; num += 1f)
			{
				float num2 = (float)this.random.Next(40, 110) + this.lander.lander2ground / 6f;
				float num3 = (float)this.random.Next(1, 65000) / 10000f;
				float num4 = (float)Math.Sin((double)num3) * num2 + this.lander.ground.X;
				float num5 = (float)Math.Cos((double)num3) * num2 + this.lander.ground.Z;
				if (!this.lander.onDropship)
				{
					float num6;
					Vector3 vector;
					this.GetHeightAndNormal(ref this.tmake.heightData, ref this.tmake.normals, new Vector3(num4, 0f, num5), out num6, out vector);
					Vector3 vector2 = new Vector3(num4, num6, num5);
					Vector3 vector3 = Vector3.Reflect(new Vector3(num4, -((float)Math.Sin((double)(num / 80f)) * 10f + 26f), num5) - new Vector3(this.lander.ground.X, 0f, this.lander.ground.Z), vector) * (float)(this.random.Next(600, 3000) / 300);
					if (vector3.Y < 0f)
					{
						vector3.Y = Math.Abs(vector3.Y) + vector3.Y * (float)this.random.Next(1, 100) / 100f;
					}
					this.dust.AddParticle(vector2, vector3);
				}
				else
				{
					float num6;
					Vector3 vector;
					this.ShipHeightandNormal(new Vector3(num4, 0f, num5), out num6, out vector);
					if (num6 != -5000f)
					{
						Vector3 vector4 = new Vector3(num4, num6, num5);
						Vector3 vector5 = Vector3.Reflect(new Vector3(num4, -((float)Math.Sin((double)(num / 80f)) * 10f + 26f), num5) - new Vector3(this.lander.ground.X, 0f, this.lander.ground.Z), vector) * (float)(this.random.Next(600, 3000) / 300);
						if (vector5.Y < 0f)
						{
							vector5.Y = Math.Abs(vector5.Y) + vector5.Y * (float)this.random.Next(1, 100) / 100f;
						}
						this.dust.AddParticle(vector4, vector5);
					}
				}
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0041B6CC File Offset: 0x004198CC
		public void GetHeightAndNormal(ref int[,] heightData, ref Vector3[,] normals, Vector3 position, out float height, out Vector3 normal)
		{
			if (this.nearfarm)
			{
				this.GetHeightNormFarm(position, out height, out normal);
				return;
			}
			int num = (this.bitmap - 1) * this.gridscale;
			position.X = (position.X % (float)num + 1.5f * (float)num) % (float)num;
			position.Z = (position.Z % (float)num + 1.5f * (float)num) % (float)num;
			Vector3 vector = position;
			int num2 = (int)vector.X / this.gridscale;
			int num3 = (int)vector.Z / this.gridscale;
			float num4 = vector.X % (float)this.gridscale / (float)this.gridscale;
			float num5 = vector.Z % (float)this.gridscale / (float)this.gridscale;
			int num6 = num2 + 1;
			int num7 = num3 + 1;
			if (num6 > this.bitmap - 2)
			{
				num6 = 0;
			}
			if (num7 > this.bitmap - 2)
			{
				num7 = 0;
			}
			Vector3 vector2 = new Vector3((float)num2, (float)heightData[num2, num3], (float)num3);
			if (num4 + num5 >= 1f)
			{
				vector2 = new Vector3((float)num6, (float)heightData[num6, num7], (float)num7);
			}
			Vector3 vector3 = new Vector3((float)num2, (float)heightData[num2, num7], (float)num7);
			Vector3 vector4 = new Vector3((float)num6, (float)heightData[num6, num3], (float)num3);
			Vector2 vector5 = new Vector2(vector.X / (float)this.gridscale, vector.Z / (float)this.gridscale);
			float num8 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num9 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num8;
			float num10 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num8;
			float num11 = 1f - num9 - num10;
			height = num9 * vector2.Y + num10 * vector3.Y + num11 * vector4.Y;
			if (num4 + num5 > 1f)
			{
				vector2 = new Vector3((float)num6, (float)heightData[num6, num7], (float)num7);
			}
			vector2.Y /= (float)this.gridscale;
			vector3.Y /= (float)this.gridscale;
			vector4.Y /= (float)this.gridscale;
			Vector3 vector6 = vector2;
			Vector3 vector7 = vector3;
			Vector3 vector8 = vector4;
			Vector3 vector9 = Vector3.Cross(vector8 - vector7, vector6 - vector7);
			Vector3 vector10 = Vector3.Normalize(vector9);
			if (vector10.Y < 0f)
			{
				vector10 = -vector10;
			}
			normal = vector10;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0041B9EC File Offset: 0x00419BEC
		public void GetHeightOnly(ref int[,] heightData, Vector3 position, out float height)
		{
			int num = (this.bitmap - 1) * this.gridscale;
			position.X = (position.X % (float)num + 1.5f * (float)num) % (float)num;
			position.Z = (position.Z % (float)num + 1.5f * (float)num) % (float)num;
			Vector3 vector = position;
			int num2 = (int)vector.X / this.gridscale;
			int num3 = (int)vector.Z / this.gridscale;
			float num4 = vector.X % (float)this.gridscale / (float)this.gridscale;
			float num5 = vector.Z % (float)this.gridscale / (float)this.gridscale;
			int num6 = num2 + 1;
			int num7 = num3 + 1;
			if (num6 > this.bitmap - 2)
			{
				num6 = 0;
			}
			if (num7 > this.bitmap - 2)
			{
				num7 = 0;
			}
			Vector3 vector2 = new Vector3((float)num2, (float)heightData[num2, num3], (float)num3);
			if (num4 + num5 >= 1f)
			{
				vector2 = new Vector3((float)num6, (float)heightData[num6, num7], (float)num7);
			}
			Vector3 vector3 = new Vector3((float)num2, (float)heightData[num2, num7], (float)num7);
			Vector3 vector4 = new Vector3((float)num6, (float)heightData[num6, num3], (float)num3);
			Vector2 vector5 = new Vector2(vector.X / (float)this.gridscale, vector.Z / (float)this.gridscale);
			float num8 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num9 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num8;
			float num10 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num8;
			float num11 = 1f - num9 - num10;
			height = num9 * vector2.Y + num10 * vector3.Y + num11 * vector4.Y;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0041BC44 File Offset: 0x00419E44
		public void ShipHeight(Vector3 position, out float height)
		{
			int num = 81;
			float num2 = 102.5f;
			float num3 = (float)(num - 1) * num2;
			position.X = ((position.X - this.dropshipPOS.X) % num3 + 1.5f * num3) % num3;
			position.Z = ((position.Z - this.dropshipPOS.Z) % num3 + 1.5f * num3) % num3;
			Vector3 vector = position;
			int num4 = (int)vector.X / (int)num2;
			int num5 = (int)vector.Z / (int)num2;
			float num6 = vector.X % num2 / num2;
			float num7 = vector.Z % num2 / num2;
			int num8 = num4 + 1;
			int num9 = num5 + 1;
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			if (num9 > num - 2)
			{
				num9 = 0;
			}
			float num10 = MathHelper.Lerp(this.lander.drophite[num4, num5], this.lander.drophite[num8, num5], num6);
			float num11 = MathHelper.Lerp(this.lander.drophite[num4, num9], this.lander.drophite[num8, num9], num6);
			height = MathHelper.Lerp(num10, num11, num7);
			if (height <= 0f)
			{
				height = -5000f;
				return;
			}
			height += this.dropshipPOS.Y;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0041BD94 File Offset: 0x00419F94
		public void ShipHeightandNormal(Vector3 position, out float height, out Vector3 normal)
		{
			int num = 81;
			float num2 = 102.5f;
			float num3 = (float)(num - 1) * num2;
			position.X = ((position.X - this.dropshipPOS.X) % num3 + 1.5f * num3) % num3;
			position.Z = ((position.Z - this.dropshipPOS.Z) % num3 + 1.5f * num3) % num3;
			Vector3 vector = position;
			int num4 = (int)vector.X / (int)num2;
			int num5 = (int)vector.Z / (int)num2;
			float num6 = vector.X % num2 / num2;
			float num7 = vector.Z % num2 / num2;
			int num8 = num4 + 1;
			int num9 = num5 + 1;
			if (num8 > num - 2)
			{
				num8 = 0;
			}
			if (num9 > num - 2)
			{
				num9 = 0;
			}
			float num10 = MathHelper.Lerp(this.lander.drophite[num4, num5], this.lander.drophite[num8, num5], num6);
			float num11 = MathHelper.Lerp(this.lander.drophite[num4, num9], this.lander.drophite[num8, num9], num6);
			height = MathHelper.Lerp(num10, num11, num7);
			if (height <= 0f)
			{
				height = -5000f;
			}
			else
			{
				height += this.dropshipPOS.Y;
			}
			normal = Vector3.Normalize(new Vector3(-this.lander.drophite[num8, num5] + this.lander.drophite[num4, num5], 102.5f, -this.lander.drophite[num4, num9] + this.lander.drophite[num4, num5]));
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0041BF50 File Offset: 0x0041A150
		public void GetHeightFarm(Vector3 position, out float height)
		{
			Vector3 vector = new Vector3(3000f - this.farmLocation.X, 0f, 3000f - this.farmLocation.Z);
			position += vector;
			int num = (int)MathHelper.Clamp(position.X / 30f, 0f, 198f);
			int num2 = (int)MathHelper.Clamp(position.Z / 30f, 0f, 198f);
			float num3 = position.X % 30f / 30f;
			float num4 = position.Z % 30f / 30f;
			float num5 = (1f - num3) * (float)this.heights[num, num2] + num3 * (float)this.heights[num + 1, num2];
			float num6 = (1f - num3) * (float)this.heights[num, num2 + 1] + num3 * (float)this.heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0041C068 File Offset: 0x0041A268
		public void GetHeightFarm2(Vector3 position, out float height)
		{
			Vector3 vector = new Vector3(3000f - this.farmLocation.X, 0f, 3000f - this.farmLocation.Z);
			position += vector;
			int num = (int)MathHelper.Clamp(position.X / 30f, 0f, 198f);
			int num2 = (int)MathHelper.Clamp(position.Z / 30f, 0f, 198f);
			float num3 = position.X % 30f / 30f;
			float num4 = position.Z % 30f / 30f;
			int num5 = num + 1;
			int num6 = num2 + 1;
			if (num5 > 198)
			{
				num5 = 0;
			}
			if (num6 > 198)
			{
				num6 = 0;
			}
			Vector3 vector2 = new Vector3((float)num, (float)this.heights[num, num2], (float)num2);
			if (num3 + num4 >= 1f)
			{
				vector2 = new Vector3((float)num5, (float)this.heights[num5, num6], (float)num6);
			}
			Vector3 vector3 = new Vector3((float)num, (float)this.heights[num, num6], (float)num6);
			Vector3 vector4 = new Vector3((float)num5, (float)this.heights[num5, num2], (float)num2);
			Vector2 vector5 = new Vector2(position.X / 30f, position.Z / 30f);
			float num7 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num8 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num7;
			float num9 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num7;
			float num10 = 1f - num8 - num9;
			height = num8 * vector2.Y + num9 * vector3.Y + num10 * vector4.Y;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0041C2C4 File Offset: 0x0041A4C4
		public void GetHeightNormFarm(Vector3 position, out float height, out Vector3 normal)
		{
			Vector3 vector = new Vector3(3000f - this.farmLocation.X, 0f, 3000f - this.farmLocation.Z);
			position += vector;
			int num = (int)MathHelper.Clamp(position.X / 30f, 0f, 198f);
			int num2 = (int)MathHelper.Clamp(position.Z / 30f, 0f, 198f);
			float num3 = position.X % 30f / 30f;
			float num4 = position.Z % 30f / 30f;
			int num5 = num + 1;
			int num6 = num2 + 1;
			if (num5 > 198)
			{
				num5 = 0;
			}
			if (num6 > 198)
			{
				num6 = 0;
			}
			Vector3 vector2 = new Vector3((float)num, (float)this.heights[num, num2], (float)num2);
			if (num3 + num4 >= 1f)
			{
				vector2 = new Vector3((float)num5, (float)this.heights[num5, num6], (float)num6);
			}
			Vector3 vector3 = new Vector3((float)num, (float)this.heights[num, num6], (float)num6);
			Vector3 vector4 = new Vector3((float)num5, (float)this.heights[num5, num2], (float)num2);
			Vector2 vector5 = new Vector2(position.X / 30f, position.Z / 30f);
			float num7 = (vector3.Z - vector4.Z) * (vector2.X - vector4.X) + (vector4.X - vector3.X) * (vector2.Z - vector4.Z);
			float num8 = ((vector3.Z - vector4.Z) * (vector5.X - vector4.X) + (vector4.X - vector3.X) * (vector5.Y - vector4.Z)) / num7;
			float num9 = ((vector4.Z - vector2.Z) * (vector5.X - vector4.X) + (vector2.X - vector4.X) * (vector5.Y - vector4.Z)) / num7;
			float num10 = 1f - num8 - num9;
			height = num8 * vector2.Y + num9 * vector3.Y + num10 * vector4.Y;
			if (num3 + num4 > 1f)
			{
				vector2 = new Vector3((float)num5, (float)this.heights[num5, num6], (float)num6);
			}
			vector2.Y /= 30f;
			vector3.Y /= 30f;
			vector4.Y /= 30f;
			Vector3 vector6 = vector2;
			Vector3 vector7 = vector3;
			Vector3 vector8 = vector4;
			Vector3 vector9 = Vector3.Cross(vector8 - vector7, vector6 - vector7);
			Vector3 vector10 = Vector3.Normalize(vector9);
			if (vector10.Y < 0f)
			{
				vector10 = -vector10;
			}
			normal = vector10;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0041C5D0 File Offset: 0x0041A7D0
		private void DrawPrints(ref GameplayScreen.hole hole)
		{
			int stainMax = hole.stainMax;
			if (stainMax < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = this.footprint.Meshes[0].MeshParts[0];
			hole.stainBuffer.SetData<GameplayScreen.hitStream>(hole.stainTrans, 0, stainMax, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.Parameters["World"].SetValue(Matrix.Identity);
			effect.Parameters["NormalMap"].SetValue(this.normalmap);
			effect.Parameters["AmbientColor"].SetValue(new Vector4(0.1f, 0.1f, 0.1f, 1f));
			effect.Parameters["DiffuseColor"].SetValue(new Vector4(1f, 1f, 1f, 1f));
			effect.Parameters["LightDirection"].SetValue(new Vector3(this.sunDir.X, this.sunDir.Y, this.sunDir.Z));
			effect.CurrentTechnique = effect.Techniques["feet"];
			effect.Parameters["View"].SetValue(this.viewMatrix);
			effect.Parameters["Projection"].SetValue(this.projectionMatrix);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(hole.stainBuffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, stainMax);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0041C7E4 File Offset: 0x0041A9E4
		private void RestoreRenderStates()
		{
			this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
			this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			this.sc.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0041C84C File Offset: 0x0041AA4C
		public bool queryButton2(Rectangle rr)
		{
			this.adj = new Vector2((float)this.mouseState.X, (float)this.mouseState.Y);
			if (this.sc.aspectratio <= 1f)
			{
				this.adj.Y = this.adj.Y - ((float)this.sc.screenSize.Height - (float)this.sc.screenSize.Height * this.sc.aspectratio) * (0.5f / this.sc.aspectratio);
			}
			else
			{
				this.adj.X = this.adj.X - ((float)this.sc.screenSize.Width - (float)this.sc.screenSize.Width / this.sc.aspectratio) * (0.5f * this.sc.aspectratio);
			}
			Vector2 vector = new Vector2((float)this.sc.screenSize.Width / (float)this.sc.origSize.Width, (float)this.sc.screenSize.Height / (float)this.sc.origSize.Height);
			this.adj /= vector;
			return this.adj.Y > (float)rr.Y && this.adj.Y <= (float)(rr.Y + rr.Height) && this.adj.X > (float)rr.X && this.adj.X <= (float)(rr.X + rr.Width);
		}

		// Token: 0x04003AB8 RID: 15032
		private const float rotationSpeed = 0.3f;

		// Token: 0x04003AB9 RID: 15033
		private const float moveSpeed = 30f;

		// Token: 0x04003ABA RID: 15034
		private float flowervol;

		// Token: 0x04003ABB RID: 15035
		private int flowervolumeTimer;

		// Token: 0x04003ABC RID: 15036
		private int tragetCount;

		// Token: 0x04003ABD RID: 15037
		private Vector3 barnloc1;

		// Token: 0x04003ABE RID: 15038
		private Vector2 xSpace;

		// Token: 0x04003ABF RID: 15039
		private Vector2 zSpace;

		// Token: 0x04003AC0 RID: 15040
		private int firstsapphire;

		// Token: 0x04003AC1 RID: 15041
		private int firstshale;

		// Token: 0x04003AC2 RID: 15042
		private int firstruby;

		// Token: 0x04003AC3 RID: 15043
		private bool softlockMess1 = true;

		// Token: 0x04003AC4 RID: 15044
		private bool softlockMess2 = true;

		// Token: 0x04003AC5 RID: 15045
		private bool cameditMess1 = true;

		// Token: 0x04003AC6 RID: 15046
		private bool cameditMess2 = true;

		// Token: 0x04003AC7 RID: 15047
		private bool editcam;

		// Token: 0x04003AC8 RID: 15048
		private bool camadjust;

		// Token: 0x04003AC9 RID: 15049
		private Vector2 adj = Vector2.Zero;

		// Token: 0x04003ACA RID: 15050
		private Rectangle rovercamedit = new Rectangle(0, 0, 404, 346);

		// Token: 0x04003ACB RID: 15051
		private Rectangle landercamedit = new Rectangle(0, 0, 404, 346);

		// Token: 0x04003ACC RID: 15052
		private Vector2 menuposition = new Vector2(50f, 150f);

		// Token: 0x04003ACD RID: 15053
		private Rectangle buttonhalf = new Rectangle(0, 346, 33, 34);

		// Token: 0x04003ACE RID: 15054
		private Rectangle buttonfill = new Rectangle(35, 346, 33, 34);

		// Token: 0x04003ACF RID: 15055
		private Rectangle buttonhand = new Rectangle(70, 350, 30, 30);

		// Token: 0x04003AD0 RID: 15056
		private Rectangle buttonchoice = new Rectangle(70, 350, 30, 30);

		// Token: 0x04003AD1 RID: 15057
		private Rectangle buttonzoom = new Rectangle(100, 350, 30, 30);

		// Token: 0x04003AD2 RID: 15058
		private Vector2[] pointbox1 = new Vector2[]
		{
			new Vector2(330f, 65f),
			new Vector2(330f, 111f),
			new Vector2(330f, 159f),
			new Vector2(330f, 206f)
		};

		// Token: 0x04003AD3 RID: 15059
		public int humanonramp;

		// Token: 0x04003AD4 RID: 15060
		private Vector3 dumpos = Vector3.Zero;

		// Token: 0x04003AD5 RID: 15061
		private Vector3 groundy = Vector3.Zero;

		// Token: 0x04003AD6 RID: 15062
		private Vector3 normalxxx = Vector3.Zero;

		// Token: 0x04003AD7 RID: 15063
		private int waiting;

		// Token: 0x04003AD8 RID: 15064
		private int recruitCount;

		// Token: 0x04003AD9 RID: 15065
		private int rescueCount;

		// Token: 0x04003ADA RID: 15066
		public GameplayScreen.act emo;

		// Token: 0x04003ADB RID: 15067
		private int refineRuby;

		// Token: 0x04003ADC RID: 15068
		private int refineBlue;

		// Token: 0x04003ADD RID: 15069
		private int refineShale;

		// Token: 0x04003ADE RID: 15070
		private Effect reflectEffect;

		// Token: 0x04003ADF RID: 15071
		private Effect muzzleEffect;

		// Token: 0x04003AE0 RID: 15072
		private Effect buildingEffect;

		// Token: 0x04003AE1 RID: 15073
		private Texture2D mountTexture;

		// Token: 0x04003AE2 RID: 15074
		private Texture2D grassTexture;

		// Token: 0x04003AE3 RID: 15075
		private Texture2D buildingRGB;

		// Token: 0x04003AE4 RID: 15076
		private Texture2D buildingShadow;

		// Token: 0x04003AE5 RID: 15077
		private Model heightmodel;

		// Token: 0x04003AE6 RID: 15078
		private int[,] heights = new int[0, 0];

		// Token: 0x04003AE7 RID: 15079
		private Vector3 farmLocation = new Vector3(0f, 5f, 0f);

		// Token: 0x04003AE8 RID: 15080
		private bool nearfarm;

		// Token: 0x04003AE9 RID: 15081
		private Thread backgroundThread;

		// Token: 0x04003AEA RID: 15082
		private EventWaitHandle backgroundThreadExit;

		// Token: 0x04003AEB RID: 15083
		private bool isBusy;

		// Token: 0x04003AEC RID: 15084
		private bool isDrawing;

		// Token: 0x04003AED RID: 15085
		private List<int> loads = new List<int>();

		// Token: 0x04003AEE RID: 15086
		private int print;

		// Token: 0x04003AEF RID: 15087
		private string mytext = "none";

		// Token: 0x04003AF0 RID: 15088
		private int roverY = 30;

		// Token: 0x04003AF1 RID: 15089
		private int landerY = 150;

		// Token: 0x04003AF2 RID: 15090
		private int frameRate;

		// Token: 0x04003AF3 RID: 15091
		private int frameCounter;

		// Token: 0x04003AF4 RID: 15092
		private TimeSpan elapsedTime = TimeSpan.Zero;

		// Token: 0x04003AF5 RID: 15093
		private float X;

		// Token: 0x04003AF6 RID: 15094
		private float Y;

		// Token: 0x04003AF7 RID: 15095
		private float W;

		// Token: 0x04003AF8 RID: 15096
		private float X2;

		// Token: 0x04003AF9 RID: 15097
		private float Y2;

		// Token: 0x04003AFA RID: 15098
		private float W2;

		// Token: 0x04003AFB RID: 15099
		private Vector3 mercury = new Vector3(1f, 0.95f, 0.87f);

		// Token: 0x04003AFC RID: 15100
		private Vector3 venus = new Vector3(0.9f, 1f, 0.9f);

		// Token: 0x04003AFD RID: 15101
		private Vector3 earth = new Vector3(1f, 1f, 0.95f);

		// Token: 0x04003AFE RID: 15102
		private Vector3 mars = new Vector3(1.1f, 0.8f, 0.7f);

		// Token: 0x04003AFF RID: 15103
		private Vector3 jupiter = new Vector3(0.9f, 0.8f, 0.7f);

		// Token: 0x04003B00 RID: 15104
		private Vector3 saturn = new Vector3(0.9f, 0.9f, 0.7f);

		// Token: 0x04003B01 RID: 15105
		private Vector3 uranus = new Vector3(0.6f, 0.6f, 1.1f);

		// Token: 0x04003B02 RID: 15106
		private Vector3 neptune = new Vector3(0.5f, 0.8f, 0.9f);

		// Token: 0x04003B03 RID: 15107
		private Vector3 pluto = new Vector3(0.95f, 1f, 0.9f);

		// Token: 0x04003B04 RID: 15108
		private Vector3 c10 = new Vector3(0.4f, 0.4f, 0.4f);

		// Token: 0x04003B05 RID: 15109
		private Vector3[] tinting;

		// Token: 0x04003B06 RID: 15110
		private Vector3[] chosenBouy = new Vector3[3];

		// Token: 0x04003B07 RID: 15111
		private Vector3[] colorBouy = new Vector3[]
		{
			new Vector3(0.8f, 1.2f, 0.9f),
			new Vector3(1.2f, 0.8f, 0.9f),
			new Vector3(0.8f, 0.92f, 1.2f)
		};

		// Token: 0x04003B08 RID: 15112
		private int[] pulseBouy = new int[] { 120, 70, 172 };

		// Token: 0x04003B09 RID: 15113
		private Effect blurEffect;

		// Token: 0x04003B0A RID: 15114
		private bool onDropship = true;

		// Token: 0x04003B0B RID: 15115
		private bool displaymap;

		// Token: 0x04003B0C RID: 15116
		private float displaymapx;

		// Token: 0x04003B0D RID: 15117
		private float displaymapz;

		// Token: 0x04003B0E RID: 15118
		private int myframe;

		// Token: 0x04003B0F RID: 15119
		private int horntimer;

		// Token: 0x04003B10 RID: 15120
		private int horncount;

		// Token: 0x04003B11 RID: 15121
		private int radiotimer;

		// Token: 0x04003B12 RID: 15122
		private int radiocount;

		// Token: 0x04003B13 RID: 15123
		private int landNum1 = 1;

		// Token: 0x04003B14 RID: 15124
		private int landNum2 = 12;

		// Token: 0x04003B15 RID: 15125
		private int landNum3 = 43;

		// Token: 0x04003B16 RID: 15126
		private int landNum4 = 67;

		// Token: 0x04003B17 RID: 15127
		private float surf;

		// Token: 0x04003B18 RID: 15128
		private Effect mybasic;

		// Token: 0x04003B19 RID: 15129
		private Texture2D dropshipTexture;

		// Token: 0x04003B1A RID: 15130
		private Texture2D dropshipLights;

		// Token: 0x04003B1B RID: 15131
		private Texture2D shieldTexture;

		// Token: 0x04003B1C RID: 15132
		private Texture2D distortion;

		// Token: 0x04003B1D RID: 15133
		private float shortclip = 15f;

		// Token: 0x04003B1E RID: 15134
		private float farclip = 75000f;

		// Token: 0x04003B1F RID: 15135
		private float shortclip2 = 5f;

		// Token: 0x04003B20 RID: 15136
		private float farclip2 = 150000f;

		// Token: 0x04003B21 RID: 15137
		public float val1 = 4580f;

		// Token: 0x04003B22 RID: 15138
		public float val2 = 4580f;

		// Token: 0x04003B23 RID: 15139
		private bool atRover;

		// Token: 0x04003B24 RID: 15140
		private bool atLander;

		// Token: 0x04003B25 RID: 15141
		private Rectangle rect_Blue = new Rectangle(793, 34, 30, 30);

		// Token: 0x04003B26 RID: 15142
		private Rectangle rect_Xbutton = new Rectangle(776, 75, 30, 30);

		// Token: 0x04003B27 RID: 15143
		private Rectangle rect_Abutton = new Rectangle(719, 34, 30, 30);

		// Token: 0x04003B28 RID: 15144
		private Rectangle rect_Bbutton = new Rectangle(755, 34, 30, 30);

		// Token: 0x04003B29 RID: 15145
		private Rectangle rect_Wbutton = new Rectangle(991, 34, 30, 30);

		// Token: 0x04003B2A RID: 15146
		private Rectangle rect_Ybutton = new Rectangle(831, 34, 30, 30);

		// Token: 0x04003B2B RID: 15147
		private Rectangle rect_Bulb = new Rectangle(871, 34, 30, 30);

		// Token: 0x04003B2C RID: 15148
		private Rectangle rect_Heart = new Rectangle(828, 110, 37, 34);

		// Token: 0x04003B2D RID: 15149
		private Rectangle rect_Lock = new Rectangle(910, 34, 30, 30);

		// Token: 0x04003B2E RID: 15150
		private Rectangle rect_Lock2 = new Rectangle(952, 75, 30, 30);

		// Token: 0x04003B2F RID: 15151
		private Rectangle rect_Exclaim = new Rectangle(951, 34, 30, 30);

		// Token: 0x04003B30 RID: 15152
		private Rectangle rect_Exclaim2 = new Rectangle(1031, 81, 44, 39);

		// Token: 0x04003B31 RID: 15153
		private Rectangle[] rect_choice = new Rectangle[]
		{
			new Rectangle(1028, 30, 53, 51),
			new Rectangle(1028, 30, 53, 51),
			new Rectangle(909, 74, 32, 32),
			new Rectangle(830, 74, 32, 32),
			new Rectangle(951, 34, 30, 30),
			new Rectangle(994, 75, 30, 30),
			new Rectangle(699, 76, 58, 30),
			new Rectangle(828, 110, 10, 10)
		};

		// Token: 0x04003B32 RID: 15154
		private bool atFarmer;

		// Token: 0x04003B33 RID: 15155
		private bool lookatFarmer;

		// Token: 0x04003B34 RID: 15156
		private bool atPig;

		// Token: 0x04003B35 RID: 15157
		private bool atBarnDoor;

		// Token: 0x04003B36 RID: 15158
		private bool atKissing;

		// Token: 0x04003B37 RID: 15159
		private bool nearAstro;

		// Token: 0x04003B38 RID: 15160
		private bool viewGrinder;

		// Token: 0x04003B39 RID: 15161
		private bool nearFlower;

		// Token: 0x04003B3A RID: 15162
		private bool atPump1;

		// Token: 0x04003B3B RID: 15163
		private bool atPump2;

		// Token: 0x04003B3C RID: 15164
		private bool viewPump2;

		// Token: 0x04003B3D RID: 15165
		private bool viewPump1;

		// Token: 0x04003B3E RID: 15166
		private int localPump;

		// Token: 0x04003B3F RID: 15167
		private int pump1Level = 12;

		// Token: 0x04003B40 RID: 15168
		private int pump2Level = 12;

		// Token: 0x04003B41 RID: 15169
		private float waterRamp1;

		// Token: 0x04003B42 RID: 15170
		private float waterRamp2;

		// Token: 0x04003B43 RID: 15171
		private bool atLever;

		// Token: 0x04003B44 RID: 15172
		private int leverLevel;

		// Token: 0x04003B45 RID: 15173
		private float leverRamp;

		// Token: 0x04003B46 RID: 15174
		private int leverTimer;

		// Token: 0x04003B47 RID: 15175
		private int shockDelay;

		// Token: 0x04003B48 RID: 15176
		private int shatterDelay;

		// Token: 0x04003B49 RID: 15177
		private int shatterBigCount;

		// Token: 0x04003B4A RID: 15178
		private static StringBuilder gamertagBuild = new StringBuilder(32, 32);

		// Token: 0x04003B4B RID: 15179
		private static StringBuilder nearastroBuild = new StringBuilder(64, 64);

		// Token: 0x04003B4C RID: 15180
		private static StringBuilder closehelpBuild = new StringBuilder(64, 64);

		// Token: 0x04003B4D RID: 15181
		private static StringBuilder helpingUpBuild = new StringBuilder(64, 64);

		// Token: 0x04003B4E RID: 15182
		private static StringBuilder atDoorBuild = new StringBuilder(32, 32);

		// Token: 0x04003B4F RID: 15183
		private static StringBuilder atPump1Build = new StringBuilder(32, 32);

		// Token: 0x04003B50 RID: 15184
		private static StringBuilder atPump2Build = new StringBuilder(32, 32);

		// Token: 0x04003B51 RID: 15185
		private static StringBuilder atPumpBusyBuild = new StringBuilder(32, 32);

		// Token: 0x04003B52 RID: 15186
		private static StringBuilder atgrinderBuild = new StringBuilder(52, 52);

		// Token: 0x04003B53 RID: 15187
		private static StringBuilder atKissingBuild = new StringBuilder(52, 52);

		// Token: 0x04003B54 RID: 15188
		private static StringBuilder needbloodBuild = new StringBuilder(32, 32);

		// Token: 0x04003B55 RID: 15189
		private static StringBuilder reloadBuild = new StringBuilder(32, 32);

		// Token: 0x04003B56 RID: 15190
		private static StringBuilder atLever1Build = new StringBuilder(44, 44);

		// Token: 0x04003B57 RID: 15191
		private static StringBuilder atLever2Build = new StringBuilder(44, 44);

		// Token: 0x04003B58 RID: 15192
		private static StringBuilder atLever3Build = new StringBuilder(44, 44);

		// Token: 0x04003B59 RID: 15193
		private static StringBuilder atDoorLockedBuild = new StringBuilder(40, 40);

		// Token: 0x04003B5A RID: 15194
		private static StringBuilder atFarmerBuild = new StringBuilder(40, 40);

		// Token: 0x04003B5B RID: 15195
		private static StringBuilder pickupMilkBuild = new StringBuilder(40, 40);

		// Token: 0x04003B5C RID: 15196
		private static StringBuilder pickupAmmoBuild = new StringBuilder(40, 40);

		// Token: 0x04003B5D RID: 15197
		private static StringBuilder pickupHulkBuild = new StringBuilder(40, 40);

		// Token: 0x04003B5E RID: 15198
		private static StringBuilder pickupRocketBuild = new StringBuilder(40, 40);

		// Token: 0x04003B5F RID: 15199
		private static StringBuilder pickupFullBuild = new StringBuilder(40, 40);

		// Token: 0x04003B60 RID: 15200
		private static StringBuilder pickupGrenBuild = new StringBuilder(40, 40);

		// Token: 0x04003B61 RID: 15201
		private static StringBuilder pickupPillBuild = new StringBuilder(40, 40);

		// Token: 0x04003B62 RID: 15202
		private static StringBuilder pickWeaponBuild = new StringBuilder(40, 40);

		// Token: 0x04003B63 RID: 15203
		private static StringBuilder liftingFriendBuild = new StringBuilder(40, 40);

		// Token: 0x04003B64 RID: 15204
		private static StringBuilder coltammoBuild = new StringBuilder(40, 40);

		// Token: 0x04003B65 RID: 15205
		private static StringBuilder akammoBuild = new StringBuilder(40, 40);

		// Token: 0x04003B66 RID: 15206
		private static StringBuilder akmagBuild = new StringBuilder(40, 40);

		// Token: 0x04003B67 RID: 15207
		private static StringBuilder memo = new StringBuilder(75, 75);

		// Token: 0x04003B68 RID: 15208
		private int memoTimer;

		// Token: 0x04003B69 RID: 15209
		private int memoIcon;

		// Token: 0x04003B6A RID: 15210
		private static StringBuilder dpad = new StringBuilder(44, 44);

		// Token: 0x04003B6B RID: 15211
		private int dpadTimer;

		// Token: 0x04003B6C RID: 15212
		private int dpadCount;

		// Token: 0x04003B6D RID: 15213
		private static StringBuilder my_stringbuilder = new StringBuilder(64, 64);

		// Token: 0x04003B6E RID: 15214
		private static StringBuilder myScore = new StringBuilder(32, 32);

		// Token: 0x04003B6F RID: 15215
		private static StringBuilder remScore = new StringBuilder(32, 32);

		// Token: 0x04003B70 RID: 15216
		private static StringBuilder pigsalive = new StringBuilder(32, 32);

		// Token: 0x04003B71 RID: 15217
		private static StringBuilder whatday = new StringBuilder(32, 32);

		// Token: 0x04003B72 RID: 15218
		private static StringBuilder surviveT = new StringBuilder(12, 12);

		// Token: 0x04003B73 RID: 15219
		private static StringBuilder survive4 = new StringBuilder(12, 12);

		// Token: 0x04003B74 RID: 15220
		private static StringBuilder survive3 = new StringBuilder(12, 12);

		// Token: 0x04003B75 RID: 15221
		private static StringBuilder survive2 = new StringBuilder(12, 12);

		// Token: 0x04003B76 RID: 15222
		private static StringBuilder survive1 = new StringBuilder(12, 12);

		// Token: 0x04003B77 RID: 15223
		private static StringBuilder packets = new StringBuilder(64, 64);

		// Token: 0x04003B78 RID: 15224
		public static StringBuilder stat1Build = new StringBuilder(32, 32);

		// Token: 0x04003B79 RID: 15225
		public string stat1 = "";

		// Token: 0x04003B7A RID: 15226
		public static StringBuilder stat2Build = new StringBuilder(32, 32);

		// Token: 0x04003B7B RID: 15227
		public string stat2 = "";

		// Token: 0x04003B7C RID: 15228
		public static StringBuilder stat3Build = new StringBuilder(32, 32);

		// Token: 0x04003B7D RID: 15229
		public string stat3 = "";

		// Token: 0x04003B7E RID: 15230
		public static StringBuilder stat4Build = new StringBuilder(32, 32);

		// Token: 0x04003B7F RID: 15231
		public string stat4 = "";

		// Token: 0x04003B80 RID: 15232
		public static StringBuilder stat5Build = new StringBuilder(32, 32);

		// Token: 0x04003B81 RID: 15233
		public string stat5 = "";

		// Token: 0x04003B82 RID: 15234
		public static StringBuilder stat6Build = new StringBuilder(32, 32);

		// Token: 0x04003B83 RID: 15235
		public string stat6 = "";

		// Token: 0x04003B84 RID: 15236
		public static StringBuilder messBuild = new StringBuilder(32, 32);

		// Token: 0x04003B85 RID: 15237
		public string mess = "";

		// Token: 0x04003B86 RID: 15238
		public static StringBuilder rover1Build = new StringBuilder(32, 32);

		// Token: 0x04003B87 RID: 15239
		public string rover1 = "";

		// Token: 0x04003B88 RID: 15240
		private Model sphere;

		// Token: 0x04003B89 RID: 15241
		private int cutoff;

		// Token: 0x04003B8A RID: 15242
		private Matrix starRot = Matrix.CreateRotationX(1.1f) * Matrix.CreateRotationZ(0.5f);

		// Token: 0x04003B8B RID: 15243
		private Vector3 trnLoctn = Vector3.Zero;

		// Token: 0x04003B8C RID: 15244
		private Vector3 oldtrnLoctn;

		// Token: 0x04003B8D RID: 15245
		private Vector3 trnLoctnLast;

		// Token: 0x04003B8E RID: 15246
		private Vector3 oldtrnLoctn2;

		// Token: 0x04003B8F RID: 15247
		private Vector3 delta;

		// Token: 0x04003B90 RID: 15248
		private ContentManager content;

		// Token: 0x04003B91 RID: 15249
		private Vector3 moonPos = new Vector3(500f, -3000f, 500f);

		// Token: 0x04003B92 RID: 15250
		private Random random;

		// Token: 0x04003B93 RID: 15251
		private Vector3 shake = Vector3.Zero;

		// Token: 0x04003B94 RID: 15252
		private int vibrateRover;

		// Token: 0x04003B95 RID: 15253
		private float vibrateLander;

		// Token: 0x04003B96 RID: 15254
		private int vibrateControl;

		// Token: 0x04003B97 RID: 15255
		private float xx;

		// Token: 0x04003B98 RID: 15256
		private float zz;

		// Token: 0x04003B99 RID: 15257
		private float yy = 200f;

		// Token: 0x04003B9A RID: 15258
		private float rot;

		// Token: 0x04003B9B RID: 15259
		private float myrotter;

		// Token: 0x04003B9C RID: 15260
		private int clickradius1 = 2;

		// Token: 0x04003B9D RID: 15261
		private float clickradius2 = 500f;

		// Token: 0x04003B9E RID: 15262
		private int landerbuttonindex = 1;

		// Token: 0x04003B9F RID: 15263
		private int roverbuttonindex = 1;

		// Token: 0x04003BA0 RID: 15264
		private Vector3 sunDir;

		// Token: 0x04003BA1 RID: 15265
		private bool delayinput = true;

		// Token: 0x04003BA2 RID: 15266
		private GamePadState prevstate;

		// Token: 0x04003BA3 RID: 15267
		private PlayerIndex myplayer;

		// Token: 0x04003BA4 RID: 15268
		private GamePadState gamePadState;

		// Token: 0x04003BA5 RID: 15269
		private KeyboardState keyState;

		// Token: 0x04003BA6 RID: 15270
		private KeyboardState prevkeyState;

		// Token: 0x04003BA7 RID: 15271
		private MouseState mouseState;

		// Token: 0x04003BA8 RID: 15272
		private MouseState prevMouse;

		// Token: 0x04003BA9 RID: 15273
		private float mouseX;

		// Token: 0x04003BAA RID: 15274
		private float mouseY;

		// Token: 0x04003BAB RID: 15275
		private readonly Vector3 CameraTargetOffset = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BAC RID: 15276
		private Model stardome;

		// Token: 0x04003BAD RID: 15277
		private Model darkFog;

		// Token: 0x04003BAE RID: 15278
		private Model LanderShadow;

		// Token: 0x04003BAF RID: 15279
		private Model tankShadow;

		// Token: 0x04003BB0 RID: 15280
		private Model bigquad;

		// Token: 0x04003BB1 RID: 15281
		private Model galaxy;

		// Token: 0x04003BB2 RID: 15282
		private Rover rover;

		// Token: 0x04003BB3 RID: 15283
		private Lander lander;

		// Token: 0x04003BB4 RID: 15284
		public Facility facility;

		// Token: 0x04003BB5 RID: 15285
		private Model dropship;

		// Token: 0x04003BB6 RID: 15286
		private Model dropshipShade;

		// Token: 0x04003BB7 RID: 15287
		private Vector3 dropshipPOS = Vector3.Zero;

		// Token: 0x04003BB8 RID: 15288
		private BoundingBox dropshipBox;

		// Token: 0x04003BB9 RID: 15289
		private Vector3 DSmax = new Vector3(410.99f, 347f, 819.26f);

		// Token: 0x04003BBA RID: 15290
		private Vector3 DSmin = new Vector3(-410.99f, -245f, -699.58f);

		// Token: 0x04003BBB RID: 15291
		private float dropshipAcc = 20f;

		// Token: 0x04003BBC RID: 15292
		private tmake tmake;

		// Token: 0x04003BBD RID: 15293
		private MoonSurface surface;

		// Token: 0x04003BBE RID: 15294
		private Vector3 leg1 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BBF RID: 15295
		private Vector3 leg2 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC0 RID: 15296
		private Vector3 leg3 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC1 RID: 15297
		private Vector3 leg4 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC2 RID: 15298
		private Vector3 lastleg1 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC3 RID: 15299
		private Vector3 lastleg2 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC4 RID: 15300
		private Vector3 lastleg3 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC5 RID: 15301
		private Vector3 lastleg4 = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC6 RID: 15302
		private Vector3 lastpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC7 RID: 15303
		private Vector3 lastlanderpos = new Vector3(0f, 0f, 0f);

		// Token: 0x04003BC8 RID: 15304
		private Matrix projectionMatrix;

		// Token: 0x04003BC9 RID: 15305
		private Matrix longProjection;

		// Token: 0x04003BCA RID: 15306
		private Matrix longerProjection;

		// Token: 0x04003BCB RID: 15307
		private Matrix viewMatrix;

		// Token: 0x04003BCC RID: 15308
		private float lens = 70f;

		// Token: 0x04003BCD RID: 15309
		private Vector3 aCamtarget;

		// Token: 0x04003BCE RID: 15310
		private Vector3 aUppy = Vector3.Up;

		// Token: 0x04003BCF RID: 15311
		private Vector3 bCampos;

		// Token: 0x04003BD0 RID: 15312
		private Vector3 bCamtarget;

		// Token: 0x04003BD1 RID: 15313
		private Vector3 bUppy = Vector3.Up;

		// Token: 0x04003BD2 RID: 15314
		private Vector3 lastCampos = Vector3.Zero;

		// Token: 0x04003BD3 RID: 15315
		private int vehicleindex = 3;

		// Token: 0x04003BD4 RID: 15316
		private Overlay overlay;

		// Token: 0x04003BD5 RID: 15317
		private int musictick;

		// Token: 0x04003BD6 RID: 15318
		private SoundEffectInstance landerEngineI;

		// Token: 0x04003BD7 RID: 15319
		private SoundEffectInstance roverEngineI;

		// Token: 0x04003BD8 RID: 15320
		private SoundEffectInstance dropEngineI;

		// Token: 0x04003BD9 RID: 15321
		private SoundEffectInstance gravelI;

		// Token: 0x04003BDA RID: 15322
		private SoundEffectInstance overtureInstance;

		// Token: 0x04003BDB RID: 15323
		private SoundEffectInstance flowerradioInstance;

		// Token: 0x04003BDC RID: 15324
		private SoundEffectInstance breathing;

		// Token: 0x04003BDD RID: 15325
		private float breathvol = 0.5f;

		// Token: 0x04003BDE RID: 15326
		private Effect terrainEffect;

		// Token: 0x04003BDF RID: 15327
		private Effect shadowEffect;

		// Token: 0x04003BE0 RID: 15328
		private Effect simpEffect;

		// Token: 0x04003BE1 RID: 15329
		private Effect starEffect;

		// Token: 0x04003BE2 RID: 15330
		private SpriteBatch spriteBatch;

		// Token: 0x04003BE3 RID: 15331
		private SpriteBatch starBatch;

		// Token: 0x04003BE4 RID: 15332
		private int numVertices;

		// Token: 0x04003BE5 RID: 15333
		private int numTriangles;

		// Token: 0x04003BE6 RID: 15334
		private Vector3[,] normalData;

		// Token: 0x04003BE7 RID: 15335
		private int[,] heightData;

		// Token: 0x04003BE8 RID: 15336
		private int[,] objectData;

		// Token: 0x04003BE9 RID: 15337
		private int myposx = 1000;

		// Token: 0x04003BEA RID: 15338
		private int myposz = 1000;

		// Token: 0x04003BEB RID: 15339
		private Texture2D tt;

		// Token: 0x04003BEC RID: 15340
		private Texture2D layer1;

		// Token: 0x04003BED RID: 15341
		private Texture2D layer2;

		// Token: 0x04003BEE RID: 15342
		private Texture2D layer3;

		// Token: 0x04003BEF RID: 15343
		private Texture2D layer4;

		// Token: 0x04003BF0 RID: 15344
		private ParticleSystem skids;

		// Token: 0x04003BF1 RID: 15345
		private ParticleSystem streaks;

		// Token: 0x04003BF2 RID: 15346
		private ParticleSystem dust;

		// Token: 0x04003BF3 RID: 15347
		private SpriteFont font;

		// Token: 0x04003BF4 RID: 15348
		private SpriteFont ammoMedium2;

		// Token: 0x04003BF5 RID: 15349
		private SpriteFont afont1;

		// Token: 0x04003BF6 RID: 15350
		private SpriteFont afont2;

		// Token: 0x04003BF7 RID: 15351
		private SpriteFont afont3;

		// Token: 0x04003BF8 RID: 15352
		private SpriteFont afont4;

		// Token: 0x04003BF9 RID: 15353
		private SpriteFont afont5;

		// Token: 0x04003BFA RID: 15354
		private SpriteFont afont6;

		// Token: 0x04003BFB RID: 15355
		private SpriteFont afont7;

		// Token: 0x04003BFC RID: 15356
		private SpriteFont afont8;

		// Token: 0x04003BFD RID: 15357
		private SpriteFont[] ammoMedium;

		// Token: 0x04003BFE RID: 15358
		private int fontindex;

		// Token: 0x04003BFF RID: 15359
		private int bitmap;

		// Token: 0x04003C00 RID: 15360
		private int gridscale;

		// Token: 0x04003C01 RID: 15361
		private int unit;

		// Token: 0x04003C02 RID: 15362
		private int zgrid;

		// Token: 0x04003C03 RID: 15363
		private int xgrid;

		// Token: 0x04003C04 RID: 15364
		private int lastzgrid;

		// Token: 0x04003C05 RID: 15365
		private int lastxgrid;

		// Token: 0x04003C06 RID: 15366
		private int whatz;

		// Token: 0x04003C07 RID: 15367
		private int whatx;

		// Token: 0x04003C08 RID: 15368
		private int index;

		// Token: 0x04003C09 RID: 15369
		private Vector3 normal = Vector3.Zero;

		// Token: 0x04003C0A RID: 15370
		private RenderTarget2D shadowTarget;

		// Token: 0x04003C0B RID: 15371
		private RenderTarget2D resolveTarget2;

		// Token: 0x04003C0C RID: 15372
		private RenderTarget2D resolveTarget1;

		// Token: 0x04003C0D RID: 15373
		private float LensX = 0.14f;

		// Token: 0x04003C0E RID: 15374
		private float LensZ = -0.03f;

		// Token: 0x04003C0F RID: 15375
		private float fader = 1f;

		// Token: 0x04003C10 RID: 15376
		private int myvarIndex;

		// Token: 0x04003C11 RID: 15377
		private int myvarIndex2;

		// Token: 0x04003C12 RID: 15378
		private int[] myVar = new int[] { 3000, 2, 10, 25, 2, 6, 3, 1 };

		// Token: 0x04003C13 RID: 15379
		private int[] myVar2 = new int[] { 1, 33, 98, 97 };

		// Token: 0x04003C14 RID: 15380
		private string[] myString = new string[] { "Base", "1st Bumps", "Valleys", "Mountains", "1st Dips", "2nd Bumps", "2nd Dips", "TextureDivide" };

		// Token: 0x04003C15 RID: 15381
		private string[] myString2 = new string[] { "Texture1", "Texture2", "Texture3", "Texture4" };

		// Token: 0x04003C16 RID: 15382
		private Vector3 ambient;

		// Token: 0x04003C17 RID: 15383
		private Vector3 diffuse;

		// Token: 0x04003C18 RID: 15384
		private Vector3 difDay = new Vector3(1f, 1f, 1f);

		// Token: 0x04003C19 RID: 15385
		private Vector3 ambDay = new Vector3(0.5f, 0.5f, 0.5f);

		// Token: 0x04003C1A RID: 15386
		private Vector3 difSol = new Vector3(2f, 2f, 2f);

		// Token: 0x04003C1B RID: 15387
		private Vector3 ambSol = new Vector3(0.18f, 0.18f, 0.18f);

		// Token: 0x04003C1C RID: 15388
		private Vector3 difSet = new Vector3(0.88f, 0.37f, -0.35f);

		// Token: 0x04003C1D RID: 15389
		private Vector3 ambSet = new Vector3(0.25f, 0.18f, 0.25f);

		// Token: 0x04003C1E RID: 15390
		private Vector3 ambNite = new Vector3(0.21f, 0.37f, 0.33f);

		// Token: 0x04003C1F RID: 15391
		private Vector3 difNite = new Vector3(0.12f, 0.19f, 0.31f);

		// Token: 0x04003C20 RID: 15392
		private int diffiz = 99;

		// Token: 0x04003C21 RID: 15393
		private int diffix = 99;

		// Token: 0x04003C22 RID: 15394
		private Vector2 errorCatch = new Vector2(0f, 0f);

		// Token: 0x04003C23 RID: 15395
		private Vector4 bigX = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04003C24 RID: 15396
		private Vector4 bigZ = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04003C25 RID: 15397
		private float terrainDivider = 1f;

		// Token: 0x04003C26 RID: 15398
		private Texture2D starSheet;

		// Token: 0x04003C27 RID: 15399
		private Plane wallPlane;

		// Token: 0x04003C28 RID: 15400
		private Vector3[] origRegion1 = new Vector3[]
		{
			new Vector3(-0.636f, 0.05f, -2.2f),
			new Vector3(0.643f, 0.05f, -2.2f),
			new Vector3(0.498f, 0f, -0.007f),
			new Vector3(-0.525f, 0f, -0.007f),
			new Vector3(0f, 0.05f, -1.5f)
		};

		// Token: 0x04003C29 RID: 15401
		private Vector3[] bucketRegion1 = new Vector3[5];

		// Token: 0x04003C2A RID: 15402
		private Matrix inDumper = Matrix.Identity;

		// Token: 0x04003C2B RID: 15403
		private int totalGems;

		// Token: 0x04003C2C RID: 15404
		private int gemOffset;

		// Token: 0x04003C2D RID: 15405
		private gemstruct shale1 = new gemstruct();

		// Token: 0x04003C2E RID: 15406
		private gemstruct shale2 = new gemstruct();

		// Token: 0x04003C2F RID: 15407
		private gemstruct shale3 = new gemstruct();

		// Token: 0x04003C30 RID: 15408
		private rockstruct rock = new rockstruct();

		// Token: 0x04003C31 RID: 15409
		private InstancedModel chain;

		// Token: 0x04003C32 RID: 15410
		private Matrix[] ropeTrans;

		// Token: 0x04003C33 RID: 15411
		private ropeDupe rope;

		// Token: 0x04003C34 RID: 15412
		private Vector2 gemPOS = new Vector2(0f, 0f);

		// Token: 0x04003C35 RID: 15413
		private Vector2 oldgemPOS = new Vector2(0f, 0f);

		// Token: 0x04003C36 RID: 15414
		private Vector2 oldgemPOS2 = new Vector2(0f, 0f);

		// Token: 0x04003C37 RID: 15415
		private float var1;

		// Token: 0x04003C38 RID: 15416
		private float var2;

		// Token: 0x04003C39 RID: 15417
		private float var3;

		// Token: 0x04003C3A RID: 15418
		private float vehicleDistance;

		// Token: 0x04003C3B RID: 15419
		private float dropshipDistance;

		// Token: 0x04003C3C RID: 15420
		private float faciltyDistance;

		// Token: 0x04003C3D RID: 15421
		private Model beacon;

		// Token: 0x04003C3E RID: 15422
		private buildStars starmap = new buildStars();

		// Token: 0x04003C3F RID: 15423
		private PresentationParameters pp;

		// Token: 0x04003C40 RID: 15424
		private int lastAlias;

		// Token: 0x04003C41 RID: 15425
		private float aspectratio;

		// Token: 0x04003C42 RID: 15426
		private ScreenManager sc;

		// Token: 0x04003C43 RID: 15427
		private int[] myradius = new int[] { 160, 200, 440, 770, 1500 };

		// Token: 0x04003C44 RID: 15428
		private Vector3 campos = new Vector3(975f, 250f, 1650f);

		// Token: 0x04003C45 RID: 15429
		private Vector3 aCampos = new Vector3(975f, 250f, 1650f);

		// Token: 0x04003C46 RID: 15430
		private float camhite = 350f;

		// Token: 0x04003C47 RID: 15431
		private Vector3 oldcampos = new Vector3(975f, 250f, 1650f);

		// Token: 0x04003C48 RID: 15432
		private bool jumping = true;

		// Token: 0x04003C49 RID: 15433
		private bool jumpCalled;

		// Token: 0x04003C4A RID: 15434
		private Vector3 camlookpos;

		// Token: 0x04003C4B RID: 15435
		private float camradian = 5.15f;

		// Token: 0x04003C4C RID: 15436
		private float camheight = 3.55f;

		// Token: 0x04003C4D RID: 15437
		private float oldcamheight = 4f;

		// Token: 0x04003C4E RID: 15438
		private int stepCount;

		// Token: 0x04003C4F RID: 15439
		private int stepFlag;

		// Token: 0x04003C50 RID: 15440
		private float bobber;

		// Token: 0x04003C51 RID: 15441
		private float mytimer;

		// Token: 0x04003C52 RID: 15442
		private bool iscamShake;

		// Token: 0x04003C53 RID: 15443
		private int camShakeTimer;

		// Token: 0x04003C54 RID: 15444
		private Vector3 camShake;

		// Token: 0x04003C55 RID: 15445
		public float groundHeight;

		// Token: 0x04003C56 RID: 15446
		private Vector3 vec;

		// Token: 0x04003C57 RID: 15447
		public Vector3 fallVec = Vector3.Zero;

		// Token: 0x04003C58 RID: 15448
		public Vector3 hitVec = Vector3.Zero;

		// Token: 0x04003C59 RID: 15449
		public float fallLim = 5f;

		// Token: 0x04003C5A RID: 15450
		private float slopereducer = 1f;

		// Token: 0x04003C5B RID: 15451
		public float fallPosition;

		// Token: 0x04003C5C RID: 15452
		public float fallGrav;

		// Token: 0x04003C5D RID: 15453
		public float gravLim = 4f;

		// Token: 0x04003C5E RID: 15454
		public float gravLimdown = -8f;

		// Token: 0x04003C5F RID: 15455
		public float fallAcc = -0.15f;

		// Token: 0x04003C60 RID: 15456
		private int jumpCount;

		// Token: 0x04003C61 RID: 15457
		private bool rejump;

		// Token: 0x04003C62 RID: 15458
		private float doubleJump;

		// Token: 0x04003C63 RID: 15459
		private float dist;

		// Token: 0x04003C64 RID: 15460
		private float olddist;

		// Token: 0x04003C65 RID: 15461
		private float walkspeed;

		// Token: 0x04003C66 RID: 15462
		private Texture2D blurdot;

		// Token: 0x04003C67 RID: 15463
		private GameplayScreen.hitStream hitstreamTemp = default(GameplayScreen.hitStream);

		// Token: 0x04003C68 RID: 15464
		private static VertexDeclaration vd2 = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
		});

		// Token: 0x04003C69 RID: 15465
		private GameplayScreen.hole prints;

		// Token: 0x04003C6A RID: 15466
		private Model footprint;

		// Token: 0x04003C6B RID: 15467
		private Texture2D normalmap;

		// Token: 0x04003C6C RID: 15468
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x04003C6D RID: 15469
		private MoonSurface.tex[] texdata;

		// Token: 0x04003C6E RID: 15470
		private int textflag;

		// Token: 0x04003C6F RID: 15471
		private float typewriterblank;

		// Token: 0x04003C70 RID: 15472
		private float typewriterwait = 500f;

		// Token: 0x04003C71 RID: 15473
		private float typeposition;

		// Token: 0x04003C72 RID: 15474
		private float typevertical = 110f;

		// Token: 0x04003C73 RID: 15475
		private int typewriterdelay = 3;

		// Token: 0x04003C74 RID: 15476
		private SoundEffect pop1;

		// Token: 0x04003C75 RID: 15477
		private float typewritercount;

		// Token: 0x04003C76 RID: 15478
		private string text = "";

		// Token: 0x04003C77 RID: 15479
		private SpriteFont typer;

		// Token: 0x04003C78 RID: 15480
		private float timer;

		// Token: 0x04003C79 RID: 15481
		private int objIndex;

		// Token: 0x04003C7A RID: 15482
		private int displayObjective = 650;

		// Token: 0x04003C7B RID: 15483
		private bool firstMess = true;

		// Token: 0x04003C7C RID: 15484
		private List<string> objectiveData = new List<string>();

		// Token: 0x0200018B RID: 395
		public enum act
		{
			// Token: 0x04003C7F RID: 15487
			rovered,
			// Token: 0x04003C80 RID: 15488
			landered,
			// Token: 0x04003C81 RID: 15489
			killed,
			// Token: 0x04003C82 RID: 15490
			rescued,
			// Token: 0x04003C83 RID: 15491
			recruited,
			// Token: 0x04003C84 RID: 15492
			facility,
			// Token: 0x04003C85 RID: 15493
			chill,
			// Token: 0x04003C86 RID: 15494
			rocked,
			// Token: 0x04003C87 RID: 15495
			refined,
			// Token: 0x04003C88 RID: 15496
			facilityfound,
			// Token: 0x04003C89 RID: 15497
			cloned,
			// Token: 0x04003C8A RID: 15498
			waited,
			// Token: 0x04003C8B RID: 15499
			dropship,
			// Token: 0x04003C8C RID: 15500
			nothing
		}

		// Token: 0x0200018C RID: 396
		public struct hitStream : IVertexType
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0041CD51 File Offset: 0x0041AF51
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return GameplayScreen.hitStream.VertexDeclaration;
				}
			}

			// Token: 0x04003C8D RID: 15501
			public Matrix Trans;

			// Token: 0x04003C8E RID: 15502
			public float Fade;

			// Token: 0x04003C8F RID: 15503
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
			});
		}

		// Token: 0x0200018D RID: 397
		public struct hole
		{
			// Token: 0x04003C90 RID: 15504
			public float[] drift;

			// Token: 0x04003C91 RID: 15505
			public GameplayScreen.hitStream[] stainTrans;

			// Token: 0x04003C92 RID: 15506
			public int stainIndex;

			// Token: 0x04003C93 RID: 15507
			public int stainMax;

			// Token: 0x04003C94 RID: 15508
			public int stainCapacity;

			// Token: 0x04003C95 RID: 15509
			public DynamicVertexBuffer stainBuffer;
		}
	}
}
