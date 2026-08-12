using System;
using Steamworks;

namespace Blood
{
	// Token: 0x02000017 RID: 23
	public class dummyOwner : IComparable<dummyOwner>
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0002C04D File Offset: 0x0002A24D
		public dummyOwner(remotePlayer4 r, CSteamID id)
		{
			this.r = r;
			this.id = id;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0002C063 File Offset: 0x0002A263
		public int CompareTo(dummyOwner other)
		{
			return this.id.m_SteamID.CompareTo(other.id.m_SteamID);
		}

		// Token: 0x040005E1 RID: 1505
		public remotePlayer4 r;

		// Token: 0x040005E2 RID: 1506
		public CSteamID id;
	}
}
