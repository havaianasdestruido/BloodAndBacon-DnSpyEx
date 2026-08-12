using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000060 RID: 96
	public class Cursor
	{
		// Token: 0x06000376 RID: 886 RVA: 0x000D5CB0 File Offset: 0x000D3EB0
		public void LoadContent(ContentManager content, GraphicsDevice device, ref Model model1, ref Model doorModel, ref Model model2)
		{
			this.gr = device;
			this.Position = new Vector2((float)this.gr.Viewport.Width, (float)this.gr.Viewport.Height) / 2f;
			this.pickedTriangle = new Vector3[3];
			this.pickedTriangle[0] = Vector3.Zero;
			this.pickedTriangle[1] = Vector3.Zero;
			this.pickedTriangle[2] = Vector3.Zero;
			this.tagDataDoor = (Dictionary<string, object>)doorModel.Tag;
			this.doorVertices = (Vector3[])this.tagDataDoor["Vertices"];
			this.tagData = (Dictionary<string, object>)model1.Tag;
			this.boundingSphere = (BoundingSphere)this.tagData["BoundingSphere"];
			this.vertices = (Vector3[])this.tagData["Vertices"];
			this.tagData2 = (Dictionary<string, object>)model2.Tag;
			this.vertices1 = (Vector3[])this.tagData["Vertices"];
			this.vertices2 = (Vector3[])this.tagData2["Vertices"];
			this.minLength = this.vertices1.Length;
			this.maxLength = this.vertices1.Length + this.vertices2.Length;
			Array.Resize<Vector3>(ref this.vertices, this.vertices1.Length + this.vertices2.Length);
			for (int i = 0; i < this.vertices1.Length; i++)
			{
				this.vertices[i] = this.vertices1[i];
			}
			for (int j = 0; j < this.vertices2.Length; j++)
			{
				this.vertices[j + this.vertices1.Length] = this.vertices2[j];
			}
			this.myBox = new BoundingBox(Vector3.Zero, Vector3.Up);
			this.myTarget = new BoundingSphere(Vector3.Zero, 5f);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000D5EEC File Offset: 0x000D40EC
		public void addTriangles()
		{
			this.vertLength = this.maxLength;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000D5EFA File Offset: 0x000D40FA
		public void delTriangles()
		{
			this.vertLength = this.minLength;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000D5F08 File Offset: 0x000D4108
		public void addTunnel(Model tunnel, ref Model door1, ref Model crypt)
		{
			this.tagDataTunnel = (Dictionary<string, object>)tunnel.Tag;
			this.tunnelvertices3 = (Vector3[])this.tagDataTunnel["Vertices"];
			this.minLength = this.vertices1.Length + this.tunnelvertices3.Length;
			this.maxLength = this.vertices1.Length + this.tunnelvertices3.Length + this.vertices2.Length;
			this.vertLength = this.maxLength;
			Array.Resize<Vector3>(ref this.vertices, this.vertices1.Length + this.vertices2.Length + this.tunnelvertices3.Length);
			for (int i = 0; i < this.vertices1.Length; i++)
			{
				this.vertices[i] = this.vertices1[i];
			}
			for (int j = 0; j < this.tunnelvertices3.Length; j++)
			{
				this.vertices[j + this.vertices1.Length] = this.tunnelvertices3[j];
			}
			for (int k = 0; k < this.vertices2.Length; k++)
			{
				this.vertices[k + this.vertices1.Length + this.tunnelvertices3.Length] = this.vertices2[k];
			}
			this.tagDataDoor1 = (Dictionary<string, object>)door1.Tag;
			this.door1Vertices = (Vector3[])this.tagDataDoor1["Vertices"];
			this.tagDataDoor2 = (Dictionary<string, object>)crypt.Tag;
			this.cryptVertices = (Vector3[])this.tagDataDoor2["Vertices"];
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000D60C0 File Offset: 0x000D42C0
		public void addTunnel2(Model tunnel, ref Model door1, ref Model door2, ref Model crypt, ref Model crypt2)
		{
			this.tagDataTunnel = (Dictionary<string, object>)tunnel.Tag;
			this.tunnelvertices3 = (Vector3[])this.tagDataTunnel["Vertices"];
			this.tagDataDoor1 = (Dictionary<string, object>)door1.Tag;
			this.door1Vertices = (Vector3[])this.tagDataDoor1["Vertices"];
			this.tagDataDoorH = (Dictionary<string, object>)door2.Tag;
			this.door2Vertices = (Vector3[])this.tagDataDoorH["Vertices"];
			this.tagDataDoor2 = (Dictionary<string, object>)crypt.Tag;
			this.cryptVertices = (Vector3[])this.tagDataDoor2["Vertices"];
			this.tagDataDoor3 = (Dictionary<string, object>)crypt2.Tag;
			this.cryptVertices2 = (Vector3[])this.tagDataDoor3["Vertices"];
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000D61B0 File Offset: 0x000D43B0
		public float? hitBox(Vector3 nearPoint, Vector3 farPoint, Vector3 min, Vector3 max)
		{
			this.direction = farPoint - nearPoint;
			this.direction.Normalize();
			this.ray.Position = nearPoint;
			this.ray.Direction = this.direction;
			this.myBox.Min = min;
			this.myBox.Max = max;
			return this.myBox.Intersects(this.ray);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000D621C File Offset: 0x000D441C
		public float? hitSphere(Vector3 nearPoint, Vector3 farPoint, Vector3 cent, float radius)
		{
			this.direction = farPoint - nearPoint;
			this.direction.Normalize();
			this.ray.Position = nearPoint;
			this.ray.Direction = this.direction;
			this.myTarget.Center = cent;
			this.myTarget.Radius = radius;
			return this.myTarget.Intersects(this.ray);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000D6288 File Offset: 0x000D4488
		public float? hitSphere2(Vector3 nearPoint, Vector3 farPoint, Vector3 cent, float radius)
		{
			this.direction = farPoint - nearPoint;
			this.direction.Normalize();
			this.ray.Position = nearPoint;
			this.ray.Direction = this.direction;
			this.rayDir = this.ray.Direction;
			this.rayPos = this.ray.Position;
			this.myTarget.Center = cent;
			this.myTarget.Radius = radius;
			return this.myTarget.Intersects(this.ray);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000D6318 File Offset: 0x000D4518
		public Ray CalculateCursorRay(Matrix projectionMatrix, Matrix viewMatrix)
		{
			this.nearSource = new Vector3(this.Position, 0f);
			this.farSource = new Vector3(this.Position, 1f);
			this.nearPoint = this.gr.Viewport.Unproject(this.nearSource, projectionMatrix, viewMatrix, Matrix.Identity);
			this.farPoint = this.gr.Viewport.Unproject(this.farSource, projectionMatrix, viewMatrix, Matrix.Identity);
			this.direction = this.farPoint - this.nearPoint;
			this.direction.Normalize();
			this.ray.Direction = this.direction;
			this.ray.Position = this.nearPoint;
			return this.ray;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000D63E8 File Offset: 0x000D45E8
		public void RayIntersectsModel3(Ray ray, Matrix modelTransform, Matrix doorTransform)
		{
			Matrix matrix = modelTransform;
			this.inverseTransform = Matrix.Invert(modelTransform);
			ray.Position = Vector3.Transform(ray.Position, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(ray.Direction, this.inverseTransform);
			this.isDoor = false;
			if (this.boundingSphere.Intersects(ray) == null)
			{
				this.closestIntersection = 10000f;
				return;
			}
			this.closestIntersection = 10000f;
			this.i = 0;
			while (this.i < this.vertLength)
			{
				this.intersection = 10000f;
				Cursor.RayIntersectsTriangle(ref ray, ref this.vertices[this.i], ref this.vertices[this.i + 1], ref this.vertices[this.i + 2], out this.intersection);
				if (this.intersection != 10000f && (this.closestIntersection == 10000f || this.intersection < this.closestIntersection))
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.vertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.vertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.vertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
				}
				this.i += 3;
			}
			this.inverseTransform = Matrix.Invert(doorTransform);
			ray.Position = Vector3.Transform(ray.Position, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(ray.Direction, this.inverseTransform);
			modelTransform = matrix;
			modelTransform *= doorTransform;
			this.i = 0;
			while (this.i < this.doorVertices.Length)
			{
				Cursor.RayIntersectsTriangle(ref ray, ref this.doorVertices[this.i], ref this.doorVertices[this.i + 1], ref this.doorVertices[this.i + 2], out this.intersection);
				if (this.intersection < this.closestIntersection)
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.doorVertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.doorVertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.doorVertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
					this.isDoor = true;
				}
				this.i += 3;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000D66DC File Offset: 0x000D48DC
		public void RayInteresectGeneric(Ray ray, Matrix myTransform, ref Vector3[] verts, ref bool mybool)
		{
			this.inverseTransform = Matrix.Invert(myTransform);
			ray.Position = Vector3.Transform(this.rayPos, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(this.rayDir, this.inverseTransform);
			if (this.boundingSphere.Intersects(ray) != null || this.ignorebounds)
			{
				Matrix matrix = myTransform;
				this.i = 0;
				while (this.i < verts.Length)
				{
					Cursor.RayIntersectsTriangle(ref ray, ref verts[this.i], ref verts[this.i + 1], ref verts[this.i + 2], out this.intersection);
					if (this.intersection < this.closestIntersection)
					{
						this.closestIntersection = this.intersection;
						Vector3.Transform(ref verts[this.i], ref matrix, out this.pickedTriangle[0]);
						Vector3.Transform(ref verts[this.i + 1], ref matrix, out this.pickedTriangle[1]);
						Vector3.Transform(ref verts[this.i + 2], ref matrix, out this.pickedTriangle[2]);
						mybool = true;
					}
					this.i += 3;
				}
			}
			this.ignorebounds = false;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000D6834 File Offset: 0x000D4A34
		public void RayIntersectsModel2(Ray ray, Matrix modelTransform, Matrix doorTransform, Matrix doorTransform1, Matrix doorTransform2, Matrix doorTransform3, Matrix cryptTransform)
		{
			this.rayDir = (this.rayPos = (this.pickedTriangle[0] = (this.pickedTriangle[1] = (this.pickedTriangle[2] = Vector3.Zero))));
			this.rayDir = ray.Direction;
			this.rayPos = ray.Position;
			Matrix matrix = modelTransform;
			this.inverseTransform = Matrix.Invert(modelTransform);
			ray.Position = Vector3.Transform(ray.Position, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(ray.Direction, this.inverseTransform);
			this.isDoor = false;
			this.isDoor1 = false;
			this.isDoor2 = false;
			this.isDoor3 = false;
			this.isCrypt = false;
			if (this.boundingSphere.Intersects(ray) == null)
			{
				this.closestIntersection = 10000f;
				return;
			}
			this.closestIntersection = 10000f;
			this.i = 0;
			while (this.i < this.vertLength)
			{
				this.intersection = 10000f;
				Cursor.RayIntersectsTriangle(ref ray, ref this.vertices[this.i], ref this.vertices[this.i + 1], ref this.vertices[this.i + 2], out this.intersection);
				if (this.intersection != 10000f && (this.closestIntersection == 10000f || this.intersection < this.closestIntersection))
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.vertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.vertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.vertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
				}
				this.i += 3;
			}
			this.inverseTransform = Matrix.Invert(doorTransform);
			ray.Position = Vector3.Transform(ray.Position, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(ray.Direction, this.inverseTransform);
			modelTransform = matrix;
			modelTransform *= doorTransform;
			this.i = 0;
			while (this.i < this.doorVertices.Length)
			{
				Cursor.RayIntersectsTriangle(ref ray, ref this.doorVertices[this.i], ref this.doorVertices[this.i + 1], ref this.doorVertices[this.i + 2], out this.intersection);
				if (this.intersection < this.closestIntersection)
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.doorVertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.doorVertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.doorVertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
					this.isDoor = true;
				}
				this.i += 3;
			}
			this.inverseTransform = Matrix.Invert(doorTransform1);
			ray.Position = Vector3.Transform(this.rayPos, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(this.rayDir, this.inverseTransform);
			modelTransform = doorTransform1;
			this.i = 0;
			while (this.i < this.door1Vertices.Length)
			{
				Cursor.RayIntersectsTriangle(ref ray, ref this.door1Vertices[this.i], ref this.door1Vertices[this.i + 1], ref this.door1Vertices[this.i + 2], out this.intersection);
				if (this.intersection < this.closestIntersection)
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.door1Vertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.door1Vertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.door1Vertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
					this.isDoor1 = true;
				}
				this.i += 3;
			}
			this.inverseTransform = Matrix.Invert(doorTransform2);
			ray.Position = Vector3.Transform(this.rayPos, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(this.rayDir, this.inverseTransform);
			modelTransform = doorTransform2;
			this.i = 0;
			while (this.i < this.door1Vertices.Length)
			{
				Cursor.RayIntersectsTriangle(ref ray, ref this.door1Vertices[this.i], ref this.door1Vertices[this.i + 1], ref this.door1Vertices[this.i + 2], out this.intersection);
				if (this.intersection < this.closestIntersection)
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.door1Vertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.door1Vertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.door1Vertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
					this.isDoor2 = true;
				}
				this.i += 3;
			}
			this.inverseTransform = Matrix.Invert(doorTransform3);
			ray.Position = Vector3.Transform(this.rayPos, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(this.rayDir, this.inverseTransform);
			modelTransform = doorTransform3;
			this.i = 0;
			while (this.i < this.door1Vertices.Length)
			{
				Cursor.RayIntersectsTriangle(ref ray, ref this.door1Vertices[this.i], ref this.door1Vertices[this.i + 1], ref this.door1Vertices[this.i + 2], out this.intersection);
				if (this.intersection < this.closestIntersection)
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.door1Vertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.door1Vertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.door1Vertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
					this.isDoor3 = true;
				}
				this.i += 3;
			}
			this.inverseTransform = Matrix.Invert(cryptTransform);
			ray.Position = Vector3.Transform(this.rayPos, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(this.rayDir, this.inverseTransform);
			modelTransform = cryptTransform;
			this.i = 0;
			while (this.i < this.cryptVertices.Length)
			{
				Cursor.RayIntersectsTriangle(ref ray, ref this.cryptVertices[this.i], ref this.cryptVertices[this.i + 1], ref this.cryptVertices[this.i + 2], out this.intersection);
				if (this.intersection < this.closestIntersection)
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.cryptVertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.cryptVertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.cryptVertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
					this.isCrypt = true;
				}
				this.i += 3;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000D70C8 File Offset: 0x000D52C8
		public void RayIntersectsModel(Ray ray, Matrix modelTransform, Matrix doorTransform, bool checkDoor)
		{
			this.rayDir = (this.rayPos = (this.pickedTriangle[0] = (this.pickedTriangle[1] = (this.pickedTriangle[2] = Vector3.Zero))));
			this.rayDir = ray.Direction;
			this.rayPos = ray.Position;
			this.inverseTransform = Matrix.Invert(modelTransform);
			ray.Position = Vector3.Transform(ray.Position, this.inverseTransform);
			ray.Direction = Vector3.TransformNormal(ray.Direction, this.inverseTransform);
			this.isDoor = false;
			this.isDoor1 = false;
			this.isDoor2 = false;
			this.isDoor3 = false;
			this.isCrypt = false;
			if (this.boundingSphere.Intersects(ray) == null)
			{
				this.closestIntersection = 10000f;
				return;
			}
			this.closestIntersection = 10000f;
			this.i = 0;
			while (this.i < this.vertLength)
			{
				this.intersection = 10000f;
				Cursor.RayIntersectsTriangle(ref ray, ref this.vertices[this.i], ref this.vertices[this.i + 1], ref this.vertices[this.i + 2], out this.intersection);
				if (this.intersection != 10000f && (this.closestIntersection == 10000f || this.intersection < this.closestIntersection))
				{
					this.closestIntersection = this.intersection;
					Vector3.Transform(ref this.vertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
					Vector3.Transform(ref this.vertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
					Vector3.Transform(ref this.vertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
				}
				this.i += 3;
			}
			if (checkDoor)
			{
				this.inverseTransform = Matrix.Invert(doorTransform);
				ray.Position = Vector3.Transform(ray.Position, this.inverseTransform);
				ray.Direction = Vector3.TransformNormal(ray.Direction, this.inverseTransform);
				modelTransform *= doorTransform;
				this.i = 0;
				while (this.i < this.doorVertices.Length)
				{
					Cursor.RayIntersectsTriangle(ref ray, ref this.doorVertices[this.i], ref this.doorVertices[this.i + 1], ref this.doorVertices[this.i + 2], out this.intersection);
					if (this.intersection < this.closestIntersection)
					{
						this.closestIntersection = this.intersection;
						Vector3.Transform(ref this.doorVertices[this.i], ref modelTransform, out this.pickedTriangle[0]);
						Vector3.Transform(ref this.doorVertices[this.i + 1], ref modelTransform, out this.pickedTriangle[1]);
						Vector3.Transform(ref this.doorVertices[this.i + 2], ref modelTransform, out this.pickedTriangle[2]);
						this.isDoor = true;
					}
					this.i += 3;
				}
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000D7444 File Offset: 0x000D5644
		private static void RayIntersectsTriangle(ref Ray ray, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out float result)
		{
			Vector3.Subtract(ref vertex2, ref vertex1, out Cursor.edge1);
			Vector3.Subtract(ref vertex3, ref vertex1, out Cursor.edge2);
			Vector3 vector;
			Vector3.Cross(ref ray.Direction, ref Cursor.edge2, out vector);
			Vector3.Dot(ref Cursor.edge1, ref vector, out Cursor.determinant);
			if (Cursor.determinant > -1E-45f && Cursor.determinant < 1E-45f)
			{
				result = 10000f;
				return;
			}
			Cursor.inverseDeterminant = 1f / Cursor.determinant;
			Vector3.Subtract(ref ray.Position, ref vertex1, out Cursor.distanceVector);
			Vector3.Dot(ref Cursor.distanceVector, ref vector, out Cursor.triangleU);
			Cursor.triangleU *= Cursor.inverseDeterminant;
			if (Cursor.triangleU < 0f || Cursor.triangleU > 1f)
			{
				result = 10000f;
				return;
			}
			Vector3.Cross(ref Cursor.distanceVector, ref Cursor.edge1, out Cursor.distanceCrossEdge1);
			Vector3.Dot(ref ray.Direction, ref Cursor.distanceCrossEdge1, out Cursor.triangleV);
			Cursor.triangleV *= Cursor.inverseDeterminant;
			if (Cursor.triangleV < 0f || Cursor.triangleU + Cursor.triangleV > 1f)
			{
				result = 10000f;
				return;
			}
			Vector3.Dot(ref Cursor.edge2, ref Cursor.distanceCrossEdge1, out Cursor.rayDistance);
			Cursor.rayDistance *= Cursor.inverseDeterminant;
			if (Cursor.rayDistance < 0f)
			{
				result = 10000f;
				return;
			}
			result = Cursor.rayDistance;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000D75B4 File Offset: 0x000D57B4
		public void findAdjacent(bool isDoor, Matrix modelTransform, Matrix doorTransform, Vector3 a, Vector3 b, Vector3 c, out Vector3 found)
		{
			found = Vector3.Zero;
			if (isDoor)
			{
				for (int i = 0; i < this.doorVertices.Length; i += 3)
				{
					this.v1 = Vector3.Transform(this.doorVertices[i], modelTransform * doorTransform);
					this.v2 = Vector3.Transform(this.doorVertices[i + 1], modelTransform * doorTransform);
					this.v3 = Vector3.Transform(this.doorVertices[i + 2], modelTransform * doorTransform);
					if ((!(a != this.v1) || !(a != this.v2) || !(a != this.v3)) && (!(b != this.v1) || !(b != this.v2) || !(b != this.v3)) && !(c == this.v1) && !(c == this.v2) && !(c == this.v3))
					{
						if (a == this.v2 && b == this.v3)
						{
							found = this.v1;
							return;
						}
						if (b == this.v2 && a == this.v3)
						{
							found = this.v1;
							return;
						}
						if (a == this.v1 && b == this.v3)
						{
							found = this.v2;
							return;
						}
						if (b == this.v1 && a == this.v3)
						{
							found = this.v2;
							return;
						}
						if (a == this.v1 && b == this.v2)
						{
							found = this.v3;
							return;
						}
						if (b == this.v1 && a == this.v2)
						{
							found = this.v3;
							return;
						}
					}
				}
				return;
			}
			for (int j = 0; j < this.vertLength; j += 3)
			{
				this.v1 = Vector3.Transform(this.vertices[j], modelTransform);
				this.v2 = Vector3.Transform(this.vertices[j + 1], modelTransform);
				this.v3 = Vector3.Transform(this.vertices[j + 2], modelTransform);
				if ((!(a != this.v1) || !(a != this.v2) || !(a != this.v3)) && (!(b != this.v1) || !(b != this.v2) || !(b != this.v3)) && !(c == this.v1) && !(c == this.v2) && !(c == this.v3))
				{
					if (a == this.v2 && b == this.v3)
					{
						found = this.v1;
						return;
					}
					if (b == this.v2 && a == this.v3)
					{
						found = this.v1;
						return;
					}
					if (a == this.v1 && b == this.v3)
					{
						found = this.v2;
						return;
					}
					if (b == this.v1 && a == this.v3)
					{
						found = this.v2;
						return;
					}
					if (a == this.v1 && b == this.v2)
					{
						found = this.v3;
						return;
					}
					if (b == this.v1 && a == this.v2)
					{
						found = this.v3;
						return;
					}
				}
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000D7A08 File Offset: 0x000D5C08
		public void findAdjacentGen(ref Vector3[] vertices, Matrix modelTransform, Vector3 a, Vector3 b, Vector3 c, out Vector3 found)
		{
			found = Vector3.Zero;
			for (int i = 0; i < vertices.Length; i += 3)
			{
				this.v1 = Vector3.Transform(vertices[i], modelTransform);
				this.v2 = Vector3.Transform(vertices[i + 1], modelTransform);
				this.v3 = Vector3.Transform(vertices[i + 2], modelTransform);
				if ((!(a != this.v1) || !(a != this.v2) || !(a != this.v3)) && (!(b != this.v1) || !(b != this.v2) || !(b != this.v3)) && !(c == this.v1) && !(c == this.v2) && !(c == this.v3))
				{
					if (a == this.v2 && b == this.v3)
					{
						found = this.v1;
						return;
					}
					if (b == this.v2 && a == this.v3)
					{
						found = this.v1;
						return;
					}
					if (a == this.v1 && b == this.v3)
					{
						found = this.v2;
						return;
					}
					if (b == this.v1 && a == this.v3)
					{
						found = this.v2;
						return;
					}
					if (a == this.v1 && b == this.v2)
					{
						found = this.v3;
						return;
					}
					if (b == this.v1 && a == this.v2)
					{
						found = this.v3;
						return;
					}
				}
			}
		}

		// Token: 0x04000E72 RID: 3698
		private const float CursorSpeed = 250f;

		// Token: 0x04000E73 RID: 3699
		private GraphicsDevice gr;

		// Token: 0x04000E74 RID: 3700
		public bool ignorebounds;

		// Token: 0x04000E75 RID: 3701
		public Vector2 Position;

		// Token: 0x04000E76 RID: 3702
		public Vector3 rayPos = Vector3.Zero;

		// Token: 0x04000E77 RID: 3703
		public Vector3 rayDir = Vector3.Zero;

		// Token: 0x04000E78 RID: 3704
		public bool isTunnel;

		// Token: 0x04000E79 RID: 3705
		public bool isDoor;

		// Token: 0x04000E7A RID: 3706
		public bool isDoor1;

		// Token: 0x04000E7B RID: 3707
		public bool isDoor2;

		// Token: 0x04000E7C RID: 3708
		public bool isDoor3;

		// Token: 0x04000E7D RID: 3709
		public bool isCrypt;

		// Token: 0x04000E7E RID: 3710
		public bool isCrypt2;

		// Token: 0x04000E7F RID: 3711
		public float closestIntersection = float.MaxValue;

		// Token: 0x04000E80 RID: 3712
		public Vector3[] pickedTriangle;

		// Token: 0x04000E81 RID: 3713
		private Matrix inverseTransform;

		// Token: 0x04000E82 RID: 3714
		private Dictionary<string, object> tagData;

		// Token: 0x04000E83 RID: 3715
		private Dictionary<string, object> tagData2;

		// Token: 0x04000E84 RID: 3716
		private Dictionary<string, object> tagDataDoor;

		// Token: 0x04000E85 RID: 3717
		private Dictionary<string, object> tagDataTunnel;

		// Token: 0x04000E86 RID: 3718
		private Dictionary<string, object> tagDataDoor1;

		// Token: 0x04000E87 RID: 3719
		private Dictionary<string, object> tagDataDoor2;

		// Token: 0x04000E88 RID: 3720
		private Dictionary<string, object> tagDataDoor3;

		// Token: 0x04000E89 RID: 3721
		private Dictionary<string, object> tagDataDoorH;

		// Token: 0x04000E8A RID: 3722
		private BoundingSphere boundingSphere;

		// Token: 0x04000E8B RID: 3723
		public Vector3[] vertices;

		// Token: 0x04000E8C RID: 3724
		public Vector3[] vertices1;

		// Token: 0x04000E8D RID: 3725
		public Vector3[] vertices2;

		// Token: 0x04000E8E RID: 3726
		public Vector3[] doorVertices;

		// Token: 0x04000E8F RID: 3727
		public Vector3[] tunnelvertices3;

		// Token: 0x04000E90 RID: 3728
		public Vector3[] door1Vertices;

		// Token: 0x04000E91 RID: 3729
		public Vector3[] cryptVertices;

		// Token: 0x04000E92 RID: 3730
		public Vector3[] cryptVertices2;

		// Token: 0x04000E93 RID: 3731
		public Vector3[] door2Vertices;

		// Token: 0x04000E94 RID: 3732
		private int vertLength;

		// Token: 0x04000E95 RID: 3733
		private int maxLength;

		// Token: 0x04000E96 RID: 3734
		private int minLength;

		// Token: 0x04000E97 RID: 3735
		private float intersection;

		// Token: 0x04000E98 RID: 3736
		private static Vector3 edge1;

		// Token: 0x04000E99 RID: 3737
		private static Vector3 edge2;

		// Token: 0x04000E9A RID: 3738
		private static float determinant;

		// Token: 0x04000E9B RID: 3739
		private static float inverseDeterminant;

		// Token: 0x04000E9C RID: 3740
		private static Vector3 distanceVector;

		// Token: 0x04000E9D RID: 3741
		private static float triangleU;

		// Token: 0x04000E9E RID: 3742
		private static Vector3 distanceCrossEdge1;

		// Token: 0x04000E9F RID: 3743
		private static float triangleV;

		// Token: 0x04000EA0 RID: 3744
		private static float rayDistance;

		// Token: 0x04000EA1 RID: 3745
		private Vector3 v1;

		// Token: 0x04000EA2 RID: 3746
		private Vector3 v2;

		// Token: 0x04000EA3 RID: 3747
		private Vector3 v3;

		// Token: 0x04000EA4 RID: 3748
		private Vector3 nearSource;

		// Token: 0x04000EA5 RID: 3749
		private Vector3 farSource;

		// Token: 0x04000EA6 RID: 3750
		private Vector3 nearPoint;

		// Token: 0x04000EA7 RID: 3751
		private Vector3 farPoint;

		// Token: 0x04000EA8 RID: 3752
		private Vector3 direction;

		// Token: 0x04000EA9 RID: 3753
		private Ray ray;

		// Token: 0x04000EAA RID: 3754
		private int i;

		// Token: 0x04000EAB RID: 3755
		private BoundingBox myBox;

		// Token: 0x04000EAC RID: 3756
		private BoundingSphere myTarget;
	}
}
