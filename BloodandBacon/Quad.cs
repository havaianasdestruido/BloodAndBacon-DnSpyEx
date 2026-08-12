using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000010 RID: 16
	public class Quad
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0002AC39 File Offset: 0x00028E39
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0002AC41 File Offset: 0x00028E41
		public VertexPositionNormalTexture[] Vertices
		{
			get
			{
				return this._vertices;
			}
			set
			{
				this._vertices = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0002AC4A File Offset: 0x00028E4A
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0002AC52 File Offset: 0x00028E52
		public int[] Indexes
		{
			get
			{
				return this._indexes;
			}
			set
			{
				this._indexes = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0002AC5B File Offset: 0x00028E5B
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0002AC63 File Offset: 0x00028E63
		public Vector3 Origin
		{
			get
			{
				return this._origin;
			}
			set
			{
				this._origin = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0002AC6C File Offset: 0x00028E6C
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0002AC74 File Offset: 0x00028E74
		public Vector3 Normal
		{
			get
			{
				return this._normal;
			}
			set
			{
				this._normal = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0002AC7D File Offset: 0x00028E7D
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0002AC85 File Offset: 0x00028E85
		public Vector3 Up
		{
			get
			{
				return this._up;
			}
			set
			{
				this._up = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0002AC8E File Offset: 0x00028E8E
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0002AC96 File Offset: 0x00028E96
		public Vector3 Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this._left = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0002AC9F File Offset: 0x00028E9F
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0002ACA7 File Offset: 0x00028EA7
		public Vector3 UpperLeft
		{
			get
			{
				return this._upperLeft;
			}
			set
			{
				this._upperLeft = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0002ACB0 File Offset: 0x00028EB0
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		public Vector3 UpperRight
		{
			get
			{
				return this._upperRight;
			}
			set
			{
				this._upperRight = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0002ACC1 File Offset: 0x00028EC1
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0002ACC9 File Offset: 0x00028EC9
		public Vector3 LowerLeft
		{
			get
			{
				return this._lowerLeft;
			}
			set
			{
				this._lowerLeft = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0002ACD2 File Offset: 0x00028ED2
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0002ACDA File Offset: 0x00028EDA
		public Vector3 LowerRight
		{
			get
			{
				return this._lowerRight;
			}
			set
			{
				this._lowerRight = value;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0002ACE4 File Offset: 0x00028EE4
		public Quad(Vector3 origin, Vector3 normal, Vector3 up, float width, float height)
		{
			this.Vertices = new VertexPositionNormalTexture[4];
			this.Indexes = new int[6];
			this.Origin = origin;
			this.Normal = normal;
			this.Up = up;
			this.Left = Vector3.Cross(normal, this.Up);
			Vector3 vector = this.Up * height / 2f + origin;
			this.UpperLeft = vector + this.Left * width / 2f;
			this.UpperRight = vector - this.Left * width / 2f;
			this.LowerLeft = this.UpperLeft - this.Up * height;
			this.LowerRight = this.UpperRight - this.Up * height;
			this.FillVertices();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0002ADDC File Offset: 0x00028FDC
		private void FillVertices()
		{
			Vector2 vector = new Vector2(0f, 0f);
			Vector2 vector2 = new Vector2(1f, 0f);
			Vector2 vector3 = new Vector2(0f, 1f);
			Vector2 vector4 = new Vector2(1f, 1f);
			for (int i = 0; i < this.Vertices.Length; i++)
			{
				this.Vertices[i].Normal = this.Normal;
			}
			this.Vertices[0].Position = this.LowerLeft;
			this.Vertices[0].TextureCoordinate = vector3;
			this.Vertices[1].Position = this.UpperLeft;
			this.Vertices[1].TextureCoordinate = vector;
			this.Vertices[2].Position = this.LowerRight;
			this.Vertices[2].TextureCoordinate = vector4;
			this.Vertices[3].Position = this.UpperRight;
			this.Vertices[3].TextureCoordinate = vector2;
			this.Indexes[0] = 0;
			this.Indexes[1] = 1;
			this.Indexes[2] = 2;
			this.Indexes[3] = 2;
			this.Indexes[4] = 1;
			this.Indexes[5] = 3;
		}

		// Token: 0x040005A6 RID: 1446
		private VertexPositionNormalTexture[] _vertices;

		// Token: 0x040005A7 RID: 1447
		private int[] _indexes;

		// Token: 0x040005A8 RID: 1448
		private Vector3 _origin;

		// Token: 0x040005A9 RID: 1449
		private Vector3 _normal;

		// Token: 0x040005AA RID: 1450
		private Vector3 _up;

		// Token: 0x040005AB RID: 1451
		private Vector3 _left;

		// Token: 0x040005AC RID: 1452
		private Vector3 _upperLeft;

		// Token: 0x040005AD RID: 1453
		private Vector3 _upperRight;

		// Token: 0x040005AE RID: 1454
		private Vector3 _lowerLeft;

		// Token: 0x040005AF RID: 1455
		private Vector3 _lowerRight;
	}
}
