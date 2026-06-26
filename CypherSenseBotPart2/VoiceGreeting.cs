using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace CypherSenseBotPart2
{
 public class VoiceGreeting
    {
        // method that plays the audio
        public static void PlayAudio(string filepath)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = filepath;
            player.Play();
        }
    }
}
