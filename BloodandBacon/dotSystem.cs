using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000031 RID: 49
	internal class dotSystem : ParticleSystem
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0004B080 File Offset: 0x00049280
		public dotSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0004B08C File Offset: 0x0004928C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "dot";
			settings.MaxParticles = 4000;
			settings.Duration = TimeSpan.FromSeconds(4.199999809265137);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(100, 100, 245, 120);
			settings.MaxColor = new Color(255, 255, 255, 250);
			settings.MinRotateSpeed = 2f;
			settings.MaxRotateSpeed = 4f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 40f;
			settings.MinEndSize = 3f;
			settings.MaxEndSize = 12f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
