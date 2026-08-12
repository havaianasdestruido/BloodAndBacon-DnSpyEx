using System;

namespace Blood
{
	// Token: 0x02000078 RID: 120
	internal class packetHandler
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x000E69D1 File Offset: 0x000E4BD1
		public packetHandler(ref byte[] packet)
		{
			this.thispacket = new byte[packet.Length];
			Array.Copy(packet, this.thispacket, packet.Length);
			this.index = 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000E6A00 File Offset: 0x000E4C00
		public byte ReadByte()
		{
			byte b = this.thispacket[this.index];
			this.index++;
			return b;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000E6A2C File Offset: 0x000E4C2C
		public int ReadInt32()
		{
			int num = BitConverter.ToInt32(this.thispacket, this.index);
			this.index += 4;
			return num;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000E6A5C File Offset: 0x000E4C5C
		public ushort ReadUInt16()
		{
			ushort num = BitConverter.ToUInt16(this.thispacket, this.index);
			this.index += 2;
			return num;
		}

		// Token: 0x04001083 RID: 4227
		private byte[] thispacket;

		// Token: 0x04001084 RID: 4228
		public int index;
	}
}
