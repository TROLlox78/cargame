using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;


namespace samochod
{
    public class AudioManager
    {
        public Dictionary<Sound,SoundEffect> sounds;
        public  Dictionary<string,SoundEffectInstance> instances;

        public SoundEffectInstance themeInstance;
        public AudioManager() 
        { 
            sounds = new Dictionary<Sound, SoundEffect>();
            instances = new Dictionary<string,SoundEffectInstance>();
        }

        public SoundEffectInstance Get(Sound sound)
        {
            return sounds[sound].CreateInstance();
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
                themeInstance.Volume = 0.4f;
            }

            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = 0.4f;
            //MediaPlayer.Play(songs[song]);
            
        }
        public void StopMusic() 
        {
            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop(); // stop current audio playback if playing or paused.
            }
        }
        public void Update(GameTime gameTime)
        {
            if (themeInstance.State != SoundState.Playing)
            {
                themeInstance.Play();
            }
        }
        public void ChangeMusicVolume(float vol)
        {
            Math.Clamp(vol, 0f, 1f);
            MediaPlayer.Volume = vol;
        }
            
    }
}
