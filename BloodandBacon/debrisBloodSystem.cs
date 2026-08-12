using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000B8 RID: 184
	internal class debrisBloodSystem : ParticleSystem
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0014C948 File Offset: 0x0014AB48
		public debrisBloodSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0014C954 File Offset: 0x0014AB54
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bloodgrey2";
			settings.MaxParticles = 150000;
			settings.Duration = TimeSpan.FromSeconds(43.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -10f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(125, 125, 125, 255);
			settings.MaxColor = new Color(125, 125, 125, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 12f;
			settings.MaxStartSize = 12f;
			settings.MinEndSize = 3f;
			settings.MaxEndSize = 3f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
