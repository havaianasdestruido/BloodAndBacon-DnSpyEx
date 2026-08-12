using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000B2 RID: 178
	internal class SmokePlumeSettingsSystem : ParticleSystem
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x001489BA File Offset: 0x00146BBA
		public SmokePlumeSettingsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x001489C4 File Offset: 0x00146BC4
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "smoke";
			settings.MaxParticles = 3500;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0.22f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 15f;
			settings.MinVerticalVelocity = 10f;
			settings.MaxVerticalVelocity = 20f;
			settings.Gravity = new Vector3(-20f, -5f, 0f);
			settings.EndVelocity = 0.75f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = -1f;
			settings.MaxRotateSpeed = 1f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 80f;
			settings.MinEndSize = 90f;
			settings.MaxEndSize = 200f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
