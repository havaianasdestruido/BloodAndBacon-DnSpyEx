using System;
using Steamworks;

namespace Blood
{
	// Token: 0x02000113 RID: 275
	public class achieve
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0025A768 File Offset: 0x00258968
		public achieve(ScreenManager sm)
		{
			if (SteamAPI.IsSteamRunning())
			{
				this.LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindResult));
				SteamAPICall_t steamAPICall_t = SteamUserStats.FindLeaderboard("pigskilled");
				this.LeaderboardFindResult.Set(steamAPICall_t, null);
				this.LeaderboardFindResult2 = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindResult2));
				SteamAPICall_t steamAPICall_t2 = SteamUserStats.FindLeaderboard("mostpigsgrinded");
				this.LeaderboardFindResult2.Set(steamAPICall_t2, null);
				this.LeaderboardFindResult3 = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindResult3));
				SteamAPICall_t steamAPICall_t3 = SteamUserStats.FindLeaderboard("milksdrank");
				this.LeaderboardFindResult3.Set(steamAPICall_t3, null);
				this.LeaderboardFindResult4 = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindResult4));
				SteamAPICall_t steamAPICall_t4 = SteamUserStats.FindLeaderboard("mostko");
				this.LeaderboardFindResult4.Set(steamAPICall_t4, null);
			}
			this.getStat("stat_pigskilled", ref this.pigskilled);
			this.getStat("stat_mostpigsgrinded", ref this.mostpigsgrinded);
			this.getStat("stat_milksdrank", ref this.milksdrank);
			this.getStat("stat_mostko", ref this.mostko);
			this.sc = sm;
			this.init();
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0025A894 File Offset: 0x00258A94
		public void init()
		{
			this.kaboom = new achieve.ach();
			this.initAchievement(this.kaboom, "kaboom", true, 10);
			this.whileyourdown = new achieve.ach();
			this.initAchievement(this.whileyourdown, "whileyourdown", true, 10);
			this.youmurderer = new achieve.ach();
			this.initAchievement(this.youmurderer, "youmurderer", true, 100);
			this.hulk = new achieve.ach();
			this.initAchievement(this.hulk, "hulk", true, 5);
			this.skidmarked = new achieve.ach();
			this.initAchievement(this.skidmarked, "skidmarked", false, 0);
			this.knockout = new achieve.ach();
			this.initAchievement(this.knockout, "knockout", true, 10);
			this.bitethehand = new achieve.ach();
			this.initAchievement(this.bitethehand, "bitethehand", true, 10);
			this.gasguzzler = new achieve.ach();
			this.initAchievement(this.gasguzzler, "gasguzzler", false, 0);
			this.ambidextrous = new achieve.ach();
			this.initAchievement(this.ambidextrous, "ambidextrous", false, 0);
			this.needygreedy = new achieve.ach();
			this.initAchievement(this.needygreedy, "needygreedy", false, 0);
			this.royaltykiller = new achieve.ach();
			this.initAchievement(this.royaltykiller, "royaltykiller", false, 0);
			this.pyromaniac = new achieve.ach();
			this.initAchievement(this.pyromaniac, "pyromaniac", false, 0);
			this.fryertuck = new achieve.ach();
			this.initAchievement(this.fryertuck, "fryertuck", true, 10);
			this.chunkkicker = new achieve.ach();
			this.initAchievement(this.chunkkicker, "chunkkicker", true, 50);
			this.acidwashed = new achieve.ach();
			this.initAchievement(this.acidwashed, "acidwashed", false, 0);
			this.credits = new achieve.ach();
			this.initAchievement(this.credits, "credits", false, 0);
			this.graphics = new achieve.ach();
			this.initAchievement(this.graphics, "graphics", false, 0);
			this.blindedbythelight = new achieve.ach();
			this.initAchievement(this.blindedbythelight, "blindedbythelight", false, 0);
			this.thelongestyard = new achieve.ach();
			this.initAchievement(this.thelongestyard, "thelongestyard", false, 0);
			this.youarethechampion = new achieve.ach();
			this.initAchievement(this.youarethechampion, "youarethechampion", false, 0);
			this.aptpupil = new achieve.ach();
			this.initAchievement(this.aptpupil, "aptpupil", false, 0);
			this.milehighclub = new achieve.ach();
			this.initAchievement(this.milehighclub, "milehighclub", false, 0);
			this.frankenstein = new achieve.ach();
			this.initAchievement(this.frankenstein, "frankenstein", true, 10);
			this.bounce = new achieve.ach();
			this.initAchievement(this.bounce, "bounce", true, 20);
			this.swallow = new achieve.ach();
			this.initAchievement(this.swallow, "swallow", true, 20);
			this.coward = new achieve.ach();
			this.initAchievement(this.coward, "coward", false, 0);
			this.ridethewave = new achieve.ach();
			this.initAchievement(this.ridethewave, "ridethewave", false, 0);
			this.offkey = new achieve.ach();
			this.initAchievement(this.offkey, "offkey", false, 0);
			this.birds = new achieve.ach();
			this.initAchievement(this.birds, "birds", false, 0);
			this.hardened = new achieve.ach();
			this.initAchievement(this.hardened, "hardened", false, 0);
			this.death = new achieve.ach();
			this.initAchievement(this.death, "death", false, 0);
			this.madhatter = new achieve.ach();
			this.initAchievement(this.madhatter, "madhatter", false, 0);
			this.drwho = new achieve.ach();
			this.initAchievement(this.drwho, "drwho", true, 10);
			this.hindenburg = new achieve.ach();
			this.initAchievement(this.hindenburg, "hindenburg", false, 0);
			this.redsun = new achieve.ach();
			this.initAchievement(this.redsun, "redsun", false, 0);
			this.unfriended = new achieve.ach();
			this.initAchievement(this.unfriended, "unfriended", true, 10);
			this.walkinghere = new achieve.ach();
			this.initAchievement(this.walkinghere, "walkinghere", false, 0);
			this.spacedeath = new achieve.ach();
			this.initAchievement(this.spacedeath, "spacedeath", false, 0);
			this.spaceevent = new achieve.ach();
			this.initAchievement(this.spaceevent, "spaceevent", false, 0);
			this.heirlooms = new achieve.ach();
			this.initAchievement(this.heirlooms, "heirlooms", false, 0);
			this.mrgreen = new achieve.ach();
			this.initAchievement(this.mrgreen, "mrgreen", false, 0);
			this.freefall = new achieve.ach();
			this.initAchievement(this.freefall, "freefall", false, 0);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0025AD9C File Offset: 0x00258F9C
		private void initAchievement(achieve.ach ach, string name, bool useStat, int val)
		{
			ach.wait = false;
			ach.statreached = val;
			ach.useStat = useStat;
			ach.name = name;
			ach.statname = "stat_" + name;
			if (useStat)
			{
				ach.stat = 0;
				this.getStat(ach.statname, ref ach.stat);
			}
			ach.trophywon = false;
			this.getachStatus(ach.name, ref ach.trophywon);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0025AE0C File Offset: 0x0025900C
		public void checkAll()
		{
			this.allcomplete = true;
			this.checkAchievement(this.kaboom, "kaboom", true, 10);
			if (!this.kaboom.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.whileyourdown, "whileyourdown", true, 10);
			if (!this.whileyourdown.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.youmurderer, "youmurderer", true, 100);
			if (!this.youmurderer.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.hulk, "hulk", true, 5);
			if (!this.hulk.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.skidmarked, "skidmarked", false, 0);
			if (!this.skidmarked.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.knockout, "knockout", true, 10);
			if (!this.knockout.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.bitethehand, "bitethehand", true, 10);
			if (!this.bitethehand.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.gasguzzler, "gasguzzler", false, 0);
			if (!this.gasguzzler.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.ambidextrous, "ambidextrous", false, 0);
			if (!this.ambidextrous.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.needygreedy, "needygreedy", false, 0);
			if (!this.needygreedy.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.royaltykiller, "royaltykiller", false, 0);
			if (!this.royaltykiller.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.pyromaniac, "pyromaniac", false, 0);
			if (!this.pyromaniac.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.fryertuck, "fryertuck", true, 10);
			if (!this.fryertuck.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.chunkkicker, "chunkkicker", true, 50);
			if (!this.chunkkicker.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.acidwashed, "acidwashed", false, 0);
			if (!this.acidwashed.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.credits, "credits", false, 0);
			if (!this.credits.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.graphics, "graphics", false, 0);
			if (!this.graphics.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.blindedbythelight, "blindedbythelight", false, 0);
			if (!this.blindedbythelight.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.thelongestyard, "thelongestyard", false, 0);
			if (!this.thelongestyard.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.youarethechampion, "youarethechampion", false, 0);
			if (!this.youarethechampion.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.aptpupil, "aptpupil", false, 0);
			if (!this.aptpupil.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.milehighclub, "milehighclub", false, 0);
			if (!this.milehighclub.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.frankenstein, "frankenstein", true, 10);
			if (!this.frankenstein.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.bounce, "bounce", true, 20);
			if (!this.bounce.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.swallow, "swallow", true, 20);
			if (!this.youmurderer.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.coward, "coward", false, 0);
			if (!this.coward.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.ridethewave, "ridethewave", false, 0);
			if (!this.ridethewave.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.offkey, "offkey", false, 0);
			if (!this.offkey.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.birds, "birds", false, 0);
			if (!this.birds.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.hardened, "hardened", false, 0);
			if (!this.hardened.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.death, "death", false, 0);
			if (!this.death.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.madhatter, "madhatter", false, 0);
			if (!this.madhatter.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.drwho, "drwho", true, 10);
			if (!this.drwho.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.hindenburg, "hindenburg", false, 0);
			if (!this.hindenburg.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.redsun, "redsun", false, 0);
			if (!this.redsun.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.unfriended, "unfriended", true, 10);
			if (!this.unfriended.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.walkinghere, "walkinghere", false, 0);
			if (!this.walkinghere.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.spacedeath, "spacedeath", false, 0);
			if (!this.spacedeath.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.spaceevent, "spaceevent", false, 0);
			if (!this.spaceevent.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.heirlooms, "heirloom", false, 0);
			if (!this.heirlooms.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.mrgreen, "mrgreen", false, 0);
			if (!this.mrgreen.trophywon)
			{
				this.allcomplete = false;
			}
			this.checkAchievement(this.freefall, "freefall", false, 0);
			if (!this.freefall.trophywon)
			{
				this.allcomplete = false;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0025B492 File Offset: 0x00259692
		private void checkAchievement(achieve.ach ach, string name, bool useStat, int val)
		{
			if (!ach.trophywon)
			{
				this.getachStatus(ach.name, ref ach.trophywon);
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0025B4AE File Offset: 0x002596AE
		public void leaderPigsKilled()
		{
			if (this.pigskilled == 0)
			{
				this.getStat("stat_pigskilled", ref this.pigskilled);
				return;
			}
			this.pigskilled++;
			this.setStat("stat_pigskilled", this.pigskilled);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0025B4E9 File Offset: 0x002596E9
		public void leaderPigsGrinded()
		{
			if (this.mostpigsgrinded == 0)
			{
				this.getStat("stat_mostpigsgrinded", ref this.mostpigsgrinded);
				return;
			}
			this.mostpigsgrinded++;
			this.setStat("stat_mostpigsgrinded", this.mostpigsgrinded);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0025B524 File Offset: 0x00259724
		public void leaderMostKO()
		{
			if (this.mostko == 0)
			{
				this.getStat("stat_mostko", ref this.mostko);
				return;
			}
			this.mostko++;
			this.setStat("stat_mostko", this.mostko);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0025B55F File Offset: 0x0025975F
		public void leaderMostMilkDrank()
		{
			if (this.milksdrank == 0)
			{
				this.getStat("stat_milksdrank", ref this.milksdrank);
				return;
			}
			this.milksdrank++;
			this.setStat("stat_milksdrank", this.milksdrank);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0025B59C File Offset: 0x0025979C
		public void updateLeaderboards()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			try
			{
				SteamUserStats.UploadLeaderboardScore(this.m_SteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.pigskilled, null, 0);
			}
			catch
			{
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0025B5DC File Offset: 0x002597DC
		public void updateLeaderboards2()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			try
			{
				SteamUserStats.UploadLeaderboardScore(this.m_SteamLeaderboard2, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.mostpigsgrinded, null, 0);
			}
			catch
			{
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0025B61C File Offset: 0x0025981C
		public void updateLeaderboards3()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			try
			{
				SteamUserStats.UploadLeaderboardScore(this.m_SteamLeaderboard3, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.milksdrank, null, 0);
			}
			catch
			{
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0025B65C File Offset: 0x0025985C
		public void updateLeaderboards4()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			try
			{
				SteamUserStats.UploadLeaderboardScore(this.m_SteamLeaderboard4, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.mostko, null, 0);
			}
			catch
			{
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0025B69C File Offset: 0x0025989C
		private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_bLeaderboardFound != 0)
			{
				this.m_SteamLeaderboard = pCallback.m_hSteamLeaderboard;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0025B6B4 File Offset: 0x002598B4
		private void OnLeaderboardFindResult2(LeaderboardFindResult_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_bLeaderboardFound != 0)
			{
				this.m_SteamLeaderboard2 = pCallback.m_hSteamLeaderboard;
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0025B6CC File Offset: 0x002598CC
		private void OnLeaderboardFindResult3(LeaderboardFindResult_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_bLeaderboardFound != 0)
			{
				this.m_SteamLeaderboard3 = pCallback.m_hSteamLeaderboard;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0025B6E4 File Offset: 0x002598E4
		private void OnLeaderboardFindResult4(LeaderboardFindResult_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_bLeaderboardFound != 0)
			{
				this.m_SteamLeaderboard4 = pCallback.m_hSteamLeaderboard;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0025B6FC File Offset: 0x002598FC
		public void UpdateState()
		{
			this.myframe++;
			if (this.myframe % 180 == 0)
			{
				this.kaboom.wait = false;
				this.whileyourdown.wait = false;
				this.youmurderer.wait = false;
				this.hulk.wait = false;
				this.skidmarked.wait = false;
				this.knockout.wait = false;
				this.bitethehand.wait = false;
				this.ambidextrous.wait = false;
				this.gasguzzler.wait = false;
				this.needygreedy.wait = false;
				this.royaltykiller.wait = false;
				this.pyromaniac.wait = false;
				this.fryertuck.wait = false;
				this.chunkkicker.wait = false;
				this.acidwashed.wait = false;
				this.credits.wait = false;
				this.graphics.wait = false;
				this.youarethechampion.wait = false;
				this.thelongestyard.wait = false;
				this.blindedbythelight.wait = false;
				this.aptpupil.wait = false;
				this.milehighclub.wait = false;
				this.frankenstein.wait = false;
				this.bounce.wait = false;
				this.swallow.wait = false;
				this.coward.wait = false;
				this.ridethewave.wait = false;
				this.offkey.wait = false;
				this.birds.wait = false;
				this.hardened.wait = false;
				this.death.wait = false;
				this.madhatter.wait = false;
				this.drwho.wait = false;
				this.hindenburg.wait = false;
				this.redsun.wait = false;
				this.unfriended.wait = false;
				this.spacedeath.wait = false;
				this.spaceevent.wait = false;
				this.walkinghere.wait = false;
				this.heirlooms.wait = false;
				this.mrgreen.wait = false;
				this.freefall.wait = false;
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0025B920 File Offset: 0x00259B20
		public void win(achieve.ach ach)
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			if (ach.wait)
			{
				return;
			}
			ach.wait = true;
			if (ach.useStat)
			{
				this.getStat(ach.statname, ref ach.stat);
				ach.stat++;
				this.setStat(ach.statname, ach.stat);
				if (ach.stat >= ach.statreached)
				{
					if (!ach.trophywon)
					{
						this.sc.achieve1.Play(this.sc.ev, 0f, 0f);
					}
					this.setAchievement(ach.name);
					ach.trophywon = true;
					return;
				}
			}
			else
			{
				if (!ach.trophywon)
				{
					this.sc.achieve1.Play(this.sc.ev, 0f, 0f);
				}
				this.setAchievement(ach.name);
				ach.trophywon = true;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0025BA18 File Offset: 0x00259C18
		private void getachStatus(string name, ref bool stat)
		{
			if (SteamAPI.IsSteamRunning())
			{
				bool flag = false;
				try
				{
					if (SteamUserStats.GetAchievement(name, out flag))
					{
						stat = flag;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0025BA50 File Offset: 0x00259C50
		private void getStat(string name, ref int stat)
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			SteamUserStats.GetStat(name, out stat);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0025BA62 File Offset: 0x00259C62
		private void setStat(string name, int stat)
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			SteamUserStats.SetStat(name, stat);
			SteamUserStats.StoreStats();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0025BA7A File Offset: 0x00259C7A
		private void setAchievement(string AchName)
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			SteamUserStats.SetAchievement(AchName);
			SteamUserStats.StoreStats();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0025BA91 File Offset: 0x00259C91
		private void requestStats()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			SteamUserStats.RequestCurrentStats();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0025BAA1 File Offset: 0x00259CA1
		public void resetStats()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			SteamUserStats.ResetAllStats(true);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0025BAB2 File Offset: 0x00259CB2
		private void setAchievement2(achieve.ach ac, string AchName, bool what, int num)
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			SteamUserStats.SetAchievement(AchName);
			SteamUserStats.StoreStats();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0025BACC File Offset: 0x00259CCC
		public void gimme()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			this.setAchievement2(this.kaboom, "kaboom", true, 10);
			this.setAchievement2(this.whileyourdown, "whileyourdown", true, 10);
			this.setAchievement2(this.youmurderer, "youmurderer", true, 100);
			this.setAchievement2(this.hulk, "hulk", true, 5);
			this.setAchievement2(this.skidmarked, "skidmarked", false, 0);
			this.setAchievement2(this.knockout, "knockout", true, 10);
			this.setAchievement2(this.bitethehand, "bitethehand", true, 10);
			this.setAchievement2(this.gasguzzler, "gasguzzler", false, 0);
			this.setAchievement2(this.ambidextrous, "ambidextrous", false, 0);
			this.setAchievement2(this.needygreedy, "needygreedy", false, 0);
			this.setAchievement2(this.royaltykiller, "royaltykiller", false, 0);
			this.setAchievement2(this.pyromaniac, "pyromaniac", false, 0);
			this.setAchievement2(this.fryertuck, "fryertuck", true, 10);
			this.setAchievement2(this.chunkkicker, "chunkkicker", true, 50);
			this.setAchievement2(this.acidwashed, "acidwashed", false, 0);
			this.setAchievement2(this.credits, "credits", false, 0);
			this.setAchievement2(this.graphics, "graphics", false, 0);
			this.setAchievement2(this.blindedbythelight, "blindedbythelight", false, 0);
			this.setAchievement2(this.thelongestyard, "thelongestyard", false, 0);
			this.setAchievement2(this.youarethechampion, "youarethechampion", false, 0);
			this.setAchievement2(this.aptpupil, "aptpupil", false, 0);
			this.setAchievement2(this.milehighclub, "milehighclub", false, 0);
			this.setAchievement2(this.frankenstein, "frankenstein", true, 10);
			this.setAchievement2(this.bounce, "bounce", true, 20);
			this.setAchievement2(this.swallow, "swallow", true, 20);
			this.setAchievement2(this.coward, "coward", false, 0);
			this.setAchievement2(this.ridethewave, "ridethewave", false, 0);
			this.setAchievement2(this.offkey, "offkey", false, 0);
			this.setAchievement2(this.birds, "birds", false, 0);
			this.setAchievement2(this.hardened, "hardened", false, 0);
			this.setAchievement2(this.death, "death", false, 0);
			this.setAchievement2(this.madhatter, "madhatter", false, 0);
			this.setAchievement2(this.drwho, "drwho", true, 10);
			this.setAchievement2(this.hindenburg, "hindenburg", false, 0);
			this.setAchievement2(this.redsun, "redsun", false, 0);
			this.setAchievement2(this.unfriended, "unfriended", true, 10);
			this.setAchievement2(this.spacedeath, "spacedeath", false, 0);
			this.setAchievement2(this.spaceevent, "spaceevent", false, 0);
			this.setAchievement2(this.walkinghere, "walkinghere", false, 0);
			this.setAchievement2(this.heirlooms, "heirlooms", false, 0);
			this.setAchievement2(this.mrgreen, "mrgreen", false, 0);
			this.setAchievement2(this.freefall, "freefall", false, 0);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0025BE0C File Offset: 0x0025A00C
		public void gimmeSpace()
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return;
			}
			this.setAchievement2(this.spacedeath, "spacedeath", false, 0);
			this.setAchievement2(this.spaceevent, "spaceevent", false, 0);
			this.setAchievement2(this.walkinghere, "walkinghere", false, 0);
		}

		// Token: 0x0400281B RID: 10267
		public bool allcomplete;

		// Token: 0x0400281C RID: 10268
		private SteamLeaderboard_t m_SteamLeaderboard;

		// Token: 0x0400281D RID: 10269
		public CallResult<LeaderboardFindResult_t> LeaderboardFindResult;

		// Token: 0x0400281E RID: 10270
		private SteamLeaderboard_t m_SteamLeaderboard2;

		// Token: 0x0400281F RID: 10271
		public CallResult<LeaderboardFindResult_t> LeaderboardFindResult2;

		// Token: 0x04002820 RID: 10272
		private SteamLeaderboard_t m_SteamLeaderboard3;

		// Token: 0x04002821 RID: 10273
		public CallResult<LeaderboardFindResult_t> LeaderboardFindResult3;

		// Token: 0x04002822 RID: 10274
		private SteamLeaderboard_t m_SteamLeaderboard4;

		// Token: 0x04002823 RID: 10275
		public CallResult<LeaderboardFindResult_t> LeaderboardFindResult4;

		// Token: 0x04002824 RID: 10276
		private int myframe;

		// Token: 0x04002825 RID: 10277
		private ScreenManager sc;

		// Token: 0x04002826 RID: 10278
		public achieve.ach kaboom;

		// Token: 0x04002827 RID: 10279
		public achieve.ach whileyourdown;

		// Token: 0x04002828 RID: 10280
		public achieve.ach youmurderer;

		// Token: 0x04002829 RID: 10281
		public achieve.ach hulk;

		// Token: 0x0400282A RID: 10282
		public achieve.ach skidmarked;

		// Token: 0x0400282B RID: 10283
		public achieve.ach knockout;

		// Token: 0x0400282C RID: 10284
		public achieve.ach bitethehand;

		// Token: 0x0400282D RID: 10285
		public achieve.ach gasguzzler;

		// Token: 0x0400282E RID: 10286
		public achieve.ach ambidextrous;

		// Token: 0x0400282F RID: 10287
		public achieve.ach needygreedy;

		// Token: 0x04002830 RID: 10288
		public achieve.ach royaltykiller;

		// Token: 0x04002831 RID: 10289
		public achieve.ach pyromaniac;

		// Token: 0x04002832 RID: 10290
		public achieve.ach fryertuck;

		// Token: 0x04002833 RID: 10291
		public achieve.ach chunkkicker;

		// Token: 0x04002834 RID: 10292
		public achieve.ach acidwashed;

		// Token: 0x04002835 RID: 10293
		public achieve.ach credits;

		// Token: 0x04002836 RID: 10294
		public achieve.ach graphics;

		// Token: 0x04002837 RID: 10295
		public achieve.ach blindedbythelight;

		// Token: 0x04002838 RID: 10296
		public achieve.ach thelongestyard;

		// Token: 0x04002839 RID: 10297
		public achieve.ach youarethechampion;

		// Token: 0x0400283A RID: 10298
		public achieve.ach aptpupil;

		// Token: 0x0400283B RID: 10299
		public achieve.ach milehighclub;

		// Token: 0x0400283C RID: 10300
		public achieve.ach frankenstein;

		// Token: 0x0400283D RID: 10301
		public achieve.ach bounce;

		// Token: 0x0400283E RID: 10302
		public achieve.ach swallow;

		// Token: 0x0400283F RID: 10303
		public achieve.ach coward;

		// Token: 0x04002840 RID: 10304
		public achieve.ach ridethewave;

		// Token: 0x04002841 RID: 10305
		public achieve.ach offkey;

		// Token: 0x04002842 RID: 10306
		public achieve.ach birds;

		// Token: 0x04002843 RID: 10307
		public achieve.ach hardened;

		// Token: 0x04002844 RID: 10308
		public achieve.ach death;

		// Token: 0x04002845 RID: 10309
		public achieve.ach madhatter;

		// Token: 0x04002846 RID: 10310
		public achieve.ach drwho;

		// Token: 0x04002847 RID: 10311
		public achieve.ach hindenburg;

		// Token: 0x04002848 RID: 10312
		public achieve.ach redsun;

		// Token: 0x04002849 RID: 10313
		public achieve.ach unfriended;

		// Token: 0x0400284A RID: 10314
		public achieve.ach walkinghere;

		// Token: 0x0400284B RID: 10315
		public achieve.ach spacedeath;

		// Token: 0x0400284C RID: 10316
		public achieve.ach spaceevent;

		// Token: 0x0400284D RID: 10317
		public achieve.ach heirlooms;

		// Token: 0x0400284E RID: 10318
		public achieve.ach mrgreen;

		// Token: 0x0400284F RID: 10319
		public achieve.ach freefall;

		// Token: 0x04002850 RID: 10320
		private int pigskilled;

		// Token: 0x04002851 RID: 10321
		private int mostpigsgrinded;

		// Token: 0x04002852 RID: 10322
		private int milksdrank;

		// Token: 0x04002853 RID: 10323
		private int mostko;

		// Token: 0x02000114 RID: 276
		public class ach
		{
			// Token: 0x04002854 RID: 10324
			public bool wait;

			// Token: 0x04002855 RID: 10325
			public bool useStat;

			// Token: 0x04002856 RID: 10326
			public int statreached;

			// Token: 0x04002857 RID: 10327
			public string name = "null";

			// Token: 0x04002858 RID: 10328
			public bool trophywon;

			// Token: 0x04002859 RID: 10329
			public string statname = "null";

			// Token: 0x0400285A RID: 10330
			public int stat;
		}
	}
}
