using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class AudioManager
    {
        public List<Song> songs;
        public Dictionary<Sound,SoundEffect> sounds;

        public AudioManager() 
        { 
            songs = new List<Song>();
            sounds = new Dictionary<Sound, SoundEffect>();
        }


        void PlaySound(Sound sound)
        {
            var instance = sounds[sound].CreateInstance();
            instance.Play();
        }
        public void StartMusic()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(songs[0]);
            
        }
        public void StopMusic() 
        {
            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop(); // stop current audio playback if playing or paused.
            }
        }
        public void UpdateVolume()
        {
            // based on range to win zone
        }
        public void ChangeMusicVolume(float vol)
        {
            Math.Clamp(vol, 0f, 1f);
            MediaPlayer.Volume = vol;
        }
            
    }
}
