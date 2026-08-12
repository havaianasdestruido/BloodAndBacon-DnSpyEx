using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Steamworks;

namespace Blood
{
	// Token: 0x02000069 RID: 105
	public class Lobby
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x000E282C File Offset: 0x000E0A2C
		public Lobby(ScreenManager screenmanager, SoundEffect noise)
		{
			this.signal = noise;
			this.sc = screenmanager;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000E2950 File Offset: 0x000E0B50
		public void acceptRequests()
		{
			if (SteamAPI.IsSteamRunning())
			{
				this.inviteRequest = false;
				this.lobbyJoinRequest = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.onJoinRequestResult));
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000E2978 File Offset: 0x000E0B78
		public void onJoinRequestResult(GameLobbyJoinRequested_t pCallback)
		{
			if (this.inviteRequest)
			{
				return;
			}
			if (SteamAPI.IsSteamRunning())
			{
				this.inviteLobbyID = pCallback.m_steamIDLobby;
				this.inviteFriendID = pCallback.m_steamIDFriend;
				this.inviteRequest = true;
				this.inviteName = "NO";
				try
				{
					string friendPersonaName = SteamFriends.GetFriendPersonaName(this.inviteFriendID);
					char[] array = friendPersonaName.ToCharArray();
					this.inviteName = "";
					for (int i = 0; i < array.Length; i++)
					{
						if (!this.sc.font2.Characters.Contains(array[i]))
						{
							array[i] = '*';
						}
						this.inviteName += array[i].ToString();
					}
				}
				catch
				{
				}
				if (this.inviteName == "")
				{
					this.inviteName = "NO";
				}
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000E2A60 File Offset: 0x000E0C60
		public void getFriendsLobby()
		{
			int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
			for (int i = 0; i < friendCount; i++)
			{
				CSteamID friendByIndex = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
				FriendGameInfo_t friendGameInfo_t;
				if (SteamFriends.GetFriendGamePlayed(friendByIndex, out friendGameInfo_t) && friendGameInfo_t.m_steamIDLobby.IsValid() && friendGameInfo_t.m_gameID.AppID().m_AppId == 434570U)
				{
					CSteamID steamIDLobby = friendGameInfo_t.m_steamIDLobby;
					string friendPersonaName = SteamFriends.GetFriendPersonaName(friendByIndex);
					string lobbyData = SteamMatchmaking.GetLobbyData(steamIDLobby, "state");
					string lobbyData2 = SteamMatchmaking.GetLobbyData(steamIDLobby, "players");
					char[] array = friendPersonaName.ToCharArray();
					string text = "";
					try
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (!this.sc.font2.Characters.Contains(array[j]))
							{
								array[j] = '*';
							}
							text += array[j].ToString();
						}
					}
					catch
					{
					}
					if (text == "")
					{
						text = "waiting";
					}
					Lobby.lobbyowner lobbyowner = new Lobby.lobbyowner();
					lobbyowner.id = steamIDLobby;
					lobbyowner.name = text;
					lobbyowner.state = lobbyData;
					bool flag = true;
					for (int k = 0; k < this.lobbys.Count; k++)
					{
						if (lobbyowner.id == this.lobbys[k].id)
						{
							flag = false;
							break;
						}
					}
					if (flag && lobbyData2 == this.friendnum)
					{
						this.lobbys.Add(lobbyowner);
					}
				}
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000E2C00 File Offset: 0x000E0E00
		private void onLobbyRequestResult(LobbyMatchList_t pCallback, bool fails)
		{
			uint nLobbiesMatching = pCallback.m_nLobbiesMatching;
			this.lobbys.Clear();
			if (nLobbiesMatching > 0U)
			{
				int num = 0;
				while ((long)num < (long)((ulong)nLobbiesMatching))
				{
					CSteamID lobbyByIndex = SteamMatchmaking.GetLobbyByIndex(num);
					string lobbyData = SteamMatchmaking.GetLobbyData(lobbyByIndex, "name");
					string lobbyData2 = SteamMatchmaking.GetLobbyData(lobbyByIndex, "state");
					char[] array = lobbyData.ToCharArray();
					string text = "";
					try
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (!this.sc.font2.Characters.Contains(array[i]))
							{
								array[i] = '*';
							}
							text += array[i].ToString();
						}
					}
					catch
					{
					}
					if (text == "")
					{
						text = "waiting";
					}
					Lobby.lobbyowner lobbyowner = new Lobby.lobbyowner();
					lobbyowner.id = lobbyByIndex;
					lobbyowner.name = text;
					lobbyowner.state = lobbyData2;
					this.lobbys.Add(lobbyowner);
					num++;
				}
			}
			this.sc.lobby.getFriendsLobby();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000E2D20 File Offset: 0x000E0F20
		public void requestLobby(string num)
		{
			if (SteamAPI.IsSteamRunning())
			{
				this.friendnum = num;
				SteamMatchmaking.AddRequestLobbyListStringFilter("players", num, ELobbyComparison.k_ELobbyComparisonEqual);
				this.lobbyRequestResult = CallResult<LobbyMatchList_t>.Create(new CallResult<LobbyMatchList_t>.APIDispatchDelegate(this.onLobbyRequestResult));
				SteamMatchmaking.AddRequestLobbyListResultCountFilter(50);
				SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide);
				SteamAPICall_t steamAPICall_t = SteamMatchmaking.RequestLobbyList();
				this.lobbyRequestResult.Set(steamAPICall_t, null);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000E2D80 File Offset: 0x000E0F80
		private void onLobbyEnterResult(LobbyEnter_t pCallback, bool failed)
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (pCallback.m_EChatRoomEnterResponse == 1U)
				{
					CSteamID csteamID = new CSteamID(pCallback.m_ulSteamIDLobby);
					this.lobbyplayers = SteamMatchmaking.GetLobbyData(csteamID, "players");
					this.joinedLobby.Add(csteamID);
					this.inLobby = true;
					return;
				}
				CSteamID csteamID2 = default(CSteamID);
				this.lobbyplayers = "0";
				this.joinedLobby.Add(csteamID2);
				this.inLobby = false;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000E2DF8 File Offset: 0x000E0FF8
		public int joinLobby(CSteamID lobbyid)
		{
			if (!SteamAPI.IsSteamRunning())
			{
				return 0;
			}
			if (this.createdLobby.Contains(lobbyid))
			{
				return 0;
			}
			if (this.createdLobby.Count > 0)
			{
				this.uncreateLobby();
			}
			if (!this.joinedLobby.Contains(lobbyid))
			{
				if (this.joinedLobby.Count > 0)
				{
					this.leaveLobby();
				}
				this.lobbyEnteredResult = CallResult<LobbyEnter_t>.Create(new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.onLobbyEnterResult));
				SteamAPICall_t steamAPICall_t = SteamMatchmaking.JoinLobby(lobbyid);
				this.lobbyEnteredResult.Set(steamAPICall_t, null);
				this.inLobby = false;
				this.lobbyChar = "";
				this.lobbyDay = "wait";
				this.lobbyState = "";
				this.lobbyVersion = "";
				this.lobbyPassword = "";
				return 200;
			}
			int num = 0;
			if (SteamMatchmaking.GetLobbyData(lobbyid, "state") == "10")
			{
				string lobbyData = SteamMatchmaking.GetLobbyData(lobbyid, "version");
				bool flag = this.lobbyplayers == "2" && lobbyData == this.versionNumber.ToString();
				bool flag2 = this.lobbyplayers == "4" && lobbyData == this.version4Player.ToString();
				bool flag3 = this.lobbyplayers == "6" && lobbyData == this.version6Player.ToString();
				bool flag4 = this.lobbyplayers == "4" && lobbyData == this.version4Tunnel.ToString();
				bool flag5 = this.lobbyplayers == "3" && SteamMatchmaking.GetLobbyData(lobbyid, "day") == this.paintVersion.ToString();
				if (flag4 || flag5 || flag || flag2 || flag3)
				{
					string text = SteamMatchmaking.GetLobbyData(lobbyid, "day");
					if (flag5)
					{
						text = "1";
					}
					try
					{
						if (text != "")
						{
							num = Convert.ToInt32(text);
						}
					}
					catch (OverflowException)
					{
						num = 0;
					}
					catch (FormatException)
					{
						num = 0;
					}
				}
			}
			return num;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000E302C File Offset: 0x000E122C
		private void onLobbyCreatedResult(LobbyCreated_t pCallback, bool fails)
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (pCallback.m_eResult == EResult.k_EResultOK)
				{
					this.ver = this.versionNumber.ToString();
					if (this.players == "6")
					{
						this.ver = this.version6Player.ToString();
					}
					if (this.players == "4")
					{
						this.ver = this.version4Player.ToString();
					}
					if (this.players == "44")
					{
						this.ver = this.version4Tunnel.ToString();
						this.players = "4";
					}
					CSteamID csteamID = (CSteamID)pCallback.m_ulSteamIDLobby;
					string personaName = SteamFriends.GetPersonaName();
					SteamMatchmaking.SetLobbyData(csteamID, "name", personaName);
					SteamMatchmaking.SetLobbyData(csteamID, "state", "0");
					SteamMatchmaking.SetLobbyData(csteamID, "day", "0");
					SteamMatchmaking.SetLobbyData(csteamID, "char", "11");
					SteamMatchmaking.SetLobbyData(csteamID, "nickname", this.nickname);
					SteamMatchmaking.SetLobbyData(csteamID, "version", this.ver);
					SteamMatchmaking.SetLobbyData(csteamID, "password", this.password);
					SteamMatchmaking.SetLobbyData(csteamID, "players", this.players);
					if (!this.createdLobby.Contains(csteamID))
					{
						this.createdLobby.Add(csteamID);
						this.sc.errorMessage = "lobby created";
						this.sc.errorMessageTimer = 150;
					}
					else
					{
						this.sc.errorMessage = "lobby null";
						this.sc.errorMessageTimer = 150;
					}
					this.ALLClear = true;
				}
				else
				{
					this.sc.errorMessage = "lobby failed";
					this.sc.errorMessageTimer = 150;
				}
				this.ALLClear = true;
				return;
			}
			this.sc.errorMessage = "no lobby no steam";
			this.sc.errorMessageTimer = 150;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000E321D File Offset: 0x000E141D
		public void setPassword(CSteamID lobby)
		{
			if (SteamAPI.IsSteamRunning())
			{
				SteamMatchmaking.SetLobbyData(lobby, "password", this.password);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000E3238 File Offset: 0x000E1438
		public void createLobby(int num)
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (this.createdLobby.Count > 0)
				{
					this.uncreateLobby();
				}
				if (this.joinedLobby.Count > 0)
				{
					this.leaveLobby();
				}
				this.players = num.ToString();
				if (num == 44)
				{
					num = 4;
				}
				this.lobbyCreatedResult = CallResult<LobbyCreated_t>.Create(new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.onLobbyCreatedResult));
				SteamAPICall_t steamAPICall_t = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, num);
				this.lobbyCreatedResult.Set(steamAPICall_t, null);
				this.ALLClear = false;
				this.sc.errorMessage = "request sent waiting...";
				this.sc.errorMessageTimer = 700;
				return;
			}
			this.sc.errorMessage = "request failed";
			this.sc.errorMessageTimer = 150;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000E3300 File Offset: 0x000E1500
		public void leaveLobby()
		{
			if (SteamAPI.IsSteamRunning())
			{
				for (int i = 0; i < this.joinedLobby.Count; i++)
				{
					SteamMatchmaking.LeaveLobby(this.joinedLobby[i]);
				}
				this.joinedLobby.Clear();
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000E3348 File Offset: 0x000E1548
		public void uncreateLobby()
		{
			if (SteamAPI.IsSteamRunning())
			{
				for (int i = 0; i < this.createdLobby.Count; i++)
				{
					SteamMatchmaking.LeaveLobby(this.createdLobby[i]);
				}
				this.createdLobby.Clear();
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000E338E File Offset: 0x000E158E
		public bool inviteFriend(CSteamID ID)
		{
			if (SteamAPI.IsSteamRunning())
			{
				SteamFriends.ActivateGameOverlayInviteDialog(ID);
				return true;
			}
			return false;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000E33A0 File Offset: 0x000E15A0
		public bool inthisLobby(CSteamID lobbyID)
		{
			if (SteamAPI.IsSteamRunning())
			{
				CSteamID steamID = SteamUser.GetSteamID();
				for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(lobbyID); i++)
				{
					if (SteamMatchmaking.GetLobbyMemberByIndex(lobbyID, i) == steamID)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000E33E0 File Offset: 0x000E15E0
		public void grabDayCharState(CSteamID lobbyid)
		{
			this.lobbyChar = "";
			this.lobbyDay = "null";
			this.lobbyState = "";
			this.lobbyVersion = "";
			this.lobbyVersion4 = "";
			this.lobbyVersion6 = "";
			this.lobbyPassword = "";
			if (SteamAPI.IsSteamRunning())
			{
				this.lobbyDay = SteamMatchmaking.GetLobbyData(lobbyid, "day");
				this.lobbyChar = SteamMatchmaking.GetLobbyData(lobbyid, "char");
				this.lobbyState = SteamMatchmaking.GetLobbyData(lobbyid, "state");
				this.lobbyVersion = SteamMatchmaking.GetLobbyData(lobbyid, "version");
				this.lobbyPassword = SteamMatchmaking.GetLobbyData(lobbyid, "password");
				if (this.lobbyDay == "")
				{
					this.lobbyDay = "null";
				}
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000E34B3 File Offset: 0x000E16B3
		public void closeSession(CSteamID id)
		{
			if (SteamAPI.IsSteamRunning())
			{
				SteamNetworking.CloseP2PSessionWithUser(id);
			}
		}

		// Token: 0x04000FE5 RID: 4069
		private SoundEffect signal;

		// Token: 0x04000FE6 RID: 4070
		private ScreenManager sc;

		// Token: 0x04000FE7 RID: 4071
		public bool ALLClear = true;

		// Token: 0x04000FE8 RID: 4072
		public int ALLCount;

		// Token: 0x04000FE9 RID: 4073
		public string ver = "";

		// Token: 0x04000FEA RID: 4074
		public int paintVersion = 59;

		// Token: 0x04000FEB RID: 4075
		public int versionNumber = 18;

		// Token: 0x04000FEC RID: 4076
		public int version4Player = 39;

		// Token: 0x04000FED RID: 4077
		public int version6Player = 25;

		// Token: 0x04000FEE RID: 4078
		public int version4Tunnel = 160;

		// Token: 0x04000FEF RID: 4079
		public CSteamID inviteLobbyID;

		// Token: 0x04000FF0 RID: 4080
		public CSteamID inviteFriendID;

		// Token: 0x04000FF1 RID: 4081
		public bool inviteRequest;

		// Token: 0x04000FF2 RID: 4082
		public string inviteName = "NO";

		// Token: 0x04000FF3 RID: 4083
		public bool inLobby;

		// Token: 0x04000FF4 RID: 4084
		public string lobbyplayers = "";

		// Token: 0x04000FF5 RID: 4085
		public string friendnum = "";

		// Token: 0x04000FF6 RID: 4086
		public string nickname = "Player";

		// Token: 0x04000FF7 RID: 4087
		public string password = "";

		// Token: 0x04000FF8 RID: 4088
		public string players = "2";

		// Token: 0x04000FF9 RID: 4089
		public string lobbyVersion = "";

		// Token: 0x04000FFA RID: 4090
		public string lobbyVersion4 = "";

		// Token: 0x04000FFB RID: 4091
		public string lobbyVersion6 = "";

		// Token: 0x04000FFC RID: 4092
		public string lobbyState = "";

		// Token: 0x04000FFD RID: 4093
		public string lobbyDay = "";

		// Token: 0x04000FFE RID: 4094
		public string lobbyChar = "";

		// Token: 0x04000FFF RID: 4095
		public string lobbyName = "";

		// Token: 0x04001000 RID: 4096
		public string lobbyPassword = "";

		// Token: 0x04001001 RID: 4097
		public CallResult<LobbyCreated_t> lobbyCreatedResult;

		// Token: 0x04001002 RID: 4098
		public CallResult<LobbyMatchList_t> lobbyRequestResult;

		// Token: 0x04001003 RID: 4099
		public CallResult<LobbyEnter_t> lobbyEnteredResult;

		// Token: 0x04001004 RID: 4100
		public Callback<GameLobbyJoinRequested_t> lobbyJoinRequest;

		// Token: 0x04001005 RID: 4101
		public List<Lobby.dummyOwner> testlist = new List<Lobby.dummyOwner>();

		// Token: 0x04001006 RID: 4102
		public List<Lobby.lobbyowner> lobbys = new List<Lobby.lobbyowner>();

		// Token: 0x04001007 RID: 4103
		public List<CSteamID> joinedLobby = new List<CSteamID>();

		// Token: 0x04001008 RID: 4104
		public List<CSteamID> createdLobby = new List<CSteamID>();

		// Token: 0x0200006A RID: 106
		public class dummyOwner : IComparable<Lobby.dummyOwner>
		{
			// Token: 0x060003D8 RID: 984 RVA: 0x000E34C3 File Offset: 0x000E16C3
			public dummyOwner(CSteamID id, string name, Color cc)
			{
				this.id = id;
				this.name = name;
				this.mycolor = cc;
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x000E34F6 File Offset: 0x000E16F6
			public int CompareTo(Lobby.dummyOwner other)
			{
				return this.id.m_SteamID.CompareTo(other.id.m_SteamID);
			}

			// Token: 0x04001009 RID: 4105
			public CSteamID id;

			// Token: 0x0400100A RID: 4106
			public string name = "empty-slot";

			// Token: 0x0400100B RID: 4107
			public Color mycolor = Color.White;
		}

		// Token: 0x0200006B RID: 107
		public class lobbyowner
		{
			// Token: 0x0400100C RID: 4108
			public CSteamID id;

			// Token: 0x0400100D RID: 4109
			public string name = "empty-slot";

			// Token: 0x0400100E RID: 4110
			public string state = "null";
		}
	}
}
