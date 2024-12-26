using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace samochod
{
    public class AudioManager
    {
        public Dictionary<Sound,SoundEffect> sounds;
        public  Dictionary<string,SoundEffectInstance> instances;
        public SoundEffectInstance themeInstance;
        private bool mute = Game1.audioMute;
        public AudioManager() 
        { 
            sounds = new Dictionary<Sound, SoundEffect>();
            instances = new Dictionary<string,SoundEffectInstance>();
        }

        public SoundEffectInstance Get(Sound sound)
        {
            var xd = sounds[sound].CreateInstance();
            //instances.Add(xd);
            return xd;
        }
        public SoundEffectInstance PlaySound(Sound sound)
        {
            var instance = sounds[sound].CreateInstance();
            instance.Play(); 
            return instance;
        }

        public void StartMusic(Sound song)
        {
            if (themeInstance == null) {
                themeInstance = Get(song);
            }

        }

        public void Update(GameTime gameTime)
        {
            mute = Game1.audioMute;
            if (mute) {
                themeInstance.Volume = 0;
            }
            else if (!mute ||(themeInstance.State != SoundState.Playing))
            {
                themeInstance.Volume = 0.4f;
                themeInstance.Play();
            }
        }

        public void ChangeMusicVolume(float vol)
        {
            Math.Clamp(vol, 0f, 1f);
            MediaPlayer.Volume = vol;
        }
        public void SwitchMute() { Game1.audioMute = !Game1.audioMute;  }

    }
}
