using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000EF RID: 239
	internal class rocketSystem : ParticleSystem
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x001D9552 File Offset: 0x001D7752
		public rocketSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x001D955C File Offset: 0x001D775C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "mistmini2";
			settings.MaxParticles = 2000;
			settings.Duration = TimeSpan.FromSeconds(3.5);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(4f, 2f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(190, 190, 190, 150);
			settings.MaxColor = new Color(210, 210, 210, 195);
			settings.MinRotateSpeed = -1f;
			settings.MaxRotateSpeed = 1f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 34f;
			settings.MinEndSize = 36f;
			settings.MaxEndSize = 45f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
